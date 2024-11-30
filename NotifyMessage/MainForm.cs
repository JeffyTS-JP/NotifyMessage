using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;    // Add



namespace NotifyMessage
{
    public partial class MainForm : Form
    {
        // This Program Name
        private const string PROGRAM_NAME = "NotifyMessage";
        // Messages
        private const string MSG_USAGE = "Notifiying a message in Action center.\r\n" +
                                         "\r\n" +
                                         " Usage: \r\n" +
                                         "  " + PROGRAM_NAME + ".exe [opt] \"Your message\"\r\n" +
                                         "\r\n" +
                                         " Option:\r\n" +
                                         "    /INFO(default) | /WARN | /ERR";
        private const string MSG_ABOUT = "Thanks to Microsoft Corporation.\r\n" +
                                         "\r\n" +
                                         "This program has been developed on\r\n" +
                                         "   Visual Studio Community 2015\r\n" +
                                         "   Windows10 Professional 64bit.\r\n" +
                                         "\r\n" +
                                         MSG_USAGE;

        public MainForm()
        {
            // Messageはここで出さないと終了処理がうまくいかない(プロセスが残る)
            if (Program.Message == "")
            {
                //MessageBox.Show(MSG_USAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AboutDialog();
                Program.Message = "";           // 消してしまう(flag代わり)
            }
            else if (Program.Message != "")     // vmxFilePathが指定されていないときは初期化せず終了
            {
                InitializeComponent();

                this.notifyIcon_MainForm.Text = PROGRAM_NAME;   // Program.NotifyMessage;
                this.notifyIcon_MainForm.BalloonTipText = Program.Message;
                this.notifyIcon_MainForm.Icon = Properties.Resources.NotifyMessageTray;

                switch (Program.msglevel)
                {
                    case Program.MSGLEVEL.INFO:
                        this.notifyIcon_MainForm.BalloonTipIcon = ToolTipIcon.Info;
                        break;
                    case Program.MSGLEVEL.WARN:
                        this.notifyIcon_MainForm.BalloonTipIcon = ToolTipIcon.Warning;
                        break;
                    case Program.MSGLEVEL.ERR:
                        this.notifyIcon_MainForm.BalloonTipIcon = ToolTipIcon.Error;
                        break;
                    default:
                        this.notifyIcon_MainForm.BalloonTipIcon = ToolTipIcon.None;
                        break;
                }

                this.notifyIcon_MainForm.ShowBalloonTip(5000);  // Balloon tip 表示

                System.Threading.Thread.Sleep(5000);
                
                // Exit
                this.notifyIcon_MainForm.Visible = false;   // タスクトレイからアイコンを取り除く
                Application.Exit();                         // アプリケーション終了
            }
        }

        private void AboutDialog()
        {
            string message;

            //AssemblyTitleの取得
            AssemblyTitleAttribute asmTitle = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute));
            //AssemblyDescriptionの取得
            AssemblyDescriptionAttribute asmDesc = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute));
            //AssemblyCompanyの取得
            AssemblyCompanyAttribute asmCompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute));
            //AssemblyProductの取得
            AssemblyProductAttribute asmPrdct = (AssemblyProductAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute));
            //AssemblyCopyrightの取得
            AssemblyCopyrightAttribute asmCpyrght = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute));
            //AssemblyTrademarkの取得
            AssemblyTrademarkAttribute asmTM = (AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(
                                                    Assembly.GetExecutingAssembly(), typeof(AssemblyTrademarkAttribute));
            //バージョンの取得
            Assembly asm = Assembly.GetExecutingAssembly();
            Version ver = asm.GetName().Version;

            message = asmTitle.Title + "\r\n" +
                      "\r\n" +
                      asmDesc.Description + "\r\n" +
                      "\r\n" +
                      MSG_ABOUT + "\r\n" +
                      "\r\n" +
                      asmCompany.Company + "\r\n" +
                      asmPrdct.Product + "  Rev. " + ver + "\r\n" +
                      asmCpyrght.Copyright + "\r\n";
            MessageBox.Show(message, PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
