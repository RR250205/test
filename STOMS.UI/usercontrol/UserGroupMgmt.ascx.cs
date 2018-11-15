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
using System.Web.UI.HtmlControls;

namespace STOMS.UI.usercontrol
{
    public partial class UserGroupMgmt : System.Web.UI.UserControl
    {
        private string _activeParentTab="";

        public string ActiveParentTab
        {
            get { return _activeParentTab; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ltrInformation.Text = "";
                getUserGroupList();                
                List<AppUserBO> GetAppUser = new KoAccess(Constant.DBConnectionString).GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
                if (GetAppUser.Count > 0)
                {
                    ddlMembers.DataTextField = "AppUserName";
                    ddlMembers.DataValueField = "AppUserID";
                    ddlMembers.DataSource = GetAppUser;
                    ddlMembers.DataBind();                   
                }
                dvAddMembers.Visible = false;                
            }
        }

        private void ActiveProfileTab()
        {
            profileTab.Attributes.Add("class", "active");
            tabProfile.Attributes.Add("class", "tab-pane active");
            permissionsTab.Attributes.Add("class", "Inactive");
            tabPermissions.Attributes.Add("class", "tab-pane fade");
            membersTab.Attributes.Add("class", "Inactive");
            tabMembers.Attributes.Add("class", "tab-pane fade");
        }

        private void ActivePermissionsTab()
        {
            profileTab.Attributes.Add("class", "Inactive");
            tabProfile.Attributes.Add("class", "tab-pane fade");
            permissionsTab.Attributes.Add("class", "active");
            tabPermissions.Attributes.Add("class", "tab-pane active");
            membersTab.Attributes.Add("class", "Inactive");
            tabMembers.Attributes.Add("class", "tab-pane fade");
        }

        private void ActiveMemberTab()
        {
            profileTab.Attributes.Add("class", "Inactive");
            tabProfile.Attributes.Add("class", "tab-pane fade");
            permissionsTab.Attributes.Add("class", "Inactive");
            tabPermissions.Attributes.Add("class", "tab-pane fade");
            membersTab.Attributes.Add("class", "active");
            tabMembers.Attributes.Add("class", "tab-pane active");
        }
        
        private void getUserGroupList()
        {
            List<UserGroupBO> userGrpList = new KoAccess(Constant.DBConnectionString).GetUserGroup(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
            if(userGrpList.Count>0)
            {
                gdvwUserGroup.DataSource = userGrpList;                
                gdvwUserGroup.DataBind();
            }            
        }

        protected void btnUserGroup_Click(object sender, EventArgs e)
        {
            string user = "";
            if (chkIsActive.Checked == true) {
                user = "Active";
            }
            else {
                user = "InActive";                
            }
            int userId =Convert.ToInt32(hUserGroupID.Value);
           
            if (userId != 0)
              hEditUserGroupID.Value = userId.ToString();

            UserBO usergroup = new UserBO()
            {
                UserGroupID = Convert.ToInt32(hUserGroupID.Value),
                UserName = txtGroupName.Text.Trim(),
                UserDescription = txtDescription.Text.Trim(),                
                UserGroupStatus = user,
                TenantID=Convert.ToInt32(Session["OrgID"]),
                kProductID= Convert.ToInt32(Constant.ProductID),
                CreatedBy = Convert.ToInt32(Session["UserID"]),
                IsStandard= Convert.ToBoolean(chkIsStandard.Checked),
            };

            AdminDA dauser = new AdminDA();
            int userid= dauser.SaveUsergroup(usergroup);
            hUserGroupID.Value = Convert.ToString(userid);
            ViewMembers();
            if (userid > 0)
            {
                lblGroupName.Text = txtGroupName.Text;
                ltrInformation.Text = " Group Info has been saved successfully";
                btnAddMembers.Visible = true;
                btnAddMembers.Text = "Add Members";
                dvPermissions.Visible = true;
                getSubscribedFunctions();
            }
            else
            {
                btnAddMembers.Visible = false;
                dvPermissions.Visible = false;
            }            
            btnUserGroup.Visible = false;
            btnEditMembers.Visible = true;
            dvMemberfileView.Visible = true;
            dvAddGroup.Visible = false;
        }

        protected void gdvwUserGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvwUserGroup.PageIndex = e.NewPageIndex;
            getUserGroupList();
        }

