using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STOMS.FEDEX.CLIENT.TrackServiceWebReference;
using System.Web.Services.Protocols;

namespace STOMS.FEDEX.CLIENT
{
    public class TrackServiceClient
    {
        private string _trackNumber = "";

        public TrackServiceClient(string TrackNumber)
        {
            _trackNumber = TrackNumber;
        }
        public TrackRequest Request()
        {
            return CreateTrackRequest();
        }

        public TrackReply Replay(TrackRequest request)
        {
            return new TrackService().track(request);
        }

        /*void Main()
        {
            TrackRequest request = CreateTrackRequest();
            //
            TrackService service = new TrackService();
            if (usePropertyFile())
            {
                service.Url = getProperty("endpoint");
            }
            //
            try
            {
                // Call the Track web service passing in a TrackRequest and returning a TrackReply
                TrackReply reply = service.track(request);
                if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    ShowTrackReply(reply);
                }
                ShowNotifications(reply);
            }
            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }*/

        private  TrackRequest CreateTrackRequest()
        {
            // Build the TrackRequest
            TrackRequest request = new TrackRequest();
            //
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = "IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = "uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX" with the Password
            request.WebAuthenticationDetail.ParentCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.ParentCredential.Key = "IoXBEjJlfQOOLlrx"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.ParentCredential.Password = "uwcmw4WV7TpoeZ0hOvK1jy9fF"; // Replace "XXX"
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.WebAuthenticationDetail.UserCredential.Key = getProperty("key");
                request.WebAuthenticationDetail.UserCredential.Password = getProperty("password");
                request.WebAuthenticationDetail.ParentCredential.Key = getProperty("parentkey");
                request.WebAuthenticationDetail.ParentCredential.Password = getProperty("parentpassword");
            }
            //
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = "510088000"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = "118817920"; // Replace "XXX" with the client's meter number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.ClientDetail.AccountNumber = getProperty("accountnumber");
                request.ClientDetail.MeterNumber = getProperty("meternumber");
            }
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Track Request using VC#***";  //This is a reference field for the customer.  Any value can be used and will be provided in the response.
            //
            request.Version = new VersionId();
            //
            // Tracking information
            request.SelectionDetails = new TrackSelectionDetail[1] { new TrackSelectionDetail() };
            request.SelectionDetails[0].PackageIdentifier = new TrackPackageIdentifier();
            request.SelectionDetails[0].PackageIdentifier.Value = _trackNumber;// Replace "XXX" with tracking number or door tag
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.SelectionDetails[0].PackageIdentifier.Value = getProperty("trackingnumber");
            }
            request.SelectionDetails[0].PackageIdentifier.Type = TrackIdentifierType.TRACKING_NUMBER_OR_DOORTAG;
            //
            // Date range is optional.
            // If omitted, set to false
            request.SelectionDetails[0].ShipDateRangeBegin = DateTime.Parse("06/18/2012"); //MM/DD/YYYY
            request.SelectionDetails[0].ShipDateRangeEnd = request.SelectionDetails[0].ShipDateRangeBegin.AddDays(0);
            request.SelectionDetails[0].ShipDateRangeBeginSpecified = false;
            request.SelectionDetails[0].ShipDateRangeEndSpecified = false;
            //
            // Include detailed scans is optional.
            // If omitted, set to false
            request.ProcessingOptions = new TrackRequestProcessingOptionType[1];
            request.ProcessingOptions[0] = TrackRequestProcessingOptionType.INCLUDE_DETAILED_SCANS;
            return request;
        }

        public  string ShowTrackReply(TrackReply reply)
        {
            string rtn = "";
            // Track details for each package
            foreach (CompletedTrackDetail completedTrackDetail in reply.CompletedTrackDetails)
            {
                foreach (TrackDetail trackDetail in completedTrackDetail.TrackDetails)
                {
                    //rtn+=("<b>Tracking details:</b><br/>");
                    //rtn+=("**************************************<br>");
                    ShowNotification(trackDetail.Notification);
                    rtn+="<b>Tracking number:</b>"+ trackDetail.TrackingNumber+"<br>";
                    rtn+="<b>Tracking number unique identifier:</b>"+ trackDetail.TrackingNumberUniqueIdentifier+"<br>";
                    rtn+="<b>Track Status:</b>"+trackDetail.StatusDetail.Description+"<br>";
                    //rtn+="<b>Carrier code:</b>"+trackDetail.CarrierCode+"<br>";

                    //if (trackDetail.OtherIdentifiers != null)
                    //{
                    //    foreach (TrackOtherIdentifierDetail identifier in trackDetail.OtherIdentifiers)
                    //    {
                    //        Console.WriteLine("Other Identifier: {0} {1}", identifier.PackageIdentifier.Type, identifier.PackageIdentifier.Value);
                    //    }
                    //}
                    if (trackDetail.Service != null)
                    {
                        rtn+="<b>ServiceInfo: </b>"+ trackDetail.Service.Description+"<br>";
                    }
                    if (trackDetail.PackageWeight != null)
                    {
                        rtn+="<b>Package weight:</b> "+trackDetail.PackageWeight.Value+", "+ trackDetail.PackageWeight.Units+"<br>";
                    }
                    if (trackDetail.ShipmentWeight != null)
                    {
                        rtn+="<b>Shipment weight:</b>"+ trackDetail.ShipmentWeight.Value+", "+ trackDetail.ShipmentWeight.Units+"<br>";
                    }
                    if (trackDetail.Packaging != null)
                    {
                        rtn+="<b>Packaging: </b>"+ trackDetail.Packaging+"<br>";
                    }
                    rtn+="<b>Package Sequence Number: </b>"+ trackDetail.PackageSequenceNumber+"<br>";
                    rtn+="<b>Package Count:</b>"+ trackDetail.PackageCount+"<br>";
                    if (trackDetail.DatesOrTimes != null)
                    {
                        foreach (TrackingDateOrTimestamp timestamp in trackDetail.DatesOrTimes)
                        {
                            switch (timestamp.Type.ToString())
                            {
                                case "ACTUAL_DELIVERY":
                                    rtn += "<b>Actual Delevery</b>" + ", " + timestamp.DateOrTimestamp + "<br>";
                                    break;
                                case "ACTUAL_PICKUP":
                                    rtn += "<b>Actual Pickup</b>" + ": " + timestamp.DateOrTimestamp + "<br>";
                                    break;
                                case "APPOINTMENT_DELIVERY":
                                    rtn += "<b>Apoinment Delevery</b>" + ": " + timestamp.DateOrTimestamp + "<br>";
                                    break;
                                case "ESTIMATED_DELIVERY":
                                    rtn += "<b>Estimated Delevery</b>" + ": " + timestamp.DateOrTimestamp + "<br>";
                                    break;
                                case "ESTIMATED_PICKUP":
                                    rtn += "<b>Estimated Pickup</b>" + ": " + timestamp.DateOrTimestamp + "<br>";
                                    break;
                            }
                           

                        }
                    }
                    if (trackDetail.DestinationAddress != null)
                    {
                        rtn+="<b>Destination:</b> "+ trackDetail.DestinationAddress.City+", "+ trackDetail.DestinationAddress.StateOrProvinceCode+"<br>";
                    }
                    if (trackDetail.AvailableImages != null)
                    {
                        foreach (AvailableImagesDetail ImageDetail in trackDetail.AvailableImages)
                        {
                            rtn+="<b>Image availability:</b> "+ ImageDetail.Type+"<br>";
                        }
                    }
                    //if (trackDetail.NotificationEventsAvailable != null)
                    //{
                    //    foreach (NotificationEventType notificationEventType in trackDetail.NotificationEventsAvailable)
                    //    {
                    //        rtn+="<b>NotificationEvent type : </b>"+ notificationEventType+"<br>";
                    //    }
                    //}

                    //Events
                    //Console.WriteLine();
                    if (trackDetail.Events != null)
                    {
                        rtn += "<b><u>Track Events</u></b><br>";
                        foreach (TrackEvent trackevent in trackDetail.Events)
                        {
                            if (trackevent.TimestampSpecified)
                            {
                                rtn += "<b>Timestamp:</b>"+ trackevent.Timestamp+":   ";
                            }
                            rtn += "<b>Event: </b>"+ trackevent.EventDescription+"<br>";
                            //Console.WriteLine("***");
                        }
                        //Console.WriteLine();
                    }
                    //Console.WriteLine("**************************************");
                }
            }

            return rtn;

        }
        private void ShowNotification(Notification notification)
        {
            Console.WriteLine(" Severity: {0}", notification.Severity);
            Console.WriteLine(" Code: {0}", notification.Code);
            Console.WriteLine(" Message: {0}", notification.Message);
            Console.WriteLine(" Source: {0}", notification.Source);
        }
        private void ShowNotifications(TrackReply reply)
        {
            Console.WriteLine("Notifications");
            for (int i = 0; i < reply.Notifications.Length; i++)
            {
                Notification notification = reply.Notifications[i];
                Console.WriteLine("Notification no. {0}", i);
                ShowNotification(notification);
            }
        }
        private bool usePropertyFile() //Set to true for common properties to be set with getProperty function.
        {
            //return getProperty("usefile").Equals("True");
            return false;
        }
        private String getProperty(String propertyname) //Sets common properties for testing purposes.
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

    }
}
