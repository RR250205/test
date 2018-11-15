using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using STOMS.Common;

namespace STOMS.UI.pages
{
    public partial class SampleTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if(Convert.ToBoolean(Session["TodayRequests"]) == true)
                //{
                //    SampReceive.popSampleList("Received", DateTime.Now);
                //    ltrSampReceiveCount.Text = SampReceive.SpecimenCount;

                //    SampResult.popSampleList("Ready for Assay", DateTime.Now);
                //    ltrReadyforAssay.Text = SampResult.SpecimenCount;

                //    SampAssay.popSampleList("Assigned to Assay", DateTime.Now);
                //    ltrSampAssayCount.Text = SampAssay.SpecimenCount;

                //    //SampResult.popSampleList("Result Recorded", DateTime.Now);
                //    //ltrSampResultCount.Text = SampResult.SpecimenCount;

                //    //SampRejected.popSampleList("Rejected", DateTime.Now);
                //    //ltrSampRejectedCount.Text = SampRejected.SpecimenCount;
                //    //Session.Remove("TodayRequests");
                //}
                //else
                //{
                //    SampReceive.popSampleList("Received");
                //    ltrSampReceiveCount.Text = SampReceive.SpecimenCount;

                //    SampAssay.popSampleList("Assigned to Assay");
                //    ltrSampAssayCount.Text = SampAssay.SpecimenCount;

                //    SampResult.popSampleList("Ready for Assay");
                //    ltrReadyforAssay.Text = SampResult.SpecimenCount;

                //    //SampRejected.popSampleList("Rejected");
                //    //ltrSampRejectedCount.Text = SampRejected.SpecimenCount;
                //}
                //Session["PgContentTitle"] = "Specimen Tracking";
            }
        }

        protected void btnAssay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/assay");
        }

        //protected void ddlspecimen_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlspecimen.SelectedValue == "SampReceive")
        //    {
        //        dvSampReceive.Visible = true;
        //        dvSampResult.Visible = false;
        //        dvSampAssay.Visible = false;
                
        //    }
        //    if (ddlspecimen.SelectedValue == "SampResult")
        //    {
        //        dvSampResult.Visible = true;
        //        dvSampAssay.Visible = false;
        //        dvSampReceive.Visible = false;
        //    }
        //    if (ddlspecimen.SelectedValue == "SampAssay")
        //    {
        //        dvSampAssay.Visible = true;
        //        dvSampReceive.Visible = false;
        //        dvSampResult.Visible = false;
        //    }
        //}
    }
}
