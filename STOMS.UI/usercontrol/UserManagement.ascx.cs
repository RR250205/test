using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using STOMS.Common;
using KoSoft.Entitlement;

namespace STOMS.UI.usercontrol
{
    public partial class UserManagement : System.Web.UI.UserControl
    {
        private string _activeParentTab = "";

        public string ActiveParentTab
        {
            get { return _activeParentTab; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblNewUserInformation.Text = "";
                lblUserInformation.Text = "";
                GetUserList();
                //lblNewUserInformation.Visible = false;
            }
        }

        private void ClearUserInputFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailId.Text = "";
            txtTelephone.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            
                UserBO userDetails = new UserBO()
            {
                UserFirstName = txtFirstName.Text.Trim(),
                UserLastName = txtLastName.Text.Trim(),
                UserEmail = txtEmailId.Text.Trim(),
                Password = KoSoft.Utility.KoCrypt.Encrypt(txtPassword.Text.Trim()),
                UserMobileNumber = txtTelephone.Text.Trim(),
                UserStatus = "Active",
                TenantID = Convert.ToInt32(Session["OrgID"]),
                kProductID = Convert.ToInt32(Constant.ProductID),
                CreatedBy = Convert.ToInt32(Session["UserID"]),
                UserID = 0,
                UserTypeName = stlabUser.Value,
            };

            DocumentDA documentda = new DocumentDA();

            int rtnappuserID = documentda.SaveUserCreationform(userDetails);            
            if (rtnappuserID == 0)
            {
                lblNewUserInformation.Text = "Email ID already exist";
                lblNewUserInformation.Attributes.Add("style", "color:Red");
            }
            else
            {
                ClearUserInputFields();
                lblNewUserInformation.Text = "User Added Successfully";
                lblNewUserInformation.Attributes.Add("style", "color:rgb(12, 156, 18)");
                GetUserList();
                dvListUser.Visible = true;
                dvAddUser.Visible = false;
            }
        }

        private void GetUserList()
        {
            List<AppUserBO> GetUserDeatils= new KoAccess(Constant.DBConnectionString).GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
            if(GetUserDeatils.Count>0)
            {
                rptUserInfo.DataSource = GetUserDeatils;
                rptUserInfo.DataBind();
                btnViewUsers.Visible = true;
            } 
            else
            {
                btnViewUsers.Visible = false;
            }
        }

        protected void rptUserInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AppUserBO appuserInfo = (AppUserBO)e.Item.DataItem;
                Label lbluserStatus = (Label)e.Item.FindControl("lblStatus");
                DropDownList ddlUserStatus = (DropDownList)e.Item.FindControl("ddlStatus");
               
                if (appuserInfo.UserStatus == "Active")
                {
                    lbluserStatus.Text = "";
                    lbluserStatus.Visible = false;
                    ddlUserStatus.Visible = true;
                    ddlUserStatus.SelectedItem.Text = appuserInfo.UserStatus;                    
                }
                else
                {
                    lbluserStatus.Visible = true;
                    ddlUserStatus.Visible = false;
                    lbluserStatus.Text = appuserInfo.UserStatus;
                    lbluserStatus.Attributes.Add("Style", "Color:Red");
                }
            }
        }       

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            lblNewUserInformation.Text = "";
            lblUserInformation.Text = "";
            ClearUserInputFields();
            dvAddUser.Visible = true;
            dvListUser.Visible = false;            
        }

        protected void btnViewUsers_Click(object sender, EventArgs e)
        {
            GetUserList();
            dvListUser.Visible = true;
            dvAddUser.Visible = false;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUserInformation.Text = "";
            DropDownList ddlUserStatus = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlUserStatus.NamingContainer;
           
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnappuserID = (HiddenField)item.FindControl("hAppUserID");
               
                if (ddlUserStatus.SelectedItem.Text == "InActive")
                {
                    if(Convert.ToInt32(Session["UserID"]) == Convert.ToInt32(hdnappuserID.Value))
                    {                        
                        lblUserInformation.Text = "You can't Inactive Yourself";
                        lblUserInformation.Attributes.Add("style", "color:Red");
                    }                        
                    else
                    { 
                        new DocumentDA().UpdateUserStatus(ddlUserStatus.SelectedItem.Text, Convert.ToInt32(hdnappuserID.Value));
                        GetUserList();
                    }
                }
            }            
        }

        private void ClearUserInputField()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailId.Text = "";
            txtPassword.Text = "";
            txtTelephone.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearUserInputField();
            lblNewUserInformation.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dvListUser.Visible = true;
            dvAddUser.Visible = false;
        }

        protected void rptUserInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Repeater rpt = (Repeater)source;
            //if(e.CommandName=="FirstName")
            //{
            //    HiddenField hdnappuserID = (HiddenField)rpt.FindControl("hFirstname");
            //    Response.Redirect("~/pages/myprofile/profile?usp=" + Convert.ToString(e.CommandArgument)  +Convert.ToString(e.CommandName));
            //}
            Response.Redirect("~/pages/myprofile/profile?usp=" + Convert.ToString(e.CommandArgument));
        }
    }
}