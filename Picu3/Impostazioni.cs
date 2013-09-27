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
        }

        private void Impostazioni_Load(object sender, EventArgs e)
        {
            // Might be very slow if the gallery is full
            pesogalleria.Text = Math.Round(((pesoGalleria() / 1024f) / 1024f), 1) + " MB";
        }

        /// <summary>
        /// Questo metodo si occupa di calcolare lo spazio occupato da tutti i files della galleria
        /// </summary>
        /// <returns>Ritorna il peso in bytes</returns>
        private double pesoGalleria()
        {
            if (!Directory.Exists(Form1.settings.GalleryPath)) return 0.0;
            long peso = 0;

            foreach (string file in Directory.GetFiles(Form1.settings.GalleryPath))
            {
                peso += new FileInfo(file).Length;
            }

            return peso;
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
            Utils.ClearList(true);
            pesogalleria.Text = "0 MB";
            MessageBox.Show("Galleria pulita");
        }
    }
}
