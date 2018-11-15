using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STOMS.API.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace STOMS.API.DataAccess
{
    public class ApiDA
    {
        private string _connectionString;
       public ApiDA()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        
        public int SaveOrderKitData(KitOrder kitOrder)
        {
            int rtval = 0;

            using (SqlConnection con=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sspSaveOrderKit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@AuthenticateToken", kitOrder.AuthenticateToken));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", kitOrder.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", kitOrder.LastName));
                    cmd.Parameters.Add(new SqlParameter("@RequesterType", kitOrder.RequesterType));
                    cmd.Parameters.Add(new SqlParameter("@OrgName", kitOrder.OrgName));
                    cmd.Parameters.Add(new SqlParameter("@Address", kitOrder.Address));
                    cmd.Parameters.Add(new SqlParameter("@City", kitOrder.City));
                    cmd.Parameters.Add(new SqlParameter("@State", kitOrder.State));
                    cmd.Parameters.Add(new SqlParameter("@Country", kitOrder.Country));
                    cmd.Parameters.Add(new SqlParameter("@Zip", kitOrder.Zip));
                    cmd.Parameters.Add(new SqlParameter("@Telephone", kitOrder.Telephone));
                    cmd.Parameters.Add(new SqlParameter("@Email", kitOrder.Email));
                    cmd.Parameters.Add(new SqlParameter("@NoOfKits", kitOrder.NoOfKits));
                    cmd.Parameters.Add(new SqlParameter("@Message", kitOrder.Message));
                    cmd.Parameters.Add(new SqlParameter("@KitType", kitOrder.KitType));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtval = Convert.ToInt32(reader[0]);
                    }
                    con.Close();
                }
            }

            return rtval;

        }
        public int CheckEntity(string AuthenticateToken)
        {
            bool isEntity = false;
            int TenntID = 0;
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("select AuthenticateToken,PrimaryCompanyID from tblTenantAuthentication where AuthenticateToken=@AuthenticateToken", con);
                cmd.Parameters.Add(new SqlParameter("@AuthenticateToken", AuthenticateToken));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string token = Convert.ToString( reader[0]);
                    TenntID = reader[1]==DBNull.Value?0: Convert.ToInt32(reader[1]);
                    isEntity = true;
                }
                con.Close();
            }
            catch(Exception e)
            {
                throw;
            }

            return TenntID;
        }

        public int SaveCustomerNumber(CustomerBO customernumberBO)
        {
            int customerNumber = 0;
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveCustomer", sqlCon))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@FirstName", customernumberBO.FirstName));
                        sqlCmd.Parameters.Add(new SqlParameter("@LastName", customernumberBO.LastName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Address1", customernumberBO.Address1));
                        sqlCmd.Parameters.Add(new SqlParameter("@Facility", customernumberBO.Facility));
                        sqlCmd.Parameters.Add(new SqlParameter("@RequesterType", customernumberBO.RequesterType));
                        sqlCmd.Parameters.Add(new SqlParameter("@City", customernumberBO.City));
                        sqlCmd.Parameters.Add(new SqlParameter("@State", customernumberBO.State));
                        sqlCmd.Parameters.Add(new SqlParameter("@Country", customernumberBO.Country));
                        sqlCmd.Parameters.Add(new SqlParameter("@Zipcode", customernumberBO.Zipcode));
                        sqlCmd.Parameters.Add(new SqlParameter("@Phone", customernumberBO.Phone));
                        sqlCmd.Parameters.Add(new SqlParameter("@Email", customernumberBO.Email));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", customernumberBO.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@Message", customernumberBO.Message));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustID", customernumberBO.CustID));


                        sqlCon.Open();
                        SqlDataAdapter ad = new SqlDataAdapter(sqlCmd);
                        ad.Fill(set);
                        if (set.Tables.Count > 1)
                        {
                            customerNumber = set.Tables[1].Rows[0].ItemArray[0] == DBNull.Value ? 0 : Convert.ToInt32(set.Tables[1].Rows[0].ItemArray[0]);

                            //ordKitBO.OrderNumber = set.Tables[0].Rows[0].ItemArray[0] == DBNull.Value ? string.Empty : Convert.ToString(set.Tables[0].Rows[0].ItemArray[0]);
                        }
                        sqlCon.Close();

                    }
                }
            }

            catch (Exception ex)
            {
              //strErrorMsg = ex.Message;
                throw;
            }
            return customerNumber;
        }

        public int SaveInsurance(PreInsurance preIns)
        {
            int InsuranceNo = 0;
            // DataSet set = new DataSet();
            PreInsurance objpreinsu = new PreInsurance();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveInsurance", sqlCon))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@PatientName", preIns.PatientName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Gender", preIns.Gender));
                        sqlCmd.Parameters.Add(new SqlParameter("@Dataofbirth", preIns.Dataofbirth));
                        sqlCmd.Parameters.Add(new SqlParameter("@InsuranceCard_IDno", preIns.InsuranceCard_IDno ));
                        sqlCmd.Parameters.Add(new SqlParameter("@MobileNumber", preIns.MobileNumber));
                        sqlCmd.Parameters.Add(new SqlParameter("@PolicyName", preIns.PolicyName));
                        sqlCmd.Parameters.Add(new SqlParameter("@PolicyNumber", preIns.PolicyNumber));
                        sqlCmd.Parameters.Add(new SqlParameter("@PrimaryInsName", preIns.PrimaryInsName));
                        //sqlCmd.Parameters.Add(new SqlParameter("@OtherInsurance", preIns.OtherInsurance));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID",preIns.TenantID));
                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                InsuranceNo = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
                            }
                        }
                        reader.Close();
                        reader = null;
                        //if (set.Tables.Count > 1)
                        //{
                        //    customerNumber = set.Tables[1].Rows[0].ItemArray[0] == DBNull.Value ? 0 : Convert.ToInt32(set.Tables[1].Rows[0].ItemArray[0]);

                        //    //ordKitBO.OrderNumber = set.Tables[0].Rows[0].ItemArray[0] == DBNull.Value ? string.Empty : Convert.ToString(set.Tables[0].Rows[0].ItemArray[0]);
                        //}
                        sqlCon.Close();

                    }
                }
            }

            catch (Exception ex)
            {
                //strErrorMsg = ex.Message;
                throw;
            }
            return InsuranceNo;
        }


        public OrderRequestBO SaveOrderRequest(OrderRequestBO orderRequestBO)
        {
            OrderRequestBO ordBO = new OrderRequestBO();
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("sspOrderRequest", sqlcon))
                    {
                        sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.Add(new SqlParameter("@NoOfKits", orderRequestBO.NoOfKits));
                        sqlcmd.Parameters.Add(new SqlParameter("@KitType", orderRequestBO.KitType));
                        sqlcmd.Parameters.Add(new SqlParameter("@TenantID", orderRequestBO.TenantID));
                        sqlcmd.Parameters.Add(new SqlParameter("@CustomerNumber", orderRequestBO.CustomerNumber));
                        sqlcmd.Parameters.Add(new SqlParameter("@RequestID", orderRequestBO.RequestID));
                        //sqlcmd.Parameters.Add(new SqlParameter("@RequestDate", orderRequestBO.RequestDate));
                        //sqlcmd.Parameters.Add(new SqlParameter("@Status", orderRequestBO.Status));

                        // SqlDataReader reader = sqlcmd.ExecuteReader();
                        //reader.Close();
                        //reader = null;
                        SqlDataAdapter ad = new SqlDataAdapter(sqlcmd);
                        ad.Fill(set);
                        if (set.Tables.Count > 0)
                        {
                            ordBO.RequestID = set.Tables[1].Rows[0].ItemArray[0] == DBNull.Value ? 0 : Convert.ToInt32(set.Tables[1].Rows[0].ItemArray[0]);
                            ordBO.RequestNumber = set.Tables[0].Rows[0].ItemArray[0] == DBNull.Value ? string.Empty : Convert.ToString(set.Tables[0].Rows[0].ItemArray[0]);
                            //ordKitBO.OrderNumber = set.Tables[0].Rows[0].ItemArray[0] == DBNull.Value ? string.Empty : Convert.ToString(set.Tables[0].Rows[0].ItemArray[0]);
                        }
                    }
                    sqlcon.Close();

                }

            }
            catch (Exception ex)
            {
               //trErrorMsg = ex.Message;
                throw;
            }

            return ordBO;
        }

        public TenantDetailsBO getTenantDetails(int TenantID)
        {
            TenantDetailsBO tenantDetBO = new TenantDetailsBO();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("select * from tblTenantProfile where TenantID = @TenantID", connection);
                {
                    command.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tenantDetBO.TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"]);
                            tenantDetBO.TenantName = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]);
                            tenantDetBO.TenantType = reader["TenantType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantType"]);
                            tenantDetBO.ParentTenantID = reader["ParentTenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ParentTenantID"]);
                            tenantDetBO.TaxID = reader["TaxID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TaxID"]);
                            tenantDetBO.InCorpState = reader["InCorpState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InCorpState"]);
                            tenantDetBO.Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]);
                            tenantDetBO.Address2 = reader["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address2"]);
                            tenantDetBO.City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]);
                            tenantDetBO.State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]);
                            tenantDetBO.Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]);
                            tenantDetBO.Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]);
                            tenantDetBO.Website = reader["Website"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Website"]);
                            tenantDetBO.PrimaryPhone = reader["PrimaryPhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PrimaryPhone"]);
                            tenantDetBO.AlternatePhone = reader["AlternatePhone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AlternatePhone"]);
                            tenantDetBO.Fax = reader["Fax"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Fax"]);
                            tenantDetBO.Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]);
                            tenantDetBO.Logo = reader["Logo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Logo"]);
                            tenantDetBO.SmallLogo = reader["SmallLogo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SmallLogo"]);
                            tenantDetBO.DomainPrefix = reader["DomainPrefix"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DomainPrefix"]);
                            tenantDetBO.TenantAdminID = reader["TenantAdminID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantAdminID"]);
                            tenantDetBO.TenantStatus = reader["TenantStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantStatus"]);
                            tenantDetBO.ProfileCreatedOn = reader["ProfileCreatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["ProfileCreatedOn"]);
                            tenantDetBO.CreatedFromIP = reader["CreatedFromIP"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedFromIP"]);
                            tenantDetBO.GeoRegion = reader["GeoRegion"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GeoRegion"]);
                            tenantDetBO.BillAddress1 = reader["BillAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillAddress1"]);
                            tenantDetBO.BillAddress2 = reader["BillAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillAddress2"]);
                            tenantDetBO.BillCity = reader["BillCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillCity"]);
                            tenantDetBO.BillZipCode = reader["BillZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillZipCode"]);
                            tenantDetBO.BillState = reader["BillState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillState"]);
                            tenantDetBO.BillCountry = reader["BillCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BillCountry"]);
                            tenantDetBO.IsProfileComplete = reader["IsProfileComplete"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsProfileComplete"]);
                            tenantDetBO.ContactUsTelephone = reader["ContactUsTelephone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactUsTelephone"]);
                            tenantDetBO.ContactUsEmailID = reader["ContactUsEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ContactUsEmailID"]);

                        }
                    }
                    reader.Close();
                    connection.Close();

                }
            }
            return tenantDetBO;
        }
        public List<EmailEnablementImplementationBO> emailEnableImplementation(int TenantID, int EmailEnablementTypeID)
        {
            List<EmailEnablementImplementationBO> emailEnableImplementationBO = new List<EmailEnablementImplementationBO>();
            using (SqlConnection connect = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("select * from svwEmailEnablement where TenantID = @TenantID and EmailEnablementTypeID = @EmailEnablementTypeID", connect);
                {

                    command.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    command.Parameters.Add(new SqlParameter("@EmailEnablementTypeID", EmailEnablementTypeID));
                    command.CommandType = CommandType.Text;
                    connect.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
    }
}