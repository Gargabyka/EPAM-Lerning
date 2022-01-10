namespace AsyncAwaitSum
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Action<string, CancellationToken> getCount = async (input, token)=> 
            {
                var result = await GetSumAsync(input, token);
                Console.WriteLine(result);
            };

            string Start()
            {
                return "Старт.\nВведите число. \nДля прерывания текущего процесса нажмите S. \nДля выхода E";
            }

            var input = Console.ReadLine();
            CancellationTokenSource cts = new CancellationTokenSource();
            while (!input.ToUpper().StartsWith("E"))
            {
                Start();
                input = Console.ReadLine();

                if  (input.ToUpper().StartsWith("S"))
                {
                    getCount(input, cts.Token);
                    cts.Cancel();
                    cts.Dispose();
                }
                
                else
                {
                    cts = new CancellationTokenSource(); 
                    getCount(input, cts.Token); 
                }
            }

            Console.WriteLine("До свидания");
        }

        static async Task<string> GetSumAsync(string number, CancellationToken token)
        {
            try
            {
                return await Task.Run((() => GetSum(long.Parse(number), token).ToString()),token);
            }
            catch (Exception e)
            {
                return "Операция отменена";
            }
        }

        static long GetSum(long number, CancellationToken token)
        {
            long result = 0;
            long temp = 0;
            
            for (long i = 0; i <= number; i++)
            {
                token.ThrowIfCancellationRequested();
                temp = i;
                result += temp;
            }

            return result;
        }
    }
}