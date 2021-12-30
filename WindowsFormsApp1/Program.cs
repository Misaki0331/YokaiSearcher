using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YokaiSearcher
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ThreadExceptionイベント・ハンドラを登録する
            Application.ThreadException += new
              ThreadExceptionEventHandler(Application_ThreadException);

            // UnhandledExceptionイベント・ハンドラを登録する
            Thread.GetDomain().UnhandledException += new
              UnhandledExceptionEventHandler(Application_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowErrorMessage(e.Exception, "スレッドによる例外エラー");
        }

        // 未処理例外をキャッチするイベント・ハンドラ
        // （主にコンソール・アプリケーション用）
        public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                ShowErrorMessage(ex, "ハンドルされていない例外エラー");
            }
        }

        // ユーザー・フレンドリなダイアログを表示するメソッド
        public static void ShowErrorMessage(Exception ex, string extraMessage)
        {
            GC.Collect();
            if (ex.GetType().ToString() == "System.OutOfMemoryException"){

                MessageBox.Show( "申し訳ありません。十分に処理するメモリが足りない為終了する必要があります。\nメモリを4GB以上搭載してもこのエラーが発生する場合は不具合ではございません。\n\n開発者Twitter : @0x7FF Discord : Misaki#0331\n\n" +
              "【エラー内容】\n" + ex.Message + "\n\n" +
              "【スタックトレース】\n" + ex.StackTrace, "YokaiSearcherで致命的なエラーが発生しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(extraMessage + $"\nException : {ex.GetType()}\n\n" +
              "致命的な問題が発生した為終了する必要があります。\nお手数ですが開発者TwitterもしくはDiscordにてエラー内容をお知らせください。\n開発者Twitter : @0x7FF Discord : Misaki#0331\n\n" +
              "【エラー内容】\n" + ex.Message + "\n\n" +
              "【スタックトレース】\n" + ex.StackTrace, "YokaiSearcherで致命的なエラーが発生しました。",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            Environment.Exit(-1);
        }
    }
}
