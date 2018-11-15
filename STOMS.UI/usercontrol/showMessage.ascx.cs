using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class showMessage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void setErrorMsg(string msg, string ind = "D")
        {
            lblMessage.Text = msg;
            if (msg != "")
            {
                dvErrorDetail.Visible = true;
                dvErrorDetail.Attributes.Remove("class");
                switch (ind)
                {
                    case "D":
                        dvErrorDetail.Attributes.Add("class", "alert alert-block alert-danger");
                        break;
                    case "W":
                        dvErrorDetail.Attributes.Add("class", "alert alert-block alert-warning");
                        break;
                    case "S":
                        dvErrorDetail.Attributes.Add("class", "alert alert-block alert-success");
                        break;
                    case "I":
                        dvErrorDetail.Attributes.Add("class", "alert alert-block alert-info");
                        break;
                }
            }
            else
            {
                dvErrorDetail.Visible = false;
            }
        }
    }
}