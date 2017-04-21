using MyCommon;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace SSL_Test
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 設定画面
        /// </summary>
        private URLSettingForm urlSettings { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.urlSettings = new URLSettingForm();
        }

        private void btnSelectURL_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK.Equals(this.urlSettings.SettingURL()))
            {
                this.txtURL.Text = this.urlSettings.SelectedURLSetting.Name;
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtURL.Text))
            {
                return;
            }

            var client = new UnivarsalWebClient();
            try
            {
                switch (this.urlSettings.SelectedURLSetting.Name)
                {
                    case "GitHub":
                        new GithubLoginHelper(client).Login(new LoginParam());
                        break;

                    //まだ動かない
                    //case "ヤフーファイナンス":
                    //    var paramY = new YahooFinanceLoginParam();
                    //    paramY.StartUrl = this.urlSettings.SelectedURLSetting.Values[1];
                    //    paramY.LoginUrl = this.urlSettings.SelectedURLSetting.Values[2];
                    //    paramY.UserName = this.urlSettings.SelectedURLSetting.Values[3];
                    //    paramY.Password = this.urlSettings.SelectedURLSetting.Values[4];
                    //    new YahooFinanceLoginHelper(client).Login(paramY);
                    //    break;

                    case "バーボンハウス":
                        var paramB = new LoginParam();
                        paramB.UserName = this.urlSettings.SelectedURLSetting.Values[1];
                        paramB.Password = this.urlSettings.SelectedURLSetting.Values[2];
                        new BourbonHouseLoginHelper(client).Login(paramB);
                        break;

                    default:
                        return;
                }

                var responseData = client.DownloadString(this.urlSettings.SelectedURLSetting.Values.First());
                this.txtResult.Text = responseData;
            }
            catch (Exception except)
            {
                this.txtResult.Text = except.Message;
            }
        }
    }
}
