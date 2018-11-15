using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class AssayMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CurrentAssayInfo.PopulateCurrentAssay();

                Session["PgContentTitle"] = "Assay Tracking";
            }
        }

        protected void btnAssay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/assay");
        }
    }
}