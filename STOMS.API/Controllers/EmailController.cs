using STOMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace STOMS.API.Controllers
{
    public class EmailController : ApiController
    {
        // GET: api/Email
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        [Route("api/Emailconfiguration/")]
        public HttpResponseMessage Post([FromBody]EmailConfigBO emailconfiguration)
        {
            HttpResponseMessage responce = new HttpResponseMessage();
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient();
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(new MailAddress(emailconfiguration.ToAddress));
                    mailMessage.Subject = emailconfiguration.Subject;
                    mailMessage.Body = emailconfiguration.Body;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
                catch (Exception e)
                {

                }

            }
            return responce;
        }

        // PUT: api/Email/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Email/5
        public void Delete(int id)
        {
        }
    }
}
