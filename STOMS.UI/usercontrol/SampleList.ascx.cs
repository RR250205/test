using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;


namespace STOMS.UI.usercontrol
{
    public partial class SampleList : System.Web.UI.UserControl
    {

        private bool _isLoad;
        public string SetStatus
        {
            get { return hStatus.Value; }
            set { hStatus.Value = value; }
        }


        public bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }
        public string AssayID
        {
            get { return hAssayID.Value; }
            set { hAssayID.Value = value; }
        }
        public string SpecimenCount
        {
            get { return hSpecimenCount.Value; }
            set { hSpecimenCount.Value = value; }
        }

        public bool doRefresh { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (_isLoad)
                    popSampleList(hStatus.Value);
                popCust();
            }
        }
        private void BindData()
        {
            gvSampleList.DataBind();

        }

        public void popSampleList(string SampleStatus)
        {
            dvNoRec.Visible = false;
            List<SpecimenInfoBO> oSpec = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(Session["OrgID"]), SampleStatus);
            gvSampleList.DataSource = oSpec.OrderByDescending(x => x.SpecimenNumber);
            gvSampleList.DataBind();
            hSpecimenCount.Value = Convert.ToString(gvSampleList.Rows.Count);
            if (gvSampleList.Rows.Count == 0)
                dvNoRec.Visible = true;
        }

        public void popSampleList(string SampleStatus, DateTime Date)
        {
            dvNoRec.Visible = false;
            List<SpecimenInfoBO> oSpec = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(Session["OrgID"]), SampleStatus, Date);
            gvSampleList.DataSource = oSpec.OrderByDescending(x => x.SpecimenNumber);
            gvSampleList.DataBind();
            hSpecimenCount.Value = Convert.ToString(gvSampleList.Rows.Count);
            if (gvSampleList.Rows.Count == 0)
                dvNoRec.Visible = true;
        }

        public void popAssaySamples()
        {
            if (hAssayID.Value != "")
            {
                dvNoRec.Visible = false;
                gvSampleList.DataSource = (new SpecimenDA()).GetAssaySamples(Convert.ToInt32(hAssayID.Value));
                gvSampleList.DataBind();
                hSpecimenCount.Value = Convert.ToString(gvSampleList.Rows.Count);
                if (gvSampleList.Rows.Count == 0)
                    dvNoRec.Visible = true;
            }
        }

        //protected void rpSampleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        SpecimenInfoBO oSamp = (SpecimenInfoBO)e.Item.DataItem;
        //        Literal ltr = (Literal)e.Item.FindControl("ltrPatName");              

        //        if (oSamp.oPatient.Gender == "F")
        //            ltr.Text = "<i class=\"icon-female pink\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" + " "+oSamp.CreatedByName + "<br /> Created On :" +" "+ oSamp.CreatedOn;
        //        else
        //            ltr.Text = "<i class=\"icon-male blue\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" +" "+ oSamp.CreatedByName + "<br /> Created On :" +" "+ oSamp.CreatedOn;
        //        ltr = (Literal)e.Item.FindControl("ltrStatus");                          

        //        Literal ltrAssay = (Literal)e.Item.FindControl("ltrAssayStatus");                

        //        if (ltr != null)
        //            ltr.Text = Common.Constant.GetStatusFormat(oSamp.SpecimenStatus);

        //        if (oSamp.SpecimenStatus == "Assigned to Assay")
        //        {
        //            //ltrAssay.Text = "TestStatus";
        //            ltrAssay.Text = "<br><b style='font-size: 12px;'> Assay Status: </b>";
        //            ltrAssay.Text+= Common.Constant.GetAssayStatusFormat(oSamp.TestStatus);
        //        }
        //        if (oSamp.SpecimenStatus == "Pending" || oSamp.SpecimenStatus == "Rejected")
        //        {
        //            LinkButton lnk= (LinkButton)e.Item.FindControl("lbtnFurtherAction");
        //            lnk.CommandArgument = oSamp.SpecimenID.ToString();
        //            lnk.Visible = true;
        //            HiddenField hiddenField = (HiddenField)e.Item.FindControl("hReasons");
        //            HiddenField hspID = (HiddenField)e.Item.FindControl("hSpID");
        //            hspID.Value = oSamp.SpecimenID.ToString();
        //            if (oSamp.SpecimenStatus == "Rejected")
        //            {
        //                string[] reasons = oSamp.RejectReasons.Split(',');
        //                hiddenField.Value = "<ul>";
        //                foreach (var reason in reasons)
        //                {
        //                    if (reason != "")
        //                    {
        //                        if (reason.Contains("Other"))
        //                        {
        //                            string rsn= reason.Remove(0, 8);
        //                            hiddenField.Value += "<li>" + rsn + "</li>";
        //                        }
        //                        else
        //                            hiddenField.Value += "<li>" + reason + "</li>";
        //                    }
        //                }
        //                hiddenField.Value += "</ul>";
        //            }
        //            //else if(oSamp.SpecimenStatus=="Pending")
        //            //{
        //            //    hiddenField.Value = oSamp.IsConsent + "," + oSamp.IsReqComplete + "," + oSamp.PendingReasons;
        //            //}
        //        }
        //    }
        // }

        protected void rpSampleList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Session["SpecimenID"] = Convert.ToString(e.CommandArgument);
            //if (e.CommandName != "Action")
            //    Response.Redirect("~/pages/Specimen?si=" + Convert.ToString(e.CommandArgument));           
        }

        protected void lnkFurtherAction_Click(object sender, EventArgs e)
        {
            // modalSpConfirm.Attributes.Add("class", "modal fade in");
            //int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
            //{
            //    SpecimenID = sid,
            //    TenantID = Convert.ToInt32(tid),
            //    IsConsent = chkConsentProvided.Checked,
            //    IsReqComplete = chkRequisition.Checked,
            //    IsSpecimenAccept = chkAcceptTest.Checked,
            //    PendingReasons = txtPendingOthers.Text.Trim(),
            //    ReactivateReason = txtPendingtoRec.Text.Trim(),
            //    SpecimenStatus = status,
            //    RejectReasons = sReasons,
            //});
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (txtReasonForReactivate.Text != "")
            {
                int x = Convert.ToInt32(hSelectedSpecimenID.Value);
                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    SpecimenID = Convert.ToInt32(hSelectedSpecimenID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = true,
                    //IsReqComplete =true,
                    IsRejection = false,
                    IsSpecimenAccept = true,
                    PendingReasons = "",
                    ReactivateReason = txtReasonForReactivate.Text.Trim(),
                    SpecimenStatus = "Received",

                    RejectReasons = "",
                });

            }
            else
            {
                int x = Convert.ToInt32(hSelectedSpecimenID.Value);
                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    SpecimenID = Convert.ToInt32(hSelectedSpecimenID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = chkConsent.Checked,
                    //IsReqComplete = chkReq.Checked,
                    IsRejection = chkReq.Checked,
                    IsSpecimenAccept = true,
                    PendingReasons = "",
                    ReactivateReason = txtReasonForReactivate.Text.Trim(),
                    SpecimenStatus = "Received",
                    RejectReasons = "",
                });
            }
            //popSampleList(hStatus.Value);
            doRefresh = true;
            chkReq.Checked = false;
            chkConsent.Checked = false;
            chkNoOther.Checked = false;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }


        protected void gvSampleList_DataBound(object sender, EventArgs e)
        {


        }
        protected void gvSampleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName != "Action")
            //    Response.Redirect("~/pages/Specimen?si=" + Convert.ToString(e.CommandArgument));
        }
        private void popCust()
        {
            DataTable dt = new DataTable();            
            gvSampleList.DataSource = dt;
           // ViewState["dirState"] = dt;
            //ViewState["sortdr"] = "Asc";
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("DOB");
            dt.Columns.Add("Facility");
            dt.Columns.Add("Specialization");
           // dt.Columns.Add("SpecimenStatus");
            //dt.Columns.Add("SpecimenNumber");
            dt.Rows.Add();           
            gvSampleList.DataSource = (new SpecimenDA()).GetPatientSpec(Convert.ToInt32(Session["OrgID"]));
            gvSampleList.DataBind();
        }
       

        protected void gvSampleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSampleList.PageIndex = e.NewPageIndex;
            this.popCust();
        }

        protected void gvSampleList_Sorting(object sender, GridViewSortEventArgs e)
        {
        //    string sortingDirection = string.Empty;
            
        //if (dir == SortDirection.Ascending)
              
        //{
        //        dir = SortDirection.Descending;               
        //    sortingDirection = "Desc";       
        //}           
        //else
        //{              
        //    dir = SortDirection.Ascending;               
        //    sortingDirection = "Asc";               
        //}
        //    DataView sortedView = new DataView();
        //    sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //    gvSampleList.DataSource = sortedView;
        //    gvSampleList.DataBind();
        }
       // public SortDirection dir
         //{
         //get
         //{
         //   if (ViewState["dirState"] == null)
         //   {
         //     ViewState["dirState"] = SortDirection.Ascending;
         //   }
         //    return (SortDirection) ViewState["dirState"];
         //}
         //   set
         //   {
         //        ViewState["dirState"] = value;
         //   }
        // }


    }
}         

      