using System;

namespace KnapsackProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj liczbę przedmiotów: ");
            int liczbaPrzedmiotów = int.Parse(Console.ReadLine());

            Console.Write("Podaj wartość seed: ");
            int seed = int.Parse(Console.ReadLine());

            Console.Write("Podaj pojemność plecaka: ");
            int pojemność = int.Parse(Console.ReadLine());

            Problem problem = new Problem(liczbaPrzedmiotów, seed);
            Console.WriteLine("\nWygenerowane przedmioty:");
            Console.WriteLine(problem);

            Wynik wynik = problem.Solve(pojemność);
            Console.WriteLine("\nRozwiązanie:");
            Console.WriteLine(wynik);
        }
    }
}
