using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{   

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

    public class EmailConfigBO
    {
        //public string UserName { get; set; }
        //public string UserEmailID { get; set; }
        //public string Password { get; set; }
        //public string SMTPServer { get; set; }
        //public string SMTPPortNumber { get; set; }
        //public string UserBackupEmailID { get; set; }
        //public string Enablessl { get; set; }
        public string GreetingText { get; set; }
        public string Signature { get; set; }
        //public static object AuthenticateToken { get; internal set; }
        public string ToAddress { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }

    public class CourierConfigurationBO
    {
        public int CourierID { get; set; }
        public int CourierTenantID { get; set; }        
        public int TenantID { get; set; }        
        public string CourierName { get; set; }
        public int CourierConfigID { get; set; }
    }

    public class CourierConfigBO
    {
        public int FedexACNo { get; set; }
        public int FedexMeterNo { get; set; }
        public string FedexUserKey { get; set; }
        public string FedexUserPassword { get; set; }
        public string FedexParentKey { get; set; }
        public string FedexParentPassword { get; set; }
        public string DefaultWeight { get; set; }
        public bool SignatureOn { get; set; }
        public int TenantID { get; set; }
        public int CourierConfigID { get; set; }     
    }

    public class KitTypeConfigurationBO
    {
        public int KitID { get; set; }
        public string KitName { get; set; }
        public int KitTenantID { get; set; }
        public int TenantID { get; set; }
    }   

    public class ConfigurationBO
    {
        public int ConfigID { get; set; }
        public string ConfigType { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public int TenantID { get; set; }
        public int PrefixYear { get; set; }
        public int SrNumber { get; set; }
    }

}