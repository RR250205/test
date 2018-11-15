using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;

namespace STOMS.UI.usercontrol
{
    public partial class ActiveClientConfiguration : System.Web.UI.UserControl
    {
        private string _activeParentTab = "";

        public string ActiveParentTab
        {
            get { return _activeParentTab; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getSelectedValue();
            }            
        }

        private void getSelectedValue()
        {
            ConfigurationBO activeBO = new ActiveClientConfigurationDA().GetConfiguration(Convert.ToInt32(Session["OrgID"]), "ActiveClients");
            rbtlActiveClient.SelectedValue = activeBO.ConfigValue;
        }

        protected void btnActiveClient_Click(object sender, EventArgs e)
        {
            _activeParentTab = "ActiveClient";
            if (rbtlActiveClient.SelectedItem == null)
            {
                ltrSuccessInformation.Text = "";
                ltrErrorInformation.Text = "Invalid Input";
            }
            else if(rbtlActiveClient.SelectedItem.Text.Trim() == "All Clients" || rbtlActiveClient.SelectedItem.Text.Trim() == "Current Year")
            {
                ConfigurationBO activeBO = new ActiveClientConfigurationDA().GetConfiguration(Convert.ToInt32(Session["OrgID"]), "ActiveClients");

                if (activeBO.ConfigName != "ActiveClients")
                {
                    ConfigurationBO saveBO = new ActiveClientConfigurationDA().SaveActiveClient(Convert.ToInt32(Session["OrgID"]), rbtlActiveClient.SelectedItem.Text.Trim());
                    ltrErrorInformation.Text = "";
                    ltrSuccessInformation.Text = "Data Configured Successfully";
                }
                else if(activeBO.ConfigName =="ActiveClients" && activeBO.ConfigType == "ActiveUsers")
                {
                    ConfigurationBO updateBO = new ActiveClientConfigurationDA().UpdateActiveClient(Convert.ToInt32(Session["OrgID"]), rbtlActiveClient.SelectedItem.Text.Trim());
                    ltrErrorInformation.Text = "";
                    ltrSuccessInformation.Text = "Configuration Updated Successfully";
                }
            }
        }
    }
}