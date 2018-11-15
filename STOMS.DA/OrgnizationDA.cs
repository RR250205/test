using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STOMS.BO;

using STOMS.Common;
using System.Data;
using System.Data.SqlClient;



namespace STOMS.DA
{
   public class OrgnizationDA
    {
        private string _connectionString;
        private string strErrorMsg;

        public int SaveOrgnization(TenantDetailsBO Orgnization)
        {
            int orgni = 0;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveOrgnization", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantRegNo", Orgnization.OrgRegNo));
                        sqlCmd.Parameters.Add(new SqlParameter("@Address1", Orgnization.Address1));
                        sqlCmd.Parameters.Add(new SqlParameter("@PrimaryPhone", Orgnization.PrimaryPhone));
                        sqlCmd.Parameters.Add(new SqlParameter("@Country", Orgnization.Country));
                        sqlCmd.Parameters.Add(new SqlParameter("@AlternatePhone", Orgnization.AlternatePhone));
                        sqlCmd.Parameters.Add(new SqlParameter("@Email", Orgnization.Email));
                        sqlCmd.Parameters.Add(new SqlParameter("@Fax", Orgnization.Fax));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", Orgnization.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@Logo", Orgnization.Logo));

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        reader.Close();
                        reader = null;
                       
                    }
                    sqlCon.Close();
                }        
            
            } 

            catch(Exception ex)
            {
                strErrorMsg = ex.Message;

            throw ;
            }

            return orgni;
        }
        public List<TenantDetailsBO> getOrgnization(int TenantID)
        {
            List<TenantDetailsBO> vieworg = new List<TenantDetailsBO>();

            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblTenantProfile where TenantID=@TenantID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    //sqlCmd.Parameters.Add(new SqlParameter("@TenantRegNo", TenantRegNo));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            vieworg.Add(new TenantDetailsBO
                            {
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                PrimaryPhone = reader["PrimaryPhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PrimaryPhone"]),
                                AlternatePhone = reader["AlternatePhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AlternatePhone"]),
                                //OrgRegNo = reader["OrgRegNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrgRegNo"]),
                                Logo = reader["Logo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Logo"]),
                                TenantRegNo = reader["TenantRegNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantRegNo"]),
                                Fax = reader["Fax"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Fax"])
                            });

                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }            
                return vieworg;
        }


    }
}
