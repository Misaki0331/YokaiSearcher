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
using System.Threading;
using System.IO;

namespace YokaiSearcher
{
    public partial class Form1 : Form
    {
        int progress_value = 0;
        int progress_max = 100;
        string progress_title ="";
        ProgressWindow pw = new ProgressWindow();

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

            this.Text = "Yokai Searcher 水咲(みさき)" + Properties.Resources.VersionText;
            Show();
            SearchingModeBox.Text = "単語検索";
            IsFirstSearch = true;
            Application.DoEvents();
        }
        bool FirstRun;
        void ButtonsEnable(bool Is)
        {
            SearchButton.Enabled = Is;
            SaveResultButton.Enabled = Is;
            UpdateButton.Enabled = Is;
            SearchClearButton.Enabled = Is;
            SearchTextBox.Enabled = Is;
            LoadSearchIndexButton.Enabled = Is;
            SearchClearButton.Enabled = Is;
            SearchContinueCheckBox.Enabled = Is;
            SearchNotCheckBox.Enabled = Is;
            TextLimiterCheckBox.Enabled = Is;
            SearchingModeBox.Enabled = Is;
        }
        void WaitForComplete()
        {
            Hide();
            pw.ExecuteTitle = progress_title;
            pw.Show();
            isCompleted = false;
            while (!isCompleted)
            {
                Text = progressStr;
                try
                {
                    pw.progressBar.Maximum = progress_max;
                    pw.progressBar.Value = progress_value;
                }
                catch
                {

                }
                pw.ProgressName.Text = progressStr;
                Application.DoEvents();
                Thread.Sleep(20);
            }
            pw.Hide();
            Show();
            this.Text = "Yokai Searcher 水咲(みさき)" + Properties.Resources.VersionText;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Loading...";

            ErrorText.Text = "";
            isCompleted = false;
            FirstRun = true;


            Refreshing.Enabled = true;
            ButtonsEnable(false);
            this.Text = "Yokai Searcher 水咲(みさき)" + Properties.Resources.VersionText;
           
        }
        void Reload()
        {
            StreamReader sr=null;
            try
            {
                int count = 0;
                int maxc = Properties.Resources.DownloadList.Split('\n').Length;
                for (int i = 0; i < maxc; i++)
                {
                    sr = new StreamReader($"temp/{i}.bin", Encoding.GetEncoding("Shift_JIS"));
                    while (sr.Peek() != -1)
                    {
                        string str = sr.ReadLine();
                        if (checkTextBox(str))
                        {
                            count++;
                            if (count % 1000 == 0) progressStr = $"{i}/{maxc} 取得中 : {count} 件";
                            PasswordList.Add(str);
                        }
                    }
                    Application.DoEvents();
                    sr.Close();
                }
                progressStr = $"データをメモリに格納中...";
                Passwords = PasswordList.ToArray();
                PasswordList.Clear();
            }
            catch (Exception ex)
            {
                if(sr!=null)sr.Close();
                PasswordList.Clear();
                Console.WriteLine(ex.Message);
            }
            Passwords_temp = Passwords;

            progressStr = $"メモリの掃除中...";
            GC.Collect();
        }
        bool downloadflg = false;
        string progressStr="";
        private void initwork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(500);
            progressStr = "データを読み込んでいます...";
            try
            {
                int count = 0;
                int maxc = Properties.Resources.DownloadList.Split('\n').Length;
                for (int i = 0; i < maxc; i++)
                {
                    StreamReader sr = new StreamReader($"temp/{i}.bin", Encoding.GetEncoding("Shift_JIS"));
                    while (sr.Peek() != -1)
                    {
                        string str = sr.ReadLine();
                        if (str.Length == 14)
                        {

                            count++;
                            if(count%1000==0)progressStr = $"{i}/{maxc} 取得中 : { string.Format("{0:N0}", count)} 件";
                            progress_max = maxc;
                            progress_value = i;
                            PasswordList.Add(str);
                        }
                    }
                    sr.Close();
                }

                progress_value = maxc;
                progressStr = $"データをメモリに格納中...";
                Passwords = PasswordList.ToArray();
                PasswordList.Clear();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                progressStr = "データ入力エラー発生";

                PasswordList.Clear();
                DialogResult result = MessageBox.Show("パスワードのデータをダウンロードする必要があります。\nモバイル回線の方はWi-Fi回線に接続することを強く推奨します。\nダウンロードしますか？",
                    "ダウンロードが必要です！",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    progressStr = "ダウンロード中...";
                    downloadflg = true;
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
            catch (Exception ex)
            {

                PasswordList.Clear();
                Console.WriteLine(ex.Message);
            }
            Passwords_temp = Passwords;
            isCompleted = true;

            progressStr = $"メモリの掃除中...";
            GC.Collect();

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
                if (ResultCount > maxList&&!IsLemitter) throw new IndexOutOfRangeException($"{ string.Format("{0:N0}", maxList)} 個を超えています。一部は省略しました。");
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
            string search=SearchTextBox.Text;
            search = search.Replace("な", "n");
            search = search.Replace("む", "m");
            search = search.Replace("こ", "c");
            search = search.Replace("ナ", "n");
            search = search.Replace("ム", "m");
            search = search.Replace("コ", "c");
            search = search.Replace("ﾅ", "n");
            search = search.Replace("ﾑ", "m");
            search = search.Replace("ｺ", "c");

            search = search.Replace("！", "!");

            if (SearchingModeBox.Text != "正規表現")
            {
                if (search.Length > 14)
                {
                    ErrorText.Text = $"14文字を超えています。";
                    return;
                }
                if (!checkTextBox(search))
                {
                    ErrorText.Text = $"無効な文字が含まれています。";
                    return;
                }
            }
            if (Passwords_temp == null)
            {
                ErrorText.Text = $"データがありません。データ更新からダウンロードしてください。";
                return;
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
            ButtonsEnable(false);
            SearchWord = search;
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
            GC.Collect();
            ButtonsEnable(true);
            isCompleted = true;
            
            if (ThreadErrorText != "OK")
            {
                ErrorText.Text = ThreadErrorText;
                ThreadErrorText = string.Empty;
            }
            Passwords_temp = PasswordList.ToArray();
            Passwords_temp2 = PasswordList2.ToArray();
            PasswordList.Clear();
            PasswordList2.Clear();
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
        bool IsOpenData;
        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (FirstRun)
            {

                this.Text = "Yokai Searcher 水咲(みさき)" + Properties.Resources.VersionText+" Please wait...";
                FirstRun = false;
                Initwork.RunWorkerAsync();
                progress_title = "起動処理を行っています";
                WaitForComplete();
                ButtonsEnable(true);
                if (Passwords!=null)ListCount.Text = $"パスワード数 : { string.Format("{0:N0}", Passwords.Length)} 件";
            }
            if (downloadflg)
            {
                downloadflg = false;
                pw.Hide();
                DownloaderForm dl = new DownloaderForm();
                dl.Show();
                Hide();
                Enabled = false;
                while (dl.downloading)
                {
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
                Text= "パスワードリスト更新中...";
                Show();
                Application.DoEvents();
                Enabled = true;
                isCompleted = false;
                ReloadThread.RunWorkerAsync();
                Focus();

                progress_title = "データを読み込んでいます";
                WaitForComplete();
                if (Passwords != null) ListCount.Text = $"パスワード数 : { string.Format("{0:N0}", Passwords.Length)} 件";
            }
            if (SavingFlg)
            {
                SavingFlg = false;
                ButtonsEnable(false);
                SaveResult.RunWorkerAsync();
                progress_title = "検索結果の保存中";
                WaitForComplete();
                ButtonsEnable(true);
                if (ThreadErrorText == "")
                {
                    MessageBox.Show("検索結果を正常に保存しました。",
                        "データ保存成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show($"検索結果を正常に保存できませんでした。\n{ThreadErrorText}",
                        "データ保存失敗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
            if (IsOpenData)
            {
                IsOpenData = false;
                ButtonsEnable(false);
                OpenResult.RunWorkerAsync();
                progress_title = "データの読込中";
                WaitForComplete();
                ButtonsEnable(true);
                if (ThreadErrorText == "")
                {
                    SearchContinueCheckBox.Checked = true;
                    IsFirstSearch = false;
                    SearchLog += $"⇒{OpenedCount.ToString().PadLeft(8, ' ')}件 データ読込\n";

                    SearchLogTextBox.Lines = SearchLog.Split('\n');
                    MessageBox.Show($"データを一時的に読み込みしました。\n新規検索またはクリアした場合は再度読み込みする必要があります。\n\n有効なデータ数 : { string.Format("{0:N0}", OpenedCount)} 件",
                        "データ読込完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show($"データの入力に失敗しました。\n{ThreadErrorText}",
                        "データの読込失敗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
            double percent = 0;
            if (Passwords_temp!=null) percent = ((double)ProgressInt / Passwords_temp.Length * 100.00);
            if (percent > 100) percent = 100;
            try
            {
                
                progressBar1.Value = ProgressInt;
            }
            catch
            {

            }
            label1.Text = $"見つかった数 : { string.Format("{0:N0}", ResultCount)}件";
            ProgressLabel.Text = $"{percent.ToString("F2")}%";
            
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter&&SearchButton.Enabled)
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
            progressBar1.Size = new Size(this.Size.Width - 8, progressBar1.Size.Height);
            ProgressLabel.Location = new Point(this.Size.Width - 106, ProgressLabel.Location.Y);
            SearchLogTextBox.Location = new Point(this.Size.Width - 288, this.Size.Height - 200);
            LogClearButton.Location = new Point(this.Size.Width - 93, this.Size.Height - 229);
            LogLabel.Location = new Point(this.Size.Width - 290, this.Size.Height - 215);
            PasswordResultBox.Size = new Size(PasswordResultBox.Size.Width, this.Height - 76);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("パスワードデータを更新しますか？\n(更新中にキャンセルすると再ダウンロードが必要になります。)\n更新する際は安定した環境で行ってください。",
                    "ダウンロードの確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                downloadflg = true;
            }
            
        }
        private void ReloadThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Reload();
        }

        private void ReloadThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            isCompleted = true; ;
        }

        private void SaveResultButton_Click(object sender, EventArgs e)
        {
            if(ResultCount>0)
            saveFileDialog.ShowDialog();
        }
        bool SavingFlg = false;
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            SavingFlg = true;

        }

        private void SaveResult_DoWork(object sender, DoWorkEventArgs e)
        {
            progress_value = 0;
            progress_max = 100;
            progressStr = $"ソートしています...\n(この処理には時間がかかることがあります)";

            StringComparer cmp = StringComparer.Ordinal;
            Array.Sort(Passwords_temp, cmp);
            GC.Collect();
            try
            {
                // テキストファイル出力（新規作成）
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false))
                {
                    int c=Passwords_temp.Length;
                    for(int i = 0; i < c; i++)
                    {
                        sw.WriteLine(Passwords_temp[i]);

                        if (i % 1000 == 0)
                        {
                            progress_max = c;
                            progress_value = i;
                            progressStr = $"結果を出力中...\n{ string.Format("{0:N0}", i)}/{ string.Format("{0:N0}", c)} ({((double)i / c * 100.0).ToString("F2")}%)";
                        }
                    }
                }
                ThreadErrorText = "";
            }
            // 例外処理
            catch (IOException ex)
            {
                ThreadErrorText = ex.Message;
            }
            isCompleted = true;
            GC.Collect();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            IsOpenData = true;
        }
        int OpenedCount;
        private void OpenResult_DoWork(object sender, DoWorkEventArgs e)
        {
            progress_value = 0;
            progress_max = 100;
            OpenedCount = 0;
            int count = 0;
            progressStr = "解析済みデータの読み込み中です...";
            try
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding("Shift_JIS"));
                while (sr.Peek() != -1)
                {
                    count++;
                    string str = sr.ReadLine();
                    if (checkTextBox(str))
                    {
                        OpenedCount++;
                        if (OpenedCount % 1000 == 0) { 
                            progressStr = $"データ読込中... { string.Format("{0:N0}", OpenedCount)} 件";
                        }
                        PasswordList.Add(str);
                    }
                }
                if (OpenedCount == 0)
                {
                    throw new ArgumentOutOfRangeException("有効なパスワードがありませんでした。正しいファイルであるかご確認ください。");

                }
                progress_value = 99;
                progressStr = "データをメモリに格納中です...";
                Passwords_temp = PasswordList.ToArray();
                PasswordList.Clear();
                ThreadErrorText = "";
            }catch(Exception ex)
            {
                ThreadErrorText = ex.Message;
            }
            GC.Collect();

        }

        private void OpenResult_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isCompleted = true;
        }

        private void LoadSearchIndexButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
    }
}
