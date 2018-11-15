using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace STOMS.UI
{
    public class Global : System.Web.HttpApplication
    {

        string glError = string.Empty;
        void Application_Start(object sender, EventArgs e)
        {
          try
          {
            RegisterRoutes(RouteTable.Routes);
          }
          catch (Exception ex)
          {
            glError = ex.Message;
          }
        }

        public void RegisterRoutes(RouteCollection routers)
        {
            #region General
           // routers.MapPageRoute("Login", "Category/{action}/{categoryName}", "~/index.aspx");
           // routers.MapPageRoute("Login", "Login", "~/index.aspx");
            routers.MapPageRoute("Index", "", "~/index.aspx");
            routers.MapPageRoute("DashBoard", "pages/dashboard", "~/pages/home.aspx");
            routers.MapPageRoute("Receiving", "pages/receiving", "~/pages/Specimen.aspx");
            routers.MapPageRoute("GenNumbers", "pages/GenNumbers", "~/pages/GenSpecimenNumber.aspx");
            routers.MapPageRoute("Assay", "pages/assay", "~/pages/AssayMgmt.aspx");
            routers.MapPageRoute("AssayDet", "pages/assaydetail", "~/pages/AssayDetail.aspx");
            routers.MapPageRoute("Kit Tracking", "pages/kitTracking", "~/pages/CourierTracking.aspx");
            routers.MapPageRoute("Order", "pages/order", "~/pages/order.aspx");
            routers.MapPageRoute("OrderMgmt", "pages/mgmtOrder", "~/pages/orderMgmt.aspx");
            routers.MapPageRoute("OrderInfo", "pages/OrderDetail", "~/pages/OrderInfo.aspx");
            routers.MapPageRoute("Report", "report/rpts", "~/reports/ReportList.aspx");
            routers.MapPageRoute("Invoice", "pages/invoice", "~/pages/invoice.aspx");
            routers.MapPageRoute("ShowInvoice", "pages/invdet", "~/pages/showinvoice.aspx");
            routers.MapPageRoute("PrintInvoice", "pages/printinv/{invno}", "~/pages/printinvoice.aspx");
            routers.MapPageRoute("TestTrack", "pages/tt", "~/pages/TestTracking.aspx");
            routers.MapPageRoute("SampleTrack", "pages/st", "~/pages/SampleTracking.aspx");
            routers.MapPageRoute("SpecimenView", "pages/Specimen", "~/pages/SpecimenView.aspx");
            routers.MapPageRoute("Customer", "admin/cust", "~/admin/Cust.aspx");
            routers.MapPageRoute("Admin", "pages/admin", "~/admin/admin.aspx");
            routers.MapPageRoute("Search", "pages/searchresult", "~/pages/search.aspx");
            routers.MapPageRoute("OrgProfile", "pages/Orgprofile", "~/pages/OrgProfile.aspx");
            routers.MapPageRoute("Login", "login", "~/index.aspx");
            //routers.MapPageRoute("Support", "pages/Support", "~/pages/support.aspx");
            routers.MapPageRoute("sorry", "sorry", "~/oops.aspx");
            routers.MapPageRoute("NoPermission", "np", "~/NoPerm.aspx");
            routers.MapPageRoute("New Request Form", "pages/New Request Form", "~/pages/kitTracking.aspx");
            routers.MapPageRoute("Exceptions", "pages/Exceptions", "~/pages/Exceptions.aspx");
            routers.MapPageRoute("ResultRecording", "pages/ResultRecording", "~/pages/Results/ResultRecording.aspx");
            routers.MapPageRoute("Insuranceagent", "pages/Insuranceagent", "~/pages/Insuranceagent.aspx");
            routers.MapPageRoute("ResultList", "pages/ResultList", "~/pages/ResultList.aspx");
            routers.MapPageRoute("PhyOrderList", "Pages/PhyOrderList", "~/pages/PhysicionOrderList.aspx");
            routers.MapPageRoute("PhyOrderView", "Pages/PhysicionOrderview", "~/pages/PhysicionOrderview.aspx");
            routers.MapPageRoute("Insurance", "Pages/ListInsurance", "~/Pages/ListInsurance.aspx");
            routers.MapPageRoute("Insuranceview", "Pages/InsuranceListView", "~/Pages/InsuranceListView.aspx");
            routers.MapPageRoute("ListInsurance", "Pages/ListInsurance", "~/Pages/ListInsurance.aspx");
            routers.MapPageRoute("ListInsuranceAdd", "Pages/InsuranceAdd", "~/Pages/InsuranceAddnew.aspx"); 
            routers.MapPageRoute("InProgressPage", "Pages/InProgress", "~/Pages/InProgress.aspx");

            // routers.MapPageRoute("SpecimenView","pages/SpecimenView", "~/pages/SpecimenView.aspx");
            #endregion

            #region Administration & my pages
            //routers.MapPageRoute("UserAdmin", "admin/UserAcct", "~/admin/UserAdmin.aspx");
            //routers.MapPageRoute("Login", "Login", "~/index.aspx");
            routers.MapPageRoute("mysetting", "pages/mysettings", "~/pages/mysettings.aspx");
            routers.MapPageRoute("myprofile", "pages/myprofile/{type}", "~/pages/myprofile.aspx");
            routers.MapPageRoute("Companyprofile", "pages/subscription", "~/admin/pages/CompanyProfile.aspx");
            routers.MapPageRoute("MyEntities", "pages/entity", "~/pages/MyEntities.aspx");
            routers.MapPageRoute("Token", "VerifyToken/{Token}", "~/pages/Results/verifyToken.aspx");
           // routers.MapPageRoute("Token", "pages/VerifyToken", "~/pages/Results/verifyToken.aspx");

            #endregion

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
         {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
