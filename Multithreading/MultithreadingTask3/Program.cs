class Program
{
    static void Main(string[] args)
    {
        var firstMatrix = CreateMatrix();
        Console.WriteLine("Первая матрица");
        PrintMatrix(firstMatrix);

        var secondMatrix = CreateMatrix();
        Console.WriteLine("\nВторая матрица");
        PrintMatrix(secondMatrix);

        var resultMatrix = MatrixMultiplication(firstMatrix, secondMatrix);
        Console.WriteLine("\nРезультат умножения матриц");
        PrintMatrix(resultMatrix);
    }

    static int[,] CreateMatrix()
    {
        var random = new Random();
        var matrix = new int[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                matrix[i, j] = random.Next(1, 10);
            }
        }

        return matrix;
    }
    
    static void PrintMatrix(int[,] matrix)
    {
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4));
            }

            Console.WriteLine();
        }
    }
    static int[,] MatrixMultiplication(int[,] matrixA, int[,] matrixB)
    {
        var matrixC = new int[5, 5];

        Parallel.For(0, 5, (i) =>
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        });

        return matrixC;
    }
}