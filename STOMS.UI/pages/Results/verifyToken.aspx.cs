using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using STOMS.Common;
using Winnovative.WnvHtmlConvert;
using System.IO;
using KoSoft.Entitlement;


namespace STOMS.UI.pages.Results
{
    public partial class verifyToken : System.Web.UI.Page
    {
        private string _meg = "";
        string tokenID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null)
            {
                 tokenID = Convert.ToString(Request.QueryString);

                GetTokenId(tokenID);

            }
  }

       public void GetTokenId(string tokenID)
        {
            List<DocumentBO> objemailtoken = (new DocumentDA()).GetTokenNum(tokenID);
           {
                
                   
                        //DateTime newDate = objemailtoken[0].ValidUpto.AddDays(2);

                       if (objemailtoken[0].ValidUpto >= DateTime.Now)
                        {

                            Response.ContentType = "application/octet-stream";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + objemailtoken[0].DocNumber + ".pdf");
                            Response.TransmitFile(Server.MapPath("/Docs/Results/" + objemailtoken[0].DocNumber + ".pdf"));
                            Response.End();
                            // ViewMode(true);


                        }
                        else
                        {
                    //Response.Redirect("pages/VerifyToken"); 
                    lblDateExper.Text = "This Link has been Expired.Please Request to Lab Administrater for another valid link";
                        }

                    }
                    

            }

        }

    }
   
