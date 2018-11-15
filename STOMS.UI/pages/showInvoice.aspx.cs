using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace STOMS.UI.pages
{
    public partial class showInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                OrderInvDA oInv = new OrderInvDA();
                List<InvoiceBO> oInvBO = new List<InvoiceBO>();
                if (Session["InvID"] != null)
                    oInvBO = oInv.GetInvoiceDetail(Convert.ToString(Session["InvID"]), "Invoice");
                else if (Session["OrderID"] != null)
                {
                    oInvBO = oInv.GetInvoiceDetail(Convert.ToString(Session["OrderID"]), "Order");
                    if (oInvBO.Count == 0)
                    {
                        oInv.SaveInvoice(Convert.ToInt32(Session["OrderID"]), DateTime.Now.ToShortDateString());
                        oInvBO = oInv.GetInvoiceDetail(Convert.ToString(Session["OrderID"]), "Order");
                    }
                }
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

                    ltrInvNum.Text = oInvBO[0].InvNumber;
                    ltrInvDate.Text = oInvBO[0].InvDate;
                    ltrAmount.Text = Convert.ToString(oInvBO[0].InvAmount);
                    if (oInvBO[0].InvFile == "")
                    {
                        string querystring = Request.Url.AbsoluteUri.ToString();
                        querystring = querystring.Replace("invdet", "printInvoice.aspx?invno=" + oInvBO[0].InvNumber);
                        Kosoft.Html2PDF.GeneratePDF oPDF = new Kosoft.Html2PDF.GeneratePDF();
                        oPDF.SavePDF(querystring, Request.PhysicalApplicationPath + "\\Docs\\invoices\\" + oInvBO[0].InvNumber + ".pdf", true);

                        oInv.SaveInvoiceFile(oInvBO[0].InvNumber + ".pdf", oInvBO[0].InvNumber);
                        ltrInvFile.Text = "<a href='printinv/" + oInvBO[0].InvNumber + "'>" + oInvBO[0].InvNumber + ".pdf" + "</a>";
                        //ltrInvFile.Text = "Invoice file not available. Click button below to create invoice document";
                        //ltrGenDate.Visible = false;
                        //btnGen.Text = "Generate";
                    }
                    else
                    {
                        ltrInvFile.Text = "<a href='pages/printinv/" + oInvBO[0].InvNumber + "'>" + oInvBO[0].InvFile + "</a>";
                        btnGen.Text = "Recreate Invoice";
                    }
                }
            }
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {


            //oPDF.SavePDF()
        }
    }
}