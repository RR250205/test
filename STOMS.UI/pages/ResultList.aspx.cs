using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class ResultList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Convert.ToBoolean(Session["TodayRequests"]) == true)
                {
                    SampResultRecorded.popSampleList("Result Recorded", DateTime.Now);
                    ltrSampRejectedCount.Text = SampResultRecorded.SpecimenCount;
                    Session.Remove("TodayRequests");
                }
                else
                {
                    SampResultRecorded.popSampleList("Result Recorded");
                    ltrSampRejectedCount.Text = SampResultRecorded.SpecimenCount;
                }
                Session["PgContentTitle"] = "Results";
            }
        }
    }
}