        private void clearUserGroupInputFields()
        {
            txtDescription.Text = "";
            txtGroupName.Text = "";            
        }

        protected void btnNewGroup_Click(object sender, EventArgs e)
        {            
            ltrInformation.Text = "";
            _activeParentTab = "UserGroup";           
            
            if (btnNewGroup.Text == "New Group")
            {
                hUserGroupID.Value = "0";
                btnNewGroup.Text = "View User Groups";
                tabPermissions.EnableViewState = true;
                clearUserGroupInputFields();
                ActiveProfileTab();
                btnEditMembers.Visible = false;
                dvAddGroup.Visible = true;
                dvNewGroup.Visible = true;
                dvListOfUserGroup.Visible = false;
                dvMemberfileView.Visible = false;
                dvPermissions.Visible = false;                
                btnAddMembers.Visible = false;
                btnUserGroup.Visible = true;
                btnMemberSave.Visible = false;
                rptPermissions.DataSource = null;
                rptPermissions.DataBind();
                gdvwUserGroupMembers.DataSource = null;
                gdvwUserGroupMembers.DataBind();
                ddlMembers.DataSource = null;
                ddlMembers.DataBind();
                ddlMembers.Visible = false;
            }
            else if(btnNewGroup.Text == "View User Groups")
            {
                btnMemberSave.Visible = false;
                btnNewGroup.Text = "New Group";
                dvListOfUserGroup.Visible = true;
                dvNewGroup.Visible = false;                
                getUserGroupList();
            }
            lblGroupName.Text = "";
        }        

        private void getUserGroupMembers(string UserGroupID, int TenantID, int ProductID)
        {
            gdvwUserGroupMembers.DataSource = null;
            List<GroupMemberBO> userGrpMemBO = new KoAccess(Constant.DBConnectionString).GetGroupMembers(UserGroupID, TenantID, ProductID);
            if (userGrpMemBO.Count > 0)
            {
                gdvwUserGroupMembers.DataSource = userGrpMemBO;
                gdvwUserGroupMembers.DataBind();
            }
            else
            {
                //dvNoRecord.Visible = true;
                gdvwUserGroupMembers.DataSource = null;
                gdvwUserGroupMembers.DataBind();
            }
        }

