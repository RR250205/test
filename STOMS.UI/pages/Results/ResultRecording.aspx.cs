using STOMS.BO;
using STOMS.Common;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winnovative.WnvHtmlConvert;

namespace STOMS.UI.pages.Results
{
    public partial class ResultRecording : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblInformation.Text = "";
                Session["PgContentTitle"] = "Result Recording";
                if (Request.QueryString["sn"] != null)
                {
                    getSpecimen(Request.QueryString["sn"]);
                    hSpecimenNumber.Value = Request.QueryString["sn"];
                }
                else
                {
                    lblSpecimenNumError.Text = "Specimen Number Missing. <a href='/st'>Go to Specimen Tracking</a>";
                    dvMitoswab.Visible = false;
                }
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"] == "view")
                    {
                        if(hIsReleased.Value !="True")
                        {
                            ViewMode(true);
                            EditMode(false);
                        }
                    }
                    else if(Request.QueryString["mode"] == "edit")
                    {
                        if(hIsReleased.Value != "True")
                        {
                            EditMode(true);
                            ViewMode(false);
                        }
                    }
                }
                if (ltrCurrentSpecimenStatus.Text == "Received")
                    btnMitViewRecord.Visible = false;
            }
        }

        protected void btnMitResultSave_Click(object sender, EventArgs e)
        {
            int tda = new TestResultsDA().saveResults(new TestResultsBO() {
                SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                AssayID = 0,
                ResultID = Convert.ToInt32(hResultID.Value),
                TotalBuccalProteinyield = txtTotalBuccalProtein.Text,
                CitrateSynthase = txtCitrateSynthase.Text,
                RC_IV = txtRCIV.Text,
                RC_I = txtRCI.Text,
                analysisReveals = hMitTestAnalysys.Value,
                Interpretation = hMitInterpretation.Value,
                Notes = "",
                TenantID = Convert.ToInt32(Session["OrgID"]),
                PerformedBy = txtMitPerformedBy.Text,
                ResultDocID = Convert.ToInt32(hDocID.Value),
                IsReleased = Convert.ToBoolean(hIsReleased.Value)
            });
            if (tda > 0)
            {
                hResultID.Value = tda.ToString();
                getResults();
                ViewMode(true);
                EditMode(false);
                new SpecimenDA().updateSpecimenStatus(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value), "Result Recorded");
                ltrCurrentSpecimenStatus.Text = "Result Recorded";
            }
        }

        private void getSpecimen(string SpecimenNumber)
        {
          
            List<SpecimenInfoBO> oSpecimen = new List<SpecimenInfoBO>();
            oSpecimen = (new SpecimenDA()).GetSpecimenNumberInfo(SpecimenNumber, Convert.ToInt32(Session["OrgID"]));
            if (oSpecimen.Count > 0)
            {
                hSpecimenID.Value = oSpecimen[0].SpecimenID.ToString();
                hTestType.Value = oSpecimen[0].TestType;
                ltrSpecimenNumber.Text =  oSpecimen[0].SpecimenNumber;
                // Patient Info
                ltrFirstName.Text = oSpecimen[0].oPatient.FirstName;
                ltrLastName.Text =  oSpecimen[0].oPatient.LastName;

               //age
               if (oSpecimen[0].oPatient.DOB != "")
                {
                    if (oSpecimen[0].oPatient.DOB != "" && oSpecimen[0].SampleReceiveDate != "" || oSpecimen[0].SampleReceiveDate != "1/1/1900 12:00:00 AM")
                    {
                        DateTime Birth = Constant.ConvertToUSDateTime<DateTime, string>(oSpecimen[0].oPatient.DOB);
                        DateTime Today = Constant.ConvertToUSDateTime<DateTime, string>(oSpecimen[0].SampleReceiveDate);
                        if (Birth <= Today)
                        {
                            TimeSpan Span = Today - Birth;
                            DateTime Age = DateTime.MinValue + Span;
                            int Years = Age.Year - 1;
                            int Months = Age.Month - 1;
                            int Days = Age.Day - 1;
                            lblAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + Years.ToString() + " Years, " + Months.ToString() + " Months, " + Days.ToString() + " Days<br />";
                        }
                        else
                        {
                            lblAgeGender.Text = oSpecimen[0].oPatient.Gender + " / - ";
                        }
                    }
                    else
                    {
                        lblAgeGender.Text = oSpecimen[0].oPatient.Gender + " / - ";
                    }
                }
                else
                {
                    lblAgeGender.Text = "- ";
                }
                //age

                //lblAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + oSpecimen[0].oPatient.DOB;
                hPatientID.Value = Convert.ToString(oSpecimen[0].oPatient.PatientID);

                if (oSpecimen[0].oResult != null)
                {
                   hAssaySpecimenID.Value = Convert.ToString(oSpecimen[0].oResult.AssaySpecimenID);
                }
              
                //Specimen Information

                //ddSpecimentype.SelectedIndex = ddSpecimentype.Items.IndexOf(ddSpecimentype.Items.FindByText(oSpecimen[0].BloodType));
                ltrCurrentSpecimenStatus.Text =  oSpecimen[0].SpecimenStatus;
                if (oSpecimen[0].CustomerID != 0)
                {
                    hCustID.Value = Convert.ToString(oSpecimen[0].CustomerID);
                    List<CustomerBO> oCust = (new CustomerDA()).GetCustomer(hCustID.Value);
                    if (oCust.Count > 0)
                    {
                        ltrPhyName.Text =  oCust[0].CustomerName;
                        ltrFacility.Text = oCust[0].Facility;
                    }
                }
                getResults();
              //ltrStatus.Text = Common.Constant.GetStatusFormat(oSpecimen[0].SpecimenStatus);
            }
            else
            {
                lblSpecimenNumError.Text = "Specimen Number Invalid <a href='/st'>Go to Specimen Tracking</a>";
                dvMitoswab.Visible = false;
                
            }
        }

        protected void btnMitResultCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnMitGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/pages/Specimen?si=" + hSpecimenID.Value);
        }

        protected void btnMitViewRecord_Click(object sender, EventArgs e)
        {
            if (hResultID.Value != "0")
            {
                getResults();
                ViewMode(true);
                EditMode(false);
            }
            else
            {
                lblSpecimenNumError.Text = "Please Update results";
                dvMitoswab.Visible = false;
            }

        }

       private void getResults()
       {
            TestResultsBO testResultsBO = new TestResultsDA().getResults(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value));
            if (testResultsBO.ResultID > 0)
            {
                hResultID.Value = testResultsBO.ResultID.ToString();
                txtCitrateSynthase.Text= ltrMitCitrateSynthase.Text = testResultsBO.CitrateSynthase;
                txtRCI.Text= ltrMitRCI.Text = testResultsBO.RC_I;
                txtRCIV.Text= ltrMitRCIV.Text = testResultsBO.RC_IV;
                txtTotalBuccalProtein.Text= ltrMitTotalBuccalProtein.Text = testResultsBO.TotalBuccalProteinyield;
                hMitTestAnalysys.Value= dvmitTestAnalysysView.InnerHtml = testResultsBO.analysisReveals;
                hMitInterpretation.Value= dvmitInterpretationView.InnerHtml = testResultsBO.Interpretation;
                //hMitNotes.Value= dvmitNotesView.InnerHtml = testResultsBO.Notes;
                txtMitPerformedBy.Text = ltrMitPerformedBy.Text= testResultsBO.PerformedBy;
                hDocID.Value = testResultsBO.ResultDocID.ToString();
                hIsReleased.Value = Convert.ToString(testResultsBO.IsReleased);
                if(testResultsBO.IsReleased)
                {
                    HideButtons();
                }
                if (hDocID.Value != "0")
                {
                    DocumentBO documentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(hDocID.Value));
                    hDocNumber.Value = documentBO.DocNumber;
                    //btnMitPreview.PostBackUrl = "/Docs/Results/" + hDocNumber.Value + ".pdf";
                    string filesource = @"\Docs\Results\" + documentBO.DocNumber + "." + "pdf";
                    string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                    aMitPreview.HRef = url;
                }
            }
            else
            {
                //lblSpecimenNumError.Text = "Please Update results";
            }
        }

        protected void btnMitEditRecord_Click(object sender, EventArgs e)
        {
            EditMode(true);
            ViewMode(false);
          
        }

        private void EditMode(bool mode)
        {
            dvMitoswabResultEdit.Visible = mode;       
            btnMitResultSave.Visible = mode;
            btnMitViewRecord.Visible = mode;
        }

        private void ViewMode(bool mode)
        {
            dvMitoswabResultView.Visible = mode;
            btnMitEditRecord.Visible = mode;
            btnMitReleaseRecord.Visible = mode;
            aMitPreview.Visible = mode;
            btnMitDownload.Visible = mode;
            btnMitGenAsPDF.Visible = mode;
            if (hDocNumber.Value != "0")
            {
               // btnMitGenAsPDF.Visible = false;

            }
            else if (hDocNumber.Value == "0")
            {
                btnMitReleaseRecord.Visible = false;
                aMitPreview.Visible = false;
                btnMitDownload.Visible = false;
              //  ancPdfGen.PostBackUrl = "/Docs/Results/" + hDocNumber.Value + ".pdf";

                //ancPdfGen.InnerText = oSpecimen[0].oResult.ResultFileName;
                //string filepath = @"\Docs\Results\" + oSpecimen[0].oResult.ResultFileName + ".pdf";
                //string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                //ancPdfGen.HRef = uri;
            }
        }

        private void HideButtons()
        {
            btnMitGoBack.Visible = false;
            aMitPreview.Visible = false;
            btnMitDownload.Visible = false;
            btnMitGenAsPDF.Visible = false;
            btnMitEditRecord.Visible = false;
            btnMitReleaseRecord.Visible = false;
        }


        protected void btnMitReleaseRecord_Click(object sender, EventArgs e)
        {

            //Token
            DocumentBO objdoc = new DocumentBO();
            objdoc.DocID = Convert.ToInt32(hDocID.Value);
            objdoc.TokenID = Guid.NewGuid().ToString();
            objdoc.DocNumber = hDocNumber.Value;
            objdoc.TenantID=Convert.ToInt32(Session["OrgID"]);
            DocumentDA objDA = new DocumentDA();
            string token = objDA.UpdateDocNum(objdoc);

  
            //TestDetails

            TestResultsBO testBO = new TestResultsBO();
            testBO.CitrateSynthase = ltrMitCitrateSynthase.Text;
            testBO.RC_I = ltrMitRCI.Text;
            testBO.RC_IV = ltrMitRCIV.Text;
            testBO.TotalBuccalProteinyield = ltrMitTotalBuccalProtein.Text;
            testBO.analysisReveals = hMitTestAnalysys.Value;
            testBO.Interpretation = hMitInterpretation.Value;
            testBO.Notes = hMitNotes.Value;
            testBO.PerformedBy = ltrMitPerformedBy.Text;
            testBO.ResultDocID = Convert.ToInt32(hDocID.Value);
            testBO.ResultID = Convert.ToInt32(hResultID.Value);
            testBO.TenantID = Convert.ToInt32(Session["OrgID"]);
            testBO.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
            testBO.AssayID = Convert.ToInt32(hAssayID.Value);
            testBO.IsReleased = true;
            
            int rtnResID = new TestResultsDA().UpdateResults(testBO);
            if(rtnResID > 0)
            {
                HideButtons();
            }

            int emailEnableID = 0;
            
             if (Convert.ToInt32(Session["OrgID"]) == 4)
                emailEnableID = 7;
            //string filepath = @"\Docs\Results\" + hDocNumber.Value + ".pdf";
            string filepath = "/VerifyToken/Token?" + token ;
           // string filepath = "/Docs/Results/" + hDocNumber.Value;

           string link = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +filepath;

            //System.IO.FileStream fs = new FileStream(Server.MapPath("~/") + filepath, FileMode.Create);

            //Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //StreamReader objreader = new StreamReader(Server.MapPath("~/") + filepath, true);
            //List<string> list = new List<string>();
            //using (StreamReader reader = new StreamReader(Server.MapPath("~/") + filepath, true))
            //{
            //    string line;
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        list.Add(line); // Add to list.
            //        Console.WriteLine(line); // Write to console.
            //    }
            //   // Response.Redirect(line);
            //    Response.Redirect(list.ToString());
            //}

            // StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + filepath, true);
            //String line = sw.WriteLine();
            //Response.Redirect(sw.ReadLine());
            //Response.Redirect("E:\\Stroms project\\update git\\STORMS\\STOMS.UI\\Docs\\Results\\2018RES00043.pdf");


            //sw.WriteLine("End Request called at " + DateTime.Now.ToString());
            //sw.Close();

            //using (StreamWriter SW = new StreamWriter(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + filepath, true))
            //{
            //    SW.WriteLine("The message date time is - " + DateTime.Now.ToString());
            //    SW.Close();



            //var yourUri = new UriBuilder(link).Uri;
            //var endPoint = new Uri("http://localhost:8080/jasperserver/rest_v2/users/" + filepath);
            //var endPoint = new Uri("http://storms-staging.ko-shuka.com/"+filepath);
            // var endPoint = new Uri(HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath);
            //patient name


            //List<SpecimenInfoBO> oSpecimen = (new SpecimenDA()).GetSpecimenNumberInfo(hSpecimenNumber.Value, Convert.ToInt32(Session["OrgID"]));

            //hPatientName.Value = oSpecimen[0].oPatient.PatientName;

            //welcome

            List<EmailEnablementImplementationBO> emailBO = new EmailConfigurationDA().emailEnableImplementation(Convert.ToInt32(Session["OrgID"]), emailEnableID);

             if (emailBO[0].emailEnablementBO.isToEndUser == true)
            {               
                emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-userName", ltrPhyName.Text);
                emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-PatientName",ltrFirstName.Text +" "+ ltrLastName.Text);
                emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@TestType", hTestType.Value);
                emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@SpecimenNumber", ltrSpecimenNumber.Text.Trim());
                emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@ReportLink", link .ToString());

                EmailConfigBO ema = new EmailConfigBO();
                ema.Body = emailBO[0].emailEnablementTypeBO.EndUserTemplate;
                ema.Subject = "Specimen Report _ " + ltrSpecimenNumber.Text;
                ema.ToAddress = emailBO[0].emailEnablementBO.ToTenantEmails;
                new STOMS.UI.CommonCS.SendEmail(ema);                
                lblInformation.Text = "Email Sent Successfully";
                lblInformation.Focus();
            }
        }
      
        private string ConvertToPdf(string fileName, string templateName)
        {            
            // string imgPath = Server.MapPath("~\\images\\IliadLogo.png");
            string pdfSourceString = File.ReadAllText(Request.PhysicalApplicationPath + "\\templates\\" + templateName);

            PdfConverter mypdfconverter = pdfConverter();
            string outFile = null;
            // fileName = ancPdfGen.InnerText;
            string filepath = @"\Docs\Results\" + fileName + ".pdf";
            string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
            //ancPdfGen.HRef = uri;
            outFile = Path.Combine(Server.MapPath("~\\Docs\\Results"), fileName + ".pdf");

           // pdfSourceString = pdfSourceString.Replace("@@PatientName", imgPath);
            pdfSourceString = pdfSourceString.Replace("@@PatientName", ltrFirstName.Text);
            pdfSourceString = pdfSourceString.Replace("@@Date", DateTime.Now.ToString("dd/MM/yyyy"));
           // pdfSourceString = pdfSourceString.Replace("@@PatientID", Convert.ToString(specimenDetails.oPatient.PatientID));
          //  pdfSourceString = pdfSourceString.Replace("@@PatientDOB", specimenDetails.oPatient.DOB);
            pdfSourceString = pdfSourceString.Replace("@@SpecimenNumber", ltrSpecimenNumber.Text);
            pdfSourceString = pdfSourceString.Replace("@@PhysicianName", ltrPhyName.Text);
            pdfSourceString = pdfSourceString.Replace("@@TotalBuccalProten", ltrMitTotalBuccalProtein.Text);
            pdfSourceString = pdfSourceString.Replace("@@CitrateSynthase", ltrMitCitrateSynthase.Text);
            pdfSourceString = pdfSourceString.Replace("@@RCIV", ltrMitRCIV.Text);
            pdfSourceString = pdfSourceString.Replace("@@RCI", ltrMitRCI.Text);
            pdfSourceString = pdfSourceString.Replace("@@TestAnalysis", hMitTestAnalysys.Value);
            pdfSourceString = pdfSourceString.Replace("@@Interpretation", hMitInterpretation.Value);
            pdfSourceString = pdfSourceString.Replace("@@PerformedBy", ltrMitPerformedBy.Text);
            //PdfConverter pdfconverter = pdfConverter();

            int tda = new TestResultsDA().saveResults(new TestResultsBO()
            {
                SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                AssayID = 0,
                ResultID = Convert.ToInt32(hResultID.Value),
                TotalBuccalProteinyield = ltrMitTotalBuccalProtein.Text,
                CitrateSynthase = ltrMitCitrateSynthase.Text,
                RC_IV = ltrMitRCIV.Text,
                RC_I = ltrMitRCI.Text,
                analysisReveals = hMitTestAnalysys.Value,
                Interpretation = hMitInterpretation.Value,
                Notes = "",
                TenantID = Convert.ToInt32(Session["OrgID"]),
                PerformedBy = ltrMitPerformedBy.Text,
                ResultDocID = Convert.ToInt32(hDocID.Value),
                IsReleased = false
            });

            try
            {
                byte[] downloadBytes = mypdfconverter.GetPdfBytesFromHtmlStringWithTempFile(pdfSourceString);
               
                FileStream LabelFile = new FileStream(outFile, FileMode.Create, FileAccess.Write);
                LabelFile.Write(downloadBytes, 0, downloadBytes.Length);
                LabelFile.Close();

                string filesource = @"\Docs\Results\" + hDocNumber.Value + "." + "pdf";
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                aMitPreview.HRef = url;
                // mypdfconverter.SavePdfFromHtmlStringToFileWithTempFile(pdfSourceString, outFile);
                //   mypdfconverter.SavePdfFromUrlToFile(Server.MapPath("~\\templates\\Testresult.html"), outFile);
                //   mypdfconverter.SavePdfFromHtmlFileToFile(Server.MapPath("~\\templates\\Testresult.html"), outFile);
                // mypdfconverter.SavePdfFromHtmlStringToFile(pdfSourceString, outFile);
                // byte[] downloadBytes = mypdfconverter.GetPdfBytesFromHtmlStringWithTempFile(pdfSourceString);

                //  string ra = Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);
                // string ra_ip = ra.Replace(".", "-");
                //  DateTime dt = DateTime.Now.Date;
                //  string new_name = dt.Month.ToString() + "-" + dt.Day + "-" + dt.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + ra_ip;
                //  Random rnd = new Random();
                //string FileName = fileName+".pdf";

                /* System.Web.HttpResponse response1 = System.Web.HttpContext.Current.Response;
                 response1.Clear();
                 response1.AddHeader("Content-Type", "binary/octet-stream");
                 response1.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "; size=" + downloadBytes.Length.ToString());
                 response1.Flush();
                 response1.BinaryWrite(downloadBytes);
                 response1.Flush();
                 response1.End();*/
            }
            catch (Exception ex)
            {
                string ex1 = ex.Message;
                //ltrRepeatNo.Text = "Target Site :+ " + ex.TargetSite + " Message : " + ex.Message;
            }
            return "";
        }

        private PdfConverter pdfConverter()
        {
            System.Drawing.Image headerImage = System.Drawing.Image.FromFile(Request.PhysicalApplicationPath + "\\assets\\TemplateImages\\Header_Religen.PNG");
            PdfConverter pdfConverter = new PdfConverter();
            pdfConverter.LicenseKey = "3fbs/ez97OT96fPt/e7s8+zv8+Tk5OQ=";
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4; //Winnovative.WnvHtmlConvert.PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = Winnovative.WnvHtmlConvert.PdfCompressionLevel.AboveNormal; // NoCompression;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
            pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;
            pdfConverter.PdfHeaderOptions.HeaderSubtitleText = "";
            pdfConverter.PdfHeaderOptions.HeaderText = "";
            pdfConverter.PdfHeaderOptions.HeaderImage = System.Drawing.Image
                .FromFile(Request.PhysicalApplicationPath + "\\assets\\TemplateImages\\Header_Religen.PNG");
            pdfConverter.PdfHeaderOptions.HeaderHeight = 80;
            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            pdfConverter.PdfDocumentOptions.LeftMargin = 15;
            pdfConverter.PdfDocumentOptions.RightMargin = 10;
            pdfConverter.PdfDocumentOptions.TopMargin = 10;
            pdfConverter.PdfDocumentOptions.BottomMargin = 10;
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
            pdfConverter.PdfDocumentOptions.AutoSizePdfPage = false;
            pdfConverter.PdfDocumentOptions.EmbedFonts = false;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
            pdfConverter.RightToLeftEnabled = false;
            pdfConverter.ScriptsEnabledInImage = false;
            pdfConverter.AvoidImageBreak = true;
            pdfConverter.AvoidTextBreak = true;
            pdfConverter.AlphaBlendEnabled = false;
            pdfConverter.PdfSecurityOptions.CanCopyContent = true;
            pdfConverter.PdfSecurityOptions.CanPrint = true;
            pdfConverter.PdfSecurityOptions.CanEditContent = true;
            pdfConverter.PdfDocumentOptions.ShowFooter = true;
            pdfConverter.PdfFooterOptions.FooterImage= System.Drawing.Image
                .FromFile(Request.PhysicalApplicationPath + "\\assets\\TemplateImages\\Footer_Religen.PNG");
            pdfConverter.PdfFooterOptions.PageNumberTextFontSize = 7;
            pdfConverter.PdfFooterOptions.PageNumberTextColor = System.Drawing.Color.Blue;
            pdfConverter.PdfFooterOptions.DrawFooterLine = false;
            pdfConverter.PdfFooterOptions.FooterText = "";
            pdfConverter.PdfFooterOptions.PageNumberText = "Page No: ";
            pdfConverter.PdfFooterOptions.PageNumberYLocation = 70;
            pdfConverter.PdfFooterOptions.ShowPageNumber = true;
            pdfConverter.PdfFooterOptions.FooterHeight = 80;
            return pdfConverter;
        }

        protected void btnMitPreview_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Server.MapPath("~/Docs/Results/") + hDocNumber.Value + ".pdf");
        }

        protected void btnMitDownload_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename="+hDocNumber.Value+".pdf");
            Response.TransmitFile(Server.MapPath("/Docs/Results/" + hDocNumber.Value + ".pdf"));
            Response.End();
            ViewMode(true);
        }

        protected void btnMitGenAsPDF_Click(object sender, EventArgs e)
        {
            if (hDocID.Value == "0")
            {
                DocumentBO documentBO = new DocumentBO()
                {
                    OrgDocName = "",
                    CreatedBy = Convert.ToInt32(Session["UserID"]),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    DocType = "pdf"
                };
                DocumentBO newDocumentBO = new DocumentDA().GenerateAndDocumentNo(documentBO, "ResultDoc");
                hDocID.Value = Convert.ToString(newDocumentBO.DocID);
                hDocNumber.Value = newDocumentBO.DocNumber;

                //string DocNumber = new ReportDA().GenerateResultReport(Convert.ToInt32(hAssaySpecimenID.Value), Convert.ToInt32(Session["OrgID"]), "pdf", Convert.ToInt32(Session["UserID"]));
                ConvertToPdf(newDocumentBO.DocNumber, "TestResultForMITO.html");

                ViewMode(true);
            }
            else
            {
                ConvertToPdf(hDocNumber.Value, "TestResultForMITO.html");

                ViewMode(true);
            }
        }

    }
}
