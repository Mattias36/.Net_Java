// low level
public static class Matrix
{
    public static double[,] GenerateRandom(int rows, int cols)
    {
        Random rand = new Random();
        double[,] matrix = new double[rows, cols];

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
    public static double[,] MultiplyLowLevel(double[,] A, double[,] B, int threads)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);

        if (colsA != rowsB)
            throw new ArgumentException("Liczba kolumn A musi być równa liczbie wierszy B");

        double[,] result = new double[rowsA, colsB];
        Thread[] threadArray = new Thread[threads];
        int chunkSize = rowsA / threads;
        object locker = new object();

        for (int t = 0; t < threads; t++)
        {
            int start = t * chunkSize;
            int end = (t == threads - 1) ? rowsA : start + chunkSize;

            threadArray[t] = new Thread(() =>
            {
                for (int i = start; i < end; i++)
                {
                    for (int j = 0; j < colsB; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < colsA; k++)
                        {
                            sum += A[i, k] * B[k, j];
                        }
                        result[i, j] = sum;
                    }
                }
            });

            threadArray[t].Start();
        }

        foreach (Thread thread in threadArray)
            thread.Join();

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
}
