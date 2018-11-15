using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.pages
{
    public partial class Insuranceagent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ReportDA objRpt = new ReportDA();
                List<DashboardStatBO> objStat = objRpt.getDashboardStat(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["ActMonth"]), Convert.ToInt32(Session["ActYear"]));
                if (objStat.Count > 0)
                {
                    ltrOrderTotal.Text = Convert.ToString(objStat[0].TotalOrders);
                    //ltrTestCount.Text = Convert.ToString(objStat[0].SampleTesting);
                    ltrCliCount.Text = Convert.ToString(objStat[0].ClientCount);
                    //ltrInvCount.Text = Convert.ToString(objStat[0].OutStandingInv);
                }
            }
        }

        protected void lbtnAction_Click(object sender, EventArgs e)
        {
            switch (hAct.Value)
            {
                case "TO":
                    Session["SearchText"] = "";
                    Session["menuID"] = "2";
                    Response.Redirect("~/pages/order");
                    break;
                case "TT":
                    Session["menuID"] = "9";
                    Response.Redirect("~/pages/st");
                    break;

                case "TC":
                    Session.Remove("menuID");
                    Response.Redirect("~/admin/cust");
                    break;

                    //case "TC":
                    //    Session.Remove("menuID");
                    //    Response.Redirect("~/admin/cust");
                    //    break;
                    //case "OI":
                    //    Session["menuID"] = "3";
                    //    Response.Redirect("~/pages/invoice");
                    //break;
            }
        }
    }
}