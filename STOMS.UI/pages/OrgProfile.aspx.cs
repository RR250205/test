using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STOMS.BO;
using STOMS.DA;
using System.IO;
using KoSoft.Entitlement;
namespace STOMS.UI.pages
{
    public partial class OrgProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PgContentTitle"] = "Organization Profile";

            OrgnizationView();
            dvsaveMessage.Visible = false;
        }

        protected void OrgnizationView()
        {
            //int logo = OrgImage.logoview();
            List<TenantDetailsBO> ViewOrgn = new OrgnizationDA().getOrgnization(Convert.ToInt32(Session["OrgID"]));
            if (ViewOrgn[0].Logo == "")
            {
                ViewOrgn[0].Logo = Convert.ToString(0);
            }
                
            ltrOrgNameView.Text = ViewOrgn[0].TenantName;
            ltrOrgnACNo.Text = ViewOrgn[0].TenantCode;
            ltrOrgnRegNo.Text = ViewOrgn[0].TenantRegNo;
            ltrAddress.Text = ViewOrgn[0].Address1;
            ltrCountry.Text = ViewOrgn[0].Country;
            ltrEmail.Text = ViewOrgn[0].Email;
            ltrSecContNo.Text = ViewOrgn[0].AlternatePhone;
            ltrPrimaryCont.Text = ViewOrgn[0].PrimaryPhone;
            ltrFaxNumber.Text = ViewOrgn[0].Fax;
            ltrEmail.Text = ViewOrgn[0].Email;
            hOrgLogo.Value = ViewOrgn[0].Logo;
            //imgLogo.ImageUrl = ViewOrgn[0].Logo;
            //List<CustomerBO> orgcount =new CustomerDA().GetCustomer(Convert.ToInt32(Session["OrgID"]));
            KoAccess oAccess = new KoSoft.Entitlement.KoAccess(Common.Constant.DBConnectionString);
            List<AppUserBO> orgcount = oAccess.GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(STOMS.Common.Constant.ProductID));
            ltrSubScriptionview.Text =Convert.ToString(orgcount.Count);
            
           DocumentBO logodocumentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(ViewOrgn[0].Logo));
            imgLogo.ImageUrl = logodocumentBO.DocNumber;
            string filesource = @"\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;
            string logo = @"~\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;
           
             imgLogo.ImageUrl = logo;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int DocID = OrgImage.fileUpload();
            if (DocID != 0)
                hOrgLogo.Value = DocID.ToString();
            
                TenantDetailsBO Organization = new TenantDetailsBO
                {
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    OrgRegNo = txtOrgnRegNo.Text.Trim(),
                    Country = ltrEditCountry.Text.Trim(),
                    PrimaryPhone = txtPrimaryContNo.Text.Trim(),
                    AlternatePhone = txtSecContNo.Text.Trim(),
                    Address1 = txtAddress.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Fax = txtFaxNumber.Text.Trim(),
                    Logo = Convert.ToString(hOrgLogo.Value),
                };

                OrgnizationDA organ = new OrgnizationDA();
                int profile = organ.SaveOrgnization(Organization);

                dvsaveMessage.Visible = true;
                dvorgView.Visible = true;
                dvOrgEdit.Visible = false;
                OrgnizationView();

    }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dvorgView.Visible = true;
            dvOrgEdit.Visible = false;
        }

        protected void btnOrgEdit_Click(object sender, EventArgs e)
        {
            dvOrgEdit.Visible =true;
            dvorgView.Visible = false;

            ltrOrgName.Text = ltrOrgNameView.Text;
            ltrtOrgACNoEdit.Text = ltrOrgnACNo.Text;
            txtOrgnRegNo.Text = ltrOrgnRegNo.Text;
            ltrEditCountry.Text = ltrCountry.Text;
            txtEmail.Text = ltrEmail.Text;
            txtFaxNumber.Text = ltrFaxNumber.Text;
            txtPrimaryContNo.Text = ltrPrimaryCont.Text;
            txtSecContNo.Text = ltrSecContNo.Text;
            txtAddress.Text = ltrAddress.Text;
            ltrSubScriptionEdit.Text = ltrSubScriptionview.Text;
        }
    }
}
