using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.usercontrol
{
    public partial class GroupHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            popGroupInfo();
        }
        //public string OrderStatus()
        //{
        //    return ltrStatus.Text;
        //}
        private void popGroupInfo()
        {
            if (Session["GroupID"] != null)
            {
                dvHeader.Visible = true;
                List<GroupBO> obGroup = new List<GroupBO>();
                obGroup = (new OrderInvDA()).GetGroupDetail(Convert.ToInt32(Session["GroupID"].ToString()));
                if (obGroup.Count > 0)
                {
                    ltrGroupName.Text = obGroup[0].GroupName;
                    ltrCreateDate.Text = Convert.ToString(obGroup[0].CreateOnTime);
                    ltrSampleCount.Text = obGroup[0].sampleCount.ToString();
                    ltrGroupNo.Text = Convert.ToString(obGroup[0].GroupID);
                    ltrStatus.Text = obGroup[0].Status;

                    switch (obGroup[0].Status)
                    {
                        case "Sample Assessment":
                            btnStatus.Attributes.Add("class", "btn btn-primary");
                            break;
                        case "Ready to Testing":
                            btnStatus.Attributes.Add("class", "btn btn-primary");
                            break;
                        case "InTesting":
                            btnStatus.Attributes.Add("class", "btn btn-primary");
                            break;
                        case "Completed":
                            btnStatus.Attributes.Add("class", "btn btn-success");
                            break;
                        case "Cancel":
                            btnStatus.Attributes.Add("class", "btn btn-danger");
                            break;
                    }
                }
            }
        }
    }
}