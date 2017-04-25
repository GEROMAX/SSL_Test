using MyCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSL_Test
{
    public partial class URLSettingForm : ValueListSettingFormBase
    {
        private List<ValueListSettings> URLs { get; set; }

        public ValueListSettings SelectedURLSetting
        {
            get
            {
                return this.URLs.Find(setting => setting.IsSelected == true);
            }
        }


        public URLSettingForm()
        {
            InitializeComponent();
        }

        protected override void LoadSettings()
        {
            this.URLs = this.LoadSettingFromFile("URL設定");
        }

        public DialogResult SettingURL()
        {
            this.Text = "URL設定";
            this.ActiveSettings = this.URLs;
            return this.StartSetting();
        }
    }
}
