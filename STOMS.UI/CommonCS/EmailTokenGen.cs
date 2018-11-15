using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.CommonCS
{
    public class EmailTokenGen
    {
        public EmailTokenGen(DocumentBO objdoc)
        {
            try
            {
                DocumentBO EmailToken = new DocumentBO();


                EmailToken.DocID = objdoc.DocID;
                EmailToken.TokenID = Guid.NewGuid().ToString();
                EmailToken.DocNumber = objdoc.DocNumber;
                EmailToken.TenantID = objdoc.TenantID;
                DocumentDA objDA = new DocumentDA();
                string token = objDA.UpdateDocNum(objdoc);
               
              

            }
            catch (Exception e)
            {


            }
        }


    }
}