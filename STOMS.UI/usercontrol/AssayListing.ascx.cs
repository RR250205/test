using STOMS.DA;
using STOMS.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class AssayListing : System.Web.UI.UserControl
    {
        public string AssayStatus
        {
            get { return hAssayStatus.Value; }
            set { hAssayStatus.Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                AssayPop();
            }
        }

        public void AssayPop()
        {
            dvNoRec.Visible = false;
            rpAssayList.DataSource = (new SpecimenDA()).GetAssayGroupList(Convert.ToInt32(Session["orgID"]), hAssayStatus.Value, 0, true);
            rpAssayList.DataBind();
            if (rpAssayList.Items.Count == 0)
            {
                dvNoRec.Visible = true;
            }
        }

        protected void rpAssayList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType ==  ListItemType.Item ||  e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltr = (Literal)e.Item.FindControl("ltrStatus");
                ltr.Text = Common.Constant.GetAssayStatusFormat(((AssayGroupBO)e.Item.DataItem).AssayStatus);

                Literal ltrStartDate = (Literal)e.Item.FindControl("ltrStartDate");
                if (ltrStartDate != null)
                    if (ltrStartDate.Text == null || ltrStartDate.Text == "")
                        ltrStartDate.Text = "-";
                Literal ltrCompleteDate = (Literal)e.Item.FindControl("ltrCompleteDate");
                if (ltrCompleteDate != null)
                    if (ltrCompleteDate.Text == null || ltrCompleteDate.Text == "")
                        ltrCompleteDate.Text = "-";

            }
        }

        protected void rpAssayList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnk")
            {
               // Session["AssayID"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/pages/assaydetail?ai="+ Convert.ToString(e.CommandArgument));
            }
        }
    }
}