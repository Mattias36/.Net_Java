using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj górny zakres liczb: ");
            if (int.TryParse(Console.ReadLine(), out int upperLimit) && upperLimit > 0)
            {
                FizzBuzz fizzBuzz = new FizzBuzz(upperLimit);
                fizzBuzz.Run();
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową liczbę.");
            }
        }
    }
}
