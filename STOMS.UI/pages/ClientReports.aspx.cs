using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using STOMS.DA;
using STOMS.BO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace STOMS.UI.pages
{
    public partial class ClientReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string abPath = HttpContext.Current.Request.Url.AbsolutePath;
            if (abPath.IndexOf("pages/order") > 0)
            {
               // btnTopCLients.Visible = true;
            }
            populateReport("");
        }



        private void populateReport(string Status)
        {
            OrderInvDA objOrder = new OrderInvDA();

            lblStatusValue.Text = Status;
            if (Status == "")
                lblStatusValue.Text = "Most Orders";

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

        }

        protected void hOrderID_ValueChanged(object sender, EventArgs e)
        {
            Session["OrderID"] = hOrderID.Value;
            switch (hAct.Value)
            {
                case "T":
                    Session["TTrackID"] = ((new OrderInvDA()).getSampleTestList(hOrderID.Value))[0].TTrackID;
                    Response.Redirect("~/pages/tt");
                    break;
                case "E":
                    if (hOrderStatus.Value == "Result Track" || hOrderStatus.Value == "Result Sent")
                        Response.Redirect("~/pages/OrderDetail");
                    else
                        Response.Redirect("~/pages/mgmtOrder");
                    break;
                case "I":
                    Response.Redirect("~/pages/invdet");
                    break;
                case "O":
                    populateReport(hOrderStatus.Value);
                    break;
            }
        }
    }
}