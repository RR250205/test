using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.Web.Script.Serialization;

namespace STOMS.UI.pages
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false)
            {
                Session["PgContentTitle"] = "Dashboard";
                popSpecimenStats(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["ActYear"]));
            }
        }

        private void popSpecimenStats(int TenantID, int Year)
        {
            List<LineChartByMonthBO> cbo = (new ChartDA()).getSpecimenStatsByWeek( TenantID, Year);        
                 
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(cbo);
            hSpecimenStats.Value = json;

            //if (cbo.Count > 0)
            //{
            //    List<int> totalMonths = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //    List<LineChartByMonthBO> cpy = new List<LineChartByMonthBO>();
            //    for (int j = 1; j <= cbo.Count; j++)
            //    {
            //        if (cbo[j - 1].Month != totalMonths[j-1])
            //        {
            //            // notIn = i;
            //            cpy.Add(new LineChartByMonthBO { Month = j, Count= 0 });
            //            //cpy.Insert(j, new LineChartByMonthBO { Month = totalMonths[j - 1], Count = 0 });
            //        }
            //        else
            //        {
            //            cpy.Add(new LineChartByMonthBO { Month = cbo[j-1].Month, Count = cbo[j-1].Count });
            //            //cpy.Insert(j, new LineChartByMonthBO { Month = totalMonths[j - 1], Count = 0 });
            //        }
            //    }
            //    //Actual Months
            //}
        }
    }
}
