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

namespace STOMS.UI.pages
{
    public partial class SpecimenDetail : System.Web.UI.Page
    {
        string DocNumber;
        string errMsg = string.Empty;

        private static int Sample = 122;
        private static int SampleReceipt = 123;
        private static int ReviewAndUpdate = 124;

        private static bool ReadAccess = false;
        private static bool ReviewAndUpdateWriteAccess = false;
        private static bool ReviewAndUpdateExecuteAccess = false;
        private static bool ExportAccess = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserGroupPermission(); 
            if (!this.IsPostBack)
            {
                Session["PgContentTitle"] = "Specimen Details";
                if (Request.QueryString["ai"] != null)
                    hAssayID.Value = Convert.ToString(Request.QueryString["ai"]);
                else
                    ltrAssayError.Text = "Assay Info is Empty. Please Load Specimen info from Assay List. otherwise Results will not saved!.";                
                if (Request.QueryString["si"] != null)
                    hSpecimenID.Value = Convert.ToString(Request.QueryString["si"]);
                if (Request.QueryString["sn"] != null)
                    getSpecimen(Request.QueryString["sn"]);
                if (hSpecimenID.Value != "" && Convert.ToInt32( hSpecimenID.Value)>0)
                {
                    AssayInfo.PopulateSpecimenAssay(Convert.ToInt32(hSpecimenID.Value));
                    popSpecimen();                    
                }
                else
                    dvSpecimenDetail.Visible = false;
                if(lblCurrentSpecimenStatus.Text =="Result Recorded")
                    btnVerifyAndSendEmail.Visible = true;     
                else
                    btnVerifyAndSendEmail.Visible = false;
            }
        }

        private void UserGroupPermission()
        { 
            List<FunctionBO> fbo = new KoSoft.Entitlement.KoAccess(STOMS.Common.Constant.DBConnectionString).GetUserGroupPermission(Convert.ToInt32(Constant.ProductID),Convert.ToInt32(Session["OrgID"]));
            foreach (var item in fbo)
            {
                if (ReviewAndUpdate == item.entServBO.ServiceID)
                {
                    if(item.WriteAccess == true)
                        ReviewAndUpdateWriteAccess = true;

                    else if (item.ExecuteAccess == true)
                        ReviewAndUpdateExecuteAccess = true;
                }
            }

            if (ReviewAndUpdateWriteAccess == false)
            { 
                aModalPatientInfo.Visible = false; 
                aModalVerification.Visible = false;
                aReqPhysicianDetails.Visible = false;
                aSpecimenInformation.Visible = false;
            }
            else
            { 
                aModalPatientInfo.Visible = true;
                aModalVerification.Visible = true;
                aReqPhysicianDetails.Visible = true;
                aSpecimenInformation.Visible = true;
            }
        }

