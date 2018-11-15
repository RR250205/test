using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class PrintInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string invno = (string)RouteData.Values["invno"];

                if (Request["invno"] != null)
                    invno = Convert.ToString(Request["invno"]);
                if (invno != null)
                {
                    OrderInvDA oInv = new OrderInvDA();
                    List<InvoiceBO> oInvBO = new List<InvoiceBO>();
                    oInvBO = oInv.GetInvoiceDetail(invno, "InvNumber");
                    if (oInvBO.Count > 0)
                    {
                        string fileContent = File.ReadAllText(Request.PhysicalApplicationPath + "\\templates\\InvoiceTemplate.html");
                        fileContent = fileContent.Replace("@@InvoicedTo", oInvBO[0].CustomerName);
                        fileContent = fileContent.Replace("@@InvoiceNo", oInvBO[0].InvNumber);
                        fileContent = fileContent.Replace("@@InvoiceDate", oInvBO[0].InvDate);

                        if (oInvBO[0].InvoiceDetail.Count > 0)
                        {
                            fileContent = fileContent.Replace("@@SNO", Convert.ToString(oInvBO[0].InvoiceDetail[0].ItemOrder));
                            fileContent = fileContent.Replace("@@Description", oInvBO[0].InvoiceDetail[0].ItemDesc);
                            fileContent = fileContent.Replace("@@Rate", Convert.ToString(oInvBO[0].InvoiceDetail[0].UnitCost));
                            fileContent = fileContent.Replace("@@Amount", Convert.ToString(oInvBO[0].InvoiceDetail[0].TotalCost));
                            fileContent = fileContent.Replace("@@InvAmount", Convert.ToString(oInvBO[0].InvoiceDetail[0].TotalCost));
                        }
                        ltrInv.Text = fileContent;
                    }
                }
            }
        }
    }
}