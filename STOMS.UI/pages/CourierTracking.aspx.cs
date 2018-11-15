using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.FEDEX.CLIENT.TrackServiceWebReference;
using STOMS.FEDEX.CLIENT;

namespace STOMS.UI.pages
{
    public partial class CourierTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void btnTrack_Click(object sender, EventArgs e)
        {
            TrackServiceClient client = new TrackServiceClient(txtTrackingNumber.Text);
            TrackRequest request = client.Request();
            TrackReply reply = client.Replay(request);
            if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
            {
               // rptTrackingDetails.DataSource = reply.CompletedTrackDetails[1].TrackDetails;
               // rptTrackingDetails.DataBind();
            }
            ltrTrackDetails.Text = client.ShowTrackReply(reply);
        }

        protected void rptTrackingDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}