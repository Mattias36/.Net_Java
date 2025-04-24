using System.Diagnostics; // Low-level

internal class Program
{
    static void Main(string[] args)
    {
        int[] sizes = { 100, 300, 600 };
        int[] threadsList = { 1, 2, 4,  8 };
        int repeats = 5;

        Console.WriteLine("Badania nisko poziomowe");
        Console.WriteLine("Rozmiar\t\tWątki\tSredni czas Watki [ms]\tSekwencyjnie [ms]\tPoprawny wynik");

        foreach (int size in sizes)
        {
            var matrixA = Matrix.GenerateRandom(size, size);
            var matrixB = Matrix.GenerateRandom(size, size);

            // Wynik referencyjny — sekwencyjne mnożenie
            double[,] sequentialResult = null;
            long sequentialTotalTime = 0;
            for (int r = 0; r < repeats; r++)
            {
                var watch = Stopwatch.StartNew();
                sequentialResult = Matrix.MultiplySequential(matrixA, matrixB);
                watch.Stop();
                sequentialTotalTime += watch.ElapsedMilliseconds;
            }
            double avgSequential = sequentialTotalTime / (double)repeats;

            foreach (int threads in threadsList)
            {
                long threadedTotalTime = 0;
                double[,] threadedResult = null;

                for (int r = 0; r < repeats; r++)
                {
                    var watch = Stopwatch.StartNew();
                    threadedResult = Matrix.MultiplyLowLevel(matrixA, matrixB, threads);
                    watch.Stop();
                    threadedTotalTime += watch.ElapsedMilliseconds;
                }

                double avgThreaded = threadedTotalTime / (double)repeats;
                bool areEqual = Matrix.Compare(sequentialResult, threadedResult);
              

                Console.WriteLine($"{size}x{size}\t\t{threads}\t{avgThreaded:F2}\t\t\t{avgSequential:F2}\t\t\t{(areEqual ? "True" : "False")}");
            }

            Console.WriteLine();
        }
    }

   
}
