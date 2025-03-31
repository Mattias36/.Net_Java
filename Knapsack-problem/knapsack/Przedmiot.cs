namespace KnapsackProblem
{
    class Przedmiot
    {
        public int Numer { get; }
        public int Waga { get; }
        public int Wartosc { get; }

        public Przedmiot(int numer, int waga, int wartosc)
        {
            Numer = numer;
            Waga = waga;
            Wartosc = wartosc;
        }

        public override string ToString()
        {
            return $"Przedmiot {Numer}: Waga = {Waga}, Wartość = {Wartosc}";
        }

    }
}
