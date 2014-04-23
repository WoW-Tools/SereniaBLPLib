using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using System.IO;

namespace BLPExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.g = panel1.CreateGraphics();
        }

        SereniaBLPLib.BlpFile exampleBLP;
        Bitmap bmp;
        Graphics g;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.exampleBLP != null)
                {
                    this.exampleBLP.close();
                    this.exampleBLP = null;
                }

                var dlg = openFileDialog.ShowDialog();
                if (dlg != DialogResult.OK)
                    return;
                FileStream file = new FileStream(openFileDialog.FileName, FileMode.Open);
                this.exampleBLP = new SereniaBLPLib.BlpFile(file);
                //MessageBox.Show("Mipmap count: "+exampleBLP.MipMapCount);

                // loading bitmap level 0
                bmp = this.exampleBLP.getBitmap(0);

                g.DrawImage(bmp, 0, 0);

                button3.Enabled = true;                
            }
            catch (FileNotFoundException fe)
            {
                MessageBox.Show("The 'example.blp' was not found!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = saveFileDialog.ShowDialog();
            if (dlg != DialogResult.OK)
                return;

            var format = ExtensionToImageFormat(saveFileDialog.FileName);
            bmp.Save(saveFileDialog.FileName, format);
        }

        private static ImageFormat ExtensionToImageFormat(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "bmp":
                    return ImageFormat.Bmp;
            }
            return ImageFormat.Jpeg;
        }
    }
}
