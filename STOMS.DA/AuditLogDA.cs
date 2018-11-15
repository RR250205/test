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
    public class AuditLogDA
    {
        private string _ErrMsg;
        private string strErrorMsg;

        public void SaveSpecimenAuditLog(AuditLogBO audit)
        {
            //int rtnSaveSpecimen = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                 SqlCommand sqlComm = new SqlCommand("sspSaveActionAuditLog", sqlCon);
                    sqlCon.Open();
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add(new SqlParameter("@EntityType", audit.EntityType));
                    sqlComm.Parameters.Add(new SqlParameter("@ActionBy", audit.ActionBy));
                    sqlComm.Parameters.Add(new SqlParameter("@EntityID", audit.EntityID));
                    sqlComm.Parameters.Add(new SqlParameter("@ActionName", audit.ActionName));
                     sqlComm.ExecuteNonQuery();
                    sqlCon.Close();
                }

            }
            catch(Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }

            //return rtnSaveSpecimen;


        }


        public List<AuditLogBO> getSpecimenAuditLogs(int SpecimenID)
        {
            List<AuditLogBO> audit = new List<AuditLogBO>();

            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from stblActionAuditLog where EntityID=@EntityID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@EntityID",SpecimenID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            audit.Add(new AuditLogBO
                            {
                                EntityType = reader["EntityType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EntityType"]),
                                ActionName = reader["ActionName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ActionName"]),
                                ActionBy = reader["ActionBy"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ActionBy"]),
                                ActionOn = reader["ActionOn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ActionOn"]),
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


            return audit;
        }





    }
}
