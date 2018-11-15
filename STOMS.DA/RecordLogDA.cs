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
    public class RecordLogDA
    {
        public string strErrorMsg;
        public int SaveRecordLog(RecordLogBO obReco)
        {

            int rtnval = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("spCRMsaveRecordLog", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@RecLogID", obReco.RecLogID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", obReco.SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@PhysicianName", obReco.PhysicianName));

                        sqlCmd.Parameters.Add(new SqlParameter("@PatientName", obReco.PatientName));
                        sqlCmd.Parameters.Add(new SqlParameter("@collectedOn", obReco.collectedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@ReportedOn", obReco.ReceivedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@Temperature", obReco.Temperature));
                        sqlCmd.Parameters.Add(new SqlParameter("@TransitTime", obReco.TransitTime));
                        sqlCmd.Parameters.Add(new SqlParameter("@TestRequested", obReco.TestRequested));
                        sqlCmd.Parameters.Add(new SqlParameter("@VolumeReceived", obReco.VolumeReceived));
                        sqlCmd.Parameters.Add(new SqlParameter("@PotInterSubstances", obReco.PotInterSubstances));
                        sqlCmd.Parameters.Add(new SqlParameter("@ConsentProvided", obReco.ConsentProvided));
                        sqlCmd.Parameters.Add(new SqlParameter("@RequisitionComplete", obReco.RequisitionComplete));
                        sqlCmd.Parameters.Add(new SqlParameter("@AcceptableTest", obReco.AcceptableTest));
                        sqlCmd.Parameters.Add(new SqlParameter("@notAcceptReason", obReco.notAcceptReason));
                        
                       
                        
                        sqlCmd.Parameters.Add(new SqlParameter("@TestedOn", obReco.TestedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@VolumeRemaining", obReco.VolumeRemaining));
                        sqlCmd.Parameters.Add(new SqlParameter("@ReportedOn", obReco.ReportedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@TurnAroundTime", obReco.TurnAroundTime));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnval = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
            return rtnval;
        }
        public List<RecordLogBO> getRecordLog()
        {
            List<RecordLogBO> obRec = new List<RecordLogBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwOrder", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obRec.Add(new RecordLogBO
                                {
                                    RecLogID = reader["RecLogID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RecLogID"]),
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenID"]),

                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    PhysicianName = reader["PhysicianName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PhysicianName"]),
                                    collectedOn = reader["collectedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["collectedOn"]),
                                    ReceivedOn = reader["ReceivedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReceivedOn"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    Temperature = reader["Temperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Temperature"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    TestRequested = reader["TestRequested"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestRequested"]),
                                    PotInterSubstances = reader["PotInterSubstances"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PotInterSubstances"]),

                                    ConsentProvided = reader["ConsentProvided"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConsentProvided"]),
                                    RequisitionComplete = reader["RequisitionComplete"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequisitionComplete"]),
                                    AcceptableTest = reader["AcceptableTest"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AcceptableTest"]),
                                    notAcceptReason = reader["notAcceptReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["notAcceptReason"]),
                                    TestedOn = reader["TestedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestedOn"]),
                                    VolumeRemaining = reader["VolumeRemaining"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeRemaining"]),
                                    ReportedOn = reader["ReportedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReportedOn"]),
                                    TurnAroundTime = reader["TurnAroundTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TurnAroundTime"]),

                                });
                            }
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
            return obRec;
        }

        public List<RecordLogBO> getOrder(string OrderID)
        {
            List<RecordLogBO> obRec = new List<RecordLogBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwOrder where OrderID = " + OrderID, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obRec.Add(new RecordLogBO
                                {
                                    RecLogID = reader["RecLogID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RecLogID"]),
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenID"]),

                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    PhysicianName = reader["PhysicianName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PhysicianName"]),
                                    collectedOn = reader["collectedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["collectedOn"]),
                                    ReceivedOn = reader["ReceivedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReceivedOn"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    Temperature = reader["Temperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Temperature"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    TestRequested = reader["TestRequested"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestRequested"]),
                                    PotInterSubstances = reader["PotInterSubstances"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PotInterSubstances"]),

                                    ConsentProvided = reader["ConsentProvided"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConsentProvided"]),
                                    RequisitionComplete = reader["RequisitionComplete"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequisitionComplete"]),
                                    AcceptableTest = reader["AcceptableTest"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AcceptableTest"]),
                                    notAcceptReason = reader["notAcceptReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["notAcceptReason"]),
                                    TestedOn = reader["TestedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestedOn"]),
                                    VolumeRemaining = reader["VolumeRemaining"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeRemaining"]),
                                    ReportedOn = reader["ReportedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReportedOn"]),
                                    TurnAroundTime = reader["TurnAroundTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TurnAroundTime"]),
                                   
                                });
                            }
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
            return obRec;
        }

    }
   
}
