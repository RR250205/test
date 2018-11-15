using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STOMS.FEDEX.CLIENT.ShipServiceWebReference;
using System.Web.Services.Protocols;
using System.IO;
using STOMS.BO;

namespace STOMS.FEDEX.CLIENT
{
  public class FedexShipServiceClient
    {
           private CourierConfigBO _credentials = new CourierConfigBO();
           private decimal _weight = 0;
           private string _packagingType = "FEDEX_BOX";
           private AddressBO _senderBO = new AddressBO();
           private AddressBO _receiverBO = new AddressBO();
        private string _path = "";
        public FedexShipServiceClient(CourierConfigBO credentials, decimal weight, string packagingType, AddressBO senderBO,AddressBO receiverBO, string path)
        {
            _credentials = credentials;
            _weight = weight;
            _packagingType = packagingType;
            _senderBO = senderBO;
            _receiverBO = receiverBO;
            _path = path;
                
                
       }
        public OrderKitBO MakeShipment()
        {
            OrderKitBO obo = new OrderKitBO();
            // Set this to true to process a COD shipment and print a COD return Label
            bool isCodShipment = false;
            ProcessShipmentRequest request = CreateShipmentRequest(isCodShipment);
           // LogXML(request, typeof(ProcessShipmentRequest));
            //
            ShipService service = new ShipService();
            if (usePropertyFile())
            {
                service.Url = getProperty("endpoint");
            }
            //
            try
            {
                // Call the ship web service passing in a ProcessShipmentRequest and returning a ProcessShipmentReply
                ProcessShipmentReply reply = service.processShipment(request);
                //
                if ((reply.HighestSeverity != NotificationSeverityType.ERROR) && (reply.HighestSeverity != NotificationSeverityType.FAILURE))
                {
                    // ShowShipmentReply(isCodShipment, reply);
                    //LogXML(reply, typeof(ProcessShipmentReply));
                    foreach (CompletedPackageDetail packageDetail in reply.CompletedShipmentDetail.CompletedPackageDetails)
                    {
                        
                        if (packageDetail.TrackingIds != null)
                        {
                            for (int i = 0; i < packageDetail.TrackingIds.Length; i++)
                            {
                                obo.TrackNumber=packageDetail.TrackingIds[i].TrackingNumber;
                                obo.LabelGeneratedOn = DateTime.Now.ToShortDateString();
                            }
                        }
                        ShowShipmentLabels(isCodShipment, reply.CompletedShipmentDetail, packageDetail);
                    }
                    if (reply.CompletedShipmentDetail.OperationalDetail.DeliveryDateSpecified)
                    {
                        obo.DeleveryOn= reply.CompletedShipmentDetail.OperationalDetail.DeliveryDate.ToShortDateString();
                    }
                }
                List<CourierNotificationBO> notifi = new List<CourierNotificationBO>();
                for (int i = 0; i < reply.Notifications.Length; i++)
                {
                    Notification notification = reply.Notifications[i];
                    notifi.Add(new CourierNotificationBO()
                    {
                        cNotification = notification.Message,
                    });
                    //obo.oNotifications.Add(new CourierNotificationBO()
                    //{
                    //    cNotification = notification.Message,
                    //    //cNotification = "Notification no. " + i + " Severity: " + notification.Severity + " Code: " + notification.Code + " Message: " + notification.Message,
                    //});
                    /* Console.WriteLine("Notification no. {0}", i);
                     Console.WriteLine(" Severity: {0}", notification.Severity);
                     Console.WriteLine(" Code: {0}", notification.Code);
                     Console.WriteLine(" Message: {0}", notification.Message);
                     Console.WriteLine(" Source: {0}", notification.Source);*/
                }
                obo.oNotifications = notifi;
                //ShowNotifications(reply);
            }
            catch (SoapException e)
            {
                obo.Exception = e.Detail.InnerText;
                //Console.WriteLine(e.Detail.InnerText);
            }
            catch (Exception e)
            {
                //obo.Exception = e.Message;
                obo.Exception = "Exception: " + "Target Site: " + e.TargetSite + "message : " + e.Message;

                //Console.WriteLine(e.Message);
            }
            //  Console.WriteLine("Press any key to quit !");
            //  Console.ReadKey();
            return obo;
        }

        private  ProcessShipmentRequest CreateShipmentRequest(bool isCodShipment)
        {
            // Build the ShipmentRequest
            ProcessShipmentRequest request = new ProcessShipmentRequest();
            //
            SetShipmentDetails(request);
            //
            SetPackageLineItems(isCodShipment, request);
            //
            return request;
        }

