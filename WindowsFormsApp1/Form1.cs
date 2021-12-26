﻿using System;
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
        string[] Passwords_temp;
        string SearchWord;
        bool isCompleted;
        int ProgressInt;
        int ResultCount;
        string SearchingMode;
        bool IsSearchingALL;
        List<string> PasswordList= new List<string>();
        public Form1()
        {
            InitializeComponent();
            IsSearchingALL = true;
            SearchingModeBox.Text = "単語検索";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Loading...";

            ErrorText.Text = "";
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

            Passwords_temp = Passwords;
            isCompleted = true;

        }

        private void MTSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Searching();
            }
            catch (Exception ex){ Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        void Searching()
        {
                PasswordList.Clear();
                ProgressInt = 0;
                ResultCount = 0;
                IsSearchingALL = false;

                for (int i = 0; i < Passwords_temp.Length; i++)
                {
                    switch (SearchingMode)
                    {
                        case "単語検索":
                            if (Passwords_temp[i].Contains(SearchWord))
                            {

                                PasswordList.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                        case "先頭検索":
                            if (Passwords_temp[i].StartsWith(SearchWord))
                            {
                                PasswordList.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                        case "終端検索":
                            if (Passwords_temp[i].EndsWith(SearchWord))
                            {
                                PasswordList.Add(Passwords_temp[i]);
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
            if (!SearchContinueCheckBox.Checked)
            {
                IsSearchingALL = true;
            }
            isCompleted = false;

            ErrorText.Text = $"";
            SearchButton.Enabled = false;
            SearchWord = SearchTextBox.Text;
            SearchingMode = SearchingModeBox.Text;
            MTSearch.RunWorkerAsync();
        }
        private void MTSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchButton.Enabled = true;
            isCompleted = true;
            
            Passwords_temp = PasswordList.ToArray();
            PasswordResultBox.Lines = PasswordList.ToArray();
            if (SearchContinueCheckBox.Checked)
            {
                if (ResultCount == 0)
                {
                    Passwords_temp = Passwords;
                    ErrorText.Text = $"見つからない為検索リストをリセットします。";
                }
            }
            else
            {

                Passwords_temp = Passwords;
                if (ResultCount == 0)
                {

                    ErrorText.Text = $"見つかりませんでした。";
                }
            }
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            IsSearchingALL = true;
        }

    }
}
