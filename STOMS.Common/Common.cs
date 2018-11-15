using System.Configuration;

namespace STOMS.Common
{
    public partial class Constant
    {
        public readonly static string DBConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        public readonly static string LicenseSignature = ConfigurationManager.AppSettings["LicSignature"];
        public readonly static string ProductID = ConfigurationManager.AppSettings["ProductID"];
        //public readonly static string InvoiceTerm = ConfigurationManager.AppSettings["InvTerm"];
        //public readonly static string InvPayInstruction = ConfigurationManager.AppSettings["InvPayInstruction"];

        public static string GetStatusFormat(string sStatus)
        {
            switch (sStatus)
            {
                case "Pending":
                    return "<span class=\"label label-sm bg-blue arrowed arrowed-right\">Pending</span>";
                case "Received":
                    return "<span class=\"label label-sm label-info arrowed arrowed-right\">Received</span>";
                case "Rejected":
                    return "<span class=\"label label-sm label-danger arrowed arrowed-right\">Rejected</span>";
                case "Assigned to Assay":
                    return "<span class=\"label label-sm label-warning arrowed arrowed-right\">Assigned to Assay</span>";
                case "Result Recorded":
                    return "<span class=\"label label-sm label-success arrowed arrowed-right\">Result Recorded</span>";
                case "Result Delivered":
                    return "<span class=\"label label-sm label-purple arrowed arrowed-right\">Result Delivered</span>";
                default:
                    return "<span class=\"label label-sm label-grey arrowed arrowed-right\">None</span>";
            }
        }
        public static string GetAssayStatusFormat(string sStatus)
        {
            switch (sStatus)
            {
                case "Loading Specimen":
                    return "<span class=\"label label-sm label-primary arrowed arrowed-right\">Loading Specimen</span>";
                case "Rejected":
                    return "<span class=\"label label-sm label-danger arrowed arrowed-right\">Rejected</span>";
                case "Test Completed":
                    return "<span class=\"label label-sm label-success arrowed arrowed-right\">Test Completed</span>";
                case "In Testing":
                    return "<span class=\"label label-sm label-warning arrowed arrowed-right\">In Testing</span>";
                case "Current":
                    return "<span class=\"label label-sm label-primary arrowed arrowed-right\">Current</span>";
                case "Ready for Testing":
                    return "<span class=\"label label-sm label-info  arrowed arrowed-right\">Ready for Testing</span>";
                default:
                    return "<span class=\"label label-sm label-grey arrowed arrowed-right\">None</span>";
            }
        }

        //Assigned to Assay
        //Result Recorded
        public static string GetToUpdateSpecimentStatus(string CurrSpecimenStatus)
        {
            switch (CurrSpecimenStatus.ToUpper())
            {
                case "RECEIVED":
                    return "Add to Assay";
                case "ASSIGNED TO ASSAY":
                    return "Save Results";
                case "RESULT RECORDED":
                    return "Generate Result Report";
                default:
                    return "Request Detail Info";
            }
        }
    }



    public static class Global
    {
        private static string _UserName = "";
        private static int _UserID = 0;
        private static string _ErrorMsg = "";
        private static int _TenantID = 0;
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public static int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public static int TenantID
        {
            get { return _TenantID; }
            set { _TenantID = value; }
        }

        public static string ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }
    }
}
