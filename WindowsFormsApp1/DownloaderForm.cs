using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YokaiSearcher
{
    public partial class DownloaderForm : Form
    {
        System.Net.WebClient downloadClient = null;
        int progresscount = 0;
        int filescount = 0;
        public bool downloading = true;
        bool IsNextGo;
        string[] downloadlist;
        bool DoneFlg;
        void Download()
        {
            try
            {
                Directory.CreateDirectory("temp");
                downloadlist = Properties.Resources.DownloadList.Split('\n');
                filescount = downloadlist.Length;
                for (progresscount = 0; progresscount < filescount; progresscount++)
                {
                    string fileName = $"temp/{progresscount}.bin";
                    //ダウンロード基のURL
                    Uri u = new Uri(downloadlist[progresscount]);

                    //WebClientの作成
                    if (downloadClient == null)
                    {
                        downloadClient = new System.Net.WebClient();
                        //イベントハンドラの作成
                        downloadClient.DownloadProgressChanged +=
                            new System.Net.DownloadProgressChangedEventHandler(
                                downloadClient_DownloadProgressChanged);
                        downloadClient.DownloadFileCompleted +=
                            new System.ComponentModel.AsyncCompletedEventHandler(
                                downloadClient_DownloadFileCompleted);
                    }
                    //非同期ダウンロードを開始する
                    downloadClient.DownloadFileAsync(u, fileName);
                    while (!IsNextGo)
                    {

                        Thread.Sleep(50);
                        if (DoneFlg)
                        {
                            break;
                        }
                    }
                    IsNextGo = false;
                }
            
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                DoneFlg = true;
            }
}
        int percent;
        long downloadTotalSize;
        long downloadGetSize;
        void downloadClient_DownloadProgressChanged(object sender,
            System.Net.DownloadProgressChangedEventArgs e)
        {
            try
            {
                downloadTotalSize = e.TotalBytesToReceive;
                downloadGetSize =e .BytesReceived;
                double p = (double)e.BytesReceived / e.TotalBytesToReceive * 1000.0;
                if (e.TotalBytesToReceive == 0)
                {

                    percent = prc * 1000;
                }
                else
                {
                    percent = prc * 1000 + (int)p;
                }
            }
            catch
            {

            }
        }
        
        int prc = 0;
        int errorcount = 0;
        void downloadClient_DownloadFileCompleted(object sender,
    System.ComponentModel.AsyncCompletedEventArgs e)
            {
            try
            {
                if (e.Cancelled)
                {
                    DoneFlg = true;
                }
                else if (e.Error != null)
                {
                    errorcount++;
                    progresscount--;
                    if (errorcount > 10)
                    {
                        if (errorcount > 11) return;
                        MessageBox.Show($"ダウンロード中にエラーが発生しました。\n{e.Error}",
                            "ダウンロードエラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        DoneFlg = true;
                    }else
                    IsNextGo = true;
                }
                else
                {
                    errorcount = 0;
                    IsNextGo = true;
                    prc++;
                    percent = prc * 1000;
                }
                if (prc == filescount)
                {
                    DoneFlg = true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
        public DownloaderForm()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
            Focus();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (downloadClient != null)
            {
                downloadClient.CancelAsync();
            }   
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Download();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DoneFlg)
            {

                Close();
                downloading = false;
            }
            progressBar1.Maximum = filescount * 1000;
            progressBar1.Value = percent;
            if (downloadlist == null) return;
            label2.Text = $"{prc}/{filescount} {(percent%1000/10.0).ToString("F1")}% ({(downloadGetSize/1024/1024.0).ToString("F2")}MB/{(downloadTotalSize / 1024 / 1024.0).ToString("F2")}MB)";
            Text = $"ダウンロード進行中... {((double)percent / filescount/10).ToString("F2")}%";
        }

        private void DownloaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!DoneFlg)e.Cancel = true;
        }
        int count = 1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            string str = "";
            
            for(int i=0; i < count; i++)
            {
                str += ".";
            }
            count++;
            if (count > 5) count = 1;
            label1.Text = "現在パスワードをダウンロード中" + str;
        }
    }
}
