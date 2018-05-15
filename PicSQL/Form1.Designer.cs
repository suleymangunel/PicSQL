namespace PicSQL
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpenDatabase = new System.Windows.Forms.ToolStripButton();
            this.tsbUploadFile = new System.Windows.Forms.ToolStripButton();
            this.tsbDownloadFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTestDatabaseConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtCurrentPassword = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCbSelectDatabaseType = new System.Windows.Forms.ToolStripComboBox();
            this.tsCbAspectRatio = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(263, 459);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenDatabase,
            this.tsbUploadFile,
            this.tsbDownloadFile,
            this.toolStripSeparator1,
            this.tsbTestDatabaseConnection,
            this.toolStripLabel1,
            this.txtCurrentPassword,
            this.toolStripSeparator2,
            this.tsbCbSelectDatabaseType,
            this.tsCbAspectRatio});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(757, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpenDatabase
            // 
            this.tsbOpenDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenDatabase.Image = global::PicSQL.Properties.Resources.database_control_play;
            this.tsbOpenDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenDatabase.Name = "tsbOpenDatabase";
            this.tsbOpenDatabase.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenDatabase.Text = "Load images from database";
            this.tsbOpenDatabase.Click += new System.EventHandler(this.tsbOpenDatabase_Click);
            // 
            // tsbUploadFile
            // 
            this.tsbUploadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUploadFile.Image = global::PicSQL.Properties.Resources.document_upload;
            this.tsbUploadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUploadFile.Name = "tsbUploadFile";
            this.tsbUploadFile.Size = new System.Drawing.Size(23, 22);
            this.tsbUploadFile.Text = "Upload image";
            this.tsbUploadFile.Click += new System.EventHandler(this.tsbUploadFile_Click);
            // 
            // tsbDownloadFile
            // 
            this.tsbDownloadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDownloadFile.Image = global::PicSQL.Properties.Resources.document_download;
            this.tsbDownloadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownloadFile.Name = "tsbDownloadFile";
            this.tsbDownloadFile.Size = new System.Drawing.Size(23, 22);
            this.tsbDownloadFile.Text = "Download image";
            this.tsbDownloadFile.Click += new System.EventHandler(this.tsbDownloadFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTestDatabaseConnection
            // 
            this.tsbTestDatabaseConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTestDatabaseConnection.Image = global::PicSQL.Properties.Resources.database_check;
            this.tsbTestDatabaseConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTestDatabaseConnection.Name = "tsbTestDatabaseConnection";
            this.tsbTestDatabaseConnection.Size = new System.Drawing.Size(23, 22);
            this.tsbTestDatabaseConnection.Text = "Check database connection";
            this.tsbTestDatabaseConnection.Click += new System.EventHandler(this.tsbTestDatabaseConnection_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabel1.Text = "Database Password";
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCbSelectDatabaseType
            // 
            this.tsbCbSelectDatabaseType.Items.AddRange(new object[] {
            "MS SQL Server",
            "SQLite",
            "MySQL"});
            this.tsbCbSelectDatabaseType.Name = "tsbCbSelectDatabaseType";
            this.tsbCbSelectDatabaseType.Size = new System.Drawing.Size(121, 25);
            this.tsbCbSelectDatabaseType.Text = "Database Type";
            this.tsbCbSelectDatabaseType.SelectedIndexChanged += new System.EventHandler(this.tsbCbSelectDatabaseType_SelectedIndexChanged);
            // 
            // tsCbAspectRatio
            // 
            this.tsCbAspectRatio.Items.AddRange(new object[] {
            "Keep Aspect Ratio",
            "Stretch"});
            this.tsCbAspectRatio.Name = "tsCbAspectRatio";
            this.tsCbAspectRatio.Size = new System.Drawing.Size(150, 25);
            this.tsCbAspectRatio.Text = "Scale";
            this.tsCbAspectRatio.SelectedIndexChanged += new System.EventHandler(this.tsCbAspectRatio_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(453, 449);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(282, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 459);
            this.panel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 510);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "PicSQL v1.0.0.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpenDatabase;
        private System.Windows.Forms.ToolStripButton tsbUploadFile;
        private System.Windows.Forms.ToolStripButton tsbDownloadFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTestDatabaseConnection;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox tsbCbSelectDatabaseType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripComboBox tsCbAspectRatio;
    }
}

