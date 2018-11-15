using STOMS.BO;
using STOMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace STOMS.DA
{
    public class OrderInvDA
    {
        private string strErrorMsg;
        SqlConnection sqlCon = null; 
              
        public List<OrderBO> getOrderList(int OrgID, int ActivityMonth, int ActivityYear)
        {
            List<OrderBO> objOrder = new List<OrderBO>();
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);

                //using (SqlCommand sqlCmd = new SqlCommand("select * from svwOrder where OrgID = @OrgID", sqlCon))
                using (SqlCommand sqlCmd = new SqlCommand("select* from svwOrderDetails where TenantID =@OrgID", sqlCon))
                    
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@OrgID", OrgID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objOrder.Add(new OrderBO
                            {
                                OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                OrderDate = reader["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["OrderDate"]),
                                //OrderAmount = reader["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotOrderCost"]),
                                OrderStatus = reader["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderStatus"]),
                                //CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                //SampleCount = reader["CountOfSamples"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountOfSamples"]),
                                CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                            });
                        }
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
            return objOrder;
        }

        public List<OrderBO> getOrderSarchResult(string searchString)
        {
            List<OrderBO> objOrder = new List<OrderBO>();
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);

                using (SqlCommand sqlCmd = new SqlCommand("sspGetSearchResult", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@SearchType", "Order"));
                    sqlCmd.Parameters.Add(new SqlParameter("@SearchText", searchString));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objOrder.Add(new OrderBO
                            {
                                OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                OrderDate = reader["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["OrderDate"]),
                                OrderAmount = reader["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotOrderCost"]),
                                OrderStatus = reader["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderStatus"]),
                                CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                SampleCount = reader["CountOfSamples"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountOfSamples"]),
                                CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                            });
                        }
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
            return objOrder;
        }

        public List<OrderBO> getOrder(string OrderID)
        {
            List<OrderBO> objOrder = new List<OrderBO>();
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
                                objOrder.Add(new OrderBO
                                {
                                    OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                    OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                    OrderDate = reader["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["OrderDate"]),
                                    OrderAmount = reader["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotOrderCost"]),
                                    OrderStatus = reader["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderStatus"]),
                                    CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                    SampleCount = reader["CountOfSamples"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountOfSamples"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),

                                    ShipOption = reader["ShipOption"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ShipOption"]),
                                    DeliveryEmail = reader["DeliveryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DeliveryEmail"]),
                                    ShipName = reader["SName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SName"]),
                                    ShipAddress1 = reader["SAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SAddress1"]),
                                    ShipAddress2 = reader["SAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SAddress2"]),
                                    ShipCity = reader["SCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SCity"]),
                                    ShipZipCode = reader["SZipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SZipcode"]),
                                    ShipState = reader["SState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SState"]),
                                    ShipCountry = reader["SCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SCountry"]),

                                    IsFolateBinding = reader["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBindTest"]),
                                    IsFolateBlocking = reader["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBlockTest"])
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
            return objOrder;
        }

        public List<OrderBO> getPhyOrderList(int CustID)
        {
            List<OrderBO> objOrder = new List<OrderBO>();
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);

                using (SqlCommand sqlCmd = new SqlCommand("select * from stblOrder where CustID=@CustID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@CustID", CustID));
                    
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objOrder.Add(new OrderBO
                            {
                                OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                OrderDate = reader["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["OrderDate"]),
                                OrderAmount = reader["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotOrderCost"]),
                                OrderStatus = reader["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderStatus"]),
                                CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                //SampleCount = reader["CountOfSamples"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountOfSamples"]),
                                //CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                            });
                        }
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
            return objOrder;
        }

        //public List<OrderBO> getPhyOrder(int CustID)
        //{
        //    List<OrderBO> objOrder = new List<OrderBO>();
        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("select * from stblOrder where CustID=@CustID",sqlCon))
        //            {
        //                sqlCon.Open();
        //                sqlCmd.CommandType = CommandType.Text;
        //                sqlCmd.Parameters.Add(new SqlParameter("@CustID", "CustID"));

        //                SqlDataReader reader = sqlCmd.ExecuteReader();
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        objOrder.Add(new OrderBO
        //                        {
        //                            OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
        //                            OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
        //                            OrderDate = reader["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["OrderDate"]),
        //                            OrderAmount = reader["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotOrderCost"]),
        //                            OrderStatus = reader["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderStatus"]),
        //                            CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
        //                            SampleCount = reader["CountOfSamples"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CountOfSamples"]),
        //                            CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),

        //                            ShipOption = reader["ShipOption"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ShipOption"]),
        //                            DeliveryEmail = reader["DeliveryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DeliveryEmail"]),
        //                            ShipName = reader["SName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SName"]),
        //                            ShipAddress1 = reader["SAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SAddress1"]),
        //                            ShipAddress2 = reader["SAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SAddress2"]),
        //                            ShipCity = reader["SCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SCity"]),
        //                            ShipZipCode = reader["SZipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SZipcode"]),
        //                            ShipState = reader["SState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SState"]),
        //                            ShipCountry = reader["SCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SCountry"]),

        //                            IsFolateBinding = reader["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBindTest"]),
        //                            IsFolateBlocking = reader["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBlockTest"])
        //                        });
        //                    }
        //                }
        //                sqlCon.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorMsg = ex.Message;
        //        throw;
        //    }
        //    return objOrder;
        //}


        public int SaveOrderDA(OrderBO oOrder)
        {
            int rtnval = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveOrder", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@RequestID", oOrder.OrderID));
                        sqlCmd.Parameters.Add(new SqlParameter("@orderDate", oOrder.OrderDate));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustID", oOrder.CustomerID));

                        sqlCmd.Parameters.Add(new SqlParameter("@DeliveryOption", oOrder.ShipOption));
                        sqlCmd.Parameters.Add(new SqlParameter("@DeliveryEmail", oOrder.DeliveryEmail));
                        sqlCmd.Parameters.Add(new SqlParameter("@SName", oOrder.ShipName));
                        sqlCmd.Parameters.Add(new SqlParameter("@SAddress1", oOrder.ShipAddress1));
                        sqlCmd.Parameters.Add(new SqlParameter("@SAddress2", oOrder.ShipAddress2));
                        sqlCmd.Parameters.Add(new SqlParameter("@SCity", oOrder.ShipCity));
                        sqlCmd.Parameters.Add(new SqlParameter("@SState", oOrder.ShipState));
                        sqlCmd.Parameters.Add(new SqlParameter("@SZipCode", oOrder.ShipZipCode));
                        sqlCmd.Parameters.Add(new SqlParameter("@SCountry", oOrder.ShipCountry));

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

        public int SaveOrderDA(CustomerBO objCust)
        {
            int rtnval = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveCustomer", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@CustmerID", objCust.CustomerID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustomerName", objCust.CustomerName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Address1", objCust.Address1));
                        sqlCmd.Parameters.Add(new SqlParameter("@Address2", objCust.Address2));
                        sqlCmd.Parameters.Add(new SqlParameter("@City", objCust.City));
                        sqlCmd.Parameters.Add(new SqlParameter("@State", objCust.State));
                        sqlCmd.Parameters.Add(new SqlParameter("@Zip", objCust.Zipcode));
                        sqlCmd.Parameters.Add(new SqlParameter("@Country", objCust.Country));
                        sqlCmd.Parameters.Add(new SqlParameter("@Email", objCust.Email));
                        sqlCmd.Parameters.Add(new SqlParameter("@Phone", objCust.Phone));
                        sqlCmd.Parameters.Add(new SqlParameter("@Fax", objCust.Fax));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustStatus", objCust.Status));

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

        public void SaveOrderDA(List<PatientBO> objPatientBO, bool IsFolateBinding, bool IsFolateBlocking)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSavePatient", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        foreach (PatientBO oPat in objPatientBO)
                        {
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.Add(new SqlParameter("@PatientName", oPat.PatientName));
                            sqlCmd.Parameters.Add(new SqlParameter("@Gender", oPat.Gender));
                            sqlCmd.Parameters.Add(new SqlParameter("@DOB", oPat.DOB));
                            sqlCmd.Parameters.Add(new SqlParameter("@Diagnosis", oPat.Diagnosis));
                            sqlCmd.Parameters.Add(new SqlParameter("@RequestID", oPat.OrderID));
                            sqlCmd.Parameters.Add(new SqlParameter("@IsBlock", IsFolateBlocking));
                            sqlCmd.Parameters.Add(new SqlParameter("@IsBind", IsFolateBinding));
                            sqlCmd.Parameters.Add(new SqlParameter("@PatientID", oPat.PatientID));
                            SqlDataReader reader = sqlCmd.ExecuteReader();
                            reader.Close();
                            reader = null;
                        }
                    }
                    sqlCon.Close();
                }
            }

            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }

        }

        public List<TestResultBO> getSampleTestList(string OrderID)
        {
            List<TestResultBO> objOrder = new List<TestResultBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwSampleTestTrack where OrderID = " + OrderID, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PatientBO oPat = new PatientBO();
                                oPat.PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]);
                                oPat.PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]);
                                objOrder.Add(new TestResultBO
                                {
                                    TTrackID = reader["TTrackID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TTrackID"]),
                                    DateAssayBIN = reader["DateAssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBIN"]),
                                    DateAssayBLO = reader["DateAssayBLO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBLO"]),
                                    ResultBIN = reader["ResultBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBIN"]),
                                    ResultBL = reader["ResultBL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBL"]),
                                    SampleBarCode = reader["SampleBarCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleBarCode"]),
                                    SampleStatus = reader["SampleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleStatus"]),
                                    ResultSentDate = reader["ResultSentDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultSentDate"]),
                                    IsFolateBinding = reader["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBindTest"]),
                                    IsFolateBlocking = reader["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBlockTest"]),
                                    oPatient = oPat
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
            return objOrder;
        }

        public List<OrderBO> getViewOrder(string OrderID)
        {
            List<OrderBO> objOrder = new List<OrderBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwPhysicionOrderview where OrderID =@OrderID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objOrder.Add(new OrderBO
                                {
                                    OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),

                                objCustomer = (new CustomerBO {

                                    CustNumber = reader["CustNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustNumber"]),
                                        CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                        Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    }),

                                    specimenInfoBO = (new SpecimenInfoBO {
                                        SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                        SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                        SpecimentType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                        TestType= reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"]),
                                        PaymentMode= reader["PaymentMode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PaymentMode"]),
                                        SpecimenID= reader["SpecimenID"] == DBNull.Value ?0 : Convert.ToInt32(reader["SpecimenID"]),

                                    }),
                                    Ordpatient=(new PatientBO {

                                        FirstName= reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName= reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        EmailID= reader["PatientEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientEmailID"]),
                                    }),
                                    OrdDocument=(new DocumentBO
                                    {
                                        DocNumber=reader["DocNumber"] == DBNull.Value ? string.Empty: Convert.ToString(reader["DocNumber"]),
                                        DocumentName=reader["DocumentName"]== DBNull.Value?string.Empty:Convert.ToString(reader["DocumentName"]),
                                        DocType= reader["DocType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DocType"])
                                        //OrgDocName= reader["OrgDocName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrgDocName"])
                                    })

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
            return objOrder;
        }


        public List<TestResultsBO> getTestResult(int specimenID)
        {
            List<TestResultsBO> testsresult = new List<TestResultsBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwPhyViewResult where SpecimenID=@specimenID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@specimenID", specimenID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                testsresult.Add(new TestResultsBO
                                {
                                 PerformedBy=reader["PerformedBy"]==DBNull.Value?String.Empty:Convert.ToString(reader["PerformedBy"]),
                                 ResultDocID= reader["ResultDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ResultDocID"]),
                                 IsReleased = reader["IsReleased"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsReleased"]),
                                 objDocumentBO=(new DocumentBO
                                 {
                                     DocNumber= reader["DocNumber"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocNumber"]),
                                     DocType= reader["DocType"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocType"]),
                                     DocumentName= reader["DocumentName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocumentName"]),
                                     DocStatus= reader["DocStatus"] == DBNull.Value ? String.Empty : Convert.ToString(reader["DocStatus"]),
                                     OrgDocName= reader["OrgDocName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["OrgDocName"]),
                                     TokenID= reader["TokenID"] == DBNull.Value ? String.Empty : Convert.ToString(reader["TokenID"]),
                                 }),
                                 objpayment=(new PaymentBO
                                 {
                                     PaymentStatus=reader["PaymentStatus"]==DBNull.Value?string.Empty:Convert.ToString(reader["PaymentStatus"]),
                                     CreditDetails=(new CreditCardBO
                                     {
                                         CardType = reader["CardType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CardType"]),
                                         HolderName= reader["HolderName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["HolderName"]),
                                     })
                                 })

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
            return testsresult;
        }



        public List<TestResultBO> getSampleTestList(bool isInTest = true)
        {
            string sSql = "select * from svwSampleTestTrack where coalesce(SampleStatus,'') <> 'Completed'";
            if (!isInTest)
                sSql = "select * from svwSampleTestTrack";

            List<TestResultBO> objOrder = new List<TestResultBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sSql, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PatientBO oPat = new PatientBO();
                                oPat.PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]);
                                oPat.PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]);

                                objOrder.Add(new TestResultBO
                                {
                                    TTrackID = reader["TTrackID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TTrackID"]),
                                    OrderCustomer = reader["CustOrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustOrderNumber"]),
                                    DateAssayBIN = reader["DateAssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBIN"]),
                                    DateAssayBLO = reader["DateAssayBLO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBLO"]),
                                    ResultBIN = reader["ResultBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBIN"]),
                                    ResultBL = reader["ResultBL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBL"]),
                                    SampleBarCode = reader["SampleBarCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleBarCode"]),
                                    SampleStatus = reader["SampleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    DateReceived = reader["DateReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateReceived"]),
                                    ResultSentDate = reader["ResultSentDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultSentDate"]),
                                    IsFolateBinding = reader["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBindTest"]),
                                    IsFolateBlocking = reader["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBlockTest"]),
                                    oPatient = oPat
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
            return objOrder;
        }

        public List<TestResultBO> getSampleTestListByTT(string TTrackID, bool isAllTrack = true)
        {
            string sSql = "select * from svwSampleTestTrack where OrderID in (select OrderID from svwSampleTestTrack where TTrackID=" + TTrackID + ")";
            if (!isAllTrack)
                sSql = "select * from svwSampleTestTrack where TTrackID=" + TTrackID;

            List<TestResultBO> objOrder = new List<TestResultBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sSql, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PatientBO oPat = new PatientBO();
                                oPat.PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]);
                                oPat.PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]);
                                oPat.DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]);
                                oPat.Diagnosis = reader["Diagnosis"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Diagnosis"]);

                                objOrder.Add(new TestResultBO
                                {
                                    TTrackID = reader["TTrackID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TTrackID"]),
                                    DateAssayBIN = reader["DateAssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBIN"]),
                                    DateAssayBLO = reader["DateAssayBLO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateAssayBLO"]),
                                    ResultBIN = reader["ResultBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBIN"]),
                                    ResultBL = reader["ResultBL"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultBL"]),
                                    SampleBarCode = reader["SampleBarCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleBarCode"]),
                                    SampleStatus = reader["SampleStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleStatus"]),
                                    ResultSentDate = reader["ResultSentDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultSentDate"]),
                                    oPatient = oPat
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
            return objOrder;
        }

        #region Customer

        #endregion

        public List<TestResultBO> GetPatientByOrderID(string OrderID)
        {
            List<TestResultBO> objOrder = new List<TestResultBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwSampleTestTrack where OrderID = " + OrderID, sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PatientBO oPat = new PatientBO();
                                oPat.PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]);
                                oPat.PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]);
                                oPat.Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]);
                                oPat.DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]);
                                oPat.Diagnosis = reader["Diagnosis"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Diagnosis"]);
                                objOrder.Add(new TestResultBO
                                {
                                    IsFolateBinding = reader["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBindTest"]),
                                    IsFolateBlocking = reader["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(reader["isBlockTest"]),
                                    oPatient = oPat
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
            return objOrder;
        }

        public List<InvoiceBO> GetInvoice()
        {
            List<InvoiceBO> objInv = new List<InvoiceBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwInvoice", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objInv.Add(new InvoiceBO
                                {
                                    InvoiceID = reader["InvoiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["InvoiceID"]),
                                    InvNumber = reader["InvNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvNumber"]),
                                    InvDate = reader["InvDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvDate"]),
                                    InvAmount = reader["InvAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InvAmount"]),
                                    OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                    OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                    InvStatus = reader["InvStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvStatus"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"])
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
            return objInv;
        }

        public List<InvoiceBO> GetInvoice(string IDValue, string IDType)
        {
            SqlConnection sqlCon = null;
            List<InvoiceBO> objInv = new List<InvoiceBO>();
            string sSql = "select * from svwInvoice where OrderID=@ID";
            if (IDType.ToUpper() == "INVOICE")
                sSql = "select * from svwInvoice where InvoiceID=@ID";
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sSql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@ID", IDValue));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objInv.Add(new InvoiceBO
                            {
                                InvoiceID = reader["InvoiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["InvoiceID"]),
                                InvNumber = reader["InvNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvNumber"]),
                                InvDate = reader["InvDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvDate"]),
                                InvAmount = reader["InvAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InvAmount"]),
                                OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                InvStatus = reader["InvStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvStatus"]),
                                CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"])
                            });
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return objInv;
        }

        public List<InvoiceBO> GetInvoiceDetail(string IDValue, string IDType)
        {
            SqlConnection sqlCon = null;
            List<InvoiceBO> objInv = new List<InvoiceBO>();
            List<InvoiceDetailBO> oInvDetails = new List<InvoiceDetailBO>();

            string sSql = string.Empty;

            switch (IDType.ToUpper())
            {
                case "INVOICE":
                    sSql = "select * from svwInvoiceDetail where InvoiceID=@ID";
                    break;
                case "ORDER":
                    sSql = "select * from svwInvoiceDetail where OrderID=@ID";
                    break;
                case "INVNUMBER":
                    sSql = "select * from svwInvoiceDetail where invNumber=@ID";
                    break;
            }

            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand(sSql, sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@ID", IDValue));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        bool isFirst = true;
                        while (reader.Read())
                        {
                            if (isFirst)
                            {
                                objInv.Add(new InvoiceBO
                                {
                                    InvoiceID = reader["InvoiceID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["InvoiceID"]),
                                    InvNumber = reader["InvNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvNumber"]),
                                    InvDate = reader["InvDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvDate"]),
                                    InvAmount = reader["InvAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["InvAmount"]),
                                    OrderID = reader["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OrderID"]),
                                    //OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                    InvStatus = reader["InvStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InvStatus"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                    InvFile = reader["invFile"] == DBNull.Value ? string.Empty : Convert.ToString(reader["invFile"])
                                });
                                isFirst = false;
                            }
                            oInvDetails.Add(new InvoiceDetailBO
                            {
                                ItemOrder = reader["itemOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["itemOrder"]),
                                ItemDesc = reader["ItemDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ItemDesc"]),
                                Quantity = reader["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Quantity"]),
                                UnitCost = reader["UnitCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["UnitCost"]),
                                TotalCost = reader["TotalCost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalCost"])
                            });
                        }
                        objInv[0].InvoiceDetail = oInvDetails;
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return objInv;
        }

        public int SaveInvoice(int OrderID, string InvDate)
        {
            int rtnval = 0;
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspSaveInvoice", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@RequestID", OrderID));
                    sqlCmd.Parameters.Add(new SqlParameter("@InvDate", InvDate));
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
            return rtnval;
        }

        public void SaveInvoiceFile(string InvoiceFile, string invoiceNum)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("Update stblInvoice set invFile = @invFile where invNumber=@invoiceNum", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@invFile", InvoiceFile));
                    sqlCmd.Parameters.Add(new SqlParameter("@invoiceNum", invoiceNum));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
        }
    }

    public class CustomerDA
    {
        private string strErrorMsg;

        public List<CustomerBO> GetCustomer(string CustomerID)
        {
            List<CustomerBO> objCust = new List<CustomerBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblCustomer where CustID = @CustomerID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new CustomerBO
                                {
                                    CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                    CustNumber = reader["CustNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustNumber"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                    Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    Fax = reader["Fax"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Fax"]),
                                    Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                    Diagnosis = reader["Diagnosis"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Diagnosis"]),
                                    DiagnosisCode = reader["DiagnosisCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DiagnosisCode"]),
                                    ResultType = reader["ResultType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultType"]),
                                    Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),   
                                    Specialization= reader["Specialization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Specialization"])
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
            return objCust;
        }

        public List<CustomerBO> GetCustomer(int TenantID)
        {
            List<CustomerBO> objCust = new List<CustomerBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblCustomer where TenantID = @TenantID order by CustomerName", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new CustomerBO
                                {
                                    CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    Zipcode = reader["ZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ZipCode"]),
                                    Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    Fax = reader["Fax"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Fax"]),
                                    Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                    Status = reader["custStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustStatus"]),
                                    Diagnosis = reader["Diagnosis"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Diagnosis"]),
                                    DiagnosisCode = reader["DiagnosisCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DiagnosisCode"]),
                                    ResultType = reader["ResultType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultType"]),
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
            return objCust;
        }



        public List<OrderBO> GetAppEmail(string Email)
        {
            List<OrderBO> objCust = new List<OrderBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwPhysicianListOrder where Email=@Email", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@Email", Email));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new OrderBO
                                {

                                    AppEmail = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    OrderID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    OrderNumber = reader["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OrderNum"]),
                                    CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                    objCustomer = (new CustomerBO
                                    {
                                        
                                        CustNumber= reader["CustNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustNumber"]),

                                    }),

                                    specimenInfoBO=(new SpecimenInfoBO
                                    {
                                        SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                        SpecimenNumber= reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                        SpecimenStatus=reader["SpecimenStatus"]== DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                        CreatedOn= reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                        
                                    })
                                    

                                });


                                }


                            }
                        }
                        sqlCon.Close();
                    }
                
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return objCust;
        }

        public List<PreInsurance> GetPreInsurance()
        {
            List<PreInsurance> objCust = new List<PreInsurance>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblInsurancePreauthorization", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        //sqlCmd.Parameters.Add(new SqlParameter("@Email", Email));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new PreInsurance
                                {

                                    PreInsuranceNo= reader["PreInsuranceNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PreInsuranceNo"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    Gender= reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    MobileNumber= reader["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MobileNumber"]),
                                    InsuranceCard_IDno= reader["InsuranceCard_IDno"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsuranceCard_IDno"]),
                                    PolicyName= reader["PolicyName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyName"]),
                                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyNumber"]),
                                    Dataofbirth = reader["Dataofbirth"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Dataofbirth"]),
                                    CreatedOn= reader["Createon"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["Createon"]),
                                    PrimaryInsName = reader["PrimaryInsName"]==DBNull.Value ? String.Empty:Convert.ToString(reader["PrimaryInsName"]),
                                    Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"])
                                    
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
            return objCust;
        }

        public List<PreInsurance> GetPreInsuTenant(string labtenant)
        {
            List<PreInsurance> objCust = new List<PreInsurance>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblInsurancePreauthorization where TenantID=@TenantID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", labtenant));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new PreInsurance
                                {

                                    PreInsuranceNo = reader["PreInsuranceNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PreInsuranceNo"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    MobileNumber = reader["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MobileNumber"]),
                                    InsuranceCard_IDno = reader["InsuranceCard_IDno"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsuranceCard_IDno"]),
                                    PolicyName = reader["PolicyName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyName"]),
                                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyNumber"]),
                                    Dataofbirth = reader["Dataofbirth"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Dataofbirth"]),
                                    CreatedOn = reader["Createon"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["Createon"]),
                                    PrimaryInsName = reader["PrimaryInsName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["PrimaryInsName"]),
                                    Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"])

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
            return objCust;
        }

        public List<PreInsurance> GetPreInsurance(int insId)
        {
            List<PreInsurance> objCust = new List<PreInsurance>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblInsurancePreauthorization where PreInsuranceNo=@PreInsuranceNo", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@PreInsuranceNo", insId));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new PreInsurance
                                {

                                    PreInsuranceNo = reader["PreInsuranceNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PreInsuranceNo"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    MobileNumber = reader["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MobileNumber"]),
                                    InsuranceCard_IDno = reader["InsuranceCard_IDno"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsuranceCard_IDno"]),
                                    PolicyName = reader["PolicyName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyName"]),
                                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PolicyNumber"]),
                                    Dataofbirth = reader["Dataofbirth"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Dataofbirth"]),
                                    CreatedOn = reader["Createon"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["Createon"]),
                                    PrimaryInsName = reader["PrimaryInsName"] == DBNull.Value ? String.Empty : Convert.ToString(reader["PrimaryInsName"]),
                                    TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                    Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"])




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
            return objCust;
        }

        public List<PreInsurance> GetTenantInsurance(int Tenant)
        {
            List<PreInsurance> objCust = new List<PreInsurance>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwPreInsurance where Insurance_TenantID=@TenantID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", Tenant));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new PreInsurance
                                {

                                    TenantID = reader["Labuser_TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Labuser_TenantID"]),
                                    Status = reader["Status"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Status"]),
                                    Tenantname = reader["TenantName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantName"]),
                                    TenantCode = reader["TenantCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TenantCode"])
                                    
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
            return objCust;
        }


        public int UpdateInsurance(PreInsurance upInsurance)
        {
            int PreInsuranceNo = 0;
            //// DataSet set = new DataSet();
            
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveInsurance", sqlCon))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@PreInsuranceNo", upInsurance.PreInsuranceNo));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientName", upInsurance.PatientName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Gender", upInsurance.Gender));
                        sqlCmd.Parameters.Add(new SqlParameter("@Dataofbirth", upInsurance.Dataofbirth));
                        sqlCmd.Parameters.Add(new SqlParameter("@InsuranceCard_IDno", upInsurance.InsuranceCard_IDno));
                        sqlCmd.Parameters.Add(new SqlParameter("@MobileNumber", upInsurance.MobileNumber));
                        sqlCmd.Parameters.Add(new SqlParameter("@PolicyName", upInsurance.PolicyName));
                        sqlCmd.Parameters.Add(new SqlParameter("@PolicyNumber", upInsurance.PolicyNumber));
                        sqlCmd.Parameters.Add(new SqlParameter("@PrimaryInsName", upInsurance.PrimaryInsName));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", upInsurance.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@Comments", upInsurance.Comments));

                        sqlCon.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PreInsuranceNo = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
            return PreInsuranceNo;
        }

        public PreInsurance GetPreInsurance(string insInsurancenumber)
        {
            PreInsurance objPreInsuranceBO = new PreInsurance();

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlcomd = new SqlCommand("select * from stblInsurancePreauthorization where InsuranceCard_IDno=@insInsurancenumber", sqlcon))
                    {
                        sqlcon.Open();
                        sqlcomd.CommandType = CommandType.Text;
                        sqlcomd.Parameters.Add(new SqlParameter("@insInsurancenumber", insInsurancenumber));
                        SqlDataReader reader = sqlcomd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPreInsuranceBO.InsuranceCard_IDno= reader["InsuranceCard_IDno"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InsuranceCard_IDno"]);
                                objPreInsuranceBO.PreInsuranceNo = reader["PreInsuranceNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PreInsuranceNo"]);

                            }
                        }
                        sqlcon.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return objPreInsuranceBO;
        }



        public List<CustomerBO> GetCustomerByOrderID(string OrderID)
        {
            List<CustomerBO> objCust = new List<CustomerBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("select * from stblCustomer where CustID in (select CustID from stblOrder where OrderID=" + OrderID + ") ", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objCust.Add(new CustomerBO
                                {
                                    CustomerID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]),
                                    CustomerName = reader["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerName"]),
                                    Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]),
                                    Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]),
                                    Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]),
                                    Fax = reader["Fax"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Fax"]),
                                    Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                    Diagnosis = reader["Diagnosis"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Diagnosis"]),
                                    DiagnosisCode = reader["DiagnosisCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DiagnosisCode"]),
                                    ResultType = reader["ResultType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultType"]),
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
            return objCust;
        }

        public CustomerBO GetCustomerNumber(string CustNumber)
        {
           CustomerBO objcustomernumberBO = new CustomerBO();

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlcomd = new SqlCommand("select * from stblCustomer where CustNumber=@CustNumber", sqlcon))
                    {
                        sqlcon.Open();
                        sqlcomd.CommandType = CommandType.Text;
                        sqlcomd.Parameters.Add(new SqlParameter("@CustNumber",CustNumber));
                         SqlDataReader reader = sqlcomd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objcustomernumberBO.CustID = reader["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustID"]);
                                objcustomernumberBO.FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]);
                                objcustomernumberBO.LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]);
                                objcustomernumberBO.Address1 = reader["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Address1"]);
                                objcustomernumberBO.RequesterType = reader["RequesterType"] == DBNull.Value ? String.Empty : Convert.ToString(reader["RequesterType"]);
                                objcustomernumberBO.Facility = reader["Facility"] == DBNull.Value ? String.Empty : Convert.ToString(reader["Facility"]);
                                objcustomernumberBO.City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]);
                                objcustomernumberBO.State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]);
                                objcustomernumberBO.Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]);
                                objcustomernumberBO.Zipcode = reader["Zipcode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zipcode"]);
                                objcustomernumberBO.Phone = reader["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Phone"]);
                                objcustomernumberBO.Email = reader["Email"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Email"]);
                                objcustomernumberBO.Message = reader["Message"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Message"]);
                                objcustomernumberBO.CustNumber = reader["CustNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustNumber"]);

                            }
                        }
                        sqlcon.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return objcustomernumberBO;
        }

        public int SaveCustomerNumber(CustomerBO customernumberBO)
        {
            int customerNumber = 0;
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
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
                strErrorMsg = ex.Message;
                throw;
            }
            return customerNumber;
        }

        public CustomerBO SaveCustomer(CustomerBO oCustomer, int SpecimenID = 0)
        {
            CustomerBO oCust = new CustomerBO();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveMinCustomer", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oCustomer.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustName", oCustomer.CustomerName));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustAddress1", oCustomer.Address1));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustCity", oCustomer.City));
                        sqlCmd.Parameters.Add(new SqlParameter("@State", oCustomer.State));
                        sqlCmd.Parameters.Add(new SqlParameter("@Country", oCustomer.Country));
                        sqlCmd.Parameters.Add(new SqlParameter("@custPhone", oCustomer.Phone));
                        sqlCmd.Parameters.Add(new SqlParameter("@Email", oCustomer.Email));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustID", oCustomer.CustomerID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@Specialization", oCustomer.Specialization));
                        //sqlCmd.Parameters.Add(new SqlParameter("@Diagnosis", oCustomer.Diagnosis));
                        //sqlCmd.Parameters.Add(new SqlParameter("@DiagnosisCode", oCustomer.DiagnosisCode));
                        //sqlCmd.Parameters.Add(new SqlParameter("@ResultType", oCustomer.ResultType));
                        sqlCmd.Parameters.Add(new SqlParameter("@Zipcode", oCustomer.Zipcode));
                        sqlCmd.Parameters.Add(new SqlParameter("@Fax", oCustomer.Fax));
                        sqlCmd.Parameters.Add(new SqlParameter("@Facility", oCustomer.Facility));



                        /* SqlDataReader reader = sqlCmd.ExecuteReader();
                         if (reader.HasRows)
                         {
                             reader.Read();
                             oCust.CustomerID = (reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]));
                             oCust.CustomerNo = (reader["CustomerNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CustomerNumber"]));
                         }
                         reader.Close();
                         reader = null;*/
                        SqlDataAdapter adaptor = new SqlDataAdapter(sqlCmd);
                        adaptor.Fill(ds);

                        sqlCon.Close();
                        if (Convert.ToInt32(oCustomer.CustomerID) != 0)
                        {
                            oCust.CustomerName = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[1]);
                            oCust.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            oCust.CustomerName = Convert.ToString(ds.Tables[1].Rows[0].ItemArray[1]);
                            oCust.CustomerID = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0]);
                        }
                    }
                    sqlCon.Close();
                }

               
            }

            
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return oCust;
        }
    }

    public class OrderRequestDA
    {
        private string strErrorMsg;

        public OrderRequestBO SaveOrderRequest(OrderRequestBO orderRequestBO)
        {
            OrderRequestBO ordBO = new OrderRequestBO();
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Constant.DBConnectionString))
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
            catch(Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }

              return ordBO;
        }

    }

    public class SpecimenDA
    {
        private string strErrorMsg;

        public string GetNextSpecimenNo()
        {
            string rtnval = string.Empty;
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetNextSpecimenNumber", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rtnval = (reader[0] == DBNull.Value ? string.Empty : Convert.ToString(reader[0]));
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return rtnval;
        }

        public List<SpecimenInfoBO> GetGeneratedSpecimenNos(int TenantID)
        {
            List<SpecimenInfoBO> rtnval = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from stblSpecimenID where TenantID = @TenantID and Status='Active'", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rtnval.Add(new SpecimenInfoBO { SpecimenNumber = (reader["SpecimenNum"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNum"])) });
                        }
                    }
                    reader.Close();
                    reader = null;

                }
                sqlCon.Close();
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
            return rtnval;
        }

        public List<SpecimenInfoBO> GetNextSpecimenNo(int TenantID, int UserID = 0, int NoOfValues = 1)
        {
            List<SpecimenInfoBO> rtnval = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                SqlParameter outParam = new SqlParameter();
                outParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outParam.ParameterName = "@ReturnNum";
                outParam.Size = 30;
                outParam.Value = "";
                outParam.Direction = System.Data.ParameterDirection.Output;

                using (SqlCommand sqlCmd = new SqlCommand("sspGetNextSeqNumber", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@ConfigName", "Specimen"));
                    sqlCmd.Parameters.Add(new SqlParameter("@GeneratedBy", UserID));
                    sqlCmd.Parameters.Add(outParam);

                    for (int idx = 1; idx <= NoOfValues; idx++)
                    {
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtnval.Add(new SpecimenInfoBO { SpecimenNumber = (reader[0] == DBNull.Value ? string.Empty : Convert.ToString(reader[0])) });
                        }
                        reader.Close();
                        reader = null;
                    }
                }
                sqlCon.Close();
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
            return rtnval;
        }

        public int SavePatientInfoDA(PatientBO oPat)
        {
            int rtn = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("sspSavePatientNew", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@FirstName", oPat.FirstName));
                        sqlCmd.Parameters.Add(new SqlParameter("@LastName", oPat.LastName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Gender", oPat.Gender));
                        sqlCmd.Parameters.Add(new SqlParameter("@DOB", oPat.DOB));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientID", oPat.PatientID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oPat.CreatedBy));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oPat.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@Street",oPat.Street));
                        if (oPat.City != "")
                            sqlCmd.Parameters.Add(new SqlParameter("@City", oPat.City));
                        if (oPat.State != "")
                            sqlCmd.Parameters.Add(new SqlParameter("@State", oPat.State));
                        if (oPat.Zip != "")
                            sqlCmd.Parameters.Add(new SqlParameter("@Zip", oPat.Zip));
                        if (oPat.Country != "")
                            sqlCmd.Parameters.Add(new SqlParameter("@Country", oPat.Country));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianFName", oPat.GuardianFirstName));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianLName", oPat.GuardianLastName));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianStreet", oPat.GuardianStreet));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianCity", oPat.GuardianCity));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianState", oPat.GuardianState));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianCountry", oPat.GuardianCountry));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianZip", oPat.GuardianZip));
                        sqlCmd.Parameters.Add(new SqlParameter("@isPatientAddressSame", oPat.GuardianZip));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianRelationship", oPat.GuardianRelationship));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientEmailID",oPat.EmailID));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientContactNo",oPat.ContactNo));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianEmailID",oPat.GuardianEmailID));
                        sqlCmd.Parameters.Add(new SqlParameter("@GuardianContactNo",oPat.GuardianContactNo));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtn = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
            return rtn;
        }

        public int SaveVerificationInfo(SpecimenInfoBO oSpecimen)
        {
            int rtn = 0;
            try
            {

                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("sspSaveVerificationNew", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@IsConsent", oSpecimen.IsConsent));
                        sqlCmd.Parameters.Add(new SqlParameter("@IsRejection ", oSpecimen.IsRejection));
                        sqlCmd.Parameters.Add(new SqlParameter("@IsSpecimenAccept", oSpecimen.IsSpecimenAccept));
                        //sqlCmd.Parameters.Add(new SqlParameter("@CreatedOn", oSpecimen.CreatedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", oSpecimen.SpecimenID));
                        //sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oSpecimen.CreatedBy));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oSpecimen.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", oSpecimen.SpecimenStatus));
                        sqlCmd.Parameters.Add(new SqlParameter("@RejectReasons", oSpecimen.RejectReasons));
                        sqlCmd.Parameters.Add(new SqlParameter("@PendingReason", oSpecimen.PendingReasons));
                        sqlCmd.Parameters.Add(new SqlParameter("@ReactivateReason", oSpecimen.ReactivateReason));
                        
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rtn = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
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
            return rtn;

        }

        public SpecimenInfoBO  CreateSpecimenInfo(SpecimenInfoBO oSample)
        {
            //int rtn = 0;
            SpecimenInfoBO SpecimenAleart = new SpecimenInfoBO();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspCreateSpecimenInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@PatientID", oSample.PatientID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenNumber", oSample.SpecimenNumber));
                    sqlCmd.Parameters.Add(new SqlParameter("@SampleReceiveDate", oSample.SampleReceiveDate));
                    sqlCmd.Parameters.Add(new SqlParameter("@ReceivedTime", oSample.ReceivedTime));
                    sqlCmd.Parameters.Add(new SqlParameter("@DateDrawn", oSample.DateDrawn));
                    sqlCmd.Parameters.Add(new SqlParameter("@TimeDrawn", oSample.TimeDrawn));
                    sqlCmd.Parameters.Add(new SqlParameter("@TransitTime", oSample.TransitTime));
                    sqlCmd.Parameters.Add(new SqlParameter("@TransitTemperature", oSample.TransitTemperature));
                    sqlCmd.Parameters.Add(new SqlParameter("@VolumeReceived", oSample.VolumeReceived));
                    sqlCmd.Parameters.Add(new SqlParameter("@Unit", oSample.unit));
                    sqlCmd.Parameters.Add(new SqlParameter("@InterSubstance", oSample.InterSubstance));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsConsent", oSample.IsConsent));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsRejection", oSample.IsRejection));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsSpecimenAccept", oSample.IsSpecimenAccept));
                    sqlCmd.Parameters.Add(new SqlParameter("@RejectReasons", oSample.RejectReasons));
                    sqlCmd.Parameters.Add(new SqlParameter("@ReqFormCopyID", oSample.ReqFormCopyID));
                    sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", oSample.CreatedBy));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", oSample.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", oSample.SpecimenStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenType", oSample.SpecimentType));
                    sqlCmd.Parameters.Add(new SqlParameter("@PaymentMode", oSample.PaymentMode));
                    sqlCmd.Parameters.Add(new SqlParameter("@TestType", oSample.TestType));
                    sqlCmd.Parameters.Add(new SqlParameter("@PendingReason", oSample.PendingReasons));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                    while(reader.Read())
                        {
                            SpecimenAleart.SpecimenID = reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]);
                            SpecimenAleart.SpecimenError = reader["Errormsg"] == DBNull.Value ? string.Empty : Convert.ToString(reader ["Errormsg"]);

                            //if (oSample.CreatedBy == 0)
                            //{
                            //    SpecimenAleart.SpecimenError = reader["Errormsg"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Errormsg"]);
                            //}
                        }
                        //rtn = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return SpecimenAleart;
        }

        public int SaveOrder(SpecimenInfoBO objSpecimen)
               {
            int sporder = 0;
             try
             {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("sspGenerateOrder", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objSpecimen.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientID", objSpecimen.PatientID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", objSpecimen.SpecimenID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                       if (reader.HasRows)
                        {
                            reader.Read();

                           // sporder=(reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));

                            //sporder.Add(new PatientBO{  = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));

                            // sporder.Add(new PatientBO { OrderID = (reader['OrderID'] == DBNull.Value ? string.0 : Convert.ToInt32(reader['OrderID']))});

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
            return sporder;
        }


        public int UpdateOrder(SpecimenInfoBO objSpecimen)
        {
            int sporder = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("Update stblOrder set CustID=@CustID where SpecimenID=@SpecimenID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", objSpecimen.TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@PatientID", objSpecimen.PatientID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", objSpecimen.SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustID", objSpecimen.CustomerID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // sporder=(reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));

                            //sporder.Add(new PatientBO{  = (reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]));

                            // sporder.Add(new PatientBO { OrderID = (reader['OrderID'] == DBNull.Value ? string.0 : Convert.ToInt32(reader['OrderID']))});

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
            return sporder;
        }

        public void UpdateOrder(OrderBO orderBO)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspUpdateOrder", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.specimenInfoBO.SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@OrderNum", orderBO.OrderNumber));
                        sqlCmd.Parameters.Add(new SqlParameter("@OrderStatus", orderBO.OrderStatus));
                        sqlCmd.Parameters.Add(new SqlParameter("@CustID", orderBO.CustomerID));                        
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.StatusCode));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.OrderCount));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.SampleCount));
                        sqlCmd.Parameters.Add(new SqlParameter("@SName", orderBO.ShipName));
                        sqlCmd.Parameters.Add(new SqlParameter("@SAddress1", orderBO.ShipAddress1));
                        sqlCmd.Parameters.Add(new SqlParameter("@SAddress2", orderBO.ShipAddress2));
                        sqlCmd.Parameters.Add(new SqlParameter("@SCity", orderBO.ShipCity));
                        sqlCmd.Parameters.Add(new SqlParameter("@SZipCode", orderBO.ShipZipCode));
                        sqlCmd.Parameters.Add(new SqlParameter("@SState", orderBO.ShipState));
                        sqlCmd.Parameters.Add(new SqlParameter("@SCountry", orderBO.ShipCountry));
                        sqlCmd.Parameters.Add(new SqlParameter("@ShipOption", orderBO.ShipOption));
                        sqlCmd.Parameters.Add(new SqlParameter("@DeliveryEmail", orderBO.DeliveryEmail));
                        sqlCmd.Parameters.Add(new SqlParameter("@BName", orderBO.BillName));
                        sqlCmd.Parameters.Add(new SqlParameter("@BAddress1", orderBO.BillAddress1));
                        sqlCmd.Parameters.Add(new SqlParameter("@BAddress2", orderBO.BillAddress2));
                        sqlCmd.Parameters.Add(new SqlParameter("@BCity", orderBO.BillCity));
                        sqlCmd.Parameters.Add(new SqlParameter("@BZipCode", orderBO.BillZipCode));
                        sqlCmd.Parameters.Add(new SqlParameter("@BState", orderBO.BillState));
                        sqlCmd.Parameters.Add(new SqlParameter("@BCountry", orderBO.BillCountry));
                        sqlCmd.Parameters.Add(new SqlParameter("@Discount", orderBO.OrderDiscount));
                        sqlCmd.Parameters.Add(new SqlParameter("@DiscountType", orderBO.DiscountType));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.OrderAmount));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.OrderNetAmount));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.ClientDiscountTier));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.CompletedBy));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.CompletedOn));
                        sqlCmd.Parameters.Add(new SqlParameter("@GenOrderCustPhone", orderBO.GeneralOrderPhone));
                        sqlCmd.Parameters.Add(new SqlParameter("@GenOrderCustName", orderBO.GeneralOrderName));
                        sqlCmd.Parameters.Add(new SqlParameter("@PaymentStatus", orderBO.PaymentType));
                        sqlCmd.Parameters.Add(new SqlParameter("@BillAddSameAs", orderBO.BillAddSameAs));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.CustomerName));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.IsFolateBinding));
                        //sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.IsFolateBlocking)); 

                        sqlCmd.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
        }

        public OrderBO GetOrder(OrderBO orderBO)
        {
            OrderBO rtnorderBO = new OrderBO();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from stblOrder where SpecimenID = @SpecimenID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", orderBO.specimenInfoBO.SpecimenID));
                    //sqlCmd.Parameters.Add(new SqlParameter("@TenantID", orderBO.specimenInfoBO.TenantID));
                    sqlCmd.CommandType = CommandType.Text;

                    SqlDataReader read = sqlCmd.ExecuteReader();

                    if(read.HasRows)
                    {
                        while(read.Read())
                        {
                            rtnorderBO.OrderID = read["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(read["OrderID"]);
                            rtnorderBO.OrderNumber = read["OrderNum"] == DBNull.Value ? string.Empty : Convert.ToString(read["OrderNum"]);
                            rtnorderBO.OrderDate = read["OrderDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(read["OrderDate"]);
                            rtnorderBO.OrderStatus = read["OrderStatus"] == DBNull.Value ? string.Empty : Convert.ToString(read["OrderStatus"]);
                            rtnorderBO.CustomerID = read["CustID"] == DBNull.Value ? 0 : Convert.ToInt32(read["CustID"]);
                            rtnorderBO.OrderDiscount = read["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(read["Discount"]);
                            rtnorderBO.DiscountType = read["DiscountType"] == DBNull.Value ? string.Empty : Convert.ToString(read["DiscountType"]);
                            rtnorderBO.OrderAmount = read["TotOrderCost"] == DBNull.Value ? 0 : Convert.ToDecimal(read["TotOrderCost"]);
                            rtnorderBO.OrderNetAmount = read["NetOrderTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(read["NetOrderTotal"]);
                            rtnorderBO.BillName = read["BName"] == DBNull.Value ? string.Empty : Convert.ToString(read["BName"]);
                            rtnorderBO.BillAddress1 = read["BAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(read["BAddress1"]);
                            rtnorderBO.BillAddress2 = read["BAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(read["BAddress2"]);
                            rtnorderBO.BillCity = read["BCity"] == DBNull.Value ? string.Empty : Convert.ToString(read["BCity"]);
                            rtnorderBO.BillState = read["BState"] == DBNull.Value ? string.Empty : Convert.ToString(read["BState"]);
                            rtnorderBO.BillZipCode = read["BZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(read["BZipCode"]);
                            rtnorderBO.BillCountry = read["BCountry"] == DBNull.Value ? string.Empty : Convert.ToString(read["BCountry"]);
                            rtnorderBO.ShipName = read["SName"] == DBNull.Value ? string.Empty : Convert.ToString(read["SName"]);
                            rtnorderBO.ShipAddress1 = read["SAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(read["SAddress1"]);
                            rtnorderBO.ShipAddress2 = read["SAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(read["SAddress2"]);
                            rtnorderBO.ShipCity = read["SCity"] == DBNull.Value ? string.Empty : Convert.ToString(read["SCity"]);
                            rtnorderBO.ShipState = read["SState"] == DBNull.Value ? string.Empty : Convert.ToString(read["SState"]);
                            rtnorderBO.ShipZipCode = read["SZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(read["SZipCode"]);
                            rtnorderBO.ShipCountry = read["SCountry"] == DBNull.Value ? string.Empty : Convert.ToString(read["SCountry"]);
                            rtnorderBO.PaymentType = read["PaymentStatus"] == DBNull.Value ? string.Empty : Convert.ToString(read["PaymentStatus"]);
                            rtnorderBO.ShipOption = read["ShipOption"] == DBNull.Value ? string.Empty : Convert.ToString(read["ShipOption"]);
                            rtnorderBO.DeliveryEmail = read["DeliveryEmail"] == DBNull.Value ? string.Empty : Convert.ToString(read["DeliveryEmail"]);
                            rtnorderBO.CompletedOn = read["OrderCompletedOn"] == DBNull.Value ? string.Empty : Convert.ToString(read["OrderCompletedOn"]);
                            rtnorderBO.CompletedBy = read["CompletedBy"] == DBNull.Value ? string.Empty : Convert.ToString(read["CompletedBy"]);
                            rtnorderBO.GeneralOrderPhone = read["GenOrderCustPhone"] == DBNull.Value ? string.Empty : Convert.ToString(read["GenOrderCustPhone"]);
                            rtnorderBO.GeneralOrderName = read["GenOrderCustName"] == DBNull.Value ? string.Empty : Convert.ToString(read["GenOrderCustName"]);
                            rtnorderBO.ClientDiscountTier = read["ClientDiscountTier"] == DBNull.Value ? 0 : Convert.ToInt32(read["ClientDiscountTier"]);
                            rtnorderBO.IsFolateBinding = read["isBindTest"] == DBNull.Value ? false : Convert.ToBoolean(read["isBindTest"]);
                            rtnorderBO.IsFolateBlocking = read["isBlockTest"] == DBNull.Value ? false : Convert.ToBoolean(read["isBlockTest"]);
                            SpecimenInfoBO specimenBO = new SpecimenInfoBO();
                            specimenBO.CreatedBy = read["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(read["CreatedBy"]);
                            specimenBO.CreatedOn = read["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(read["CreatedOn"]);
                            specimenBO.SpecimenID = read["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(read["SpecimenID"]);
                            rtnorderBO.specimenInfoBO = specimenBO;
                            rtnorderBO.TenantID = read["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(read["TenantID"]);
                            rtnorderBO.BillAddSameAs = read["BillAddSameAs"] == DBNull.Value ? string.Empty : Convert.ToString(read["BillAddSameAs"]);
                        }
                    } 
                }
            }
            catch(Exception e)
            {
                strErrorMsg = e.Message;
                throw;
            }
            return rtnorderBO;
        }

        public void UpdateSpecimenInfo(SpecimenInfoBO oSample)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspupdateSpecimenInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", oSample.SpecimenID));
                    sqlCmd.Parameters.Add(new SqlParameter("@DateDrawn", oSample.DateDrawn));
                    sqlCmd.Parameters.Add(new SqlParameter("@TimeDrawn", oSample.TimeDrawn));
                    sqlCmd.Parameters.Add(new SqlParameter("@TransitTime", oSample.TransitTime));
                    sqlCmd.Parameters.Add(new SqlParameter("@TransitTemperature", oSample.TransitTemperature));
                    sqlCmd.Parameters.Add(new SqlParameter("@VolumeReceived", oSample.VolumeReceived));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenType", oSample.SpecimentType));
                    sqlCmd.Parameters.Add(new SqlParameter("@BloodType ", oSample.BloodType));
                    sqlCmd.Parameters.Add(new SqlParameter("@Comment", oSample.Comment));
                    sqlCmd.Parameters.Add(new SqlParameter("@SampleReceiveDate", oSample.SampleReceiveDate));
                    sqlCmd.Parameters.Add(new SqlParameter("@ReceivedTime", oSample.ReceivedTime));
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCon.Close();
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
        }

        public List<SpecimenInfoBO> GetSpecimenInfo(int TenantID, string SampleStatus, int UserID = 0, string SpecimenCreateDate = "")
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", SampleStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    if (SpecimenCreateDate != "")
                        sqlCmd.Parameters.Add(new SqlParameter("@CreatedOn", SpecimenCreateDate));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                       while (reader.Read())
                        {
                            string assayStatus = "";
                            if (SampleStatus == "Assigned to Assay")
                            {
                                List <SpecimenInfoBO> oAssay = GetAssaydetail(reader["SpecimenID"] ==DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"])/*,Convert.ToString(reader["TestStatus"])*/);
                                assayStatus = oAssay[0].TestStatus;

                                //oSample = oAssay;
                                oSample.Add(new SpecimenInfoBO
                                {
                                    //TestStatus= reader["TestStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestStatus"]),
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    oPatient = (new PatientBO
                                    {
                                        PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                        PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                        DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                    }),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                    TestStatus = assayStatus,
                                    IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                   IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                    IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                    PendingReasons = reader["PendingReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PendingReason"]),
                                    RejectReasons= reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                });
                                
                            }
                            else
                            {
                            oSample.Add(new SpecimenInfoBO
                                {
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                        oPatient = (new PatientBO
                                        {
                                            PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                            PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                            FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                            LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                            Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                            DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                        }),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                PendingReasons = reader["PendingReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PendingReason"]),
                                RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                            });
                            }
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return oSample;
        }
        public List<PatientBO> GetPatientSpec(int TenantID)
        {
            List<PatientBO> objPt = new List<PatientBO>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select * from svwSpecimen where TenantID = @TenantID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objPt.Add(new PatientBO
                                {
                                    Specialization = reader["Specialization"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Specialization"]),
                                    //RequesterType = reader["RequesterType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RequesterType"]),
                                    Facility = reader["Facility"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Facility"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    //SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    //SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
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
            return objPt;
        }
        public static string GetSamplelist(string searchTerm, int pageIndex)
        {
            string query = "[GetSamplelist_Pager]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 5).Direction = ParameterDirection.Output;
            return GetData(cmd, pageIndex).GetXml();
        }

        private static DataSet GetData(SqlCommand cmd, int pageIndex)
        {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {                   
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "Customers");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            
        }
        public List<SpecimenInfoBO> GetSpecimenInfo(int TenantID, string SampleStatus, DateTime TodayDate)
        {
            List<SpecimenInfoBO> specDet = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", SampleStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@TodayDate", TodayDate));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            specDet.Add(new SpecimenInfoBO
                            {
                                SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                oPatient = (new PatientBO
                                {
                                    PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                }),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                PendingReasons = reader["PendingReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PendingReason"]),
                                RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
                return specDet;
        }

        public List<SpecimenInfoBO> GetAssaydetail(int SpecimenID/*,string TestStatus*/)
        {
           List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from svwAssaySpecimens where SpecimenID=@SpecimenID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));
                    //sqlCmd.Parameters.Add(new SqlParameter("@TestStatus", TestStatus));
                    //if (SpecimenCreateDate != "")
                    //    sqlCmd.Parameters.Add(new SqlParameter("@CreatedOn", SpecimenCreateDate));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oSample.Add(new SpecimenInfoBO
                            {
                                SpecimenID=reader["SpecimenID"]==DBNull.Value ? 0:Convert.ToInt32(reader["SpecimenID"]),
                                AssaySpecimenID = reader["AssaySpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssaySpecimenID"]),
                                TestStatus= reader["AssayStatus"] == DBNull.Value ? string.Empty: Convert.ToString(reader["AssayStatus"])
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oSample;
        }

        public List<SpecimenInfoBO> GetSpecimenInfo(int SpecimenID)
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenDetail", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oSample.Add(new SpecimenInfoBO
                            {
                                SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                oPatient = (new PatientBO
                                {
                                    PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]),
                                    Street= reader["Street"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Street"]),
                                    City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                    State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                    Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]),
                                    Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                    GuardianFirstName = reader["GuardianFName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianFName"]),
                                    GuardianLastName = reader["GuardianLName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianLName"]),
                                    GuardianStreet = reader["GuardianStreet"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianStreet"]),
                                    GuardianCity = reader["GuardianCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCity"]),
                                    GuardianState = reader["GuardianState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianState"]),
                                    GuardianCountry = reader["GuardianCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCountry"]),
                                    GuardianZip = reader["GuardianZip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianZip"]),
                                    GuardianRelationship = reader["GuardianRelationship"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianRelationship"]),
                                    isPatientAddressSame = reader["isPatientAddressSame"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPatientAddressSame"]),
                                }),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                InterSubstance = reader["InterSubstance"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InterSubstance"]),
                                IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                TimeDrawn = reader["TimeDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeDrawn"]),
                                TransitTemperature = reader["TransitTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTemperature"]),
                                TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                BloodType= reader["BloodType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BloodType"]),
                                SpecimentType= reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                SampleReceiveDate = reader["SampleReceivedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceivedDate"]),
                                ReceivedTime = reader["ReceivedTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReceivedTime"]),
                                ReqFormCopyID = reader["ReqFormCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReqFormCopyID"]),
                                PaymentMode = reader["PaymentMode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PaymentMode"]),
                                TestType = reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"])
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oSample;
        }

        private void GetReportLink()
        {
            //<a href="#"><img src="/images/pdfIco.png" /></a>&nbsp;
        }

        public List<SpecimenInfoBO> GetSpecimenInfo(int SpecimenID,int TenantID, int AssayID = 0)
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenDetail", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayID", AssayID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (AssayID == 0)
                        {
                            while (reader.Read())
                            {
                                oSample.Add(new SpecimenInfoBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    oPatient = (new PatientBO
                                    {
                                        PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                        PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                        DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]),
                                        City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                        Street= reader["Street"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Street"]),
                                        State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                        Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]),
                                        Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                        GuardianFirstName = reader["GuardianFName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianFName"]),
                                        GuardianLastName = reader["GuardianLName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianLName"]),
                                        GuardianStreet = reader["GuardianStreet"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianStreet"]),
                                        GuardianCity = reader["GuardianCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCity"]),
                                        GuardianState = reader["GuardianState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianState"]),
                                        GuardianCountry = reader["GuardianCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCountry"]),
                                        GuardianZip = reader["GuardianZip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianZip"]),
                                        isPatientAddressSame= reader["isPatientAddressSame"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPatientAddressSame"]),
                                        GuardianRelationship= reader["GuardianRelationship"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianRelationship"]),
                                        EmailID = reader["PatientEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientEmailID"]),
                                        ContactNo = reader["PatientContactNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientContactNo"]),
                                        GuardianEmailID = reader["GuardianEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianEmailID"]),
                                        GuardianContactNo = reader["GuardianContactNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianContactNo"]),
                                    }),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                    InterSubstance = reader["InterSubstance"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InterSubstance"]),
                                    IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                    IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                    IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                    RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                    PendingReasons=reader["PendingReason"]== DBNull.Value ? string.Empty : Convert.ToString(reader["PendingReason"]),
                                    TimeDrawn = reader["TimeDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeDrawn"]),
                                    TransitTemperature = reader["TransitTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTemperature"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                    ReqFormCopyID= reader["ReqFormCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReqFormCopyID"]),
                                    SpecimentType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                    PaymentMode = reader["PaymentMode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PaymentMode"]),
                                    TestType = reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"]),
                                    SampleReceiveDate = reader["SampleReceivedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceivedDate"]),
                                    ReceivedTime = reader["ReceivedTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReceivedTime"]),
                                    ReactivateReason= reader["ReactivateReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReactivateReason"]),
                                    BloodType = reader["BloodType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BloodType"])                                    
                                });
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                oSample.Add(new SpecimenInfoBO
                                {
                                    TenantID = reader["TenantID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TenantID"]),
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    oPatient = (new PatientBO
                                    {
                                        PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                        PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                        DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]),
                                        City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                        Street = reader["Street"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Street"]),
                                        State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                        Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]),
                                        Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                        GuardianFirstName = reader["GuardianFName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianFName"]),
                                        GuardianLastName = reader["GuardianLName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianLName"]),
                                        GuardianStreet = reader["GuardianStreet"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianStreet"]),
                                        GuardianCity = reader["GuardianCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCity"]),
                                        GuardianState = reader["GuardianState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianState"]),
                                        GuardianCountry = reader["GuardianCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCountry"]),
                                        GuardianZip = reader["GuardianZip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianZip"]),
                                        isPatientAddressSame = reader["isPatientAddressSame"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPatientAddressSame"]),
                                        GuardianRelationship = reader["GuardianRelationship"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianRelationship"]),
                                        EmailID = reader["PatientEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientEmailID"]),
                                        ContactNo = reader["PatientContactNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientContactNo"]),
                                        GuardianEmailID = reader["GuardianEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianEmailID"]),
                                        GuardianContactNo = reader["GuardianContactNo"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianContactNo"]),
                                        //Specialization = reader["Specialization"]== DBNull.Value ? string.Empty:Convert.ToString(reader["Specialization"])
                                    }),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                    InterSubstance = reader["InterSubstance"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InterSubstance"]),
                                    IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                    IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                    IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                    RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                    PendingReasons = reader["PendingReason"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PendingReason"]),
                                    TimeDrawn = reader["TimeDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeDrawn"]),
                                    TransitTemperature = reader["TransitTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTemperature"]),
                                    BloodType = reader["BloodType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BloodType"]),
                                    SpecimentType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                    SampleReceiveDate = reader["SampleReceivedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceivedDate"]),
                                    ReceivedTime = reader["ReceivedTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ReceivedTime"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                    oResult = new ResultBO
                                    {
                                        AssaySpecimenID = reader["AssaySpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssaySpecimenID"]),
                                        AssayID = reader["AssayID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssayID"]),
                                        SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                        IsRepeat = reader["IsRepeat"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRepeat"]),
                                        RepeatNo = reader["RepeatCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RepeatCount"]),
                                        OutofBINDate = reader["OutofBINDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OutofBINDate"]),
                                        AssignedToBINOn = reader["AssignedToBINOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssignedToBINOn"]),
                                        SpecimenType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                        SubSpecimenType = reader["SubSpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SubSpecimenType"]),
                                        RemainVol = reader["RemainVol"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RemainVol"]),
                                        BindValue = reader["BindValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BindValue"]),
                                        BindValComment = reader["BindValComment"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BindValComment"]),
                                        BlockValue = reader["BlockValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BlockValue"]),
                                        BlockValComment = reader["BlockValComment"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BlockValComment"]),
                                        ResultFileName = reader["ResultFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultFileName"]),
                                        ResultDocID = reader["ResultDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ResultDocID"]),
                                        ResultSentDate = reader["ResultSentDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultSentDate"]),
                                        AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"])
                                    },
                                        ReqFormCopyID = reader["ReqFormCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReqFormCopyID"]),
                                        PaymentMode = reader["PaymentMode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PaymentMode"]),
                                        TestType = reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"])
                                });
                            }
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return oSample;
        }

        public void UpdatePaymentMode(SpecimenInfoBO specimenInfoBO)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("Update stblSpecimenInfo set PaymentMode = @PaymentMode where SpecimenID = @SpecimenID and TenantID = @TenantID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", specimenInfoBO.TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", specimenInfoBO.SpecimenID));
                    sqlCmd.Parameters.Add(new SqlParameter("@PaymentMode", specimenInfoBO.PaymentMode));
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCon.Close();
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
        }

        public List<SpecimenInfoBO> GetSpecimenNumberInfo(string SpecimenNumber, int TenantID, int AssayID = 0)
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspViewSpecimenDetail", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenNumber", SpecimenNumber));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayID", AssayID));
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (AssayID == 0)
                        {
                            while (reader.Read())
                            {
                                oSample.Add(new SpecimenInfoBO
                                {
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    oPatient = (new PatientBO
                                    {
                                        PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                        PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                        DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]),
                                        City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                        Street = reader["Street"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Street"]),
                                        State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                        Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]),
                                        Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                        GuardianFirstName = reader["GuardianFName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianFName"]),
                                        GuardianLastName = reader["GuardianLName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianLName"]),
                                        GuardianStreet = reader["GuardianStreet"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianStreet"]),
                                        GuardianCity = reader["GuardianCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCity"]),
                                        GuardianState = reader["GuardianState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianState"]),
                                        GuardianCountry = reader["GuardianCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCountry"]),
                                        GuardianZip = reader["GuardianZip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianZip"]),
                                        GuardianRelationship= reader["GuardianRelationship"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianRelationship"])
                                    }),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                    InterSubstance = reader["InterSubstance"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InterSubstance"]),
                                    IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                    IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                    IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                    RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                    TimeDrawn = reader["TimeDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeDrawn"]),
                                    TransitTemperature = reader["TransitTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTemperature"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    SpecimentType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                    TestType = reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"]),
                                    CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                    ReqFormCopyID = reader["ReqFormCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReqFormCopyID"]),
                                    SampleReceiveDate = reader["SampleReceivedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceivedDate"]),
                                });
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                oSample.Add(new SpecimenInfoBO
                                {
                                    SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                    SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                    oPatient = (new PatientBO
                                    {
                                        PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                        PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                        FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                        LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                        Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                        DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"]),
                                        City = reader["City"] == DBNull.Value ? string.Empty : Convert.ToString(reader["City"]),
                                        Street = reader["Street"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Street"]),
                                        State = reader["State"] == DBNull.Value ? string.Empty : Convert.ToString(reader["State"]),
                                        Zip = reader["Zip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Zip"]),
                                        Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                                        GuardianFirstName = reader["GuardianFName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianFName"]),
                                        GuardianLastName = reader["GuardianLName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianLName"]),
                                        GuardianStreet = reader["GuardianStreet"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianStreet"]),
                                        GuardianCity = reader["GuardianCity"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCity"]),
                                        GuardianState = reader["GuardianState"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianState"]),
                                        GuardianCountry = reader["GuardianCountry"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianCountry"]),
                                        GuardianZip = reader["GuardianZip"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianZip"]),
                                        GuardianRelationship = reader["GuardianRelationship"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GuardianRelationship"])
                                    }),
                                    //SampleReceiveDate = reader["SampleReceiveDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceiveDate"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SampleReceiveDate = reader["SampleReceivedDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleReceivedDate"]),
                                    SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                    SpecimentType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                    TestType = reader["TestType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestType"]),
                                    DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                    CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                    InterSubstance = reader["InterSubstance"] == DBNull.Value ? string.Empty : Convert.ToString(reader["InterSubstance"]),
                                    IsConsent = reader["IsConsent"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsConsent"]),
                                    IsRejection = reader["IsRejection"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRejection"]),
                                    IsSpecimenAccept = reader["IsSpecimenAccept"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsSpecimenAccept"]),
                                    RejectReasons = reader["RejectReasons"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RejectReasons"]),
                                    TimeDrawn = reader["TimeDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TimeDrawn"]),
                                    TransitTemperature = reader["TransitTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTemperature"]),
                                    TransitTime = reader["TransitTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TransitTime"]),
                                    VolumeReceived = reader["VolumeReceived"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VolumeReceived"]),
                                    CustomerID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]),
                                    oResult = new ResultBO
                                    {
                                        AssaySpecimenID = reader["AssaySpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssaySpecimenID"]),
                                        AssayID = reader["AssayID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssayID"]),
                                        SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                        IsRepeat = reader["IsRepeat"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsRepeat"]),
                                        RepeatNo = reader["RepeatCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RepeatCount"]),
                                        OutofBINDate = reader["OutofBINDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OutofBINDate"]),
                                        AssignedToBINOn = reader["AssignedToBINOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssignedToBINOn"]),
                                        SpecimenType = reader["SpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenType"]),
                                        SubSpecimenType = reader["SubSpecimenType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SubSpecimenType"]),
                                        RemainVol = reader["RemainVol"] == DBNull.Value ? string.Empty : Convert.ToString(reader["RemainVol"]),
                                        BindValue = reader["BindValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BindValue"]),
                                        BindValComment = reader["BindValComment"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BindValComment"]),
                                        BlockValue = reader["BlockValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BlockValue"]),
                                        BlockValComment = reader["BlockValComment"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BlockValComment"]),
                                        ResultFileName = reader["ResultFileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultFileName"]),
                                        ResultDocID = reader["ResultDocID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ResultDocID"]),
                                        ResultSentDate = reader["ResultSentDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ResultSentDate"]),
                                        AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"])
                                    },
                                    ReqFormCopyID = reader["ReqFormCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReqFormCopyID"])
                                });
                            }
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return oSample;
        }

        public List<SpecimenInfoBO> GetAssaySamples(int AssayID)
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from svwAssaySpecimenPatients where AssayID=@AssayID", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayID", AssayID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int CustID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]);
                            CustomerBO oCust = new CustomerBO();
                            if (CustID > 0)
                            {
                                List<CustomerBO> oCustList = (new CustomerDA()).GetCustomer(Convert.ToString(CustID));
                                if (oCustList.Count > 0)
                                    oCust = oCustList[0];
                            }

                            ResultBO oRes = new ResultBO();
                            
                            oSample.Add(new SpecimenInfoBO
                            {
                                SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                oPatient = (new PatientBO
                                {
                                    PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                }),
                                CreatedOn = reader["SpecimenCreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenCreatedOn"]),
                                SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                oCustomer = oCust,
                                oResult = oRes
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oSample;
        }

        public List<SpecimenInfoBO> GetAssaynumberSamples(string AssayBIN)
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select * from svwAssaySpecimenPatients where AssayBIN=@AssayBIN", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayBIN", AssayBIN));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int CustID = reader["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CustomerID"]);
                            CustomerBO oCust = new CustomerBO();
                            if (CustID > 0)
                            {
                                List<CustomerBO> oCustList = (new CustomerDA()).GetCustomer(Convert.ToString(CustID));
                                if (oCustList.Count > 0)
                                    oCust = oCustList[0];
                            }

                            ResultBO oRes = new ResultBO();

                            oSample.Add(new SpecimenInfoBO
                            {
                                SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                oPatient = (new PatientBO
                                {
                                    PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                }),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"]),
                                oCustomer = oCust,
                                oResult = oRes
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oSample;
        }

        public List<SpecimenInfoBO> GetPendingSpecimenInfo(int TenantID, string SampleStatus, int UserID = 0, string SpecimenCreateDate = "")
        {
            List<SpecimenInfoBO> oSample = new List<SpecimenInfoBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", SampleStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    if (SpecimenCreateDate != "")
                        sqlCmd.Parameters.Add(new SqlParameter("@CreatedOn", SpecimenCreateDate));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oSample.Add(new SpecimenInfoBO
                            {
                                SpecimenID = reader["SpecimenID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SpecimenID"]),
                                SpecimenNumber = reader["SpecimenNumber"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenNumber"]),
                                oPatient = (new PatientBO
                                {
                                    PatientID = reader["PatientID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PatientID"]),
                                    PatientName = reader["PatientName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PatientName"]),
                                    FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                                    LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                                    Gender = reader["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Gender"]),
                                    DOB = reader["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DOB"])
                                }),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                SpecimenStatus = reader["SpecimenStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SpecimenStatus"]),
                                DateDrawn = reader["DateDrawn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DateDrawn"]),
                                CreatedByName = reader["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedByName"])
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return oSample;
        }

        public List<AssayGroupBO> GetAssayGroupList(int TenantID, string AssayStatus, int AssayID = 0, bool IsAssayOnly = false)
        {
            List<AssayGroupBO> oAssay = new List<AssayGroupBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetAssayInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayID", AssayID));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsAssayOnly", IsAssayOnly));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!IsAssayOnly)
                            {
                                List<SpecimenInfoBO> oSample = GetAssaySamples(Convert.ToInt32(reader["AssayID"]));

                                oAssay.Add(new AssayGroupBO
                                {
                                    AssayID = Convert.ToInt32(reader["AssayID"]),
                                    AssayBIN = reader["AssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayBIN"]),
                                    AssayType = reader["AssayType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayType"]),
                                    AssayName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]),
                                    oSample = oSample,
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SampleCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]),
                                    AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"]),
                                    AssayDesc = reader["AssayDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayDesc"]),
                                    AssayLoadDateTime = reader["AssayLoadDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayLoadDateTime"]),
                                    AssayCompleteDateTime = reader["AssayCompleteDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayCompleteDateTime"]),
                                    SampleMaxDate = reader["SampleMaxDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleMaxDate"]),
                                });
                            }
                            else
                            {
                                oAssay.Add(new AssayGroupBO
                                {
                                    AssayID = Convert.ToInt32(reader["AssayID"]),
                                    AssayBIN = reader["AssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayBIN"]),
                                    AssayType = reader["AssayType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayType"]),
                                    AssayName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SampleCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]),
                                    AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"]),
                                    AssayDesc = reader["AssayDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayDesc"]),
                                    AssayLoadDateTime = reader["AssayLoadDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayLoadDateTime"]),
                                    AssayCompleteDateTime = reader["AssayCompleteDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayCompleteDateTime"]),
                                    SampleMaxDate = reader["SampleMaxDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleMaxDate"])
                                });
                            }
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return oAssay;
        }        

        public string GetAssayGroup(int TenantID, string AssayType, string AssayStatus)
        {
            string assName = "";
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select AssayName from stblAssayGroup Where TenantID = @TenantID and AssayStatus = @AssayStatus and AssayType = @AssayType", sqlCon))
                { 
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayType", AssayType));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            assName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]);
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return assName;
        }

        public int GetSampleCount(int TenantID, string AssayType, string AssayStatus)
        {
            int sampCount = 0;
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("select SampleCount from stblAssayGroup Where TenantID = @TenantID and AssayStatus = @AssayStatus and AssayType = @AssayType", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayType", AssayType));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sampCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]);
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return sampCount;
        }

        public void AddAssayName(int TenantID,string AssayName, string AssayStatus)
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("Insert into stblAssayGroup(AssayName, TenantID, AssayStatus) values(@AssayName, @TenantID, @AssayStatus)", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayName", AssayName));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCon.Close();
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
        }

        public List<AssayGroupBO> GetAssayNumberList(int TenantID, string AssayStatus, string  AssayBIN, bool IsAssayOnly = false)
        {
            List<AssayGroupBO> oAssay = new List<AssayGroupBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspviewAssayInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                    sqlCmd.Parameters.Add(new SqlParameter("@AssayBIN", AssayBIN));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsAssayOnly", IsAssayOnly));
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!IsAssayOnly)
                            {
                                List<SpecimenInfoBO> oSample = GetAssaynumberSamples(Convert.ToString(reader["AssayBIN"]));

                                oAssay.Add(new AssayGroupBO
                                {
                                    AssayID = Convert.ToInt32(reader["AssayID"]),
                                    AssayBIN = reader["AssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayBIN"]),
                                    AssayType = reader["AssayType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayType"]),
                                    AssayName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]),
                                    oSample = oSample,
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SampleCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]),
                                    AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"]),
                                    AssayDesc = reader["AssayDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayDesc"]),
                                    AssayLoadDateTime = reader["AssayLoadDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayLoadDateTime"]),
                                    AssayCompleteDateTime = reader["AssayCompleteDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayCompleteDateTime"]),
                                    SampleMaxDate = reader["SampleMaxDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleMaxDate"]),
                                });
                            }
                            else
                            {
                                oAssay.Add(new AssayGroupBO
                                {
                                    AssayID = Convert.ToInt32(reader["AssayBIN"]),
                                    AssayBIN = reader["AssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayBIN"]),
                                    AssayType = reader["AssayType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayType"]),
                                    AssayName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]),
                                    CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                    SampleCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]),
                                    AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"]),
                                    AssayDesc = reader["AssayDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayDesc"]),
                                    AssayLoadDateTime = reader["AssayLoadDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayLoadDateTime"]),
                                    AssayCompleteDateTime = reader["AssayCompleteDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayCompleteDateTime"]),
                                    SampleMaxDate = reader["SampleMaxDate"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SampleMaxDate"])
                                });
                            }
                        }
                    }
                    reader.Close();
                    reader = null;
                }
                sqlCon.Close();
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
            return oAssay;
        }

        public List<AssayGroupBO> GetAssaySpecimens(int TenantID, int SpecimenID)
        {
            List<AssayGroupBO> oAssay = new List<AssayGroupBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCmd = new SqlCommand("sspGetSpecimenAssayInfo", sqlCon))
                {
                    sqlCon.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                    sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<SpecimenInfoBO> oSample = GetAssaySamples(Convert.ToInt32(reader["AssayID"]));

                            oAssay.Add(new AssayGroupBO
                            {
                                AssayID = Convert.ToInt32(reader["AssayID"]),
                                AssayBIN = reader["AssayBIN"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayBIN"]),
                                AssayType = reader["AssayType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayType"]),
                                AssayName = reader["AssayName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayName"]),
                                oSample = oSample,
                                CreatedOn = reader["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedOn"]),
                                SampleCount = reader["SampleCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SampleCount"]),
                                AssayStatus = reader["AssayStatus"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayStatus"]),
                                AssayDesc = reader["AssayDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AssayDesc"])
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
                strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
              return oAssay;
        }

        public int AddSpecimenToAssay(int TenantID, int SpecimenID, string AssayType, string AssayName)
        {
            int assID = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspAddSpecimenToAssay", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@AssayType", AssayType));
                        sqlCmd.Parameters.Add(new SqlParameter("@AssayName", AssayName));

                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            //assID = (reader["AssayID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssayID"]));
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
            return assID;
        }

        public void UpdateAssayStatus(int AssayID, DateTime ActDateTime, string AssayStatus)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspUpdateAssayGroup", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@AssayID", AssayID));
                        sqlCmd.Parameters.Add(new SqlParameter("@ActDate", ActDateTime));
                        sqlCmd.Parameters.Add(new SqlParameter("@AssayStatus", AssayStatus));
                        SqlDataReader reader = sqlCmd.ExecuteReader();
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
        }

        public void SaveTestResult(ResultBO oResult)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("sspUpdateTestResult", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.Add(new SqlParameter("@AssayID", oResult.AssayID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", oResult.SpecimenID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenType", oResult.SpecimenType));
                        sqlCmd.Parameters.Add(new SqlParameter("@SubSpecimenType", oResult.SubSpecimenType));
                        sqlCmd.Parameters.Add(new SqlParameter("@RemainVol", oResult.RemainVol));
                        sqlCmd.Parameters.Add(new SqlParameter("@BindValue", oResult.BindValue));
                        sqlCmd.Parameters.Add(new SqlParameter("@BindValComment", oResult.BindValComment));
                        sqlCmd.Parameters.Add(new SqlParameter("@BlockValue", oResult.BlockValue));
                        sqlCmd.Parameters.Add(new SqlParameter("@BlockValComment", oResult.BlockValComment));
                        sqlCmd.Parameters.Add(new SqlParameter("@IsRepeat", oResult.IsRepeat));

                        sqlCmd.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
        }

        public List<RefTestProfileBO> getSpcimenType(int TenantID)
        {
            List<RefTestProfileBO> spcimentype = new List<RefTestProfileBO>();
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection(Constant.DBConnectionString);
                using (SqlCommand sqlCom = new SqlCommand("select * from stblRefTestProfile where CreatedBy=@CreatedBy", sqlCon))
                {
                    sqlCon.Open();
                    sqlCom.CommandType = CommandType.Text;
                    sqlCom.Parameters.Add(new SqlParameter("@CreatedBy", TenantID));
                    SqlDataReader reader = sqlCom.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            spcimentype.Add(new RefTestProfileBO
                            {
                                CreatedBy = reader["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CreatedBy"]),
                                TestProfileName = reader["TestProfileName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestProfileName"]),
                                TestProfileCode = reader["TestProfileCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestProfileCode"]),
                                TestProfileDesc = reader["TestProfileDesc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TestProfileDesc"])
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
                strErrorMsg = ex.Message;
            }
            return spcimentype;
        }

        public int getTodayRequests(int TenantID)
        {
            int count = 0;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select Count(*) from stblSpecimenInfo where CreatedOn >= cast(getdate() as date) and CreatedOn < cast(getdate() + 1 as date) and TenantID = @TenantID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                               count = Convert.ToInt32(reader[0]);
                            }
                        }
                        reader.Close();
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }
            return count;
        }

        public void updateSpecimenStatus(int TenantID,int SpecimenID,string SpecimenStatus)
        {            
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Update stblSpecimenInfo set SpecimenStatus = @SpecimenStatus where SpecimenID = @SpecimenID and TenantID = @TenantID", sqlCon))
                    {
                        sqlCon.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add(new SqlParameter("@TenantID", TenantID));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenStatus", SpecimenStatus));
                        sqlCmd.Parameters.Add(new SqlParameter("@SpecimenID", SpecimenID));

                        sqlCmd.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                throw;
            }            
        }

    }

}
