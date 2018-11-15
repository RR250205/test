using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using STOMS.API.Models;
using STOMS.API.DataAccess;
using System.Net.Mail;

namespace STOMS.API.Controllers
{
    public class KitorderController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        /*public void Post( [FromBody]string value)
        {
        }*/

        [Route("api/Kitorder/")]
        public HttpResponseMessage Post([FromBody]KitOrder kitOrder /*string LastName, string PersionType, string OrgName, string Address, string City, string State, string Country, string Zip, string Telephone, string Email, string Message, string AuthenticateToken*/)
        {
            ApiDA apiDA = new ApiDA();
            HttpResponseMessage responce = new HttpResponseMessage();
           
           // bool isEntity=apiDA.CheckEntity(kitOrder.AuthenticateToken);
           int TenantID= apiDA.CheckEntity(kitOrder.AuthenticateToken);
            if (TenantID>0)
            {
                CustomerBO cutomerBo = new CustomerBO
                {
                    FirstName = kitOrder.FirstName.Trim(),
                    LastName = kitOrder.LastName.Trim(),
                    TenantID = TenantID,
                    Address1 = kitOrder.Address.Trim(),
                    City = kitOrder.City.Trim(),
                    State = kitOrder.State.Trim(),
                    Zipcode = kitOrder.Zip.Trim(),
                    Email = kitOrder.Email.Trim(),
                    Message = kitOrder.Message.Trim(),
                    RequesterType = kitOrder.RequesterType.Trim(),
                    Facility = kitOrder.OrgName.Trim(),
                    Country = kitOrder.Country.Trim(),
                    Phone = kitOrder.Telephone.Trim(),
                    CustID = 0,
                    
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


                ApiDA customerDA = new ApiDA();
                int customerNo = customerDA.SaveCustomerNumber(cutomerBo);

                //Save and Edit request Details
                if (customerNo > 0 )
                {
                    
                    OrderRequestBO ordreqBO = new ApiDA().SaveOrderRequest(new OrderRequestBO
                    {
                        RequestID = Convert.ToInt32(0),
                        TenantID = TenantID,
                        NoOfKits = Convert.ToInt32(kitOrder.NoOfKits),
                        KitType = "Frat",
                        CustomerNumber = customerNo.ToString(),
                    });
                   
                    try
                    {
                        OrderKitBO okBO = new OrderKitBO();
                        TenantDetailsBO tenantDetailBO = new TenantDetailsBO();
                        tenantDetailBO = new ApiDA().getTenantDetails(TenantID);

                        string ordNumber = ordreqBO.RequestNumber;
                        List<EmailEnablementImplementationBO> emailBO = new ApiDA().emailEnableImplementation(TenantID, 1);
                        if (emailBO[0].emailEnablementBO.isToTenant == true)
                        {
                            emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@TenantName", tenantDetailBO.TenantName);
                            emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@End-userName", (kitOrder.FirstName.Trim() + " " + kitOrder.LastName.Trim()));
                            emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                            emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@RequestID", ordNumber);

                            EmailConfigBO ema = new EmailConfigBO();
                            ema.Body = emailBO[0].emailEnablementTypeBO.TenantTemplate;
                            ema.Subject = "Order Confirmation";
                            ema.ToAddress = emailBO[0].emailEnablementBO.ToTenantEmails;
                            SendEmail(ema);
                        }

                        if (emailBO[0].emailEnablementBO.isToEndUser == true)
                        {
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-userName", (kitOrder.FirstName.Trim() + " " + kitOrder.LastName.Trim()));
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@RequestID", ordNumber);
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@DeliveryDuration", "5-10 days");
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Telephone", tenantDetailBO.ContactUsTelephone);
                            emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@ContactusEmailID", tenantDetailBO.ContactUsEmailID);

                            EmailConfigBO ema = new EmailConfigBO();
                            ema.Body = emailBO[0].emailEnablementTypeBO.EndUserTemplate;
                            ema.Subject = "Order Confirmation";
                            ema.ToAddress = kitOrder.Email.Trim();
                            SendEmail(ema);
                        }
                        responce = Request.CreateResponse(HttpStatusCode.OK, "");
                    }
                    catch (Exception exp)
                    {
                       // btnSubmit.Text = "TargetSite :" + exp.TargetSite + "Message :" + exp.Message;
                    }
                }
                else
                {
                    HttpError error = new HttpError("Oops!");
                    responce = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
                }









                //int id= apiDA.SaveOrderKitData(kitOrder);
                //if (id > 0)
                //{
                //    responce= Request.CreateResponse(HttpStatusCode.OK,"");
                //}
                //else
                //{
                   
                //}
            }
            else
            {
                HttpError error = new HttpError("User is not Authenticated");
                responce= Request.CreateErrorResponse(HttpStatusCode.Unauthorized, error);
            }
            return responce;
            
        }
        // PUT api/<controller>/5                                   
        public void Put(int id, [FromBody]string value)             
        {                                                          
        }                                                          
                                                                   
        // DELETE api/<controller>/5                               
        public void Delete(int id)                                 
        {                                                           
        }
        public void SendEmail(EmailConfigBO emailConBO)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage();

                mailMessage.To.Add(new MailAddress(emailConBO.ToAddress));
                mailMessage.Subject = emailConBO.Subject;
                mailMessage.Body = emailConBO.Body;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {

            }
        }
    }
}