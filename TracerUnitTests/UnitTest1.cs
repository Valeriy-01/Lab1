using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTracer;
using System.Diagnostics;
using System.Threading;

namespace TracerUnitTests
{
    [TestClass]
    public class MyUnitTest
    {
        private Tracer tracer;

        [TestInitialize]
        public void init()
        {
            tracer = new Tracer();
        }
        [TestMethod]
        public void TestMethodA()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tracer.StartTrace();
            Thread.Sleep(1500);
            tracer.StopTrace();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(time >= tracer.GetTraceResult().executionTime + 2 || time <= tracer.GetTraceResult().executionTime + 2);
        }
        [TestMethod]
        public void TestMethodB()
        {
            TestMethodA();
            TestMethodC();
        }
        [TestMethod]
        public void TestMethodC()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tracer.StartTrace();
            TestMethodA();
            tracer.StopTrace();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(time >= tracer.GetTraceResult().executionTime + 2 || time <= tracer.GetTraceResult().executionTime + 2);
        }
       
        [TestMethod]
        public void TestMethodD()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tracer.StartTrace();
            Thread.Sleep(1200);
            tracer.StopTrace();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(time >= tracer.GetTraceResult().executionTime + 2 || time <= tracer.GetTraceResult().executionTime + 2);
        }
    }
}
