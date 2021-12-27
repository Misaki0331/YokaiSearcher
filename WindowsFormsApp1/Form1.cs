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

namespace YokaiSearcher
{
    public partial class Form1 : Form
    {
        string[] Passwords;
        string[] Passwords_temp;
        string[] Passwords_temp2;
        string SearchWord;
        bool isCompleted;
        bool IsLemitter;
        int ProgressInt;
        int ResultCount;
        string SearchingMode;
        string ThreadErrorText;
        bool IsNotSearch;
        string SearchLog="";
        bool IsFirstSearch;
        bool IsContinueSearch;
        const int maxList = 10000;
        List<string> PasswordList= new List<string>();
        List<string> PasswordList2= new List<string>();
        public Form1()
        {
            InitializeComponent();
            Show();
            SearchingModeBox.Text = "単語検索";
            IsFirstSearch = true;
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
                    if (Passwords_temp[i].Length < 14) continue;
                    switch (SearchingMode)
                    {
                        case "単語検索":
                            if (Passwords_temp[i].Contains(SearchWord))
                            {

                                if(!IsNotSearch)AddCollection(i);
                            }
                            else
                            {
                                if (IsNotSearch) AddCollection(i);
                            }
                            break;
                        case "先頭検索":
                            if (Passwords_temp[i].StartsWith(SearchWord))
                            {

                                if (!IsNotSearch) AddCollection(i);
                            }
                            else
                            {
                                if (IsNotSearch) AddCollection(i);
                            }
                            break;
                        case "終端検索":
                            if (Passwords_temp[i].EndsWith(SearchWord))
                            {

                                if (!IsNotSearch) AddCollection(i);
                            }
                            else
                            {
                                if (IsNotSearch) AddCollection(i);
                            }
                            break;
                        case "正規表現":
                            if (rx.IsMatch(Passwords_temp[i]))
                            {

                                if (!IsNotSearch) AddCollection(i);
                            }
                            else
                            {
                                if (IsNotSearch) AddCollection(i);
                            }
                            break;
                    }
                    ProgressInt++;


                }
                isCompleted = true;
                if (ResultCount > maxList&&!IsLemitter) throw new IndexOutOfRangeException($"{maxList} 個を超えています。一部は省略しました。");
                ThreadErrorText = "OK";
            }
            catch(Exception ex)
            {
                ThreadErrorText = ex.Message;
            }
            
        }
        void AddCollection(int i)
        {
            PasswordList.Add(Passwords_temp[i]);
            if (ResultCount < maxList || IsLemitter) PasswordList2.Add(Passwords_temp[i]);
            ResultCount++;
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchEnter();
        }
        void SearchEnter()
        {
            
            if (SearchingModeBox.Text != "正規表現")
            {
                if (SearchTextBox.Text.Length > 14)
                {
                    ErrorText.Text = $"14文字を超えています。";
                    return;
                }
                if (!checkTextBox(SearchTextBox.Text))
                {
                    ErrorText.Text = $"無効な文字が含まれています。";
                    return;
                }
            }
            IsContinueSearch = SearchContinueCheckBox.Checked;
            if (!IsContinueSearch)
            {
                Passwords_temp = Passwords;
                IsFirstSearch = true;
            }
            IsLemitter = TextLimiterCheckBox.Checked;
            IsNotSearch = SearchNotCheckBox.Checked;
            progressBar1.Value = 0;
            progressBar1.Maximum = Passwords_temp.Length;
            isCompleted = false;
            ErrorText.Text = $"";
            TextLimiterCheckBox.Enabled = false;
            SearchButton.Enabled = false;
            SearchWord = SearchTextBox.Text;
            SearchingMode = SearchingModeBox.Text;
            MTSearch.RunWorkerAsync();
        }
        void LogAdd()
        {
            string tmp;
            if (IsFirstSearch)
            {
                tmp = $"＋";
            }
            else
            {
                tmp = $"↓";
            }
            tmp += $"{ ResultCount.ToString().PadLeft(8, ' ')}件 ";
            if (IsNotSearch)
            {
                tmp += "－";
            }
            else
            {
                tmp += "＆";
            }
            tmp += $"{SearchingMode} {SearchWord}";
            SearchLog += tmp + "\n";
            SearchLogTextBox.Lines = SearchLog.Split('\n');
            SearchLogTextBox.SelectionStart = SearchLogTextBox.Text.Length;
            SearchLogTextBox.ScrollToCaret();
        }
        private void MTSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchButton.Enabled = true;
            TextLimiterCheckBox.Enabled = true;
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
            LogAdd();

            IsFirstSearch = false;
            if (IsContinueSearch)
            {
                if (ResultCount == 0)
                {
                    Passwords_temp = Passwords;
                    ErrorText.Text = $"見つからない為検索リストをリセットします。";

                    IsFirstSearch = true ;
                }
            }
            else
            {
                if (ResultCount == 0)
                {

                    Passwords_temp = Passwords;
                    ErrorText.Text = $"見つかりませんでした。";
                }
            }
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            double percent = ((double)ProgressInt / Passwords_temp.Length * 100.00);
            if (percent > 100) percent = 100;
            try
            {
                
                progressBar1.Value = ProgressInt;
            }
            catch
            {

            }
            label1.Text = $"見つかった数 : {ResultCount}件";
            ProgressLabel.Text = $"{percent.ToString("F2")}%";
            
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
            IsFirstSearch = true;
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

        private void TextLimiterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TextLimiterCheckBox.Checked)
            {
                DialogResult result = MessageBox.Show("表示上限を突破するとアプリが応答しない状態に陥る可能性があります。\nマシンが高スペックまたは大量のデータが必要でない限りは\nこのチェックボックスを無効にすることを推奨します。\n表示上限のリミッターを外しますか？",
                    "警告 (よく読んでね!)",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                if(result== DialogResult.No)
                {
                    TextLimiterCheckBox.Checked = false;
                }
            }
            if (TextLimiterCheckBox.Checked)
            {
                TextLimiterCheckBox.Text = "うぉー！！フル回転！！！！";
            }
            else
            {
                TextLimiterCheckBox.Text = "表示上限を無視する";

            }
        }

        private void LogClearButton_Click(object sender, EventArgs e)
        {

            SearchLog = "";
            SearchLogTextBox.Lines = SearchLog.Split('\n');
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            Console.WriteLine($"{Size.Width},{Size.Height}");
            progressBar1.Size = new Size(this.Size.Width - 6, progressBar1.Size.Height);
            ProgressLabel.Location = new Point(this.Size.Width - 106, ProgressLabel.Location.Y);
            SearchLogTextBox.Location = new Point(this.Size.Width - 288, this.Size.Height - 200);
            LogClearButton.Location = new Point(this.Size.Width - 93, this.Size.Height - 229);
            LogLabel.Location = new Point(this.Size.Width - 290, this.Size.Height - 215);
            PasswordResultBox.Size = new Size(PasswordResultBox.Size.Width, this.Height - 76);
        }
    }
}
