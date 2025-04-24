using System.Diagnostics; // High-level

internal class Program
{
    static void Main(string[] args)
    {
        int[] sizes = { 100, 300, 600 };      
        int[] threadsList = { 1, 2, 4, 8 }; // liczba rdzeni komputera: 6  
        int repeats = 5;

        Console.WriteLine("Badania wysoko poziomowe");
        Console.WriteLine("Rozmiar\t\tWątki\tSredni czas Watki [ms]\tSekwencyjnie [ms]\tPoprawny wynik");

        foreach (int size in sizes)
        {
            var matrixA = Matrix.GenerateRandom(size, size);
            var matrixB = Matrix.GenerateRandom(size, size);

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
                long totalTime = 0;
                double[,] result = null;
                for (int r = 0; r < repeats; r++)
                {
                    var watch = Stopwatch.StartNew();
                    result = Matrix.Multiply(matrixA, matrixB, threads);
                    watch.Stop();
                    totalTime += watch.ElapsedMilliseconds;
                }

                double avgTime = totalTime / (double)repeats;
                bool areEqual = Matrix.Compare(result,sequentialResult);
                Console.WriteLine($"{size}x{size}\t\t{threads}\t{avgTime:F2}\t\t\t{avgSequential:F2}\t\t\t{(areEqual ? "True" : "False")}");
            }
            Console.WriteLine(); 
        }

    }

}

// punkt 3 obraz gdzies ok. 1000x1000 rozmiar