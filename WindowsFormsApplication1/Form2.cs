using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Labe : Form
    {
        String idsave, set;
        int s, m = 20, i, questionNumber, score, questionSet;
        String[] questions = new String[25];
        String[] optionA = new String[25];
        String[] optionB = new String[25];
        String[] optionC = new String[25];
        String[] optionD = new String[25];
        String[]  answers= new String[25];
        String[] answerED = new String[25];
        int[] checkBox = new int[25];
        SQLiteConnection m_dbConnection, m_dbConnection1;
        public Labe( string getID, int setpaper)
        {
            InitializeComponent();
            idsave = getID;
            questionSet = setpaper;
            timer1.Start();
            dboper();
            questionDbOpen();
            score = 0;
            i = 0;
            questionNumber = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                checkRadioButton(questionNumber);
                label2.Text = questions[questionNumber + 1];
                radioButton1.Text = optionA[questionNumber + 1];
                radioButton2.Text = optionB[questionNumber + 1];
                radioButton3.Text = optionC[questionNumber + 1];
                radioButton4.Text = optionD[questionNumber + 1];
                settingRadioButtons(questionNumber + 1);
                questionNumber++;
                int die = questionNumber + 1;
                label5.Text = die + ")";
            }
            catch(Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                checkRadioButton(questionNumber);
                label2.Text = questions[questionNumber - 1];
                radioButton1.Text = optionA[questionNumber - 1];
                radioButton2.Text = optionB[questionNumber - 1];
                radioButton3.Text = optionC[questionNumber - 1];
                radioButton4.Text = optionD[questionNumber - 1];
                settingRadioButtons(questionNumber - 1);
                questionNumber--;
                int die = questionNumber + 1;
                label5.Text =  die+")";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Warning", MessageBoxButtons.OK);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void dboper()
        {
            m_dbConnection1 = new SQLiteConnection("Data Source=Tracking.sqlite;Version=3;");
            m_dbConnection1.Open();

        }
        public void questionDbOpen() {
            try {
                switch (questionSet)
                {
                    case 0:
                        {
                            set = "setA";
                            break;
                        }
                    case 1:
                        {
                            set = "setB";
                            break;
                        }
                    case 2:
                        {
                            set = "setC";
                            break;
                        }
                    case 3:
                        {
                            set = "setD";
                            break;
                        }

                }
                m_dbConnection = new SQLiteConnection("Data Source="+set+".sqlite;Version=3;");
                m_dbConnection.Open();
                string sql1 = "select * from questions order by serial";
                SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                SQLiteDataReader reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    questions[i] = reader["Question"].ToString();
                    optionA[i] = reader["Option A"].ToString();
                    optionB[i] = reader["Option B"].ToString();
                    optionC[i] = reader["Option C"].ToString();
                    optionD[i] = reader["Option D"].ToString();
                    answers[i] = reader["Answer"].ToString();
                    i++;
                }
                label2.Text = questions[0];
                radioButton1.Text = optionA[0];
                radioButton2.Text = optionB[0];
                radioButton3.Text = optionC[0];
                radioButton4.Text = optionD[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Warning", MessageBoxButtons.OK);
            }
                }

        private void button3_Click(object sender, EventArgs e)
        {
            finished(questionNumber);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s = s - 1;
            if (s == -1)
            {
                m = m - 1;
                s = 59;
            }
            if (m == 0 && s == 0)
            {
                timer1.Stop();
                MessageBox.Show("Time up", "Warning", MessageBoxButtons.OK);
                finished(questionNumber);
                this.Close();
            }
            String mm = m.ToString("00");
            String ss = s.ToString("00");
            min.Text = mm;
            sec.Text = ss;
        }
        public void checkRadioButton(int questionNumber1)
        {
            if (radioButton1.Checked)
            {
                answerED[questionNumber1] = optionA[questionNumber1];
                checkBox[questionNumber1] = 1;
            }
            else if (radioButton2.Checked)
            {
                answerED[questionNumber1] = optionB[questionNumber1];
                checkBox[questionNumber1] = 2;
            }
            else if (radioButton3.Checked)
            {
                answerED[questionNumber1] = optionC[questionNumber1];
                checkBox[questionNumber1] = 3;
            }
            else if (radioButton4.Checked)
            {
                answerED[questionNumber1] = optionD[questionNumber1];
                checkBox[questionNumber1] = 4;
            }
            else
            {
                answerED[questionNumber1] = "Die";
                checkBox[questionNumber1] = 0;
            }
        }
        public void settingRadioButtons( int questionNumber2)
        {
            switch (checkBox[questionNumber2])
            {
                case 0:
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    break;
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    break;
                case 4:
                    radioButton4.Checked = true;
                    break;
            }
         
        }
        public void finished(int qq)
        {
            checkRadioButton(qq);
            settingRadioButtons(qq);
            int j;
            for(j=0; j<25;j++)
            {
                Boolean value = false;
                if (answerED [j]!= null&& answers[j]!=null)
                {
                    value = answerED[j].Equals(answers[j]);
                }
                if (value)
                {
                    score = score + 1;
                }
            }
            try
            {
                string sql = "update Tracker set score =" + score + " where CognitID1 ='" + idsave + "'";
                Console.WriteLine(sql);
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection1);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Warning", MessageBoxButtons.OK);
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to quit?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Your score is "+ score, "Warning", MessageBoxButtons.OK);
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }
    }
}
