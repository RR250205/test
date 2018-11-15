using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STOMS.Common;
using STOMS.BO;
using System.Data;
using System.Data.SqlClient;

namespace STOMS.DA
{
  public  class ChartDA
    {
        string _connectionString = "";
        public ChartDA()
        {
            _connectionString = Constant.DBConnectionString;
        }

        public List<LineChartByMonthBO> getSpecimenStatsByMonth(int TenantID, int Year)
        {
            List<LineChartByMonthBO> rtnBO = new List<LineChartByMonthBO>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select MONTH(CreatedOn)[Month], count(*) as [Count] from stblSpecimenInfo where TenantID = @TenantID and YEAR(CreatedOn) = @Year group by(MONTH(CreatedOn))", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@Year", Year));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            rtnBO.Add(new LineChartByMonthBO
                            {
                                Month = reader["Month"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Month"]),
                                Count = reader["Count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Count"])
                            });
                        }
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return rtnBO;
        }

        public List<LineChartByMonthBO> getSpecimenStatsByWeek(int TenantID, int Year)
        {
            List<LineChartByMonthBO> rtnBO = new List<LineChartByMonthBO>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT datepart(day, datediff(day, 0, CreatedOn) / 7 * 7) / 7 + 1 as [Week],datepart(month, datediff(day, 0, CreatedOn)) as [Month], count(*) as [Count] FROM stblSpecimenInfo WHERE TenantID = @TenantID and YEAR(CreatedOn) = @Year GROUP BY datepart(day, datediff(day, 0, CreatedOn) / 7 * 7) / 7 + 1, datepart(month, datediff(day, 0, CreatedOn)) ORDER BY datepart(month, datediff(day, 0, CreatedOn)), datepart(day, datediff(day, 0, CreatedOn) / 7 * 7) / 7 + 1", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@Year", Year));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rtnBO.Add(new LineChartByMonthBO
                            {
                                Week = reader["Week"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Week"]),
                                Month = reader["Month"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Month"]),
                                Count = reader["Count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Count"])
                            });
                        }
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return rtnBO;
        }
    }
}
