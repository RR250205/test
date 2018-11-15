using STOMS.BO;
using STOMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;



namespace STOMS.DA
{
    public class ReportDA
    {
        private string strErrorMsg;
        public List<DashboardStatBO> getDashboardStat(int OrgID, int ActivityMonth, int ActivityYear)
        {
            List<DashboardStatBO> objDash = new List<DashboardStatBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspGetTenantDashboardStats", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@OrgID", OrgID));
                        sqlCmd.Parameters.Add(new SqlParameter("@ActivityMonth", ActivityMonth));
                        sqlCmd.Parameters.Add(new SqlParameter("@ActivityYear", ActivityYear));

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            objDash.Add(new DashboardStatBO
                            {
                                TotalOrders = reader["TotalOrders"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalOrders"]),
                                SampleTesting = reader["TestInProgress"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TestInProgress"]),
                                ReceivedSpecimens = reader["ReceivedSpecimens"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReceivedSpecimens"]),
                                ReadyforAssaySpecimes = reader["ReadyforAssaySpecimens"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReadyforAssaySpecimens"]),
                                AssigntoAssaySpecimens = reader["AssigntoAssaySpecimens"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssigntoAssaySpecimens"]),
                                ClientCount = reader["ClientCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClientCount"]),
                                OutStandingInv = reader["OutStandingInv"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OutStandingInv"])
                            });
                        }
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return objDash;
        }

        public string GenerateResultReport(int AssaySpecimenID, int TenentID, string DocType, int CreatedBy)
        {
            DocumentBO documentBO = new DocumentBO();
            string rtnDocNumber = "";
            //SpecimenDA oSpec = new SpecimenDA();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspGenerateReport", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@AssaySpecimenID", AssaySpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenentID));
                        sqlCmd.Parameters.Add(new SqlParameter("@DocType", DocType));
                        sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            rtnDocNumber = reader[0] == DBNull.Value ? String.Empty : Convert.ToString(reader[0]); 
                        }
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return rtnDocNumber;
        }

        public DocumentBO ViewhardCopy(int ReqFormCopyID)
        {
            DocumentBO documentBO = new DocumentBO();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlcom = new SqlCommand("Select * from stblDocuments where DocID= @ReqFormCopyID", sqlcon))
                    {
                        sqlcon.Open();
                        sqlcom.CommandType = CommandType.Text;
                        
                        sqlcom.Parameters.Add(new SqlParameter("@ReqFormCopyID", ReqFormCopyID));
                        //sqlcom.Parameters.Add(new SqlParameter("@DocNumber", DocNumber));

                        SqlDataReader reader = sqlcom.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documentBO.DocID = reader["DocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DocID"]);
                                documentBO.DocNumber = reader["DocNumber"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocNumber"]);
                                documentBO.DocType = reader["DocType"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocType"]);
                            }
                        }
                        reader.Close();
                        sqlcon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return documentBO;
        }

        public void updateReqFormCopyID(int TenantId, string docId, string specimenId)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("update stblSpecimenInfo set ReqFormCopyID=@DocID where TenantID=@TenantID and SpecimenID=@SpecimenID", sqlcon))
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantId));
                        sqlCmd.Parameters.Add(new SqlParameter("@DocID", docId));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", specimenId));
                        sqlCmd.CommandType = CommandType.Text;
                        sqlcon.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlcon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
        }
    }
}
