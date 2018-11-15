using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace STOMS.UI.usercontrol
{
    public partial class Report : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //string abPath = HttpContext.Current.Request.Url.AbsolutePath;
                //if (abPath.IndexOf("pages/order") > 0)
                //{
                //    btnTopCLients.Visible = true;
                //}
                populateReport("");
                //Session.Remove("DBVal");
            }
        }

        private void populateReport(string Status)
        {
            OrderInvDA objOrder = new OrderInvDA();

            lblStatusValue.Text = Status;
            if (Status == "")
                lblStatusValue.Text = "Delivered";

            if (Session["SearchText"] != null)
            {
                tgrdReport.DataSource = objOrder.getOrderSearchResult(Convert.ToString(Session["SearchText"]));
                Session.Remove("SearchText");
            }
            else
                tgrdReport.DataSource = objOrder.getOrderList(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["ActMonth"]), Convert.ToInt32(Session["ActYear"]), lblStatusValue.Text);
            tgrdReport.DataBind();

        }

        protected void tgrdReport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HtmlGenericControl sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderDate");
                DateTime dtOrderDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "OrderDate"));
                sp1.InnerText = dtOrderDate.ToString("dd MMM, yyyy");

                sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderStatus");
                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderStatus")) == "Active")
                {
                    sp1.Attributes.Add("class", "label label-success");
                    sp1.InnerText = "Active";
                }
                else
                {
                    sp1.Attributes.Add("class", "label label-warning");
                    sp1.InnerText = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderStatus"));
                }


            }
        }

        //protected void btnTopCLients_Click(object sender, EventArgs e)
        //{
        //    Session.Remove("OrderID");
        //    Response.Redirect("~/pages/ClientReports");
        //}

        protected void hOrderID_ValueChanged(object sender, EventArgs e)
        {
            Session["OrderID"] = hOrderID.Value;
            switch (hAct.Value)
            {
               case "I":
                    Response.Redirect("~/pages/invdet");
                    break;
                case "O":
                    populateReport(hOrderStatus.Value);
                    break;
            }
        }

        protected void hOrderStatus_ValueChanged(object sender, EventArgs e)
        {
            populateReport(hOrderStatus.Value);
            
        }       
    }
}