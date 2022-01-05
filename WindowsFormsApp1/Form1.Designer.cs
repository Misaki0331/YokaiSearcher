
namespace YokaiSearcher
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.Initwork = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.MTSearch = new System.ComponentModel.BackgroundWorker();
            this.PasswordResultBox = new System.Windows.Forms.TextBox();
            this.Refreshing = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SearchingModeBox = new System.Windows.Forms.ComboBox();
            this.SearchClearButton = new System.Windows.Forms.Button();
            this.ErrorText = new System.Windows.Forms.Label();
            this.SearchContinueCheckBox = new System.Windows.Forms.CheckBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.TextLimiterCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchNotCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchLogTextBox = new System.Windows.Forms.TextBox();
            this.LogLabel = new System.Windows.Forms.Label();
            this.LogClearButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ListCount = new System.Windows.Forms.Label();
            this.ReloadThread = new System.ComponentModel.BackgroundWorker();
            this.SaveResultButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SaveResult = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenResult = new System.ComponentModel.BackgroundWorker();
            this.LoadSearchIndexButton = new System.Windows.Forms.Button();
            this.Password_Dif = new System.Windows.Forms.Button();
            this.DifListThread = new System.ComponentModel.BackgroundWorker();
            this.SamplingThread = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Enabled = false;
            this.SearchTextBox.Location = new System.Drawing.Point(325, 51);
            this.SearchTextBox.MaxLength = 100;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(177, 19);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(508, 50);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(49, 19);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // Initwork
            // 
            this.Initwork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.initwork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Maximum = 4244113;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(800, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // MTSearch
            // 
            this.MTSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MTSearch_DoWork);
            this.MTSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MTSearch_RunWorkerCompleted);
            // 
            // PasswordResultBox
            // 
            this.PasswordResultBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordResultBox.Location = new System.Drawing.Point(12, 29);
            this.PasswordResultBox.MaxLength = 99999999;
            this.PasswordResultBox.Multiline = true;
            this.PasswordResultBox.Name = "PasswordResultBox";
            this.PasswordResultBox.ReadOnly = true;
            this.PasswordResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PasswordResultBox.Size = new System.Drawing.Size(191, 409);
            this.PasswordResultBox.TabIndex = 4;
            // 
            // Refreshing
            // 
            this.Refreshing.Interval = 20;
            this.Refreshing.Tick += new System.EventHandler(this.Refresh_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ここに見つかった数が表示されます。";
            // 
            // SearchingModeBox
            // 
            this.SearchingModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchingModeBox.FormattingEnabled = true;
            this.SearchingModeBox.Items.AddRange(new object[] {
            "単語検索",
            "先頭検索",
            "終端検索",
            "正規表現"});
            this.SearchingModeBox.Location = new System.Drawing.Point(229, 50);
            this.SearchingModeBox.Name = "SearchingModeBox";
            this.SearchingModeBox.Size = new System.Drawing.Size(90, 20);
            this.SearchingModeBox.TabIndex = 7;
            // 
            // SearchClearButton
            // 
            this.SearchClearButton.Location = new System.Drawing.Point(563, 50);
            this.SearchClearButton.Name = "SearchClearButton";
            this.SearchClearButton.Size = new System.Drawing.Size(75, 19);
            this.SearchClearButton.TabIndex = 8;
            this.SearchClearButton.Text = "検索クリア";
            this.SearchClearButton.UseVisualStyleBackColor = true;
            this.SearchClearButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ErrorText
            // 
            this.ErrorText.AutoSize = true;
            this.ErrorText.ForeColor = System.Drawing.Color.Red;
            this.ErrorText.Location = new System.Drawing.Point(227, 85);
            this.ErrorText.Name = "ErrorText";
            this.ErrorText.Size = new System.Drawing.Size(137, 12);
            this.ErrorText.TabIndex = 9;
            this.ErrorText.Text = "ここにエラーが表示されます。";
            // 
            // SearchContinueCheckBox
            // 
            this.SearchContinueCheckBox.AutoSize = true;
            this.SearchContinueCheckBox.Location = new System.Drawing.Point(229, 31);
            this.SearchContinueCheckBox.Name = "SearchContinueCheckBox";
            this.SearchContinueCheckBox.Size = new System.Drawing.Size(98, 16);
            this.SearchContinueCheckBox.TabIndex = 10;
            this.SearchContinueCheckBox.Text = "続けて検索する";
            this.SearchContinueCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Location = new System.Drawing.Point(700, 29);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(100, 18);
            this.ProgressLabel.TabIndex = 11;
            this.ProgressLabel.Text = "label3";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextLimiterCheckBox
            // 
            this.TextLimiterCheckBox.AutoSize = true;
            this.TextLimiterCheckBox.ForeColor = System.Drawing.Color.Red;
            this.TextLimiterCheckBox.Location = new System.Drawing.Point(463, 31);
            this.TextLimiterCheckBox.Name = "TextLimiterCheckBox";
            this.TextLimiterCheckBox.Size = new System.Drawing.Size(124, 16);
            this.TextLimiterCheckBox.TabIndex = 12;
            this.TextLimiterCheckBox.Text = "表示上限を無視する";
            this.TextLimiterCheckBox.UseVisualStyleBackColor = true;
            this.TextLimiterCheckBox.CheckedChanged += new System.EventHandler(this.TextLimiterCheckBox_CheckedChanged);
            // 
            // SearchNotCheckBox
            // 
            this.SearchNotCheckBox.AutoSize = true;
            this.SearchNotCheckBox.Location = new System.Drawing.Point(325, 31);
            this.SearchNotCheckBox.Name = "SearchNotCheckBox";
            this.SearchNotCheckBox.Size = new System.Drawing.Size(71, 16);
            this.SearchNotCheckBox.TabIndex = 13;
            this.SearchNotCheckBox.Text = "NOT検索";
            this.SearchNotCheckBox.UseVisualStyleBackColor = true;
            // 
            // SearchLogTextBox
            // 
            this.SearchLogTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchLogTextBox.Location = new System.Drawing.Point(518, 275);
            this.SearchLogTextBox.Multiline = true;
            this.SearchLogTextBox.Name = "SearchLogTextBox";
            this.SearchLogTextBox.ReadOnly = true;
            this.SearchLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SearchLogTextBox.Size = new System.Drawing.Size(270, 163);
            this.SearchLogTextBox.TabIndex = 14;
            // 
            // LogLabel
            // 
            this.LogLabel.AutoSize = true;
            this.LogLabel.Location = new System.Drawing.Point(516, 260);
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(47, 12);
            this.LogLabel.TabIndex = 15;
            this.LogLabel.Text = "検索ログ";
            // 
            // LogClearButton
            // 
            this.LogClearButton.Location = new System.Drawing.Point(713, 246);
            this.LogClearButton.Name = "LogClearButton";
            this.LogClearButton.Size = new System.Drawing.Size(75, 23);
            this.LogClearButton.TabIndex = 16;
            this.LogClearButton.Text = "ログ消去";
            this.LogClearButton.UseVisualStyleBackColor = true;
            this.LogClearButton.Click += new System.EventHandler(this.LogClearButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Enabled = false;
            this.UpdateButton.Location = new System.Drawing.Point(229, 134);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 17;
            this.UpdateButton.Text = "データ更新";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ListCount
            // 
            this.ListCount.AutoSize = true;
            this.ListCount.Location = new System.Drawing.Point(227, 160);
            this.ListCount.Name = "ListCount";
            this.ListCount.Size = new System.Drawing.Size(0, 12);
            this.ListCount.TabIndex = 18;
            // 
            // ReloadThread
            // 
            this.ReloadThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReloadThread_DoWork);
            this.ReloadThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ReloadThread_RunWorkerCompleted);
            // 
            // SaveResultButton
            // 
            this.SaveResultButton.Location = new System.Drawing.Point(310, 100);
            this.SaveResultButton.Name = "SaveResultButton";
            this.SaveResultButton.Size = new System.Drawing.Size(75, 19);
            this.SaveResultButton.TabIndex = 19;
            this.SaveResultButton.Text = "結果を保存";
            this.SaveResultButton.UseVisualStyleBackColor = true;
            this.SaveResultButton.Click += new System.EventHandler(this.SaveResultButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "標準テキストファイル (*.txt) | *.txt";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.Title = "ファイルに検索結果を保存する...";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // SaveResult
            // 
            this.SaveResult.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveResult_DoWork);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "標準テキストファイル (*.txt) | *.txt";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // OpenResult
            // 
            this.OpenResult.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OpenResult_DoWork);
            this.OpenResult.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OpenResult_RunWorkerCompleted);
            // 
            // LoadSearchIndexButton
            // 
            this.LoadSearchIndexButton.Location = new System.Drawing.Point(229, 100);
            this.LoadSearchIndexButton.Name = "LoadSearchIndexButton";
            this.LoadSearchIndexButton.Size = new System.Drawing.Size(75, 19);
            this.LoadSearchIndexButton.TabIndex = 20;
            this.LoadSearchIndexButton.Text = "データ読込";
            this.LoadSearchIndexButton.UseVisualStyleBackColor = true;
            this.LoadSearchIndexButton.Click += new System.EventHandler(this.LoadSearchIndexButton_Click);
            // 
            // Password_Dif
            // 
            this.Password_Dif.Location = new System.Drawing.Point(310, 134);
            this.Password_Dif.Name = "Password_Dif";
            this.Password_Dif.Size = new System.Drawing.Size(86, 23);
            this.Password_Dif.TabIndex = 21;
            this.Password_Dif.Text = "パスワード比較";
            this.Password_Dif.UseVisualStyleBackColor = true;
            this.Password_Dif.Click += new System.EventHandler(this.Password_Dif_Click);
            // 
            // DifListThread
            // 
            this.DifListThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DifListThread_DoWork);
            this.DifListThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DifListThread_RunWorkerCompleted);
            // 
            // SamplingThread
            // 
            this.SamplingThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SamplingThread_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Password_Dif);
            this.Controls.Add(this.LoadSearchIndexButton);
            this.Controls.Add(this.SaveResultButton);
            this.Controls.Add(this.ListCount);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.LogClearButton);
            this.Controls.Add(this.LogLabel);
            this.Controls.Add(this.SearchLogTextBox);
            this.Controls.Add(this.SearchNotCheckBox);
            this.Controls.Add(this.TextLimiterCheckBox);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.SearchContinueCheckBox);
            this.Controls.Add(this.ErrorText);
            this.Controls.Add(this.SearchClearButton);
            this.Controls.Add(this.SearchingModeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordResultBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchTextBox);
            this.MinimumSize = new System.Drawing.Size(655, 320);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.ComponentModel.BackgroundWorker Initwork;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker MTSearch;
        private System.Windows.Forms.TextBox PasswordResultBox;
        private System.Windows.Forms.Timer Refreshing;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox SearchingModeBox;
        private System.Windows.Forms.Button SearchClearButton;
        private System.Windows.Forms.Label ErrorText;
        private System.Windows.Forms.CheckBox SearchContinueCheckBox;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.CheckBox TextLimiterCheckBox;
        private System.Windows.Forms.CheckBox SearchNotCheckBox;
        private System.Windows.Forms.TextBox SearchLogTextBox;
        private System.Windows.Forms.Label LogLabel;
        private System.Windows.Forms.Button LogClearButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label ListCount;
        private System.ComponentModel.BackgroundWorker ReloadThread;
        private System.Windows.Forms.Button SaveResultButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.ComponentModel.BackgroundWorker SaveResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker OpenResult;
        private System.Windows.Forms.Button LoadSearchIndexButton;
        private System.Windows.Forms.Button Password_Dif;
        private System.ComponentModel.BackgroundWorker DifListThread;
        private System.ComponentModel.BackgroundWorker SamplingThread;
    }
}

