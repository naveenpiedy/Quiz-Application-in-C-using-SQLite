using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public String cognitID; 
        SQLiteConnection m_dbConnection;
        Labe questionAire = Application.OpenForms["Form2"] as Labe;
        Form3 show = new Form3(); 
        public Form1()
        {
            InitializeComponent();
            dboper();
        }
        public void dboper()
        {
            if (!File.Exists("Tracking.sqlite"))
            {
                SQLiteConnection.CreateFile("Tracking.sqlite");
            }
            m_dbConnection = new SQLiteConnection("Data Source=Tracking.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "create table if not exists Tracker (serial integer primary key, CognitId1 varchar(20) Unique, CognitId2 varchar(20) Unique,CognitId3 varchar(20), score int, SetNum int, PhoneNumber varchar(10))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (passwordbox.Text == "start")
            {
                cognitID = cognitIDtextbox.Text;
                String cognID2 = cognitid2.Text;
                String cognID3 = cognitid1.Text;
                String phonenumber = textBox1.Text;
                int questionSet = qpSet.SelectedIndex;
                if (cognitID != null && questionSet >= 0)
                {
                    try
                    {
                        string sql = "insert into Tracker (CognitId1, CognitId2, CognitId3, PhoneNumber, SetNum, score) values ('" + cognitID + "','" + cognID2 + "','" + cognID3 + "','" + phonenumber + "'," + questionSet + ",0)";
                        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                        command.ExecuteNonQuery();
                        if (questionAire == null)
                        {
                            Labe questionAire = new Labe(cognitID, questionSet);
                            questionAire.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace, "Warning", MessageBoxButtons.OK);
                    }
                }
                else
                    MessageBox.Show("Enter details", "Warning", MessageBoxButtons.OK);
            }
            else if (passwordbox.Text == "show")
            {
                show.Show();
            }
            cognitIDtextbox.Text = "";
            cognitid1.Text = "";
            cognitid2.Text = "";
            passwordbox.Text = "";
            textBox1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
