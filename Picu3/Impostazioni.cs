using Picu3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Picu3
{
    public partial class Impostazioni : Form
    {

        public Impostazioni()
        {
            InitializeComponent();

            // Carica la configurazione
            textBox1.Text = Form1.settings.GalleryDir;
            comboBox1.SelectedItem = Utils.StringFromImageFormat(Form1.settings.Formato);

            this.Size = new Size(388, 132);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (!System.IO.File.Exists("settings.ico"))
                MessageBox.Show("Impossibile trovare settings.ico");
            else
                this.Icon = new Icon("settings.ico", 128, 128);

            long peso = 0;

            foreach(string file in Directory.GetFiles(Form1.settings.GalleryDir))
            {
                peso += new FileInfo(file).Length;
            }

            pesogalleria.Text = ((peso / 1024f) / 1024f) + " mb";
        }

        private void Impostazioni_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.settings.Formato = Utils.ImageFormatFromString((string)comboBox1.SelectedItem);
            Form1.settings.GalleryDir = textBox1.Text;
            Form1.settings.SaveConfig();

            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Form1.settings.GalleryDir);

            foreach(string file in files)
            {
                File.Delete(file);
            }

            File.Delete("images.txt");

            MessageBox.Show("Galleria pulita, files: " + files.Length);
            pesogalleria.Text = "0 mb";
        }
    }
}
