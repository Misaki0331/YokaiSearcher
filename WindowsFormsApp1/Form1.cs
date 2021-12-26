using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] Passwords;
        string SearchWord;
        bool isCompleted;
        int ProgressInt;
        int ResultCount;
        List<string> PasswordList= new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Loading...";
            isCompleted = false;
            Initwork.RunWorkerAsync();
            while (!isCompleted)
            {
                Application.DoEvents();
            }
            Refreshing.Enabled = true;
            this.Text = "Yokai Searcher (水咲製作) Version.1.00";
        }


        private void initwork(object sender, DoWorkEventArgs e)
        {
            Passwords = Properties.Resources.passwords.Split('\n');
            isCompleted = true;

        }

        private void MTSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            Searching();
        }

        void Searching()
        {
            PasswordList.Clear();
            ProgressInt = 0;
            ResultCount = 0;
            for (int i = 0; i < Passwords.Length; i++)
            {
                if (Passwords[i].Contains(SearchWord))
                {
                    PasswordList.Add(Passwords[i]);
                    ResultCount++;
                }
                ProgressInt++;
            }
            isCompleted = true;
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            isCompleted = false;
            SearchButton.Enabled = false;
            SearchWord = SearchTextBox.Text;
            //Searching();
            MTSearch.RunWorkerAsync();
        }

        private void MTSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchButton.Enabled = true;
            isCompleted = true;
            PasswordResultBox.Lines = PasswordList.ToArray();
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            try
            {

                progressBar1.Value = ProgressInt;
                label1.Text = $"Found : {ResultCount}";
            }
            catch
            {

            }
        }
    }
}
