using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;
using System.Text;
using STOMS.Common;

namespace STOMS.UI.usercontrol
{
    public partial class Menubar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                    
            if (!this.IsPostBack)
            {
                if (Session["serviceID"] != null)
                {
                    genMenu(Convert.ToInt32(Session["serviceID"]));
                }
                else
                {
                    genMenu(0);
                }
            }
        }

        private void genMenu(int srvID)
        {
            KoAccess objMnu = new KoAccess(STOMS.Common.Constant.DBConnectionString);
            int parentID = -1;
            int childID = srvID;
            do
            {
                parentID = objMnu.GetServiceParentID(childID.ToString(),Convert.ToInt32(Constant.ProductID));
                if (parentID == 0)
                    break;
                else
                {
                    childID = parentID;
                }
            } while (parentID != 0);

            parentID = childID;
            List<EntitleServiceBO> objSrv = objMnu.GetUserAccessModulesOnly(Session["UserID"].ToString(), Convert.ToInt32(Constant.ProductID));
            if (objSrv.Count > 0)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<ul class=\"nav nav-list\">");

                for (int idx = 0; idx < objSrv.Count; idx++)
                {
                    if (objSrv[idx].isChild)
                    {
                           

                        sb1 = RecrusiveMenuString(sb1, objSrv[idx].ServiceID, objSrv[idx].MenuIcon, objSrv[idx].ServiceName, parentID, srvID);

                    }
                    else
                    {
                    if (objSrv[idx].ServiceID == srvID)
                        sb1.Append("<li class=\"active\"><a href=\"javascript:getPage(" + objSrv[idx].ServiceID.ToString() + ");\"><i class=\"" + objSrv[idx].MenuIcon + "\"></i><span class=\"menu-text\">" + objSrv[idx].ServiceName + "</span> </a></li>");
                    else
                        sb1.Append("<li><a href=\"javascript:getPage(" + objSrv[idx].ServiceID.ToString() + ");\"><i class=\"" + objSrv[idx].MenuIcon + "\"></i><span class=\"menu-text\">" + objSrv[idx].ServiceName + "</span> </a></li>");

                    //    if (objSrv[idx].ServiceID == parentID)
                    //    {


                    //    sb1.Append("<li class=\"active open\"><a href=\"#\" class=\"dropdown-toggle\"><i class=\"" + objSrv[idx].MenuIcon + "\"></i><span class=\"menu-text\">" + objSrv[idx].ServiceName + "</span>");
                    //        parentID = objMnu.GetServiceParentID(objSrv[idx].ServiceID.ToString(),Convert.ToInt32(Constant.ProductID));
                    //    }
                    //    else
                    //    {
                    //        if (objSrv[idx].ServiceID == srvID)
                    //            sb1.Append("<li class=\"active\"><a href=\"javascript:getPage(" + objSrv[idx].ServiceID.ToString() + ");\"><i class=\"" + objSrv[idx].MenuIcon + "\"></i><span class=\"menu-text\">" + objSrv[idx].ServiceName + "</span> </a></li>");
                    //        else
                    //            sb1.Append("<li><a href=\"javascript:getPage(" + objSrv[idx].ServiceID.ToString() + ");\"><i class=\"" + objSrv[idx].MenuIcon + "\"></i><span class=\"menu-text\">" + objSrv[idx].ServiceName + "</span> </a></li>");

                    //}
                }
            }
                sb1.Append("</ul>");
                ltrstomMenu.Text = sb1.ToString();
            }
        }

        private StringBuilder RecrusiveMenuString(StringBuilder sb1, int ServiceID, string MenuIcon, string ServiceName, int parentID, int clickedSrv)
        {
            KoAccess objMnu = new KoAccess(STOMS.Common.Constant.DBConnectionString);
            List<EntitleServiceBO> objSMnu = objMnu.GetUserAccessModuleServices(Session["UserID"].ToString(), ServiceID,Convert.ToInt32(Constant.ProductID));
            if (ServiceID == parentID)
                sb1.Append("<li class=\"active open\"><a href=\"#\" class=\"dropdown-toggle\"><i class=\"" + MenuIcon + "\"></i><span class=\"menu-text\">" + ServiceName + "</span>");
            else
                sb1.Append("<li><a href=\"#\" class=\"dropdown-toggle\"><i class=\"" + MenuIcon + "\"></i><span class=\"menu-text\">" + ServiceName + "</span>");
            sb1.Append("<b class=\"arrow icon-angle-down\"></b></a>");
            sb1.Append("<ul class=\"submenu\">");
            for (int jdx = 0; jdx < objSMnu.Count; jdx++)
            {
                if (objSMnu[jdx].isChild)
                {
                    sb1 = RecrusiveMenuString(sb1, objSMnu[jdx].ServiceID, objSMnu[jdx].MenuIcon, objSMnu[jdx].ServiceName, parentID, clickedSrv);
                }
                else
                {
                    if (objSMnu[jdx].ServiceID == clickedSrv)
                        sb1.Append("<li class=\"active\"><a href=\"javascript:getPage(" + objSMnu[jdx].ServiceID.ToString() + ");\"><i class=\"icon-double-angle-right\"></i>" + objSMnu[jdx].ServiceName + "</a></li>");
                    else
                        sb1.Append("<li><a href=\"javascript:getPage(" + objSMnu[jdx].ServiceID.ToString() + ");\">" + objSMnu[jdx].ServiceName + "</a></li>");
                }
            }

            sb1.Append("</ul></li>");
            return sb1;


        }

        }
    }
