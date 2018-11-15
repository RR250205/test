using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class Exceptions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Convert.ToBoolean(Session["TodayRequests"]) == true)
                {
                    SampRejected.popSampleList("Rejected", DateTime.Now);
                    ltrSampRejectedCount.Text = SampRejected.SpecimenCount;
                    Session.Remove("TodayRequests");
                }
                else
                {   
                    SampRejected.popSampleList("Rejected");
                    ltrSampRejectedCount.Text = SampRejected.SpecimenCount;
                }
                Session["PgContentTitle"] = "Exceptions";
            }
        }
    }
}