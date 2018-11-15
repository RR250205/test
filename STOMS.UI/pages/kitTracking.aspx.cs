using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using STOMS.FEDEX.CLIENT;
using STOMS.FEDEX.CLIENT.ShipServiceWebReference;
using STOMS.FEDEX.CLIENT.TrackServiceWebReference;
using STOMS.FEDEX.CLIENT.AddressValidationServiceWebReference;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data;

namespace STOMS.UI.pages
{
    public partial class kitTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                Session["PgContentTitle"] = "Request Kit Tracking";
                popoOrderKit();
                popKitType();
                dvKitOrderManual.Visible = false;
                dvKitOrderShow.Visible = true;
                //if(ltrCurrentStatus.Text == "Kit Assigned")
                //{
                //    btnCheckServiceAvailability.Visible = true;
                //}
                lblRequest.Text = "";
                
            }
        }

        private void popKitType()
        {
            List<KitTypeConfigurationBO> kitTypeBO = new OrderKitDA().getKitTypeManual(Convert.ToInt32(Session["OrgId"]));
            ddlKitType.DataSource = kitTypeBO;
            ddlKitType.DataTextField = "KitName";
            ddlKitType.DataValueField = "KitId";
            ddlKitType.DataBind();
        }

        private void popoOrderKit()
        {
            List<CustomerBO> customerBO = new OrderKitDA().getviewOrderrequest(Convert.ToInt32(Session["OrgId"]));
            if (customerBO.Count > 0)
            {
                dvHasData.Visible = true;
                dvHasnotData.Visible = false;

                rptviewOrderRequest.DataSource = customerBO;
                rptviewOrderRequest.DataBind();
            }
            else
            {
                dvHasData.Visible = false;
                dvHasnotData.Visible = true;
                dvCourierInterface.Visible = false;
                lnkbtnAssignkits.Visible = false;
                btnEdit.Visible = false;
            }
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            lblRequest.Text = "";
            dvKitOrderManual.Visible = true;
            dvKitOrderShow.Visible = false;
            clearInputField();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearInputField();
        }

        private void clearInputField()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtRequesterType.Text = "";
            txtFacility.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtCountry.Text = "";
            txtZip.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            txtNoOfKits.Text = "";
            txtMessage.Text = "";           
            hRequestID.Value = "0";
            hCustomerID.Value = "0";
            //ddlCustomer.SelectedValue = "-1";
            //lblRequest.Text = "";
        }

        protected void btnCheckServiceAvailability_Click(object sender, EventArgs e)
        {
            try
            {
                List<CourierConfigurationBO> cbo = new EmailConfigurationDA().GetCourierTenant(Convert.ToInt32(Session["OrgID"]));
                if (cbo.Count > 0)
                {
                    rptCouriers.DataSource = cbo;
                    rptCouriers.DataBind();
                    dvAddAvlResult.Visible = true;
                    dvNoCourierError.Visible = false;
                }
                else
                {
                    dvNoCourierError.Visible = true;
                    dvAddAvlResult.Visible = true;
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnToCreateService_Click(object sender, EventArgs e)
        {

        }


        private string CheckWithFedex(CourierConfigBO configuration)
        {
            string Checkresult = "No";
            FedexAddressValidationServiceClient addressValidation = new FedexAddressValidationServiceClient(new AddressBO()
            {
                Address1 = ltrAddress.Text,
                Address2 = "",
                Address3 = "",
                City = ltrCity.Text,
                State = ltrState.Text,
                Zip = ltrZip.Text,
                Telephone = ltrTelephone.Text
            },
            configuration
            );
            /* AddressValidationRequest request = addressValidation.AddressValidateRequest();
             AddressValidationReply reply = addressValidation.AddressValidationReply(request);
             if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
             {
                 string text = "";
                 foreach (AddressValidationResult result in reply.AddressResults)
                 {
                     text += "<b>Address Id </b> : " + result.ClientReferenceId + "<br/> ";
                     if (result.ClassificationSpecified) { text += "<b>Classification: </b>" + result.Classification + "<br> "; }
                     if (result.StateSpecified) { text += "<b>State: </b> " + result.State + "<br> "; }
                     //text +="Proposed Address--"+"<br> ";
                     //Address address = result.EffectiveAddress;
                     //foreach (String street in address.StreetLines)
                     //{
                     //    text+="   Street: " + street + "<br> ";
                     //}
                     //text+="     City: " + address.City + "<br> ";
                     //text+="    ST/PR: " + address.StateOrProvinceCode + "<br> ";
                     //text+="   Postal: " + address.PostalCode + "<br> ";
                     //text+="  Country: " + address.CountryCode + "<br> ";
                     //text+= "<br> ";
                     //text+="Address Attributes:" + "<br> ";
                     foreach (AddressAttribute attribute in result.Attributes)
                     {
                         text += "<b>" + attribute.Name + ":</b> " + attribute.Value + "<br> ";
                     }
                 }
                 ltrAddressValidationResult.Text = text;
                 //btnToCreateService.Visible = true;
                 dvAddAvlResult.Visible = true;
             }*/
            return addressValidation.Result();
        }

        protected void rptCouriers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrCourierName = (Literal)e.Item.FindControl("ltrCourierName");
                ltrCourierName.Text = ((CourierConfigurationBO)e.Item.DataItem).CourierName.Trim();

                Literal ltrAddrsAvailability = (Literal)e.Item.FindControl("ltrAddrsAvailability");
                if (ltrCourierName.Text.Trim() == "FedEx")
                {
                    CourierConfigBO cb = new CourierConfigBO();
                    cb = new EmailConfigurationDA().getcourierdetail(Convert.ToInt32(Session["OrgID"]));
                    if (cb.FedexACNo != 0 && cb.FedexMeterNo != 0 && cb.FedexParentKey != "" && cb.FedexParentPassword != "" && cb.FedexUserKey != "" && cb.FedexUserPassword != "")
                    {
                        if (CheckWithFedex(cb) == "yes")
                        {
                            TextBox txtWeight = (TextBox)e.Item.FindControl("txtCourierWeight");
                            txtWeight.Text = cb.DefaultWeight.ToString();
                            ltrAddrsAvailability.Text = "Address is available!<br><b>Note: Courier will be sent through Default Sender Address</b>";
                            LinkButton btn = (LinkButton)e.Item.FindControl("lbtnProceed");
                            HtmlGenericControl dv = (HtmlGenericControl)e.Item.FindControl("dvProceed");
                            dv.Visible = true;
                            btn.CommandArgument = ((CourierConfigurationBO)e.Item.DataItem).CourierID.ToString();
                            //btn.Text = "Proceed";
                            //btn.Visible = true;
                        }
                        else
                        {
                            ltrAddrsAvailability.Text = "Address is not available!";
                        }
                    }
                    else
                    {
                        ltrAddrsAvailability.Text = "You have choosed Fedex Courier but Credentials are missing. please update";
                    }
                }
                else if (ltrCourierName.Text.Trim() == "USPS")
                {
                    ltrAddrsAvailability.Text = "You have choosed USPS Courier but Credentials are missing. please update";
                }
            }
        }

        protected void rptCouriers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CourierConfigBO cb = new CourierConfigBO();
            cb = new EmailConfigurationDA().getcourierdetail(Convert.ToInt32(Session["OrgID"]));

            if (cb.FedexACNo != 0 && cb.FedexMeterNo != 0 && cb.FedexParentKey != "" && cb.FedexParentPassword != "" && cb.FedexUserKey != "" && cb.FedexUserPassword != "")
            {
                TextBox txtWeight = (TextBox)e.Item.FindControl("txtCourierWeight");

                DropDownList ddlist = (DropDownList)e.Item.FindControl("ddlFedexParcelType");
                List<AddressBO> add = new OrderKitDA().getAddresses(Convert.ToInt32(Session["OrgID"]), "Active", true);
                if (add.Count > 0)
                {
                    List<KitOrderBO> kbo = new OrderKitDA().getRequestKitDetails(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hRequestID.Value), Convert.ToInt32(hCustomerID.Value), "Kit Assigned");

                    if (kbo.Count > 0)
                    {
                        foreach (var item_kbo in kbo)
                        {
                            FedexShipServiceClient shipServiceClient = new FedexShipServiceClient(cb, Convert.ToDecimal(txtWeight.Text.Trim()), ddlist.SelectedValue, add[0],
                        new AddressBO()
                        {
                            Address1 = ltrAddress.Text,
                            Address2 = "",
                            Address3 = "",
                            City = ltrCity.Text,
                            State = ltrState.Text,
                            Zip = ltrZip.Text,
                            Telephone = ltrTelephone.Text,
                            FirstName = ltrFirstName.Text.Trim(),
                            LastName = ltrLastName.Text.Trim(),

                        },
                        Server.MapPath("~\\Docs\\CourierLabels\\FedEx\\"));
                            OrderKitBO cbo = shipServiceClient.MakeShipment();
                            HtmlGenericControl sp = (HtmlGenericControl)e.Item.FindControl("spCourierError");
                            sp.InnerText = "";
                            if (cbo.Exception == null)
                            {
                                if (cbo.TrackNumber != null)
                                {
                                    string filepath = @"\Docs\CourierLabels\FedEx\" + cbo.TrackNumber + ".pdf";
                                    string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                                    // hyTrackNumber.Text = cbo.TrackNumber;
                                    //  hyTrackNumber.NavigateUrl = uri;
                                    //  hyTrackNumber.Target = "_blank";
                                    lblDelivery.Text = cbo.DeleveryOn;
                                    lblLabelGeneratedOn.Text = cbo.LabelGeneratedOn;
                                    //cbo.PickedUpOn = DateTime.Now.ToString();
                                    cbo.KitOrderID = Convert.ToInt32(hRequestID.Value);                                   

                                    new OrderKitDA().UpdateKitLabelDetails(cbo.TrackNumber, Convert.ToInt32(hRequestID.Value), Convert.ToInt32(item_kbo.KitID));
                                    new OrderKitDA().UpdateLabelStatusDetails(Convert.ToInt32(hRequestID.Value), "Label Generated");

                                    // new OrderKitDA().updateCourierInfoToOrderKit(cbo);
                                    dvCourierInterface.Visible = false;
                                    // dvCourierSuccess.Visible = true;
                                    ltrCurrentStatus.Text = "Label Generated";
                                }
                                Literal ltr = (Literal)e.Item.FindControl("ltrCourierNotifications");
                                ltr.Text = "";
                                foreach (var item in cbo.oNotifications)
                                {
                                    ltr.Text += item.cNotification + "<br/>";
                                }
                            }
                            else
                            {
                                sp.Visible = true;
                                sp.InnerText = cbo.Exception;
                            }
                        }
                    }
                    else
                    {
                        HtmlGenericControl sp = (HtmlGenericControl)e.Item.FindControl("spCourierError");
                        sp.Visible = true;
                        sp.InnerText = "Sorry! \n You can not proceed \n Kits Not assigned to this request";
                        // No kit has been assigned
                    }
                }
                else
                {
                    //there is 0 address configureed
                }
            }
        }

        protected void ddlOrderKitStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Visible = true;
            btnCheckServiceAvailability.Visible = true;
            //Check service availability and Edit button visibility
            if (ddlOrderKitStatus.SelectedValue == "Request Dispatched" || ddlOrderKitStatus.SelectedValue =="Label Generated")
            {
                btnCheckServiceAvailability.Visible = false;
                btnEdit.Visible = false;
            }            
            
            if(ddlOrderKitStatus.SelectedValue == "Label Generated")
            {
                dvCourierInterface.Visible = false;
                lnkbtnAssignkits.Visible = true; 
            }
            List<CustomerBO> orderKitBO = new OrderKitDA().getOrderKitDetails(Convert.ToInt32(Session["OrgId"]), ddlOrderKitStatus.SelectedValue);
            rptviewOrderRequest.DataSource = orderKitBO;
            rptviewOrderRequest.DataBind();
            if (orderKitBO.Count > 0)
            {
                dvKitOrderManual.Visible = false;
                dvKitOrderShow.Visible = true;
                dvHasData.Visible = true;
                dvHasnotData.Visible = false;
            }
            
            else
            {
                dvHasData.Visible = false;
                dvHasnotData.Visible = true;
                lnkbtnAssignkits.Visible = false;
                btnEdit.Visible = false;
                dvCourierInterface.Visible = false;
                dvCourierSuccess.Visible = false;
            }
        }

        protected void btnIsPicked_Click(object sender, EventArgs e)
        {
            OrderKitBO okitBO = new OrderKitBO();

            okitBO.PickedUpOn = Convert.ToString(txtIsPicked.Text);
            okitBO.KitOrderID = Convert.ToInt32(hRequestID.Value);
            new OrderKitDA().updateCourierInfoToOrderKit_Pickuped(okitBO);

            lblPickedUpOn.Text = txtIsPicked.Text;
            txtIsPicked.Text = "";
        }

        protected void btnCustomerNumber_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerBO objcustomer = new CustomerDA().GetCustomerNumber(txtCustomerNumber.Text);
                txtFirstName.Text = objcustomer.FirstName;
                txtLastName.Text = objcustomer.LastName;
                txtAddress.Text = objcustomer.Address1;
                txtCity.Text = objcustomer.City;
                txtCountry.Text = objcustomer.Country;
                txtFacility.Text = objcustomer.Facility;
                txtMessage.Text = objcustomer.Message;
                txtRequesterType.Text = objcustomer.RequesterType;
                txtState.Text = objcustomer.State;
                txtZip.Text = objcustomer.Zipcode;
                txtTelephone.Text = objcustomer.Phone;
                txtEmail.Text = objcustomer.Email;
                hCustomerID.Value = Convert.ToString(objcustomer.CustID);
            }
            catch
            {

            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedItem.Text == "New Customer")
            {
                dvCustNumber.Visible = false;               
                clearInputField();
                
            }
            if (ddlCustomer.SelectedItem.Text == "Existing Customer")
            {
                dvCustNumber.Visible = true;
                clearInputField();             
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Save Customer Details
            lblRequest.Text = "";
            CustomerBO customerBo = new CustomerBO
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                TenantID = Convert.ToInt32(Session["OrgID"]),
                Address1 = txtAddress.Text.Trim(),
                City = txtCity.Text.Trim(),
                State = txtState.Text.Trim(),
                Zipcode = txtZip.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Message = txtMessage.Text.Trim(),
                RequesterType = txtRequesterType.Text.Trim(),
                Facility = txtFacility.Text.Trim(),
                Country = txtCountry.Text.Trim(),
                Phone = txtTelephone.Text.Trim(),
                CustID = Convert.ToInt32(hCustomerID.Value),             

            };

            //if (btnSubmit.OnClientClick ="true") 
            //{
            //    lblSubmit.Text = "SuccessMessage";
            //}
            //string message = "Your details have been saved successfully.";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "')};";
            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            
            CustomerDA customerDA = new CustomerDA();
            int customerNo = customerDA.SaveCustomerNumber(customerBo);

            //Save and Edit request Details
            if (customerNo > 0 || hCustomerID.Value !="")
            {                
                if (customerNo>0)
                {
                    hCustomerID.Value = customerNo.ToString();
                    hRequestID.Value = "0";                    
                }

                lblRequest.Text = "Customer Request have been saved successfully.";

                OrderRequestBO ordreqBO = new OrderRequestDA().SaveOrderRequest(new OrderRequestBO
                {
                    RequestID =Convert.ToInt32(hRequestID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    NoOfKits = Convert.ToInt32(txtNoOfKits.Text.Trim()),
                    KitType = ddlKitType.SelectedItem.Text,
                    CustomerNumber = hCustomerID.Value,
                });
                if (ordreqBO.RequestID > 0)
                {
                }
                try
                {
                    OrderKitBO okBO = new OrderKitBO();
                    TenantDetailsBO tenantDetailBO = new TenantDetailsBO();
                    tenantDetailBO = new TenantDetailsDA().getTenantDetails(Convert.ToInt32(Session["OrgID"]));

                    string ordNumber = ordreqBO.RequestNumber;
                    List<EmailEnablementImplementationBO> emailBO = new EmailConfigurationDA().emailEnableImplementation(Convert.ToInt32(Session["OrgID"]), 1);
                    if (emailBO[0].emailEnablementBO.isToTenant == true)
                    {
                        emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@TenantName", Convert.ToString(Session["OrgName"]));
                        emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@End-userName", (txtFirstName.Text + " " + txtLastName.Text));
                        emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                        emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@RequestID", ordNumber);

                        EmailConfigBO ema = new EmailConfigBO();
                        ema.Body = emailBO[0].emailEnablementTypeBO.TenantTemplate;
                        ema.Subject = "Order Confirmation";
                        ema.ToAddress = emailBO[0].emailEnablementBO.ToTenantEmails;
                        new STOMS.UI.CommonCS.SendEmail(ema);
                    }

                    if (emailBO[0].emailEnablementBO.isToEndUser == true)
                    {
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-userName", (txtFirstName.Text + " " + txtLastName.Text));
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@RequestID", ordNumber);
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@DeliveryDuration", "5-10 days");
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Telephone", tenantDetailBO.ContactUsTelephone);
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@ContactusEmailID", tenantDetailBO.ContactUsEmailID);

                        EmailConfigBO ema = new EmailConfigBO();
                        ema.Body = emailBO[0].emailEnablementTypeBO.EndUserTemplate;
                        ema.Subject = "Order Confirmation";
                        ema.ToAddress = txtEmail.Text;
                        new STOMS.UI.CommonCS.SendEmail(ema);
                    }
                }
                catch (Exception exp)
                {
                    btnSubmit.Text = "TargetSite :" + exp.TargetSite + "Message :" + exp.Message;
                }
            }
            popoOrderKit();
            clearInputField();           
        }

        protected void btnOrderrequest_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnAssignkits_Click(object sender, EventArgs e)
        {
            if (lnkbtnAssignkits.Text == "Assign Kits")
            {
                List<KitOrderBO> kitorderBO = new OrderKitDA().PopAssignedKits(Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(ltrNoOfKits.Text.Trim()), "InStock");
                if (kitorderBO.Count > 0)
                {
                    btnassign.Visible = true;
                    btnclose.Visible = true;
                    gdvwassignkit.DataSource = kitorderBO;
                    gdvwassignkit.DataBind();

                    gdvwassignkit.Attributes.Add("class", "table");
                    gdvwassignkit.Attributes.Add("Style", "border-top-color: 1px solid 0e0d0d");
                    gdvwassignkit.Visible = true;
                    dvNoKits.Visible = false;
                    gvAssignedView.Visible = false;
                }
                else
                {
                    gdvwassignkit.Visible = false;
                    dvNoKits.Visible = true;
                    gvAssignedView.Visible = false;
                }

                myModel.Attributes.Remove("class");
                myModel.Attributes.Add("class", "modal fade in");
                myModel.Attributes.Add("Style", "display:block");
            }
            else if (lnkbtnAssignkits.Text == "View Assigned Kits")
            {
                List<KitOrderBO> kbo = new OrderKitDA().getRequestKitDetails(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hRequestID.Value), Convert.ToInt32(hCustomerID.Value), ltrCurrentStatus.Text);

                gvAssignedView.DataSource = kbo;
                gvAssignedView.DataBind();
                gvAssignedView.Visible = true;
                gdvwassignkit.Visible = false;
                dvNoKits.Visible = false;
                gvAssignedView.Attributes.Add("class", "table");
                gvAssignedView.Attributes.Add("Style", "border-top-color: 1px solid 0e0d0d");
                myModel.Attributes.Remove("class");
                myModel.Attributes.Add("class", "modal fade in");
                myModel.Attributes.Add("Style", "display:block");
                btnassign.Visible = false;
                btnclose.Visible = true;
            }
            else
            {
                dvNoKits.Visible = true;
                btnassign.Visible = false;
            }
        }

        protected void btnTopClose_Click(object sender, EventArgs e)
        {
            closeFunction();

        }

        private void closeFunction()
        {
            //myModel.Visible = false;
            txtNoOfKits.Text = "";
            myModel.Attributes.Add("Style", "display:none");
        }
        
        protected void btnbottomclose_Click(object sender, EventArgs e)
        {
            closeFunction();
        }

        protected void btnassign_Click(object sender, EventArgs e)
        {
            if (lnkbtnAssignkits.Text == "Assign Kits")
            {
                DataTable dt = new DataTable();
                List<KitOrderBO> kbo = new List<KitOrderBO>();

                dt.Columns.AddRange(new DataColumn[4]
                {
                        new DataColumn("KitID", typeof(int)),
                        new DataColumn("CustomerID",typeof(int)),
                        new DataColumn("TenantID", typeof(int)),
                        new DataColumn("RequestID", typeof(int))
                });

                //dtMasterKit.Columns.AddRange(new DataColumn[2]
                //{
                //     new DataColumn("KitID", typeof(int)),
                //     new DataColumn("Status", typeof(string)),
                //});
                foreach (GridViewRow rw in gdvwassignkit.Rows)
                {
                    kbo.Add(new KitOrderBO
                    {
                        KitID = int.Parse(rw.Cells[0].Text.Trim()),
                        Status = "Kit Assigned",
                    });
                    int KitId = int.Parse(rw.Cells[0].Text.Trim());
                    int CustomerID = Convert.ToInt32(hCustomerID.Value); //rw.Cells[1].Text.Trim();
                    int TenantID = Convert.ToInt32(Session["OrgID"]);
                    int RequestID = Convert.ToInt32(hRequestID.Value);
                    dt.Rows.Add(KitId, CustomerID, TenantID, RequestID);
                }

                if (dt.Rows.Count > 0)
                {
                    KitOrderBO kitOrdBO = new KitOrderBO();
                    new OrderKitDA().SaveAssignKits(dt);
                    new OrderKitDA().updateMasterKit(kbo);
                    myModel.Attributes.Add("Style", "display:none");
                    new OrderKitDA().updateKitRequestStatus(Convert.ToInt32(hRequestID.Value), "Kit Assigned");
                    ltrCurrentStatus.Text = "Kit Assigned";
                    //lnkbtnAssignkits.Visible = false;
                    lnkbtnAssignkits.Text = "View Assigned Kits";
                    btnCheckServiceAvailability.Visible = true;
                    btnEdit.Visible = false;
                }
            }
            else if (lnkbtnAssignkits.Text == "View Assigned Kits")
            {
                // Call for get Kit details based on request ID from view
                //view ned to be generate
            }
        }

        protected void rptviewOrderRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            dvKitOrderShow.Visible = true;
            dvKitOrderManual.Visible = false;

            getRequestDetails(Convert.ToInt32(e.CommandArgument));

            dvAddAvlResult.Visible = false;
        }

        private void getRequestDetails(int RequestID)
        {
            List<CustomerBO> customerBO = new OrderKitDA().getRequestDetails(RequestID);

            hRequestID.Value = Convert.ToString(customerBO[0].RequestID);
            hCustomerID.Value = Convert.ToString(customerBO[0].CustID);
            ltrFirstName.Text = customerBO[0].FirstName;
            ltrLastName.Text = customerBO[0].LastName;
            ltrRequesterType.Text = customerBO[0].RequesterType;
            ltrFacility.Text = customerBO[0].Facility;
            ltrAddress.Text = customerBO[0].Address1;
            ltrCity.Text = customerBO[0].City;
            ltrState.Text = customerBO[0].State;
            ltrCountry.Text = customerBO[0].Country;
            ltrZip.Text = customerBO[0].Zipcode;
            ltrKitType.Text = customerBO[0].KitType;// ddlKitType.SelectedItem.Text;
            ltrTelephone.Text = customerBO[0].Phone;
            ltrEmail.Text = customerBO[0].Email;
            ltrNoOfKits.Text = Convert.ToString(customerBO[0].NoOfKits);
            ltrMessage.Text = customerBO[0].Message;
            ltrDate.Text = Convert.ToString(customerBO[0].RequestDate);
            ltrOrderNumber.Text = Convert.ToString(customerBO[0].RequestNumber);
            ltrCurrentStatus.Text = customerBO[0].Status;
            lblCustNo.Text = customerBO[0].CustNumber;

           
            if (customerBO[0].Status == "Kit Requested")
            {
                lnkbtnAssignkits.Text = "Assign Kits";
                btnCheckServiceAvailability.Visible = true;                
                lnkbtnAssignkits.Visible = true;
                dvCourierInterface.Visible = true;
                dvAddAvlResult.Visible = false;
            }
            else
            {
                lnkbtnAssignkits.Text = "View Assigned Kits";
                lnkbtnAssignkits.Visible = true;
            }

            if (customerBO[0].Status == "Kit Assigned")
            {
                btnCheckServiceAvailability.Visible = true;
                 dvCourierInterface.Visible = true;
                dvAddAvlResult.Visible = false;
            }
            if (customerBO[0].Status == "Label Generated")
            {
                dvCourierInterface.Visible = false;
               
            }
            }

        protected void rptviewOrderRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    getRequestDetails(((CustomerBO)e.Item.DataItem).RequestID);
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblRequest.Text = "";
            dvKitOrderManual.Visible = true;
            dvKitOrderShow.Visible = false;

            txtFirstName.Text = ltrFirstName.Text;
            txtLastName.Text = ltrLastName.Text;
            txtRequesterType.Text = ltrRequesterType.Text;
            txtFacility.Text = ltrFacility.Text;
            txtAddress.Text = ltrAddress.Text;
            txtCity.Text = ltrCity.Text;
            txtState.Text = ltrState.Text;
            txtCountry.Text = ltrCountry.Text;
            txtZip.Text = ltrZip.Text;
            ddlKitType.SelectedItem.Text = ltrKitType.Text;
            txtTelephone.Text = ltrTelephone.Text;
            txtEmail.Text = ltrEmail.Text;
            txtNoOfKits.Text = ltrNoOfKits.Text;
            txtMessage.Text = ltrMessage.Text;
        }



        /*protected void gvAssignedView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Find the TextBox control.
            //    TextBox txtName = (e.Row.FindControl("txtName") as TextBox);
            //    GridLines LabelNumber = (e.Row.FindControl("LabelNumber") as GridLines);
                string LabelNo = (e.Row.DataItem as DataRowView)["Label Number"].ToString();
                string filepath = @"\Docs\CourierLabels\FeDex" + LabelNo + ".pdf";
                string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;

            
        }*/
    }
}


