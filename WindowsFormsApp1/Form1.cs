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
        string SearchingMode;
        List<string> PasswordList= new List<string>();
        public Form1()
        {
            InitializeComponent();
            SearchingModeBox.Text = "単語検索";
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
            this.Text = "Yokai Searcher (水咲製作)" + Properties.Resources.VersionText;
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
                switch (SearchingMode)
                {
                    case "単語検索":
                        if (Passwords[i].Contains(SearchWord))
                        {

                            PasswordList.Add(Passwords[i]);
                            ResultCount++;
                        }
                        break;
                    case "先頭検索":
                        if (Passwords[i].StartsWith(SearchWord)){
                            PasswordList.Add(Passwords[i]);
                            ResultCount++;
                        }
                        break;
                    case "終端検索":
                        if (Passwords[i].EndsWith(SearchWord))
                        {
                            PasswordList.Add(Passwords[i]);
                            ResultCount++;
                        }
                        break;
                }
                ProgressInt++;

            }
            isCompleted = true;
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchEnter();
        }
        void SearchEnter()
        {
            isCompleted = false;
            SearchButton.Enabled = false;
            SearchWord = SearchTextBox.Text;
            SearchingMode = SearchingModeBox.Text;

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

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchEnter();
            }
        }
    }
}
