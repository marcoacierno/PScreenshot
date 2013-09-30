﻿namespace Picu3
{
    partial class UploadList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("In Upload", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("OK", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Errore", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("OK", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Errori", System.Windows.Forms.HorizontalAlignment.Left);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.inupload = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.header_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.header_result = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancellaCodaUploadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancellaCodaUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lista = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancellaListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancellaListaFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.inupload.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.lista.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.inupload);
            this.tabControl1.Controls.Add(this.lista);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 435);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // inupload
            // 
            this.inupload.Controls.Add(this.listView1);
            this.inupload.Location = new System.Drawing.Point(4, 22);
            this.inupload.Name = "inupload";
            this.inupload.Size = new System.Drawing.Size(352, 409);
            this.inupload.TabIndex = 0;
            this.inupload.Text = "In upload";
            this.inupload.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_name,
            this.header_result});
            this.listView1.ContextMenuStrip = this.contextMenuStrip2;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "In Upload";
            listViewGroup1.Name = "in_upload";
            listViewGroup2.Header = "OK";
            listViewGroup2.Name = "Terminati con successo";
            listViewGroup3.Header = "Errore";
            listViewGroup3.Name = "Errore";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(352, 409);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // header_name
            // 
            this.header_name.Text = "Nome";
            this.header_name.Width = 200;
            // 
            // header_result
            // 
            this.header_result.Text = "Risultato";
            this.header_result.Width = 148;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancellaCodaUploadsToolStripMenuItem,
            this.cancellaCodaUploadToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(200, 70);
            // 
            // cancellaCodaUploadsToolStripMenuItem
            // 
            this.cancellaCodaUploadsToolStripMenuItem.Name = "cancellaCodaUploadsToolStripMenuItem";
            this.cancellaCodaUploadsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.cancellaCodaUploadsToolStripMenuItem.Text = "Cancella coda uploads";
            this.cancellaCodaUploadsToolStripMenuItem.Click += new System.EventHandler(this.cancellaCodaUploadsToolStripMenuItem_Click);
            // 
            // cancellaCodaUploadToolStripMenuItem
            // 
            this.cancellaCodaUploadToolStripMenuItem.Name = "cancellaCodaUploadToolStripMenuItem";
            this.cancellaCodaUploadToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.cancellaCodaUploadToolStripMenuItem.Text = "Cancella coda + upload";
            this.cancellaCodaUploadToolStripMenuItem.Click += new System.EventHandler(this.cancellaCodaUploadToolStripMenuItem_Click);
            // 
            // lista
            // 
            this.lista.Controls.Add(this.listView2);
            this.lista.Location = new System.Drawing.Point(4, 22);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(352, 409);
            this.lista.TabIndex = 1;
            this.lista.Text = "Lista";
            this.lista.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView2.ContextMenuStrip = this.contextMenuStrip1;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup4.Header = "OK";
            listViewGroup4.Name = "Terminati con successo";
            listViewGroup5.Header = "Errori";
            listViewGroup5.Name = "Errore";
            this.listView2.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5});
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(352, 409);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Risultato";
            this.columnHeader2.Width = 145;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancellaListaToolStripMenuItem,
            this.cancellaListaFilesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 48);
            // 
            // cancellaListaToolStripMenuItem
            // 
            this.cancellaListaToolStripMenuItem.Name = "cancellaListaToolStripMenuItem";
            this.cancellaListaToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.cancellaListaToolStripMenuItem.Text = "Cancella lista";
            this.cancellaListaToolStripMenuItem.Click += new System.EventHandler(this.cancellaListaToolStripMenuItem_Click);
            // 
            // cancellaListaFilesToolStripMenuItem
            // 
            this.cancellaListaFilesToolStripMenuItem.Name = "cancellaListaFilesToolStripMenuItem";
            this.cancellaListaFilesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.cancellaListaFilesToolStripMenuItem.Text = "Cancella lista + files";
            this.cancellaListaFilesToolStripMenuItem.Click += new System.EventHandler(this.cancellaListaFilesToolStripMenuItem_Click);
            // 
            // UploadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 435);
            this.Controls.Add(this.tabControl1);
            this.Name = "UploadList";
            this.Text = "Upload List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadList_FormClosing);
            this.Load += new System.EventHandler(this.UploadList_Load);
            this.tabControl1.ResumeLayout(false);
            this.inupload.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.lista.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage inupload;
        private System.Windows.Forms.TabPage lista;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader header_name;
        private System.Windows.Forms.ColumnHeader header_result;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancellaListaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancellaListaFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cancellaCodaUploadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancellaCodaUploadToolStripMenuItem;
    }
}