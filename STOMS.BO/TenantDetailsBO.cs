using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
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
        public string TenantRegNo { get; set; }
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
        public string OrgRegNo { get; set; }
        public bool IsProfileComplete { get; set; }
        public string ContactUsTelephone { get; set; }
        public string ContactUsEmailID { get; set; }
    }
    
}
