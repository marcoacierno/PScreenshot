namespace Picu3
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.captureall = new System.Windows.Forms.RadioButton();
            this.captureactwindow = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.scegliFile = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uploadListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scegliFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impostazioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // captureall
            // 
            this.captureall.AutoSize = true;
            this.captureall.Checked = true;
            this.captureall.Location = new System.Drawing.Point(12, 12);
            this.captureall.Name = "captureall";
            this.captureall.Size = new System.Drawing.Size(83, 17);
            this.captureall.TabIndex = 2;
            this.captureall.TabStop = true;
            this.captureall.Text = "Cattura tutto";
            this.captureall.UseVisualStyleBackColor = true;
            this.captureall.CheckedChanged += new System.EventHandler(this.captureall_CheckedChanged);
            // 
            // captureactwindow
            // 
            this.captureactwindow.AutoSize = true;
            this.captureactwindow.Location = new System.Drawing.Point(12, 41);
            this.captureactwindow.Name = "captureactwindow";
            this.captureactwindow.Size = new System.Drawing.Size(136, 17);
            this.captureactwindow.TabIndex = 3;
            this.captureactwindow.Text = "Cattura la finestra attiva";
            this.captureactwindow.UseVisualStyleBackColor = true;
            this.captureactwindow.CheckedChanged += new System.EventHandler(this.captureactwindow_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Upload list";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(294, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Scegli";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(371, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Impostazioni";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // scegliFile
            // 
            this.scegliFile.Multiselect = true;
            this.scegliFile.FileOk += new System.ComponentModel.CancelEventHandler(this.scegliFile_FileOk);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Picu3 - Clicca per aprire";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadListToolStripMenuItem,
            this.scegliFilesToolStripMenuItem,
            this.impostazioniToolStripMenuItem,
            this.chiudiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 92);
            // 
            // uploadListToolStripMenuItem
            // 
            this.uploadListToolStripMenuItem.Name = "uploadListToolStripMenuItem";
            this.uploadListToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.uploadListToolStripMenuItem.Text = "Upload list";
            this.uploadListToolStripMenuItem.Click += new System.EventHandler(this.uploadListToolStripMenuItem_Click);
            // 
            // scegliFilesToolStripMenuItem
            // 
            this.scegliFilesToolStripMenuItem.Name = "scegliFilesToolStripMenuItem";
            this.scegliFilesToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.scegliFilesToolStripMenuItem.Text = "Scegli files";
            this.scegliFilesToolStripMenuItem.Click += new System.EventHandler(this.scegliFilesToolStripMenuItem_Click);
            // 
            // impostazioniToolStripMenuItem
            // 
            this.impostazioniToolStripMenuItem.Name = "impostazioniToolStripMenuItem";
            this.impostazioniToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.impostazioniToolStripMenuItem.Text = "Impostazioni";
            this.impostazioniToolStripMenuItem.Click += new System.EventHandler(this.impostazioniToolStripMenuItem_Click);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 103);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.captureactwindow);
            this.Controls.Add(this.captureall);
            this.Name = "Form1";
            this.Text = "Picu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton captureall;
        private System.Windows.Forms.RadioButton captureactwindow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog scegliFile;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uploadListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scegliFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impostazioniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiudiToolStripMenuItem;
    }
}

