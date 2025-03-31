namespace ConsoleApp1
{
    internal class FizzBuzz
    {
        private int _upperLimit;

        public FizzBuzz(int upperLimit)
        {
            _upperLimit = upperLimit;
        }

        public void Run()
        {
            for (int i = 1; i <= _upperLimit; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
