using System;
using System.Collections.Generic;
using System.Linq;


using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Unit_tests")]
[assembly: InternalsVisibleTo("KnapsackGUI")]

namespace KnapsackProblem
{
    class Problem
    {
        public int LiczbaPrzedmiotów { get; }
        public List<Przedmiot> Przedmioty { get; } = new List<Przedmiot>();

        public Problem(int n, int seed)
        {
            LiczbaPrzedmiotów = n;
            Random random = new Random(seed);

            for (int i = 1; i <= n; i++)
            {
                int waga = random.Next(1, 11);
                int wartosc = random.Next(1, 11);
                Przedmioty.Add(new Przedmiot(i, waga, wartosc));
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Przedmioty);
        }

        public Wynik Solve(int capacity)
        {
            var posortowane = Przedmioty.OrderByDescending(p => (double)p.Wartosc / p.Waga).ToList();
            List<int> wybranePrzedmioty = new List<int>();
            int sumaWag = 0;
            int sumaWartosci = 0;

            foreach(var przedmiot in posortowane)
            {
                if (sumaWag+przedmiot.Waga <= capacity)
                {
                    wybranePrzedmioty.Add(przedmiot.Numer);
                    sumaWag += przedmiot.Waga;
                    sumaWartosci += przedmiot.Wartosc;
                }
            }

            return new Wynik(wybranePrzedmioty,sumaWartosci,sumaWag);

        }
    }
}
