using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NotifyMessage
{
    static class Program
    {
        // グローバル変数
        public static string Message = "";
        public enum MSGLEVEL : int  // MessageLevel
        {
            INFO = 1,
            WARN,
            ERR,
        };
        public static MSGLEVEL msglevel = MSGLEVEL.INFO;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 分けて処理をして最初にフォームを表示しないようにする
            // Application.Run(new MainForm());

            string[] cmds = System.Environment.GetCommandLineArgs();
            for (int i = 1; i < cmds.Length; i++)
            {
                if (0 <= cmds[i].IndexOf("/ERR", StringComparison.OrdinalIgnoreCase)) msglevel = MSGLEVEL.ERR;
                else if (0 <= cmds[i].IndexOf("/WARN", StringComparison.OrdinalIgnoreCase)) msglevel = MSGLEVEL.WARN;
                else if (0 <= cmds[i].IndexOf("/INFO", StringComparison.OrdinalIgnoreCase)) msglevel = MSGLEVEL.INFO;
                else Message = cmds[i];
            }

            new MainForm();

            if (Message != "")
            {
                //Application.Run();
            }
        }
    }
}
