
namespace STOMS.BO
{
    // this is for Physician or company which orders the test
    public class CustomerBO
    {
        public int CustID { get; set; }
        public int CustomerID { get; set; }
      
        public string CustNumber { get; set; }
        public string Specialization { get; set; }
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
        public string ShipName{get;set;}
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

    public class EntitlemenuBO
    {
        public int EntFuncID { get; set; }
        public string FuncName { get; set; }
        public int FuncOrder { get; set; }
        public string MenuLink { get; set; }
        public int ParentFuncID { get; set; }
        public string DisplayIndicator { get; set; }
        public string isChild { get; set; }
        public string MenuIcon { get; set; }
        public int UserTypeID { get; set; }
    }

    public class UserBO
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserTypeID { get; set; }
        public string UserStatus { get; set; }
        public string LastLogin { get; set; }
        public int PriOrgID { get; set; }
        public string PriOrgName { get; set; }
        public int PriLocationID { get; set; }
        public string UserTypeName { get; set; }
        public bool IsCCMService { get; set; }
        public string UserGroup { get; set; }
        public int UserGroupID { get; set; }
        public string UserDescription { get; set; }
        public string UserName { get; set; }
        public string UserGroupStatus { get; set; }
        public int TenantID{ get; set; }
        public int kProductID { get; set; }
        public bool IsStandard { get; set; }
        public int CreatedBy{ get; set; }
        public int SubscriptionID { get; set; }





    }

    public class CountryMasterBO
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public bool isSelected { get; set; }
    }

    public class SettingBO
    {
        public int CompanyID { get; set; }
        public string CustPrefix { get; set; }
        public string OrderPrefix { get; set; }
        public string POPrefix { get; set; }
        public string DefaultCurrency { get; set; }
        public int VendorMarkup { get; set; }
        public int MinimumMarkup { get; set; }
        public int Tier1Markup { get; set; }
        public int Tier1Default { get; set; }
        public int Tier2Markup { get; set; }
        public int Tier2Default { get; set; }
        public int StdImageWidth { get; set; }
        public int StdImageheight { get; set; }
        public int ThumbNailfactor { get; set; }
        public string BarcodeType { get; set; }
        public string Barcodefield { get; set; }
        public string SettingType { get; set; }
    }

    public class MySettingBO
    {
        public int SettingID { get; set; }
        public int UserID { get; set; }
        public string ShortCutURL1 { get; set; }
        public string ShortCutURL2 { get; set; }
        public string ShortCutURL3 { get; set; }
        public string ShortCutURL4 { get; set; }
        public string PeriodSetting { get; set; }
    }

    public class CrossTenantBO
    {
        public int AppUserID { get; set; }
        public int TenantID { get; set; }
        public int CrossTenantID { get; set; }
        public int PrimaryCompanyID { get; set; }
        public string TenantCode { get; set; } 
        public string AppUserName { get; set; }
        public string TenantName { get; set; }
        public string Status { get; set; }
        



    }

}
