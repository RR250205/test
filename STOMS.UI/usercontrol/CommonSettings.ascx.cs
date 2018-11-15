using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class CommonSettings : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void chkSpecAutoGenerate_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurationBO configurationBO = new ConfigurationBO()
            {
                ConfigID = 35,
                ConfigType = "AutoGenerateSpecimenNumbers",
                ConfigName = "AutoGenerateSpecimenNumbers",
                ConfigValue = chkSpecAutoGenerate.Checked.ToString(),
                TenantID = Convert.ToInt32(Session["OrgID"]),
                PrefixYear = DateTime.Now.Year,
                SrNumber = 1
            };

            int AffetedValue = new Config().SaveConfig(configurationBO);
        }
    }
}