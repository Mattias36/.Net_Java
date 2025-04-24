using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelImageFiltersApp
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = originalImage;
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            
            Bitmap image1 = (Bitmap)originalImage.Clone();
            Bitmap image2 = (Bitmap)originalImage.Clone();
            Bitmap image3 = (Bitmap)originalImage.Clone();
            Bitmap image4 = (Bitmap)originalImage.Clone();

            Bitmap gray = null;
            Bitmap negative = null;
            Bitmap binary = null;
            Bitmap green = null;

            Parallel.Invoke(
                () => { gray = ApplyGrayScale(image1); },
                () => { negative = ApplyNegative(image2); },
                () => { binary = ApplyThreshold(image3); },
                () => { green = ApplyGreenTint(image4); }
            );

            pictureBox2.Image = gray;
            pictureBox3.Image = negative;
            pictureBox4.Image = binary;
            pictureBox5.Image = green;
        }


        private Bitmap ApplyGrayScale(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    result.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            return result;
        }

        private Bitmap ApplyNegative(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            return result;
        }

        private Bitmap ApplyThreshold(Bitmap source, int threshold = 128)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    result.SetPixel(x, y, avg >= threshold ? Color.White : Color.Black);
                }
            }
            return result;
        }

        private Bitmap ApplyGreenTint(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(0, pixel.G, 0));
                }
            }
            return result;
        }
    }
}
