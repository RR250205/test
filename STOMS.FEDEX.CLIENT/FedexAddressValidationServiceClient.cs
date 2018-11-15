using STOMS.FEDEX.CLIENT.AddressValidationServiceWebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using STOMS.BO;
namespace STOMS.FEDEX.CLIENT
{
   public class FedexAddressValidationServiceClient
    {

        private AddressBO _addressBO=new AddressBO ();
        private CourierConfigBO _configBO = new CourierConfigBO();
        public FedexAddressValidationServiceClient(AddressBO addressBO, CourierConfigBO configBO)
        {
            _addressBO = addressBO;
            _configBO = configBO;
        }
        public string Result()
        {
            string FinalResult = "no";
            bool isContrySupported = false;
            bool isResolved = false;
            AddressValidationRequest request = CreateAddressValidationRequest();
            //
            AddressValidationService service = new AddressValidationService();
            if (usePropertyFile())
            {
                service.Url = getProperty("endpoint");
            }
            //
            try
            {
                // Call the AddressValidationService passing in an AddressValidationRequest and returning an AddressValidationReply
                AddressValidationReply reply = service.addressValidation(request);
                //
                if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    //ShowAddressValidationReply(reply);
                    foreach (AddressValidationResult result in reply.AddressResults)
                    {
                        //Console.WriteLine("Address Id : " + result.ClientReferenceId);
                       // if (result.ClassificationSpecified) { Console.WriteLine("Classification: " + result.Classification); }
                      //  if (result.StateSpecified) { Console.WriteLine("State: " + result.State); }
                     //   Console.WriteLine("Proposed Address--");
                    //    Address address = result.EffectiveAddress;
                      //  foreach (String street in address.StreetLines)
                      //  {
                      //      Console.WriteLine("   Street: " + street);
                       // }
                      //  Console.WriteLine("     City: " + address.City);
                      //  Console.WriteLine("    ST/PR: " + address.StateOrProvinceCode);
                       // Console.WriteLine("   Postal: " + address.PostalCode);
                       // Console.WriteLine("  Country: " + address.CountryCode);
                       // Console.WriteLine();
                      //  Console.WriteLine("Address Attributes:");
                        foreach (AddressAttribute attribute in result.Attributes)
                        {
                            if (attribute.Name == "CountrySupported" && attribute.Value == "true")
                                isContrySupported = true;
                            else if (attribute.Name == "Resolved" && attribute.Value == "true")
                                isResolved = true;
                            //Console.WriteLine("  " + attribute.Name + ": " + attribute.Value);

                        }
                    }
                }
                else
                {
                   // foreach (Notification notification in reply.Notifications)
                   //     Console.WriteLine(notification.Message);
                }
            }
            catch (SoapException e)
            {
               // Console.WriteLine(e.Detail.InnerText);
            }
            catch (Exception e)
            {
               // Console.WriteLine(e.Message);
            }
            //  Console.WriteLine("Press any key to quit!");
            //Console.ReadKey();
            if (isContrySupported && isResolved)
                FinalResult = "yes";
            return FinalResult;
        }

        //private AddressValidationRequest AddressValidateRequest()
        //{
        //    return CreateAddressValidationRequest();
        //}
        //private AddressValidationReply AddressValidationReply(AddressValidationRequest request)
        //{
        //     AddressValidationService service = new AddressValidationService();

        //    return service.addressValidation(request);           
        //}

        //public void Initilise()
        //{
           
        //}

