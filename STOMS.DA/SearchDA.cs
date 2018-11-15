using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STOMS.BO;
using STOMS.Common;
using System.Data.SqlClient;
using System.Data;

namespace STOMS.DA
{
    public class SearchDA
    {
        private string _Errmsg;

        public List<SearchBO> GetSearchValues(string SearchText,int ProfileID, int TenantID, bool IsConsolidate=true)
        {
            List<SearchBO> searBO = new List<SearchBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("spSearchResult", sqlCon))
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SearchText));
                        sqlCmd.Parameters.Add(new SqlParameter("@ProfileID", ProfileID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@IsConsolidate", IsConsolidate));                        
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                searBO.Add(new SearchBO
                                {                                   
                                    MainTitle = reader["MainTitle"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MainTitle"]),                                    
                                    Sub1 = reader["Sub1"] == DBNull.Value ? string.Empty : Convert.ToString(Convert.ToDateTime(reader["Sub1"]).ToShortDateString()),
                                    Sub2 = reader["Sub2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Sub2"]),
                                    Sub3 = reader["Sub3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Sub3"]),
                                    Sub4 = reader["Sub4"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Sub4"]),
                                });
                            }
                        }
                        reader.Close();
                        reader = null;
                        sqlCon.Close();
                    }                    
                }
            }
            catch (Exception ex)
            {
                _Errmsg = ex.Message;
                throw;
            }
            return searBO;
        }

        public SearchProfileBO GetSearchProfileDetails(SearchProfileBO searchProfileBO)
        {
            SearchProfileBO searBO = new SearchProfileBO();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from tblSearchProfile where TenantID = @TenantID and SeaProfileName = @SearchProfileName", sqlCon))
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", searchProfileBO.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SearchProfileName", searchProfileBO.SeaProfileName));                        
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                searBO.SeaProfileID = reader["SeaProfileID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SeaProfileID"]);
                                searBO.SeaProfileName = reader["SeaProfileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SeaProfileName"]);
                                searBO.DBObjectName = reader["DBObjectName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DBObjectName"]);
                                searBO.SeaSQL = reader["SeaSQL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SeaSQL"]);
                                searBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);                                
                            }
                        }
                        reader.Close();
                        reader = null;
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _Errmsg = ex.Message;
                throw;
            }
            return searBO;
        }
    }
}
