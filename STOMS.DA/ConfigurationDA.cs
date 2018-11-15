using STOMS.BO;
using STOMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using STOMS.DA;

namespace STOMS.DA
{
    public class EmailConfigurationDA
    {
        private string _ErrMsg;
        public List<EmailEnablementTypeBO> getEmailEnablementType(int EmailManagementTypeID = 0)
        {
            List<EmailEnablementTypeBO> emailEnablementTypeBO = new List<EmailEnablementTypeBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        if (EmailManagementTypeID == 0)
                        {
                            sqlCmd.CommandText = "Select * from stblEmailEnablementTypes";
                        }
                        else
                        {
                            sqlCmd.CommandText = "Select * from stblEmailEnablementTypes where EmailEnablementTypeID=@EmailEnablementTypeID";
                            sqlCmd.Parameters.Add(new SqlParameter("@EmailEnablementTypeID", EmailManagementTypeID));
                        }
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    emailEnablementTypeBO.Add(new EmailEnablementTypeBO()
                                    {
                                        EmailEnablementTypeID = reader["EmailEnablementTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementTypeID"]),
                                        EmailEnablementType = reader["Type"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Type"]),
                                        EndUserTemplate = reader["EndUserTemplate"] == DBNull.Value ? String.Empty : Convert.ToString(reader["EndUserTemplate"]),
                                        TenantTemplate = reader["TenantTemplate"] == DBNull.Value ? String.Empty : Convert.ToString(reader["TenantTemplate"]),
                                        Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"]),

                                    });

                                    //emailEnablementBO.Add(new EmailEnablementBO()
                                    //{
                                    //    EmailEnablementID = reader["EmailEnablementID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementID"]),

                                    //    isToEndUser = reader["ToEndUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToEndUser"]),
                                    //    isToTenant = reader["ToEndUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToEndUser"])

                                    //});
                                }
                            }
                            reader.Close();
                            // reader = null;
                            sqlCon.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return emailEnablementTypeBO;
        }
        public EmailEnablementBO getEmailEnablement(int TenantID, int EmailEnablementTypeID)
        {
            EmailEnablementBO emailEnablementBO = new EmailEnablementBO();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("Select * from stblEmailEnablement Where TenantID=@TenantID and EmailEnablementTypeID=@EmailEnablementTypeID", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@EmailEnablementTypeID", EmailEnablementTypeID));
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    emailEnablementBO.EmailEnablementID = reader["EmailEnablementID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementID"]);
                                    emailEnablementBO.isToEndUser = reader["ToEndUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToEndUser"]);
                                    emailEnablementBO.isToTenant = reader["ToTenant"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToTenant"]);
                                    emailEnablementBO.ToTenantEmails = reader["ToTenantEmails"] == DBNull.Value ? String.Empty : Convert.ToString(reader["ToTenantEmails"]);

                                    //emailEnablementBO.Add(new EmailEnablementBO()
                                    //{
                                    //    EmailEnablementID = reader["EmailEnablementID"] == DBNull.Value?0: Convert.ToInt32(reader["EmailEnablementID"]),
                                    //   emailEnablementTypeBO=new EmailEnablementTypeBO() {
                                    //       EmailEnablementTypeID= reader["EmailEnablementtype"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementtype"]),
                                    //       EmailEnablementType=reader["Type"]==DBNull.Value?String.Empty: Convert.ToString(reader["Type"]),
                                    //       EndUserTemplate= reader["EndUserTemplate"] == DBNull.Value ? String.Empty : Convert.ToString(reader["EndUserTemplate"]),


                                    //   },
                                    //   isToEndUser=reader["ToEndUser"]==DBNull.Value?false:Convert.ToBoolean(reader["ToEndUser"]),
                                    //   isToTenant= reader["ToEndUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToEndUser"])

                                    //});
                                }
                            }
                            reader.Close();
                            // reader = null;
                            sqlCon.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return emailEnablementBO;
        }
        public int SaveEmailEnablement(EmailEnablementBO emailEnablementBO, int EmailEnablementTypeID, int TenantID)
        {
            int returnvalue = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspSaveEmailEnablement", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@EmailEnablementID", emailEnablementBO.EmailEnablementID));
                        sqlCmd.Parameters.Add(new SqlParameter("@EmailEnablementTypeID", EmailEnablementTypeID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));

                        sqlCmd.Parameters.Add(new SqlParameter("@ToEnduser", emailEnablementBO.isToEndUser));

                        sqlCmd.Parameters.Add(new SqlParameter("@ToTenant", emailEnablementBO.isToTenant));
                        sqlCmd.Parameters.Add(new SqlParameter("@ToTenantEmails", emailEnablementBO.ToTenantEmails));

                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                returnvalue = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
                _ErrMsg = ex.Message;
                throw;
            }
            return returnvalue;
        }

        public List<EmailEnablementImplementationBO> emailEnableImplementation(int TenantID,int EmailEnablementID)
        {
            List<EmailEnablementImplementationBO> emailEnableImplementationBO = new List<EmailEnablementImplementationBO>();
            using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from svwEmailEnablement where TenantID = @TenantID and EmailEnablementID = @EmailEnablementID", connect);
                {

                    command.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    command.Parameters.Add(new SqlParameter("@EmailEnablementID", EmailEnablementID));
                    command.CommandType = CommandType.Text;
                    connect.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {                            
                            EmailEnablementBO ebo = new EmailEnablementBO();
                            EmailEnablementTypeBO ebto = new EmailEnablementTypeBO();

                            ebo.EmailEnablementID = reader["EmailEnablementID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementID"]);
                            ebo.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                            ebo.isToEndUser = reader["ToEndUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToEndUser"]);
                            ebo.isToTenant = reader["ToTenant"] == DBNull.Value ? false : Convert.ToBoolean(reader["ToTenant"]);
                            ebo.ToTenantEmails = reader["ToTenantEmails"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ToTenantEmails"]);
                            ebto.EmailEnablementTypeID = reader["EmailEnablementTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmailEnablementTypeID"]);
                            ebto.EndUserTemplate = reader["EndUserTemplate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EndUserTemplate"]);
                            ebto.TenantTemplate = reader["TenantTemplate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantTemplate"]);
                            ebto.Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]);

                            emailEnableImplementationBO.Add(new EmailEnablementImplementationBO
                            {
                                emailEnablementBO = ebo,
                                emailEnablementTypeBO = ebto,
                                type = reader["Type"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Type"]),
                            });
                        }
                    }
                    reader.Close();
                    connect.Close();                    
                }
            }
            return emailEnableImplementationBO;
        }

        public List<CourierConfigurationBO> GetCourierType()
        {
            List<CourierConfigurationBO> courierconfigBO = new List<CourierConfigurationBO>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("select * from stblCouriers", sqlCon);
                    {

                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                courierconfigBO.Add(new CourierConfigurationBO
                                {
                                    CourierID = reader["CourierID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierID"]),
                                    CourierName = reader["CourierName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["CourierName"]),
                                });
                            }
                        }

                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return courierconfigBO;
        }


        public List<CourierConfigurationBO> GetCourierTenant(int TenantID)
        {
            List<CourierConfigurationBO> cbo = new List<CourierConfigurationBO>();
            try {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("select * from svwCourierTenant where TenantID=@TenantID", sqlCon);
                    {

                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cbo.Add(new CourierConfigurationBO
                                {
                                    CourierID = reader["CourierID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierID"]),
                                    CourierName = reader["CourierName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["CourierName"]),
                                    TenantID=reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                    CourierTenantID= reader["CourierTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierTenantID"]),
                                });
                            }
                        }

                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }


            return cbo;
        }

        public CourierConfigurationBO GetCourierTenant(int TenantID, int CourierID)
        {
            CourierConfigurationBO displaycourierBO = new CourierConfigurationBO();
            try
            {  
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("select * from stblCourierTenant where TenantID=@TenantID and CourierID=@CourierID", sqlCon);
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierID", CourierID));
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                displaycourierBO.CourierID = reader["CourierID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierID"]);
                                displaycourierBO.CourierTenantID = reader["CourierTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierTenantID"]);
                                displaycourierBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                            }
                        }
                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return displaycourierBO;
        }

        public int SaveCourierConfiguration(CourierConfigBO objCourier)
        {

            int returnvalue = 0;
            try
            { 
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sppCourierConfigur", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierConfigID", objCourier.CourierConfigID));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexAcNo", objCourier.FedexACNo));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexMeterNo", objCourier.FedexMeterNo));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexUserKey", objCourier.FedexUserKey));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexUserPassword", objCourier.FedexUserPassword));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexParentKey", objCourier.FedexParentKey));
                        sqlCmd.Parameters.Add(new SqlParameter("@FedexParentPassword", objCourier.FedexParentPassword));
                        sqlCmd.Parameters.Add(new SqlParameter("@DefaultWeight", objCourier.DefaultWeight));
                        sqlCmd.Parameters.Add(new SqlParameter("@SignatureOn", objCourier.SignatureOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objCourier.TenantID));
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                returnvalue = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
                _ErrMsg = ex.Message;
                throw;
            }
            return returnvalue;
        }

        public CourierConfigBO getcourierdetail(int TenantID)
        {
            CourierConfigBO courierBO = new CourierConfigBO();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("Select * from stblCourierConfig where TenantID=@TenantID ", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        //  sqlCmd.Parameters.Add(new SqlParameter("@CourierConfigID", CourierConfigID));
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    courierBO.CourierConfigID = reader["CourierConfigID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierConfigID"]);
                                    courierBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                                    courierBO.FedexMeterNo = reader["FedexMeterNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FedexMeterNo"]);
                                    courierBO.FedexACNo = reader["FedexACNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FedexACNo"]);
                                    courierBO.FedexParentKey = reader["FedexParentKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FedexParentKey"]);
                                    courierBO.FedexParentPassword = reader["FedexParentPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FedexParentPassword"]);
                                    courierBO.FedexUserKey = reader["FedexUserKey"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FedexUserKey"]);
                                    courierBO.FedexUserPassword = reader["FedexUserPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FedexUserPassword"]);
                                    courierBO.DefaultWeight = reader["DefaultWeight"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DefaultWeight"]);
                                    courierBO.FedexUserPassword = reader["FedexUserPassword"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FedexUserPassword"]);
                                    courierBO.SignatureOn = reader["SignatureOn"] == DBNull.Value ? false : Convert.ToBoolean(reader["SignatureOn"]);
                                }
                            }
                            reader.Close();
                            // reader = null;
                            sqlCon.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return courierBO;
        }

        public void deleteCourierConfig(int TenantID, int CourierID )
        {
            //CourierConfigurationBO deletecourierBO = new CourierConfigurationBO();
            try
            {

                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("delete from stblCourierTenant where CourierID=@CourierID and TenantID=@TenantID ", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierID", CourierID));
                        //sqlCmd.Parameters.Add(new SqlParameter("@CourierTenantID",CourierTenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));


                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }                    
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }

        }

        public void deleteCourierConfiguration(int TenantID, int CourierConfigID)
        {           
            try
            {

                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("delete from stblCourierConfig where CourierConfigID=@CourierConfigID and TenantID=@TenantID ", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierConfigID", CourierConfigID));
                        //sqlCmd.Parameters.Add(new SqlParameter("@CourierTenantID",CourierTenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID",TenantID));

                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
        }

        public int SaveCourierTenant(int TenantID, CourierConfigurationBO cbo)
        {

            int rtnValue = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    //SqlCommand sqlCmd = new SqlCommand("insert into stblCourierTenant (CourierID,TenantID) values (@CourierID,@TenantID);", sqlCon);
                    SqlCommand sqlCmd = new SqlCommand("sspSaveCourierTenant", sqlCon);
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierID", cbo.CourierID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CourierTenantID", cbo.CourierTenantID));

                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rtnValue = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
                _ErrMsg = ex.Message;
                throw;
            }
            return rtnValue;
        }

    }

    public class KitTypeConfigurationDA
    {
        private string _ErrMsg;        

        public List<KitTypeConfigurationBO> GetKitType()
        {
            List<KitTypeConfigurationBO> kitTypeconfigurationBO = new List<KitTypeConfigurationBO>();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("select * from stblKit", sqlCon);
                    {                        
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                kitTypeconfigurationBO.Add(new KitTypeConfigurationBO
                                {
                                    KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]),
                                    KitName = reader["KitName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["KitName"]),
                                });
                            }
                        }
                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return kitTypeconfigurationBO;
        }

        public KitTypeConfigurationBO UpdateKitConfiguration(int TenantID, int KitID,int KitTenantID)
        {
            KitTypeConfigurationBO UpdateKitBO = new KitTypeConfigurationBO();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspSaveKitTenant", sqlCon);
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@KitID",KitID));
                        sqlCmd.Parameters.Add(new SqlParameter("@KitTenantID", KitTenantID));                       
                        
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UpdateKitBO.KitTenantID = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));                                                               
                            }
                        }
                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return UpdateKitBO;
        }

        public KitTypeConfigurationBO DisplayKitConfiguration(int TenantID, int KitID)
        {
            KitTypeConfigurationBO displaykitBO = new KitTypeConfigurationBO();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("select * from stblKitTenant where TenantID=@TenantID and KitID=@KitID", sqlCon);
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@KitID", KitID));
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                displaykitBO.KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]);
                                displaykitBO.KitTenantID = reader["KitTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitTenantID"]);
                                displaykitBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                            }
                        }
                        reader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return displaykitBO;
        }

        public KitTypeConfigurationBO deleteKitConfiguration(int TenantID,int KitID)
        {
            KitTypeConfigurationBO DeleteKitBO = new KitTypeConfigurationBO();
            try
            {
                using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Delete from stblKitTenant where TenantID=@TenantID and KitID=@KitID", con);
                    {
                        cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        cmd.Parameters.Add(new SqlParameter("@KitID", KitID));
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;

                        con.Open();
                        cmd.ExecuteNonQuery();                        
                        con.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return DeleteKitBO;
        }
    }

    public class ActiveClientConfigurationDA
    { 

        public ConfigurationBO GetConfiguration(int TenantID,string ConfigName)
        {
            ConfigurationBO actBO = new ConfigurationBO();

            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from stblConfigSettings where TenantID = @TenantID and ConfigName = @ConfigName", con);
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@ConfigName", ConfigName));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            actBO.ConfigID = reader["ConfigID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ConfigID"]);
                            actBO.ConfigType = reader["ConfigType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConfigType"]);
                            actBO.ConfigName = reader["ConfigName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConfigName"]);
                            actBO.ConfigValue = reader["ConfigValue"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ConfigValue"]);
                            actBO.TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                            actBO.PrefixYear = reader["PrefixYear"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PrefixYear"]);
                            actBO.SrNumber = reader["SrNumber"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SrNumber"]);
                        }
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return actBO;
        } 
        
        public ConfigurationBO SaveActiveClient(int TenantID,string ConfigValue)
        {
            ConfigurationBO ActivesaveBO = new ConfigurationBO();

            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                SqlCommand cmd = new SqlCommand("insert into stblConfigSettings(ConfigType,ConfigName,ConfigValue,TenantID,PrefixYear) values('ActiveUsers', 'ActiveClients', @ConfigValue, @TenantID, @Year)", con);
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@ConfigValue",ConfigValue));
                    cmd.Parameters.Add(new SqlParameter("@Year", year));
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();                   
                    con.Close();                    
                }
            }
            return ActivesaveBO;
        }

        public ConfigurationBO UpdateActiveClient(int TenantID, string ConfigValue)
        {
            ConfigurationBO ActiveupdateBO = new ConfigurationBO();

            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("update stblConfigSettings set ConfigValue = @ConfigValue where TenantID = @TenantID and ConfigName = 'ActiveClients'", con);
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@ConfigValue", ConfigValue));
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return ActiveupdateBO;
        }

        public List<OrderKitBO> ActiveClientReport(int TenantID,string ConfigValue)
        {
            List<OrderKitBO> ordBO = new List<OrderKitBO>(); 
            using (SqlConnection con = new SqlConnection(Constant.DBConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sspActiveClient", con);
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@ConfigValue", ConfigValue));
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();                    
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {                            
                            ordBO.Add(new OrderKitBO
                            {
                                FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                RequesterType = reader["RequesterType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequesterType"]),
                                Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),
                                Address = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                Zip = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]),
                                KitType = reader["KitType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitType"]),
                                RequestNumber = reader["RequestNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestNumber"]),                                
                                FullAddress = (reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"])) + ",\n" + (reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"])) + ",\n" + (reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"])) + ",\n" + (reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"])) + ",\n" + (reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"])),                                                               
                             });
                        }
                    }
                    reader.Close();
                    con.Close();                
                }
            }
            return ordBO;
        }
    }

}
