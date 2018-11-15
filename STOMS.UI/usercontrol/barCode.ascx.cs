using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class barCode : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["bc"] != null)
                {
                    setvalue(Convert.ToString(Request["bc"]));
                }
            }
        }

        public void setvalue(string bcValue)
        {
            barcodeValue.Value = bcValue;
            Page.ClientScript.RegisterStartupScript(GetType(), "barKey", "generateBarcode()");
        }
    }
}