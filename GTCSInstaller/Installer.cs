using System;
using System.Diagnostics;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Threading.Tasks;

namespace GTCSInstaller
{
    public partial class Installer : Form
    {
        bool finishCS = false;
        bool finishEF = false;
        bool Finish { get { return finishCS && finishEF; } }

        Timer timer = new Timer();
        int currMsgID = 0;
        string[] msgs = new string[] {
            "呢个系猪仔比猫猫既十七岁生日礼物",
            "猪仔一直想卑喵喵一个独一无二既礼物",
            "不过时间仓促……",
            "猪仔能力有限……",
            "做得粗糙（模型未做完……T^T）",
            "请喵喵见谅……",
            "不过猪仔会定时更新噶！",
            "林翻起个几月前开始呢个项目阵……",
            "系甘兴奋……(*^__^*)",
            "日日忙碌……好充实……",
            "宜家要“交货”了~",
            "为“喵”消得人憔悴",
            "一切都值得！",
            "希望可以陪你到永久",
            "我一直都好想一齐养D猫",
            "目前来讲只能够做到甘了……",
            "Sorry……喵……",
            "睇睇“佢”……",
            "希望可以陪你到永久"};

        public Installer()
        {
            InitializeComponent();

            CreateShortcut();
            ExtractFile();

            timer.Tick += new EventHandler(timer_Tick);
            UpdateWord();
        }

        private void ExtractFile()
        {
            Task task = new Task(() =>
            {
                Process exep = new Process();
                exep.StartInfo.FileName = "7za.exe";
                exep.StartInfo.Arguments = "x -y data.7z -o\""
                    + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\"";
                exep.StartInfo.CreateNoWindow = true;
                exep.StartInfo.UseShellExecute = false;
                exep.Start();
                exep.WaitForExit();
                finishEF = true;
            });
            task.Start();
        }

        private void CreateShortcut()
        {
            Task task = new Task(() =>
            {
                WshShell shell = new WshShell();
                string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/喵哦~.lnk";
                IWshShortcut shortcut = shell.CreateShortcut(shortcutPath) as IWshShortcut;
                shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                    + "\\GiftTocatSeventeen\\GiftTocatSeventeen.exe";
                shortcut.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                    + "\\GiftTocatSeventeen";
                shortcut.WindowStyle = 1;
                shortcut.Description = "喵喵既17岁生日礼物";
                shortcut.Save();
                finishCS = true;
            });
            task.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateWord();
        }

        private void UpdateWord()
        {
            timer.Enabled = false;
            if (currMsgID == msgs.Length)
            {
                if (Finish)
                {
                    timer.Enabled = false;
                    Close();
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/喵哦~.lnk");
                }
                return;
            }
            MyWord.Text = msgs[currMsgID];
            timer.Interval = msgs[currMsgID].Length * 250;
            ++currMsgID;
            InstallProgressBar.Value = (int)(((double)currMsgID / (double)msgs.Length) * 100);
            timer.Enabled = true;
        }
    }
}
