using System;
using System.Windows.Forms;
using KnapsackProblem;

namespace KnapsackGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {

                if (!int.TryParse(txtItemCount.Text, out int itemCount) || itemCount <= 0)
                {
                    MessageBox.Show("Wprowad� poprawn� liczb� przedmiot�w!", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSeed.Text, out int seed))
                {
                    MessageBox.Show("Wprowad� poprawn� warto�� seeda!", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 0)
                {
                    MessageBox.Show("Wprowad� poprawn� pojemno�� plecaka!", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                Problem problem = new Problem(itemCount, seed);


                txtInstanceResult.Clear();
                txtInstanceResult.AppendText("Wygenerowane przedmioty:" + Environment.NewLine + Environment.NewLine);
                foreach (var item in problem.Przedmioty)
                {
                    txtInstanceResult.AppendText($"Numer: {item.Numer}, Waga: {item.Waga}, Warto��: {item.Wartosc}" + Environment.NewLine);
                }

                Wynik wynik = problem.Solve(capacity);

                txtResult.Clear();
                txtResult.AppendText("Rozwi�zanie problemu plecakowego:" + Environment.NewLine + Environment.NewLine);

                foreach (var item in wynik.WybranePrzedmioty)
                {
                    txtResult.AppendText("Wybrane przedmioty: "+ item + Environment.NewLine);
                }

                
                txtResult.AppendText(Environment.NewLine + $"��czna warto��: {wynik.SumaWartosci}" + Environment.NewLine);
                txtResult.AppendText($"��czna waga: {wynik.SumaWag}" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("B��d: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
