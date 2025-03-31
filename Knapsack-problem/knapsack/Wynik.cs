using System;
using System.Collections.Generic;
using System.Linq;



namespace KnapsackProblem
{
    class Wynik
    {
        public List<int> WybranePrzedmioty { get; }
        public int SumaWartosci { get; }
        public int SumaWag { get; }

        public Wynik(List<int> przedmioty, int wartosc, int waga)
        {
            WybranePrzedmioty = przedmioty;
            SumaWartosci = wartosc;
            SumaWag = waga;
        }
        public override string ToString()
        {
            return $"Wybrane przedmioty: {string.Join(", ", WybranePrzedmioty)}\n" +
                   $"Suma wartości: {SumaWartosci}, Suma wag: {SumaWag}";
        }
    }
}
