
using System.Collections.Generic;

namespace KoSoft.Entitlement
{

    public class AppUserBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string Password { get; set; }
        public string AppUserStatus { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Salutation { get; set; }
        public string Suffix { get; set; }
        public string FullName { get; set; }
        public string UserStatus { get; set; }
        public string Department { get; set; }
        public string ProfilePhoto { get; set; }
        public int ProfilePhotoID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }        
        public int UserAccessID { get; set; }
        public string UserAccessType { get; set; }
        public bool IsActivated { get; set; }
        public string AccesKey { get; set; }
        public string KeyGenDate { get; set; }
        public string KeyExpiryTime { get; set; }
        public string ActivatedOn { get; set; }
        public string ActIPAddress { get; set; }
        public string LastLoginTime { get; set; }
        public string LoginFromIP { get; set; }
        public TenantBO Company { get; set; }
        public List<UserRoleBO>  MyRoles { get; set; }
        public int ReturnCode { get; set; }
        public string DefaultPage { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public string PhoneCarrier { get; set; }
        public string VerificationCode { get; set; }
        public string CodeGenTime { get; set; }
        public string CodeExpTime { get; set; }
        public  ErrorMessageBO ErrorMsg { get; set; }
    }    

    public class TenantBO
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string TenantStatus { get; set; }
        public string Address1{ get; set; }
        public string Address2{ get; set; }
        public string City{ get; set; }
        public string State{ get; set; }
        public string Zip{ get; set; }
        public string Country{ get; set; }
        public string CurrentPackage{ get; set; }
        public string SubscribedOn{ get; set; }
        public string SubExpiryDate{ get; set; }      
        public string TaxID { get; set; }
        public string InCorpState { get; set; }
        public string TenantType { get; set; }
    }

    public class UserGroupBO
    {
        public int UserGroupID { get; set; }
        public string UserGroup { get; set; }
        public string UserGroupDesc { get; set; }
        public bool IsStandard { get; set; }
        public string UserGroupStatus { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string MemberCount { get; set; }
       
    }

   

    public class UserFunctionBO
    {
        public int UserID { get; set; }
        public string FunctionIDs { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public bool FunctionType { get; set; }
        public int UserFuncID { get; set; }
    }

    public class GroupMemberBO
    {
        public int GroupMemberID { get; set; }
        public int AppUserID { get; set; }
        public int UserGroupID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string UserGroup { get; set; }
        public string UserDept { get; set; }
        public AppUserBO UserBO { get; set; }
        public UserGroupBO userGroupBO { get; set; }
    }

    public class EntitleServiceBO
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceOrder { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceType { get; set; }
        public int ParentServiceID { get; set; }
        public string ParentServiceName { get; set; }
        public string serviceDesc { get; set; }
        public string serviceGroup { get; set; }
        public bool Read_Attri { get; set; }
        public bool Write_Attri { get; set; }
        public bool Delete_Attri { get; set; }
        public bool Approve_Attri { get; set; }
        public bool Export_Attri { get; set; }
        public bool isEntitlable { get; set; }
        public int globalOrderBy { get; set; }
        public string displayIndicator { get; set; }
        public bool isChild { get; set; }
        public string MenuIcon { get; set; }
        public int TenantID { get; set; }
    }

    public class EntitleServiceUserGroupBO
    {
        public int GroupID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceAttributes { get; set; }
    }

    public class EntitleModuleBO
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string NavURL { get; set; }
        public int ParentMenuID { get; set; }
        public int MenuOrder { get; set; }
        public string UserGroup { get; set; }
    }

    public class UserRoleBO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleCategory { get; set; }
        public string RoleDescription { get; set; }
        public int ReportToRoleID { get; set; }
        public string RoportingToRole { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public string RoleStatus { get; set; }
        public string UserRoleStatus { get; set; }
        public bool IsPrimary { get; set; }
        public int UserID { get; set; }
    }

    public class AuthLogBO
    {
        public int AuthID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string AuthDate { get; set; }
        public string ClientIP { get; set; }
        public string AccesRegion { get; set; }
        public string AuthStatus { get; set; }
    }

    // Following BO created to simiply WCF method calling
    public class UserAuthBO
    {
        public int AppUserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string UserStatus { get; set; }
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
    }

    public class ErrorMessageBO
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorDesc { get; set; }
        public string Module { get; set; }
        public int ProductID { get; set; }
    }

    public class FunctionBO
    {
        public string FunctionName { get; set; }
        public int FunctionID { get; set; }
        public string FunctionCode { get; set; }
        public int ParentFunctionID { get; set; }
        public string ProductID { get; set; }
        public string FunctionStatus { get; set; }
        public bool ReadAccess { get; set; }
        public bool WriteAccess { get; set; }
        public bool ExecuteAccess { get; set; }
        public bool ExportAccess { get; set; }
        public bool RoleSpecificAccess { get; set; }
        public int srvUserGroupID { get; set; }
        public int UserGroupFunctionID { get; set; }
        public EntitleServiceBO entServBO { get; set; }
    }

    public class EntSubscriptionBO
    {
        public int EntSubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
        public string SubscriptionDesc { get; set; }
        public string SubscriptionStatus { get; set; }

    }

    public class SubscribProdBO
    {
        public int SubscriptionID { get; set; }
        public int TenantID { get; set; }   
        public int KProductID { get; set; }
        public List<FunctionBO> oFunction { get; set; }

    }

    

}