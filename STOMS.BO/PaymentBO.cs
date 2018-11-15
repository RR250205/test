using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    public class PaymentBO
    {
        public int PaymentID { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
        public CashBO CashDetails { get; set; }
        public CreditCardBO CreditDetails { get; set; }
        public ChequeBO chequeDetails  { get; set; }
        public InsuranceBO InsuranceDetails { get; set; }
        public SpecimenInfoBO SpecimenDetails { get; set; }
    }

    public class CashBO
    {
        public decimal Cash { get; set; }
        public string TransactionDate { get; set; }
        //public string TransactionTime { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }

    public class CreditCardBO
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string CVVNumber { get; set; }
        public string ExpireDate { get; set; }
        public decimal CreditAmount { get; set; }
    }
        
    public class ChequeBO
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int ChequeNumber { get; set; }
        public string ChequeDate { get; set; }
        public decimal ChequeAmount { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string MemoDescription { get; set; }
        public bool ChequeUpload { get; set; }
    }

    public class InsuranceBO
    {
        public string InsuranceType { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceNumber { get; set; }        
        public string MemberName { get; set; }
        public string MemberShipNumber { get; set; }
        public string GroupNumber { get; set; }
        public string PreAuthCode { get; set; }
        public int PreInsuranceNo { get; set; }
        //public CoPaymentBO coPaymentDetails { get; set; }
    }

    public class CoPaymentBO
    {
        public CashBO CashDetails { get; set; }
        public CreditCardBO CreditDetails { get; set; }
        public ChequeBO chequeDetails { get; set; }
    }
    
}
