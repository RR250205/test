using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace STOMS.API.Models
{
    public class PreInsurance 
    {
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
    }
}