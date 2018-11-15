using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;
using STOMS.Common;
using System.Web.UI.HtmlControls;

namespace STOMS.UI.admin
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)

            {
                Session["PgContentTitle"] = "Administration/Configuration";
               

                // popCust();
                //popUser();
                //popCountry();
            }
            if (ucUserManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucUserManagement.ActiveParentTab);
            }
            else if (ucUserGroupManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucUserGroupManagement.ActiveParentTab);
            }
            //else if (ucEmailConfiguration.ActiveParentTab != "")
            //{
            //    setActiveAdminTab(ucEmailConfiguration.ActiveParentTab);
            //}
            //else if (ucCourierConfiguration.ActiveParentTab != "")
            //{
            //    setActiveAdminTab(ucCourierConfiguration.ActiveParentTab);
            //}
            else if (ucKitTypeConfiguration.ActiveParentTab != "")
            {
                setActiveAdminTab(ucKitTypeConfiguration.ActiveParentTab);
            }
            else if(ucActiveClientConfiguration.ActiveParentTab !="")
            {
                setActiveAdminTab(ucActiveClientConfiguration.ActiveParentTab);
            }
            else if (ucTestProfileManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucTestProfileManagement.ActiveParentTab);
            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ucCourierConfiguration.onLoad = true;
            ucCourierConfiguration.Refresh();
            if(ucUserManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucUserGroupManagement.ActiveParentTab);
            }
            else if (ucUserGroupManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucUserGroupManagement.ActiveParentTab);
            }
            //else if (ucEmailConfiguration.ActiveParentTab != "")
            //{
            //    setActiveAdminTab(ucEmailConfiguration.ActiveParentTab);
            //}
            //else if (ucCourierConfiguration.ActiveParentTab != "")
            //{
            //    setActiveAdminTab(ucCourierConfiguration.ActiveParentTab);
            //}
            else if (ucKitTypeConfiguration.ActiveParentTab != "")
            {
                setActiveAdminTab(ucKitTypeConfiguration.ActiveParentTab);

                
                //popCust();
                //popUser();
                //popCountry();

            }
            else if (ucActiveClientConfiguration.ActiveParentTab != "")
            {
                setActiveAdminTab(ucActiveClientConfiguration.ActiveParentTab);
            }
            else if (ucTestProfileManagement.ActiveParentTab != "")
            {
                setActiveAdminTab(ucTestProfileManagement.ActiveParentTab);
            }
        }
        private void setActiveAdminTab(string ActiveParentTab)
        {
            switch (ActiveParentTab)
            {
                case "UserManagement":
                    userManagementTab.Attributes.Add("class", "active");
                    tabUserManagement.Attributes.Add("class", "tab-pane active");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "UserGroup":                    
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "active");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane active");                    
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "Email":
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "active");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "Courier":
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "active");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane active");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "KitType":                    
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "active");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane active");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "ActiveClient":
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "active");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane active");
                    testProfileManagementTab.Attributes.Add("class", "Inactive");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");
                    break;

                case "TestProfile":
                    userManagementTab.Attributes.Add("class", "Inactive");
                    tabUserManagement.Attributes.Add("class", "tab-pane fade");
                    userGrpManagementTab.Attributes.Add("class", "Inactive");
                    tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                    emailConfigurationTab.Attributes.Add("class", "Inactive");
                    tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                    courierConfigurationTab.Attributes.Add("class", "Inactive");
                    tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                    kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                    tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                    activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                    tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                    testProfileManagementTab.Attributes.Add("class", "active");
                    tabTestProfileManagement.Attributes.Add("class", "tab-pane active");
                    break;

                default:
                    break;
            }           
            
        }
        private void popCust()
        {
            tgrdCust.DataSource = (new CustomerDA()).GetCustomer(Convert.ToInt32(Session["OrgID"]));
            tgrdCust.DataBind();
        }

        private void SetActivetab(int tabNo)
        {
            tbCalc.Attributes.Remove("class");
            tbCountry.Attributes.Remove("class");
            tbCust.Attributes.Remove("class");
            tbUser.Attributes.Remove("class");
            switch (tabNo)
            {
                case 1:
                    tbCust.Attributes.Add("class", "active");
                    break;
                case 2:
                    tbUser.Attributes.Add("class", "active");
                    break;
                case 3:
                    tbCalc.Attributes.Add("class", "active");
                    break;
                case 4:
                    tbCountry.Attributes.Add("class", "active");
                    break;
            }
        }
        private void popUser()
        {
            KoAccess oAccess = new KoSoft.Entitlement.KoAccess(Common.Constant.DBConnectionString);
            tgrdUser.DataSource = oAccess.GetAppUser(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(STOMS.Common.Constant.ProductID));
            tgrdUser.DataBind();
        }

        private void popCountry()
        {
            List<CountryMasterBO> objCountry = (new AdminDA()).getCountryList(false);
            frmCountry.Items.Clear();
            for (int idx = 0; idx < objCountry.Count; idx++)
            {
                frmCountry.Items.Add(new ListItem(objCountry[idx].CountryName, objCountry[idx].CountryID.ToString()));

                if (objCountry[idx].isSelected)
                    frmCountry.Items[idx].Selected = true;
            }
        }
        protected void tgrdCust_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void tgrdUser_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void btnSaveCountry_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            SetActivetab(2);
            phUserList.Visible = false;
            phUserAddEdit.Visible = true;
        }


        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            //KoAccess oAccess = new KoAccess(Common.Constant.DBConnectionString);
            //TenantBO oCustomer = new TenantBO { TenantID = Convert.ToInt32(Session["OrgID"]) };
            //List<UserRoleBO> oRole = new List<UserRoleBO>();
            //oRole.Add(new UserRoleBO { ProductID = Convert.ToInt32(Common.Constant.ProductID) });

            //oAccess.AddAppUser(new KoSoft.Entitlement.AppUserBO
            //{
            //    UserName = "",
            //    Password = "",
            //    Company = oCustomer,
            //    FirstName = "",
            //    LastName = "",
            //    MiddleName = "",
            //    UserStatus = "Active",
            //    MyRoles = oRole,
            //    CreatedBy = Convert.ToInt32(Session["UserID"])
            //});

            popUser();
        }

        protected void hActiveTab_ValueChanged(object sender, EventArgs e)
        {           
            if (hActiveTab.Value != "")
            {
                switch (hActiveTab.Value)
                {
                    case "tabUserManagement":
                        userManagementTab.Attributes.Add("class", "active");
                        tabUserManagement.Attributes.Add("class", "tab-pane active");
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "active");
                        tabUserManagement.Attributes.Add("class", "tab-pane active");
                        emailConfigurationTab.Attributes.Add("class", "Inactive");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                        
                        break;

                    case "tabUserGrpManagement":
                        userGrpManagementTab.Attributes.Add("class", "active");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane active");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "Inactive");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane fade");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                       
                        break;

                    case "tabEmailConfiguration":
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "active");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                        
                        break;
                        
                    case "tabCourierConfiguration":
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "active");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                        
                        break;

                    case "tabKitTypeConfiguration":
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "active");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                        
                        break;

                    case "tabActiveClientConfiguration":
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "active");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "Inactive");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane fade");                        
                        break;

                    case "tabTestProfileManagement":
                        userGrpManagementTab.Attributes.Add("class", "Inactive");
                        tabUserGrpManagement.Attributes.Add("class", "tab-pane fade");
                        userManagementTab.Attributes.Add("class", "Inactive");
                        tabUserManagement.Attributes.Add("class", "tab-pane fade");
                        emailConfigurationTab.Attributes.Add("class", "active");
                        tabEmailConfiguration.Attributes.Add("class", "tab-pane active");
                        courierConfigurationTab.Attributes.Add("class", "Inactive");
                        tabCourierConfiguration.Attributes.Add("class", "tab-pane fade");
                        kitTypeConfigurationTab.Attributes.Add("class", "Inactive");
                        tabKitTypeConfiguration.Attributes.Add("class", "tab-pane fade");
                        activeClientConfigurationTab.Attributes.Add("class", "Inactive");
                        tabActiveClientConfiguration.Attributes.Add("class", "tab-pane fade");
                        testProfileManagementTab.Attributes.Add("class", "active");
                        tabTestProfileManagement.Attributes.Add("class", "tab-pane active");                        
                        break;
                }
            }
        }
    }
 }
