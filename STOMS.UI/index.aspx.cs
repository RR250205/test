using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;
using KoSoft.Entitlement;


namespace STOMS.UI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrMsg.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           if ((stlStromsUser.Value).Contains("#"))
            {
                string[] User = stlStromsUser.Value.Split('#');


                husertype.Value = User[1];
            }
           

                if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "")
                {
                    List<AppUserBO> objUsr = (new KoAccess(STOMS.Common.Constant.DBConnectionString)).GetAppUser(txtUserName.Text.Trim(), KoSoft.Utility.KoCrypt.Encrypt(txtPassword.Text.Trim()), Request.UserHostAddress, Convert.ToInt32(STOMS.Common.Constant.ProductID), husertype.Value);
                    if (objUsr.Count > 0)
                    {
                        if (objUsr[0].ErrorMsg.ErrorCode == 0)
                        {
                            using (STOMS.UI.CommonCS.CustomLogs clog = new CommonCS.CustomLogs())
                            {
                                string logString = $"\n{DateTime.Now.ToString()} \t Loggedin as {objUsr[0].FirstName} {objUsr[0].AppUserName} for {objUsr[0].Company.TenantName}";
                                string filepath = Server.MapPath("~/Logs/AuditLog_" + DateTime.Today.ToString("MM-dd-yy") + ".log");
                                clog.UpdateToLog(filepath, logString);
                            }

                            Session["FirstName"] = objUsr[0].FirstName;
                            Session["FullName"] = objUsr[0].FullName;
                            Session["OrgName"] = objUsr[0].Company.TenantName;
                            Session["OrgID"] = objUsr[0].Company.TenantID;
                            Session["ActYear"] = DateTime.Now.Year;
                            Session["ActMonth"] = DateTime.Now.Month;
                            Session["menuID"] = 0;
                            Session["UserID"] = objUsr[0].AppUserID;
                            Session["UserMail"] = objUsr[0].AppUserName;
                            Session["UserType"] = objUsr[0].UserAccessType;
                        //spanActMonth.InnerText = "Activity Month: " + dt.ToString("MMM yyyy");
                   
                        if (objUsr[0].DefaultPage != "")
                        {
                            if (Convert.ToString(Session["UserType"]) == "Insuranceagent")
                            {
                                Response.Redirect("~/pages/ListInsurance");
                            }
                            else
                            {
                                Response.Redirect(objUsr[0].DefaultPage);
                            }
                                
                        }



                        else
                        {
                            Response.Redirect("~/pages/dashboard");
                        }
                               
                        }
                        else
                            ltrMsg.Visible = true;

                        //ltrMsg.Text = objUsr[0].ErrorMsg.ErrorMsg;
                    }
                    else
                        ltrMsg.Text = "Authentication failed...";
                }
            
           
        }    

        protected void PasswordChange_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }

        protected void btnLoginAccess_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {

        }

        protected void btnViewResult_Click(object sender, EventArgs e)
        {

        }

        //protected void btnregRegister_Click(object sender, EventArgs e)
        //{
           
        //    if(txtregPassword.Text.Trim()==txtregConfirmPassword.Text.Trim())

        //    { 
        //    RegisterUserBO RegisterUser = new RegisterUserBO();
        //    RegisterUser.Address = txtregAddress.Text.Trim();
        //    RegisterUser.City = txtregCity.Text.Trim();
        //    RegisterUser.State = txtregState.Text.Trim();
        //    RegisterUser.Country = txtregCountry.Text.Trim();
        //    RegisterUser.AppUserPassword = KoSoft.Utility.KoCrypt.Encrypt(txtregPassword.Text.Trim()) ;
        //    RegisterUser.Email = txtregEmail.Text.Trim();
        //    RegisterUser.FirstName = txtregFirstName.Text.Trim();
        //    RegisterUser.LastName = txtregLastName.Text.Trim();
        //    RegisterUser.Website = txtregWebSite.Text.Trim();
        //    RegisterUser.UserType = "physician";
        //    RegisterUser.PrimaryPhone = txtregPhoneNumber.Text.Trim();

        //        DocumentDA userRegister = new DocumentDA();
        //        userRegister.SaveUserRegister(RegisterUser);
        //    }

        //    else
        //    {

        //    }





        //}
    }
}
