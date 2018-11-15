using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winnovative.WnvHtmlConvert;

namespace STOMS.UI.pages
{
    public partial class sampleReport : System.Web.UI.Page
    {
        //<hr style='border-top: 4px solid rgba(121, 85, 72, 0.8);'><br/>
        private string pdffooterString = "51110 Campus Drive Suite 190, plymouth Meeting, PA, 19462; Phone: 610-441-9050;Fax:6105375075 www.iliadneuro.com";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
              /*  string invno = (string)RouteData.Values["invno"];

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
                }*/
            }
        }

        //string fileContent = File.ReadAllText(Request.PhysicalApplicationPath + "\\templates\\InvoiceTemplate.html");

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string imgPath = Server.MapPath("~\\assets\\img\\pdflogo.png");
            string pdfSourceString = "<div style='width:840px;'>" +
                "<img src='" + imgPath + "'style='margin-left:300px'/>" +
        "<h2 style = 'position:center' ><u> Anti - Folate Receptor A Antibodies - Binding and Blocking Assay Report </u></h2>" +
                " <br/><br/><br/>" +
                 "<h3><b> Date: " + DateTime.Now.ToShortDateString() + "<span style = 'margin-left:450px'> PA: " + txtPAName.Text + " </span></b></h3>" +
                             "<br/><br/>" +
                            " <table style = 'border:1px solid orange;font-size: 22px;border-collapse: collapse; width:750px;'>" +
                                 " <tr style = 'border:1px solid orange '>" +
                                      " <th style = 'border:1px solid orange;text-align: left;font-size: 22px;padding: 10px; '> Patient ID </th>" +
                                           " <td style = 'border:1px solid orange;font-size: 22px;padding: 10px; ' colspan = '2'> " + txtName.Text + " </td>" +

                                              " </tr>" +
                                              " <tr style = 'border:1px solid orange;font-size: 22px; '>" +
                                                   " <th style = 'border:1px solid orange;text-align: left;padding: 10px; '> DOB </th>" +
                                                   " <td style = 'border:1px solid orange;padding: 10px; ' colspan = '2'> " + txtDOB.Text + " </td>" +


                                                   " </tr>" +
                                                    "<tr style = 'border:1px solid orange '>" +
                                                        " <td style = 'border:1px solid orange;padding: 10px; '>" +

                                                         " </td>" +
                                                         " <td style = 'border:1px solid orange;padding: 10px; '></td>" +
                                                          " <th style = 'border:1px solid orange;padding: 10px; '> Comments </ th>" +
                                                      " </tr>" +
                                                      " <tr style = 'border:1px solid orange;padding: 10px; '>" +
                                                            "<td style = 'border:1px solid orange;padding: 10px; '><b><u> Binding Assay </u></b><br/> (pmol IgG bound/ ml serum) </td>" +
                                                                           "<td style = 'border:1px solid orange;padding: 10px; '>" +
                                                                                  txtBindingAssay.Text +
                                                                            "</td>" +
                                                                            "<td style = 'border:1px solid orange;padding: 10px; '>" +
                                                                                 txtBindingAssayComments.Text +
                                                                            " </td>" +
                                                                         "</tr>" +
                                                                        " <tr style = 'border:1px solid orange '>" +
                                                                             " <td style = 'border:1px solid orange;padding: 10px; '><b><u> Blocking Assay </u></b><br/> (pmol folate blocked/ ml serum)  </td>" +
                                                                                             " <td style = 'border:1px solid orange;padding: 10px; '>" +
                                                                                             txtBlockingAssay.Text +
                                                                                              " </td>" +
                                                                                              " <td style = 'border:1px solid orange;padding: 10px; '>" +
                                                                                                   txtBlockingAssayComments.Text +
                                                                                               " </td>" +
                                                                                           " </tr>" +
                                                                                       " </table>" +
                                                                                       "<br/><br/>" +//Here is the page break
                                                                                       "<div style='page-break-before: always; page -break-after: always; font-size=18px'>" +
                                                                                       "<b><u style='font-size=22px'>Assay Notes:</u></b>" +
                                                                                        "<br/><br/>" +
                                                                                       "<ul>" +
                                                                                       "<li>Folate receptor (FR) blocking antibody is defined as an antibody that blocks the binding of folic acid to the folate receptor.</li>" +
                                                                                       "<br/><br/>" +
                                                                                       "<li>Binding autoantibodies against the folate receptor recognize multiple epitopes on the folate receptor and can be blocking and binding antibodies(IgG only).</li>" +
                                                                                      "<br/><br/>" +
                                                                                       "<li>As in the case of most autoimmune disorders, the presence of an autoantibody should be considered as possibly abnormal.Low titers of antibodies have been found in individuals who are otherwise healthy. Typically, lower CSF 5-MTHF levels have been associated with higher antibody titers but sometimes, low CSF 5 - MTHF values have been observed in patients with low titers or no antibodies.  </li>" +
                                                                                      "<br/><br/>" +
                                                                                       "<li>The concentration of antibodies is known to fluctuate over time (and in response to changes in cow’s milk intake) so repeating the test, if clinically warranted, has been recommended.</li>" +
                                                                                      "<br/><br/>" +
                                                                                       "<li>The designation “low”, “medium” or “high” is arbitrary and is based on the cumulative experience of performing the assays, not on its clinical relevance.</li>" +

                                                                                       "<br/>" +
                                                                                       "<br/>" +
                                                                                       "<b><u style='font-size=22px'>DISCLAIMER </u></b>" +
                                                                                      "<br/><br/>" +
                                                                                       "At this time, the assays are provided as research tools only. They are not intended to diagnose any disease or form the basis for any clinical decisions, and are not approved by any regulatory body." +
                                                                                       "</div>" +//Here is the page break
                                                                                       "<div style='page-break-before: always; page -break-after: always;font-size=18px'>" +
                                                                                       "<b><u style='font-size=22px' >References:</u></b>" +
                                                                                       "<br/><br/>" +
                                                                                       "<u>Frye RE</u>, <u>Sequeira JM</u>, <u>Quadros EV</u>, <u>James SJ</u>, <u>Rossignol DA</u>. Cerebral folate receptor autoantibodies in autism spectrum disorder.Mol.Psychiatry. 2013 Mar; 18(3):369 - 81." +
                                                                                       "<br/><br/>" +
                                                                                       "Ramaekers VT, Sequeira JM, Blau N, Quadros EV. A milk-free diet downregulates folate receptor autoimmunity in cerebral folate deficiency syndrome. Dev Med Child Neurol. 2008 May; 50(5):346 - 52." +
                                                                                        "<br/><br/>" +
                                                                                       "Ramaekers <u>VT</u>, <u>Blau N</u>, <u>Sequeira JM</u>, <u>Nassogne MC</u>, <u>Quadros EV</u>. Folate receptor autoimmunity and cerebral folate deficiency in low - functioning autism with neurological deficits. Neuropediatrics. 2007 Dec; 38(6):276 - 81." +
                                                                                       "<br/><br/>" +
                                                                                       "Rossignol,DA, Frye RE: Folate receptor alpha autoimmunity and cerebral folate deficiency in autism spectrum disorders(REVIEW).<u>Journal of Pediatric Biochemistry</u>. 2012: 263–271." +
                                                                                       "</div>" +
                                                                                       " </div>";
            PdfConverter pdfconverter = GetPdfConverter();
            //GetPdfBytesFromHtmlFile()
            byte[] downloadBytes = pdfconverter.GetPdfBytesFromHtmlStringWithTempFile(pdfSourceString);

            string ra = Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);
            string ra_ip = ra.Replace(".", "-");
            DateTime dt = DateTime.Now.Date;
            string new_name = dt.Month.ToString() + "-" + dt.Day + "-" + dt.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + ra_ip;
            Random rnd = new Random();
            string FileName = "SpecimenResult.pdf";

            System.Web.HttpResponse response1 = System.Web.HttpContext.Current.Response;
            response1.Clear();
            response1.AddHeader("Content-Type", "binary/octet-stream");
            response1.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "; size=" + downloadBytes.Length.ToString());
            response1.Flush();
            response1.BinaryWrite(downloadBytes);
            response1.Flush();
            response1.End();
        }
        private PdfConverter GetPdfConverter()
        {
            PdfConverter pdfConverter = new PdfConverter();
            pdfConverter.LicenseKey = "3fbs/ez97OT96fPt/e7s8+zv8+Tk5OQ=";

            pdfConverter.PageWidth = 0;

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal; // NoCompression;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
            pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;
            pdfConverter.PdfHeaderOptions.HeaderSubtitleText = "";
            pdfConverter.PdfHeaderOptions.HeaderText = "";
            pdfConverter.PdfDocumentOptions.ShowHeader = false;
            pdfConverter.PdfDocumentOptions.ShowFooter = true;
            pdfConverter.PdfDocumentOptions.LeftMargin = 50;
            pdfConverter.PdfDocumentOptions.RightMargin = 50;
            pdfConverter.PdfDocumentOptions.TopMargin = 50;
            pdfConverter.PdfDocumentOptions.BottomMargin = 30;
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
            pdfConverter.PdfDocumentOptions.FitWidth = true;
            pdfConverter.PdfDocumentOptions.AutoSizePdfPage = true;
            pdfConverter.PdfDocumentOptions.EmbedFonts = false;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
            pdfConverter.RightToLeftEnabled = false;
            pdfConverter.ScriptsEnabledInImage = false;
            pdfConverter.AvoidImageBreak = false;

            pdfConverter.PdfFooterOptions.FooterText = pdffooterString;
            pdfConverter.PdfFooterOptions.DrawFooterLine = true;

            pdfConverter.PdfFooterOptions.PageNumberText = "Page";
            pdfConverter.PdfFooterOptions.ShowPageNumber = true;

            // pdfConverter.PdfHeaderOptions.HeaderImage = footerImage();
            //pdfConverter.PdfHeaderOptions.HeaderImageLocation

            //pdfConverter.


            return pdfConverter;
        }

        private System.Drawing.Image footerImage()
        {
            System.Drawing.Image img;
            //img.FromFile(@"~/assets/img/pdflogo.png");

            return System.Drawing.Image.FromFile(Server.MapPath("~\\assets\\img\\pdflogo.png"));
        }

        protected void btnGenerateFromHtlm_Click(object sender, EventArgs e)
        {
            /*string fileContent = File.ReadAllText(Request.PhysicalApplicationPath + "\\templates\\InvoiceTemplate.html");
            fileContent = fileContent.Replace("@@InvoicedTo", txtName.Text);
            fileContent = fileContent.Replace("@@InvoiceNo", txt);
            fileContent = fileContent.Replace("@@InvoiceDate", oInvBO[0].InvDate);

            if (oInvBO[0].InvoiceDetail.Count > 0)
            {
                fileContent = fileContent.Replace("@@SNO", Convert.ToString(oInvBO[0].InvoiceDetail[0].ItemOrder));
                fileContent = fileContent.Replace("@@Description", oInvBO[0].InvoiceDetail[0].ItemDesc);
                fileContent = fileContent.Replace("@@Rate", Convert.ToString(oInvBO[0].InvoiceDetail[0].UnitCost));
                fileContent = fileContent.Replace("@@Amount", Convert.ToString(oInvBO[0].InvoiceDetail[0].TotalCost));
                fileContent = fileContent.Replace("@@InvAmount", Convert.ToString(oInvBO[0].InvoiceDetail[0].TotalCost));
            }
            ltrInv.Text = fileContent;*/
        }
    }
}