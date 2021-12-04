using System;
using System.Threading;
using MyTracer;

namespace TestApplication
{
    public class Program
    {
        public static Tracer tracer = new Tracer();

        public static void MethodA()
        {
            tracer.StartTrace();
            Thread.Sleep(1156);
            tracer.StopTrace();
        }
        public static void MethodB()
        {
            tracer.StartTrace();
            Thread.Sleep(1523);
            tracer.StopTrace();
        }
        public static void MethodC()
        {
            tracer.StartTrace();
            MethodD();
            tracer.StopTrace();
        }
        public static void MethodD()
        {
            tracer.StartTrace();
            Thread.Sleep(1231);
            tracer.StopTrace();
        }

        public static void Main(string[] args)
        {
            MethodA();
            MethodB();
            MethodC();

            Console.WriteLine("В каком формате сохранить результат?");

            Console.WriteLine("1 - В формате JSON");

            Console.WriteLine("2 - В формате XML");

            string serializeSelect = Console.ReadLine();
            string result;
            if (serializeSelect.Equals("1")) // JSON формат
            {
                JSONSerialization serialization = new JSONSerialization();
                result = serialization.serialize(tracer.GetTraceResult());
            }
            else if (serializeSelect.Equals("2")) // XML формат
            {
                XMLSerialization serialization = new XMLSerialization();
                result = serialization.serialize(tracer.GetTraceResult());
            }
            else 
            {
                Console.WriteLine("Ошибка ввода!");
                return;
            }

            Console.WriteLine("Где вывести результат?");

            Console.WriteLine("1 - В консоле");

            Console.WriteLine("2 - В файле");

            string writeSelect = Console.ReadLine();

            if (writeSelect.Equals("1")) // Вывод в консоль
            {
                ConsoleResultWriter writer = new ConsoleResultWriter();
                writer.WriteResult(result);
            }
            else if (writeSelect.Equals("2")) // Вывод в файл
            {
                FileResultWriter writer = new FileResultWriter();
                writer.WriteResult(result);
            }
        }
    }
}
