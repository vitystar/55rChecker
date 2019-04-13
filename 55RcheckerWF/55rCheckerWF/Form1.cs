using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _55rCheckerWF
{
    public partial class Form1 : Form
    {

        private CookieMsg Cookie;
        private readonly string cookieFileName = "cookie";
        private CheckMessage message = new CheckMessage() { All = "null", Level = "null", Used = "null", Username = "null", Surplus = "null" };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtExpire.Text) || string.IsNullOrWhiteSpace(txtKey.Text) || string.IsNullOrWhiteSpace(txtUid.Text))
            {
                MessageBox.Show("cookie信息不完整");
                return;
            }
            else
            {
                Cookie = new CookieMsg() { email = txtEmail.Text.Trim(), key = txtKey.Text.Trim(), uid = txtUid.Text.Trim(), expire_in = txtExpire.Text.Trim() };
                FileHelper.SaveObject(Cookie, cookieFileName);
                ShowBar();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
               
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (FileHelper.FileExits(cookieFileName))
            {
                Cookie = FileHelper.GetObject<CookieMsg>(cookieFileName);
                ShowBar();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string html = "failed";
            while (html == "failed")
                html = RequsetHelper.GetHtml(Cookie.email, Cookie.key, Cookie.uid, Cookie.expire_in);
            Refresh();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 300000;
        }

        private void 分钟ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 600000;
        }

        private void 分钟ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1800000;
        }

        private void 小时ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 3600000;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; //不退出程序
            this.WindowState = FormWindowState.Minimized; //最小化
            this.ShowInTaskbar = false; //在任务栏中不显示窗体
            this.notifyIcon1.Visible = true; //托盘图标可见
        }


        private void ShowBar() => Refresh(() =>
        {
            MessageBox.Show("cookie信息错误或失效");
            this.Show();
        });

        private void Refresh(Action errorHandle)
        {
            string html = "failed";
            while (html == "failed")
                html = RequsetHelper.GetHtml(Cookie.email, Cookie.key, Cookie.uid, Cookie.expire_in);
            if (!html.Contains("总流量"))
            {
                errorHandle();
                return;
            }
            message = TextHelper.GetMessage(html,out double percent);
            notifyIcon1.Icon = ImageHelper.MakeIcon(percent);
            notifyIcon1.Text = String.Format("用户名:{0}\r\n用户等级:{1}\r\n总流量:{2}\r\n已用流量:{3}\r\n剩余流量:{4}", message.Username, message.Level, message.All, message.Used, message.Surplus);
            this.Visible = false;
        }
    }
}
