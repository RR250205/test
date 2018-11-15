using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STOMS.API.Models
{
    public class EmailConfigBO
    {
        //public string UserName { get; set; }
        //public string UserEmailID { get; set; }
        //public string Password { get; set; }
        //public string SMTPServer { get; set; }
        //public string SMTPPortNumber { get; set; }
        //public string UserBackupEmailID { get; set; }
        //public string Enablessl { get; set; }
        public string GreetingText { get; set; }
        public string Signature { get; set; }
        //public static object AuthenticateToken { get; internal set; }
        public string ToAddress { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

    }
}