        protected void gdvwUserGroupMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "InActive")
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gdvwUserGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "InActive")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gdvwUserGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _activeParentTab = "UserGroup";
            ltrInformation.Text = "";
            dvPermissions.Visible = true;
            ActiveProfileTab();            

            if (e.CommandName == "GroupName")
            {                
                btnNewGroup.Text = "View User Groups";
                dvListOfUserGroup.Visible = false;
                dvNewGroup.Visible = true;             
                getUserGroupMembers(e.CommandArgument.ToString(), Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
                hUserGroupID.Value = e.CommandArgument.ToString();                
                dvMemberfileView.Visible = true;
                dvAddMembers.Visible = false;
                btnAddMembers.Text = "Add Members";                                                               
                dvAddGroup.Visible = false;
                
                List<AppUserBO> GetAppUser = new KoAccess(Constant.DBConnectionString).GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
                if (GetAppUser.Count > 0)
                {
                    ddlMembers.DataTextField = "AppUserName";
                    ddlMembers.DataValueField = "AppUserID";
                    ddlMembers.DataSource = GetAppUser;
                    ddlMembers.DataBind();
                }               
                ViewMembers();
                btnEditMembers.Visible = true;
                getSubscribedFunctions();
            } 
            else if(e.CommandName == "RemoveGroup")
            {
                new KoAccess(Constant.DBConnectionString).DeleteUserGroup(e.CommandArgument.ToString());
                getUserGroupList();
            }
        }

        protected void gdvwGroupMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvwUserGroupMembers.PageIndex = e.NewPageIndex;
            getUserGroupMembers(hUserGroupID.Value, Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
        }

        private void GetddlMembers()
        {
            List<AppUserBO> GetAppUser = new KoAccess(Constant.DBConnectionString).GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
            if (GetAppUser.Count > 0)
            {
                ddlMembers.DataTextField = "AppUserName";
                ddlMembers.DataValueField = "AppUserID";
                ddlMembers.DataSource = GetAppUser;
                ddlMembers.DataBind();
            }                
        }

        protected void ddlMembers_DataBound(object sender, EventArgs e)
        {
            List<GroupMemberBO> userGrpMemBO = new KoAccess(Constant.DBConnectionString).GetGroupMembers(hUserGroupID.Value, Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
            if (userGrpMemBO.Count > 0)
            {
                foreach (ListItem item in ddlMembers.Items)
                {
                    foreach (GroupMemberBO members in userGrpMemBO)
                    {
                        if (item.Text == members.Email)
                            item.Selected = true;                        
                    }
                }
            }
        }

        protected void btnMemberSave_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in ddlMembers.Items)
            {
                if (item.Selected)
                {
                    GroupMemberBO ObjGroupMember = new GroupMemberBO
                    {
                        UserGroupID = Convert.ToInt32(hUserGroupID.Value),
                        AppUserID = Convert.ToInt32(item.Value),
                    };
                    KoAccess objSaveMembers = new KoAccess(Constant.DBConnectionString);
                    objSaveMembers.AddUsergroupMembers(ObjGroupMember);
                }
                else
                {
                    foreach (GridViewRow Grw in gdvwUserGroupMembers.Rows)
                    {
                        LinkButton btn = (LinkButton)Grw.FindControl("lnkRemoveGrpMem");
                        if (Convert.ToInt32(btn.CommandArgument) == Convert.ToInt32(item.Value))
                            RemoveUserGroupMember(Convert.ToString(hUserGroupID.Value), Convert.ToInt32(item.Value));                        
                    }
                }               
            }
            getUserGroupMembers(hUserGroupID.Value, Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
            dvListOfUserGrpMembers.Visible = true;
            dvAddMembers.Visible = false;
            btnMemberSave.Visible = false;            
            btnAddMembers.Text = "Add Members";
        }       

        protected void btnAddMembers_Click(object sender, EventArgs e)
        {
            ltrInformation.Text = "";
            btnNewGroup.Text = "View User Groups";
            ActiveMemberTab();
            if (btnAddMembers.Text =="Add Members")
            {
                ddlMembers.Visible = true;
                GetddlMembers();
                dvListOfUserGrpMembers.Visible = false;
                btnMemberSave.Visible = true;
                dvAddMembers.Visible = true;
                btnAddMembers.Text = "View Members";                                       
            } 
            else if(btnAddMembers.Text == "View Members")
            {
                getUserGroupMembers(hUserGroupID.Value, Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));                                
                dvListOfUserGrpMembers.Visible = true;
                btnMemberSave.Visible = false;
                dvAddMembers.Visible = false;
                btnAddMembers.Text = "Add Members";
            }            
        }                
        
        private void RemoveUserGroupMember(string UserGroupID, int AppUserID)
        {
            new KoAccess(Constant.DBConnectionString).DeleteUserFromUserGroup(UserGroupID, AppUserID);            
        }
        protected void gdvwUserGroupMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "RemoveGroupMember")
            {
                _activeParentTab = "UserGroup";
                RemoveUserGroupMember(hUserGroupID.Value, Convert.ToInt32(e.CommandArgument));
                ActiveMemberTab();
                getUserGroupMembers(hUserGroupID.Value, Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));                
            }
        }        

        public void ViewMembers()
        {
            List<UserBO> ViewMembers = new AdminDA().getMembers(Convert.ToInt32(hUserGroupID.Value));
            {
                lblGroupName.Text=lblEdtGroupName.Text = ViewMembers[0].UserGroup;
                lblEdtDescription.Text = ViewMembers[0].UserDescription;
                lblEdtIsActive.Text = ViewMembers[0].UserGroupStatus;
                lblIsStandard.Text = ViewMembers[0].IsStandard.ToString();
            }
        }

        protected void btnEditMembers_Click(object sender, EventArgs e)
        {
            ActiveProfileTab();
            ltrInformation.Text = "";
            btnUserGroup.Visible = true;
            btnEditMembers.Visible = false;
            dvMemberfileView.Visible = false;
            dvAddGroup.Visible = true;            
            string check = "";

            if (lblEdtIsActive.Text == "Active")
            {
                check = "true";
            }
            else
            {
                check = "false"; 
            }           
            txtGroupName.Text = lblEdtGroupName.Text;
            txtDescription.Text = lblEdtDescription.Text;
            chkIsActive.Checked = Convert.ToBoolean(check);
            chkIsStandard.Checked = Convert.ToBoolean(lblIsStandard.Text);
        }

        protected void rptPermissions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
            {
                FunctionBO fbo =(FunctionBO) e.Item.DataItem;
                Literal ltrFunctionName = (Literal)e.Item.FindControl("ltrFunctionName");
                CheckBox chkRead = (CheckBox)e.Item.FindControl("chkRead");
                CheckBox chkWrite = (CheckBox)e.Item.FindControl("chkWrite");
                CheckBox chkExecute = (CheckBox)e.Item.FindControl("chkExecute");
                CheckBox chkExport = (CheckBox)e.Item.FindControl("chkExport");
                HiddenField hsrvUserGroupID = (HiddenField)e.Item.FindControl("hsrvUserGroupID");
                HiddenField hEntServiceID = (HiddenField)e.Item.FindControl("hEntServiceID");

                if (fbo.ParentFunctionID == 0)
                {
                    ltrFunctionName.Text = "&nbsp;&nbsp;&nbsp;" + "<b>" + fbo.FunctionName + "</b>";
                    chkRead.Visible = false;
                    chkWrite.Visible = false;
                    chkExecute.Visible = false;
                    chkExport.Visible = false;
                }
                else 
                {
                    if (fbo.entServBO.Read_Attri == true)
                        chkRead.Enabled = true;
                    else
                        chkRead.Enabled = false;
                    if (fbo.entServBO.Write_Attri == true)
                        chkWrite.Enabled = true;
                    else
                        chkWrite.Enabled = false;
                    if (fbo.entServBO.Export_Attri == true)
                        chkExport.Enabled = true;
                    else
                        chkExport.Enabled = false;
                    if (fbo.entServBO.Approve_Attri == true)
                        chkExecute.Enabled = true;
                    else
                        chkExecute.Enabled = false;

                    ltrFunctionName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fbo.FunctionName;
                    KoAccess access = new KoAccess(Constant.DBConnectionString);
                    FunctionBO fPermissionbo = access.GetUserGroupFunction(Convert.ToInt32(hUserGroupID.Value), fbo.entServBO.ServiceID);
                    hsrvUserGroupID.Value = fPermissionbo.srvUserGroupID.ToString();
                    chkRead.Checked = fPermissionbo.ReadAccess;
                    chkWrite.Checked = fPermissionbo.WriteAccess;
                    chkExport.Checked = fPermissionbo.ExportAccess;
                    chkExecute.Checked = fPermissionbo.ExecuteAccess;
                }
            }
        }

        private void getSubscribedFunctions()
        {
            KoAccess access = new KoAccess(Constant.DBConnectionString);
            List<FunctionBO> fbo = access.GetUserGroupPermission(Convert.ToInt32(Constant.ProductID),Convert.ToInt32(Session["OrgID"]));
            rptPermissions.DataSource = fbo;
            rptPermissions.DataBind();
        }

        protected void chkRead_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var item = (RepeaterItem)chk.NamingContainer;
            ActivePermissionsTab();

            if (item.ItemType == ListItemType.Item|| item.ItemType==ListItemType.AlternatingItem)
            {
                CheckBox chkRead = (CheckBox)item.FindControl("chkRead");
                CheckBox chkWrite = (CheckBox)item.FindControl("chkWrite");
                CheckBox chkExecute = (CheckBox)item.FindControl("chkExecute");
                CheckBox chkExport = (CheckBox)item.FindControl("chkExport");
                HiddenField hsrvUserGroupID = (HiddenField)item.FindControl("hsrvUserGroupID");
                HiddenField hEntServiceID = (HiddenField)item.FindControl("hEntServiceID");

                EntitleServiceBO entserviceBO = new EntitleServiceBO();
                entserviceBO.ServiceID = Convert.ToInt32(hEntServiceID.Value);
                FunctionBO functBO = new FunctionBO();
                functBO.ReadAccess = chkRead.Checked;
                functBO.WriteAccess = chkWrite.Checked;
                functBO.ExecuteAccess = chkExecute.Checked;
                functBO.ExportAccess = chkExport.Checked;
                functBO.srvUserGroupID = Convert.ToInt32(hsrvUserGroupID.Value);
                functBO.entServBO = entserviceBO; 

                int rtnServiceUserGroupID =  new KoAccess(Constant.DBConnectionString).SaveUserGroupFunction(Convert.ToInt32(hUserGroupID.Value),functBO);

                if(rtnServiceUserGroupID!=0)
                    hsrvUserGroupID.Value = Convert.ToString(rtnServiceUserGroupID);
                //find your items here
            }
        }
    }
}
