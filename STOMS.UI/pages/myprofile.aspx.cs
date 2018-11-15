using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;
using STOMS.BO;
using STOMS.DA;
using STOMS.Common;
namespace STOMS.UI.pages
{
    public partial class myprofile1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                string pgType = (string)RouteData.Values["type"];
                //profile
                //pass
                //role

                if (Request.QueryString["usp"] != null)
                {
                    ViewProfileImage(Convert.ToInt32(Request.QueryString["usp"]));
                    
                }
                else
                {
                    ViewProfileImage(Convert.ToInt32(Session["UserID"]));
                }
                if (ltrProfileFirst.Text == "")
                {
                    Session["PgContentTitle"] = "";
                }
                else
                {
                    Session["PgContentTitle"] = ltrProfileFirst.Text + "'s   Profile";
                    
                }
               
                lblProfileMessage.Visible = false;

                if (imgProfileLogo.ImageUrl == "~\\Docs\\Profile\\.")
                {
                    dvProfileavatar.Visible = true;
                    ProfileLogo.Visible = false;
                    dvProfilePreview.Visible = false;
                    dvViewProImge.Visible = false;

                   
                }
                else
                {
                    dvProfileavatar.Visible = false;
                    ProfileLogo.Visible = false;
                    dvProfilePreview.Visible =false;
                    dvViewProImge.Visible = true;


                }

