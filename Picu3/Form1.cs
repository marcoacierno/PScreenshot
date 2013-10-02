using Picu3.Properties;
using ScreenShot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

//Nella vita non c'è niente di cosi terrificante e allo stesso tempo esaltate che prendere l'iniziativa.. quando si rischia tutto e ci si butta


namespace Picu3
{
    public partial class Form1 : Form
    {
        #region Fields
        /// <summary>
        /// Versione del programma
        /// </summary>
        private const string version = "3.0.5"; 

        /// <summary>
        /// Si riferisce al form che contiene l'upload list
        /// </summary>
        private UploadList uploadlist;
        /// <summary>
        /// Controlla ogni ms se il tasto STAMP è stato premuto
        /// </summary>
        private Timer keyCheck = new Timer();
        /// <summary>
        /// Classe upload
        /// </summary>
        public static Upload upload = new Upload();
        /// <summary>
        /// Settings del programma
        /// </summary>
        public static Settings settings;
        public Impostazioni impostazioni;
        /// <summary>
        /// Indica l'ultimo stato conosciuto della finestra
        /// </summary>
        private FormWindowState state = FormWindowState.Normal;
        #endregion

        #region Esterni
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        #endregion

        public Form1()
        { 
            InitializeComponent();
            Logs.Log("Inizializzazione componenti OK.");

            this.Text = "Picu " + version;

            // Creo il form e lo nascondo durante l'avvio dell'applicazione
            // In questo modo evito la creazione di un eccezione in caso l'utente inizi un upload senza aver mai aperto l'uploadlist (1.5 bug)
            uploadlist = new UploadList();
            uploadlist.Opacity = 0.0;
            uploadlist.ShowInTaskbar = false;
            uploadlist.Show();
            uploadlist.Hide();
            uploadlist.ShowInTaskbar = true;
            uploadlist.Opacity = 100.0;

            Upload.uploadlist = uploadlist;

            keyCheck.Interval = 1;
            keyCheck.Tick += keyCheck_Tick;
            keyCheck.Start();

            settings.LoadConfig();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 128 too big? use 18x18 mb?
            if (!System.IO.File.Exists("icon.ico"))
                MessageBox.Show("Impossibile trovare icon.ico");
            else
                this.Icon = new Icon("icon.ico", 128, 128);

            impostazioni = new Impostazioni();

            if (!File.Exists(Environment.SpecialFolder.MyDocuments + "Picu\\version.txt"))
            {
                using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\version.txt"))
                {
                    sw.WriteLine(version);
                }
            }
        }

        /// <summary>
        /// Ad ogni tick controlla se il tasto è stato premuto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void keyCheck_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.PrintScreen) == Int16.MinValue)
            {
                DoScreen.Screenshot(settings.ScreenArea);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (settings.ScreenArea == SCREEN_AREA.CAPTURE_ALL)
            {
                captureall.Checked = true;
            }
            else
            {
                captureactwindow.Checked = true;
            }
        }

        #region Scegli file region
        private void button2_Click(object sender, EventArgs e)
        {
            scegliFile.ShowDialog();
        }

        private void scegliFile_FileOk(object sender, CancelEventArgs e)
        {
            string[] files = scegliFile.FileNames;

            foreach(string file in files)
            {
                upload.AddUpload(file);
            }
        } 
        #endregion

        #region Screen area settings

        private void button1_Click(object sender, EventArgs e)
        {
            uploadlist.Show();
        }

        private void captureall_CheckedChanged(object sender, EventArgs e)
        {
            settings.ScreenArea = SCREEN_AREA.CAPTURE_ALL;
        }

        private void captureactwindow_CheckedChanged(object sender, EventArgs e)
        {
            settings.ScreenArea = SCREEN_AREA.CAPTURE_ACT_WINDOW;
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            impostazioni.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (upload.inWorking)
            {
                DialogResult result = MessageBox.Show("Attualmente il programma sta ancora termindo l'upload, vuoi veramente uscire?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #region File drop

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                upload.AddUpload(new FileInfo(file).FullName);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        } 
        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            FormWindowState ws = this.WindowState;
            
            if (state != ws)
            {
                state = ws;

                if (ws == FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = false;
                }
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    //this.ShowInTaskbar = false;
                    this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    this.ShowInTaskbar = true;
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void uploadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadlist.Show();
        }

        private void scegliFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scegliFile.ShowDialog();
        }

        private void impostazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            impostazioni.Show();
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
