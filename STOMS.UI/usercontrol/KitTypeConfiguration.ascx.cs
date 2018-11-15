using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;
using STOMS.Common;

namespace STOMS.UI.usercontrol
{
    public partial class KitTypeConfiguration : System.Web.UI.UserControl
    {
        private string _activeParentTab = "";

        public string ActiveParentTab
        {
            get { return _activeParentTab; }
        }
        protected void Page_Load(object sender, EventArgs e)  
        {
            if (!IsPostBack)
            {
                rptkitTypeConfiguration.DataSource = new KitTypeConfigurationDA().GetKitType();
                rptkitTypeConfiguration.DataBind();
            }
        }

        protected void rptkitTypeConfiguration_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KitTypeConfigurationBO kitBO = new KitTypeConfigurationDA().DisplayKitConfiguration(Convert.ToInt32(Session["OrgID"]),((KitTypeConfigurationBO)e.Item.DataItem).KitID);

                if (((KitTypeConfigurationBO)e.Item.DataItem).KitID == kitBO.KitID)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkKitType");
                    chk.Checked = true;

                    HiddenField hfKitID = (HiddenField)e.Item.FindControl("hKitID");
                    hfKitID.Value = Convert.ToString(kitBO.KitID);

                    HiddenField hfKitTenantID = (HiddenField)e.Item.FindControl("hKitTenantID");
                    hfKitTenantID.Value = Convert.ToString(kitBO.KitTenantID);
                }                
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _activeParentTab = "KitType";
                foreach (RepeaterItem item in rptkitTypeConfiguration.Items)
                {
                    CheckBox rptChkbox = (CheckBox)item.FindControl("chkKitType");
                    HiddenField hfKitID = (HiddenField)item.FindControl("hKitID");
                    HiddenField hfKitTenantID = (HiddenField)item.FindControl("hKitTenantID");                    
                    
                    if (rptChkbox != null && rptChkbox.Checked)
                    {
                         KitTypeConfigurationBO kitupdateBO = new KitTypeConfigurationDA().UpdateKitConfiguration(Convert.ToInt32(Session["OrgID"]),Convert.ToInt32(hfKitID.Value),Convert.ToInt32(hfKitTenantID.Value));                     
                        if (kitupdateBO.KitTenantID > 0)
                        {
                            hfKitTenantID.Value = Convert.ToString(kitupdateBO.KitTenantID);                           
                        }
                    }

                    else
                    {
                        KitTypeConfigurationBO kitdeleteBO = new KitTypeConfigurationDA().deleteKitConfiguration(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hfKitID.Value));
                        hfKitTenantID.Value = "0";
                    }
                }
            }

            catch(Exception ex)
            {
                
            }
        }
    }
}