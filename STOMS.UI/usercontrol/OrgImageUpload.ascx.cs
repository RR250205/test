using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.IO;


namespace STOMS.UI.usercontrol
{
    public partial class OrgImageUpload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void fileUploadHandler_ValueChanged(object sender, EventArgs e)
        {

        }
        
        public int fileUpload()
        {
           
            if (flSampleHardCopy.HasFile)
            {
                string fileName = Path.GetFileName(flSampleHardCopy.FileName);
                DocumentBO documentBO = new DocumentBO()
                {
                    OrgDocName = fileName.Substring(0, fileName.LastIndexOf('.')),
                    CreatedBy = Convert.ToInt32(Session["UserID"]),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    DocType = fileName.Substring(fileName.LastIndexOf('.') + 1)
                };
                DocumentBO newDocumentBO = new DocumentDA().SaveReqFormCopy(documentBO);
                hDocId.Value = Convert.ToString(newDocumentBO.DocID);
                string DocName = newDocumentBO.DocNumber;
                flSampleHardCopy.SaveAs(Server.MapPath("~/Docs/Profile/") + DocName + "." + fileName.Substring(fileName.LastIndexOf('.') + 1));
                return newDocumentBO.DocID;

                  }

            return 0;
        }


        //public int logoview()
        //{
        //    Image image = new Image();
        //    image.Height = 150;
        //    image.Width = 150;
              
           
        //    DocumentBO logodocumentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(hDocId.Value));
        //    hDocNumber.Value = logodocumentBO.DocNumber;
        //    string filesource = @"\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;
        //    string logo = @"~\Docs\Profile\" + logodocumentBO.DocNumber + "." + logodocumentBO.DocType;

        //    //string Path = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
        //    hDocNumber.Value = logo;
        //    return 0;
        //}
    }
}