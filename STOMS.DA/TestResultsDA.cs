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
    public class TestResultsDA
    {
        private string strErrorMsg;
        SqlConnection sqlCon = null;

        public int saveResults(TestResultsBO testBO)
        {
            int resID=0;
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sspSaveResults", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@ResultID", testBO.ResultID));
                        command.Parameters.Add(new SqlParameter("@SpecimenID", testBO.SpecimenID));
                        command.Parameters.Add(new SqlParameter("@AssayID", testBO.AssayID));
                        command.Parameters.Add(new SqlParameter("@TotalBuccalProteinyield", testBO.TotalBuccalProteinyield));
                        command.Parameters.Add(new SqlParameter("@CitrateSynthase", testBO.CitrateSynthase));
                        command.Parameters.Add(new SqlParameter("@RC_IV", testBO.RC_IV));
                        command.Parameters.Add(new SqlParameter("@RC_I", testBO.RC_I));
                        command.Parameters.Add(new SqlParameter("@analysisReveals", testBO.analysisReveals));
                        command.Parameters.Add(new SqlParameter("@Interpretation", testBO.Interpretation));
                        command.Parameters.Add(new SqlParameter("@Notes", testBO.Notes));
                        command.Parameters.Add(new SqlParameter("@TenantID", testBO.TenantID));
                        command.Parameters.Add(new SqlParameter("@PerformedBy", testBO.PerformedBy));
                        command.Parameters.Add(new SqlParameter("@ResultDocID", testBO.ResultDocID));
                        command.Parameters.Add(new SqlParameter("@IsReleased", testBO.IsReleased));
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader read = command.ExecuteReader();

                        if(read.HasRows)
                        {
                            while(read.Read())
                            {
                                resID = Convert.ToInt32(read[0]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
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
            return resID;
        }

        public TestResultsBO getResults(int TenantID,int SpecimenID)
        {
            TestResultsBO resBO = new TestResultsBO();
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("select * from stblTestResults where TenantID = @TenantID and SpecimenID = @SpecimenID", connect))
                    {
                        connect.Open();                        
                        command.Parameters.Add(new SqlParameter("@SpecimenID",SpecimenID));                        
                        command.Parameters.Add(new SqlParameter("@TenantID",TenantID));
                        command.CommandType = CommandType.Text;

                        SqlDataReader read = command.ExecuteReader();

                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                resBO.ResultID = read["ResultID"] == DBNull.Value ? 0 : Convert.ToInt32(read["ResultID"]);
                                resBO.SpecimenID = read["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(read["SpecimenID"]);
                                resBO.AssayID = read["AssayID"] == DBNull.Value ? 0 : Convert.ToInt32(read["AssayID"]);
                                resBO.TotalBuccalProteinyield = read["TotalBuccalProteinyield"] == DBNull.Value ? string.Empty : Convert.ToString(read["TotalBuccalProteinyield"]);
                                resBO.CitrateSynthase = read["CitrateSynthase"] == DBNull.Value ? string.Empty : Convert.ToString(read["CitrateSynthase"]);
                                resBO.RC_IV = read["RC_IV"] == DBNull.Value ? string.Empty : Convert.ToString(read["RC_IV"]);
                                resBO.RC_I = read["RC_I"] == DBNull.Value ? string.Empty : Convert.ToString(read["RC_I"]);
                                resBO.analysisReveals = read["analysisReveals"] == DBNull.Value ? string.Empty : Convert.ToString(read["analysisReveals"]);
                                resBO.Interpretation = read["Interpretation"] == DBNull.Value ? string.Empty : Convert.ToString(read["Interpretation"]);
                                resBO.Notes = read["Notes"] == DBNull.Value ? string.Empty : Convert.ToString(read["Notes"]);  
                                resBO.PerformedBy= read["PerformedBy"] == DBNull.Value ? string.Empty : Convert.ToString(read["PerformedBy"]);
                                resBO.ResultDocID= read["ResultDocID"] == DBNull.Value ? 0 : Convert.ToInt32(read["ResultDocID"]);
                                resBO.IsReleased = read["IsReleased"] == DBNull.Value ? false : Convert.ToBoolean(read["IsReleased"]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
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
            return resBO;
        }

        public TestResultsBO getResults(int TenantID, int SpecimenID,int ResultID)
        {
            TestResultsBO resBO = new TestResultsBO();
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("select * from stblTestResults where TenantID = @TenantID and SpecimenID = @SpecimenID and ResultID=@ResultID", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));
                        command.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        command.Parameters.Add(new SqlParameter("@ResultID", ResultID));
                        command.CommandType = CommandType.Text;

                        SqlDataReader read = command.ExecuteReader();

                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                resBO.ResultID = read["ResultID"] == DBNull.Value ? 0 : Convert.ToInt32(read["ResultID"]);
                                resBO.SpecimenID = read["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(read["SpecimenID"]);
                                resBO.AssayID = read["AssayID"] == DBNull.Value ? 0 : Convert.ToInt32(read["AssayID"]);
                                resBO.TotalBuccalProteinyield = read["TotalBuccalProteinyield"] == DBNull.Value ? string.Empty : Convert.ToString(read["TotalBuccalProteinyield"]);
                                resBO.CitrateSynthase = read["CitrateSynthase"] == DBNull.Value ? string.Empty : Convert.ToString(read["CitrateSynthase"]);
                                resBO.RC_IV = read["RC_IV"] == DBNull.Value ? string.Empty : Convert.ToString(read["RC_IV"]);
                                resBO.RC_I = read["RC_I"] == DBNull.Value ? string.Empty : Convert.ToString(read["RC_I"]);
                                resBO.analysisReveals = read["analysisReveals"] == DBNull.Value ? string.Empty : Convert.ToString(read["analysisReveals"]);
                                resBO.Interpretation = read["Interpretation"] == DBNull.Value ? string.Empty : Convert.ToString(read["Interpretation"]);
                                resBO.Notes = read["Notes"] == DBNull.Value ? string.Empty : Convert.ToString(read["Notes"]);
                                resBO.PerformedBy = read["PerformedBy"] == DBNull.Value ? string.Empty : Convert.ToString(read["PerformedBy"]);
                                resBO.IsReleased = read["IsReleased"] == DBNull.Value ? false : Convert.ToBoolean(read["IsReleased"]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
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
            return resBO;
        }

        public int UpdateResults(TestResultsBO testResBO)
        {
            int resID = 0;
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sspSaveResults", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@ResultID", testResBO.ResultID));
                        command.Parameters.Add(new SqlParameter("@SpecimenID", testResBO.SpecimenID));
                        command.Parameters.Add(new SqlParameter("@AssayID", testResBO.AssayID));
                        command.Parameters.Add(new SqlParameter("@TotalBuccalProteinyield", testResBO.TotalBuccalProteinyield));
                        command.Parameters.Add(new SqlParameter("@CitrateSynthase", testResBO.CitrateSynthase));
                        command.Parameters.Add(new SqlParameter("@RC_IV", testResBO.RC_IV));
                        command.Parameters.Add(new SqlParameter("@RC_I", testResBO.RC_I));
                        command.Parameters.Add(new SqlParameter("@analysisReveals", testResBO.analysisReveals));
                        command.Parameters.Add(new SqlParameter("@Interpretation", testResBO.Interpretation));
                        command.Parameters.Add(new SqlParameter("@Notes", testResBO.Notes));
                        command.Parameters.Add(new SqlParameter("@TenantID", testResBO.TenantID));
                        command.Parameters.Add(new SqlParameter("@PerformedBy", testResBO.PerformedBy));
                        command.Parameters.Add(new SqlParameter("@ResultDocID", testResBO.ResultDocID));
                        command.Parameters.Add(new SqlParameter("@IsReleased", testResBO.IsReleased));
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader read = command.ExecuteReader();

                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                resID = Convert.ToInt32(read[0]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
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
            return resID;
        }       
    }
}
