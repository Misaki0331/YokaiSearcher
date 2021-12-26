using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] Passwords;
        string[] Passwords_temp;
        string[] Passwords_temp2;
        string SearchWord;
        bool isCompleted;
        int ProgressInt;
        int ResultCount;
        string SearchingMode;
        string ThreadErrorText;
        const int maxList = 10000;
        List<string> PasswordList= new List<string>();
        List<string> PasswordList2= new List<string>();
        public Form1()
        {
            InitializeComponent();
            Show();
            SearchingModeBox.Text = "単語検索";
            Application.DoEvents();
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
            this.Text = "Yokai Searcher 水咲(みさき)" + Properties.Resources.VersionText;
           
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
            try
            {
                PasswordList.Clear();
                PasswordList2.Clear();
                ProgressInt = 0;
                ResultCount = 0;
                Regex rx = null;
                if (SearchingMode == "正規表現") rx = new Regex(SearchWord, RegexOptions.Compiled);

                for (int i = 0; i < Passwords_temp.Length; i++)
                {
                    switch (SearchingMode)
                    {
                        case "単語検索":
                            if (Passwords_temp[i].Contains(SearchWord))
                            {

                                PasswordList.Add(Passwords_temp[i]);
                                if(ResultCount<maxList)PasswordList2.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                        case "先頭検索":
                            if (Passwords_temp[i].StartsWith(SearchWord))
                            {
                                 PasswordList.Add(Passwords_temp[i]);
                                if (ResultCount < maxList) PasswordList2.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                        case "終端検索":
                            if (Passwords_temp[i].EndsWith(SearchWord))
                            {
                                 PasswordList.Add(Passwords_temp[i]);
                                if (ResultCount < maxList) PasswordList2.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                        case "正規表現":
                            if (rx.IsMatch(Passwords_temp[i]))
                            {
                                PasswordList.Add(Passwords_temp[i]);
                                if (ResultCount < maxList) PasswordList2.Add(Passwords_temp[i]);
                                ResultCount++;
                            }
                            break;
                    }
                    ProgressInt++;


                }
                isCompleted = true;
                if (ResultCount > maxList) throw new IndexOutOfRangeException($"{maxList} 個を超えています。一部は省略しました。");
                ThreadErrorText = "OK";
            }
            catch(Exception ex)
            {
                ThreadErrorText = ex.Message;
            }
            
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchEnter();
        }
        void SearchEnter()
        {
            if (SearchTextBox.Text.Length > 14)
            {
                ErrorText.Text = $"14文字を超えています。";
                return;            
            }
            if (SearchingModeBox.Text != "正規表現")
            {
                if (!checkTextBox(SearchTextBox.Text))
                {
                    ErrorText.Text = $"無効な文字が含まれています。";
                    return;
                }
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
            if (ThreadErrorText != "OK")
            {
                ErrorText.Text = ThreadErrorText;
                ThreadErrorText = string.Empty;
            }
            Passwords_temp = PasswordList.ToArray();
            Passwords_temp2 = PasswordList2.ToArray();
            StringComparer cmp = StringComparer.Ordinal;
            Array.Sort(Passwords_temp2, cmp);
            PasswordResultBox.Lines = Passwords_temp2;
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
                
                progressBar1.Maximum = Passwords_temp.Length;
                progressBar1.Value = ProgressInt;
            }
            catch
            {

            }
            label1.Text = $"見つかった数 : {ResultCount}件";
            label3.Text = $"{((double)ProgressInt / Passwords_temp.Length*100.00).ToString("F2")}%";
            
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

            Passwords_temp = Passwords;
        }


        private bool checkTextBox(string str)
        {
            string contain = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!-.nmc";
            int count = 0;
            if (str.Length > 14)
            {
                return false;
            }
            for(int i = 0; i < str.Length; i++)
            {
                for(int j = 0; j <= 42; j++)
                {
                    if (j==42)
                    {
                        return false;
                    }else
                    if (str[i] == contain[j])
                    {
                        Console.WriteLine($"{i}:{str[i]}={j}");
                        count++;
                        break;
                    }
                }
            }
            if (str.Length == count)
            {
                return true;
            }
            return false;
        }
    }
}
