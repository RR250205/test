﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace KoSoft.Entitlement
{
    public class KoAccess
    {
        //version 4

        string strErrorDesc = "";

        string _connectionString = string.Empty;

        public KoAccess(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<AppUserBO> GetAppUser(int TenantID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            String sSql = "SELECT* FROM vwEntAppUser where TenantID = @TenantID";
            if (ProductID != 0)
                sSql = "SELECT* FROM vwEntAppUser where TenantID = @TenantID and KProductID=@ProductID";
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sSql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    if (ProductID != 0)
                        sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<UserRoleBO> uRoles = GetMyUserRoles(Convert.ToInt32(reader["appUserID"]), ProductID);
                            string ProfilePhotoPath = GetDocumentPath(reader["ProfileImageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ProfileImageID"]));
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                MyRoles = uRoles,
                                Company = (new TenantBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TenantID"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                    TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"])
                                }),
                                ProfilePhoto = ProfilePhotoPath,
                                PrimaryEmail = reader["PrimaryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PrimaryEmail"]),
                                SecondaryEmail = reader["SecondaryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SecondaryEmail"]),
                                ContactPhone = reader["ContactPhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactPhone"]),

                                LastLoginTime = reader["LastSigninAt"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastSigninAt"]),
                                LoginFromIP = reader["LastSigninIP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastSigninIP"]),
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objAppUserBO;
        }

        //Dont delete this method
        //public void DeleteAppUser(int AppUserID)
        //{
        //    SqlConnection sqlCon = null;            
        //    try
        //    {
        //        sqlCon = new SqlConnection(_connectionString);
        //        using (SqlCommand sqlCmd = new SqlCommand("Delete from tblEntAppUser where AppUserID=@AppUserID", sqlCon))
        //        {
        //            sqlCon.Open();
        //            sqlCmd.CommandType = CommandType.Text;
        //            sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", AppUserID));
        //            sqlCmd.ExecuteNonQuery();
        //            sqlCon.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorDesc = ex.Message; throw;
        //    }
        //    finally
        //    {
        //        if (sqlCon != null)
        //        {
        //            sqlCon.Dispose();
        //        }
        //    }            
        //}

        public List<AppUserBO> GetAppUser(string AppUserName, string UserPassword, string LoginIP = "", int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAccessUserInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserName", AppUserName));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserPassword", UserPassword));
                    sqlCmd.Parameters.Add(new SqlParameter("@LoginIP", LoginIP));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<UserRoleBO> uRoles = GetMyUserRoles(Convert.ToInt32(reader["appUserID"]), ProductID);
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                MyRoles = uRoles,
                                Company = (new TenantBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TenantID"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                    TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"])
                                }),
                                ProfilePhotoID = reader["profileImageID"] == DBNull.Value ? -4 : Convert.ToInt32(reader["profileImageID"]),
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"]),
                                DefaultPage = reader["DefaultPage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DefaultPage"]),
                                ErrorMsg = (new Entitlement.ErrorMessageBO
                                {
                                    ErrorCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"]),
                                    ErrorMsg = reader["ErrorMsg"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ErrorMsg"])
                                })
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Dispose();
                }
            }
            return objAppUserBO;
        }

      

        public List<AppUserBO> GetAppUser(UserAuthBO oAuth, string LoginIP = "", int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("spEntAccessUserInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserName", oAuth.UserName));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserPassword", oAuth.Password));
                    sqlCmd.Parameters.Add(new SqlParameter("@LoginIP", LoginIP));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<UserRoleBO> uRoles = GetMyUserRoles(Convert.ToInt32(reader["appUserID"]), ProductID);
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                MyRoles = uRoles,
                                Company = (new TenantBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TenantID"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                    TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"])
                                }),
                                ProfilePhotoID = reader["profileImageID"] == DBNull.Value ? -4 : Convert.ToInt32(reader["profileImageID"]),
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Dispose();
                }
            }
            return objAppUserBO;
        }


        public List<AppUserBO> GetAppUser(string AppUserName, string UserPassword, string LoginIP = "", int ProductID = 0, string UserType = "")
        {
            SqlConnection sqlCon = null;
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAccessUserInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserName", AppUserName));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserPassword", UserPassword));
                    sqlCmd.Parameters.Add(new SqlParameter("@LoginIP", LoginIP));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserType", UserType));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<UserRoleBO> uRoles = GetMyUserRoles(Convert.ToInt32(reader["appUserID"]), ProductID);
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                UserAccessType = reader["UserType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserType"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                MyRoles = uRoles,
                                Company = (new TenantBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TenantID"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                    TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"])
                                }),
                                ProfilePhotoID = reader["profileImageID"] == DBNull.Value ? -4 : Convert.ToInt32(reader["profileImageID"]),
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"]),
                                DefaultPage = reader["DefaultPage"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DefaultPage"]),
                                ErrorMsg = (new Entitlement.ErrorMessageBO
                                {
                                    ErrorCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"]),
                                    ErrorMsg = reader["ErrorMsg"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ErrorMsg"])
                                })
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Dispose();
                }
            }
            return objAppUserBO;
        }


        public List<AppUserBO> GetAppUserInfo(int UserID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", UserID)); ;
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID)); ;
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<UserRoleBO> uRoles = GetMyUserRoles(Convert.ToInt32(reader["appUserID"]), 0);
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                //UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                MyRoles = uRoles,
                                Company = (new TenantBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["TenantID"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]),
                                    TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"])
                                }),
                               
                                 Prefix = reader["Prefix"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Prefix"]),
                                ProfilePhotoID = reader["profileImageID"] == DBNull.Value ? -4 : Convert.ToInt32(reader["profileImageID"]),
                                PrimaryEmail = reader["PrimaryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PrimaryEmail"]),
                                SecondaryEmail = reader["SecondaryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SecondaryEmail"]),
                                ContactPhone = reader["ContactPhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactPhone"]),
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"]),
                                AppUserStatus= reader["AppUserStatus"] == DBNull.Value ? string.Empty: Convert.ToString(reader["AppUserStatus"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Dispose();
                }
            }
            return objAppUserBO;
        }

        public string AddAppUser(AppUserBO objAppUserBO)
        {
            SqlConnection sqlCon = null;
            string rtnVal = string.Empty;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAddAppUser", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserName", objAppUserBO.AppUserName));
                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserPassword", objAppUserBO.Password));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objAppUserBO.Company.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@FirstName", objAppUserBO.FirstName));
                    sqlCmd.Parameters.Add(new SqlParameter("@LastName", objAppUserBO.LastName));
                    sqlCmd.Parameters.Add(new SqlParameter("@MiddleName", objAppUserBO.MiddleName));
                    sqlCmd.Parameters.Add(new SqlParameter("@ActivationKey", objAppUserBO.UserStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", objAppUserBO.MyRoles[0].ProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", objAppUserBO.CreatedBy));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToString(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public string UpdateUserInfo(AppUserBO objAppUserBO)
        {
            SqlConnection sqlCon = null;
            string rtnVal = string.Empty;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntUpdateUserInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", objAppUserBO.AppUserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@Prefix", objAppUserBO.Prefix));
                    sqlCmd.Parameters.Add(new SqlParameter("@FirstName", objAppUserBO.FirstName));
                    sqlCmd.Parameters.Add(new SqlParameter("@LastName", objAppUserBO.LastName));
                    //sqlCmd.Parameters.Add(new SqlParameter("@MiddleName", objAppUserBO.MiddleName));
                    sqlCmd.Parameters.Add(new SqlParameter("@Department", objAppUserBO.Department));

                    sqlCmd.Parameters.Add(new SqlParameter("@PrimaryEmail", objAppUserBO.PrimaryEmail));
                    sqlCmd.Parameters.Add(new SqlParameter("@SecondaryEmail", objAppUserBO.SecondaryEmail));
                    sqlCmd.Parameters.Add(new SqlParameter("@ContactPhone", objAppUserBO.ContactPhone));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProfileImageID", objAppUserBO.ProfilePhotoID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    reader.Close();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                rtnVal = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public string ChangeAppUserPassword(string UserID, string NewPassword, string CurrentPass = "", bool isReset = false)
        {
            SqlConnection sqlCon = null;
            string rtnVal = string.Empty;
            try
            {

                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntChangeAppUserPassword", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@NewPassword", NewPassword));
                    if (isReset)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@CurrentPassword", CurrentPass));
                        sqlCmd.Parameters.Add(new SqlParameter("@IsReset", isReset));
                    }
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToString(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public void SaveAuthLog(AuthLogBO objLog)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAuthLogging", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", objLog.UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@loginIP", objLog.ClientIP));
                    sqlCmd.Parameters.Add(new SqlParameter("@AuthStatus", objLog.AuthStatus));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public int GetServiceParentID(string ServiceID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            int rtnVal = 0;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select parentServiceID from tblEntServices where entServiceID=@ServiceID and KProductID=@ProductID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@ServiceID", ServiceID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public List<EntitleServiceBO> GetUserAccessModules(String userID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserAccessModules", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", userID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"]),
                                isChild = reader["isChild"] == DBNull.Value ? false : Convert.ToBoolean(reader["isChild"]),
                                MenuIcon = reader["MenuIcon"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MenuIcon"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceBO> GetUserAccessModulesOnly(string userID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserAccessModuleOnly", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", userID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"]),
                                isChild = reader["isChild"] == DBNull.Value ? false : Convert.ToBoolean(reader["isChild"]),
                                MenuIcon = reader["MenuIcon"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MenuIcon"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<FunctionBO> getUserFunctions(int UserID, int TenantID,int ProductID)
        {
            SqlConnection sqlCon = null;
            List<FunctionBO> getUserfundet = new List<FunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupFunction where KProductID = @ProductID and TenantID = @TenantID and CreatedBy = @userID and resourceType = 'function_service'", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;                    
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EntitleServiceBO entsrBO = new EntitleServiceBO();
                            entsrBO.ServiceID = reader["EntServiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EntServiceID"]);
                            getUserfundet.Add(new FunctionBO
                            {
                                ReadAccess = reader["ReadAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ReadAccess"]),
                                WriteAccess = reader["WriteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["WriteAccess"]),
                                ExecuteAccess = reader["ExecuteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExecuteAccess"]),
                                ExportAccess = reader["ExportAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExportAccess"]),
                                RoleSpecificAccess = reader["RoleSpecificAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["RoleSpecificAccess"]),
                                entServBO = entsrBO,
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }

            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return getUserfundet;
        }

        public List<EntitleServiceBO> GetUserAccessModuleServices(string userID, int moduleID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserAccessModuleServices", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", userID));
                    sqlCmd.Parameters.Add(new SqlParameter("@moduleID", moduleID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"]),
                                isChild = reader["isChild"] == DBNull.Value ? false : Convert.ToBoolean(reader["isChild"]),
                                MenuIcon = reader["MenuIcon"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MenuIcon"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }        

        public List<UserGroupBO> GetUserGroup(string userGroupID)
        {
            SqlConnection sqlCon = null;
            List<UserGroupBO> objUserGroupBO = new List<UserGroupBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntUserGroup where userGroupID=@userGroupID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objUserGroupBO.Add(new UserGroupBO
                            {
                                UserGroupID = reader["UserGroupID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserGroupID"]),
                                UserGroup = reader["UserGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroup"]),
                                UserGroupDesc = reader["UserGroupDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupDesc"]),
                                IsStandard = reader["IsStandard"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsStandard"]),
                                UserGroupStatus = reader["UserGroupStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupStatus"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objUserGroupBO;
        }        

        public List<UserGroupBO> GetUserGroup(int TenantID = 0, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<UserGroupBO> objUserGroupBO = new List<UserGroupBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserGroup", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objUserGroupBO.Add(new UserGroupBO
                            {
                                UserGroupID = reader["UserGroupID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserGroupID"]),
                                UserGroup = reader["UserGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroup"]),
                                UserGroupDesc = reader["UserGroupDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupDesc"]),
                                IsStandard = reader["IsStandard"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsStandard"]),
                                UserGroupStatus = reader["UserGroupStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroupStatus"]),
                                CreatedBy = reader["AppUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AppUserName"]),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(Convert.ToDateTime(reader["CreatedOn"]).ToShortDateString()),
                                MemberCount = reader["MemberCount"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MemberCount"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objUserGroupBO;

        }

        public void DeleteUserGroup(string UserGroupID)
        {
            SqlConnection sqlCon = null;
            string query = "Delete from tblEntUserGroup where UserGroupID=@UserGroupID;";
            query += "Delete from tblEntUserGroupMembers where UserGroupID=@UserGroupID;";
            query += "Delete from tblEntServiceUserGroup where UserGroupID = @UserGroupID";
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                
                using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));
                    sqlCmd.ExecuteNonQuery();                   
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                sqlCon.Dispose();
            }           
        }

        public List<GroupMemberBO> GetGroupMembers(string UserGroupID)
        {
            SqlConnection sqlCon = null;
            AppUserBO objUserBO = new AppUserBO();
            UserGroupBO objUserGroupBO = new UserGroupBO();
            List<GroupMemberBO> objGroupMemberBO = new List<GroupMemberBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupMembers where userGroupID=@UserGroupID  order by fullname", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objGroupMemberBO.Add(new GroupMemberBO
                            {
                                GroupMemberID = reader["grpMemID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["grpMemID"]),
                                FullName = reader["fullname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["fullname"]),                               
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objGroupMemberBO;
        }

        public List<GroupMemberBO> GetGroupMembers(string UserGroupID,int TenantID,int ProductID)
        {
            SqlConnection sqlCon = null;
            AppUserBO objUserBO = new AppUserBO();
            UserGroupBO objUserGroupBO = new UserGroupBO();
            List<GroupMemberBO> objGroupMemberBO = new List<GroupMemberBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupMembersList where userGroupID=@userGroupID and TenantID = @TenantID and kProductID = @ProductID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objGroupMemberBO.Add(new GroupMemberBO
                            {
                                GroupMemberID = reader["GroupMemID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["GroupMemID"]),
                                FullName = reader["FullName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FullName"]),
                                Email = reader["AppUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AppUserName"]),
                                UserBO = new AppUserBO()
                                {
                                    AppUserID= reader["AppUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["AppUserID"]),
                                    AppUserStatus = reader["AppUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AppUserStatus"]),
                                },
                                userGroupBO = new UserGroupBO()
                                {
                                    UserGroup = reader["UserGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserGroup"]),
                                }
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message;throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objGroupMemberBO;
        }

        public List<AppUserBO> GetGroupNonMembers(string UserGroupID, int ProductID = 0, int TenantID = 0)
        {
            SqlConnection sqlCon = null;
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntGetNonGroupMembers", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string ProfilePhotoPath = GetDocumentPath(reader["ProfileImageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ProfileImageID"]));
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                Department = reader["Department"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Department"]),
                                ProfilePhoto = ProfilePhotoPath,
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objAppUserBO;
        }

        public string GetDocumentPath(int DocID)
        {
            SqlConnection sqlCon = null;
            string rtnVal = string.Empty;
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from vwGenDocuments where DocID=@DocID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@DocID", DocID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = (reader["RepoPath"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RepoPath"])) +
                                    (reader["GenFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GenFileName"]));
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public List<AppUserBO> GetGroupNonMembersFind(string userGroupID, string searchCriteria)
        {
            SqlConnection sqlCon = null;
            string sql = "select * from vwAppUsers where AppUserID not in (select appUserID from vwEntUserGroupMembers where userGroupID=@userGroupID) and (@searchCriteria) order by firstName,lastname";
            List<AppUserBO> objAppUserBO = new List<AppUserBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@searchCriteria", searchCriteria));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string ProfilePhotoPath = GetDocumentPath(reader["ProfileImageID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ProfileImageID"]));
                            objAppUserBO.Add(new AppUserBO
                            {
                                AppUserID = reader["appUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["appUserID"]),
                                AppUserName = reader["appUserName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserName"]),
                                FirstName = reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"]),
                                LastName = reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"]),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MiddleName"]),
                                FullName =
                                    (reader["firstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["firstName"])) + " " +
                                    (reader["lastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["lastName"])),
                                UserStatus = reader["appUserStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["appUserStatus"]),
                                ProfilePhoto = ProfilePhotoPath,
                                ReturnCode = reader["ReturnCode"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReturnCode"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objAppUserBO;
        }

        public void AddUserToUserGroup(string userGroupID, string userID)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAddUserToGroup", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", userID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public void DeleteUserFromUserGroup(string userGroupID, string userID)
        {
            SqlConnection sqlCon = null;
            string sql = "Delete from tblEntUserGroupMembers where userGroupID=@userGroupID  and appUserID=@userID";
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                        sqlCmd.Parameters.Add(new SqlParameter("@userID", userID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public void DeleteUserFromUserGroup(string userGroupID, int AppUserID)
        {
            SqlConnection sqlCon = null;
            string sql = "Delete from tblEntUserGroupMembers where userGroupID=@userGroupID  and AppUserID=@AppUserID";
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                        sqlCmd.Parameters.Add(new SqlParameter("@AppUserID", AppUserID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        reader.Close();
                        reader = null;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public int SaveUserGroup(UserGroupBO objUserGroupBO)
        {
            SqlConnection sqlCon = null;
            int rtnVal = 0;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntUpdateUserGroupDefn", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@usrGroupID", objUserGroupBO.UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroup", objUserGroupBO.UserGroup));
                    sqlCmd.Parameters.Add(new SqlParameter("@groupDesc", objUserGroupBO.UserGroupDesc));
                    sqlCmd.Parameters.Add(new SqlParameter("@isStandard", objUserGroupBO.IsStandard));
                    sqlCmd.Parameters.Add(new SqlParameter("@groupStatus", objUserGroupBO.UserGroupStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", objUserGroupBO.ProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objUserGroupBO.TenantID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnVal = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnVal;
        }

        public List<EntitleServiceBO> GetServiceDetail(int serviceID)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntServices where entServiceID=@serviceID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@serviceID", @serviceID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceBO> GetServiceList(int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            string sql = "Select * from vwEntService where ProductID=@TenantID order by globalOrderBy";
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ParentServiceName = reader["ParentServiceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ParentServiceName"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"]),
                                Read_Attri = reader["Read_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Read_Attri"]),
                                Write_Attri = reader["Write_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Write_Attri"]),
                                Delete_Attri = reader["Delete_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Delete_Attri"]),
                                Approve_Attri = reader["Approve_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Approve_Attri"]),
                                Export_Attri = reader["Export_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Export_Attri"]),
                                isChild = reader["isChild"] == DBNull.Value ? false : Convert.ToBoolean(reader["isChild"]),
                                MenuIcon = reader["MenuIcon"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MenuIcon"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceBO> GetServiceList(string ParentServiceID)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntServices where parentServiceID=@parentServiceID  order by globalOrderBy", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@parentServiceID", ParentServiceID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceBO> GetServiceList(string ParentServiceID, string UserID)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntServices where parentServiceID=@parentServiceID  order by globalOrderBy", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@parentServiceID", ParentServiceID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceBO> GetServiceList(string ParentServiceID, string UserID, int ProductID)
        {
            SqlConnection sqlCon = null;
            List<EntitleServiceBO> objEntSrvBO = new List<EntitleServiceBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from tblEntServices where parentServiceID=@parentServiceID  order by globalOrderBy", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@parentServiceID", ParentServiceID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvBO.Add(new EntitleServiceBO
                            {
                                ServiceID = reader["entServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["entServiceID"]),
                                ServiceName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ServiceOrder = reader["serviceOrder"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceOrder"]),
                                ResourceAction = reader["resourceAction"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceAction"]),
                                ParentServiceID = reader["parentServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["parentServiceID"]),
                                ResourceType = reader["resourceType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["resourceType"]),
                                serviceDesc = reader["serviceDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceDesc"]),
                                serviceGroup = reader["serviceGroup"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceGroup"]),
                                displayIndicator = reader["displayIndicator"] == DBNull.Value ? string.Empty : Convert.ToString(reader["displayIndicator"]),
                                globalOrderBy = reader["globalOrderBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["globalOrderBy"]),
                                isEntitlable = reader["isEntitlable"] == DBNull.Value ? false : Convert.ToBoolean(reader["isEntitlable"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvBO;
        }

        public List<EntitleServiceUserGroupBO> GetUserGroupServiceList(string userGroupID)
        {
            SqlConnection sqlCon = null;
            string sql = "select * from tblEntServiceUserGroup where userGroupID = @userGroupID";
            List<EntitleServiceUserGroupBO> objEntSrvUsrGrpBO = new List<EntitleServiceUserGroupBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", userGroupID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objEntSrvUsrGrpBO.Add(new EntitleServiceUserGroupBO
                            {
                                ServiceID = reader["EntServiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["EntServiceID"]),
                                GroupID = reader["UsergroupID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UsergroupID"]),
                                ServiceAttributes = reader["ServiceAttributes"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ServiceAttributes"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objEntSrvUsrGrpBO;
        }

        public void UpdateServiceAttributes(string userGroupID, string serviceID, string serviceAttribute)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("spEntUpdateUserGroupServices", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@usergroupID", userGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@entServiceID", serviceID));
                    sqlCmd.Parameters.Add(new SqlParameter("@serviceAttribute", serviceAttribute));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public void UpdateParentServiceAttributes(string userGroupID)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntUpdateUserGroupParentServices", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@usergroupID", userGroupID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public void AddUserRole(UserRoleBO objUserRoleBO)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntAddUserRole", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@roleName", objUserRoleBO.RoleName));
                    //sqlCmd.Parameters.Add(new SqlParameter("@roleCategory", objUserRoleBO.RoleCategory));
                    sqlCmd.Parameters.Add(new SqlParameter("@roleDesc", objUserRoleBO.RoleDescription));
                    sqlCmd.Parameters.Add(new SqlParameter("@reportToRole", objUserRoleBO.ReportToRoleID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", objUserRoleBO.ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public void SaveUserAdditionalRole(UserRoleBO objUserRoleBO)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntSaveMyRoles", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", objUserRoleBO.UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@roleID", objUserRoleBO.RoleID));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsPrimary", objUserRoleBO.IsPrimary));
                    sqlCmd.Parameters.Add(new SqlParameter("@roleStatus", objUserRoleBO.RoleStatus));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

        public List<UserRoleBO> GetMyUserRoles(int UserID, int ProductID = 0)
        {
            SqlConnection sqlCon = null;
            List<UserRoleBO> objUserRoleBO = new List<UserRoleBO>();
            string sql = "select * from vwEntMyRoles where UserID=@UserID and KProductID=@ProductID";
            if (ProductID == 0)
                sql = "select * from vwEntMyRoles where UserID=@UserID";
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", UserID));
                    if (ProductID != 0)
                        sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objUserRoleBO.Add(new UserRoleBO
                            {
                                RoleID = reader["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RoleID"]),
                                RoleName = reader["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RoleName"]),
                                RoleStatus = reader["UserRoleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UserRoleStatus"]),
                                RoleCategory = reader["RoleCategory"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RoleCategory"]),
                                IsPrimary = reader["IsPrimaryRole"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsPrimaryRole"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return objUserRoleBO;
        }

        public List<UserFunctionBO> GetUserFunctions(int UserID)
        {
            SqlConnection sqlCon = null;
            List<UserFunctionBO> oRtn = new List<UserFunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("spEntGetUserFunctions", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oRtn.Add(new UserFunctionBO
                            {
                                FunctionID = reader["FunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FunctionID"]),
                                FunctionName = reader["EntFunction"] == DBNull.Value ? "" : Convert.ToString(reader["EntFunction"]),
                                //FunctionType= reader["FunctionType"] == DBNull.Value ? false : Convert.ToBoolean(reader["FunctionType"]),
                                UserFuncID = reader["UserFuncID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserFuncID"]),
                                UserID = reader["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserID"])
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            return oRtn;
        }

        public void SaveUserFunctions(int UserID, string FuncIDs)
        {
            SqlConnection sqlCon = null;
            List<UserFunctionBO> oRtn = new List<UserFunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("spEntSaveUserFunctions", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@FuncIDs", FuncIDs));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
        }

        #region commented and need revisit
        //public List<UserRoleBO> GetUserRoleList()
        //{
        //    SqlConnection sqlCon = null;
        //    List<UserRoleBO> objUserRoleBO = new List<UserRoleBO>();
        //    string sql = "Select * from vwEntUserRole order by roleName";
        //    try
        //    {
        //        sqlCon = new SqlConnection(_connectionString);
        //        using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCon))
        //        {
        //            sqlCon.Open();
        //            sqlCmd.CommandType = CommandType.Text;
        //            SqlDataReader reader = sqlCmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    objUserRoleBO.Add(new UserRoleBO
        //                    {
        //                        UserRoleID = reader["UserRoleID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["UserRoleID"]),
        //                        RoleName = reader["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RoleName"]),
        //                        RoleDescription = reader["RoleDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RoleDescription"]),
        //                        ReportToRoleID = reader["ReportToRoleID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ReportToRoleID"]),
        //                        RoportingToRole = reader["ReportToRoleName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReportToRoleName"])
        //                    });
        //                }
        //            }
        //            reader.Close();
        //            reader = null;
        //        }
        //        sqlCon.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorDesc = ex.Message; throw;
        //    }
        //    finally
        //    {
        //        if (sqlCon != null)
        //            sqlCon.Dispose();
        //    }
        //    return objUserRoleBO;
        //}


        //public string GetBreadCrumb(int serviceID)
        //{
        //    SqlConnection sqlCon = null;
        //    string rtnVal = string.Empty;
        //    try
        //    {
        //        sqlCon = new SqlConnection(_connectionString);
        //        using (SqlCommand sqlCmd = new SqlCommand("spGetBreadCrumb", sqlCon))
        //        {
        //            sqlCon.Open();
        //            sqlCmd.CommandType = CommandType.StoredProcedure;
        //            sqlCmd.Parameters.Add(new SqlParameter("@serviceID", serviceID));
        //            SqlDataReader reader = sqlCmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                reader.Read();
        //                rtnVal = Convert.ToString(reader[0]);
        //            }
        //            reader.Close();
        //            reader = null;
        //        }
        //        sqlCon.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorDesc = ex.Message; throw;
        //    }
        //    finally
        //    {
        //        if (sqlCon != null)
        //            sqlCon.Dispose();
        //    }
        //    return rtnVal;
        //}
        #endregion


        public void AddUsergroupMembers(GroupMemberBO ObjAddUser)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntUserToUserGroup", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@userID", ObjAddUser.AppUserID));
                    sqlCmd.Parameters.Add(new SqlParameter("@userGroupID", ObjAddUser.UserGroupID));
                    sqlCmd.ExecuteNonQuery();                
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }

            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
        }

#region DeadCode
        public List<FunctionBO> GetUserGroupFunctions(int UserGroupID)
        {
            SqlConnection sqlCon = null;
            List<FunctionBO> oRtn = new List<FunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupFunctions where GroupID=@UserGroupID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                       
                        while (reader.Read())
                        {
                            oRtn.Add(new FunctionBO()
                            {
                                FunctionID = reader["FunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FunctionID"]),
                                FunctionName = reader["FunctionName"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionName"]),
                                //FunctionType= reader["FunctionType"] == DBNull.Value ? false : Convert.ToBoolean(reader["FunctionType"]),
                                FunctionCode = reader["FunctionCode"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionCode"]),
                                FunctionStatus = reader["FunctionStatus"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionStatus"]),
                                ReadAccess= reader["ReadAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ReadAccess"]),
                                WriteAccess= reader["WriteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["WriteAccess"]),
                                ExecuteAccess= reader["ExecuteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExecuteAccess"]),
                                ExportAccess= reader["ExportAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExportAccess"]),
                                RoleSpecificAccess= reader["RoleSpecificAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["RoleSpecificAccess"]),
                                UserGroupFunctionID = reader["UsergroupfunID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UsergroupfunID"]),
                            });
                            
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }

            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oRtn;
        }
#endregion

        public List<FunctionBO> GetUserGroupPermission(int ProductID,int TenantID)
        {
            SqlConnection sqlCon = null;
            List<FunctionBO> rtnvalues = new List<FunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupPermission where KProductID = @ProductID and  TenantID = @TenantID and resourceType = 'function_service'", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    
                    sqlCmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID",TenantID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EntitleServiceBO entservBO = new EntitleServiceBO();
                            entservBO.ServiceID = reader["entServiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["entServiceID"]);
                            entservBO.Read_Attri = reader["Read_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Read_Attri"]);
                            entservBO.Write_Attri = reader["Write_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Write_Attri"]);
                            entservBO.Export_Attri = reader["Export_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Export_Attri"]);
                            entservBO.Approve_Attri = reader["Approve_Attri"] == DBNull.Value ? false : Convert.ToBoolean(reader["Approve_Attri"]);

                            rtnvalues.Add(new FunctionBO()
                            {
                                FunctionName = reader["serviceName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["serviceName"]),
                                ParentFunctionID = reader["parentServiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["parentServiceID"]),
                                entServBO = entservBO,
                            });
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }

            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return rtnvalues;
        }

        public FunctionBO GetUserGroupFunction(int UserGroupID,int EntServiceID)
        {
            SqlConnection sqlCon = null;
            FunctionBO oRtn =new  FunctionBO();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntUserGroupFunction where resourceType = 'function_service' and UserGroupID=@UserGroupID and EntServiceID = @EntServiceID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@EntServiceID", EntServiceID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //oRtn.FunctionCode = reader["FunctionCode"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionCode"]);
                            //oRtn.FunctionStatus = reader["FunctionStatus"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionStatus"]);
                            oRtn.ReadAccess = reader["ReadAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ReadAccess"]);
                            oRtn.WriteAccess = reader["WriteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["WriteAccess"]);
                            oRtn.ExecuteAccess = reader["ExecuteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExecuteAccess"]);
                            oRtn.ExportAccess = reader["ExportAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExportAccess"]);
                            oRtn.RoleSpecificAccess = reader["RoleSpecificAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["RoleSpecificAccess"]);
                            oRtn.srvUserGroupID = reader["srvUserGroupID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["srvUserGroupID"]);
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }

            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oRtn;
        }

        public List<FunctionBO> GetSubscriptionFunctions(int TenantID, string KProductID="")
        {
            SqlConnection sqlCon = null;
            List<FunctionBO> oRtn = new List<FunctionBO>();
            try
            {
                sqlCon = new SqlConnection(_connectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from vwEntSubscribFunction where TenantID=@TenantID and KProductID=@KProductID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@KProductID", KProductID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oRtn.Add(new FunctionBO()
                            {
                                FunctionID = reader["FunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FunctionID"]),
                                FunctionName = reader["FunctionName"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionName"]),
                                //FunctionType= reader["FunctionType"] == DBNull.Value ? false : Convert.ToBoolean(reader["FunctionType"]),
                                FunctionCode = reader["FunctionCode"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionCode"]),
                                FunctionStatus = reader["FunctionStatus"] == DBNull.Value ? "" : Convert.ToString(reader["FunctionStatus"]),
                                ParentFunctionID= reader["ParentFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ParentFunctionID"]),
                                // ReadAccess = reader["ReadAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ReadAccess"]),
                                // WriteAccess = reader["WriteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["WriteAccess"]),
                                // ExecuteAccess = reader["ExecuteAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExecuteAccess"]),
                                // ExportAccess = reader["ExportAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["ExportAccess"]),
                                // RoleSpecificAccess = reader["RoleSpecificAccess"] == DBNull.Value ? false : Convert.ToBoolean(reader["RoleSpecificAccess"]),

                            });

                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            return oRtn;
        }

        public int SaveUserGroupFunction(int UserGroupID, FunctionBO fbo)
        {
            SqlConnection sqlCon = null;
            int rtnsrvUserGrpID = 0;
            try
            {
                sqlCon = new SqlConnection(_connectionString);
                using (SqlCommand sqlCmd = new SqlCommand("spEntUpdateUserGroupFunction", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@EntServiceID", fbo.entServBO.ServiceID));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserGroupID", UserGroupID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ReadAccess", fbo.ReadAccess));
                    sqlCmd.Parameters.Add(new SqlParameter("@WriteAccess", fbo.WriteAccess));
                    sqlCmd.Parameters.Add(new SqlParameter("@ExecuteAccess", fbo.ExecuteAccess));
                    sqlCmd.Parameters.Add(new SqlParameter("@ExportAccess", fbo.ExportAccess));
                    sqlCmd.Parameters.Add(new SqlParameter("@srvUserGroupID", fbo.srvUserGroupID));

                    SqlDataReader read = sqlCmd.ExecuteReader();

                    if(read.HasRows)
                    {
                        while (read.Read())
                        {
                            if (fbo.srvUserGroupID == 0)
                                rtnsrvUserGrpID = read["ServiceUserGroupID"] == DBNull.Value ? 0 : Convert.ToInt32(read["ServiceUserGroupID"]);
                        }
                    }
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strErrorDesc = ex.Message; throw;
            }
            finally
            {
                if (sqlCon != null)
                sqlCon.Dispose();
            }
            return rtnsrvUserGrpID;
        }
       
    }
}