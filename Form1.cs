using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client.Events;
using RabbitMQManager;

namespace FakePhoneApp
{

    public partial class Form1 : Form
    {

        SingleManager sm;

        public Form1()
        {
            sm = new SingleManager(UpdateTextBox, "44107");
            
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }


        private void Form1_Load(object sender, EventArgs e) {
            queueName.Text = sm.queueName;
        }
        public void UpdateTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBox), new object[] { value });
                return;
            }
            richTextBox1.Text = value;
        }

        private void login_Click(object sender, EventArgs e)
        {
            sm.login();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        protected override void Dispose(bool disposing)
        {
            sm.Close();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void logout_Click(object sender, EventArgs e)
        {
            sm.logout();
        }

        private void history_Click(object sender, EventArgs e)
        {
            sm.history();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            sm.history();
        }

        private void keepAlive_Click(object sender, EventArgs e)
        {
            sm.keepAlive();
        }

        private void loginSveta_Click(object sender, EventArgs e)
        {
            sm.loginSveta();
        }

        private void test1_Click(object sender, EventArgs e)
        {
            ManagersPerformance mp = new ManagersPerformance();
            mp.BasicTest();
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            string managerEmployeeId = txtManagerEmployeeId.Text;
            string messageNotification = txtMessageNotification.Text;
            sm.TestNotification(managerEmployeeId, messageNotification);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLoginUser_Click(object sender, EventArgs e)
        {
            sm.loginUser();
        }

        private void btnRefreshSveta_Click(object sender, EventArgs e)
        {
            sm.refreshSveta();
        }

        private void btnLoginWrong_Click(object sender, EventArgs e)
        {
            sm.loginUserWrong();
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            sm.sendLog(txtFileName.Text, txtFileContent.Text);
        }

        private void btnReceived_Click(object sender, EventArgs e)
        {
            sm.received(txtAgentLogID.Text);
        }
    }
}
