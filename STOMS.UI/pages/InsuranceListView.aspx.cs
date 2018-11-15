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
    public partial class InsuranceListView : System.Web.UI.Page
    {
        int InsuranceId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Convert.ToInt32(Session["InsuranceNo"])!=0)
            {
                Session["PgContentTitle"] = "Pre-Authorization Insurance Details";

                InsuranceId = Convert.ToInt32(Session["InsuranceNo"]);
                GetInsurance(InsuranceId);
                hInsuranceId.Value = InsuranceId.ToString();
            }
           
        }

        public void GetInsurance(int Insurance)
        {
           List<PreInsurance> objins = new CustomerDA().GetPreInsurance(Insurance);


            ltrPhyPatientName.Text = objins[0].PatientName;
            ltrDatabirth.Text = objins[0].Dataofbirth;
            ltrGender.Text = objins[0].Gender;
            ltrPatientMobileNo.Text = objins[0].MobileNumber;
            ltrInsuranceCard_IDno.Text = objins[0].InsuranceCard_IDno;
            ltrPolicyNumber.Text = objins[0].PolicyNumber;
            ltrPrimaryInsName.Text = objins[0].PrimaryInsName;
            ltrPolicyName.Text = objins[0].PolicyName;
            hTenantID.Value = objins[0].TenantID.ToString();
        }

        protected void Resultremove_Click(object sender, EventArgs e)
        {
        
            if(InsuranceUpdate.Visible==true)
            {
                dvInsuranceEdit.Visible = false;
                dvOrderView.Visible =true;
                btninsuranceedit.Visible = true;
                InsuranceUpdate.Visible = false;

            }
            else
            {

                Session["ddlInsurancelist"] = hTenantID.Value;
                //int id = Convert.ToInt32(Session["OrgID"]);
                Response.Redirect("~/Pages/ListInsurance");
            }
            
                // Response.Redirect("~/Pages/PhyOrderList");
        }

        protected void btninsuranceedit_Click(object sender, EventArgs e)
        {
            dvOrderView.Visible = false;
            dvInsuranceEdit.Visible = true;
            btninsuranceedit.Visible = false;
            InsuranceUpdate.Visible =true;
            txtInsuranceCard_IDno.Text = ltrInsuranceCard_IDno.Text;
            txtMobileNo.Text = ltrPatientMobileNo.Text;
            txtPatientDOB.Text = ltrDatabirth.Text;
            txtPatientName.Text = ltrPhyPatientName.Text;
            txtPolicyName.Text = ltrPolicyName.Text;
            txtPolicyNumber.Text = ltrPolicyNumber.Text;
            txtPrimaryInsName.Text = ltrPrimaryInsName.Text;

            char[] genter = ltrGender.Text.ToCharArray();



                if (Char.IsUpper(genter[0]))
                {
                    optF.Checked = (ltrGender.Text == "Female" ? true : false);
                    optM.Checked = (ltrGender.Text == "Male" ? true : false);
                    optUn.Checked = (ltrGender.Text == "Other" ? true : false);
                }
                else
                {
                    optF.Checked = (ltrGender.Text == "female" ? true : false);
                    optM.Checked = (ltrGender.Text == "male" ? true : false);
                    optUn.Checked = (ltrGender.Text == "other" ? true : false);
                }
   
        }

        protected void InsuranceUpdate_Click(object sender, EventArgs e)
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
            objpreins.PreInsuranceNo =Convert.ToInt32(hInsuranceId.Value);
            objpreins.PrimaryInsName = txtPrimaryInsName.Text.Trim();
            objpreins.Dataofbirth = txtPatientDOB.Text.Trim();
            objpreins.Gender = _Gender;
            objpreins.InsuranceCard_IDno = txtInsuranceCard_IDno.Text.Trim();
            objpreins.TenantID = Convert.ToInt32(hTenantID.Value);
            objpreins.Comments = txtComments.Text;

            CustomerDA updateins = new CustomerDA();
            int val = updateins.UpdateInsurance(objpreins);

            dvOrderView.Visible = true;
            dvInsuranceEdit.Visible = false;
            btninsuranceedit.Visible = true;
            InsuranceUpdate.Visible = false;
            GetInsurance(InsuranceId);

        }
    }
}