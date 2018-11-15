using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using STOMS.BO;

namespace STOMS.UI.CommonCS
{
    public class SendEmail
    {
        public SendEmail(EmailConfigBO emailConBO)
        { 
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(emailConBO.ToAddress));
                mailMessage.Subject = emailConBO.Subject;
                mailMessage.Body = emailConBO.Body;
                //smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);              
            }
            catch (Exception e)
            {


            }
        }
    }
}
