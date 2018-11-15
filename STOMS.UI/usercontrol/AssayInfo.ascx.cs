using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.usercontrol
{
    public partial class AssayInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void PopulateCurrentAssay()
        {
            dvNoRec.Visible = false;
            List<AssayGroupBO> oAssay = (new SpecimenDA()).GetAssayGroupList(Convert.ToInt32(Session["OrgID"]), "Current");
            if (oAssay.Count > 0)
            {
                //ltrCurrentAssay.Text = oAssay[0].AssayBIN + (oAssay[0].AssayDesc == string.Empty ? "" : " - " + oAssay[0].AssayDesc);
                hAssayID.Value = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.CommandArgument = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.Text = Convert.ToString(oAssay[0].AssayBIN);
                rpAssaySpecimens.DataSource = oAssay[0].oSample;
                rpAssaySpecimens.DataBind();
            }
            else
            {
                dvNoRec.Visible = true;
            }
        }

        public void PopulateAssay(int AssayID)
        {
            dvNoRec.Visible = false;
            List<AssayGroupBO> oAssay = (new SpecimenDA()).GetAssayGroupList(Convert.ToInt32(Session["OrgID"]), "Current", AssayID);
            if (oAssay.Count > 0)
            {
                hAssayID.Value = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.CommandArgument = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.Text = Convert.ToString(oAssay[0].AssayBIN);

                rpAssaySpecimens.DataSource = oAssay[0].oSample;
                rpAssaySpecimens.DataBind();
            }
            else
            {
                dvNoRec.Visible = true;
            }
        }

        public void PopulateSpecimenAssay(int SpecimenID)
        {
            lbtnAssay.Text = "";
            rpAssaySpecimens.DataSource = null;
            rpAssaySpecimens.DataBind();
            dvNoRec.Visible = false;
            List<AssayGroupBO> oAssay = (new SpecimenDA()).GetAssaySpecimens(Convert.ToInt32(Session["OrgID"]), SpecimenID);
            if (oAssay.Count > 0)
            {
                hAssayID.Value = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.CommandArgument = Convert.ToString(oAssay[0].AssayID);
                lbtnAssay.Text = Convert.ToString(oAssay[0].AssayBIN);
                rpAssaySpecimens.DataSource = oAssay[0].oSample;
                rpAssaySpecimens.DataBind();                
            }
            else
            {
                dvNoRec.Visible = true;
            }
        }

        protected void rpAssaySpecimens_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SpecimenInfoBO oSamp = (SpecimenInfoBO)e.Item.DataItem;
                Literal ltr = (Literal)e.Item.FindControl("ltrPatName");
                //    <i class="icon-male icon-female pink blue"></i>

                if (oSamp.oPatient.Gender == "F")
                    ltr.Text = "<i class=\"icon-female pink\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" + " " + oSamp.CreatedByName + "<br /> Created On :" + " " + oSamp.CreatedOn;
                else
                    ltr.Text = "<i class=\"icon-male blue\"></i>&nbsp;" + /*oSamp.oPatient.PatientName*/  "Created By :" + " " + oSamp.CreatedByName + "<br /> Created On :" + " " + oSamp.CreatedOn;
                ltr = (Literal)e.Item.FindControl("ltrStatus");
                if (ltr != null)
                    ltr.Text = Common.Constant.GetStatusFormat(oSamp.SpecimenStatus);
            }
        }

        protected void rpAssaySpecimens_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
               // Session["SpecimenID"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/pages/Specimen?si="+ Convert.ToString(e.CommandArgument));
            }
        }

        protected void lbtnAssay_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ADetail")
            {
               // Session["AssayID"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/pages/assaydetail?ai="+ Convert.ToString(e.CommandArgument));
            }
        }
    }
}