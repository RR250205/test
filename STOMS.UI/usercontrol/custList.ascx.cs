using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class custList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popCust();
            }
        }

        private void popCust()
        {
            tgrdCust.DataSource = (new CustomerDA()).GetCustomer(Convert.ToInt32(Session["OrgID"]));
            tgrdCust.DataBind();
        }

        protected void tgrdCust_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
    }
}