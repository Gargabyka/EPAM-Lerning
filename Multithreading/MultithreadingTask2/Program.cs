class Program
{
    static void Main(string[] args)
    {
        var task1 = Task.Run(() => CreateMass());
        var task2 = task1.ContinueWith(task => MultiplyMass(task1.Result));
        var task3 = task2.ContinueWith(task => SortMass(task2.Result));
        var task4 = task3.ContinueWith(task => AvgMass(task3.Result));

        Task.WaitAll(task1, task2, task3, task4);
    }

    static int[] CreateMass()
    {
        Console.WriteLine("\nTask 1 - Создание массива");
        var random = new Random();
        int[] mass = new int[10];

        for (int i = 0; i < mass.Length; i++)
        {
            mass[i] = random.Next(1, 10);
        }

        Console.WriteLine("Создан массив:");
        foreach (var m in mass)
        {
            Console.Write($"{m}\t");
        }

        return mass;
    }

    static int[] MultiplyMass(int[] mass)
    {
        Console.WriteLine("\nTask 2 - Умножение массива");
        var random = new Random();
        int number = random.Next(1,10);

        Console.WriteLine($"Число на которое будет умножен массив - {number}");
        
        for (int i = 0; i < mass.Length; i++)
        {
            mass[i] *= number;
        }

        Console.WriteLine("\nВывод умноженного массива");
        foreach (var m in mass)
        {
            Console.Write($"{m}\t");
        }

        return mass;
    }

    static int[] SortMass(int[] mass)
    {
        Console.WriteLine("\nTask 3 - Сортировка массива");

        int temp;
        for (int i = 0; i < mass.Length - 1; i++)
        {
            for (int j = i + 1; j < mass.Length; j++)
            {
                if (mass[i] > mass[j])
                {
                    temp = mass[i];
                    mass[i] = mass[j];
                    mass[j] = temp;
                }
            }
        }
        
        Console.WriteLine("Вывод отсортированного массива");
        foreach (var m in mass)
        {
            Console.Write($"{m}\t");
        }

        return mass;
    }

    static void AvgMass(int[] mass)
    {
        Console.WriteLine("\nTask 4 - Среднее значение массива");
        int sum = 0;
        
        for (int i = 0; i < mass.Length; i++)
        {
            sum += mass[i];
        }

        int result = sum / mass.Length;

        Console.WriteLine($"Среднее значение массива - {result}");
    }
}