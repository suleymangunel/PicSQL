using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace PicSQL
{
    public partial class Form1 : Form
    {
        public Bitmap bmpImage;
        public static string strSQLconn_MSSQL = "";
        public static string strSQLconn_SQLite = "";
        public static string strSQLconn_MySQL = "";

        const int DBTYPE_NONE = -1;
        const int DBTYPE_MSSQL = 0;
        const int DBTYPE_SQLite = 1;
        const int DBTYPE_MySQL = 2;
        public int DBTYPE = DBTYPE_NONE;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.Text = "PicSQL (v" + version.ToString() + ")";
            tsbCbSelectDatabaseType.SelectedIndex = 2;
            tsCbAspectRatio.SelectedIndex = 0;
        }

        private void tsbCbSelectDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tsbCbSelectDatabaseType.SelectedIndex)
                {
                case DBTYPE_MSSQL:
                    DBTYPE = DBTYPE_MSSQL;
                    SetDatabaseParameters_MSSQL();
                    OpenDatabase_MSSQL();
                    break;
                case DBTYPE_SQLite:
                    DBTYPE = DBTYPE_SQLite;
                    SetDatabaseParameters_SQLite();
                    OpenDatabase_SQLite();
                    break;
                case DBTYPE_MySQL:
                    DBTYPE = DBTYPE_MySQL;
                    SetDatabaseParameters_MySQL();
                    OpenDatabase_MySQL();
                    break;
            }
        }

        private void tsbOpenDatabase_Click(object sender, EventArgs e)
        {
            switch (DBTYPE)
            {
                case DBTYPE_MSSQL:
                    OpenDatabase_MSSQL();
                    break;
                case DBTYPE_SQLite:
                    OpenDatabase_SQLite();
                    break;
                case DBTYPE_MySQL:
                    OpenDatabase_MySQL();
                    break;
            }
        }

        private void tsbTestDatabaseConnection_Click(object sender, EventArgs e)
        {
            switch (DBTYPE)
            {
                case DBTYPE_MSSQL:
                    TestDatabaseConnection_MSSQL();
                    break;
                case DBTYPE_SQLite:
                    TestDatabaseConnection_SQLite();
                    break;
                case DBTYPE_MySQL:
                    TestDatabaseConnection_MySQL();
                    break;
            }
        }

        private void tsbUploadFile_Click(object sender, EventArgs e)
        {
            bool result = false;
            DialogResult dr = openFileDialog1.ShowDialog();
            string strFullPath = openFileDialog1.FileName;
            string strFileName = openFileDialog1.SafeFileName;

            switch (DBTYPE)
            {
                case DBTYPE_MSSQL:
                    result = UploadImage_MSSQL(strFullPath, strFileName);
                    break;
                case DBTYPE_SQLite:
                    result = UploadImage_SQLite(strFullPath, strFileName);
                    break;
                case DBTYPE_MySQL:
                    result = UploadImage_MySQL(strFullPath, strFileName);
                    break;
            }

            if (result)
                MessageBox.Show("Upload completed: " + strFileName, "Image Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Upload failed: " + strFileName, "Image Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void tsbDownloadFile_Click(object sender, EventArgs e)
        {
            string strImageName = null;
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult dr = fb.ShowDialog();
            if (dr == DialogResult.OK && !string.IsNullOrWhiteSpace(fb.SelectedPath))
            {
                string strPath = fb.SelectedPath;
                strPath += strPath.Substring(strPath.Length - 1, 1) == "\\" ? string.Empty : "\\";

                switch (DBTYPE)
                {
                    case DBTYPE_MSSQL:
                        strImageName = DownloadImage_MSSQL(strPath);
                        break;
                    case DBTYPE_SQLite:
                        strImageName = DownloadImage_SQLite(strPath);
                        break;
                    case DBTYPE_MySQL:
                        strImageName = DownloadImage_MySQL(strPath);
                        break;
                }

                if (strImageName != null)
                    MessageBox.Show("Image saved on location: " + strPath + strImageName, "Image Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Image download error", "Image Download", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < 0)
                return;
            try
            {
                string name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                string strCMD = "SELECT * FROM Images WHERE Name='" + name + "';";
                DataSet ds = new DataSet();
                switch (DBTYPE)
                {
                    case DBTYPE_MSSQL:
                        using (SqlConnection connDEO = new SqlConnection(strSQLconn_MSSQL))
                        {
                            SqlDataAdapter sqlDA = new SqlDataAdapter(strCMD, connDEO);
                            sqlDA.Fill(ds, "Images");
                        }
                        break;
                    case DBTYPE_SQLite:
                        using (SQLiteConnection connDEO = new SQLiteConnection(strSQLconn_SQLite))
                        {
                            SQLiteDataAdapter sqlDA = new SQLiteDataAdapter(strCMD, connDEO);
                            sqlDA.Fill(ds, "Images");
                        }
                        break;
                    case DBTYPE_MySQL:
                        using (MySqlConnection connDEO = new MySqlConnection(strSQLconn_MySQL))
                        {
                            MySqlDataAdapter sqlDA = new MySqlDataAdapter(strCMD, connDEO);
                            sqlDA.Fill(ds, "Images");
                        }
                        break;
                }
                byte[] Data = (byte[])ds.Tables["Images"].Rows[0]["Data"];
                bmpImage = ByteToImage(Data);
                ShowPicture((tsCbAspectRatio.SelectedIndex==0? true :false));
            }
            catch
            {
                pictureBox1.Image = null;
            }
        }

        public void ShowPicture(bool KeepAspectRatio)
        {
            pictureBox1.Image = bmpImage;
            if (!KeepAspectRatio)
            {
                pictureBox1.Size = panel1.Size;
                return;
            }

            float factor = (float)bmpImage.Width / (float)bmpImage.Height;
            
            pictureBox1.Width = bmpImage.Width;
            pictureBox1.Height = bmpImage.Height;

            if (bmpImage.Width > panel1.Width || bmpImage.Height > panel1.Height)
            {
                if (factor > 1)
                {
                    pictureBox1.Width = panel1.Width;
                    pictureBox1.Height = (int)((float)panel1.Width / factor);
                }
                else
                {
                    pictureBox1.Height = panel1.Height;
                    pictureBox1.Width = (int)((float)panel1.Height * factor);
                }

                if (pictureBox1.Height > panel1.Height)
                {
                    pictureBox1.Height = panel1.Height;
                    pictureBox1.Width = (int)(float)(panel1.Height * factor);
                }

                if (pictureBox1.Width > panel1.Width)
                {
                    pictureBox1.Width = panel1.Width;
                    pictureBox1.Height = (int)(float)(panel1.Width / factor);
                }
            }
        }

        public void OpenDatabase_MSSQL()
        {
            string strCMD = "SELECT Name FROM Images;";
            DataSet ds = new DataSet();
            using (SqlConnection connDEO = new SqlConnection(strSQLconn_MSSQL))
            {
                SqlDataAdapter sqlDA = new SqlDataAdapter(strCMD, connDEO);
                sqlDA.Fill(ds, "Images");
            }
            try
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Images";
                dataGridView1.Refresh();
            }
            catch
            {

            }
        }

        public void OpenDatabase_SQLite()
        {
            string strCMD = "SELECT Name FROM Images;";
            DataSet ds = new DataSet();
            using (SQLiteConnection connDEO = new SQLiteConnection(strSQLconn_SQLite))
            {
                SQLiteDataAdapter sqlDA = new SQLiteDataAdapter(strCMD, connDEO);
                sqlDA.Fill(ds, "Images");
            }
            try
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Images";
                dataGridView1.Refresh();
            }
            catch
            {

            }
        }

        public void OpenDatabase_MySQL()
        {
            string strCMD = "SELECT Name FROM images;";
            DataSet ds = new DataSet();
            using (MySqlConnection connDEO = new MySqlConnection(strSQLconn_MySQL))
            {
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(strCMD, connDEO);
                sqlDA.Fill(ds, "images");
            }
            try
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "images";
                dataGridView1.Refresh();
            }
            catch
            {

            }
        }

        public void TestDatabaseConnection_MSSQL()
        {
            try
            {
                SqlConnection connDEO = new SqlConnection(strSQLconn_MSSQL);
                connDEO.Open();
                connDEO.Close();
                MessageBox.Show("Test Completed.");
            }
            catch
            {
                MessageBox.Show("Test Failed !!!");
            }
        }

        public void TestDatabaseConnection_SQLite()
        {
            try
            {
                SQLiteConnection connDEO = new SQLiteConnection(strSQLconn_SQLite);
                connDEO.Open();
                connDEO.Shutdown();
                connDEO.Close();
                MessageBox.Show("Test Completed.");
            }
            catch
            {
                MessageBox.Show("Test Failed !!!");
            }
        }

        public void TestDatabaseConnection_MySQL()
        {
            try
            {
                MySqlConnection connDEO = new MySqlConnection(strSQLconn_MySQL);
                connDEO.Open();
                connDEO.Close();
                MessageBox.Show("Test Completed.");
            }
            catch
            {
                MessageBox.Show("Test Failed !!!");
            }
        }

        public static bool SetDatabaseParameters_MSSQL()
        {
            try
            {
                /*For future use
                string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PicSQL.config");
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = exePath;
                Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                AppSettingsSection cs =(AppSettingsSection) cfg.GetSection("appSettings");
                */

                System.Data.SqlClient.SqlConnectionStringBuilder sqlCnnStrBld = new System.Data.SqlClient.SqlConnectionStringBuilder();
                sqlCnnStrBld.DataSource = ConfigurationManager.AppSettings["dbDataSource_MSSQL"].ToString();
                sqlCnnStrBld.InitialCatalog = ConfigurationManager.AppSettings["dbInitialCatalog_MSSQL"].ToString();
                sqlCnnStrBld.PersistSecurityInfo = true;
                sqlCnnStrBld.UserID = ConfigurationManager.AppSettings["dbUserID_MSSQL"].ToString();
                sqlCnnStrBld.Password = ConfigurationManager.AppSettings["dbPassword_MSSQL"].ToString();
                strSQLconn_MSSQL = sqlCnnStrBld.ConnectionString;
                sqlCnnStrBld.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetDatabaseParameters_SQLite()
        {
            try
            {
                //DialogResult dr = openFileDialog1.ShowDialog();
                //string file = openFileDialog1.FileName;

                SQLiteConnectionStringBuilder sqlCnnStrBld = new SQLiteConnectionStringBuilder();
                sqlCnnStrBld.DataSource = @"C:\Users\SAURON\Desktop\IMAGES.db"; //@"D:\Visual Studio 2015\Projects\suleymangunel\suleymangunel\Databases\Blog.db"; // AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["dbDataSource_SQLite"];
                sqlCnnStrBld.Password = txtCurrentPassword.Text.Trim(); //ConfigurationManager.AppSettings["dbPassword_SQLite"].ToString();
                sqlCnnStrBld.JournalMode = SQLiteJournalModeEnum.Persist;
                sqlCnnStrBld.SyncMode = SynchronizationModes.Full;
                sqlCnnStrBld.Version = 3;
                sqlCnnStrBld.PageSize = 4096; //512 katları, default:4096
                strSQLconn_SQLite = sqlCnnStrBld.ConnectionString;
                sqlCnnStrBld.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetDatabaseParameters_MySQL()
        {
            try
            {
                MySql.Data.MySqlClient.MySqlConnectionStringBuilder sqlCnnStrBld = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
                sqlCnnStrBld.Server = ConfigurationManager.AppSettings["dbServer_MySQL"].ToString();
                sqlCnnStrBld.Database = ConfigurationManager.AppSettings["dbDatabase_MySQL"].ToString();
                sqlCnnStrBld.UserID = ConfigurationManager.AppSettings["dbUserID_MySQL"].ToString();
                sqlCnnStrBld.Password = ConfigurationManager.AppSettings["dbPassword_MySQL"].ToString();
                strSQLconn_MySQL = sqlCnnStrBld.ConnectionString;
                sqlCnnStrBld.Pooling = false;
                sqlCnnStrBld.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UploadImage_MSSQL(string strFullPath, string strFileName)
        {
            try
            {
                string strCMD = "SELECT * FROM Images WHERE Name='1';";
                using (SqlConnection connDEO = new SqlConnection(strSQLconn_MSSQL))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter(strCMD, connDEO);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");

                    byte[] bImageData;
                    if (!ReadFromDisc(strFullPath,out bImageData))
                        return false;

                    DataRow nr = ds.Tables["Images"].NewRow();
                    nr["Name"] = strFileName;
                    nr["Data"] = bImageData;
                    ds.Tables["Images"].Rows.Add(nr);
                    SqlCommandBuilder sqlCmdBld = new SqlCommandBuilder(sqlDA);
                    sqlDA.InsertCommand = sqlCmdBld.GetInsertCommand();
                    //sqlDA.UpdateCommand = sqlCmdBld.GetUpdateCommand();
                    //sqlDA.DeleteCommand = sqlCmdBld.GetDeleteCommand();
                    sqlDA.Update(ds, "Images");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UploadImage_SQLite(string strFullPath, string strFileName)
        {
            try
            {
                string strCMD = "SELECT * FROM Images WHERE Name='1';";
                using (SQLiteConnection connDEO = new SQLiteConnection(strSQLconn_SQLite))
                {
                    SQLiteCommand cmd = new SQLiteCommand(strCMD, connDEO);
                    SQLiteDataAdapter sqlDA = new SQLiteDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");

                    byte[] bImageData;
                    if (!ReadFromDisc(strFullPath, out bImageData))
                        return false;

                    DataRow nr = ds.Tables["Images"].NewRow();
                    nr["Name"] = strFileName;
                    nr["Data"] = bImageData;
                    ds.Tables["Images"].Rows.Add(nr);
                    SQLiteCommandBuilder sqlCmdBld = new SQLiteCommandBuilder(sqlDA);
                    sqlDA.InsertCommand = sqlCmdBld.GetInsertCommand();
                    //sqlDA.UpdateCommand = sqlCmdBld.GetUpdateCommand();
                    //sqlDA.DeleteCommand = sqlCmdBld.GetDeleteCommand();
                    sqlDA.Update(ds, "Images");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UploadImage_MySQL(string strFullPath, string strFileName)
        {
            try
            {
                string strCMD = "SELECT * FROM images WHERE Name='1';";
                using (MySqlConnection connDEO = new MySqlConnection(strSQLconn_MySQL))
                {
                    MySqlDataAdapter sqlDA = new MySqlDataAdapter(strCMD,connDEO);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");

                    byte[] bImageData;
                    if(!ReadFromDisc(strFullPath, out bImageData))
                        return false;

                    DataRow nr = ds.Tables["Images"].NewRow();
                    nr["Name"] = strFileName;
                    nr["Data"] = bImageData;
                    ds.Tables["Images"].Rows.Add(nr);
                    MySqlCommandBuilder sqlCmdBld = new MySqlCommandBuilder(sqlDA);
                    sqlDA.InsertCommand = sqlCmdBld.GetInsertCommand();
                    //sqlDA.UpdateCommand = sqlCmdBld.GetUpdateCommand();
                    //sqlDA.DeleteCommand = sqlCmdBld.GetDeleteCommand();
                    sqlDA.Update(ds, "Images");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string DownloadImage_MSSQL(string strPath)
        {
            try
            {
                string strImageName = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                byte[] bImageData;

                string strCMD = "SELECT * FROM Images WHERE Name='" + strImageName + "';";
                using (SqlConnection connDEO = new SqlConnection(strSQLconn_MSSQL))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter(strCMD, connDEO);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");
                    bImageData = (byte[])ds.Tables["Images"].Rows[0]["Data"];
                }
                return SaveToDisc(strPath, strImageName, bImageData);
            }
            catch
            {
                return null;
            }
        }

        public string DownloadImage_SQLite(string strPath)
        {
            try {
                string strImageName = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                byte[] bImageData;

                string strCMD = "SELECT * FROM Images WHERE Name='" + strImageName + "';";
                using (SQLiteConnection connDEO = new SQLiteConnection(strSQLconn_SQLite))
                {
                    SQLiteDataAdapter sqlDA = new SQLiteDataAdapter(strCMD, connDEO);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");
                    bImageData = (byte[])ds.Tables["Images"].Rows[0]["Data"];
                }
                return SaveToDisc(strPath, strImageName, bImageData);
            }
            catch
            {
                return null;
            }
        }

        public string DownloadImage_MySQL(string strPath)
        {
            try
            {
                string strImageName = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                byte[] bImageData;

                string strCMD = "SELECT * FROM Images WHERE Name='" + strImageName + "';";
                using (MySqlConnection connDEO = new MySqlConnection(strSQLconn_MySQL))
                {
                    MySqlDataAdapter sqlDA = new MySqlDataAdapter(strCMD, connDEO);
                    DataSet ds = new DataSet();
                    sqlDA.Fill(ds, "Images");
                    bImageData = (byte[])ds.Tables["Images"].Rows[0]["Data"];
                }
                return SaveToDisc(strPath, strImageName, bImageData);
            }
            catch
            {
                return null;
            }
        }

        public bool ReadFromDisc(string strFullPath,out byte[] bImageData)
        {
            bImageData = null;
            try {
                //nr["Data"] = File.ReadAllBytes(@"C:\Users\SAURON\Desktop\001.gif"); //BinaryRead faster than this
                FileStream fsFile = new FileStream(strFullPath, FileMode.Open);
                FileInfo fiFile = new FileInfo(strFullPath);
                int iBoy = (Int32)fiFile.Length;
                BinaryReader brImage = new BinaryReader(fsFile);
                bImageData = brImage.ReadBytes(iBoy);
                fsFile.Close();
                fsFile.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string SaveToDisc(string strPath, string strImageName, byte[] bImageData)
        {
            try
            {
                //File.WriteAllBytes(@"C:\Users\SAURON\Desktop\benim.gif", (byte[])ds.Tables["Images"].Rows[0]["Data"]); //BinaryWrite faster than this
                FileStream fsFile = new FileStream(strPath + strImageName, FileMode.Create);
                BinaryWriter bwImage = new BinaryWriter(fsFile);
                bwImage.Write(bImageData);
                fsFile.Close();
                fsFile.Dispose();
                return strImageName;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void tsCbAspectRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPicture((tsCbAspectRatio.SelectedIndex == 0 ? true : false));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ShowPicture((tsCbAspectRatio.SelectedIndex == 0 ? true : false));
        }
    }
}
