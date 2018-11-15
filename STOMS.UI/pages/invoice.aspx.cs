using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popInvoice();
            }
        }

        private void popInvoice()
        {
            tgrdInvoice.DataSource = (new OrderInvDA()).GetInvoice();
            tgrdInvoice.DataBind();
        }

        protected void tgrdInvoice_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
    }
}