class Program
{
    private static object locker = new object();
    static void Main(string[] args)
    {
        for (int i = 0; i <= 100; i++)
        {
            Thread myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(i); 
        }
    }
    

    static void Count(object numb)
    {
        lock (locker)
        {
            int number = (int)numb;
        
            for (int i = 0; i <= 1000; i++)
            {
                Console.WriteLine($"Task #{number} – {i}");
            }
        }
    }
}