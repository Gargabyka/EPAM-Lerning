class Program
{
    static AutoResetEvent waitList = new AutoResetEvent(false);
    static AutoResetEvent waitPrint = new AutoResetEvent(true);
    
    /*static object listLocker = new object();
    private static object printLocker = new object();*/

    static List<int> _list = new List<int>();
    static Random rand = new Random();

    static void Main(string[] args)
    {
        ThreadPool.QueueUserWorkItem(ListAdd);
        ThreadPool.QueueUserWorkItem(PrintList);
        Thread.Sleep(2000);
    }

    static void ListAdd(object obj)
    {
        for (int i = 0; i < 10; i++)
        {
            /*lock (listLocker)
            {
                var number = rand.Next(1, 10);
                _list.Add(number);
            }*/
            
            var number = rand.Next(1, 10);
            waitPrint.WaitOne();
            _list.Add(number);
            waitList.Set();
        }
    }

    static void PrintList(object obj)
    {
        for (int i = 0; i < 10; i++)
        {
            /*lock (listLocker)
            {
                Console.WriteLine();
                foreach (var l in _list)
                {
                    Console.Write($"{l} ");
                }
            }*/
            
            waitList.WaitOne();
            Console.WriteLine();
            foreach (var l in _list)
            {
                Console.Write($"{l} ");
            }
            waitPrint.Set();
        }
    }
}