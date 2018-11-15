using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using STOMS.BO;
using STOMS.Common;
using System.Data;
using System.Configuration;

namespace STOMS.DA
{
    public class OrderKitDA
    {
        private string _connectionString;

        public OrderKitDA()
        {
            _connectionString = Constant.DBConnectionString;
        }

        public List<CustomerBO> getOrderKitDetails(int TenantID, string OrderKitStatus)
        {
            List<CustomerBO> orderKitBO = new List<CustomerBO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string Query = "select * from svwOrderRequest  where TenantID=@TenantID and Status=@Status order by RequestDate desc";
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@Status", OrderKitStatus));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            orderKitBO.Add(new CustomerBO
                            {                                
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                RequestID = reader["RequestID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RequestID"]),
                                RequestNumber = reader["RequestNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestNumber"]),
                                FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                Message = reader["Message"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Message"]),
                                Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),
                                RequesterType = reader["RequesterType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequesterType"]),
                                City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]),
                                Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                                KitType = reader["KitType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitType"]),
                                Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                RequestDate = reader["RequestDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestDate"]),
                                Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"])
                            });
                        }
                    }
                    con.Close();
                    reader.Close();
                }
            }
            return orderKitBO;
        }

        public List<KitOrderBO> getRequestKitDetails(int TenantID, int RequestID,int CustomerID,string OrderKitStatus)
        {
            List<KitOrderBO> orderKitBO = new List<KitOrderBO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string Query = "select * from svwOrderRequestKit  where TenantID=@TenantID and  Status=@Status and CustID=@CustID  and RequestID=@RequestID ";
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@Status", OrderKitStatus));
                    cmd.Parameters.Add(new SqlParameter("@CustID", CustomerID));
                    cmd.Parameters.Add(new SqlParameter("@RequestID", RequestID));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            orderKitBO.Add(new KitOrderBO
                            {
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                RequestID = reader["RequestID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RequestID"]),
                                RequestNumber = reader["RequestNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestNumber"]),
                                KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]),
                                KitNumber= reader["KitNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitNumber"]),
                                LabelNumber= reader["LabelNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LabelNumber"]),
                                LabelGenaratedOn= reader["LabelGeneratedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LabelGeneratedOn"]),
                                ReUseCount = reader["ReUseCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReUseCount"])
                            });
                        }
                    }
                    con.Close();
                    reader.Close();
                }
            }
            return orderKitBO;
        }
        public OrderKitBO GetOrderKitDetails(string orderKitID)
        {
            OrderKitBO orderKitBO = new OrderKitBO();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from stblOrderKit where OrderKitID=@OrderKitID", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@OrderKitID", orderKitID));
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                orderKitBO.FirstName = reader["FirstName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["FirstName"]);
                                orderKitBO.LastName = reader["LastName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["LastName"]);
                                orderKitBO.OrgName = (reader["OrgName"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["OrgName"]);
                                orderKitBO.RequesterType = (reader["RequesterType"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["RequesterType"]);
                                orderKitBO.Address = (reader["Address"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Address"]);
                                orderKitBO.City = (reader["City"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["City"]);
                                orderKitBO.State = (reader["State"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["State"]);
                                orderKitBO.Country = (reader["Country"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Country"]);
                                orderKitBO.Zip = (reader["Zip"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Zip"]);
                                orderKitBO.Phone = (reader["Phone"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Phone"]);
                                orderKitBO.Email = (reader["Email"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Email"]);
                                orderKitBO.NoOfKits = (reader["NoOfKits"]) == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]);
                                orderKitBO.Message = (reader["Message"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Message"]);
                                orderKitBO.Status = (reader["Status"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"]);
                                orderKitBO.KitOrderID = (reader["OrderKitID"]) == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderKitID"]);
                                orderKitBO.TenantID = (reader["TenantID"]) == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]);
                                orderKitBO.Date = (reader["OrderedOn"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["OrderedOn"]);
                                orderKitBO.RequestNumber = (reader["OrderNumber"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["OrderNumber"]);
                                orderKitBO.KitType = (reader["KitType"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["KitType"]);
                                orderKitBO.TrackNumber = (reader["TrackNumber"]) == DBNull.Value ? string.Empty : Convert.ToString(reader["TrackNumber"]);
                                orderKitBO.DeleveryOn = (reader["DeleveryOn"]) == DBNull.Value ? string.Empty : Convert.ToString(reader["DeleveryOn"]);
                                orderKitBO.LabelGeneratedOn = (reader["LabelGeneratedOn"]) == DBNull.Value ? string.Empty : Convert.ToString(reader["LabelGeneratedOn"]);
                                orderKitBO.PickedUpOn = (reader["PickedUpOn"]) == DBNull.Value ? string.Empty : Convert.ToString(reader["PickedUpOn"]);
                            }
                        }
                        con.Close();
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return orderKitBO;
        }

        public List<AddressBO> getAddresses(int TenantID, string AddressStatus, bool isDefault)
        {
            List<AddressBO> abo = new List<AddressBO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from stblCourierSenderAddress where TenantID=@TenantID and AddressStatus=@AddressStatus and isDefault=@isDefault", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    cmd.Parameters.Add(new SqlParameter("@AddressStatus", AddressStatus));
                    cmd.Parameters.Add(new SqlParameter("@isDefault", isDefault));
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            abo.Add(new AddressBO()
                            {
                                FirstName = (reader["ShipperName"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["ShipperName"]),
                                CompanyName = (reader["CompanyName"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["CompanyName"]),
                                Address1 = (reader["Address1"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Address1"]),
                                Address2 = (reader["Address2"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Address2"]),
                                Address3 = (reader["Address3"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Address3"]),
                                AddressStatus = (reader["AddressStatus"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["AddressStatus"]),
                                CountryCode = (reader["CountryCode"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["CountryCode"]),
                                City = (reader["City"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["City"]),
                                State = (reader["State"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["State"]),
                                Country = (reader["CountryName"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["CountryName"]),
                                Zip = (reader["Zip"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Zip"]),
                                Telephone = (reader["PhoneNumber"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["PhoneNumber"]),
                                //  Email = (reader["Email"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["Email"]),
                                CourierShipperID = (reader["CourierShipperID"]) == DBNull.Value ? 0 : Convert.ToInt32(reader["CourierShipperID"]),
                                isDefault = (reader["isDefault"]) == DBNull.Value ? false : Convert.ToBoolean(reader["isDefault"]),
                            });
                        }
                    }
                }
            }
            return abo;
        }

        public OrderKitBO SaveOrderKitManual(OrderKitBO KitOrderManual)
        {
            OrderKitBO ordKitBO = new OrderKitBO();
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sspSaveOrderKitManual", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@FirstName", KitOrderManual.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", KitOrderManual.LastName));
                        cmd.Parameters.Add(new SqlParameter("@RequesterType", KitOrderManual.RequesterType));
                        cmd.Parameters.Add(new SqlParameter("@OrgName", KitOrderManual.OrgName));
                        cmd.Parameters.Add(new SqlParameter("@Address", KitOrderManual.Address));
                        cmd.Parameters.Add(new SqlParameter("@City", KitOrderManual.City));
                        cmd.Parameters.Add(new SqlParameter("@State", KitOrderManual.State));
                        cmd.Parameters.Add(new SqlParameter("@Country", KitOrderManual.Country));
                        cmd.Parameters.Add(new SqlParameter("@Zip", KitOrderManual.Zip));
                        cmd.Parameters.Add(new SqlParameter("@KitType", KitOrderManual.KitType));
                        cmd.Parameters.Add(new SqlParameter("@Telephone", KitOrderManual.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Email", KitOrderManual.Email));
                        cmd.Parameters.Add(new SqlParameter("@NoOfKits", KitOrderManual.NoOfKits));
                        cmd.Parameters.Add(new SqlParameter("@Message", KitOrderManual.Message));
                        cmd.Parameters.Add(new SqlParameter("@TenantID", KitOrderManual.TenantID));
                        cmd.Parameters.Add(new SqlParameter("@orderKitStatus", KitOrderManual.Status));
                        cmd.Parameters.Add(new SqlParameter("@OrderKitID", KitOrderManual.KitOrderID));

                        con.Open();

                        //SqlDataReader readData = cmd.ExecuteReader();
                        //if (readData.HasRows)
                        //{
                        //    readData.Read();
                        //    Number = readData[0] == DBNull.Value ? 0 : Convert.ToInt32(readData[0]);
                        //}
                        SqlDataAdapter ad = new SqlDataAdapter(cmd);
                        ad.Fill(set);
                        if (set.Tables.Count > 1)
                        {
                            ordKitBO.KitOrderID = set.Tables[1].Rows[0].ItemArray[0] == DBNull.Value ? 0 : Convert.ToInt32(set.Tables[1].Rows[0].ItemArray[0]);
                            ordKitBO.RequestNumber = set.Tables[0].Rows[0].ItemArray[0] == DBNull.Value ? string.Empty : Convert.ToString(set.Tables[0].Rows[0].ItemArray[0]);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exx)
            {

            }
            return ordKitBO;
        }

        public List<KitTypeConfigurationBO> getKitTypeManual(int TenantID)
        {
            List<KitTypeConfigurationBO> kitTypeBO = new List<KitTypeConfigurationBO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from svwKitType where TenantID=@TenantID", con);
                cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kitTypeBO.Add(new KitTypeConfigurationBO
                        {
                            KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]),
                            KitName = reader["KitName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitName"]),
                        });
                    }
                }
                con.Close();
                reader.Close();
            }
            return kitTypeBO;
        }

        public List<SpecimenReportBO> getSpecimenReport(int TenantID, string FromDate = "", string ToDate = "", string Status = "")
        {
            List<SpecimenReportBO> reportBO = new List<SpecimenReportBO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (FromDate != "" && ToDate != "" && Status == "Select Status")
                    {
                        cmd.CommandText = "select * from stblSpecimenInfo where TenantID=@TenantID and CreatedOn between @FromDate and @ToDate";

                        cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        cmd.Parameters.Add(new SqlParameter("@FromDate", FromDate));
                        cmd.Parameters.Add(new SqlParameter("@ToDate", ToDate));
                    }

                    else if (FromDate == "" && ToDate == "" && Status != "")
                    {
                        cmd.CommandText = "select * from stblSpecimenInfo where TenantID=@TenantID and SpecimenStatus=@SpecimenStatus";

                        cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        cmd.Parameters.Add(new SqlParameter("@SpecimenStatus", Status));
                    }

                    else if (FromDate != "" && ToDate != "" && Status != "")
                    {
                        cmd.CommandText = "select * from stblSpecimenInfo where TenantID = @TenantID and CreatedOn between @FromDate and @ToDate and SpecimenStatus=@SpecimenStatus";
                        cmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        cmd.Parameters.Add(new SqlParameter("@FromDate", FromDate));
                        cmd.Parameters.Add(new SqlParameter("@ToDate", ToDate));
                        cmd.Parameters.Add(new SqlParameter("@SpecimenStatus", Status));
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int customerID = 0;
                            SpecimenInfoBO sbo = new SpecimenInfoBO();
                            //                            CustomerBO cbo = new CustomerBO();
                            List<CustomerBO> cbo = new List<CustomerBO>();
                            PatientBO pbo = new PatientBO();
                            sbo.SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? String.Empty : Convert.ToString(reader["SpecimenNumber"]);
                            sbo.SpecimentType = reader["SpecimenType"] == DBNull.Value ? String.Empty : Convert.ToString(reader["SpecimenType"]);
                            sbo.SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? String.Empty : Convert.ToString(reader["SpecimenStatus"]);

                            //pbo.PatientName =((reader["FirstName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["FirstName"])) +" "+(reader["LastName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["LastName"])));
                            // pbo.Gender = reader["Gender"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Gender"]);

                            //cbo.CustomerName = (reader["CustomerName"]) == DBNull.Value ? String.Empty : Convert.ToString(reader["CustomerName"]);
                            //
                            /*Custoer Details*/

                            customerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]);
                       
                            if (customerID > 0) {
                                cbo = new CustomerDA().GetCustomer(customerID.ToString());
                            }
                            reportBO.Add(new SpecimenReportBO
                            {
                                specimenBO = sbo,
                                patientBO = pbo,
                                customerBO = cbo.Count>0? cbo[0]:new CustomerBO(),
                            });
                        }

                    }
                    con.Close();
                }

            }
            return reportBO;

        }

        public void updateCourierInfoToOrderKit(OrderKitBO obo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblOrderKit set  TrackNumber=@TrackNumber, DeleveryOn=@DeleveryOn, LabelGeneratedOn=@LabelGeneratedOn , OrderKitStatus='Label Generated' where OrderKitID=@OrderKitID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@TrackNumber", obo.TrackNumber));
                    cmd.Parameters.Add(new SqlParameter("@DeleveryOn", obo.DeleveryOn));
                    cmd.Parameters.Add(new SqlParameter("@LabelGeneratedOn", obo.LabelGeneratedOn));
                    // cmd.Parameters.Add(new SqlParameter("@PickedUpOn", obo.PickedUpOn));
                    cmd.Parameters.Add(new SqlParameter("@OrderKitID", obo.KitOrderID));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void updateCourierInfoToOrderKit_Pickuped(OrderKitBO obo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblOrderKit set PickedUpOn=@PickedUpOn, OrderKitStatus='Kit Dispatched' where OrderKitID=@OrderKitID", con))
                {

                    cmd.Parameters.Add(new SqlParameter("@PickedUpOn", obo.PickedUpOn));
                    cmd.Parameters.Add(new SqlParameter("@OrderKitID", obo.KitOrderID));
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public List<KitOrderBO> PopAssignedKits(int TenantID, int NoOfKits, string Status)
        {
            List<KitOrderBO> assignedkit = new List<KitOrderBO>();

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select top(@NoOfKits) * from stblMasterKit where status=@Status and TenantID=@TenantID", sqlcon))
                {
                    sqlcomm.CommandType = CommandType.Text;

                    sqlcomm.Parameters.Add(new SqlParameter("@Status", Status));
                    sqlcomm.Parameters.Add(new SqlParameter("@NoOfkits", NoOfKits));
                    sqlcomm.Parameters.Add(new SqlParameter("@TenantID", TenantID));

                    //sqlcomm.Parameters.Add(new SqlParameter("@ReUseCount",ReUseCount));
                    sqlcon.Open();
                    SqlDataReader reader = sqlcomm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            assignedkit.Add(new KitOrderBO
                            {
                                KitID = reader["KitID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitID"]),
                                KitNumber = reader["KitNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitNumber"]),
                                ReUseCount = reader["ReUseCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReUseCount"]),

                                //NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                            });
                        }
                    }

                    sqlcon.Close();
                    reader.Close();
                }

                return assignedkit;
            }

        }


        public List<KitOrderBO> saveOrderkitdetails(int RequestID, string KitID)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1]
            {
                new DataColumn("KitID", typeof(string))
            });

            List<KitOrderBO> order = new List<KitOrderBO>();
            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlBulkCopy sqlbulk = new SqlBulkCopy(sqlcon))
                {
                    sqlbulk.DestinationTableName = "dbo.stblOrderKit26";
                    sqlbulk.ColumnMappings.Add("KitID", "KitID");
                    sqlcon.Open();
                    sqlbulk.WriteToServer(dt);
                    sqlcon.Close();
                }
            }
            return order;
        }

        public void updateMasterKit(List<KitOrderBO> kbo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblMasterKit set  Status=@Status where KitID=@KitID", con))
                {
                    con.Open();
                    foreach (var item in kbo)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Status", item.Status));
                        cmd.Parameters.Add(new SqlParameter("@KitID", item.KitID));

                        cmd.CommandType = CommandType.Text;
                        
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                       
                    }
                    con.Close();

                }
            }

        }

        public void updateKitRequestStatus(int RequestID, string Status )
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblOrderRequest set  Status=@Status where RequestID=@RequestID", con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@Status", Status));
                    cmd.Parameters.Add(new SqlParameter("@RequestID", RequestID));

                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    
                    con.Close();

                }
            }

        }



        public List<CustomerBO> getviewOrderrequest(int TenantID, string Status = "Kit Requested")
        {
            List<CustomerBO> objorderreq = new List<CustomerBO>();

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                //using (SqlCommand sqlcomm = new SqlCommand("sspViewOrderRequest", sqlcon))
                using (SqlCommand sqlcomm = new SqlCommand("select * from svwOrderRequest where TenantID=@TenantID and Status=@Status order by RequestDate desc", sqlcon))

                {
                    sqlcomm.CommandType = CommandType.Text;
                    sqlcomm.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlcomm.Parameters.Add(new SqlParameter("@Status", Status));

                    //sqlcomm.Parameters.Add(new SqlParameter("@ReUseCount",ReUseCount));
                    sqlcon.Open();
                    SqlDataReader reader = sqlcomm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            objorderreq.Add(new CustomerBO
                            {
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                RequestID = reader["RequestID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RequestID"]),
                                RequestNumber = reader["RequestNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestNumber"]),
                                FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                Message = reader["Message"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Message"]),
                                Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),
                                RequesterType = reader["RequesterType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequesterType"]),
                                City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]),
                                Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                                KitType = reader["KitType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitType"]),
                                Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                RequestDate = reader["RequestDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestDate"]),
                                Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                Address1=reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"])
                                //NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                            });
                        }
                    }

                    sqlcon.Close();
                    reader.Close();
                }

                return objorderreq;

            }
        }

        public List<CustomerBO> getRequestDetails(int RequestID)
        {
            List<CustomerBO> objReq = new List<CustomerBO>();

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                //using (SqlCommand sqlcomm = new SqlCommand("sspViewOrderRequest", sqlcon))
                using (SqlCommand sqlcomm = new SqlCommand("select * from svwOrderRequest where RequestID=@RequestID", sqlcon))
                {
                    sqlcomm.CommandType = CommandType.Text;
                    sqlcomm.Parameters.Add(new SqlParameter("@RequestID", RequestID));

                    //sqlcomm.Parameters.Add(new SqlParameter("@ReUseCount",ReUseCount));
                    sqlcon.Open();
                    SqlDataReader reader = sqlcomm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objReq.Add(new CustomerBO
                            {
                                TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                RequestID = reader["RequestID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RequestID"]),
                                RequestNumber = reader["RequestNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestNumber"]),
                                FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                Message = reader["Message"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Message"]),
                                Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),
                                RequesterType = reader["RequesterType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequesterType"]),
                                City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                Status = reader["Status"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Status"]),
                                Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                                KitType = reader["KitType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KitType"]),
                                Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                RequestDate = reader["RequestDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequestDate"]),
                                Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                CustNumber= reader["CustNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustNumber"]),
                                CustID= reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"])

                                //NoOfKits = reader["NoOfKits"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfKits"]),
                            });
                        }
                    }

                    sqlcon.Close();
                    reader.Close();
                }


                return objReq;


            }


        }
        public void SaveAssignKits(DataTable dt)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con))
                {

                    bulkcopy.DestinationTableName = "dbo.stblOrderKit";

                    bulkcopy.ColumnMappings.Add("KitID", "KitID");
                    bulkcopy.ColumnMappings.Add("CustomerID", "CustomerID");
                    bulkcopy.ColumnMappings.Add("TenantID", "TenantID");
                    bulkcopy.ColumnMappings.Add("RequestID", "RequestID");
                    con.Open();
                    bulkcopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }

        public void UpdateKitLabelDetails(string LabelNumber,int RequestId,int KitID)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update  stblOrderKit set LabelNumber= @LabelNumber, Couriertype='FeDex', LabelGeneratedOn=getdate() where RequestID=@RequestID and kitID=@KitID", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@LabelNumber", LabelNumber));
                    cmd.Parameters.Add(new SqlParameter("@RequestID", RequestId));
                    cmd.Parameters.Add(new SqlParameter("@KitID", KitID));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void UpdateLabelStatusDetails(int RequestId, string Status)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update stblOrderRequest set status=@Status where RequestID=@RequestID", con))
                {
                    cmd.CommandType = CommandType.Text;                    
                    cmd.Parameters.Add(new SqlParameter("@RequestID", RequestId));
                    cmd.Parameters.Add(new SqlParameter("@Status", Status));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
    }




