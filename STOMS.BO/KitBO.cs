using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
  public  class OrderKitBO
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

        public List<CourierNotificationBO> oNotifications { get; set; }
        public string Exception { get; set; }        
        public string FullAddress { get; set; }
    }

   

    public class KitOrderBO
    {
        public int TenantID { get; set; }
        public int KitID { get; set; }
        public string KitNumber { get; set; }
        public string KitType { get; set; }
        public int ReUseCount { get; set; }
        public string Status { get; set; }
        public DateTime DestroyedOn { get; set; }
        public int CustNumber { get; set; }
        public int RequestID { get; set; }
        public string RequestNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public int NoOfKits { get; set; }
        public int OrderKitID { get; set; }
        public int CustID { get; set; }
        public string CourierType { get; set; }
        public string  LabelNumber { get; set; }
        public string LabelGenaratedOn { get; set; }
        
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

    public class AddressBO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
      //  public string OrgName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string AddressStatus { get; set; }
        public int TenantID { get; set; }
        public int CourierShipperID { get; set; }
        public string CompanyName { get; set; }
        public bool isDefault { get; set; }
    }

    public class CourierNotificationBO
    {
        public string cNotification { get; set; }
    }
}

