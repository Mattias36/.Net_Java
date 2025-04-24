// high level
public class Matrix
{
    public static double[,] GenerateRandom(int rows, int cols)
    {
        var rand = new Random();
        var matrix = new double[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix[i, j] = rand.NextDouble() * 10;
        return matrix;
    }
    public static double[,] MultiplySequential(double[,] A, double[,] B)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);

        if (colsA != rowsB)
            throw new ArgumentException("Liczba kolumn A musi być równa liczbie wierszy B");

        double[,] result = new double[rowsA, colsB];

        for (int i = 0; i < rowsA; i++)
            for (int j = 0; j < colsB; j++)
            {
                double sum = 0;
                for (int k = 0; k < colsA; k++)
                    sum += A[i, k] * B[k, j];
                result[i, j] = sum;
            }

        return result;
    }
    public static double[,] Multiply(double[,] A, double[,] B, int threads)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int colsB = B.GetLength(1);
        var result = new double[rowsA, colsB];
        if (colsB != rowsA)
            throw new ArgumentException("Liczba kolumn A musi być równa liczbie wierszy B");

        var options = new ParallelOptions { MaxDegreeOfParallelism = threads };

        Parallel.For(0, rowsA, options, i =>
        {
            for (int j = 0; j < colsB; j++)
            {
                double sum = 0;
                for (int k = 0; k < colsA; k++)
                    sum += A[i, k] * B[k, j];
                result[i, j] = sum;
            }
        });

        return result;
    }

    public static bool Compare(double[,] A, double[,] B, double epsilon = 1e-6)
    {
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            return false;

        for (int i = 0; i < A.GetLength(0); i++)
            for (int j = 0; j < A.GetLength(1); j++)
                if (Math.Abs(A[i, j] - B[i, j]) > epsilon)
                    return false;
        return true;
    }


    public static void Print(double[,] matrix, string name)
    {
        Console.WriteLine($"Macierz {name}:");
        int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write($"{matrix[i, j]:0.00}\t");
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