        private  WebAuthenticationDetail SetWebAuthenticationDetail()
        {
            WebAuthenticationDetail wad = new WebAuthenticationDetail();
            wad.UserCredential = new WebAuthenticationCredential();
            wad.UserCredential.Key = _credentials.FedexUserKey.Trim(); //"IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            wad.UserCredential.Password = _credentials.FedexUserPassword.Trim();//"uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX" with the Password
            wad.ParentCredential = new WebAuthenticationCredential();
            wad.ParentCredential.Key = _credentials.FedexParentKey.Trim(); //"IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            wad.ParentCredential.Password = _credentials.FedexParentPassword.Trim(); //"uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX"
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                wad.UserCredential.Key = getProperty("key");
                wad.UserCredential.Password = getProperty("password");
                wad.ParentCredential.Key = getProperty("parentkey");
                wad.ParentCredential.Password = getProperty("parentpassword");
            }
            return wad;
        }

        private  void SetShipmentDetails(ProcessShipmentRequest request)
        {
            request.WebAuthenticationDetail = SetWebAuthenticationDetail();
            //
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = Convert.ToString( _credentials.FedexACNo);//"510088000"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = Convert.ToString(_credentials.FedexMeterNo); //"118817920"; // Replace "XXX" with the client's meter number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.ClientDetail.AccountNumber = getProperty("accountnumber");
                request.ClientDetail.MeterNumber = getProperty("meternumber");
            }
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Express Domestic Ship Request using VC#***"; // The client will get the same value back in the response
            //
            request.Version = new VersionId();
            //
            request.RequestedShipment = new RequestedShipment();
            request.RequestedShipment.ShipTimestamp = DateTime.Now; // Ship date and time
            request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP;
            request.RequestedShipment.ServiceType = ServiceType.PRIORITY_OVERNIGHT; // Service types are STANDARD_OVERNIGHT, PRIORITY_OVERNIGHT, ...

            PackagingType selectedtype;
            if (Enum.TryParse(_packagingType, out selectedtype))
            {
                switch (selectedtype)
                {
                    case PackagingType.FEDEX_10KG_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_25KG_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_ENVELOPE:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_EXTRA_LARGE_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_LARGE_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_MEDIUM_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_PAK:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_SMALL_BOX:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.FEDEX_TUBE:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    case PackagingType.YOUR_PACKAGING:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                    default:
                        request.RequestedShipment.PackagingType = selectedtype;
                        break;
                }
            }

