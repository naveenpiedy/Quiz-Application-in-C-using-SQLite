using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        SQLiteConnection m_dbConnection1, m_dbConnection;
        public Form3()
        {
            InitializeComponent();
            if (!File.Exists("Tracking.sqlite"))
            {
                SQLiteConnection.CreateFile("Tracking.sqlite");
            }
            m_dbConnection = new SQLiteConnection("Data Source=Tracking.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "create table if not exists Tracker (serial integer primary key, CognitId1 varchar(20) Unique, CognitId2 varchar(20) Unique,CognitId3 varchar(20), score int, SetNum int, PhoneNumber varchar(10))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            dboper();
        }
        public void dboper()
        {
            m_dbConnection1 = new SQLiteConnection("Data Source=Tracking.sqlite;Version=3;");
            m_dbConnection1.Open();
            string sql = "select * from Tracker order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection1);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["serial"].ToString());
                item.SubItems.Add(reader["CognitID1"].ToString());
                item.SubItems.Add(reader["CognitID2"].ToString());
                item.SubItems.Add(reader["CognitID3"].ToString());
                item.SubItems.Add(reader["score"].ToString());
                item.SubItems.Add(reader["PhoneNumber"].ToString());
                listView1.Items.Add(item);
            }
        }
    }
}
