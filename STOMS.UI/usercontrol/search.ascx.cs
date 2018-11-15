using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string abPath = HttpContext.Current.Request.Url.AbsolutePath;
            if (abPath.IndexOf("pages/order") > 0)
            {
                ltrSearchCap.Text = "Search Order";
                hSearchMode.Value = "Order";
            }
        }

        protected void lbtn1_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() != "")
            {
                string abPath = HttpContext.Current.Request.Url.AbsolutePath;
                Session["SearchText"] = txtInput.Text.Trim();
                if (abPath.IndexOf("pages/order") > 0)
                    Response.Redirect("~/pages/order");
                else if (abPath.IndexOf("pages/dashboard") > 0)
                    Response.Redirect("~/pages/searchresult");
            }
        }
    }
}