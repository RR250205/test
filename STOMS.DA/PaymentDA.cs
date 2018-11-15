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
    public class PaymentDA
    {
        private string _strErrorMsg;
        SqlConnection sqlCon = null;

        public PaymentBO savePaymentDetails(PaymentBO payBO)
        {
            PaymentBO paymBO = new PaymentBO();
            paymBO.CashDetails = new CashBO();
            paymBO.CreditDetails = new CreditCardBO();
            paymBO.chequeDetails = new ChequeBO();
            paymBO.InsuranceDetails = new InsuranceBO();
            paymBO.SpecimenDetails = new SpecimenInfoBO();
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sspSavePayment", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@PaymentID", payBO.PaymentID));
                        command.Parameters.Add(new SqlParameter("@PaymentMode", payBO.PaymentMode));
                        command.Parameters.Add(new SqlParameter("@PaymentStatus", payBO.PaymentStatus));
                        command.Parameters.Add(new SqlParameter("@TenantID", payBO.SpecimenDetails.TenantID));
                        command.Parameters.Add(new SqlParameter("@SpecimenID", payBO.SpecimenDetails.SpecimenID));
                        command.Parameters.Add(new SqlParameter("@Cash", payBO.CashDetails.Cash));
                        command.Parameters.Add(new SqlParameter("@TransactionDate", payBO.CashDetails.TransactionDate));
                        command.Parameters.Add(new SqlParameter("@Currency", payBO.CashDetails.Currency));
                        command.Parameters.Add(new SqlParameter("@Description", payBO.CashDetails.Description));
                        command.Parameters.Add(new SqlParameter("@CardType", payBO.CreditDetails.CardType));
                        command.Parameters.Add(new SqlParameter("@CardNumber", payBO.CreditDetails.CardNumber));
                        command.Parameters.Add(new SqlParameter("@HolderName", payBO.CreditDetails.HolderName));
                        command.Parameters.Add(new SqlParameter("@CVVNumber", payBO.CreditDetails.CVVNumber));
                        command.Parameters.Add(new SqlParameter("@ExpireDate", payBO.CreditDetails.ExpireDate));
                        command.Parameters.Add(new SqlParameter("@CreditAmount", payBO.CreditDetails.CreditAmount));
                        command.Parameters.Add(new SqlParameter("@BankName", payBO.chequeDetails.BankName));
                        command.Parameters.Add(new SqlParameter("@BranchName", payBO.chequeDetails.BranchName));
                        command.Parameters.Add(new SqlParameter("@ChequeNumber", payBO.chequeDetails.ChequeNumber));
                        command.Parameters.Add(new SqlParameter("@ChequeDate", payBO.chequeDetails.ChequeDate));
                        command.Parameters.Add(new SqlParameter("@ChequeAmount", payBO.chequeDetails.ChequeAmount));
                        command.Parameters.Add(new SqlParameter("@AccountNumber", payBO.chequeDetails.AccountNumber));
                        command.Parameters.Add(new SqlParameter("@RoutingNumber", payBO.chequeDetails.RoutingNumber));
                        command.Parameters.Add(new SqlParameter("@MemoDescription", payBO.chequeDetails.MemoDescription));
                        command.Parameters.Add(new SqlParameter("@ChequeUpload", payBO.chequeDetails.ChequeUpload));
                        command.Parameters.Add(new SqlParameter("@InsuranceType", payBO.InsuranceDetails.InsuranceType));
                        command.Parameters.Add(new SqlParameter("@InsuranceCompany", payBO.InsuranceDetails.InsuranceCompany));
                        command.Parameters.Add(new SqlParameter("@InsuranceNumber", payBO.InsuranceDetails.InsuranceNumber));
                        command.Parameters.Add(new SqlParameter("@MemberName", payBO.InsuranceDetails.MemberName));
                        command.Parameters.Add(new SqlParameter("@MemberShipNumber", payBO.InsuranceDetails.MemberShipNumber));
                        command.Parameters.Add(new SqlParameter("@GroupNumber", payBO.InsuranceDetails.GroupNumber));
                        command.Parameters.Add(new SqlParameter("@PreAuthCode", payBO.InsuranceDetails.PreAuthCode));
                        command.Parameters.Add(new SqlParameter("@PreInsuranceNo", payBO.InsuranceDetails.PreInsuranceNo));
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader read = command.ExecuteReader();

                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                paymBO.PaymentID = Convert.ToInt32(read[0]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return paymBO;
        }

        public PaymentBO getPaymentDetails(PaymentBO payBO)
        {
            PaymentBO paymBO = new PaymentBO();
            paymBO.CashDetails = new CashBO();
            paymBO.CreditDetails = new CreditCardBO();
            paymBO.chequeDetails = new ChequeBO();
            paymBO.InsuranceDetails = new InsuranceBO();
            paymBO.SpecimenDetails = new SpecimenInfoBO();
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("select * from svwPayment where TenantID = @TenantID and SpecimenID = @SpecimenID", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@TenantID", payBO.SpecimenDetails.TenantID));
                        command.Parameters.Add(new SqlParameter("@SpecimenID", payBO.SpecimenDetails.SpecimenID));
                        command.CommandType = CommandType.Text;
                        SqlDataReader read = command.ExecuteReader();

                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                paymBO.PaymentID = read["PaymentID"] == DBNull.Value ? 0 : Convert.ToInt32(read["PaymentID"]);
                                paymBO.PaymentMode = read["PaymentMode"] == DBNull.Value ? string.Empty : Convert.ToString(read["PaymentMode"]);
                                paymBO.PaymentStatus = read["PaymentStatus"] == DBNull.Value ? string.Empty : Convert.ToString(read["PaymentStatus"]);
                                paymBO.CashDetails.Cash = read["Cash"] == DBNull.Value ? 0 : Convert.ToDecimal(read["Cash"]);
                                paymBO.CashDetails.TransactionDate = read["TransactionDate"] == DBNull.Value ? string.Empty : Convert.ToString(read["TransactionDate"]);
                                paymBO.CashDetails.Currency = read["Currency"] == DBNull.Value ? string.Empty : Convert.ToString(read["Currency"]);
                                paymBO.CashDetails.Description = read["Description"] == DBNull.Value ? string.Empty : Convert.ToString(read["Description"]);
                                paymBO.CreditDetails.CardType = read["CardType"] == DBNull.Value ? string.Empty : Convert.ToString(read["CardType"]);
                                paymBO.CreditDetails.CardNumber = read["CardNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["CardNumber"]);
                                paymBO.CreditDetails.HolderName = read["HolderName"] == DBNull.Value ? string.Empty : Convert.ToString(read["HolderName"]);
                                paymBO.CreditDetails.CVVNumber = read["CVVNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["CVVNumber"]);
                                paymBO.CreditDetails.ExpireDate = read["ExpireDate"] == DBNull.Value ? string.Empty : Convert.ToString(read["ExpireDate"]);
                                paymBO.CreditDetails.CreditAmount = read["CreditAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(read["CreditAmount"]);
                                paymBO.chequeDetails.BankName = read["BankName"] == DBNull.Value ? string.Empty : Convert.ToString(read["BankName"]);
                                paymBO.chequeDetails.BranchName = read["BranchName"] == DBNull.Value ? string.Empty : Convert.ToString(read["BranchName"]);
                                paymBO.chequeDetails.ChequeNumber = read["ChequeNumber"] == DBNull.Value ? 0 : Convert.ToInt32(read["ChequeNumber"]);
                                paymBO.chequeDetails.ChequeDate = read["ChequeDate"] == DBNull.Value ? string.Empty : Convert.ToString(read["ChequeDate"]);
                                paymBO.chequeDetails.ChequeAmount = read["ChequeAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(read["ChequeAmount"]);
                                paymBO.chequeDetails.AccountNumber = read["AccountNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["AccountNumber"]);
                                paymBO.chequeDetails.RoutingNumber = read["RoutingNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["RoutingNumber"]);
                                paymBO.chequeDetails.MemoDescription = read["MemoDescription"] == DBNull.Value ? string.Empty : Convert.ToString(read["MemoDescription"]);
                                paymBO.chequeDetails.ChequeUpload = read["ChequeUpload"] == DBNull.Value ? false : Convert.ToBoolean(read["ChequeUpload"]);
                                paymBO.InsuranceDetails.InsuranceType = read["InsuranceType"] == DBNull.Value ? string.Empty : Convert.ToString(read["InsuranceType"]);
                                paymBO.InsuranceDetails.InsuranceCompany = read["InsuranceCompany"] == DBNull.Value ? string.Empty : Convert.ToString(read["InsuranceCompany"]);
                                paymBO.InsuranceDetails.InsuranceNumber = read["InsuranceNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["InsuranceNumber"]);
                                paymBO.InsuranceDetails.MemberName = read["MemberName"] == DBNull.Value ? string.Empty : Convert.ToString(read["MemberName"]);
                                paymBO.InsuranceDetails.MemberShipNumber = read["MemberShipNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["MemberShipNumber"]);
                                paymBO.InsuranceDetails.GroupNumber = read["GroupNumber"] == DBNull.Value ? string.Empty : Convert.ToString(read["GroupNumber"]);
                                paymBO.InsuranceDetails.PreAuthCode = read["PreAuthCode"] == DBNull.Value ? string.Empty : Convert.ToString(read["PreAuthCode"]);
                            }
                        }
                        read.Close();
                        connect.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _strErrorMsg = ex.Message;
                throw;
            }
            finally
            {
                if (sqlCon != null)
                    sqlCon.Dispose();
            }
            return paymBO;
        }

        public void deletePaymentDetails(PaymentBO payBO)
        {
            PaymentBO paymBO = new PaymentBO();
            paymBO.CashDetails = new CashBO();
            paymBO.CreditDetails = new CreditCardBO();
            paymBO.chequeDetails = new ChequeBO();
            paymBO.InsuranceDetails = new InsuranceBO();
            paymBO.SpecimenDetails = new SpecimenInfoBO();
            try
            {
                using (SqlConnection connect = new SqlConnection(Constant.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("delete from stblPayment where TenantID = @TenantID and SpecimenID = @SpecimenID", connect))
                    {
                        connect.Open();
                        command.Parameters.Add(new SqlParameter("@TenantID", payBO.SpecimenDetails.TenantID));
                        command.Parameters.Add(new SqlParameter("@SpecimenID", payBO.SpecimenDetails.SpecimenID));
                        command.CommandType = CommandType.Text;
                        SqlDataReader read = command.ExecuteReader();
                    }
                }
            }
            catch(Exception e)
            {
                _strErrorMsg = e.Message;
                throw;
            }            
        }
    }    
}
