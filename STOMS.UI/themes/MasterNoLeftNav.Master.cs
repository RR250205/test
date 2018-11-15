using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.themes
{
    public partial class MasterNoLeftNav : System.Web.UI.MasterPage
    {
        public class ProfileMenu
        {
            public string Action { get; set; }
            public string ActTitle { get; set; }
            public string ActLogo { get; set; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("/");
            if (!this.IsPostBack)
            {

                spanRightTopUserFullName.InnerHtml = "<strong>" + Session["FullName"].ToString() + "</strong>";
                getCrossTenant();
                spanCompanyName.InnerHtml = "<strong>" + Session["OrgName"].ToString() + "</strong>";
                //DateTime dt = new DateTime(Convert.ToInt32(Session["ActYear"]), Convert.ToInt32(Session["ActMonth"]), 1);
                //spanActMonth.InnerHtml = "<strong>Active Month: " + dt.ToString("MMMM yyyy") + "</strong>";
            }
        }

        protected void lbtnSet_Click(object sender, EventArgs e)
        {
            string Tenant = "Tenant_";
            if((hActType.Value).Contains("Tenant_"))
           {
                string[] Tenantname = hActType.Value.Split('_');
                Session["OrgID"] = Tenantname[1];
                getCrossTenantDetails();
                Response.Redirect(Request.RawUrl);
            }
            else { 
            switch (hActType.Value)
            {
                case "LO":
                    Session.Abandon();
                    Response.Redirect("~/index.aspx");
                    break;
                case "PR":
                    Response.Redirect("~/pages/myprofile/profile");
                    break;
                case "LI":
                    Response.Redirect("~/pages/companyprofile");
                    break;
                case "CR":
                    Response.Redirect("~/pages/myprofile/role");
                    break;
                case "PW":
                    Response.Redirect("~/pages/myprofile/pass");
                    break;
                case "ORG":
                    Response.Redirect("~/pages/Orgprofile");
                    break;
                case "SE":
                    Response.Redirect("~/pages/mysetting");
                    break;
            }
            }
        }

        public void getCrossTenantDetails()
        {
            List<CrossTenantBO> crossTenant = new AdminDA().getCrossTenantDetails(Convert.ToInt32(Session["OrgID"]));

            Session["OrgName"] = crossTenant[0].TenantName;
        }
        public void getCrossTenant()
        {
            List<ProfileMenu> oProfileMenu = new List<ProfileMenu>();
            {
                oProfileMenu.Add(new ProfileMenu
                {
                    Action = "PR",
                    ActTitle = "My Profile",
                    ActLogo = "icon-user"
                });
                oProfileMenu.Add(new ProfileMenu
                {
                    Action = "ORG",
                    ActTitle = "Org Profile",
                    ActLogo = "icon-building"
                });
                oProfileMenu.Add(new ProfileMenu
                {
                    Action = "PW",
                    ActTitle = "Change Password",
                    ActLogo = "icon-key"
                });
                oProfileMenu.Add(new ProfileMenu
                {
                    Action = "LO",
                    ActTitle = "Logout",
                    ActLogo = "icon-off"
                });

            }
            List<CrossTenantBO> crossTenant = new AdminDA().getCrossTenant(Convert.ToInt32(Session["UserID"]));
            if (crossTenant.Count > 0)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<ul class=\"nav nav-list\"> ");
                sb1.Append("<li class=\"active mylabs\"><span class=\"menu-text\">" + "MY Labs" + "</span> </li>");
                for (int idx = 0; idx < crossTenant.Count; idx++)
                {
                    sb1.Append("<li class=\"active\"><a href=\"javascript:TMenuAction('" + "Tenant_" + crossTenant[idx].TenantID.ToString() + "');\"><span class=\"menu-text\">" + crossTenant[idx].TenantName + "</span> </a></li>");
                    //if (crossTenant[idx].AppUserID == 0)
                    //    sb1.Append("<li class=\"active\"><a href=\"javascript:getPage(" + crossTenant[idx].AppUserID.ToString() + ");\"></i><span class=\"menu-text\">" + crossTenant[idx].TenantName + "</span> </a></li>");
                    //else
                    //    sb1.Append("<li><a href=\"javascript:getPage(" + crossTenant[idx].AppUserID.ToString() + ");\"><span class=\"menu-text\">" + crossTenant[idx].TenantName + "</span> </a></li>");


                }
                sb1.Append("<li class=\"active list-group-item \"> </li>");
                for (int idx = 0; idx < oProfileMenu.Count; idx++)
                {
                    sb1.Append("<li class=\"active\"><a href=\"javascript:TMenuAction('" + oProfileMenu[idx].Action.ToString() + "');\"><i class=\"" + oProfileMenu[idx].ActLogo + "\"></i><span class=\"menu-text\">" + oProfileMenu[idx].ActTitle + "</span> </a></li>");
                    //if (crossTenant[idx].AppUserID == 0)
                    //    sb1.Append("<li class=\"active\"><a href=\"javascript:getPage(" + crossTenant[idx].AppUserID.ToString() + ");\"></i><span class=\"menu-text\">" + crossTenant[idx].TenantName + "</span> </a></li>");
                    //else
                    //    sb1.Append("<li><a href=\"javascript:getPage(" + crossTenant[idx].AppUserID.ToString() + ");\"><span class=\"menu-text\">" + crossTenant[idx].TenantName + "</span> </a></li>");


                }



                sb1.Append("</ul>");
                user_menu.InnerHtml = sb1.ToString();


            }

            else
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<ul class=\"nav nav-list\"> ");
                for (int idx = 0; idx < oProfileMenu.Count; idx++)
                {
                    sb1.Append("<li class=\"active\"><a href=\"javascript:TMenuAction('" + oProfileMenu[idx].Action.ToString() + "');\"><i class=\"" + oProfileMenu[idx].ActLogo + "\"></i><span class=\"menu-text\">" + oProfileMenu[idx].ActTitle + "</span> </a></li>");
                }

                sb1.Append("</ul>");
                user_menu.InnerHtml = sb1.ToString();


            }

        }


            protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/searchresult?searchkey=" + txtSearch.Text, true);
        }
    }
}