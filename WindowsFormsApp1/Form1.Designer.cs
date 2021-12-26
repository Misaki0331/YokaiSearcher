
namespace WindowsFormsApp1
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
            this.label2 = new System.Windows.Forms.Label();
            this.SearchingModeBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(326, 42);
            this.SearchTextBox.MaxLength = 14;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 19);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(432, 41);
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
            this.label1.Location = new System.Drawing.Point(247, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "即席で作ったのでバグが発生するかもです。\r\nその際はタスクマネージャーから消してやってください。\r\n";
            // 
            // SearchingModeBox
            // 
            this.SearchingModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchingModeBox.FormattingEnabled = true;
            this.SearchingModeBox.Items.AddRange(new object[] {
            "単語検索",
            "先頭検索",
            "終端検索"});
            this.SearchingModeBox.Location = new System.Drawing.Point(230, 41);
            this.SearchingModeBox.Name = "SearchingModeBox";
            this.SearchingModeBox.Size = new System.Drawing.Size(90, 20);
            this.SearchingModeBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SearchingModeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordResultBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox SearchingModeBox;
    }
}

