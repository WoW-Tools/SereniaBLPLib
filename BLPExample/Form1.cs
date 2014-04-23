using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

                FileStream file = new FileStream("example.blp", FileMode.Open);
                this.exampleBLP = new SereniaBLPLib.BlpFile(file);
                MessageBox.Show("Mipmap count: "+exampleBLP.MipMapCount);

                // loading bitmap level 0
                bmp = this.exampleBLP.getBitmap(0);

                g.DrawImage(bmp, 0, 0);

                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                
            }
            catch (FileNotFoundException fe)
            {
                MessageBox.Show("The 'example.blp' was not found!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.bmp.Save("example.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.bmp.Save("example.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.bmp.Save("example.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
