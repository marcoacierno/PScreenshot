﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Picu
{
    public partial class Form1 : Form
    {
        private NotifyIcon icon;
        private Timer check;
        private string last_screen;
        private const int info_time = 500;                                  // ms
        private ImageFormat format;
        private string estensione;
        private bool cattura_salva;
        private bool instant_upload;                                        // uploadAutomaticoToolStripMenuItem
        private const string url_picu = @"http://picu.site11.com/share_app.php";
        private Stack<string> thread_task;
        private Stack<int> thread_grid_task;
        private ListUp upload_list;

        /**
         * grid default text
         */
        //private const string button_rem     = "Rimuovi";
        public const string default_nolink = "No link";

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); 

        public Form1()
        {
            InitializeComponent();

            cattura_salva = true;
            instant_upload = true;

            thread_task = new Stack<string>();
            upload_list = new ListUp();
            thread_grid_task = new Stack<int>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            catturaESalvaToolStripMenuItem.Checked = true;
            uploadAutomaticoToolStripMenuItem.Checked = true;

            icon = new NotifyIcon();
           
            if (File.Exists("icon.ico"))
            {
                icon.Icon = new Icon("icon.ico", 40, 40);
            }
            
            icon.Text = "Picu - Screenshot";
            icon.MouseClick += icon_MouseClick;
            
            icon.Visible = true;

            check = new Timer();
            check.Interval = 1;
            check.Tick += check_Tick;

            check.Enabled = true;
            check.Start();

            format = ImageFormat.Png;
            estensione = "png";

            // thanks to Noam Gal
            // http://stackoverflow.com/questions/1385674/place-winform-on-bottom-right
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        void icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoScreen();
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        void check_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.PrintScreen) == Int16.MinValue)
            {
                DoScreen();
            }
        }

        void DoScreen()
        {
            ScreenShotDemo.ScreenCapture screen = new ScreenShotDemo.ScreenCapture();
            Image img = screen.CaptureScreen();

            pictureBox1.Image = img;

            if (cattura_salva)
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\Galleria\\"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Galleria\\");
                }
                
                button3.Enabled = false;
                // "Cattura e salva" è attivo
                int idx = 1;
                string time = DateTime.Now.ToString("dd-mm-yyyy HH-mm-ss");
                string url = Environment.CurrentDirectory + "\\Galleria\\" + time + "." + estensione;

                while (File.Exists(url))
                {
                    url = Environment.CurrentDirectory + "\\Galleria\\" + time + " (" + idx + ")." + estensione;
                    idx++;
                }

                img.Save(url, format);
                toolStripStatusLabel1.Text = "Screen catturato e salvato";  

                last_screen = url;

                /**
                 * se cattura e salva
                 * e pure instant_upload è attivo
                 * l'upload è automatico
                 */

                if (instant_upload)
                {
                    icon.BalloonTipText = "Screen catturato e salvato, inserito nella lista upload.";
                    PreThread(url);
                }
                else icon.BalloonTipText = "Screen catturato e salvato.";

                icon.ShowBalloonTip(info_time);
            }
            else
            {
                img.Save(Environment.CurrentDirectory + "\\tmp." + estensione, format);
                toolStripStatusLabel1.Text = "Screen catturato";
                icon.BalloonTipText = "Screen catturato";
                icon.ShowBalloonTip(info_time);

                last_screen = Environment.CurrentDirectory + "\\tmp." + estensione;
                button3.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (last_screen == null)
            {
                return;
            }

            if (!File.Exists(last_screen))
            {
                MessageBox.Show("Impossibile trovare il file", "Picu");
                return;
            }

            System.Diagnostics.Process.Start(last_screen);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Galleria\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Galleria\\");
            }

            last_screen = Environment.CurrentDirectory + "\\Galleria\\" + DateTime.Now.ToString("dd-mm-yyyy HH-mm-ss") + "." + estensione;
            File.Move(Environment.CurrentDirectory + "\\tmp." + estensione, last_screen);

            button3.Enabled = false;

            /**
             * Controllo se instant_upload è attivo
             * se lo è, carico la foto
             */

            if (instant_upload)
            {
                toolStripStatusLabel1.Text = "Screen salvato, inserito nella lista upload.";
                PreThread(last_screen);
            }
            else toolStripStatusLabel1.Text = "Screen salvato";
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            upload_list.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Galleria\\"))
            {
                MessageBox.Show("Non hai ancora salvato nessun screenshot.");
            }
            else
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\Galleria\\");
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //png
            format = ImageFormat.Png;
            estensione = "png";

            // :V
            pngToolStripMenuItem.Checked = true;
            jpegToolStripMenuItem.Checked = false;
            gifToolStripMenuItem.Checked = false;
            bmpToolStripMenuItem.Checked = false;
        }

        private void jpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //jpeg
            format = ImageFormat.Jpeg;
            estensione = "jpg";

            pngToolStripMenuItem.Checked = false;
            jpegToolStripMenuItem.Checked = true;
            gifToolStripMenuItem.Checked = false;
            bmpToolStripMenuItem.Checked = false;
        }

        private void gifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gif
            format = ImageFormat.Gif;
            estensione = "gif";

            pngToolStripMenuItem.Checked = false;
            jpegToolStripMenuItem.Checked = false;
            gifToolStripMenuItem.Checked = true;
            bmpToolStripMenuItem.Checked = false;
        }

        private void bmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bmp
            format = ImageFormat.Bmp;
            estensione = "bmp";

            pngToolStripMenuItem.Checked = false;
            jpegToolStripMenuItem.Checked = false;
            gifToolStripMenuItem.Checked = false;
            bmpToolStripMenuItem.Checked = true;
        }

        private void catturaESalvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cattura_salva = !cattura_salva;
            catturaESalvaToolStripMenuItem.Checked = cattura_salva;
        }

        private void uploadAutomaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            instant_upload = !instant_upload;
            uploadAutomaticoToolStripMenuItem.Checked = instant_upload;
        }

        private void PreThread(string file)
        {
            // aggiunge il task al thread
            thread_task.Push(file);
            // upload_list.dataGridView1.
            int n = upload_list.dataGridView1.Rows.Add(Path.GetFileName(file), "No, in attesa.", default_nolink);
            thread_grid_task.Push(n);

            // se il thread non è già attivo
            // lo starta

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (thread_task.Count > 0)
            {
                string file = thread_task.Pop();
                int id = thread_grid_task.Pop();

                upload_list.dataGridView1.Rows[id].Cells[1].Value = "In upload";
                
                WebClient wb = new WebClient();

                byte[] response = wb.UploadFile(url_picu, file);

                upload_list.dataGridView1.Rows[id].Cells[2].Value = Encoding.ASCII.GetString(response);
                upload_list.dataGridView1.Rows[id].Cells[1].Value = "No, caricato.";
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (string file in openFileDialog1.FileNames)
            {
                PreThread(file);
            }
        }

        private void caricaUnimmagineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}