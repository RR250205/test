using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.pages
{
    public partial class InsuranceAddnew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PgContentTitle"] = "Add New Insurance Details";
        }

        protected void btnremoveInsurance_Click(object sender, EventArgs e)
        {
            //Session["ddlInsurancelist"] = Convert.ToInt32(Session["ddlInsurancelist"]);
            //int id = Convert.ToInt32(Session["OrgID"]);
            Response.Redirect("~/Pages/ListInsurance");
        }

        protected void btnSaveInsurance_Click(object sender, EventArgs e)
        {
            string _Gender = "";
            if (optM.Checked)
                _Gender = "Male";
            else if (optF.Checked)
                _Gender = "Female";
            else if (optUn.Checked)
                _Gender = "Other";


            PreInsurance objpreins = new PreInsurance();

            objpreins.PatientName = txtPatientName.Text.Trim();
            objpreins.PolicyName = txtPolicyName.Text.Trim();
            objpreins.PolicyNumber = txtPolicyNumber.Text.Trim();
            objpreins.MobileNumber = txtMobileNo.Text.Trim();

            objpreins.PrimaryInsName = txtPrimaryInsName.Text.Trim();
            objpreins.Dataofbirth = txtPatientDOB.Text.Trim();
            objpreins.Gender = _Gender;
            objpreins.InsuranceCard_IDno = txtInsuranceCard_IDno.Text.Trim();
            objpreins.TenantID = Convert.ToInt32(Session["ddlInsurancelist"]);
            objpreins.Comments = txtComments.Text;

            CustomerDA updateins = new CustomerDA();
            int val = updateins.UpdateInsurance(objpreins);

            Response.Redirect("~/Pages/ListInsurance");
        }
    }
}