using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace MyTracer
{
    public class Tracer : ITracer
    {
        public TraceResult result { get; set; }
        public Tracer()
        {
            result = new TraceResult();
        }

        // Запускаем время
        public void StartTrace()
        {
            ThreadInfo threadInfo = result.threadDict.GetOrAdd(Thread.CurrentThread.ManagedThreadId, new ThreadInfo(Thread.CurrentThread.ManagedThreadId));
            StackTrace stackTrace = new StackTrace();
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            MethodInfo methodInfo = new MethodInfo(method.Name, method.ReflectedType.Name);

            methodInfo.StartCheckingTime();
            if (threadInfo.methodStack.Count == 0)
            {
                threadInfo.methods.Add(methodInfo);
            }
            else
            {
                threadInfo.methodStack.Peek().AddChildren(methodInfo);
            }
            threadInfo.methodStack.Push(methodInfo);
        }

        // Останавливаем время
        public void StopTrace()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            MethodInfo methodInfo = result.threadDict[threadID].methodStack.Pop();
            result.GetExecutionTime();
            methodInfo.StopCheckingTime();
        }

        // Получаем результат
        public TraceResult GetTraceResult()
        {
            return result;
        }
    }
}
