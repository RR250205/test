using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class PendingSampleList : System.Web.UI.UserControl
    {
        private bool _isLoad;
        public string SetStatus
        {
            get { return hPenStatus.Value; }
            set { hPenStatus.Value = value; }
        }

        public bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }
       
        public string SpecimenCount
        {
            get { return hPenSpecimenCount.Value; }
            set { hPenSpecimenCount.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (_isLoad)
                    popSampleList("Pending");
            }
        }


        public void popSampleList(string SampleStatus)
        {
            dvNoRec.Visible = false;
            List<SpecimenInfoBO> oSpec = (new SpecimenDA()).GetPendingSpecimenInfo(Convert.ToInt32(Session["OrgID"]), SampleStatus);
            rpPendingSampleList.DataSource = oSpec.OrderByDescending(x => x.CreatedOn); 
            rpPendingSampleList.DataBind();
            hPenSpecimenCount.Value = Convert.ToString(rpPendingSampleList.Items.Count);
            if (rpPendingSampleList.Items.Count == 0)
                dvNoRec.Visible = true;
        }

      

        protected void rpPendingSampleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string msg = "";
                SpecimenInfoBO oSamp = (SpecimenInfoBO)e.Item.DataItem;
                Literal ltr = (Literal)e.Item.FindControl("ltrPatName");
                //    <i class="icon-male icon-female pink blue"></i>

                if (oSamp.oPatient.Gender == "F")
                    ltr.Text = "<i class=\"icon-female pink\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" + " " + oSamp.CreatedByName + "<br /> Created On :" + " " + oSamp.CreatedOn;
                else
                    ltr.Text = "<i class=\"icon-male blue\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" + " " + oSamp.CreatedByName + "<br /> Created On :" + " " + oSamp.CreatedOn;
                ltr = (Literal)e.Item.FindControl("ltrPenStatus");
                if (ltr != null)
                    ltr.Text = Common.Constant.GetStatusFormat(oSamp.SpecimenStatus);
                ltr = (Literal)e.Item.FindControl("ltrPendingDetails");
                if (!oSamp.IsConsent)
                   msg= string.Concat("Consent Form missing <br/>",msg);
               
                if (!oSamp.IsSpecimenAccept)
                    msg = string.Concat("Specimen Accept for Test missing <br/>", msg);
                if (msg != "")
                    ltr.Text = msg;
            }
        }

        protected void rpPendingSampleList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Session["SpecimenID"] = Convert.ToString(e.CommandArgument);
            Response.Redirect("~/pages/Specimen?si=" + Convert.ToString(e.CommandArgument));
        }
    }
}