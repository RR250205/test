using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    #region Invoice BOs
    public class InvoiceDetailBO
    {
        public int InvDetailID { get; set; }
        public int InvID { get; set; }
        public int ItemID { get; set; }
        public string ItemDesc { get; set; }
        public int ItemOrder { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
    }

    public class PreInsurance
    {
        public int PreInsuranceNo { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Dataofbirth { get; set; }
        public string MobileNumber { get; set; }
        public string PrimaryInsName { get; set; }
        public string InsuranceCard_IDno { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyName { get; set; }
        public Boolean OtherInsurance { get; set; }
        public string PreAuthoriztionToken { get; set; }
        public int TenantID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Tenantname { get; set; }
        public string TenantCode { get; set; }

    }

    public class InvoicePaymentBO
    {
        public int PayID { get; set; }
        public int InvID { get; set; }
        public int PayTypeID { get; set; }
        public string PayMode { get; set; }
        public decimal PayAmount { get; set; }
    }

    public class InvoiceBO
    {
        public int InvoiceID { get; set; }
        public string InvNumber { get; set; }
        public string InvDate { get; set; }
        public decimal InvAmount { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal DiscountType { get; set; }
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string InvNotes { get; set; }
        public string InvStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string InvFile { get; set; }
        public List<InvoiceDetailBO> InvoiceDetail { get; set; }
    }
    #endregion

    public class OrderBO
    {
        public int TenantID { get; set; }
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string StatusCode { get; set; }
        public int OrderCount { get; set; }
        public int Samplescount { get; set; }
        public int SampleCount { get; set; }

        public string ShipName { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipZipCode { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }
        public string ShipOption { get; set; }
        public string DeliveryEmail { get; set; }
        public string AppEmail {get; set;}


        public string BillName { get; set; }
        public string BillAddress1 { get; set; }
        public string BillAddress2 { get; set; }
        public string BillCity { get; set; }
        public string BillZipCode { get; set; }
        public string BillState { get; set; }
        public string BillCountry { get; set; }

        public decimal OrderDiscount { get; set; }
        public string DiscountType { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderNetAmount { get; set; }
        public int ClientDiscountTier { get; set; }

        public string CompletedBy { get; set; }
        public string CompletedOn { get; set; }

        public string GeneralOrderPhone { get; set; }
        public string GeneralOrderName { get; set; }
        public int CustomerID { get; set; }
        public string PaymentType { get; set; }
        public CustomerBO objCustomer { get; set; }
        public SpecimenInfoBO specimenInfoBO { get; set; }
        public DocumentBO OrdDocument { get; set;}
        public PatientBO Ordpatient { get; set;}
        //public OrderBO objOrderBO { get; set; }
        
        public string CustomerName { get; set; }
        public bool IsFolateBinding { get; set; }
        public bool IsFolateBlocking { get; set; }
        public string BillAddSameAs { get; set; }
    }

    public class TestResultBO
    {
        public int TTrackID { get; set; }
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public string OrderCustomer { get; set; }  // Grouping filed for display purpose
        public PatientBO oPatient { get; set; }

        public bool IsFolateBinding { get; set; }  // This is redudant, but still we keep it
        public bool IsFolateBlocking { get; set; } // This is redudant, but still we keep it

        public string SampleBarCode { get; set; }
        public string DateAssayBIN { get; set; }
        public string DateAssayBLO { get; set; }
        public int ResultID { get; set; }
        public string ResultBIN { get; set; }
        public string ResultBL { get; set; }
        public string ResultSentDate { get; set; }
        public string SampleStatus { get; set; }
        public string DateDrawn { get; set; }
        public string DateReceived { get; set; }
        public int PatientAge { get; set; }

        public decimal BindingNeg { get; set; }
        public decimal BindingBL { get; set; }
        public decimal BindingPos { get; set; }

        public decimal BlockingNeg { get; set; }
        public decimal BlockingBL { get; set; }
        public decimal BlockingPos { get; set; }
        public decimal PtsNeg { get; set; }
        public decimal PtsAnyPos { get; set; }
        public decimal PtsBL { get; set; }
        public decimal PtsBothPos { get; set; }
        public decimal PtspBL { get; set; }
        public decimal ResultPos { get; set; }
        public decimal ResultNeg { get; set; }
        public decimal BINCost { get; set; }
        public decimal BLOCost { get; set; }
    }

    public class ResultBO
    {
        public int AssaySpecimenID { get; set; }
        public int AssayID { get; set; }
        public int SpecimenID { get; set; }
        public bool IsRepeat { get; set; }
        public int RepeatNo { get; set; }
        public string OutofBINDate { get; set; }
        public string AssignedToBINOn { get; set; }
        public string SpecimenType { get; set; }
        public string SubSpecimenType { get; set; }
        public string RemainVol { get; set; }
        public decimal BindValue { get; set; }
        public string BindValComment { get; set; }
        public decimal BlockValue { get; set; }
        public string BlockValComment { get; set; }
        public string ResultFileName { get; set; }
        public int ResultDocID { get; set; }
        public string ResultSentDate { get; set; }

        //Following property may need to be reviewed and deleted in future
        public string ResultBIN { get; set; }
        public string ResultBL { get; set; }

        public decimal BindingNeg { get; set; }
        public decimal BindingBL { get; set; }
        public decimal BindingPos { get; set; }

        public decimal BlockingNeg { get; set; }
        public decimal BlockingBL { get; set; }
        public decimal BlockingPos { get; set; }
        public decimal PtsNeg { get; set; }
        public decimal PtsAnyPos { get; set; }
        public decimal PtsBL { get; set; }
        public decimal PtsBothPos { get; set; }
        public decimal PtspBL { get; set; }
        public decimal ResultPos { get; set; }
        public decimal ResultNeg { get; set; }
        public decimal BINCost { get; set; }
        public decimal BLOCost { get; set; }
        public bool NeedToRetest { get; set; }
        public string  AssayStatus { get; set; }
    }

    public class OrderPaymentBO
    {
        public int OrderID { get; set; }
        public int PayTypeID { get; set; }
        public decimal PayAmount { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardExp { get; set; }
        public string CVV { get; set; }
        public string BankName { get; set; }
        public string CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string PayReference { get; set; }
        public string TransCode { get; set; }
        public DateTime PayDate { get; set; }
        public string CreditAllowComments { get; set; }
        
    }

    public class PatientBO
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string GuardianContactNo { get; set; }
        public string GuardianEmailID { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Diagnosis { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public string Street { get; set; }
        public int OrderID { get; set; }
        public string GuardianFirstName { get; set; }
        public string GuardianLastName { get; set; }
        public string GuardianStreet { get; set; }
        public string GuardianCity { get; set; }
        public string GuardianState { get; set; }
        public string GuardianZip { get; set; }
        public string GuardianCountry { get; set; }
        public bool isPatientAddressSame { get; set; }
        public string GuardianRelationship { get; set; }
        public string Facility { get; set; }
        public string RequesterType { get; set; }
        public string Specialization { get; set; }
        public string SpecimenNumber { get; set; }
        public string TestType { get; set; }
    }

    public class SpecimenInfoBO
    {
        public int SpecimenID { get; set; }
        public int PatientID { get; set; }
        public int AssaySpecimenID { get; set; }
        public string SpecimenNumber { get; set; }
        public string DateDrawn { get; set; }
        public string TimeDrawn { get; set; }
        public string TransitTime { get; set; }
        public string SampleReceiveDate { get; set; }
        public string ReceivedTime { get; set; }
        public string TransitTemperature { get; set; }
        public string VolumeReceived { get; set; }
        public string RemainingVolume { get; set; }
        public string unit{ get; set;}
        public string SpecimentType { get; set; }
        public string BloodType { get; set; }
        public string InterSubstance { get; set; }
        public bool IsConsent { get; set; }
        public bool IsRejection { get; set; }
        public bool IsSpecimenAccept { get; set; }
        public string RejectReasons { get; set; }
        public int ReqFormCopyID { get; set; }
        public string Comment { get; set; }
        public string SpecimenStatus { get; set; }
        public int CreatedBy { get; set; }
        public string TestType { get; set; }
        public string TestStatus { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedOn { get; set; }        
        public int TenantID { get; set; }
        public int CustomerID { get; set; }
        public PatientBO oPatient { get; set; }
        public CustomerBO oCustomer { get; set; }
        public ResultBO oResult { get; set; }
        public string SpecimenError { get; set; }
        public string PaymentMode { get; set; }
        public string PendingReasons{ get; set; }
        public string ReactivateReason{ get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Facility { get; set; }
        public string RequesterType { get; set; }
    }

    public class SpcimenTypeBO
    {
        public int RefTPTestCollectionID { get; set; }
        public int RefTestProfileID { get; set; }
        public int RefSpecimenID { get; set; }
        public int RefDiagDiseaseID { get; set; }
        public int TenantID { get; set; }
        public bool isSolid { get; set; }
        public bool isSemiSolid { get; set; }
        public bool isLiquid { get; set; }
        public string MinML { get; set; }
        public string MaxML { get; set; }
        public string MinKG { get; set; }
        public string MaxKG { get; set; }
        public string TPTestColectionDesc{get; set;}
        public string ChemicalParams { get; set; }
        public string TPTestCollectionStatus { get; set; }

        //stblRefSpecimens
        public int CreatedBy { get; set; }
        public string RefSpecimenName { get; set; }
        public string RefSpecimenCode { get; set; }
        public string SpecimenStatus { get; set; }

        //stblRefTestProfile
        public int TestProfileID { get; set; }
        public string TestProfileName { get; set; }
        public string TestProfileCode { get; set; }
        public string TestProfileDesc { get; set; }
        public string TestProfileStatus { get; set; }
        public bool isDiagnosis { get; set; }
        public bool isPanel { get; set; }
        public string ResultParams { get; set; }
        public string ObservationParams { get; set; }
    }

    public class AssayGroupBO
    {
        public int AssayID { get; set; }
        public string AssayBIN { get; set; }
        public string AssayType { get; set; }
        public string AssayName { get; set; }
        public string AssayDesc { get; set; }
        public string CreatedOn { get; set; }
        public string SampleMaxDate { get; set; }
        public string AssayLoadDateTime { get; set; }
        public string AssayCompleteDateTime { get; set; }
        public int SampleCount { get; set; }
        public string AssayStatus { get; set; }
        public int TenantID { get; set; }
        public List<SpecimenInfoBO> oSample { get; set; }
    }

    public class DocumentBO
    {
        public int DocID { get; set; }
        public string DocumentName { get; set; }
        public string DocNumber { get; set; }
        public int RepsitoryID { get; set; }
        public string DocType { get; set; }
        public string OrgDocName { get; set; }
        public string GenDocName { get; set; }
        public string DocStatus { get; set;}
        public int Rentention { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public string TokenID { get; set; }
        public DateTime ValidUpto { get; set; }
    }
}