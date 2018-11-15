using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;

namespace STOMS.UI.pages
{
    public partial class myprofile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                List<AppUserBO> oUser = (new KoAccess(STOMS.Common.Constant.DBConnectionString)).GetAppUserInfo(Convert.ToInt32(Session["UserID"]));
                //List<STOMS.BO.UserBO> oUser = (new AdminDA()).GetUserProfile(Convert.ToInt32(Session["UserID"]));
               if (oUser.Count > 0)
               {
                   txtFirstName.Text = oUser[0].FirstName;
                   txtLastName.Text = oUser[0].LastName;
                   txtMobileNumber.Text = oUser[0].ContactPhone;
               }
            }
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            ltrMsg1.Text = "";
            if (txtFirstName.Text.Trim() != "")
            {
                //(new AdminDA()).UpdateMyProfile(new BO.UserBO { UserFirstName = txtFirstName.Text, 
                //                                                UserLastName = txtLastName.Text, 
                //                                                UserMobileNumber = txtMobileNumber.Text, 
                //                                                UserID = Convert.ToInt32(Session["UserID"]) });
                ltrMsg1.Text = "Profile Updated successfully...";
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            ltrMsg2.Text = "";
            if (txtNewPassword.Text.Trim().Length > 2)
            {
                if (txtNewPassword.Text.Trim() == txtConfirmNewPassword.Text.Trim())
                {
                    //(new AdminDA()).UpdatePassword(Convert.ToString(Session["UserID"]), txtNewPassword.Text.Trim());
                    //ltrMsg2.Text = "Password updated successfully...";
                    //txtNewPassword.Text = txtConfirmNewPassword.Text = "";
                }
                else
                {
                    ltrMsg2.Text = "Password and confirm password not matching";
                }
            }
            else
            {
                ltrMsg2.Text = "Password cannot be empty or should more than 2 characters";
            }
        }
    }
}