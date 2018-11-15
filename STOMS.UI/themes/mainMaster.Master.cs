using STOMS.DA;
using STOMS.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;
namespace STOMS.UI
{ 
    public class ProfileMenu
{
    public string Action { get; set; }
    public string ActTitle { get; set; }
    public string ActLogo { get; set; }
}

    public partial class mainMaster : System.Web.UI.MasterPage
    {
        static int fwdcount = 0;
        static int bckcount = 0;
        static int currmonth = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("/");
            if (!this.IsPostBack)
            {
                
                //displayMenu(Convert.ToInt32(Session["menuID"]));
                spanRightTopUserFullName.InnerHtml = "<strong>" + Session["FullName"].ToString() + "</strong>";
                ltrMyName.Text = Session["FullName"].ToString();
                getCrossTenant();
                spanCompanyName.InnerHtml = "<strong>" + Session["OrgName"].ToString() + "</strong>";
                //DateTime dt = new DateTime(Convert.ToInt32(Session["ActYear"]), Convert.ToInt32(Session["ActMonth"]), 1);
                //spanActMonth.InnerHtml = "<strong>Active Month: " + dt.ToString("MMMM yyyy") + "</strong>";
            }
            if (Session["PgContentTitle"] != null)
            {
               ltrContentTitle.Text = Convert.ToString(Session["pgcontenttitle"]);
                Session.Remove("pgcontenttitle");
            }
        }

        //private void displaymenu(int mnuid)
        //{
        //    stringbuilder sb1 = new stringbuilder("<ul id=\"ulsidebar\" class=\"sidebar-menu\">");
        //    list<entitleservicebo> objewnt = (new koaccess(stoms.common.constant.dbconnectionstring)).getuseraccessmoduleservices(convert.tostring(session["userid"]), mnuid, convert.toint32(common.constant.productid));

        //    foreach (entitleservicebo oent in objewnt)
        //    {
        //        if (oent.serviceid == mnuid || mnuid == 0)
        //        {
        //            sb1.append("<li class=\"active\"><a href=\"javascript:getpage(" + convert.tostring(oent.serviceid) + ");\"><i class=\"" + oent.displayindicator + "\"></i> <span>" + oent.servicename + "</span> </a> </li>");
        //            ltrcontenttitle.text = oent.servicename;
        //            mnuid = -1;
        //        }
        //        else
        //        {
        //            sb1.append("<li><a href=\"javascript:getpage(" + convert.tostring(oent.serviceid) + ");\"><i class=\"" + oent.displayindicator + "\"> </i> <span>" + oent.servicename + "</span> </a> </li>");
        //        }
        //    }
        //    sb1.append("</ul>");
        //    ltrlmenu.text = sb1.tostring();
        //    if (session["pgcontenttitle"] != null)
        //    {
        //        ltrcontenttitle.text = convert.tostring(session["pgcontenttitle"]);
        //        session.remove("pgcontenttitle");
        //    }
        //}

        protected void lbtnSet_Click(object sender, EventArgs e)
        {
           string Tenant = "Tenant_";


            if ((hActType.Value).Contains("Tenant_"))
            {
                string[] Tenantname = hActType.Value.Split('_');
                Session["OrgID"] = Tenantname[1];
                getCrossTenantDetails();
                if(Convert.ToString(Session["UserType"])== "Insuranceagent")
                {
                    Response.Redirect("~/Pages/ListInsurance");
                    //Response.Redirect("~/Pages/ListInsurance?Tenant=" + Convert.ToString(Session["OrgID"]));
                }
                else
                {
                    Response.Redirect("DashBoard");
                }
                
            }
            else
            {
                switch (hActType.Value)
                {
                    case "LO":
                        Session.Abandon();
                        Response.Redirect("/");
                        break;
                    case "PR":
                        Response.Redirect("~/pages/myprofile/profile");
                        break;
                    case "PW":
                        Response.Redirect("~/pages/myprofile/pass");
                        break;
                    case "ORG":
                        Response.Redirect("~/pages/Orgprofile");
                        break;
                    case "PM":
                        DateTime dt = new DateTime(Convert.ToInt32(Session["ActYear"]), Convert.ToInt32(Session["ActMonth"]), 1);
                        dt = dt.AddMonths(-1);
                        Session["ActMonth"] = dt.Month;
                        Session["ActYear"] = dt.Year;
                        Response.Redirect("~/pages/dashboard");
                        break;
                    case "NM":
                        DateTime dt1 = new DateTime(Convert.ToInt32(Session["ActYear"]), Convert.ToInt32(Session["ActMonth"]), 1);
                        dt1 = dt1.AddMonths(1);
                        Session["ActMonth"] = dt1.Month;
                        Session["ActYear"] = dt1.Year;
                        Response.Redirect("~/pages/dashboard");
                        break;
                    case "LI":
                        Response.Redirect("~/pages/companyprofile");
                        break;
                    case "CR":
                        Response.Redirect("~/pages/myprofile/role");
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

            Session["OrgName"]= crossTenant[0].TenantName;
        }

        public void getCrossTenant()
        {
            List<ProfileMenu> oProfileMenu = new List<ProfileMenu>();
            
            oProfileMenu.Add(new ProfileMenu
            {
                Action = "PR",
                ActTitle = "My Profile",
                ActLogo= "fa fa-user"
            });
            oProfileMenu.Add(new ProfileMenu
            {
                Action = "ORG",
                ActTitle = "Org Profile",
                ActLogo = "fa fa-building-o"
            });
            oProfileMenu.Add(new ProfileMenu
            {
                Action = "PW",
                ActTitle = "Change Password",
                ActLogo = "fa fa-key"
            });
            oProfileMenu.Add(new ProfileMenu
            {
                Action = "LO",
                ActTitle = "Logout",
                ActLogo = "fa fa-power-off"
            });
            List<CrossTenantBO> crossTenant = new AdminDA().getCrossTenant(Convert.ToInt32(Session["UserID"]));
               if (crossTenant.Count > 0)
               {
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<ul class=\"nav nav-list\"> ");
                   sb1.Append("<li class=\"active mylabs\"><span class=\"menu-text\">" + "My Labs" + "</span> </a></li>");
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

        protected void lbtnShortCut_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/searchresult?searchkey="+txtSearch.Text,true);             
        }        

        //protected void btnbackward_ServerClick(object sender, EventArgs e)
        //{
        //    string dt = "";
        //    if (currmonth==0)
        //    {
        //        currmonth = currmonth - 1;
        //    }
        //    else if(currmonth < 0)
        //    {
        //        currmonth = currmonth - 1;
        //    }
        //    else
        //    {
        //        currmonth = currmonth - 1;
        //    }
        //    dt = DateTime.Now.AddMonths(currmonth).ToString("MMMM yyyy");
        //    spanActMonth.InnerHtml = "<strong>Previous Month: " + dt + "</strong>";
        //}
        //protected void btnForward_ServerClick(object sender, EventArgs e)
        //{
        //    string dt = "";
        //    if (currmonth == 0)
        //    {
        //        currmonth = currmonth + 1;
        //    }
        //    else if (currmonth > 0)
        //    {
        //        currmonth = currmonth + 1;
        //    }
        //    else
        //    {
        //        currmonth = currmonth + 1;
        //    }
        //    dt = DateTime.Now.AddMonths(currmonth).ToString("MMMM yyyy");
        //    spanActMonth.InnerHtml = "<strong>Next Month: " + dt + "</strong>";
        //}
    }
}
