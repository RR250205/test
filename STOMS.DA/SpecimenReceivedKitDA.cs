using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using STOMS.BO;
using STOMS.Common;

namespace STOMS.DA
{
   public  class SpecimenReceivedKitDA
    {
        public KitOrderBO getSpecimenReceivedKitDetails(int TenantID,string KitNumber)
        {
            KitOrderBO kitordBO = new KitOrderBO();
            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from stblMasterKit where TenantID = @TenantID and KitNumber = @KitNumber", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@KitNumber", KitNumber));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            kitordBO.KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]);
                            kitordBO.KitNumber = reader["KitNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitNumber"]);
                            kitordBO.KitType = reader["KitType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitType"]);
                            kitordBO.ReUseCount = reader["ReUseCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReUseCount"]);
                            kitordBO.Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]);
                            kitordBO.DestroyedOn = reader["DestroyedOn"] == DBNull.Value ? DateTime.Now: Convert.ToDateTime(reader["DestroyedOn"]);
                            kitordBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);                            
                        }
                    }
                    reader.Close();
                    con.Close();
                }                
            }
            return kitordBO;
        }
        public void UpdateStausandReuseCount(int TenantID,string KitNumber,string Status,int ReUseCount)
        {
            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblMasterKit set Status = @Status,ReUseCount = @ReUseCount where TenantID = @TenantID and KitNumber = @KitNumber", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@KitNumber", KitNumber));
                    cmd.Parameters.Add(new SqlParameter("@Status", Status));
                    cmd.Parameters.Add(new SqlParameter("@ReUseCount", ReUseCount));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void UpdateDestroyStaus(int TenantID, string KitNumber, string Status,DateTime DestroyedOn)
        {
            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblMasterKit set Status = @Status,DestroyedOn = @DestroyedOn where TenantID = @TenantID and KitNumber = @KitNumber", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@KitNumber", KitNumber));
                    cmd.Parameters.Add(new SqlParameter("@Status", Status));
                    cmd.Parameters.Add(new SqlParameter("@DestroyedOn", DestroyedOn));
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
