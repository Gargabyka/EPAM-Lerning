class Program
{
    static Semaphore sem = new Semaphore(1, 10);

    static void Main(string[] args)
    {
        int value = 10;

        ThreadPool.QueueUserWorkItem(DecrementThread, value);
        while (value > 0 )
        {
            Thread.Sleep(2000);
        }
    }

    static void Start()
    {
        
    }

    static void DecrementThread(object obj)
    {
        int value = (int)obj;
        
        if (value > 0)
        {
            sem.WaitOne();
            Console.WriteLine($"Поток №{value}");
            value--;
            sem.Release();
            ThreadPool.QueueUserWorkItem(DecrementThread, value);
        }
    }
}