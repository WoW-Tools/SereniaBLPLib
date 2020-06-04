using SereniaBLPLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BLPExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bmp;

        public static System.Drawing.Bitmap ToBitmap<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
        {
            using (var memoryStream = new MemoryStream())
            {
                var imageEncoder = image.GetConfiguration().ImageFormatsManager.FindEncoder(PngFormat.Instance);
                image.Save(memoryStream, imageEncoder);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return new System.Drawing.Bitmap(memoryStream);
            }
        }

        private void OpenClick(object sender, EventArgs e)
        {
            try
            {
                var dlg = openFileDialog.ShowDialog();
                if (dlg != DialogResult.OK)
                    return;

                var file = openFileDialog.OpenFile();

                //Image<Rgba32> img;

                using (var blp = new BlpFile(file))
                {
                    bmp = blp.GetBitmap(0);
                    //img = blp.GetImage(0);
                }

                var graphics = panel1.CreateGraphics();
                graphics.Clear(panel1.BackColor);

                //bmp = ToBitmap(img);

                graphics.DrawImage(bmp, 0, 0, panel1.Width, panel1.Height);

                button3.Enabled = true;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The 'example.blp' was not found!");
            }
        }

        private void SaveAsClick(object sender, EventArgs e)
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

        private void PanelPaint(object sender, PaintEventArgs e)
        {
            if (bmp != null) e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