        private AddressValidationRequest CreateAddressValidationRequest()
        {
            // Build the AddressValidationRequest
            AddressValidationRequest request = new AddressValidationRequest();
            //
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = _configBO.FedexUserKey.Trim();//"IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = _configBO.FedexUserPassword.Trim();//"uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX" with the Password
            request.WebAuthenticationDetail.ParentCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.ParentCredential.Key = _configBO.FedexParentKey.Trim(); //"IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.ParentCredential.Password = _configBO.FedexParentPassword.Trim(); //"uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX"
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.WebAuthenticationDetail.UserCredential.Key = getProperty("key");
                request.WebAuthenticationDetail.UserCredential.Password = getProperty("password");
                request.WebAuthenticationDetail.ParentCredential.Key = getProperty("parentkey");
                request.WebAuthenticationDetail.ParentCredential.Password = getProperty("parentpassword");
            }
            //
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = Convert.ToString(_configBO.FedexACNo);//"510088000"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = Convert.ToString(_configBO.FedexMeterNo);// "118817920"; // Replace "XXX" with the client's meter number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.ClientDetail.AccountNumber = getProperty("accountnumber");
                request.ClientDetail.MeterNumber = getProperty("meternumber");
            }
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Address Validation Request using VC#***"; // The client will get the same value back in the reply
            //
            request.Version = new VersionId(); // Creates the Version element with all child elements populated
            //
            request.InEffectAsOfTimestamp = DateTime.Now;
            request.InEffectAsOfTimestampSpecified = true;
            //
            SetAddress(request);
            //
            return request;
        }

        private  void SetAddress(AddressValidationRequest request)
        {
            request.AddressesToValidate = new AddressToValidate[1];
            request.AddressesToValidate[0] = new AddressToValidate();
            request.AddressesToValidate[0].ClientReferenceId = "ClientReferenceId1";
            request.AddressesToValidate[0].Address = new Address();

            request.AddressesToValidate[0].Address.StreetLines = new String[3] { _addressBO.Address1, _addressBO.Address2,_addressBO.Address3 };
            //if(_addressBO.Address2!="")
            //    request.AddressesToValidate[0].Address.StreetLines = new String[2] { _addressBO.Address2 };
            //if (_addressBO.Address3 != "")
            //    request.AddressesToValidate[0].Address.StreetLines = new String[3] { _addressBO.Address3 };
            request.AddressesToValidate[0].Address.PostalCode = _addressBO.Zip;
            request.AddressesToValidate[0].Address.City = _addressBO.City;
            request.AddressesToValidate[0].Address.StateOrProvinceCode = _addressBO.State;
            request.AddressesToValidate[0].Address.CountryCode = "US";
            //
            //request.AddressesToValidate[1] = new AddressToValidate();
            //request.AddressesToValidate[1].ClientReferenceId = "ClientReferenceId2";
            //request.AddressesToValidate[1].Address = new Address();
            //request.AddressesToValidate[1].Address.StreetLines = new String[1] { "167 PROSPECT HIGHWAY" };
            //request.AddressesToValidate[1].Address.PostalCode = "2147";
            //request.AddressesToValidate[1].Address.City = "New SOUTH WALES";
            //request.AddressesToValidate[1].Address.CountryCode = "AU";

            //request.AddressesToValidate[2] = new AddressToValidate();
            //request.AddressesToValidate[2].ClientReferenceId = "ClientReferenceId3";
            //request.AddressesToValidate[2].Address = new Address();
            //request.AddressesToValidate[2].Address.StreetLines = new String[2] { "3 WATCHMOOR POINT", "WATCHMOOR ROAD" };
            //request.AddressesToValidate[2].Address.PostalCode = "GU153AQ";
            //request.AddressesToValidate[2].Address.City = "CAMBERLEY";
            //request.AddressesToValidate[2].Address.CountryCode = "GB";
        }

        //private  void ShowAddressValidationReply(AddressValidationReply reply)
        //{
        //    Console.WriteLine("AddressValidationReply details:");
        //    Console.WriteLine("*****************************************************");
        //    foreach (AddressValidationResult result in reply.AddressResults)
        //    {
        //        Console.WriteLine("Address Id : " + result.ClientReferenceId);
        //        if (result.ClassificationSpecified) { Console.WriteLine("Classification: " + result.Classification); }
        //        if (result.StateSpecified) { Console.WriteLine("State: " + result.State); }
        //        Console.WriteLine("Proposed Address--");
        //        Address address = result.EffectiveAddress;
        //        foreach (String street in address.StreetLines)
        //        {
        //            Console.WriteLine("   Street: " + street);
        //        }
        //        Console.WriteLine("     City: " + address.City);
        //        Console.WriteLine("    ST/PR: " + address.StateOrProvinceCode);
        //        Console.WriteLine("   Postal: " + address.PostalCode);
        //        Console.WriteLine("  Country: " + address.CountryCode);
        //        Console.WriteLine();
        //        Console.WriteLine("Address Attributes:");
        //        foreach (AddressAttribute attribute in result.Attributes)
        //        {
        //            Console.WriteLine("  " + attribute.Name + ": " + attribute.Value);
        //        }
        //    }
        //}
        private  bool usePropertyFile() //Set to true for common properties to be set with getProperty function.
        {
            //return getProperty("usefile").Equals("True");
            return false;
        }
        private  String getProperty(String propertyname) //Sets common properties for testing purposes.
        {
            try
            {
                String filename = "C:\\filepath\\filename.txt";
                if (System.IO.File.Exists(filename))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(filename);
                    do
                    {
                        String[] parts = sr.ReadLine().Split(',');
                        if (parts[0].Equals(propertyname) && parts.Length == 2)
                        {
                            return parts[1];
                        }
                    }
                    while (!sr.EndOfStream);
                }
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
            catch (Exception)
            {
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
        }
    }
}