              //  request.RequestedShipment.PackagingType = PackagingType.FEDEX_LARGE_BOX;   //PackagingType.FEDEX_LARGE_BOX; // Packaging type FEDEX_BOK, FEDEX_PAK, FEDEX_TUBE, YOUR_PACKAGING, ...
            //
            request.RequestedShipment.TotalWeight = new Weight(); // Total weight information
            request.RequestedShipment.TotalWeight.Value = _weight ; //20M;
            request.RequestedShipment.TotalWeight.Units = WeightUnits.LB;
            //
            request.RequestedShipment.PackageCount = "1";
            //
            SetSender(request);
            //
            SetRecipient(request);
            //
            SetPayment(request);
            //
            SetRMA(request);
            //
            SetLabelDetails(request);
        }

        private  void SetSender(ProcessShipmentRequest request)
        {
            request.RequestedShipment.Shipper = new Party();
            request.RequestedShipment.Shipper.Contact = new Contact();
            request.RequestedShipment.Shipper.Contact.PersonName = _senderBO.FirstName.Trim(); //"Sender Name";
            request.RequestedShipment.Shipper.Contact.CompanyName = _senderBO.CompanyName.Trim(); //"Sender Company Name";
            request.RequestedShipment.Shipper.Contact.PhoneNumber = _senderBO.Telephone.Trim(); //"0805522713";
            request.RequestedShipment.Shipper.Address = new Address();
            request.RequestedShipment.Shipper.Address.StreetLines = new string[1] { _senderBO.Address1.Trim() };
            request.RequestedShipment.Shipper.Address.City = _senderBO.City.Trim(); //"Austin";
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = _senderBO.State.Trim(); // "TX";
            request.RequestedShipment.Shipper.Address.PostalCode = _senderBO.Zip.Trim();// "73301";
            request.RequestedShipment.Shipper.Address.CountryCode = _senderBO.CountryCode.Trim(); //"US";
            //request.RequestedShipment.Shipper = new Party();
            //request.RequestedShipment.Shipper.Contact = new Contact();
            //request.RequestedShipment.Shipper.Contact.PersonName = "Sender Name";
            //request.RequestedShipment.Shipper.Contact.CompanyName = "Sender Company Name";
            //request.RequestedShipment.Shipper.Contact.PhoneNumber = "0805522713";
            //request.RequestedShipment.Shipper.Address = new Address();
            //request.RequestedShipment.Shipper.Address.StreetLines = new string[1] { "Address Line 1" };
            //request.RequestedShipment.Shipper.Address.City = "Austin";
            //request.RequestedShipment.Shipper.Address.StateOrProvinceCode = "TX";
            //request.RequestedShipment.Shipper.Address.PostalCode = "73301";
            //request.RequestedShipment.Shipper.Address.CountryCode = "US";
        }

        private  void SetRecipient(ProcessShipmentRequest request)
        {
            request.RequestedShipment.Recipient = new Party();
            request.RequestedShipment.Recipient.Contact = new Contact();
            request.RequestedShipment.Recipient.Contact.PersonName = _receiverBO.FirstName.Trim() + " " + _receiverBO.LastName.Trim();// "Recipient Name";
            request.RequestedShipment.Recipient.Contact.CompanyName = "Recipient Company Name";
            request.RequestedShipment.Recipient.Contact.PhoneNumber = _receiverBO.Telephone.Trim();// "9012637906";
            request.RequestedShipment.Recipient.Address = new Address();
            request.RequestedShipment.Recipient.Address.StreetLines = new string[1] { _receiverBO.Address1.Trim() };
            request.RequestedShipment.Recipient.Address.City = _receiverBO.City.Trim(); //"Windsor";
            request.RequestedShipment.Recipient.Address.StateOrProvinceCode = _receiverBO.State.Trim(); //"CT";
            request.RequestedShipment.Recipient.Address.PostalCode = _receiverBO.Zip.Trim();//"06006";
            request.RequestedShipment.Recipient.Address.CountryCode = "US";
            request.RequestedShipment.Recipient.Address.Residential = true;
            //request.RequestedShipment.Recipient = new Party();
            //request.RequestedShipment.Recipient.Contact = new Contact();
            //request.RequestedShipment.Recipient.Contact.PersonName = "Recipient Name";
            //request.RequestedShipment.Recipient.Contact.CompanyName = "Recipient Company Name";
            //request.RequestedShipment.Recipient.Contact.PhoneNumber = "9012637906";
            //request.RequestedShipment.Recipient.Address = new Address();
            //request.RequestedShipment.Recipient.Address.StreetLines = new string[1] { "Address Line 1" };
            //request.RequestedShipment.Recipient.Address.City = "Windsor";
            //request.RequestedShipment.Recipient.Address.StateOrProvinceCode = "CT";
            //request.RequestedShipment.Recipient.Address.PostalCode = "06006";
            //request.RequestedShipment.Recipient.Address.CountryCode = "US";
            //request.RequestedShipment.Recipient.Address.Residential = true;
        }

        private  void SetPayment(ProcessShipmentRequest request)
        {
            request.RequestedShipment.ShippingChargesPayment = new Payment();
            request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER;
            request.RequestedShipment.ShippingChargesPayment.Payor = new Payor();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty = new Party();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = _credentials.FedexACNo.ToString(); ; //"510088000"; // Replace "XXX" with client's account number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = getProperty("payoraccount");
            }
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Contact = new Contact();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address = new Address();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address.CountryCode = "US";
        }
        private  void SetRMA(ProcessShipmentRequest request)
        {
            request.RequestedShipment.SpecialServicesRequested = new ShipmentSpecialServicesRequested();
            request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[1];
            request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = new ShipmentSpecialServiceType();
            request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.RETURN_SHIPMENT;
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail = new ReturnShipmentDetail();
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.Rma = new Rma();
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.Rma.Reason = "RMA_TEST";
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.ReturnType = ReturnType.PRINT_RETURN_LABEL;
        }
        private  void SetLabelDetails(ProcessShipmentRequest request)
        {
            request.RequestedShipment.LabelSpecification = new LabelSpecification();
            request.RequestedShipment.LabelSpecification.ImageType = ShippingDocumentImageType.PDF; // Use this line for a PDF label
            request.RequestedShipment.LabelSpecification.ImageTypeSpecified = true;
            request.RequestedShipment.LabelSpecification.LabelFormatType = LabelFormatType.COMMON2D;
            request.RequestedShipment.LabelSpecification.LabelStockType = LabelStockType.PAPER_7X475;
            request.RequestedShipment.LabelSpecification.LabelStockTypeSpecified = true;
            request.RequestedShipment.LabelSpecification.LabelPrintingOrientation = LabelPrintingOrientationType.BOTTOM_EDGE_OF_TEXT_FIRST;
            request.RequestedShipment.LabelSpecification.LabelPrintingOrientationSpecified = true;
        }

        private  void SetPackageLineItems(bool isCodShipment, ProcessShipmentRequest request)
        {
            request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[1];
            request.RequestedShipment.RequestedPackageLineItems[0] = new RequestedPackageLineItem();
            //
            request.RequestedShipment.RequestedPackageLineItems[0].SequenceNumber = "1";
            // Package weight information
            request.RequestedShipment.RequestedPackageLineItems[0].Weight = new Weight();
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.Value = _weight ; 
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.Units = WeightUnits.LB;
            //
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions = new Dimensions();
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Length = "12";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Width = "13";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Height = "14";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Units = LinearUnits.IN;
            //
            if (isCodShipment)
            {
                SetCOD(request);
            }
        }

        private  void SetCOD(ProcessShipmentRequest request)
        {
            request.RequestedShipment.SpecialServicesRequested = new ShipmentSpecialServicesRequested();
            request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[1];
            request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.COD;
            //
            request.RequestedShipment.SpecialServicesRequested.CodDetail = new CodDetail();
            request.RequestedShipment.SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.GUARANTEED_FUNDS;
            request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount = new Money();
            request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Amount = 250.00M;
            request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Currency = "USD";
        }

        private  void ShowShipmentReply(bool isCodShipment, ProcessShipmentReply reply)
        {
           // Console.WriteLine("Shipment Reply details:");
           // Console.WriteLine("Package details\n");
            // Details for each package
            foreach (CompletedPackageDetail packageDetail in reply.CompletedShipmentDetail.CompletedPackageDetails)
            {
                ShowTrackingDetails(packageDetail.TrackingIds);
                if (packageDetail.PackageRating != null && packageDetail.PackageRating.PackageRateDetails != null)
                {
                    ShowPackageRateDetails(packageDetail.PackageRating.PackageRateDetails);
                }
                else
                {
                   // Console.WriteLine("No Rating information returned.\n");
                }
                ShowBarcodeDetails(packageDetail.OperationalDetail.Barcodes);
                ShowShipmentLabels(isCodShipment, reply.CompletedShipmentDetail, packageDetail);
            }
            ShowPackageRouteDetails(reply.CompletedShipmentDetail.OperationalDetail);
        }

        private  void ShowTrackingDetails(TrackingId[] TrackingIds)
        {
            // Tracking information for each package
            Console.WriteLine("Tracking details");
            if (TrackingIds != null)
            {
                for (int i = 0; i < TrackingIds.Length; i++)
                {
                    Console.WriteLine("Tracking # {0} Form ID {1}", TrackingIds[i].TrackingNumber, TrackingIds[i].FormId);
                }
            }
        }

        private  void ShowPackageRateDetails(PackageRateDetail[] PackageRateDetails)
        {
            foreach (PackageRateDetail ratedPackage in PackageRateDetails)
            {
                Console.WriteLine("\nRate details");
                if (ratedPackage.BillingWeight != null)
                    Console.WriteLine("Billing weight {0} {1}", ratedPackage.BillingWeight.Value, ratedPackage.BillingWeight.Units);
                if (ratedPackage.BaseCharge != null)
                    Console.WriteLine("Base charge {0} {1}", ratedPackage.BaseCharge.Amount, ratedPackage.BaseCharge.Currency);
                if (ratedPackage.TotalSurcharges != null)
                    Console.WriteLine("Total surcharge {0} {1}", ratedPackage.TotalSurcharges.Amount, ratedPackage.TotalSurcharges.Currency);
                if (ratedPackage.Surcharges != null)
                {
                    // Individual surcharge for each package
                    foreach (Surcharge surcharge in ratedPackage.Surcharges)
                        Console.WriteLine(" {0} surcharge {1} {2}", surcharge.SurchargeType, surcharge.Amount.Amount, surcharge.Amount.Currency);
                }
                if (ratedPackage.NetCharge != null)
                    Console.WriteLine("Net charge {0} {1}", ratedPackage.NetCharge.Amount, ratedPackage.NetCharge.Currency);
            }
        }

        private  void ShowBarcodeDetails(PackageBarcodes barcodes)
        {
            // Barcode information for each package
            Console.WriteLine("\nBarcode details");
            if (barcodes != null)
            {
                if (barcodes.StringBarcodes != null)
                {
                    for (int i = 0; i < barcodes.StringBarcodes.Length; i++)
                    {
                        Console.WriteLine("String barcode {0} Type {1}", barcodes.StringBarcodes[i].Value, barcodes.StringBarcodes[i].Type);
                    }
                }

                if (barcodes.BinaryBarcodes != null)
                {
                    for (int i = 0; i < barcodes.BinaryBarcodes.Length; i++)
                    {
                        Console.WriteLine("Binary barcode Type {0}", barcodes.BinaryBarcodes[i].Type);
                    }
                }
            }
        }

        private  void ShowPackageRouteDetails(ShipmentOperationalDetail routingDetail)
        {
            Console.WriteLine("\nRouting details");
            Console.WriteLine("URSA prefix {0} suffix {1}", routingDetail.UrsaPrefixCode, routingDetail.UrsaSuffixCode);
            Console.WriteLine("Service commitment {0} Airport ID {1}", routingDetail.DestinationLocationId, routingDetail.AirportId);

            if (routingDetail.DeliveryDaySpecified)
            {
                Console.WriteLine("Delivery day " + routingDetail.DeliveryDay);
            }
            if (routingDetail.DeliveryDateSpecified)
            {
                Console.WriteLine("Delivery date " + routingDetail.DeliveryDate.ToShortDateString());
            }
            if (routingDetail.TransitTimeSpecified)
            {
                Console.WriteLine("Transit time " + routingDetail.TransitTime);
            }
        }

        private  string ShowShipmentLabels(bool isCodShipment, CompletedShipmentDetail completedShipmentDetail, CompletedPackageDetail packageDetail)
        {
            if (null != packageDetail.Label.Parts[0].Image)
            {
                // Save outbound shipping label
                //outFile = Path.Combine(Server.MapPath("~\\Docs\\Results"), fileName + ".pdf");
                string LabelPath = _path;// "d:\\";// _path; //"d:\\";
                if (usePropertyFile())
                {
                    LabelPath = getProperty("labelpath");
                }
                string LabelFileName = LabelPath + packageDetail.TrackingIds[0].TrackingNumber + ".pdf";
                SaveLabel(LabelFileName, packageDetail.Label.Parts[0].Image);

                if (isCodShipment)
                {
                    // Save COD Return label
                    LabelFileName = LabelPath + completedShipmentDetail.AssociatedShipments[0].TrackingId.TrackingNumber + "CR" + ".pdf";
                    SaveLabel(LabelFileName, completedShipmentDetail.AssociatedShipments[0].Label.Parts[0].Image);
                }
                return LabelFileName;
            }
            return "";
        }

        private  void SaveLabel(string labelFileName, byte[] labelBuffer)
        {
            // Save label buffer to file
            try
            {
                FileStream LabelFile = new FileStream(labelFileName, FileMode.Create,FileAccess.Write);
                LabelFile.Write(labelBuffer, 0, labelBuffer.Length);
                LabelFile.Close();

                // Display label in Acrobat
               // DisplayLabel(labelFileName);
            }
            catch(Exception exp)
            {
                throw;
            }
        }

        private  void DisplayLabel(string labelFileName)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(labelFileName);
            info.UseShellExecute = true;
            info.Verb = "open";
            System.Diagnostics.Process.Start(info);
        }

        private  void ShowNotifications(ProcessShipmentReply reply)
        {
            Console.WriteLine("Notifications");
            for (int i = 0; i < reply.Notifications.Length; i++)
            {
                Notification notification = reply.Notifications[i];
                Console.WriteLine("Notification no. {0}", i);
                Console.WriteLine(" Severity: {0}", notification.Severity);
                Console.WriteLine(" Code: {0}", notification.Code);
                Console.WriteLine(" Message: {0}", notification.Message);
                Console.WriteLine(" Source: {0}", notification.Source);
            }
        }
        private  bool usePropertyFile() //Set to true for common properties to be set with getProperty function.
        {
            //  return getProperty("usefile").Equals("True");
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
            catch (Exception e)
            {
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
        }
        private  void LogXML(Object obj, Type type)
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(type);
            TextWriter writer = new StreamWriter("..\\access.log", true);
            writer.WriteLine("-------------" + DateTime.Now.ToString() + "-------------");
            serializer.Serialize(writer, obj);
            writer.WriteLine();
            writer.WriteLine("____________________________________________________");
            writer.WriteLine();
            writer.Close();
        }
    }
}
