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
    public class AdminDA
    {
        private string _ErrMsg;
        private string strErrorMsg;

        public List<MySettingBO> GetMySettingsDetails(int userID)
        {
            List<MySettingBO> objMySetting = new List<MySettingBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from tblMySettings where userID=" + userID.ToString(), sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objMySetting.Add(new MySettingBO
                                {
                                    SettingID = reader["SettingID"] == DBNull.Value ? 0 : Convert.ToInt16(reader["SettingID"]),
                                    UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt16(reader["UserID"]),
                                    ShortCutURL1 = reader["ShortCut1"] == DBNull.Value ? "#" : Convert.ToString(reader["ShortCut1"]),
                                    ShortCutURL2 = reader["ShortCut2"] == DBNull.Value ? "#" : Convert.ToString(reader["ShortCut2"]),
                                    ShortCutURL3 = reader["ShortCut3"] == DBNull.Value ? "#" : Convert.ToString(reader["ShortCut3"]),
                                    ShortCutURL4 = reader["ShortCut4"] == DBNull.Value ? "#" : Convert.ToString(reader["ShortCut4"]),
                                    PeriodSetting = reader["PeriodSetting"] == DBNull.Value ? "Last 12 months" : Convert.ToString(reader["PeriodSetting"])
                                });

                            }

                        }
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }

            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return objMySetting;
        }
        public void SavingMySettings(MySettingBO objSetting)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("spSavingMySettings", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", objSetting.UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SC1", objSetting.ShortCutURL1));
                    sqlCmd.Parameters.Add(new SqlParameter("@SC2", objSetting.ShortCutURL2));
                    sqlCmd.Parameters.Add(new SqlParameter("@SC3", objSetting.ShortCutURL3));
                    sqlCmd.Parameters.Add(new SqlParameter("@SC4", objSetting.ShortCutURL4));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
        }
        public List<CountryMasterBO> getCountryList(bool IsSelectedOnly = false)
        {
            SqlConnection sqlCon = null;
            List<CountryMasterBO> objCountry = new List<CountryMasterBO>();
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetCountryList", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@IsSelectedOnly", IsSelectedOnly));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objCountry.Add(new CountryMasterBO
                            {
                                CountryID = reader["CountryID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountryID"]),
                                CountryCode = reader["CountryCode"] == DBNull.Value ? "" : Convert.ToString(reader["CountryCode"]),
                                CountryName = reader["CountryName"] == DBNull.Value ? "" : Convert.ToString(reader["CountryName"]),
                                Region = reader["Region"] == DBNull.Value ? "" : Convert.ToString(reader["Region"]),
                                isSelected = reader["SuppCountryID"] == DBNull.Value ? false : (Convert.ToInt32(reader["SuppCountryID"]) == 0 ? false : true)
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objCountry;
        }

        public int SaveUsergroup(UserBO usergroup)
        {
            int rtnusergroupid = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspSaveMasUsergroup", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", usergroup.UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroup", usergroup.UserName));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupDesc", usergroup.UserDescription));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupStatus", usergroup.UserGroupStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", usergroup.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@KProductID", usergroup.kProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", usergroup.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsStandard", usergroup.IsStandard));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rtnusergroupid = Convert.ToInt32(reader[0]);
                        }
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return rtnusergroupid;
        }

        public List<UserBO> getMembers(int UserGroupID)
        {
            List<UserBO> viewMem = new List<UserBO>();

            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntUserGroup where UserGroupID=@UserGroupID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            viewMem.Add(new UserBO
                            {
                                UserGroupID = reader["UserGroupID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserGroupID"]),
                                UserGroup = reader["UserGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroup"]),
                                UserDescription = reader["UserGroupDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupDesc"]),
                                UserGroupStatus = reader["UserGroupStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupStatus"]),
                                IsStandard = reader["IsStandard"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsStandard"]),
                                SubscriptionID = reader["SubscriptionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SubscriptionID"])
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
            return viewMem;
        }


        public List<CrossTenantBO> getCrossTenant(int AppUserID)
        {
            List<CrossTenantBO> objcross = new List<CrossTenantBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntCrossTenant where AppUserID=@AppUserID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", AppUserID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objcross.Add(new CrossTenantBO
                            {
                                AppUserID = reader["AppUserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AppUserID"]),
                                CrossTenantID = reader["CrossTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CrossTenantID"]),
                                PrimaryCompanyID = reader["PrimaryCompanyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PrimaryCompanyID"]),
                                TenantCode = reader["TenantCode"] == DBNull.Value ? String.Empty: Convert.ToString(reader["TenantCode"]),
                                AppUserName = reader["AppUserName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["AppUserName"]),
                                TenantName = reader["TenantName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["TenantName"]),
                                Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"]),
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"])
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
            return objcross;
        }


        public List<CrossTenantBO> getCrossTenantDetails(int TenantID)
        {
            List<CrossTenantBO> objcross = new List<CrossTenantBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntCrossTenant where TenantID=@TenantID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objcross.Add(new CrossTenantBO
                            {
                                AppUserID = reader["AppUserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AppUserID"]),
                                CrossTenantID = reader["CrossTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CrossTenantID"]),
                                PrimaryCompanyID = reader["PrimaryCompanyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PrimaryCompanyID"]),
                                TenantCode = reader["TenantCode"] == DBNull.Value ? String.Empty : Convert.ToString(reader["TenantCode"]),
                                AppUserName = reader["AppUserName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["AppUserName"]),
                                TenantName = reader["TenantName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["TenantName"]),
                                Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"]),
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"])
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

            return objcross;
        }

    }


    public class DocumentDA
    {
        private string _ErrMsg;

        public int SaveDocRecord(DocumentBO oDoc)
        {
            int rtnDocID = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspSaveDocRecord", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oDoc.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocumentName", oDoc.DocumentName));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocType",oDoc.DocType));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oDoc.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@OrgDocName", oDoc.OrgDocName));
                    sqlCmd.Parameters.Add(new SqlParameter("@GenDocName", oDoc.GenDocName));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnDocID = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return rtnDocID;
        }

        public DocumentBO SaveReqFormCopy(DocumentBO oDoc)
        {            
            DocumentBO documentBO = new DocumentBO();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspSaveReqForm", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oDoc.TenantID));
                   // sqlCmd.Parameters.Add(new SqlParameter("@DocumentName", oDoc.DocumentName));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocType", oDoc.DocType));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oDoc.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@OrgDocName", oDoc.OrgDocName));
                    //  sqlCmd.Parameters.Add(new SqlParameter("@GenDocName", oDoc.GenDocName));                    

                    SqlDataAdapter adaptar = new SqlDataAdapter(sqlCmd);
                    adaptar.Fill(dataSet,"My Data Set");
                    sqlCon.Close();
                    documentBO.DocNumber = Convert.ToString(dataSet.Tables[1].Rows[0].ItemArray[1]);
                    documentBO.DocID = Convert.ToInt32(dataSet.Tables[1].Rows[0].ItemArray[0]);
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return documentBO;
        }

        public List<DocumentBO> GetResultid(string DocNumber)
        {
            List<DocumentBO> objDocToken = new List<DocumentBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from stblDocuments where DocNumber=@DocNumber", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@DocNumber", DocNumber));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objDocToken.Add(new DocumentBO
                            {
                                DocID = reader["DocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DocID"]),
                                DocNumber = reader["DocNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DocNumber"]),
                                TokenID = reader["TokenID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TokenID"]),
                                ValidUpto = reader["ValidUpto"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ValidUpto"]),
                                //TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"])
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
                _ErrMsg = ex.Message;
                throw;

            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }

            return objDocToken;
        }



        public List<DocumentBO> GetTokenNum(string token)
        {
            List<DocumentBO> objDocToken = new List<DocumentBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from stblDocuments where TokenID=@TokenID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TokenID",token));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objDocToken.Add(new DocumentBO
                            {
                               DocID = reader["DocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DocID"]),
                               DocNumber = reader["DocNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DocNumber"]),
                               TokenID = reader["TokenID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TokenID"]),
                               ValidUpto = reader["ValidUpto"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["ValidUpto"]),
                               //TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"])
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
                _ErrMsg = ex.Message;
                throw;

            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }

            return objDocToken;
        }        

        public string UpdateDocNum(DocumentBO objtoken)
        {
          string rtnappuserId = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspCreateTokenNumber", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objtoken.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocNumber", objtoken.DocNumber));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocID", objtoken.DocID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TokenID", objtoken.TokenID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rtnappuserId = Convert.ToString(reader[0]);
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return rtnappuserId;
        }



        public DocumentBO GenerateAndDocumentNo(DocumentBO oDoc,string ConfigValue)
        {
            DocumentBO documentBO = new DocumentBO();
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("sspGenNumberAddtoDoc", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oDoc.TenantID));
                    // sqlCmd.Parameters.Add(new SqlParameter("@DocumentName", oDoc.DocumentName));
                    sqlCmd.Parameters.Add(new SqlParameter("@DocType", oDoc.DocType));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oDoc.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@OrgDocName", oDoc.OrgDocName));
                    //  sqlCmd.Parameters.Add(new SqlParameter("@GenDocName", oDoc.GenDocName));                    
                    sqlCmd.Parameters.Add(new SqlParameter("@ConfigValue", ConfigValue));
                    SqlDataAdapter adaptar = new SqlDataAdapter(sqlCmd);
                    adaptar.Fill(dataSet, "My Data Set");
                    sqlCon.Close();
                    documentBO.DocNumber = Convert.ToString(dataSet.Tables[1].Rows[0].ItemArray[1]);
                    documentBO.DocID = Convert.ToInt32(dataSet.Tables[1].Rows[0].ItemArray[0]);
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return documentBO;
        }

      

        public int SaveUserCreationform(UserBO userbo)
        {
            int rtnappuserId = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("spSaveAppUser", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@firstname", userbo.UserFirstName));
                    sqlCmd.Parameters.Add(new SqlParameter("@lastname", userbo.UserLastName));
                    sqlCmd.Parameters.Add(new SqlParameter("@appusername", userbo.UserEmail));
                    sqlCmd.Parameters.Add(new SqlParameter("@password", userbo.Password));
                    sqlCmd.Parameters.Add(new SqlParameter("@contactno", userbo.UserMobileNumber));
                    sqlCmd.Parameters.Add(new SqlParameter("@status", userbo.UserStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@tenantID", userbo.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@kproductID", userbo.kProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@createdby", userbo.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@appuserId", userbo.UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserType", userbo.UserTypeName));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rtnappuserId = Convert.ToInt32(reader[0]);
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }
            return rtnappuserId;
        }

        public void UpdateUserStatus(string AppUserStatus,int AppUserID)
        {            
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    SqlCommand sqlCmd = new SqlCommand("Update tblEntAppUser set AppUserStatus =  @appuserstatus where AppUserID = @appuserID", sqlCon);
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;                    
                    sqlCmd.Parameters.Add(new SqlParameter("@appuserstatus", AppUserStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@appuserID", AppUserID));
                    sqlCmd.ExecuteNonQuery();                    
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                _ErrMsg = ex.Message;
                throw;
            }            
        }



       

    }
}
