using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.Web.UI.HtmlControls;

namespace STOMS.UI.pages
{
    public partial class TestTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["TTrackID"] != null)
                {
                    ddSampleNumber.DataSource = (new OrderInvDA()).getSampleTestListByTT(Convert.ToString(Session["TTrackID"]));
                    ddSampleNumber.DataBind();
                    ddSampleNumber.SelectedValue = Convert.ToString(Session["TTrackID"]);
                    PopSampleRecord(Convert.ToString(Session["TTrackID"]));
                    Session.Remove("TTrackID");
                }
                else
                {
                    popGrid();
                    phList.Visible = true;
                    phTrack.Visible = false;
                }
            }
        }

        private void PopSampleRecord(string TTrackID)
        {
            List<TestResultBO> oTT = new List<TestResultBO>();
            oTT = (new OrderInvDA()).getSampleTestListByTT(TTrackID, false);
            if (oTT.Count > 0)
            {
                ltrPatName.Text = oTT[0].oPatient.PatientName;
                ltrDenAge.Text = oTT[0].oPatient.Gender + " / " + oTT[0].PatientAge.ToString();
                txtDrawnDate.Text = oTT[0].DateDrawn;
                txtArrDate.Text = oTT[0].DateReceived;
                //ddSampleStatus.SelectedValue = oTT[0].SampleStatus;

                HtmlControl ifrm1 = (HtmlControl)this.FindControl("ifrmBC");
                if (ifrm1 != null)
                {
                    ifrm1.Attributes.Remove("src");
                    ifrm1.Attributes.Add("src", "/ext/showBC.aspx?bc=" + oTT[0].SampleBarCode);
                }
            }
        }

        private void popGrid()
        {
            tgrdTT.DataSource = (new OrderInvDA()).getSampleTestList();
            tgrdTT.DataBind();
        }

        protected void ddSampleNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopSampleRecord(ddSampleNumber.SelectedValue);
        }

        protected void btnSaveResult_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateSample_Click(object sender, EventArgs e)
        {

        }

        protected void tgrdTT_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void hTTackerID_ValueChanged(object sender, EventArgs e)
        {
            Session["TTrackID"] = hTTackerID.Value;
            Response.Redirect("~/pages/tt"); 
        }
    }
}