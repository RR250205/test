using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class AuditLog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopHistory(int OrderID)
        {
            tgrdAudit.DataSource = (new STOMS.DA.AdminDA()).GetAuditLog("Order", OrderID);
            tgrdAudit.DataBind();
        }
    }
}