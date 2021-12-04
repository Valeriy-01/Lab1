namespace MyTracer
{
    interface ITracer
    {
        public void StartTrace(); // Запуск времени
        public void StopTrace(); // Остановка времени
        TraceResult GetTraceResult(); // Получить результаты
    }
}
