using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace resim2
{
    public partial class Form1 : Form
    {

        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        string path = "database.db";
        string cs = @"URI=file:"+Application.StartupPath+"\\database.db";
        int sayi2 = 0;
       

        public Form1()
        {
            
            InitializeComponent();
            Create_db();
            dataGridView1.RowTemplate.Height = 35;
            
            
            label1.Visible = false;
            label2.Visible = false;
            gizle();

            data_show2(0);
            data_show2(0);


            //Create right click menu..
            ContextMenuStrip s = new ContextMenuStrip();

            // add one right click menu item named as hello           
            ToolStripMenuItem gizleme = new ToolStripMenuItem();
            gizleme.Text = "Hide";

            // add the clickevent of hello item
            gizleme.Click += gizleme_Click;

            // add the item in right click menu
            s.Items.Add(gizleme);



            ToolStripMenuItem goster = new ToolStripMenuItem();
            goster.Text = "Show";

            // add the clickevent of hello item
            goster.Click += goster_Click;

            // add the item in right click menu
            s.Items.Add(goster);


            ToolStripMenuItem txtyaz = new ToolStripMenuItem();
            txtyaz.Text = "Import";

            // add the clickevent of hello item
            txtyaz.Click += txtyaz_Click;

            // add the item in right click menu
            s.Items.Add(txtyaz);



            // attach the right click menu with form
            this.ContextMenuStrip = s;



        }
            void goster_Click(object sender, EventArgs e)
                {
                    goster();
                }

            void gizleme_Click(object sender, EventArgs e)
            {
                gizle();
            }
        void txtyaz_Click(object sender, EventArgs e)
        {
            txtyaz();
        }

        private void calistir()
        {

          
            string q = "";
            try
            {
                String komut = '"' + label1.Text + '"';
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                }



            }
            catch (Exception ex)
            {

                q += "error";
              
            }
    
            

        }

        private void data_show()
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();

                string stm = "select * FROM database LIMIT 10";
                var cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();
                int i = 0;
                

                while (dr.Read())
                {
                    //Image.FromFile(dr.GetValue(1).ToString())
                    dataGridView1.Rows.Insert(0, Image.FromFile(dr.GetValue(1).ToString()), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString());
                    dataGridView1.RowTemplate.Height = 400;

                    Column1.Width = 400;
                    Column2.LinkBehavior = LinkBehavior.SystemDefault;
                }
        

                dr.Close();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ReadOnly = true;

                this.dataGridView1.AllowUserToAddRows = false;
              
            }
            catch (Exception ex)
            {

            
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {



            label1.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();

            calistir();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sayi2 = 0;
            data_show2(0);
        }
        private void txtyaz()
        {
            try
            {

                string dummyFileName = "Save Here";

                SaveFileDialog sf = new SaveFileDialog();
                // Feed the dummy name to the save dialog
                sf.FileName = dummyFileName;
                string txtyaz1 = "";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    // Now here's our save folder
                    txtyaz1 = Path.GetDirectoryName(sf.FileName);
                    // Do whatever
                }


                string fileName = @"1.txt";
              
                File.Delete(fileName);
                //Check if the file exists
                if (!File.Exists(fileName))
                {
                    // Create the file and use streamWriter to write text to it.
                    //If the file existence is not check, this will overwrite said file.
                    //Use the using block so the file can close and vairable disposed correctly
                    using (StreamWriter writer = File.CreateText(fileName))
                    {
                        writer.WriteLine(txtyaz1);




                    }
                   
                    label2.Visible=true;
                    label2.Text="Wait";
                    run2();
              
                    MessageBox.Show("Ok.");
                    label2.Visible=false;
                }

            }
            catch (Exception)
            { }

            data_show2(0);

        }
        private void gizle()
        {

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;


        }
        private void goster()
        {

            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true;

        }
        private void data_show2(int sayi)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            try
            {
                sayi2 = sayi2 + sayi;
                String ara ='%' + textBox1.Text + '%';
                var con = new SQLiteConnection(cs);
                con.Open();

                if (sayi2 < 0)
                {
                    sayi2 = 0;
                }

                string stm = "select * FROM database WHERE r1 LIKE '"+ara +"' OR r2 LIKE '"+ara +"' OR r3 LIKE '"+ara +"' OR r4 LIKE '"+ara +"' OR r5 LIKE '"+ara +"' OR r6 LIKE '"+ara +"'   LIMIT "+ sayi2.ToString() +" , 10";
              
                var cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();
                int i = 0;


                while (dr.Read())
                {
                    //Image.FromFile(dr.GetValue(1).ToString())
                    dataGridView1.Rows.Insert(0, Image.FromFile(dr.GetValue(1).ToString()), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(2).ToString());
                    dataGridView1.RowTemplate.Height = 400;

                    Column1.Width = 400;
                    Column2.LinkBehavior = LinkBehavior.SystemDefault;
                }


                dr.Close();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ReadOnly = true;

                this.dataGridView1.AllowUserToAddRows = false;
              

                int sonsayi = dataGridView1.RowCount;

                if (sonsayi < 10)
                {
                    pictureBox3.Enabled = false;
                }
                else {
                    pictureBox3.Enabled = true;
                }


            }
            catch (Exception ex)
            {


            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sayi2 = 0;
            data_show2(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            data_show2(-10);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            data_show2(10);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {

                    sayi2 = 0;
                    data_show2(0);
                }
                catch (Exception)
                { }


            }
        }

        private void run2()
        {
            var con = new SQLiteConnection(cs);
            con.Close();

            string q = "";
            try
            {
                /*
                String komut = "5.exe";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                }
                */
                string strCmdText;
                strCmdText= "/C 5.exe";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);


            }
            catch (Exception ex)
            {

                

            }
    



        }
        private void Create_db()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    try
                    {

                        sqlite.Open();
                        string sql = "CREATE TABLE database (id INTEGER, r1 TEXT UNIQUE ,  r2 TEXT,  r3 TEXT, r4 TEXT, r5 TEXT, r6 TEXT,  PRIMARY KEY(id AUTOINCREMENT))";
                        SQLiteCommand command = new SQLiteCommand(sql, sqlite);

                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());

                    }



                }

            }



        }





    }
}
