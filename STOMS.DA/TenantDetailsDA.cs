using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using STOMS.DA;
using STOMS.Common;
using STOMS.BO;

namespace STOMS.DA
{
    public class TenantDetailsDA
    {
        private string _ErrMsg;
        public TenantDetailsBO getTenantDetails(int TenantID)
        {
            TenantDetailsBO tenantDetBO = new TenantDetailsBO();
            using (SqlConnection connection = new SqlConnection(Constant.DBConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from tblTenantProfile where TenantID = @TenantID", connection);
                {
                    command.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while(reader.Read())
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
        
    }
}
