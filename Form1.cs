using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Button convertButton = new Button();
            convertButton.Text = "Лого хөрвүүлэх";
            convertButton.AutoSize = true;
            convertButton.Location = new Point(30, 30);
            convertButton.Click += ConvertButton_Click;
            Controls.Add(convertButton);

            Text = "Харилцагчийн лого хөрвүүлэгч";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(500, 300);
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string inputFolder = @"C:\Users\Acer\Desktop\LogoImages";
            string outputFolder = @"C:\Users\Acer\Desktop\ConvertedImages";

            Directory.CreateDirectory(outputFolder);

            string[] supported = { ".jpg", ".jpeg", ".png", ".svg" };
            string[] files = Directory.GetFiles(inputFolder);

            foreach (string file in files)
            {
                string ext = Path.GetExtension(file).ToLower();
                if (Array.IndexOf(supported, ext) == -1)
                    continue;

                try
                {
                    string filename = Path.GetFileName(file);
                    MessageBox.Show ("Лого хөрвүүлж байна: " + filename);
                    using (MagickImage image = new MagickImage(file))
                    {
                        image.Quality = 128;
                        image.Resize(0, 128);
                        string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".png");
                        image.Format = MagickFormat.Png;
                        image.Write(outputPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Лого хөрвүүлэхэд алдаа гарлаа: {Path.GetFileName(file)}\n{ex.Message}");
                }
            }

            MessageBox.Show("Лого хөрвөгдлөө!");
        }
    }
}