        private void popSpecimen()
        {            
            List<SpecimenInfoBO> oSpecimen = new List<SpecimenInfoBO>();
            if (hAssayID.Value != "")
                oSpecimen = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(hSpecimenID.Value), Convert.ToInt32(hAssayID.Value));           
            else
                oSpecimen = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(hSpecimenID.Value));
            Session.Remove("SpecimenID");
            if (oSpecimen.Count > 0)
            {
                ltrSpecimenNumber.Text = oSpecimen[0].SpecimenNumber;
                // Patient Info
                txtFirstName.Text = lblFirstName.Text = oSpecimen[0].oPatient.FirstName;
                txtLastName.Text = lblLastName.Text = oSpecimen[0].oPatient.LastName;
                ltrAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + oSpecimen[0].oPatient.DOB;
                optF.Checked = (oSpecimen[0].oPatient.Gender == "F" ? true : false);
                optM.Checked = !optF.Checked;
                txtDOB.Text = oSpecimen[0].oPatient.DOB;
                txtstreet.Text = oSpecimen[0].oPatient.Street;
                ltrLocation.Text = oSpecimen[0].oPatient.Street + (oSpecimen[0].oPatient.City == "" ? "" : " ," + oSpecimen[0].oPatient.City) + (oSpecimen[0].oPatient.Zip == "" ? "" : ", " + oSpecimen[0].oPatient.Zip) + (oSpecimen[0].oPatient.Country == "" ? "" : ", " + oSpecimen[0].oPatient.Country);
                txtCity.Text = oSpecimen[0].oPatient.City;
                txtZip.Text = oSpecimen[0].oPatient.Zip;
                
                txtCountry.Text = oSpecimen[0].oPatient.Country;

                //Verification Info
                if (oSpecimen[0].IsConsent)
                {
                    YesConsent.Visible = true;
                    NoConsent.Visible = false;
                    chkConsentProvided.Checked = true;
                }
                else
                {
                    YesConsent.Visible = false;
                    NoConsent.Visible = true;
                    chkConsentProvided.Checked = false;
                }

                if (oSpecimen[0].IsReqComplete)
                {
                    YesReqComp.Visible = true;
                    NoReqComp.Visible = false;
                    chkRequisition.Checked = true;
                }
                else
                {
                    YesReqComp.Visible = false;
                    NoReqComp.Visible = true;
                    chkRequisition.Checked = false;
                }

                if (oSpecimen[0].IsSpecimenAccept)
                {
                    YesAccept.Visible = true;
                    NoAccept.Visible = false;
                    chkAcceptTest.Checked = true;
                }
                else
                {
                    YesAccept.Visible = false;
                    NoAccept.Visible = true;
                    chkAcceptTest.Checked = false;
                }
                lblReasonRej.Text = oSpecimen[0].RejectReasons;
                //Guardian Info
                txtGuardianFirstName.Text = oSpecimen[0].oPatient.GuardianFirstName;
                txtGuardianLastName.Text = oSpecimen[0].oPatient.GuardianLastName;
                txtGuardianStreet.Text = oSpecimen[0].oPatient.GuardianStreet;
                txtGuardianCity.Text = oSpecimen[0].oPatient.GuardianCity;
                txtGuardianCountry.Text = oSpecimen[0].oPatient.GuardianCountry;
                txtGuardianZip.Text = oSpecimen[0].oPatient.GuardianZip;
                ltrGuardianName.Text = oSpecimen[0].oPatient.GuardianFirstName + " " + oSpecimen[0].oPatient.GuardianLastName;
                ltrGuardianAddress.Text = oSpecimen[0].oPatient.GuardianStreet + ", " + oSpecimen[0].oPatient.GuardianCity + ", " + oSpecimen[0].oPatient.GuardianZip + ", " + oSpecimen[0].oPatient.GuardianCountry;
               
                hPatientID.Value = Convert.ToString(oSpecimen[0].oPatient.PatientID);
                lbldrawnDatetime.Text = DateTime.Parse(oSpecimen[0].DateDrawn).ToShortDateString() + " / " + oSpecimen[0].TimeDrawn;
                lblTransitTime.Text = oSpecimen[0].TransitTime;
                lblTransitTemp.Text = oSpecimen[0].TransitTemperature;
                lblVolRec.Text = oSpecimen[0].VolumeReceived;
                lblPotInter.Text = oSpecimen[0].InterSubstance;

                //Specimen Information
                txtDrawndate.Text = DateTime.Parse(oSpecimen[0].DateDrawn).ToShortDateString();
                txtDrawnTime.Text = oSpecimen[0].TimeDrawn;
                txtTransitTime.Text = oSpecimen[0].TransitTime;
                txtTransitTemp.Text = oSpecimen[0].TransitTemperature;
                txtVolReceived.Text = oSpecimen[0].VolumeReceived;
                txtPotInterfer.Text = oSpecimen[0].InterSubstance;
                lbltype.Text = oSpecimen[0].SpecimentType;
                rdSpecimenserum.Checked = (oSpecimen[0].SpecimentType == "Serum" ? true : false);
                rdSpecimenblood.Checked = (oSpecimen[0].SpecimentType == "Blood" ? true : false);
                lblRemainType.Text = oSpecimen[0].SpecimentType;
                lblReceivedOn.Text = txtReceivedOn.Text = oSpecimen[0].SampleReceiveDate;
                lblBloodType.Text = oSpecimen[0].BloodType;
                
                ddSpecimentype.SelectedIndex = ddSpecimentype.Items.IndexOf(ddSpecimentype.Items.FindByText(oSpecimen[0].BloodType));
                lblCurrentSpecimenStatus.Text = oSpecimen[0].SpecimenStatus;

                if (oSpecimen[0].CustomerID != 0)
                {
                    hCustID.Value = Convert.ToString(oSpecimen[0].CustomerID);
                    List<CustomerBO> oCust = (new CustomerDA()).GetCustomer(hCustID.Value);
                    if (oCust.Count > 0)
                    {
                        lblPhyName.Text = txtPhyName.Text = oCust[0].CustomerName;
                        txtPhyAddress1.Text = oCust[0].Address1;
                        txtPhyCity.Text = oCust[0].City;
                        ltrReqAddress.Text = txtPhyAddress1.Text + "," + txtPhyCity.Text;
                        txtPhyPhone.Text = oCust[0].Phone;
                        txtPhyEmail.Text = oCust[0].Email;
                        txtPhyState.Text = oCust[0].State;
                        txtPhyPCode.Text = oCust[0].Zipcode;
                        txtPhyFax.Text = oCust[0].Fax;
                        ltrReqContact.Text = txtPhyPhone.Text + " - " + txtPhyEmail.Text;
                        lblDiagnosis.Text = txtDiagnosis.Text = oCust[0].Diagnosis;
                        lblDiagnosisCode.Text = txtDiagnosisCode.Text = oCust[0].DiagnosisCode;
                        lblResultType.Text = ddlResultType.SelectedValue = oCust[0].ResultType;
                    }
                }
                if (hAssayID.Value != "")
                {
                    lblRemainType.Text = oSpecimen[0].oResult.SpecimenType + (oSpecimen[0].oResult.SubSpecimenType == "" ? "" : " / " + oSpecimen[0].oResult.SubSpecimenType);

                    lblRemainVol.Text = txtRemainVol.Text = oSpecimen[0].oResult.RemainVol;
                    ltrBIN.Text = Convert.ToString(oSpecimen[0].oResult.BindValue) + " / " + oSpecimen[0].oResult.BindValComment;
                    txtBindValue.Text = Convert.ToString(oSpecimen[0].oResult.BindValue);
                    txtBindComment.Text = oSpecimen[0].oResult.BindValComment;

                    ltrBlocking.Text = Convert.ToString(oSpecimen[0].oResult.BlockValue) + " / " + oSpecimen[0].oResult.BlockValComment;
                    txtBlockValue.Text = Convert.ToString(oSpecimen[0].oResult.BlockValue);
                    txtBlockComment.Text = oSpecimen[0].oResult.BlockValComment;
                    ltrRetested.Text = oSpecimen[0].oResult.IsRepeat ? "Yes" : "No";
                    ltrRepeatNo.Text = Convert.ToString(oSpecimen[0].oResult.RepeatNo);
                    chkNeedToRetest.Checked = oSpecimen[0].oResult.IsRepeat;

                    if (oSpecimen[0].oResult.SubSpecimenType != "")
                        ddBloodType.SelectedValue = oSpecimen[0].oResult.SubSpecimenType;

                    if (oSpecimen[0].oResult.ResultFileName == "" && oSpecimen[0].SpecimenStatus == "Result Recorded")
                    {
                        lnkGenRpt.Visible = true;
                        lnkbtnRegenerate.Visible = false;
                        //lnkviewhardcopy.Visible = true;
                    }
                  
                    else if (oSpecimen[0].oResult.ResultFileName != "" && oSpecimen[0].SpecimenStatus == "Result Recorded")
                    {
                       
                        lnkGenRpt.Visible = false;
                        ancPdfGen.InnerText = oSpecimen[0].oResult.ResultFileName;
                        string filepath = @"\Docs\Results\" + oSpecimen[0].oResult.ResultFileName + ".pdf";
                        string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                        ancPdfGen.HRef = uri;

                        lnkbtnRegenerate.Visible = true;
                        //oSpecimen[0].oResult.ResultDocID 
                        //oSpecimen[0].oResult.ResultSentDate
                    }

                    else
                    {

                    }
                    if (oSpecimen[0].oResult.SpecimenType == "Serum")
                    {
                        optRemainSerum.Checked = true;
                        optRemainBlood.Checked = false;
                    }
                    hAssaySpecimenID.Value = Convert.ToString(oSpecimen[0].oResult.AssaySpecimenID);

                }
                btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(oSpecimen[0].SpecimenStatus);
                //ltrStatus.Text = Common.Constant.GetStatusFormat(oSpecimen[0].SpecimenStatus);
                hSpecimenStatus.Value = oSpecimen[0].SpecimenStatus;
                SetResultSection(oSpecimen[0].SpecimenStatus);
                if (oSpecimen[0].oResult != null && oSpecimen[0].oResult.AssayStatus != string.Empty)
                    SetResultSection(oSpecimen[0].oResult.AssayStatus);

                if (hAssayID.Value == "" || hAssayID.Value == Convert.ToString(0))
                {
                    dvResCap.Visible = true;
                    dvResInput.Visible = false;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Assay Info is Empty.Please load Specimen Information from Assay list.";
                    lnkGenRpt.Visible = false;
                }
                
                DocumentBO documentBO = new ReportDA().ViewhardCopy(oSpecimen[0].ReqFormCopyID);
                ancViewCopy.InnerText = documentBO.DocNumber;
                string filesource = @"\Docs\RequestForm\" + documentBO.DocNumber + "." + documentBO.DocType;
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                ancViewCopy.HRef = url;
            }

            else
            {
                if(hAssayID.Value != "")
                {

                    lblSpcimenNum.Text = "Invalid Assay Number ";
                }
                else
                {
                    lblSpcimenNum.Text = "Invalid Specimen Number ";
                }
                
                dvSpecimenDetail.Visible=false;
            }

        }

        private void SetResultSection(string SpStatus)
        {
            switch (SpStatus)
            {
                case "Received":
                    dvResCap.Visible = false;
                    dvResInput.Visible = false;
                    ResLink.Visible = false;
                    btnSave.Visible = true;
                    break;

                case "Rejected":
                    dvResCap.Visible = false;
                    dvResInput.Visible = false;
                    ResLink.Visible = false;
                    dvReason.Visible = true;
                    break;

                case "Assigned to Assay":
                    dvResCap.Visible = false;
                    dvResInput.Visible = false;
                    ResLink.Visible = true;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Cannot Update the Result Before test has been completed";
                    break;

                case "In Testing":
                    dvResCap.Visible = true;
                    dvResInput.Visible = false;
                    ResLink.Visible = true;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Once Assay Testing Complete, result update will be available...";
                    break;

                case "Test Completed":
                    dvResCap.Visible = true;
                    dvResInput.Visible = true;
                    ResLink.Visible = true;
                    dvResultMsg.Visible = false;
                    break;

                case "Result Recorded":
                    dvResCap.Visible = false;
                    dvResInput.Visible = true;
                    ResLink.Visible = true;
                    break;

                case "Result Delivered":
                    dvResCap.Visible = true;
                    dvResInput.Visible = false;
                    ResLink.Visible = true;
                    break;

                default:
                    dvResCap.Visible = false;
                    dvResInput.Visible = false;
                    ResLink.Visible = false;
                    break;
            }
        }

        //protected void btnNewSpecimen_Click(object sender, EventArgs e)
        //{
        //    // the ID value should be retrived using a common function, by passing tenant ID & Service Code
        //    Response.Redirect("/redirect.aspx?ID=46");
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Add to Assay")
            {
                int AssayID = (new SpecimenDA()).AddSpecimenToAssay(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value));
                hAssayID.Value = Convert.ToString(AssayID);
                Response.Redirect("~/pages/st");
            }
        }

        protected void btnSaveReq_Click(object sender, EventArgs e)
        {


            CustomerBO oCust = (new CustomerDA()).SaveCustomer(
                new CustomerBO
                {
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    CustomerName = txtPhyName.Text,
                    Address1 = txtPhyAddress1.Text,
                    City = txtPhyCity.Text,
                    State = txtPhyState.Text,
                    Country = ddCountry.SelectedValue,
                    Phone = txtPhyPhone.Text,
                    Email = txtPhyEmail.Text,
                    CustomerID = Convert.ToInt32(hCustID.Value),
                    Fax=txtPhyFax.Text.Trim(),
                    Diagnosis = txtDiagnosis.Text.Trim(),
                    DiagnosisCode = txtDiagnosisCode.Text.Trim(),
                    ResultType = ddlResultType.SelectedValue,
                    Zipcode = txtPhyPCode.Text.Trim()
                },
                Convert.ToInt32(hSpecimenID.Value));
            if (oCust != null)
            {
                hCustID.Value = Convert.ToString(oCust.CustomerID);
                lblPhyName.Text = txtPhyName.Text;
                ltrReqAddress.Text = txtPhyAddress1.Text + "," + txtPhyCity.Text;
                ltrReqContact.Text = txtPhyPhone.Text + " - " + txtPhyEmail.Text;
                lblDiagnosis.Text = txtDiagnosis.Text.Trim();
                lblDiagnosisCode.Text = txtDiagnosisCode.Text.Trim();
                lblResultType.Text = ddlResultType.SelectedValue;
            }
        }

        protected void btnAssay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/assay");
        }

        protected void btnPatientSave_Click(object sender, EventArgs e)
        {
            try
            {
                int PatientID = (new SpecimenDA()).SavePatientInfoDA(new PatientBO
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Gender = (optF.Checked ? "F" : "M"),
                    DOB = txtDOB.Text.Trim(),
                    PatientID = Convert.ToInt32(hPatientID.Value),
                    CreatedBy = Convert.ToInt32(Session["UserID"]),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    Street = txtstreet.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    Zip = txtZip.Text.Trim(),
                    Country = txtCountry.Text.Trim(),
                    GuardianFirstName = txtGuardianFirstName.Text.Trim(),
                    GuardianLastName = txtGuardianLastName.Text.Trim(),
                    GuardianStreet = txtGuardianStreet.Text.Trim(),
                    GuardianCity = txtGuardianCity.Text.Trim(),
                    GuardianCountry = txtGuardianCountry.Text.Trim(),
                    GuardianZip = txtGuardianZip.Text.Trim()
                });

                if (Convert.ToString(PatientID) != "")
                {
                    ltrLocation.Text = txtstreet.Text.Trim() + (txtCity.Text.Trim() == "" ? "" : ", " + txtCity.Text.Trim()) + (txtZip.Text.Trim() == "" ? "" : ", " + txtZip.Text.Trim()) + (txtCountry.Text.Trim() == "" ? "" : ", " + txtCountry.Text.Trim());
                    lblFirstName.Text = txtFirstName.Text;
                    lblLastName.Text = txtLastName.Text;
                    ltrAgeGender.Text = (optF.Checked ? "F" : "M") + "/ " + txtDOB.Text;
                    ltrGuardianName.Text = txtGuardianFirstName.Text + " " + txtGuardianLastName.Text;
                    ltrGuardianAddress.Text = txtGuardianStreet.Text + (txtGuardianCity.Text.Trim() == "" ? "" : ", " + txtGuardianCity.Text.Trim()) + (txtGuardianZip.Text.Trim() == "" ? "" : ", " + txtGuardianZip.Text) + (txtGuardianCountry.Text.Trim() == "" ? "" : ", " + txtGuardianCountry.Text);
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {


        }

        protected void btnResults_Click(object sender, EventArgs e)
        {
            if (hAssayID.Value != "")
            {
                string sType = "";
                if (optRemainSerum.Checked)
                    sType = "Serum";
                if (optRemainBlood.Checked)
                    sType = "Blood";

                (new SpecimenDA()).SaveTestResult(new ResultBO
                {
                    AssayID = Convert.ToInt32(hAssayID.Value),
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    SpecimenType = sType,
                    SubSpecimenType = (optRemainBlood.Checked ? ddBloodType.SelectedValue : ""),
                    RemainVol = txtRemainVol.Text,
                    BindValue = Convert.ToDecimal(txtBindValue.Text),
                    BindValComment = txtBindComment.Text,
                    BlockValue = Convert.ToDecimal(txtBlockValue.Text),
                    BlockValComment = txtBlockComment.Text,
                    IsRepeat = chkNeedToRetest.Checked
                });
                lblRemainType.Text = sType + (optRemainBlood.Checked ? "/" + ddBloodType.SelectedValue : "");
                lblRemainVol.Text = txtRemainVol.Text;
                ltrBIN.Text = txtBindValue.Text + " / " + txtBindComment.Text;
                ltrBlocking.Text = txtBlockValue.Text + " / " + txtBlockComment.Text;
                ltrRetested.Text = chkNeedToRetest.Checked ? "Yes" : "No";
                ltrRepeatNo.Text = chkNeedToRetest.Checked ? Convert.ToString(Convert.ToInt32(ltrRepeatNo.Text) + 1) : "No";
                lnkGenRpt.Visible = true;
                if (lblCurrentSpecimenStatus.Text == "Result Recorded")
                {
                    lnkGenRpt.Visible = false; 
                    if(DocNumber!=null)
                    {
                        lnkbtnRegenerate.Visible = true;
                    }
                }
                btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus("Result Recorded");                
                lblCurrentSpecimenStatus.Text = "Result Recorded";                
                hSpecimenStatus.Value = "Result Recorded";
                
            }
            else
            {

            }
        }

        protected void btnSpecimen_Click(object sender, EventArgs e)
        {
            try
            {
                int SpecimenID = (new SpecimenDA()).UpdateSpecimenInfo(new SpecimenInfoBO
                {
                    SpecimentType = (rdSpecimenserum.Checked ? "Serum" : "Blood"),
                    BloodType = ddSpecimentype.SelectedValue,
                    DateDrawn = txtDrawndate.Text.Trim(),
                    TimeDrawn = txtDrawnTime.Text.Trim(),
                    TransitTime = txtTransitTime.Text.Trim(),
                    TransitTemperature = txtTransitTemp.Text.Trim(),
                    VolumeReceived = txtVolReceived.Text.Trim(),
                    Comment = txtPotInterfer.Text.Trim(),
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    SampleReceiveDate = txtReceivedOn.Text
                });
                if (Convert.ToString(SpecimenID) != String.Empty)
                {
                    lbldrawnDatetime.Text = txtDrawndate.Text.Trim()+" "+txtDrawnTime.Text.Trim();
                    lblTransitTime.Text = txtTransitTime.Text.Trim();
                    lblTransitTemp.Text = txtTransitTemp.Text.Trim();
                    lblVolRec.Text = txtVolReceived.Text.Trim();
                    lblPotInter.Text = txtPotInterfer.Text.Trim();
                    lbltype.Text = (rdSpecimenserum.Checked ? "Serum" : "Blood");
                    lblReceivedOn.Text = txtReceivedOn.Text;
                    lblBloodType.Text = ddSpecimentype.SelectedValue;
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        protected void lnkGenRpt_Click(object sender, EventArgs e)
        {
            if (hSpecimenStatus.Value == "Result Recorded")
            {

                PatientBO patientBO = new PatientBO()
                {
                    PatientName = lblFirstName.Text + " " + lblLastName.Text,
                    PatientID = Convert.ToInt32(hPatientID.Value),
                    DOB = txtDOB.Text.Trim() != "" ? txtDOB.Text.Trim() : "",

                };

                ResultBO resultBO = new ResultBO()
                {
                    BindValue = Convert.ToDecimal(txtBindValue.Text),
                    BindValComment = txtBindComment.Text,
                    BlockValue = Convert.ToDecimal(txtBlockValue.Text),
                    BlockValComment = txtBlockComment.Text,
                    AssayID = Convert.ToInt32(hAssayID.Value),
                    AssaySpecimenID = Convert.ToInt32(hAssaySpecimenID.Value)
                };

                SpecimenInfoBO specimenInfoBO = new SpecimenInfoBO()
                {
                    oPatient = patientBO,
                    oResult = resultBO
                };
                string DocNumber = new ReportDA().GenerateResultReport(Convert.ToInt32(hAssaySpecimenID.Value), Convert.ToInt32(Session["OrgID"]), "pdf", Convert.ToInt32(Session["UserID"]));
                ConvertToPdf(DocNumber, "Testresult.html", specimenInfoBO);

                btnVerifyAndSendEmail.Visible = true;
                lnkGenRpt.Visible = false;
                ancPdfGen.InnerText = DocNumber;
                //string url = Server.MapPath(@"~\Docs\Results\"+DocNumber+".pdf");
                //ancPdfGen.HRef = url;
                string filepath = @"\Docs\Results\" + DocNumber + ".pdf";
                string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                ancPdfGen.HRef = uri;
                lnkbtnRegenerate.Visible = true;
                //oSpecimen[0].oResult.ResultDocID 
                //oSpecimen[0].oResult.ResultSentDate
                btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus("Result Recorded");

            }
        }

        //protected void lnkviewhardcopy_Click(object sender, EventArgs e)
        //{
        //    //if (lblCurrentSpecimenStatus.Text == "Result Recorded")
        //    //{
        //        //PatientBO patientBO = new PatientBO()
        //        //{
        //        //    PatientName = lblFirstName.Text + " " + lblLastName.Text,
        //        //    PatientID = Convert.ToInt32(hPatientID.Value),
        //        //    DOB = txtDOB.Text.Trim() != "" ? txtDOB.Text.Trim() : "",

        //        //};               

        //        //ResultBO resultBO = new ResultBO()
        //        //{

        //        // AssaySpecimenID = Convert.ToInt32(hAssaySpecimenID.Value)
        //        //};
        //        //string fileName = Path.GetFileName(flSampleHardCopy.FileName);
        //        string DocNumber = new ReportDA().ViewhardCopy( Convert.ToInt32(Session["OrgID"]), "jpg", Convert.ToInt32(Session["DocID"]));
        //        ancViewCopy.InnerText = DocNumber;
        //        string filepath = @"\Docs\RequestForm\" + DocNumber + ".jpg";
        //        string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
        //        ancViewCopy.HRef = uri;

        //        if (DocNumber != null)
        //        {
        //            lnkviewhardcopy.Visible = false;
        //        }
        //    //}
            
        //}

        private string ConvertToPdf(string fileName, string templateName, SpecimenInfoBO specimenDetails)
        {
            string imgPath = Server.MapPath("~\\images\\IliadLogo.png");
            string pdfSourceString = File.ReadAllText(Request.PhysicalApplicationPath + "\\templates\\" + templateName);

            PdfConverter mypdfconverter = pdfConverter();
            string outFile = null;
            // fileName = ancPdfGen.InnerText;
            string filepath = @"\Docs\Results\" + fileName + ".pdf";
            string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
            ancPdfGen.HRef = uri;
            outFile = Path.Combine(Server.MapPath("~\\Docs\\Results"), fileName + ".pdf");

            pdfSourceString = pdfSourceString.Replace("@@imgPath", imgPath);
            pdfSourceString = pdfSourceString.Replace("@@PatientName", specimenDetails.oPatient.PatientName);
            pdfSourceString = pdfSourceString.Replace("@@Date", DateTime.Now.ToString("dd/MM/yyyy"));
            pdfSourceString = pdfSourceString.Replace("@@PatientID", Convert.ToString(specimenDetails.oPatient.PatientID));
            pdfSourceString = pdfSourceString.Replace("@@PatientDOB", specimenDetails.oPatient.DOB);
            pdfSourceString = pdfSourceString.Replace("@@BindingValue", Convert.ToString(specimenDetails.oResult.BindValue));
            pdfSourceString = pdfSourceString.Replace("@@BindingComments", specimenDetails.oResult.BindValComment);
            pdfSourceString = pdfSourceString.Replace("@@BlockingValue", Convert.ToString(specimenDetails.oResult.BlockValue));
            pdfSourceString = pdfSourceString.Replace("@@BlockingComments", specimenDetails.oResult.BlockValComment);
            pdfSourceString = pdfSourceString.Replace("@@FileNumber", fileName);
            //PdfConverter pdfconverter = pdfConverter();

            try
            {
                byte[] downloadBytes = mypdfconverter.GetPdfBytesFromHtmlStringWithTempFile(pdfSourceString);

                FileStream LabelFile = new FileStream(outFile, FileMode.Create, FileAccess.Write);
                LabelFile.Write(downloadBytes, 0, downloadBytes.Length);
                LabelFile.Close();


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
                ltrRepeatNo.Text = "Target Site :+ " + ex.TargetSite + " Message : " + ex.Message;
            }
            return "";
        }
        private PdfConverter pdfConverter()
        {

            PdfConverter pdfConverter = new PdfConverter();
            pdfConverter.LicenseKey = "3fbs/ez97OT96fPt/e7s8+zv8+Tk5OQ=";


            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4; //Winnovative.WnvHtmlConvert.PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = Winnovative.WnvHtmlConvert.PdfCompressionLevel.Best; // NoCompression;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
            pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;
            pdfConverter.PdfHeaderOptions.HeaderSubtitleText = "";
            pdfConverter.PdfHeaderOptions.HeaderText = "";
            pdfConverter.PdfDocumentOptions.ShowHeader = false;
            pdfConverter.PdfDocumentOptions.ShowFooter = false;
            pdfConverter.PdfDocumentOptions.LeftMargin = 20;
            pdfConverter.PdfDocumentOptions.RightMargin = 20;
            pdfConverter.PdfDocumentOptions.TopMargin = 0;
            pdfConverter.PdfDocumentOptions.BottomMargin = 0;
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
            pdfConverter.PdfDocumentOptions.AutoSizePdfPage = true;
            pdfConverter.PdfDocumentOptions.EmbedFonts = true;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
            pdfConverter.RightToLeftEnabled = false;
            pdfConverter.ScriptsEnabledInImage = false;
            pdfConverter.AvoidImageBreak = true;
            pdfConverter.AvoidTextBreak = true;
            pdfConverter.AlphaBlendEnabled = true;
            pdfConverter.PdfSecurityOptions.CanCopyContent = true;
            pdfConverter.PdfSecurityOptions.CanPrint = true;
            pdfConverter.PdfSecurityOptions.CanEditContent = true;
            pdfConverter.PdfFooterOptions.DrawFooterLine = false;

            pdfConverter.PdfFooterOptions.PageNumberText = "";
            pdfConverter.PdfFooterOptions.ShowPageNumber = false;

            return pdfConverter;
        }

        protected void btnVerificationInfo_Click(object sender, EventArgs e)
        {
            
            try
            {
                string status = "Pending";
                    
                if (chkAcceptTest.Checked && chkConsentProvided.Checked && chkRequisition.Checked)
                    status = "Received";                    

                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    PatientID = Convert.ToInt32(hPatientID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = chkConsentProvided.Checked,
                    IsReqComplete = chkRequisition.Checked,
                    IsSpecimenAccept = chkAcceptTest.Checked,
                    SpecimenStatus = status,
                });
                if (Convert.ToString(PatientID) != String.Empty)
                {
                    if (chkConsentProvided.Checked)
                    {
                        YesConsent.Visible = true;
                        NoConsent.Visible = false;
                    }
                    else
                    {
                        YesConsent.Visible = false;
                        NoConsent.Visible = true;
                    }
                    if (chkRequisition.Checked)
                    {
                        YesReqComp.Visible = true;
                        NoReqComp.Visible = false;
                    }
                    else
                    {
                        YesReqComp.Visible = false;
                        NoReqComp.Visible = true;
                    }

                    if (chkAcceptTest.Checked)
                    {
                        YesAccept.Visible = true;
                        NoAccept.Visible = false;
                    }
                    else
                    {
                        YesAccept.Visible = false;
                        NoAccept.Visible = true;
                    }
                    btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(status);
                    lblCurrentSpecimenStatus.Text = "Received";
                    if (!chkConsentProvided.Checked || !chkRequisition.Checked)
                        lblCurrentSpecimenStatus.Text = "Pending";
                    btnSave.Visible = true;
                }


                //    IsConsent = YesConsent.Visible,
                //    IsReqComplete = YesReqComp.Visible,
                //    IsSpecimenAccept = YesAccept.Visible
                //    //PatientID = Convert.ToInt32(hPatientID.Value),
                //    //CreatedBy = Convert.ToInt32(Session["UserID"]),
                //    //TenantID = Convert.ToInt32(Session["OrgID"]),


            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            
        }

        protected void lnkbtnRegenerate_Click(object sender, EventArgs e)
        {
            try
            {
                PatientBO patientBO = new PatientBO()
                {
                    PatientName = lblFirstName.Text + " " + lblLastName.Text,
                    PatientID = Convert.ToInt32(hPatientID.Value),
                    DOB = txtDOB.Text.Trim() != "" ? txtDOB.Text.Trim() : "",

                };
                ResultBO resultBO = new ResultBO()
                {
                    BindValue = Convert.ToDecimal(txtBindValue.Text),
                    BindValComment = txtBindComment.Text,
                    BlockValue = Convert.ToDecimal(txtBlockValue.Text),
                    BlockValComment = txtBlockComment.Text,
                    AssayID = Convert.ToInt32(hAssayID.Value),
                    AssaySpecimenID = Convert.ToInt32(hAssaySpecimenID.Value)

                };

                SpecimenInfoBO specimenInfoBO = new SpecimenInfoBO()
                {
                    oPatient = patientBO,
                    oResult = resultBO
                };

                string DocNumber = ancPdfGen.InnerText; /*new ReportDA().GenerateResultReport(Convert.ToInt32(hAssaySpecimenID.Value), Convert.ToInt32(Session["OrgID"]), "pdf", Convert.ToInt32(Session["UserID"]));*/
                ConvertToPdf(DocNumber, "Testresult.html", specimenInfoBO);

                lnkGenRpt.Visible = false;
                ancPdfGen.InnerText = DocNumber;
                //string url = Server.MapPath(@"~\Docs\Results\" + DocNumber + ".pdf");
                //ancPdfGen.HRef = url;
                string filepath = @"\Docs\Results\" + DocNumber + ".pdf";
                string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                ancPdfGen.HRef = uri;
            }
            catch (Exception exp)
            {

            }
        }

        protected void btnVerifyAndSendEmail_Click(object sender, EventArgs e)
        {
            if (txtPhyEmail.Text == "")
            {
                ltrEmailSuccessmsg.Text = "";
                ltrEmailErrormsg.Text = "Require Physician Email";
            }
            else
            {
                if (ancPdfGen.HRef != "" && lblPhyName.Text.Trim() != "" && txtPhyEmail.Text != "")
                {
                    List<EmailEnablementImplementationBO> emailBO = new EmailConfigurationDA().emailEnableImplementation(Convert.ToInt32(Session["OrgID"]), 4);


                    if (emailBO[0].emailEnablementBO.isToEndUser == true)
                    {
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-userName", lblPhyName.Text.Trim());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@TestType", "Frat");
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@SpecimenNumber", ltrSpecimenNumber.Text.Trim());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@ReportLink", ancPdfGen.HRef);


                        EmailConfigBO ema = new EmailConfigBO();
                        ema.Body = emailBO[0].emailEnablementTypeBO.EndUserTemplate;
                        ema.Subject = "Report View";
                        ema.ToAddress = txtPhyEmail.Text.Trim();
                        new STOMS.UI.CommonCS.SendEmail(ema);
                        ltrEmailErrormsg.Text = "";
                        ltrEmailSuccessmsg.Text = "Email sent Successfully";
                    }
                }
            }

        }

        protected void btnSpcimenNum_Click(object sender, EventArgs e)
        {            
            getSpecimen(txtSpcimenNum.Text);
            AssayInfo.PopulateSpecimenAssay(Convert.ToInt32(hSpecimenID.Value));
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {           
               
            SpecimenDA oSpecimenDA = new SpecimenDA();           

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

                //save request form copy
                DocumentBO newDocumentBO = new DocumentDA().SaveReqFormCopy(documentBO);
                hDocID.Value = Convert.ToString(newDocumentBO.DocID);
                string DocName = newDocumentBO.DocNumber;
                flSampleHardCopy.SaveAs(Server.MapPath("~/Docs/RequestForm/") + DocName + "." + fileName.Substring(fileName.LastIndexOf('.') + 1));
                     
                ancViewCopy.InnerText = DocName;
                string file = @"\Docs\RequestForm\" + DocName + "." + documentBO.DocType;
                string Url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + file;
                ancViewCopy.HRef = Url;

                //update request form copyID
                new ReportDA().updateReqFormCopyID(Convert.ToInt32(Session["OrgID"]), hDocID.Value, hSpecimenID.Value);
            }
        }        

        private void getSpecimen(string SpecimenNumber)
        {
            dvSpecimenDetail.Visible = true;
            List<SpecimenInfoBO> oSpecimen = new List<SpecimenInfoBO>();
            oSpecimen = (new SpecimenDA()).GetSpecimenNumberInfo(SpecimenNumber,Convert.ToInt32(Session["OrgID"]));
            if (oSpecimen.Count > 0)
            {
                hSpecimenID.Value = oSpecimen[0].SpecimenID.ToString();
                ltrSpecimenNumber.Text = oSpecimen[0].SpecimenNumber;
                // Patient Info
                txtFirstName.Text = lblFirstName.Text = oSpecimen[0].oPatient.FirstName;
                txtLastName.Text = lblLastName.Text = oSpecimen[0].oPatient.LastName;
                ltrAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + oSpecimen[0].oPatient.DOB;
                optF.Checked = (oSpecimen[0].oPatient.Gender == "F" ? true : false);
                optM.Checked = !optF.Checked;
                txtDOB.Text = oSpecimen[0].oPatient.DOB;
                txtstreet.Text = oSpecimen[0].oPatient.Street;
                ltrLocation.Text = oSpecimen[0].oPatient.Street + (oSpecimen[0].oPatient.City == "" ? "" : " ," + oSpecimen[0].oPatient.City) + (oSpecimen[0].oPatient.Zip == "" ? "" : ", " + oSpecimen[0].oPatient.Zip) + (oSpecimen[0].oPatient.Country == "" ? "" : ", " + oSpecimen[0].oPatient.Country);
                txtCity.Text = oSpecimen[0].oPatient.City;
                txtZip.Text = oSpecimen[0].oPatient.Zip;
                txtCountry.Text = oSpecimen[0].oPatient.Country;
                lblSpcimenNum.Text = "";
                
                //Verification Info
                if (oSpecimen[0].IsConsent)
                {
                    YesConsent.Visible = true;
                    NoConsent.Visible = false;
                    chkConsentProvided.Checked = true;
                }
                else
                {
                    YesConsent.Visible = false;
                    NoConsent.Visible = true;
                    chkConsentProvided.Checked = false;
                }

                if (oSpecimen[0].IsReqComplete)
                {
                    YesReqComp.Visible = true;
                    NoReqComp.Visible = false;
                    chkRequisition.Checked = true;
                }
                else
                {
                    YesReqComp.Visible = false;
                    NoReqComp.Visible = true;
                    chkRequisition.Checked = false;
                }

                if (oSpecimen[0].IsSpecimenAccept)
                {
                    YesAccept.Visible = true;
                    NoAccept.Visible = false;
                    chkAcceptTest.Checked = true;
                }
                else
                {
                    YesAccept.Visible = false;
                    NoAccept.Visible = true;
                    chkAcceptTest.Checked = false;
                }
                lblReasonRej.Text = oSpecimen[0].RejectReasons;
                //Guardian Info
                txtGuardianFirstName.Text = oSpecimen[0].oPatient.GuardianFirstName;
                txtGuardianLastName.Text = oSpecimen[0].oPatient.GuardianLastName;
                txtGuardianStreet.Text = oSpecimen[0].oPatient.GuardianStreet;
                txtGuardianCity.Text = oSpecimen[0].oPatient.GuardianCity;
                txtGuardianCountry.Text = oSpecimen[0].oPatient.GuardianCountry;
                txtGuardianZip.Text = oSpecimen[0].oPatient.GuardianZip;
                ltrGuardianName.Text = oSpecimen[0].oPatient.GuardianFirstName + " " + oSpecimen[0].oPatient.GuardianLastName;
                ltrGuardianAddress.Text = oSpecimen[0].oPatient.GuardianStreet + ", " + oSpecimen[0].oPatient.GuardianCity + ", " + oSpecimen[0].oPatient.GuardianZip + ", " + oSpecimen[0].oPatient.GuardianCountry;

                hPatientID.Value = Convert.ToString(oSpecimen[0].oPatient.PatientID);
                lbldrawnDatetime.Text = DateTime.Parse(oSpecimen[0].DateDrawn).ToShortDateString() + " / " + oSpecimen[0].TimeDrawn;
                lblTransitTime.Text = oSpecimen[0].TransitTime;
                lblTransitTemp.Text = oSpecimen[0].TransitTemperature;
                lblVolRec.Text = oSpecimen[0].VolumeReceived;
                lblPotInter.Text = oSpecimen[0].InterSubstance;

                //Specimen Information
                txtDrawndate.Text = DateTime.Parse(oSpecimen[0].DateDrawn).ToShortDateString();
                txtDrawnTime.Text = oSpecimen[0].TimeDrawn;
                txtTransitTime.Text = oSpecimen[0].TransitTime;
                txtTransitTemp.Text = oSpecimen[0].TransitTemperature;
                txtVolReceived.Text = oSpecimen[0].VolumeReceived;
                txtPotInterfer.Text = oSpecimen[0].InterSubstance;
                lbltype.Text = oSpecimen[0].SpecimentType;
                rdSpecimenserum.Checked = (oSpecimen[0].SpecimentType == "Serum" ? true : false);
                rdSpecimenblood.Checked = (oSpecimen[0].SpecimentType == "Blood" ? true : false);
                lblRemainType.Text = oSpecimen[0].SpecimentType;
                lblReceivedOn.Text = txtReceivedOn.Text = oSpecimen[0].SampleReceiveDate;
                lblBloodType.Text = oSpecimen[0].BloodType;
                ddSpecimentype.SelectedIndex = ddSpecimentype.Items.IndexOf(ddSpecimentype.Items.FindByText(oSpecimen[0].BloodType));
                lblCurrentSpecimenStatus.Text = oSpecimen[0].SpecimenStatus;

                if (oSpecimen[0].CustomerID != 0)
                {
                    hCustID.Value = Convert.ToString(oSpecimen[0].CustomerID);
                    List<CustomerBO> oCust = (new CustomerDA()).GetCustomer(hCustID.Value);
                    if (oCust.Count > 0)
                    {
                        lblPhyName.Text = txtPhyName.Text = oCust[0].CustomerName;
                        txtPhyAddress1.Text = oCust[0].Address1;
                        txtPhyCity.Text = oCust[0].City;
                        ltrReqAddress.Text = txtPhyAddress1.Text + "," + txtPhyCity.Text ;
                        txtPhyPhone.Text = oCust[0].Phone;
                        txtPhyEmail.Text = oCust[0].Email;
                        ltrReqContact.Text = txtPhyPhone.Text + " - " + txtPhyEmail.Text;
                        lblDiagnosis.Text = txtDiagnosis.Text = oCust[0].Diagnosis;
                        lblDiagnosisCode.Text = txtDiagnosisCode.Text = oCust[0].DiagnosisCode;
                        lblResultType.Text = ddlResultType.SelectedValue = oCust[0].ResultType;
                    }
                }
                if (hAssayID.Value != "")
                {
                    if (oSpecimen[0].oResult != null)
                    {
                        lblRemainType.Text = oSpecimen[0].oResult.SpecimenType + (oSpecimen[0].oResult.SubSpecimenType == "" ? "" : " / " + oSpecimen[0].oResult.SubSpecimenType);

                        lblRemainVol.Text = txtRemainVol.Text = oSpecimen[0].oResult.RemainVol;
                        ltrBIN.Text = Convert.ToString(oSpecimen[0].oResult.BindValue) + " / " + oSpecimen[0].oResult.BindValComment;
                        txtBindValue.Text = Convert.ToString(oSpecimen[0].oResult.BindValue);
                        txtBindComment.Text = oSpecimen[0].oResult.BindValComment;

                        ltrBlocking.Text = Convert.ToString(oSpecimen[0].oResult.BlockValue) + " / " + oSpecimen[0].oResult.BlockValComment;
                        txtBlockValue.Text = Convert.ToString(oSpecimen[0].oResult.BlockValue);
                        txtBlockComment.Text = oSpecimen[0].oResult.BlockValComment;
                        ltrRetested.Text = oSpecimen[0].oResult.IsRepeat ? "Yes" : "No";
                        ltrRepeatNo.Text = Convert.ToString(oSpecimen[0].oResult.RepeatNo);
                        chkNeedToRetest.Checked = oSpecimen[0].oResult.IsRepeat;

                        if (oSpecimen[0].oResult.SubSpecimenType != "")
                            ddBloodType.SelectedValue = oSpecimen[0].oResult.SubSpecimenType;

                        if (oSpecimen[0].oResult.ResultFileName == "" && oSpecimen[0].SpecimenStatus == "Result Recorded" && oSpecimen[0].SpecimenStatus == "Received")
                        {
                            lnkGenRpt.Visible = true;
                            lnkbtnRegenerate.Visible = false;
                            //lnkviewhardcopy.Visible = true;
                        }

                        else if (oSpecimen[0].oResult.ResultFileName != "" && oSpecimen[0].SpecimenStatus == "Result Recorded" && oSpecimen[0].SpecimenStatus == "Received")
                        {

                            lnkGenRpt.Visible = false;
                            ancPdfGen.InnerText = oSpecimen[0].oResult.ResultFileName;
                            string filepath = @"\Docs\Results\" + oSpecimen[0].oResult.ResultFileName + ".pdf";
                            string uri = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filepath;
                            ancPdfGen.HRef = uri;

                            lnkbtnRegenerate.Visible = true;
                            //oSpecimen[0].oResult.ResultDocID 
                            //oSpecimen[0].oResult.ResultSentDate
                        }

                        else
                        {

                        }
                        if (oSpecimen[0].oResult.SpecimenType == "Serum")
                        {
                            optRemainSerum.Checked = true;
                            optRemainBlood.Checked = false;
                        }
                        hAssaySpecimenID.Value = Convert.ToString(oSpecimen[0].oResult.AssaySpecimenID);
                    }

                }
                btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(oSpecimen[0].SpecimenStatus);
                //ltrStatus.Text = Common.Constant.GetStatusFormat(oSpecimen[0].SpecimenStatus);
                hSpecimenStatus.Value = oSpecimen[0].SpecimenStatus;
                SetResultSection(oSpecimen[0].SpecimenStatus);
                if (oSpecimen[0].oResult != null && oSpecimen[0].oResult.AssayStatus != string.Empty)
                    SetResultSection(oSpecimen[0].oResult.AssayStatus);

                if (hAssayID.Value == "" || hAssayID.Value == Convert.ToString(0))
                {
                    dvResCap.Visible = true;
                    dvResInput.Visible = false;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Assay Info is Empty.Please load Specimen Information from Assay list.";
                    lnkGenRpt.Visible = false;
                }

                DocumentBO documentBO = new ReportDA().ViewhardCopy(oSpecimen[0].ReqFormCopyID);
                ancViewCopy.InnerText = documentBO.DocNumber;
                string filesource = @"\Docs\RequestForm\" + documentBO.DocNumber + "." + documentBO.DocType;
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                ancViewCopy.HRef = url;
            }

            else
            {
                lblSpcimenNum.Text = "Specimen Number Invalid";
                dvSpecimenDetail.Visible = false;
                //ltrSpecimenNumber.Text = "";
                //// Patient Info
                //txtFirstName.Text = lblFirstName.Text ="";
                //txtLastName.Text = lblLastName.Text ="";
                //ltrAgeGender.Text = "" + "  " + "";
                //optF.Checked = ("" == "F" ? true : false);
                //optM.Checked = !optF.Checked;
                //txtDOB.Text =  "";
                //txtstreet.Text = "";
                //ltrLocation.Text = "";
                //txtCity.Text = "";
                //txtZip.Text = "";
                //txtCountry.Text = "";
                //ltrGuardianAddress.Text = "";
                ////Specimen Information
                //lbldrawnDatetime.Text = "";
                //lblTransitTime.Text = "";
                //lblTransitTemp.Text = "";
                //lblVolRec.Text = "";
                //lblPotInter.Text = "";

                ////txtDrawndate.Text = "";
                ////txtDrawnTime.Text = "";
                ////txtTransitTime.Text = "";
                ////txtTransitTemp.Text = "";
                ////txtVolReceived.Text = "";
                ////txtPotInterfer.Text = "";
                //lbltype.Text = "";
                //rdSpecimenserum.Checked = ("" == "Serum" ? true : false);
                //rdSpecimenblood.Checked = ("" == "Blood" ? true : false);
                //lblRemainType.Text = "";
                //lblReceivedOn.Text = txtReceivedOn.Text = "";
                //lblBloodType.Text ="";


                //lblCurrentSpecimenStatus.Text ="";

            }
        }
    }
}