                if (pgType != null)
                {
                    switch (pgType)
                    {
                        case "profile":
                          mvSection.SetActiveView(vwProfile);
                            //List<AppUserBO> oUser = (new KoAccess(STOMS.Common.Constant.DBConnectionString)).GetAppUserInfo(Convert.ToInt32(Session["UserID"]));
                            ////List<STOMS.BO.UserBO> oUser = (new AdminDA()).GetUserProfile(Convert.ToInt32(Session["UserID"]));
                            //if (oUser.Count > 0)
                            //{
                            //    ltrProfileFirst.Text = oUser[0].FirstName;
                            //    ltrprofilePrefix.Text = oUser[0].Prefix;
                            //    ltrprofileLast.Text = oUser[0].LastName;
                            //    ltrprofileMiddle.Text = oUser[0].MiddleName;
                            //    ltrprofilePrimaryEmail.Text = oUser[0].PrimaryEmail;
                            //    ltrprofileSecondaryEmail.Text = oUser[0].SecondaryEmail;
                            //    ltrprofilePhone.Text = oUser[0].ContactPhone;
                            //    hViewProfileLogo.Value = oUser[0].ProfilePhotoID.ToString();
                               
                            //        DocumentBO logodocumentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(oUser[0].ProfilePhotoID));
                            //    imgProfileLogo.ImageUrl = logodocumentBO.DocNumber;
                            //    string filesource = @"\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;
                            //    string logo = @"~\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;

                            //    imgProfileLogo.ImageUrl = logo;

                            //}
                            break;
                        case "pass":
                            mvSection.SetActiveView(vwPass);
                            break;
                        case "role":
                            mvSection.SetActiveView(vwRole);
                            //popRole();
                            break;
                    }
                }
                else
                {
                    mvSection.SetActiveView(vwProfile);
                }
            }
        }

        protected void ViewProfileImage(int userId)
        {
             List <AppUserBO> oUser = (new KoAccess(STOMS.Common.Constant.DBConnectionString)).GetAppUserInfo(userId);
            //List<STOMS.BO.UserBO> oUser = (new AdminDA()).GetUserProfile(Convert.ToInt32(Session["UserID"]));
            if (oUser.Count > 0)
            {
                ltrProfileFirst.Text = oUser[0].FirstName;
               // Session["FirstName"]= oUser[0].FirstName;
               // hPageTittleAppUser.Value = oUser[0].FirstName;
               
                ltrprofilePrefix.Text = oUser[0].Prefix;
                ltrprofileLast.Text = oUser[0].LastName;
                ltrProfileViewStatus.Text = oUser[0].AppUserStatus;
                //ltrprofileMiddle.Text = oUser[0].MiddleName;
                ltrprofilePrimaryEmail.Text = oUser[0].PrimaryEmail;
                ltrprofileSecondaryEmail.Text = oUser[0].SecondaryEmail;
                ltrprofilePhone.Text = oUser[0].ContactPhone;
                hViewProfileLogo.Value = oUser[0].ProfilePhotoID.ToString();
                hAppUsermanageId.Value = oUser[0].AppUserID.ToString();
                DocumentBO logodocumentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(oUser[0].ProfilePhotoID));
                imgProfileLogo.ImageUrl = logodocumentBO.DocNumber;
                string filesource = @"\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;
                string logo = @"~\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;

                imgProfileLogo.ImageUrl = logo;
                ProfileLogo.Visible = false;
                btnProfileCancel.Visible = false;
                dvProfilePreview.Visible = false;
                //dvViewProImge.Attributes.Add("class", "Inactive");

            }
            else
            {
                lblUserProfilemessage.Text = "User Profile Not Availables ";
                dvUserprofileHide.Visible = false;
                Session["PgContentTitle"] = "";

            }


        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
          
            int DocID = ProfileLogo.fileUpload();

            if (DocID != 0)
                hViewProfileLogo.Value = DocID.ToString();
            {
                
                (new KoAccess(STOMS.Common.Constant.DBConnectionString)).UpdateUserInfo(new AppUserBO {
                    AppUserID = (Convert.ToInt32(hAppUsermanageId.Value)),
                    FirstName = txtFirstName.Text.Trim(),
                    //MiddleName = txtMiddleName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Department = "",
                    Prefix = ddPrefix.SelectedValue,
                    PrimaryEmail = lblPrimaryEmail.Text,
                    SecondaryEmail = txtSecEmail.Text.Trim(),
                    ContactPhone = txtPhone.Text.Trim(),
                    ProfilePhotoID = Convert.ToInt32(hViewProfileLogo.Value)
                });

                ViewProfileImage(Convert.ToInt32(hAppUsermanageId.Value));
                btnProfileEdit.Visible = true;
                dvProfileEditPage.Visible = false;
                dvProfleView.Visible = true;
                btnSaveProfile.Visible = false;
                lblProfileMessage.Visible = true;
                //dvavatar.Visible = false;
                dvViewProImge.Visible = true;
                //dvProfilePreview.Visible = false;

                //showMessage.setErrorMsg("Profile Updated successfully...");
                

            }
            if (Convert.ToInt32(hViewProfileLogo.Value) == 0 && DocID == 0)
            {
                dvViewProImge.Visible = false;
                dvProfileavatar.Visible = true;
               
            }
            else
            {
                dvViewProImge.Visible = true;
                dvProfileavatar.Visible = false;
            }
           
        }


     
        


        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Trim().Length > 2)
            {
                if (txtNewPassword.Text.Trim() == txtConfPassword.Text.Trim())
                {
                    // if ((new KoAccess(STOMS.Common.Constant.DBConnectionString)).ChangeAppUserPassword(Convert.ToString(Session["UserID"]), KoSoft.Utility.KoCrypt.Decrypt(txtNewPassword.Text.Trim())) == "0")
                    if ((new KoAccess(STOMS.Common.Constant.DBConnectionString)).ChangeAppUserPassword(Convert.ToString(Session["UserID"]), KoSoft.Utility.KoCrypt.Encrypt(txtNewPassword.Text.Trim()), "", true) == "0")
                        showMessage.setErrorMsg("Successfully updated password", "S");
                    else
                        showMessage.setErrorMsg("Error occured in password reset..", "D");
                }
                else
                {
                    showMessage.setErrorMsg("Password and confirm password not matching", "R");
                }
            }
            else
            {
                showMessage.setErrorMsg("Password cannot be empty or should more than 2 characters", "D");
            }
        }

        protected void btnProfileEdit_Click(object sender, EventArgs e)
        {
           // ViewProfileImage(Convert.ToInt32(Request.QueryString["usp"]));
            dvProfileEditPage.Visible = true;
            dvProfleView.Visible = false ;
            btnProfileEdit.Visible = false;
            btnSaveProfile.Visible = true;  
            ProfileLogo.Visible = true;
            btnProfileCancel.Visible = true;
            txtFirstName.Text = ltrProfileFirst.Text;
            ddPrefix.SelectedValue = ltrprofilePrefix.Text;
            txtLastName.Text = ltrprofileLast.Text;
            ltrProfileEditStatus.Text = ltrProfileViewStatus.Text;
            txtSecEmail.Text = ltrprofileSecondaryEmail.Text;
            txtPhone.Text = ltrprofilePhone.Text;
            lblPrimaryEmail.Text = ltrprofilePrimaryEmail.Text;
            lblProfileMessage.Visible = false;
            dvProfilePreview.Visible = true;
            dvProfileavatar.Visible = false;

            if (imgProfileLogo.ImageUrl == "~\\Docs\\Profile\\.")
            {
                dvProfileavatar.Visible = true;
            }
            //dvProfilePreview.Visible = true;
        }

        protected void btnProfileCancel_Click(object sender, EventArgs e)
        {

            dvProfileEditPage.Visible = false;
            dvProfleView.Visible = true;
            btnProfileEdit.Visible = true;
            ProfileLogo.Visible = false;
            btnSaveProfile.Visible = false;
            btnProfileCancel.Visible = false;
            dvProfilePreview.Visible = false;

        }
    }
}
