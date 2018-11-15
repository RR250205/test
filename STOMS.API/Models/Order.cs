using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STOMS.API.Models
{
    public class  KitOrder
    {
        public int KitOrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RequesterType { get; set; }
        public string OrgName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int NoOfKits { get; set; }
        public string Message { get; set; }
        public string AuthenticateToken { get; set; }
        public string KitType { get; set; }
    }

    public class CustomerBO
    {
        public int CustID { get; set; }
        public int CustomerID { get; set; }

        public string CustNumber { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Facility { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public string RequesterType { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
        public string ShipName { get; set; }
        public string Email { get; set; }
        public int NoOfKits { get; set; }
        public int RequestID { get; set; }
        public string KitType { get; set; }
        public string RequestDate { get; set; }
        public string RequestNumber { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public int TenantID { get; set; }
        public string Diagnosis { get; set; }
        public string DiagnosisCode { get; set; }
        public string ResultType { get; set; }
    }
    public class OrderRequestBO
    {
        public int RequestID { get; set; }
        public string RequestNumber { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public int NoOfKits { get; set; }
        public string KitType { get; set; }
        public int TenantID { get; set; }

    }

    public class OrderKitBO
    {
        public int KitOrderID { get; set; }
        public int RequestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RequesterType { get; set; }
        public string Facility { get; set; }
        public string OrgName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NoOfKits { get; set; }
        public string Message { get; set; }
        public int TenantID { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string KitType { get; set; }
        public string RequestNumber { get; set; }
        public string CourierName { get; set; }
        public string TrackNumber { get; set; }
        public string DeleveryOn { get; set; }
        public string LabelGeneratedOn { get; set; }
        public string PickedUpOn { get; set; }

       
        public string Exception { get; set; }
        public string FullAddress { get; set; }
    }
    public class TenantDetailsBO
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string TenantType { get; set; }
        public int ParentTenantID { get; set; }
        public string TaxID { get; set; }
        public string InCorpState { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string SmallLogo { get; set; }
        public string DomainPrefix { get; set; }
        public int TenantAdminID { get; set; }
        public string TenantStatus { get; set; }
        public DateTime ProfileCreatedOn { get; set; }
        public string CreatedFromIP { get; set; }
        public string GeoRegion { get; set; }
        public string BillAddress1 { get; set; }
        public string BillAddress2 { get; set; }
        public string BillCity { get; set; }
        public string BillZipCode { get; set; }
        public string BillState { get; set; }
        public string BillCountry { get; set; }
        public bool IsProfileComplete { get; set; }
        public string ContactUsTelephone { get; set; }
        public string ContactUsEmailID { get; set; }
    }

    public class EmailEnablementBO
    {
        public int TenantID { get; set; }
        public int EmailEnablementID { get; set; }
        public bool isToEndUser { get; set; }
        public bool isToTenant { get; set; }
        public string ToTenantEmails { get; set; }
    }

    public class EmailEnablementTypeBO
    {
        public int EmailEnablementTypeID { get; set; }
        public string EmailEnablementType { get; set; }
        public string EndUserTemplate { get; set; }
        public string TenantTemplate { get; set; }
        public string Status { get; set; }
        public EmailEnablementBO emailEnablementBO { get; set; }
    }

    public class EmailEnablementImplementationBO
    {
        public EmailEnablementBO emailEnablementBO { get; set; }
        public EmailEnablementTypeBO emailEnablementTypeBO { get; set; }
        public string type { get; set; }
    }

}