using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KnapsackProblem; 

namespace Unit_tests
{
    [TestClass]
    public class UnitTest
    {
        // czy nie jest pusty
        [TestMethod]
        public void TestNonEmptySolution()
        {
            Problem problem = new Problem(5, 42);
            Wynik wynik = problem.Solve(15); 

            Assert.IsTrue(wynik.WybranePrzedmioty.Count > 0, "Powinien być przynajmniej jeden przedmiot w plecaku.");
        }

        // czy jest pusty
        [TestMethod]
        public void TestEmptySolution()
        {
            Problem problem = new Problem(5, 42);
            Wynik wynik = problem.Solve(0); 

            Assert.AreEqual(0, wynik.WybranePrzedmioty.Count, "Plecak o pojemności 0 nie powinien zawierać żadnych przedmiotów.");
        }

        // dla konkrentej instancji
        [TestMethod]
        public void TestSpecificInstance()
        {
            Problem problem = new Problem(3, 1);
            Wynik wynik = problem.Solve(10);

            int expectedWeight = wynik.SumaWag;
            int expectedValue = wynik.SumaWartosci;

            Assert.IsTrue(expectedWeight <= 10, "Suma wag nie powinna przekroczyć pojemności plecaka.");
            Assert.IsTrue(expectedValue > 0, "Wynik powinien mieć dodatnią wartość.");
        }

        // czy algorytm faktycznie zwraca przedmioty
        [TestMethod]
        public void TestMaximizeValue()
        {
            Problem problem = new Problem(10, 100);
            Wynik wynik = problem.Solve(20);

            Assert.IsTrue(wynik.SumaWartosci > 0, "Suma wartości powinna być większa od zera.");
        }
        
        // czy nie ma ujemnej wagi
        [TestMethod]
        public void TestNoNegativeWeights()
        {
            Problem problem = new Problem(10, 123);
            foreach (var przedmiot in problem.Przedmioty)
            {
                Assert.IsTrue(przedmiot.Waga > 0, "Waga przedmiotu powinna być dodatnia.");
            }
        }
    }
}
