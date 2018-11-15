﻿using System;
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
    public partial class SpecimenView : System.Web.UI.Page
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
                clearFields();
                TenantConfiguration();
                
                if (Request.QueryString["ai"] != null)
                    hAssayID.Value = Convert.ToString(Request.QueryString["ai"]);
                else
                    ltrAssayError.Text = "Assay Info is Empty. Please Load Specimen info from Assay List. otherwise Results will not saved!.";
                if (Request.QueryString["si"] != null)
                    hSpecimenID.Value = Convert.ToString(Request.QueryString["si"]);
                if (Request.QueryString["sn"] != null)
                    getSpecimen(Request.QueryString["sn"]);
                //Reasion(); 
                setTestType();
                if (hSpecimenID.Value != "" && Convert.ToInt32(hSpecimenID.Value) > 0)
                {
                    //AssayInfo.PopulateSpecimenAssay(Convert.ToInt32(hSpecimenID.lblCurrentSpecimenStatus));
                    //getSpecimen(hSpecimenID.Value);
                    popSpecimen();
                    viewSpecimenAuditlog();
                    CheckInfo();
                    Reasion();
                }
                else                
                if (lblCurrentSpecimenStatus.Text == "Result Recorded")
                    btnVerifyAndSendEmail.Visible = true;
                else
                    btnVerifyAndSendEmail.Visible = false;

                TestResults();                
            }
        }

        private string CheckInfo()
        {
            bool PatientInformation = false;
            bool PhysicianInformation = false;
            bool SpecimenInformation = false;
            bool Verification = false;

            string errormessage = "";
            //PatientInformation
            if (lblFirstName.Text == "")
                PatientInformation = true;
            if (ltrLocation.Text == "" && ltrGuardianAddress.Text == "")
                PatientInformation = true;
            else
                if(ltrLocation.Text != "")
                {
                if (txtstreet.Text == "" || txtCity.Text == "" || txtState.Text == "" || txtCountry.Text == "" || txtZip.Text == "")
                    PatientInformation = true;
                }
                else if(ltrGuardianAddress.Text != "")
                {
                if (txtGuardianStreet.Text == "" || txtGuardianCity.Text == "" || txtGuardianState.Text == "" || txtGuardianCountry.Text == "" || txtGuardianZip.Text == "")
                    PatientInformation = true;
                }                
            if (ltrAgeGender.Text == "")
                PatientInformation = true;
            //if (ltrEmailID.Text == "" && ltrContactNo.Text == "" && ltrGuardianEmailID.Text == "" && ltrGuardianContactNo.Text == "")
            //    PatientInformation = true;
            
           
            //PhysicianInformation
            if (lblPhyName.Text == "")
                PhysicianInformation = true;
            if (lblFacility.Text == "")
                PhysicianInformation = true;
            if (lblPhyEmail.Text == "")
                PhysicianInformation = true;
            if (ltrReqAddress.Text == "")
                PhysicianInformation = true;
            if (ltrReqContact.Text == "")
                PhysicianInformation = true;
            //if (ltrFaxNumber.Text == "")
            //    PhysicianInformation = true;
            //SpecimenInformation
            if (lblSpecimenNumber.Text == "")
                SpecimenInformation = true;
            if (lblTestType.Text == "")
                SpecimenInformation = true;
            if (lbltype.Text == "")
                SpecimenInformation = true;
            //if (Convert.ToInt32(Session["OrgID"]) == 2)
            //{
            //    if (lblBloodType.Text == "")
            //        SpecimenInformation = true;
            //}
            if (lblReceivedOn.Text == "")
                SpecimenInformation = true;
            if (lbldrawnDatetime.Text == "")
                SpecimenInformation = true;
            if (lblTransitTime.Text == "")
                SpecimenInformation = true;
            if (lblTransitTemp.Text == "")
                SpecimenInformation = true;
            if (lblVolRec.Text == "")
                SpecimenInformation = true;            
            if (lblCurrentSpecimenStatus.Text == "")
                SpecimenInformation = true;
            //Verification
            if (!chkConsentProvided.Checked)
                Verification = true;
            if (!chkAcceptTest.Checked)
                Verification = true;
            if (chkRejection.Checked)
                Verification = true;
            if (chkOthers.Checked)
                Verification = true;

            if (PatientInformation)
            {
                if (lblFirstName.Text == ""&&ltrLocation.Text == "" || ltrGuardianAddress.Text == "" && ltrAgeGender.Text == "")
                    {
                    errormessage = "<li> Patient information is missing ! <br>";
                   }
                else
                {
                    errormessage += "<li>Patient / Guardian";
                    if (ltrLocation.Text == "" && ltrGuardianAddress.Text == "")
                      errormessage += " Location,";
                    else if(ltrLocation.Text != "" || ltrGuardianAddress.Text != "")
                    {
                        if(ltrLocation.Text != "")
                        {
                            if (txtstreet.Text == "")
                                errormessage += " Street,";
                            if (txtCity.Text == "")
                                errormessage += " City,";
                            if (txtState.Text == "")
                                errormessage += " State,";
                            if (txtCountry.Text == "")
                                errormessage += " Country,";
                            if (txtZip.Text == "")
                                errormessage += " Zipcode,";
                        }
                        else if(ltrGuardianAddress.Text != "")
                        {
                            if (txtGuardianStreet.Text == "")
                                errormessage += " Street,";
                            if (txtGuardianCity.Text == "")
                                errormessage += " City,";
                            if (txtGuardianState.Text == "")
                                errormessage += " State,";
                            if (txtGuardianCountry.Text == "")
                                errormessage += " Country,";
                            if (txtGuardianZip.Text == "")
                                errormessage += " Zipcode,";
                        }
                    }
                    errormessage = errormessage.Remove(errormessage.Length - 1);
                    errormessage += " is missing ! <br>";
                }
            }
                     
            if (PhysicianInformation)
            {
                if (lblPhyName.Text == ""&&lblFacility.Text == ""&&lblPhyEmail.Text == ""&&ltrReqAddress.Text == ""&&ltrReqContact.Text == ""&&ltrFaxNumber.Text == "")
                {
                    errormessage += "<li>Physician information is missing ! <br>";
                }
                else
                {
                    errormessage += "<li>Physician";
                    if (lblPhyName.Text == "")
                        errormessage += " Name,";
                    if (lblFacility.Text == "")
                        errormessage += " Facility,";
                    if (lblPhyEmail.Text == "")
                        errormessage += " Email ID,";
                    if (ltrReqAddress.Text == "")
                        errormessage += " Address,";
                    if (ltrReqContact.Text == "")
                        errormessage += " Contact,";
                    //if (ltrFaxNumber.Text == "")
                    //    errormessage += " Fax Number,";
                    errormessage = errormessage.Remove(errormessage.Length - 1);
                    errormessage += " is missing ! <br>";
                }
            }
               
            if (SpecimenInformation)
            { 
                if(lblSpecimenNumber.Text == ""&&lblTestType.Text == ""&&lbltype.Text == ""&&lblReceivedOn.Text == ""&&lbldrawnDatetime.Text == ""&& lblTransitTime.Text == ""&&lblTransitTemp.Text == ""&&lblVolRec.Text == "" )
                {
                    if (Convert.ToInt32(Session["OrgID"]) == 2)
                    {
                       errormessage += "<li>Specimen information is missing !<br>";
                    }
                    else
                    {
                        errormessage += "<li>Specimen information is missing !<br>";
                    }
                }
                else
                {
                    errormessage += "<li>Specimen";
                  
                    if (lblSpecimenNumber.Text == "")
                        errormessage += " Name,";
                    if (lblTestType.Text == "")
                        errormessage += " Test Type,";
                    if (lbltype.Text == "")
                        errormessage += " Specimen Type,";
                    if (lblReceivedOn.Text == "")
                        errormessage += " Received On,";
                    if (lbldrawnDatetime.Text == "")
                        errormessage += " Drawn Datetime,";
                    if (lblTransitTime.Text == "")
                        errormessage += " Transit Time,";
                    if (lblTransitTemp.Text == "")
                        errormessage += " Transit Temperature,";
                    if (lblVolRec.Text == "")
                        errormessage += " Vol.Received,";
                    //if (Convert.ToInt32(Session["OrgID"]) == 2)
                    //{
                    //    if (lblBloodType.Text == "")
                    //        errormessage += " Blood Type,";
                    //}
                    errormessage = errormessage.Remove(errormessage.Length - 1);
                    errormessage += " is missing ! <br>";
                }
            }

            if (Verification)
            {
                if (!chkConsentProvided.Checked && !chkAcceptTest.Checked && !chkRejection.Checked && !chkOthers.Checked)
                { errormessage += "<li>Verification is missing ! ";
                }
                else
                {
                    errormessage += "<li>Following verification is missing!<br> ";
                    if (!chkConsentProvided.Checked)
                        errormessage += "<ul><li>Consent is not provided</li></ul>";
                    if (!chkAcceptTest.Checked)
                        errormessage += "<ul><li>Still Specimen is not accepted for testing</li></ul>";
                                       
                    if (chkRejection.Checked)
                        errormessage += " <ul><li>Specimen is rejection</li></ul> ";
                    if (chkOthers.Checked)
                        errormessage += " <ul><li> Specimen has some exception</li></ul> ";
                }
            }

            ltrWarning.Text = errormessage;

            if (ltrWarning.Text == "" && ltrCurrentSpecimenStatus.Text== "Received")
            {
                if (chkConsentProvided.Checked == true && chkAcceptTest.Checked == true)
                {
                    string status = "Ready for Assay";
                    if (Convert.ToInt32(Session["OrgID"]) == 2)
                        dvAssay.Visible = true;
                    else
                        dvAssay.Visible = false;
                    btnSave.Visible = true;
                    ltrCurrentSpecimenStatus.Text = status;
                    int rtnPatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                    {
                        SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                        TenantID = Convert.ToInt32(Session["OrgID"]),
                        IsConsent = chkConsentProvided.Checked,
                        IsRejection = chkRejection.Checked,
                        IsSpecimenAccept = chkAcceptTest.Checked,
                        SpecimenStatus = status,
                        RejectReasons = "",
                        PendingReasons = "",
                        ReactivateReason = "",
                    });                    
                }                
            }
            return errormessage;
        }

        public void SaveAuditLog(string SpecimenStatus)
        {
           AuditLogBO AuditLog = new AuditLogBO();
            {
                AuditLog.EntityType = "Specimen";
                AuditLog.EntityID = Convert.ToInt32(hSpecimenID.Value);
                AuditLog.ActionName = SpecimenStatus;
                AuditLog.ActionBy = Session["FullName"].ToString();
            }
            AuditLogDA specimen = new AuditLogDA();

            specimen.SaveSpecimenAuditLog(AuditLog);
        }

        private void EmailSent()
        {
            if(ltrCurrentSpecimenStatus.Text == "Ready for Assay")
            {
                int emailEnableID = 0;
                if (Convert.ToInt32(Session["OrgID"]) == 2)
                    emailEnableID = 6;
                else if (Convert.ToInt32(Session["OrgID"]) == 4)
                    emailEnableID = 5;

                List<EmailEnablementImplementationBO> emailBO = new EmailConfigurationDA().emailEnableImplementation(Convert.ToInt32(Session["OrgID"]), emailEnableID);

                if (emailBO[0].emailEnablementBO.isToTenant == true)
                {
                    emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@OrderingParty", lblPhyName.Text.Trim());
                    emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@TestType", lblTestType.Text);
                    emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                    emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@SpecimenNumber", ltrSpecimenNumber.Text.Trim());
                    emailBO[0].emailEnablementTypeBO.TenantTemplate = emailBO[0].emailEnablementTypeBO.TenantTemplate.Replace("@SampleType", lbltype.Text.Trim());

                    EmailConfigBO ema = new EmailConfigBO();
                    ema.Body = emailBO[0].emailEnablementTypeBO.TenantTemplate;
                    ema.Subject = "Specimen Order reception _ " + ltrSpecimenNumber.Text;
                    ema.ToAddress = emailBO[0].emailEnablementBO.ToTenantEmails;
                    new STOMS.UI.CommonCS.SendEmail(ema);
                    ltrEmailErrormsg.Text = "";
                    ltrEmailSuccessmsg.Text = "Email Sent Successfully";
                }
            }
        }

        private List<AssayGroupBO> GetAssayDetails()
        {
            List<AssayGroupBO> assayDet = new SpecimenDA().GetAssaySpecimens(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value));
            return assayDet;
        }

        private void setTestType()
        {
            if (Convert.ToInt32(Session["OrgID"]) == 2)
            {
                ddlRequestedTest.SelectedValue = "Frat";
                ddlRequestedTest.Items.Remove(new ListItem()
                {
                    Text = "Mitochondrial Dysfunction Test",
                    Value = "Mitochondrial Dysfunction Test",
                });
            }
            else if (Convert.ToInt32(Session["OrgID"]) == 4)
            {
                ddlRequestedTest.SelectedValue = "Mitochondrial Dysfunction Test";
                ddlRequestedTest.Items.Remove(new ListItem()
                {
                    Text = "FRAT",
                    Value = "Frat",
                });
            }
        }

        private void clearFields()
        {
            txtFirstName.Text = ""; txtGuardianFirstName.Text = "";
            txtLastName.Text = "";  txtGuardianLastName.Text = "";
            txtstreet.Text = "";    txtGuardianStreet.Text = "";
            txtCity.Text = "";      txtGuardianCity.Text = "";
            txtState.Text = "";     txtGuardianState.Text = "";
            txtZip.Text = "";       txtGuardianZip.Text = "";
            txtCountry.Text = "";   txtGuardianCountry.Text = "";
            ddlGRelationship.SelectedValue = "0";
        }

        private void Reasion()
        {
            if (ltrCurrentSpecimenStatus.Text == "Rejected")
            {
                dvPendingReasion.Visible = false;
                dvRejectionReasion.Visible = true;
                //dvother.Visible = false;
                btnSVVerificationInfo.Visible = false;
                chkConsentProvided.Disabled = true;
                chkRejection.Disabled = true;
                chkAcceptTest.Disabled = true;
                chkOthers.Disabled = true;
                txtPendingOther.Enabled = false;
                dvRejectionReasion.Disabled = false;
                dvPendingtoRec.Visible = false;
                lbtnReasonUpdate.Visible = false;
                txtOtherRejectReason.Enabled = false;
               // ddlRejectReason.Enabled = true;
                    }
            //if (ltrCurrentSpecimenStatus.Text == "Pending")
            //{
            //    dvPendingReasion.Visible = true;
            //    dvRejectionReasion.Visible = false;
            //    chkConsentProvided.Disabled = true;
            //    chkRejection.Disabled = true;
            //    chkAcceptTest.Disabled = true;
            //    chkOthers.Disabled = true;
            //    txtPendingOther.Enabled = false;
            //    dvRejectionReasion.Visible = false;
            //    dvPendingtoRec.Visible = false;
            //    lbtnReasonUpdate.Visible = false;
            //}
            if (ltrCurrentSpecimenStatus.Text == "Received")
            {
                dvPendingtoRec.Visible = false;
                lbtnReasonUpdate.Visible = false;
                }
            if(ltrCurrentSpecimenStatus.Text == "Ready for Assay")
            {
                chkConsentProvided.Disabled = true;
                chkRejection.Disabled = true;
                chkAcceptTest.Disabled = true;
                chkOthers.Disabled = true;
                dvPendingtoRec.Visible = false;
                lbtnReasonUpdate.Visible = false;
                btnSVVerificationInfo.Visible=false;
                dvAlertWarning.Visible = false;


            }
            if (ltrCurrentSpecimenStatus.Text == "Assigned to Assay")
            {
                dvAlertWarning.Visible = false;
                chkConsentProvided.Disabled = true;
                chkRejection.Disabled = true;
                chkAcceptTest.Disabled = true;
                chkOthers.Disabled = true;
                dvPendingtoRec.Visible = false;
                lbtnReasonUpdate.Visible = false;
                btnSVVerificationInfo.Visible = false;
            }
            if(ltrCurrentSpecimenStatus.Text == "Result Recorded")
            {
                chkConsentProvided.Disabled = true;
                chkRejection.Disabled = true;
                chkAcceptTest.Disabled = true;
                chkOthers.Disabled = true;
                dvPendingtoRec.Visible = false;
                lbtnReasonUpdate.Visible = false;
                btnSVVerificationInfo.Visible = false;
                dvAlertWarning.Visible = false;
            }
            // else
            // {
            //     //btnSVVerificationInfo.Visible = false;
            //     chkConsentProvided.Disabled = false;
            //     chkRequisition.Disabled = false;
            //     chkAcceptTest.Disabled = false;
            //     chkOthers.Disabled = false;
            //     dvother.Visible = true;
            //     ddlRejectReason.Visible = true;
            //     dvPendingtoRec.Visible = true;
            //}
        }

        private void TenantConfiguration()
        {
            //Load Specimen Type Dymanically
            ListItemCollection Fratcollection = new ListItemCollection()
            {
                new ListItem("Choose Specimen","Choose",false),
                    new ListItem("Blood","Blood"),
                    new ListItem("Serum","Serum"),
                    new ListItem("Plasma","Plasma"),
                    new ListItem("Unknown","Unknown")
            };
            ListItemCollection Swabcollection = new ListItemCollection()
            {
                new ListItem("Choose Specimen","Choose",false),
                    new ListItem("Mouth Swab","Mouth Swab"),
            };
            ListItemCollection IliadassayType = new ListItemCollection()
            {
                new ListItem("Blocking"),
                new ListItem("Binding"),
                new ListItem("Blocking & Binding")
            };
            if (Convert.ToInt32(Session["OrgID"]) == 2)
            {
                ddlSpecimenType.DataSource = Fratcollection;
                ddlSpecimenType.DataBind();
                ddlRemSpecimenType.DataSource = Fratcollection;
                ddlRemSpecimenType.DataBind();
                ddlAssayType.DataSource = IliadassayType;
                ddlAssayType.DataBind();
                viewVolReceived.InnerText = "Vol.Received";
                txtVolReceived.Attributes.Add("placeholder", "Vol.Received");
                dvDrawnDate.InnerText = "Drawn Date";
                //viewBloodType.Visible = true;
                editDrawnDate.InnerText = "Drawn Date";
                //dvBloodType.Visible = true;
                ancRecResults.Visible = false;
                dvResultsPre.Visible = false;
            }
            else if (Convert.ToInt32(Session["OrgID"]) == 4)
            {
                ddlSpecimenType.DataSource = Swabcollection;
                ddlSpecimenType.DataBind();
                dvDrawnDate.InnerText = "Swab Date";
                txtVolReceived.Attributes.Add("placeholder", "Swab Count");
                viewVolReceived.InnerText = "Swab Count";
                editDrawnDate.InnerText = "Swab Date";
                //viewBloodType.Visible = false;
                //dvBloodType.Visible = false;
                dvResultView.Visible = false;
                dvResultMsg.Visible = false;
                dvResultEdit.Visible = false;
                aresults.Visible = false;
                dvResultsPre.Visible = true;
                //ancRecResults.Visible = false;
            }
        }

        private void TestResults()
        {
            TestResultsBO testBO = new TestResultsDA().getResults(Convert.ToInt32(Session["OrgID"]), int.Parse(hSpecimenID.Value));

            //hResultsPreview.Value = testBO.ResultID.ToString();
            if (testBO.ResultID>0)
            {
                ancRecResults.InnerText = "View Results";
                dvResultsPre.Visible = true;
            }
            else
            {
                ancRecResults.InnerText = "Click here for recording results";
                dvResultsPre.Visible = false;
            }
            if (testBO.ResultDocID > 0)
            {
                hDocID.Value = testBO.ResultDocID.ToString();
                DocumentBO documentBO = new ReportDA().ViewhardCopy(Convert.ToInt32(hDocID.Value));
                hDocNumber.Value = documentBO.DocNumber;
                // btnMitPreview.PostBackUrl = "/Docs/Results/" + hDocNumber.Value + ".pdf";
                string filesource = @"\Docs\Results\" + documentBO.DocNumber + "." + "pdf";
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                aMitPreview.HRef = url;
                aPreview.HRef = url;
                aPreview.Visible = true;
                aMitPreview.Visible = true;
                BtnMitDownload.Visible = true;
                lbtnDownload.Visible = true;
            }
        }

        private void fileuploadcopy()
        {
            btnFileClose.Visible = false;
            if (ancViewCopy.InnerText == "")
            {
                dvUploadFile.Visible = true;
            }
            else
            {
                btnFileClose.Visible = true;
                dvUploadFile.Visible = false;
            }
        }

        //private void UserGroupPermission()
        //{
        //    List<FunctionBO> fbo = new KoSoft.Entitlement.KoAccess(STOMS.Common.Constant.DBConnectionString).getUserFunctions(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Constant.ProductID));
        //    foreach (var item in fbo)
        //    {
        //        if (ReviewAndUpdate == item.entServBO.ServiceID)
        //        {
        //            if (item.WriteAccess == true)
        //                ReviewAndUpdateWriteAccess = true;

        //            else if (item.ExecuteAccess == true)
        //                ReviewAndUpdateExecuteAccess = true;
        //        }

        //        if (ReviewAndUpdateWriteAccess == false)
        //        {
        //            aModalPatientInfo.Visible = false;
        //            aReqPhysicianDetails.Visible = false;
        //            aSpecimenInformation.Visible = false;
        //        }
        //        else
        //        {
        //            aModalPatientInfo.Visible = true;
        //            aReqPhysicianDetails.Visible = true;
        //            aSpecimenInformation.Visible = true;
        //        }
        //    }
        //}

        private string  convertMonthDate(string date)
        {
            string returnValue = "";
            if (date != "" && date !=null)
            {
               // if (!date.Contains("/"))
               // {
                    //var drDate = DateTime.Parse(date).ToShortDateString();
                    //string[] arr = drDate.Split('-');
                    //returnValue = arr[1].ToString() + "/" + arr[0].ToString() + "/" + arr[2].ToString();
                    var drDate = DateTime.Parse(date).ToShortDateString();
                    returnValue = DateTime.Parse(drDate).Month + "/" + DateTime.Parse(drDate).Day + "/" + DateTime.Parse(drDate).Year;                   
               // }
               // else
                 //   returnValue= date;
            }
            return returnValue;
        }

        private void popSpecimen()
        {
            clearCashView();
            clearCreditCardView();
            clearChequeView();
            clearInsuranceView();
            List<SpecimenInfoBO> oSpecimen = new List<SpecimenInfoBO>();
            if (hAssayID.Value != "")
                oSpecimen = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(hSpecimenID.Value), Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hAssayID.Value));
            else
                oSpecimen = (new SpecimenDA()).GetSpecimenInfo(Convert.ToInt32(hSpecimenID.Value),Convert.ToInt32(Session["OrgID"]));
            Session.Remove("SpecimenID");
            if (oSpecimen.Count > 0)
            {   
                ltrSpecimenNumber.Text = lblSpecimenNumber.Text = oSpecimen[0].SpecimenNumber;
                // Patient Info
                ltrFirstName.Text = txtFirstName.Text = lblFirstName.Text = oSpecimen[0].oPatient.FirstName;
                ltrLastName.Text = txtLastName.Text = lblLastName.Text = oSpecimen[0].oPatient.LastName;
                ltrAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + oSpecimen[0].oPatient.DOB;
                optF.Checked = (oSpecimen[0].oPatient.Gender == "F" ? true : false);
                optM.Checked = (oSpecimen[0].oPatient.Gender == "M" ? true : false);
                optUn.Checked = (oSpecimen[0].oPatient.Gender == "Unknown" ? true : false);
                txtDOB.Text = oSpecimen[0].oPatient.DOB;
                ltrLocation.Text = oSpecimen[0].oPatient.Street + (oSpecimen[0].oPatient.City == "" ? "" : " ," + oSpecimen[0].oPatient.City) + (oSpecimen[0].oPatient.State == "" ? "" : " ," + oSpecimen[0].oPatient.State) + (oSpecimen[0].oPatient.Zip == "" ? "" : ", " + oSpecimen[0].oPatient.Zip) + (oSpecimen[0].oPatient.Country == "" ? "" : ", " + oSpecimen[0].oPatient.Country);
                ltrEmailID.Text = oSpecimen[0].oPatient.EmailID;
                ltrContactNo.Text = oSpecimen[0].oPatient.ContactNo;
                ltrGuardianContactNo.Text = oSpecimen[0].oPatient.GuardianContactNo;
                ltrGuardianEmailID.Text = oSpecimen[0].oPatient.GuardianEmailID;
                txtEmailID.Text = oSpecimen[0].oPatient.EmailID;
                txtContactNo.Text = oSpecimen[0].oPatient.ContactNo;
                txtGuardianContactNo.Text = oSpecimen[0].oPatient.GuardianContactNo;
                txtGuardianEmailID.Text = oSpecimen[0].oPatient.GuardianEmailID;
                txtstreet.Text = oSpecimen[0].oPatient.Street;
                txtCity.Text = oSpecimen[0].oPatient.City;
                txtState.Text = oSpecimen[0].oPatient.State;
                txtZip.Text = oSpecimen[0].oPatient.Zip;
                txtCountry.Text = oSpecimen[0].oPatient.Country;
                ddlSpecimenType.SelectedValue = oSpecimen[0].SpecimentType != "" ? oSpecimen[0].SpecimentType : "Choose Specimen";

                if(oSpecimen[0].oPatient.LastName==""&& oSpecimen[0].oPatient.DOB=="")
                {
                    lblPatientNamecode.InnerText = "Patient Code";
                    dvPatientNaCo.InnerText = "Patient Code";
                    bFirstNameCode.InnerText= "Patient Code";
                    chkClosePatient.Checked = true;
                }
                else
                {
                    lblPatientNamecode.InnerText = "Patient Name";
                    dvPatientNaCo.InnerText = "Patient Name";
                    bFirstNameCode.InnerText = "Patient Name";
                    chkClosePatient.Checked = false;
                }


                //age
                if (oSpecimen[0].oPatient.DOB != "") {
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
                    lblAgeGender.Text = "-";
                }
               //age
                string input = oSpecimen[0].RejectReasons;

                // Get first three characters
                string[] sub = input.Split(',');
                foreach (string s in sub)
                {
                    foreach (ListItem i  in ddlRejectReason.Items)
                    {
                        if (s == i.Value)
                        {
                            i.Selected = true;
                            txtOtherRejectReason.Visible = false;
                        }
                        else if (s.Contains("Others: "))
                        {
                            if (i.Value == "Others")
                                i.Selected = true;
                            txtOtherRejectReason.Text = s.Remove(0, 8);
                            txtOtherRejectReason.Visible = true;
                        }
                    }
                }
                
                if (oSpecimen[0].SpecimenStatus== "Rejected")
                {
                    string input1 = oSpecimen[0].RejectReasons;

                    if (oSpecimen[0].RejectReasons.Contains("Others:"))
                    {
                        string[] result = input1.Split(new string[] { "Others:" }, StringSplitOptions.None);
                        foreach (string s in result)
                            lblRejectionReasion.Text = result[0].ToString();
                            lblRejectionReasion.Text += result[1].ToString();
                        lblRejectionReasion.Text = lblRejectionReasion.Text.Remove(lblRejectionReasion.Text.Length - 1);
                    }
                    else
                    {
                        string[] result = input1.Split(new string[] { "Others:" }, StringSplitOptions.None);
                        foreach (string s in result)
                        lblRejectionReasion.Text = result[0].ToString();
                        lblRejectionReasion.Text = lblRejectionReasion.Text;
                    }
                }
                
                if (oSpecimen[0].PendingReasons !="")
                {
                    dvPendingReasion.Visible = true;
                    lblExceptionReasion.Text = "<li>" + oSpecimen[0].PendingReasons + "</li>";
                    chkOthers.Checked = true;
                    //txtPendingOthers.Text = "<ul><li>"+oSpecimen[0].PendingReasons+"</li> </ul>";
                    txtPendingOther.Text = oSpecimen[0].PendingReasons;
                }

                //if (oSpecimen[0].IsConsent == false)
                //{
                //    lblPendingReasion.Text = "<ul><li>Consent Provided is not Completed</li></ul> ";

                //    //if (oSpecimen[0].IsReqComplete == false)
                //    //{
                //    //    lblPendingReasion.Text += "<ul><li>Requisition Complete is not Completed</li></ul>";
                //    //}
                //    if (oSpecimen[0].PendingReasons != "")
                //    {
                //        lblPendingReasion.Text += "<ul><li>" + oSpecimen[0].PendingReasons + "</li> </ul>";
                //        chkOthers.Checked = true;
                //        txtPendingOther.Text = oSpecimen[0].PendingReasons;
                //    }
                //}

                //if (oSpecimen[0].IsReqComplete == false)
                //{
                //    lblPendingReasion.Text = "<ul><li>Requisition Complete is not Completed</li></ul>";

                //    if (oSpecimen[0].IsConsent == false)
                //    {
                //        lblPendingReasion.Text += "<ul><li>Consent Provided is not Completed</li></ul>";
                //    }

                //    if (oSpecimen[0].PendingReasons != "")
                //    {
                //        lblPendingReasion.Text += "<ul><li>" + oSpecimen[0].PendingReasons + "</li></ul>";
                //        chkOthers.Checked = true;
                //        txtPendingOthers.Text = oSpecimen[0].PendingReasons;
                //    }
                //}

                //if (oSpecimen[0].IsConsent == false && oSpecimen[0].IsReqComplete == false)
                //{
                //    lblPendingReasion.Text = "<ul><li>Consent Provided is not Completed</li></ul>" + "<ul><li>Requisition Complete</li></ul>";
                //}
                if (oSpecimen[0].SampleReceiveDate != "1/1/1900" && oSpecimen[0].SampleReceiveDate !="")
                {
                     //lblAgeGender.Text = (DateTime.Parse(oSpecimen[0].SampleReceiveDate) - DateTime.Parse(oSpecimen[0].oPatient.DOB)).ToString(); 
                }
                
                if (oSpecimen[0].oPatient.GuardianRelationship != "" && oSpecimen[0].oPatient.GuardianRelationship != "0")
                {
                    ddlGRelationship.SelectedValue = oSpecimen[0].oPatient.GuardianRelationship;
                    lblGRelationship.Text = oSpecimen[0].oPatient.GuardianRelationship;
                }

                else
                {
                    ddlGRelationship.SelectedValue = "0";
                    lblGRelationship.Text = "";
                }
                   
                //Verification Info
                if (oSpecimen[0].PaymentMode != "")
                {
                    hPaymentMode.Value = ddlPaymentMode.SelectedValue = oSpecimen[0].PaymentMode.Trim();
                    PaymentType(oSpecimen[0].PaymentMode.Trim());
                } 
                
                chkAcceptTest.Checked = oSpecimen[0].IsSpecimenAccept;
                chkConsentProvided.Checked = oSpecimen[0].IsConsent;
                chkRejection.Checked = oSpecimen[0].IsRejection;
                //Temporary condition
                if (chkRejection.Checked)
                {
                    ltrCurrentSpecimenStatus.Text ="Rejected";
                    //dvRejectReason.Visible = true;
                }
                    
                string otherinput = oSpecimen[0].RejectReasons;

                // Get first three characters
                string[] othetsub = otherinput.Split(',');
                foreach (string s in othetsub)
                {
                    foreach (ListItem i in ddlRejectReason.Items)
                    {
                        if (s == i.Value)
                        {
                            i.Selected = true;
                            txtOtherRejectReason.Visible = false;
                        }
                        else if (s.Contains("Others: "))
                        {
                            if (i.Value == "Others")
                                i.Selected = true;
                            txtOtherRejectReason.Text = s.Remove(0, 8);
                            //lblRejectionReasion.Text = txtOtherRejectReason.Text;
                            txtOtherRejectReason.Visible = true;
                        }
                    }
                }

                //if (oSpecimen[0].RejectReasons.Contains(',')){
                //    string str = oSpecimen[0].RejectReasons;
                //    string[] RejectReasons = str.Split(',');
                //    foreach (var item in RejectReasons)
                //    {
                //    }
                //}
                //else
                //{
                //    ddlRejectReason.SelectedValue = oSpecimen[0].RejectReasons;
                //}

                //if (oSpecimen[0].IsConsent)
                //{
                //    YesConsent.Visible = true;
                //    NoConsent.Visible = false;
                //    chkConsentProvided.Checked = true;
                //}
                //else
                //{
                //    YesConsent.Visible = false;
                //    NoConsent.Visible = true;
                //    chkConsentProvided.Checked = false;
                //}

                //if (oSpecimen[0].IsReqComplete)
                //{
                //    YesReqComp.Visible = true;
                //    NoReqComp.Visible = false;
                //    chkRequisition.Checked = true;
                //}
                //else
                //{
                //    YesReqComp.Visible = false;
                //    NoReqComp.Visible = true;
                //    chkRequisition.Checked = false;
                //}

                //if (oSpecimen[0].IsSpecimenAccept)
                //{
                //    YesAccept.Visible = true;
                //    NoAccept.Visible = false;
                //    chkAcceptTest.Checked = true;
                //}
                //else
                //{
                //    YesAccept.Visible = false;
                //    NoAccept.Visible = true;
                //    chkAcceptTest.Checked = false;
                //}
                //lblReasonRej.Text = oSpecimen[0].RejectReasons;

                //Guardian Info
                txtGuardianFirstName.Text = oSpecimen[0].oPatient.GuardianFirstName;
                txtGuardianLastName.Text = oSpecimen[0].oPatient.GuardianLastName;
                txtGuardianStreet.Text = oSpecimen[0].oPatient.GuardianStreet;
                txtGuardianCity.Text = oSpecimen[0].oPatient.GuardianCity;
                txtGuardianState.Text = oSpecimen[0].oPatient.GuardianState;
                txtGuardianCountry.Text = oSpecimen[0].oPatient.GuardianCountry;
                txtGuardianZip.Text = oSpecimen[0].oPatient.GuardianZip;
                ltrGuardianName.Text = oSpecimen[0].oPatient.GuardianFirstName + " " + oSpecimen[0].oPatient.GuardianLastName;
                ltrGuardianAddress.Text = oSpecimen[0].oPatient.GuardianStreet + (oSpecimen[0].oPatient.GuardianCity == "" ? "" : " ," + oSpecimen[0].oPatient.GuardianCity) + (oSpecimen[0].oPatient.GuardianState == "" ? "" : " ," + oSpecimen[0].oPatient.GuardianState) + (oSpecimen[0].oPatient.GuardianZip == "" ? "" : ", " + oSpecimen[0].oPatient.GuardianZip) + (oSpecimen[0].oPatient.GuardianCountry == "" ? "" : ", " + oSpecimen[0].oPatient.GuardianCountry);
                if (oSpecimen[0].oPatient.GuardianRelationship != "" && oSpecimen[0].oPatient.GuardianRelationship != "0")
                    lblGRelationship.Text = oSpecimen[0].oPatient.GuardianRelationship;
                hPatientID.Value = Convert.ToString(oSpecimen[0].oPatient.PatientID);
                lbldrawnDatetime.Text = convertMonthDate(oSpecimen[0].DateDrawn) + "/" + oSpecimen[0].TimeDrawn;
                lblReceivedOn.Text = convertMonthDate(oSpecimen[0].SampleReceiveDate) + "/" + oSpecimen[0].ReceivedTime;
                lblTransitTime.Text = oSpecimen[0].TransitTime;
                if(oSpecimen[0].TransitTemperature!="")
                    lblTransitTemp.Text = oSpecimen[0].TransitTemperature;
                lblVolRec.Text = oSpecimen[0].VolumeReceived;
                lblPotInter.Text = oSpecimen[0].InterSubstance;
                lblRemainType.Text = oSpecimen[0].SpecimentType;
                if (oSpecimen[0].SpecimentType!="" && oSpecimen[0].SpecimentType != null)     
                    lbltype.Text = ddlSpecimenType.SelectedValue = oSpecimen[0].SpecimentType.Trim();
                lblTestType.Text = oSpecimen[0].TestType;

                //Specimen Information
                txtDrawndate.Text = convertMonthDate(oSpecimen[0].DateDrawn);
                txtDrawnTime.Text = oSpecimen[0].TimeDrawn;
                txtTransitTime.Text = oSpecimen[0].TransitTime;
                if(oSpecimen[0].TransitTemperature!="")
                ddlViewTransitTemp.Text = oSpecimen[0].TransitTemperature;
                txtVolReceived.Text = oSpecimen[0].VolumeReceived;
                txtPotInterfer.Text = oSpecimen[0].InterSubstance;
                //rdSpecimenserum.Checked = (oSpecimen[0].SpecimentType == "Serum" ? true : false);
                //rdSpecimenblood.Checked = (oSpecimen[0].SpecimentType == "Blood" ? true : false);                                 
                txtReceivedOn.Text = convertMonthDate(oSpecimen[0].SampleReceiveDate);
                txtReceivedTime.Text = oSpecimen[0].ReceivedTime;
                //if(Convert.ToInt32(Session["OrgID"])==2)
                 //lblBloodType.Text = oSpecimen[0].BloodType;
                //ddSpecimentype.SelectedIndex = ddSpecimentype.Items.IndexOf(ddSpecimentype.Items.FindByText(oSpecimen[0].BloodType));
                ltrCurrentSpecimenStatus.Text = lblCurrentSpecimenStatus.Text = oSpecimen[0].SpecimenStatus;
                if (oSpecimen[0].SpecimenStatus != "Received" && oSpecimen[0].SpecimenStatus != "Rejected" && Convert.ToInt32(Session["OrgID"]) == 2)
                    dvAssay.Visible = true;
                if (oSpecimen[0].SpecimenStatus == "Ready for Assay")
                    btnSave.Visible = true;
                
                if (oSpecimen[0].CustomerID != 0)
                {
                    hCustID.Value = Convert.ToString(oSpecimen[0].CustomerID);
                    List<CustomerBO> oCust = (new CustomerDA()).GetCustomer(hCustID.Value);
                    if (oCust.Count > 0)
                    {
                      txtPhyName.Text = oCust[0].CustomerName != "" ? oCust[0].CustomerName : "-";
                        ltrPhyName.Text ="Dr"+" "+oCust[0].CustomerName + " " + oCust[0].Specialization; 
                        lblPhyName.Text="Dr"+" "+oCust[0].CustomerName+" " + oCust[0].Specialization;
                        txtPhyAddress1.Text = oCust[0].Address1;
                        txtSpecialization.Text = oCust[0].Specialization;
                        txtPhyCity.Text = oCust[0].City;
                        lblPhyEmail.Text = oCust[0].Email;
                        txtPhyState.Text = oCust[0].State;
                        txtPhyPCode.Text = oCust[0].Zipcode;
                        ltrFacility.Text = txtPhyFacility.Text = lblFacility.Text = oCust[0].Facility;
                        ltrReqAddress.Text = txtPhyAddress1.Text + ", " + txtPhyCity.Text+", "+txtPhyState.Text+", "+ ddCountry.SelectedValue+", "+ txtPhyPCode.Text;
                        txtPhyPhone.Text = oCust[0].Phone;
                        txtPhyEmail.Text = oCust[0].Email;
                        txtPhyFax.Text = oCust[0].Fax;
                        ltrReqContact.Text = txtPhyPhone.Text;
                        ltrFaxNumber.Text = txtPhyFax.Text;
                        // lblDiagnosis.Text = txtDiagnosis.Text = oCust[0].Diagnosis;
                        //lblDiagnosisCode.Text = txtDiagnosisCode.Text = oCust[0].DiagnosisCode;
                        //lblResultType.Text = ddlResultType.SelectedValue = oCust[0].ResultType;
                    }
                }
                if (hAssayID.Value != "" && hAssayID.Value != "0")
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

                    //if (oSpecimen[0].oResult.SubSpecimenType != "")
                        //ddBloodType.SelectedValue = oSpecimen[0].oResult.SubSpecimenType;

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
                        hResultDocID.Value = oSpecimen[0].oResult.ResultDocID.ToString();
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
                    //if (oSpecimen[0].oResult.SpecimenType == "Serum")
                    //{
                    //    optRemainSerum.Checked = true;
                    //    optRemainBlood.Checked = false;
                    //}
                    if (oSpecimen[0].oResult.SpecimenType != "" || oSpecimen[0].oResult.SpecimenType != null)
                        ddlRemSpecimenType.SelectedValue = oSpecimen[0].oResult.SpecimenType.Trim();

                    hAssaySpecimenID.Value = Convert.ToString(oSpecimen[0].oResult.AssaySpecimenID);
                }
                //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(oSpecimen[0].SpecimenStatus);
                //ltrStatus.Text = Common.Constant.GetStatusFormat(oSpecimen[0].SpecimenStatus);
                hSpecimenStatus.Value = oSpecimen[0].SpecimenStatus;
                SetResultSection(oSpecimen[0].SpecimenStatus);
                if (oSpecimen[0].oResult != null && oSpecimen[0].oResult.AssayStatus != string.Empty)
                    SetResultSection(oSpecimen[0].oResult.AssayStatus);

                if ((hAssayID.Value == "" || hAssayID.Value == Convert.ToString(0)))
                {
                    dvResCap.Visible = true;
                    dvResultView.Visible = false;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Assay Info is Empty.Please load Specimen Information from Assay list.";
                    lnkGenRpt.Visible = false;
                }
                if (Convert.ToInt32(Session["OrgID"]) == 4)
                {
                    aresults.Visible = false;
                    dvResultMsg.Visible = false;
                }  
                DocumentBO documentBO = new ReportDA().ViewhardCopy(oSpecimen[0].ReqFormCopyID);
                ancViewCopy.InnerText = documentBO.DocNumber;
                string filesource = @"\Docs\RequestForm\" + documentBO.DocNumber + "." + documentBO.DocType;
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                ancViewCopy.HRef = url;
                fileuploadcopy();
                //if (oSpecimen[0].ReqFormCopyID !=0)
                //{
                //    dvViewFile.Visible = true;
                //}
            }
            else if(oSpecimen.Count ==0)
            {
                if (hAssayID.Value != "")
                {
                    lblSpecimenNum.Text = "Invalid Assay Number ";
                }
                else
                {
                    lblSpecimenNum.Text = "Invalid Specimen Number ";
                }
                dvSpecimenDetail.Visible = false;
            }

            if(oSpecimen[0].PaymentMode != "")            
                EditPayment(oSpecimen[0].PaymentMode);
            else
            {
                EditPayment(ddlPaymentMode.SelectedValue.Trim());
            }

            PaymentBO payBO = GetPayment();
            hPaymentID.Value = payBO.PaymentID.ToString();
            if (payBO.PaymentID > 0)
            {
                ddlPaymentMode.SelectedValue = payBO.PaymentMode.Trim();
                ddlPaymentStaus.SelectedValue = lblPaymentStatus.Text = payBO.PaymentStatus.Trim();
                if (payBO.PaymentStatus != "Paid")
                    dvUpdate.Visible = true;
                else
                    dvUpdate.Visible = false;

                aPaymentInformation.Visible = true;                
                //btnUpdatePayment.Text = "Edit Details";
                switch (payBO.PaymentMode)
                {
                    case "Cash":
                        lblCash.Text = payBO.CashDetails.Currency;
                        lblCash.Text += " "+ payBO.CashDetails.Cash.ToString();
                        lblTransactionDate.Text = payBO.CashDetails.TransactionDate;
                        lblDescription.Text = payBO.CashDetails.Description;
                        dvCashView.Visible = true;
                        dvcashEdit.Visible = false;
                        break;

                    case "CreditCard":
                        lblCardType.Text = payBO.CreditDetails.CardType;
                        string num = KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.CardNumber.ToString());
                        string[] cardNum = num.Split(' ');
                        lblCardNo4.Text = cardNum[3].ToString();
                        lblHolderName.Text = payBO.CreditDetails.HolderName;
                        string cvv = KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.CVVNumber);
                        lblCVV.Text = "XXX";
                        lblExpireDate.Text = "MM/YY";/*KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.ExpireDate);*/
                        dvCreditCardView.Visible = true;
                        dvCreditCardEdit.Visible = false;
                        break;

                    case "Cheque":
                        lblBankName.Text = payBO.chequeDetails.BankName;
                        lblBranchName.Text = payBO.chequeDetails.BranchName;
                        lblCheqNo.Text = payBO.chequeDetails.ChequeNumber.ToString();
                        lblCheqDate.Text = payBO.chequeDetails.ChequeDate;
                        lblAcctNo.Text = payBO.chequeDetails.AccountNumber;
                        lblRoutNo.Text = payBO.chequeDetails.RoutingNumber;
                        dvChequeView.Visible = true;
                        dvChequeEdit.Visible = false;
                        break;

                    case "Insurance":
                        lblInsuranceType.Text = payBO.InsuranceDetails.InsuranceType;
                        lblInsuranceCompany.Text = payBO.InsuranceDetails.InsuranceCompany;
                        lblInsuranceNumber.Text = payBO.InsuranceDetails.InsuranceNumber;
                        lblMemberName.Text = payBO.InsuranceDetails.MemberName;
                        lblMemberShipNumber.Text = payBO.InsuranceDetails.MemberShipNumber;
                        lblGroupNumber.Text = payBO.InsuranceDetails.GroupNumber;
                        lblPreAuthCode.Text = payBO.InsuranceDetails.PreAuthCode;
                        dvInsuranceView.Visible = true;
                        dvInsuranceEdit.Visible = false;
                        break;

                    case "Free":
                        dvUpdate.Visible = false;
                        break;

                    case "Charge to physician office":
                        dvUpdate.Visible = false;
                        break;
                }
            }
            else
            {
                dvUpdate.Visible = true;
            }

            OrderBO orderBO = getOrderDetails();
            hOrderID.Value = orderBO.OrderID.ToString();
            if (orderBO.BillAddSameAs != "")
            {                
                txtBillStr.Text = orderBO.BillAddress1;
                txtBillCity.Text = orderBO.BillCity;
                txtBillState.Text = orderBO.BillState;
                txtBillCountry.Text = orderBO.BillCountry;
                txtBillZip.Text = orderBO.BillZipCode;
                if(orderBO.BillAddSameAs !=null && orderBO.BillAddSameAs.Trim()!="")
                {
                    hBillAddSameAs.Value = ddlBillAddress.SelectedValue = orderBO.BillAddSameAs.Trim();
                }
                   
                dvBillAddEdit.Visible = true;
                BillAddDisable();
                if(orderBO.BillAddSameAs == "Others")
                {
                    btnUpdateAdd.Text = "Edit";
                    btnUpdateAdd.Visible = true;
                }
            }

            //Assay Details    
            List<AssayGroupBO> assDet = GetAssayDetails();
            if(assDet.Count > 0)
            {
                rptAssayDetails.DataSource = assDet;
                rptAssayDetails.DataBind();
            }
            else
            {
                rptAssayDetails.DataSource = null;
                rptAssayDetails.DataBind();
            }

            if(assDet.Count > 2)
            {
                if ((assDet[0].AssayType == "Blocking" || assDet[0].AssayType == "Binding" || assDet[0].AssayType == "Blocking & Binding") && (assDet[1].AssayType == "Blocking" || assDet[1].AssayType == "Binding" || assDet[1].AssayType == "Blocking & Binding") && (assDet[2].AssayType == "Blocking" || assDet[2].AssayType == "Binding" || assDet[2].AssayType == "Blocking & Binding"))
                    btnSave.Visible = false;
            }
            else
                btnSave.Visible = true;
        }

        private void viewSpecimenAuditlog()
        {
            List<AuditLogBO> specimenLog = (new AuditLogDA()).getSpecimenAuditLogs(Convert.ToInt32(hSpecimenID.Value));
            rpSpecimenAuditlog.DataSource = specimenLog.OrderByDescending(x => x.ActionOn);
            rpSpecimenAuditlog.DataBind();
        }

        private void ActivePatient() 
        {
            dvPatientDetails.Attributes.Add("class", "in");
            dvPhysicianDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "out collapse");
            dvAssayDetails.Attributes.Add("class", "out collapse");
            dvBillingDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "out collapse");
            dvPatientDetails.Focus();
        }

        private void ActivePhysician()
        {
            dvPhysicianDetails.Attributes.Add("class", "in");
            dvPatientDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "out collapse");
            dvAssayDetails.Attributes.Add("class", "out collapse");
            dvBillingDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "out collapse");
            dvPhysicianDetails.Focus();
        }

        private void ActiveTestSpecimen()
        {
            dvPatientDetails.Attributes.Add("class", "out collapse");
            dvPhysicianDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "in");
            dvAssayDetails.Attributes.Add("class", "out collapse");
            dvBillingDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Focus();
        }

        private void ActiveAssay()
        {
            dvPatientDetails.Attributes.Add("class", "out collapse");
            dvPhysicianDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "out collapse");
            dvAssayDetails.Attributes.Add("class", "in");
            dvBillingDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Focus();
        }

        private void ActivePaymentMode()
        {
            dvPatientDetails.Attributes.Add("class", "out collapse");
            dvPhysicianDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "out collapse");
            dvAssayDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "out collapse");
            dvBillingDetails.Attributes.Add("class", "in");
        }

        private void ActiveDocument()
        {
            dvPatientDetails.Attributes.Add("class", "out collapse");
            dvPhysicianDetails.Attributes.Add("class", "out collapse");
            dvTestSpecDetails.Attributes.Add("class", "out collapse");
            dvAssayDetails.Attributes.Add("class", "out collapse");
            dvBillingDetails.Attributes.Add("class", "out collapse");
            dvDocument.Attributes.Add("class", "in");
            dvDocument.Focus();
        }

        private void SetResultSection(string SpStatus)
        {
            switch (SpStatus)
            {
                case "Received":
                    dvResCap.Visible = false;
                    dvResultView.Visible = false;
                    aresults.Visible = false;
                    btnSave.Visible = true;
                    break;

                case "Rejected":
                    dvResCap.Visible = false;
                    dvResultView.Visible = false;
                    aresults.Visible = false;
                    //dvReason.Visible = true;
                    break;

                case "Assigned to Assay":
                    dvResCap.Visible = false;
                    dvResultView.Visible = false;
                    aresults.Visible = true;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Cannot Update the Result Before test has been completed";
                    break;

                case "In Testing":
                    dvResCap.Visible = true;
                    dvResultView.Visible = false;
                    aresults.Visible = true;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Once Assay Testing Complete, result update will be available...";
                    break;

                case "Test Completed":
                    dvResCap.Visible = true;
                    dvResultView.Visible = true;
                    aresults.Visible = true;
                    dvResultMsg.Visible = false;
                    break;

                case "Result Recorded":
                    dvResCap.Visible = false;
                    dvResultView.Visible = true;
                    aresults.Visible = true;
                    break;

                case "Result Delivered":
                    dvResCap.Visible = true;
                    dvResultView.Visible = false;
                    aresults.Visible = true;
                    break;

                default:
                    dvResCap.Visible = false;
                    dvResultView.Visible = false;
                    aresults.Visible = false;
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
            ActiveAssay();
            if (btnSave.Text == "Add to Assay")
            {
                string assName = new SpecimenDA().GetAssayGroup(Convert.ToInt32(Session["OrgID"]), ddlAssayType.SelectedValue.Trim(), "Current");
                int sampCount = new SpecimenDA().GetSampleCount(Convert.ToInt32(Session["OrgID"]), ddlAssayType.SelectedValue.Trim(), "Current");
                if (assName.Trim() == "" || sampCount == 20)
                {
                    dvAssayName.Attributes.Add("class", "modal fade in");
                    dvAssayName.Attributes.Add("style", "display:block");
                }
                else
                {
                    int AssayID = (new SpecimenDA()).AddSpecimenToAssay(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value), ddlAssayType.SelectedValue.Trim(), assName);
                    hAssayID.Value = Convert.ToString(AssayID);                    
                }
                List<AssayGroupBO> assayDet = GetAssayDetails();
                if (assayDet.Count > 0)
                {
                    rptAssayDetails.DataSource = assayDet;
                    rptAssayDetails.DataBind();
                }
                //Response.Redirect("~/pages/st");
                dvPendingReasion.Visible = false;
                dvRejectionReasion.Visible = false;
                popSpecimen();
            }
        }

        protected void btnSaveReq_Click(object sender, EventArgs e)
        {
            CustomerBO oCust = (new CustomerDA()).SaveCustomer(
                new CustomerBO
                {
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    CustomerName = txtPhyName.Text ,
                    Specialization = txtSpecialization.Text.Trim(),
                    Address1 = txtPhyAddress1.Text,
                    City = txtPhyCity.Text,
                    State = txtPhyState.Text,
                    Country = ddCountry.SelectedValue,
                    Phone = txtPhyPhone.Text,
                    Email = txtPhyEmail.Text,
                    CustomerID = Convert.ToInt32(hCustID.Value),
                    Fax = txtPhyFax.Text.Trim(),
                    Facility = txtPhyFacility.Text.Trim(),
                    //Diagnosis = txtDiagnosis.Text.Trim(),
                    // DiagnosisCode = txtDiagnosisCode.Text.Trim(),
                    //-ResultType = ddlResultType.SelectedValue,
                    Zipcode = txtPhyPCode.Text.Trim()
                },
                Convert.ToInt32(hSpecimenID.Value));

           


            if (oCust != null)
            {
                dvPhysicianEdit.Visible = false;
                dvPhysicianView.Visible = true;
                aReqPhysicianDetails.Visible = true;
                hCustID.Value = Convert.ToString(oCust.CustomerID);
                ltrPhyName.Text = lblPhyName.Text ="Dr"+" "+txtPhyName.Text+" "+txtSpecialization.Text;
                ltrReqAddress.Text = txtPhyAddress1.Text + ", " + txtPhyCity.Text + ", "+ txtPhyState.Text + ", " + ddCountry.SelectedValue+", "+ txtPhyPCode.Text;
                ltrReqContact.Text = txtPhyPhone.Text ;
                ltrFaxNumber.Text = txtPhyFax.Text;
                ltrFacility.Text = lblFacility.Text = txtPhyFacility.Text;
                lblPhyEmail.Text = txtPhyEmail.Text.Trim();

                // lblDiagnosis.Text = txtDiagnosis.Text.Trim();
                // lblDiagnosisCode.Text = txtDiagnosisCode.Text.Trim();
                //-lblResultType.Text = ddlResultType.SelectedValue;
            }

            SpecimenInfoBO objOrder = new SpecimenInfoBO();
            {
                objOrder.TenantID = Convert.ToInt32(Session["OrgID"]);
                objOrder.PatientID = Convert.ToInt32(hPatientID.Value);
                objOrder.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
                objOrder.CustomerID = Convert.ToInt32(hCustID.Value);
                SpecimenDA objSpecimenDA = new SpecimenDA();
                objSpecimenDA.UpdateOrder(objOrder);
            }
            if (ltrCurrentSpecimenStatus.Text == "Received" || ltrCurrentSpecimenStatus.Text == "Rejected")
            {
                CheckInfo();
                EmailSent();
            }
            string SpecimenStatus = "Physician Information Updated";
            SaveAuditLog(SpecimenStatus);
            popSpecimen();
            viewSpecimenAuditlog();
        }

        //protected void btnAssay_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/pages/assay");
        //}

        protected void btnPatientSave_Click(object sender, EventArgs e)
        {
          try
            {
                PatientBO pbo = new PatientBO();
                pbo.FirstName = txtFirstName.Text.Trim();
                pbo.LastName = txtLastName.Text.Trim();
                if (chkClosePatient.Checked == false)
                {
                    pbo.Gender = (optF.Checked ? "F" : "M");
                    
                }
                else
                {
                    pbo.Gender = "";
                }
                pbo.DOB = txtDOB.Text.Trim();
                pbo.PatientID = Convert.ToInt32(hPatientID.Value);
                pbo.CreatedBy = Convert.ToInt32(Session["UserID"]);
                pbo.TenantID = Convert.ToInt32(Session["OrgID"]);
                pbo.Street = txtstreet.Text.Trim();
                pbo.City = txtCity.Text.Trim();
                pbo.State = txtState.Text.Trim();
                pbo.Zip = txtZip.Text.Trim();
                pbo.Country = txtCountry.Text.Trim();
                pbo.isPatientAddressSame = chkSameAddress.Checked;
                pbo.GuardianRelationship = ddlGRelationship.SelectedValue;
                pbo.EmailID = txtEmailID.Text.Trim();
                pbo.ContactNo =txtContactNo.Text.Trim();
                if (chkSameAddress.Checked)
                {
                    pbo.GuardianFirstName = txtGuardianFirstName.Text.Trim();
                    pbo.GuardianLastName = txtGuardianLastName.Text.Trim();
                    pbo.GuardianContactNo = txtGuardianContactNo.Text.Trim();
                    pbo.GuardianEmailID = txtGuardianEmailID.Text.Trim();
                    pbo.GuardianStreet = txtstreet.Text.Trim();
                    pbo.GuardianCity = txtCity.Text.Trim();
                    pbo.GuardianState = txtState.Text.Trim();
                    pbo.GuardianCountry = txtCountry.Text.Trim();
                    pbo.GuardianZip = txtZip.Text.Trim();
                }
                else
                {
                    pbo.GuardianFirstName = txtGuardianFirstName.Text.Trim();
                    pbo.GuardianLastName = txtGuardianLastName.Text.Trim();
                    pbo.GuardianContactNo = txtGuardianContactNo.Text;
                    pbo.GuardianEmailID = txtGuardianEmailID.Text.Trim();
                    if(ddlGRelationship.SelectedValue!="0")
                     pbo.GuardianRelationship = ddlGRelationship.Text.Trim();
                    pbo.GuardianStreet = txtGuardianStreet.Text.Trim();
                    pbo.GuardianCity = txtGuardianCity.Text.Trim();
                    pbo.GuardianState = txtGuardianState.Text.Trim();
                    pbo.GuardianCountry = txtGuardianCountry.Text.Trim();
                    pbo.GuardianZip = txtGuardianZip.Text.Trim();
                }
                int PatientID = (new SpecimenDA()).SavePatientInfoDA(pbo);

                if (Convert.ToString(PatientID) != "")
                {
                    dvPatientView.Visible = true;
                    dvGuardianView.Visible = true;
                    dvPatientEdit.Visible = false;
                    aModalPatientInfo.Visible = true;
                    ltrLocation.Text = txtstreet.Text.Trim() + (txtCity.Text.Trim() == "" ? "" : ", " + txtCity.Text.Trim()) + (txtState.Text.Trim() == "" ? "" : ", " + txtState.Text.Trim()) + (txtZip.Text.Trim() == "" ? "" : ", " + txtZip.Text.Trim()) + (txtCountry.Text.Trim() == "" ? "" : ", " + txtCountry.Text.Trim());
                    lblFirstName.Text = txtFirstName.Text;
                    lblLastName.Text = txtLastName.Text;
                    ltrAgeGender.Text = (optF.Checked ? "F" : "M") + "/ " + txtDOB.Text;
                    ltrGuardianName.Text = txtGuardianFirstName.Text + " " + txtGuardianLastName.Text;
                    ltrEmailID.Text = txtEmailID.Text.Trim();
                    ltrContactNo.Text = txtContactNo.Text.Trim();
                    ltrGuardianEmailID.Text = txtGuardianEmailID.Text.Trim();
                    ltrGuardianContactNo.Text = txtGuardianContactNo.Text.Trim();
                    if (chkSameAddress.Checked)
                    {
                        ltrGuardianAddress.Text = txtstreet.Text.Trim() + (txtCity.Text.Trim() == "" ? "" : ", " + txtCity.Text.Trim()) + (txtState.Text.Trim() == "" ? "" : ", " + txtState.Text.Trim()) + (txtZip.Text.Trim() == "" ? "" : ", " + txtZip.Text.Trim()) + (txtCountry.Text.Trim() == "" ? "" : ", " + txtCountry.Text.Trim());
                    }
                    else
                    {
                        ltrGuardianAddress.Text = txtGuardianStreet.Text + (txtGuardianCity.Text.Trim() == "" ? "" : ", " + txtGuardianCity.Text.Trim()) + (txtGuardianState.Text.Trim() == "" ? "" : ", " + txtGuardianState.Text.Trim()) + (txtGuardianZip.Text.Trim() == "" ? "" : ", " + txtGuardianZip.Text) + (txtGuardianCountry.Text.Trim() == "" ? "" : ", " + txtGuardianCountry.Text);
                    }

                    
                    //lblGRelationship.Text = ddlGRelationship.Text;
                    if(ddlGRelationship.SelectedValue!="0")
                        lblGRelationship.Text = ddlGRelationship.SelectedValue;
                }
                if (ltrCurrentSpecimenStatus.Text == "Received" || ltrCurrentSpecimenStatus.Text == "Rejected")
                {
                   
                    CheckInfo();
                    EmailSent();
                }
                
                 string SpecimenStatus = "Patient Information Updated";
                SaveAuditLog(SpecimenStatus);
                popSpecimen();
                viewSpecimenAuditlog();
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
                sType = ddlRemSpecimenType.SelectedValue;
                //if (optRemainSerum.Checked)
                //    sType = "Serum";
                //if (optRemainBlood.Checked)
                //    sType = "Blood";

                (new SpecimenDA()).SaveTestResult(new ResultBO
                {
                    AssayID = Convert.ToInt32(hAssayID.Value),
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    SpecimenType = sType,
                    SubSpecimenType = "",
                    //SubSpecimenType = (optRemainBlood.Checked ? "" : ""),
                    RemainVol = txtRemainVol.Text,
                    BindValue = Convert.ToDecimal(txtBindValue.Text),
                    BindValComment = txtBindComment.Text,
                    BlockValue = Convert.ToDecimal(txtBlockValue.Text),
                    BlockValComment = txtBlockComment.Text,
                    IsRepeat = chkNeedToRetest.Checked
                });
                lblRemainType.Text = sType /*+ (optRemainBlood.Checked ? "/" + ddBloodType.SelectedValue : "")*/;
                lblRemainVol.Text = txtRemainVol.Text;
                ltrBIN.Text = txtBindValue.Text + " / " + txtBindComment.Text;
                ltrBlocking.Text = txtBlockValue.Text + " / " + txtBlockComment.Text;
                ltrRetested.Text = chkNeedToRetest.Checked ? "Yes" : "No";
                ltrRepeatNo.Text = chkNeedToRetest.Checked ? Convert.ToString(Convert.ToInt32(ltrRepeatNo.Text) + 1) : "No";
                lnkGenRpt.Visible = true;
                dvResultView.Visible = true;
                dvResultEdit.Visible = false;
                aresults.Visible = true;
                if (lblCurrentSpecimenStatus.Text == "Result Recorded")
                {
                    lnkGenRpt.Visible = false;
                    if (DocNumber != null)
                    {
                        lnkbtnRegenerate.Visible = true;
                    }
                }
                //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus("Result Recorded");
                ltrCurrentSpecimenStatus.Text = lblCurrentSpecimenStatus.Text = "Result Recorded";
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
                (new SpecimenDA()).UpdateSpecimenInfo(new SpecimenInfoBO
                {
                    //SpecimentType = (rdSpecimenserum.Checked ? "Serum" : "Blood"),
                    BloodType = ddlBloodType.SelectedValue,
                    SpecimentType = ddlSpecimenType.SelectedValue,
                    DateDrawn = txtDrawndate.Text.Trim(),
                    TimeDrawn = txtDrawnTime.Text.Trim(),
                    TransitTime = txtTransitTime.Text.Trim(),
                    TransitTemperature = ddlViewTransitTemp.SelectedItem.Text,
                    VolumeReceived = txtVolReceived.Text.Trim(),
                    Comment = txtPotInterfer.Text.Trim(),
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    SampleReceiveDate = txtReceivedOn.Text,
                    ReceivedTime = txtReceivedTime.Text
                });
                if (hSpecimenID.Value!= String.Empty)
                {
                    dvSpecimenView.Visible = true;
                    dvSpecimenEdit.Visible = false;
                    aSpecimenInformation.Visible = true;
                    lbldrawnDatetime.Text = txtDrawndate.Text.Trim() + " " + txtDrawnTime.Text.Trim();
                    lblTransitTime.Text = txtTransitTime.Text.Trim();
                    lblTransitTemp.Text = ddlViewTransitTemp.SelectedItem.Text;
                    lblVolRec.Text = txtVolReceived.Text.Trim();
                    lblPotInter.Text = txtPotInterfer.Text.Trim();
                    lbltype.Text = ddlSpecimenType.SelectedItem.Text;
                    lblReceivedOn.Text = txtReceivedOn.Text + "/" + txtReceivedTime.Text;
                    //lblBloodType.Text = ddlBloodType.SelectedItem.Text;
                }
                if (ltrCurrentSpecimenStatus.Text == "Received" || ltrCurrentSpecimenStatus.Text == "Rejected")
                {
                    CheckInfo();
                    EmailSent();
                }
                string SpecimenStatus = "Specimen Information Updated";
                SaveAuditLog(SpecimenStatus);
                popSpecimen();
                viewSpecimenAuditlog();
                if (lblAgeGender.Text.Trim() == "-")
                {
                    
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        protected void lnkGenRpt_Click(object sender, EventArgs e)
        {
            ActiveTestSpecimen();
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
                //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus("Result Recorded");
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
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = Winnovative.WnvHtmlConvert.PdfCompressionLevel.AboveNormal; // NoCompression;
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

                if (chkAcceptTest.Checked && chkConsentProvided.Checked && chkRejection.Checked)
                    status = "Received";

                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = chkConsentProvided.Checked,
                    IsRejection = chkRejection.Checked,
                    IsSpecimenAccept = chkAcceptTest.Checked,
                    SpecimenStatus = status,
                });
                if (Convert.ToString(PatientID) != String.Empty)
                {
                    //if (chkConsentProvided.Checked)
                    //{
                    //    YesConsent.Visible = true;
                    //    NoConsent.Visible = false;
                    //}
                    //else
                    //{
                    //    YesConsent.Visible = false;
                    //    NoConsent.Visible = true;
                    //}
                    //if (chkRequisition.Checked)
                    //{
                    //    YesReqComp.Visible = true;
                    //    NoReqComp.Visible = false;
                    //}
                    //else
                    //{
                    //    YesReqComp.Visible = false;
                    //    NoReqComp.Visible = true;
                    //}

                    //if (chkAcceptTest.Checked)
                    //{
                    //    YesAccept.Visible = true;
                    //    NoAccept.Visible = false;
                    //}
                    //else
                    //{
                    //    YesAccept.Visible = false;
                    //    NoAccept.Visible = true;
                    //}
                    //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(status);
                    ltrCurrentSpecimenStatus.Text = lblCurrentSpecimenStatus.Text = "Received";
                    //if (!chkConsentProvided.Checked || !chkRejection.Checked)
                    //    ltrCurrentSpecimenStatus.Text = lblCurrentSpecimenStatus.Text = "Pending";
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
            ActiveTestSpecimen();
            if (txtPhyEmail.Text == "")
            {
                ltrEmailSuccessmsg.Text = "";
                ltrEmailErrormsg.Text = "Require Physician Email";
            }
            else
            {
                //using (STOMS.UI.CommonCS.CustomLogs clog = new CommonCS.CustomLogs())

                List<DocumentBO> objdocres = (new DocumentDA().GetResultid(ancPdfGen.InnerText.ToString()));
                hResultDocID.Value = objdocres[0].DocID.ToString();

                DocumentBO objdoc = new DocumentBO();
                objdoc.DocID = Convert.ToInt32(hResultDocID.Value);
                objdoc.TokenID = Guid.NewGuid().ToString();
                objdoc.DocNumber = ancPdfGen.InnerText;
                objdoc.TenantID = Convert.ToInt32(Session["OrgID"]);
                DocumentDA objDA = new DocumentDA();
                hEmailToken.Value = objDA.UpdateDocNum(objdoc);

                // new STOMS.UI.CommonCS.EmailTokenGen( objdoc);

                string filepath = "/VerifyToken/Token?" + hEmailToken.Value;
                // string filepath = "/Docs/Results/" + hDocNumber.Value;

                string link = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + filepath;


                if (ancPdfGen.HRef != "" && lblPhyName.Text.Trim() != "" && txtPhyEmail.Text != "")
                {
                    List<EmailEnablementImplementationBO> emailBO = new EmailConfigurationDA().emailEnableImplementation(Convert.ToInt32(Session["OrgID"]), 4);

                    if (emailBO[0].emailEnablementBO.isToEndUser == true)
                    {
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-userName", lblPhyName.Text.Trim());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@End-PatientName", ltrFirstName.Text + " " + ltrLastName.Text);
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@TestType", lblTestType.Text);
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@Date:Time", DateTime.Now.ToShortDateString());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@SpecimenNumber", ltrSpecimenNumber.Text.Trim());
                        emailBO[0].emailEnablementTypeBO.EndUserTemplate = emailBO[0].emailEnablementTypeBO.EndUserTemplate.Replace("@ReportLink", link.ToString());

                        EmailConfigBO ema = new EmailConfigBO();
                        ema.Body = emailBO[0].emailEnablementTypeBO.EndUserTemplate;
                        ema.Subject = "Report View";
                        ema.ToAddress = txtPhyEmail.Text.Trim();
                        //ema.ToAddress = emailBO[0].emailEnablementBO.ToTenantEmails;
                        new STOMS.UI.CommonCS.SendEmail(ema);
                        ltrEmailErrormsg.Text = "";
                        ltrEmailSuccessmsg.Text = "Email Sent Successfully";
                    }
                }
            }
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

                dvUploadFile.Visible = false;
                if(hDocID.Value!="")
                {
                   dvViewFile.Visible = true;
                }
                dvDocument.Attributes.Add("class", "in");
                btnFileClose.Visible = true;
                //update request form copyID
                new ReportDA().updateReqFormCopyID(Convert.ToInt32(Session["OrgID"]), hDocID.Value, hSpecimenID.Value);
            }
        }

        private void getSpecimen(string SpecimenNumber)
        {
            dvSpecimenDetail.Visible = true;
            List<SpecimenInfoBO> oSpecimen = new List<SpecimenInfoBO>();
            oSpecimen = (new SpecimenDA()).GetSpecimenNumberInfo(SpecimenNumber, Convert.ToInt32(Session["OrgID"]));

            
            if (oSpecimen.Count > 0)
            {
                hSpecimenID.Value = oSpecimen[0].SpecimenID.ToString();
                ltrSpecimenNumber.Text = lblSpecimenNumber.Text = oSpecimen[0].SpecimenNumber;
                // Patient Info
                txtFirstName.Text = ltrFirstName.Text = lblFirstName.Text = oSpecimen[0].oPatient.FirstName;
                txtLastName.Text = ltrLastName.Text = lblLastName.Text = oSpecimen[0].oPatient.LastName;
                ltrAgeGender.Text = oSpecimen[0].oPatient.Gender + " / " + oSpecimen[0].oPatient.DOB;
                optF.Checked = (oSpecimen[0].oPatient.Gender == "F" ? true : false);
                optM.Checked = !optF.Checked;
                txtDOB.Text = oSpecimen[0].oPatient.DOB;
                txtstreet.Text = oSpecimen[0].oPatient.Street;
                ltrLocation.Text = oSpecimen[0].oPatient.Street + (oSpecimen[0].oPatient.City == "" ? "" : " ," + oSpecimen[0].oPatient.City) + (oSpecimen[0].oPatient.State == "" ? "" : " ," + oSpecimen[0].oPatient.State) + (oSpecimen[0].oPatient.Zip == "" ? "" : ", " + oSpecimen[0].oPatient.Zip) + (oSpecimen[0].oPatient.Country == "" ? "" : ", " + oSpecimen[0].oPatient.Country);
                txtCity.Text = oSpecimen[0].oPatient.City;
                txtState.Text = oSpecimen[0].oPatient.State;
                txtZip.Text = oSpecimen[0].oPatient.Zip;
                txtCountry.Text = oSpecimen[0].oPatient.Country;  
                if(oSpecimen[0].oPatient.GuardianRelationship!="0")
                    lblGRelationship.Text = oSpecimen[0].oPatient.GuardianRelationship;
                if(oSpecimen[0].oPatient.GuardianRelationship!=null && oSpecimen[0].oPatient.GuardianRelationship !="")
                ddlGRelationship.SelectedValue= oSpecimen[0].oPatient.GuardianRelationship;
                lblSpecimenNum.Text = "";
                //ltrPhyName.Text=oSpecimen

                //Verification Info
                //if (oSpecimen[0].IsConsent)
                //{
                //    YesConsent.Visible = true;
                //    NoConsent.Visible = false;
                //    chkConsentProvided.Checked = true;
                //}
                //else
                //{
                //    YesConsent.Visible = false;
                //    NoConsent.Visible = true;
                //    chkConsentProvided.Checked = false;
                //}

                //if (oSpecimen[0].IsReqComplete)
                //{
                //    YesReqComp.Visible = true;
                //    NoReqComp.Visible = false;
                //    chkRequisition.Checked = true;
                //}
                //else
                //{
                //    YesReqComp.Visible = false;
                //    NoReqComp.Visible = true;
                //    chkRequisition.Checked = false;
                //}

                //if (oSpecimen[0].IsSpecimenAccept)
                //{
                //    YesAccept.Visible = true;
                //    NoAccept.Visible = false;
                //    chkAcceptTest.Checked = true;
                //}
                //else
                //{
                //    YesAccept.Visible = false;
                //    NoAccept.Visible = true;
                //    chkAcceptTest.Checked = false;
                //}
                //lblReasonRej.Text = oSpecimen[0].RejectReasons;
                //Guardian Info
                txtGuardianFirstName.Text = oSpecimen[0].oPatient.GuardianFirstName;
                txtGuardianLastName.Text = oSpecimen[0].oPatient.GuardianLastName;
                txtGuardianStreet.Text = oSpecimen[0].oPatient.GuardianStreet;
                txtGuardianCity.Text = oSpecimen[0].oPatient.GuardianCity;
                txtGuardianState.Text = oSpecimen[0].oPatient.GuardianState;
                txtGuardianCountry.Text = oSpecimen[0].oPatient.GuardianCountry;
                txtGuardianZip.Text = oSpecimen[0].oPatient.GuardianZip;
                ltrGuardianName.Text = oSpecimen[0].oPatient.GuardianFirstName + " " + oSpecimen[0].oPatient.GuardianLastName;
                //if (chkSameAddress.Checked)
                //{
                //  ltrGuardianAddress.Text = oSpecimen[0].oPatient.Street + (oSpecimen[0].oPatient.City == "" ? "" : " ," + oSpecimen[0].oPatient.City) + (oSpecimen[0].oPatient.State == "" ? "" : " ," + oSpecimen[0].oPatient.State) + (oSpecimen[0].oPatient.Zip == "" ? "" : ", " + oSpecimen[0].oPatient.Zip) + (oSpecimen[0].oPatient.Country == "" ? "" : ", " + oSpecimen[0].oPatient.Country);
                //}
                ltrGuardianAddress.Text = oSpecimen[0].oPatient.GuardianStreet + (oSpecimen[0].oPatient.GuardianCity == "" ? "" : " ," + oSpecimen[0].oPatient.GuardianCity) + (oSpecimen[0].oPatient.GuardianState == "" ? "" : " ," + oSpecimen[0].oPatient.GuardianState) + (oSpecimen[0].oPatient.GuardianZip == "" ? "" : ", " + oSpecimen[0].oPatient.GuardianZip) + (oSpecimen[0].oPatient.GuardianCountry == "" ? "" : ", " + oSpecimen[0].oPatient.GuardianCountry);
                hPatientID.Value = Convert.ToString(oSpecimen[0].oPatient.PatientID);
                lbldrawnDatetime.Text = convertMonthDate(oSpecimen[0].DateDrawn) + " / " + oSpecimen[0].TimeDrawn;
                lblReceivedOn.Text = convertMonthDate(oSpecimen[0].SampleReceiveDate) + " / " + oSpecimen[0].ReceivedTime;
                lblTransitTime.Text = oSpecimen[0].TransitTime;
                lblTransitTemp.Text = oSpecimen[0].TransitTemperature;
                lblVolRec.Text = oSpecimen[0].VolumeReceived;
                lblPotInter.Text = oSpecimen[0].InterSubstance;

                //Specimen Information
                txtDrawndate.Text = convertMonthDate(oSpecimen[0].DateDrawn);
                txtDrawnTime.Text = oSpecimen[0].TimeDrawn;
                txtTransitTime.Text = oSpecimen[0].TransitTime;
                ddlViewTransitTemp.SelectedValue = oSpecimen[0].TransitTemperature;
                txtVolReceived.Text = oSpecimen[0].VolumeReceived;
                txtPotInterfer.Text = oSpecimen[0].InterSubstance;
                lbltype.Text = oSpecimen[0].SpecimentType;
                lblTestType.Text = oSpecimen[0].TestType;
                //rdSpecimenserum.Checked = (oSpecimen[0].SpecimentType == "Serum" ? true : false);
                //rdSpecimenblood.Checked = (oSpecimen[0].SpecimentType == "Blood" ? true : false);
                lblRemainType.Text = oSpecimen[0].SpecimentType;
                txtReceivedOn.Text = convertMonthDate(oSpecimen[0].SampleReceiveDate);
                txtReceivedTime.Text = oSpecimen[0].ReceivedTime;
                //lblBloodType.Text = oSpecimen[0].BloodType;
                //ddSpecimentype.SelectedIndex = ddSpecimentype.Items.IndexOf(ddSpecimentype.Items.FindByText(oSpecimen[0].BloodType));
                ltrCurrentSpecimenStatus.Text = lblCurrentSpecimenStatus.Text = oSpecimen[0].SpecimenStatus;

                if (oSpecimen[0].CustomerID != 0)
                {
                    hCustID.Value = Convert.ToString(oSpecimen[0].CustomerID);
                    List<CustomerBO> oCust = (new CustomerDA()).GetCustomer(hCustID.Value);
                    if (oCust.Count > 0)
                    {
                        ltrPhyName.Text = lblPhyName.Text = txtPhyName.Text = oCust[0].CustomerName;
                        txtPhyAddress1.Text = oCust[0].Address1;
                        txtPhyCity.Text = oCust[0].City;
                        ltrGuardianAddress.Text = oCust[0].Address1 + (oCust[0].City == "" ? "" : " ," + oCust[0].City) + (oCust[0].State == "" ? "" : " ," + oCust[0].State) + (oCust[0].Zipcode == "" ? "" : ", " + oCust[0].Zipcode) + (oCust[0].Country == "" ? "" : ", " + oCust[0].Country);
                        ltrReqAddress.Text = txtPhyAddress1.Text + ", " + txtPhyCity.Text + ", " + txtPhyState.Text + ", " + ddCountry.SelectedValue+", "+ txtPhyPCode.Text; 
                        txtPhyPhone.Text = oCust[0].Phone;
                        txtPhyEmail.Text = oCust[0].Email;
                        ltrReqContact.Text = txtPhyPhone.Text;
                        ltrFaxNumber.Text = txtPhyFax.Text;
                        lblFacility.Text=ltrFacility.Text = oCust[0].Facility;
                        //lblDiagnosis.Text = txtDiagnosis.Text = oCust[0].Diagnosis;
                        //lblDiagnosisCode.Text = txtDiagnosisCode.Text = oCust[0].DiagnosisCode;
                        //lblResultType.Text = ddlResultType.SelectedValue = oCust[0].ResultType;
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
                        hResultDocID.Value = oSpecimen[0].oResult.ResultDocID.ToString();
                        if (oSpecimen[0].oResult.SubSpecimenType != "")
                            //ddBloodType.SelectedValue = oSpecimen[0].oResult.SubSpecimenType;

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
                        //if (oSpecimen[0].oResult.SpecimenType == "Serum")
                        //{
                        //    optRemainSerum.Checked = true;
                        //    optRemainBlood.Checked = false;
                        //}
                        if (oSpecimen[0].oResult.SpecimenType != "" || oSpecimen[0].oResult.SpecimenType != null)
                            ddlRemSpecimenType.SelectedValue = oSpecimen[0].oResult.SpecimenType.Trim();
                        hAssaySpecimenID.Value = Convert.ToString(oSpecimen[0].oResult.AssaySpecimenID);
                    }
                }
                //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(oSpecimen[0].SpecimenStatus);
                //ltrStatus.Text = Common.Constant.GetStatusFormat(oSpecimen[0].SpecimenStatus);
                hSpecimenStatus.Value = oSpecimen[0].SpecimenStatus;
                SetResultSection(oSpecimen[0].SpecimenStatus);
                if (oSpecimen[0].oResult != null && oSpecimen[0].oResult.AssayStatus != string.Empty)
                    SetResultSection(oSpecimen[0].oResult.AssayStatus);

                if (hAssayID.Value == "" || hAssayID.Value == Convert.ToString(0))
                {
                    dvResCap.Visible = true;
                    dvResultView.Visible = false;
                    dvResultMsg.Visible = true;
                    ltrResultMsg.Text = "Assay Info is Empty.Please load Specimen Information from Assay list.";
                    lnkGenRpt.Visible = false;
                }

                DocumentBO documentBO = new ReportDA().ViewhardCopy(oSpecimen[0].ReqFormCopyID);
                ancViewCopy.InnerText = documentBO.DocNumber;
                string filesource = @"\Docs\RequestForm\" + documentBO.DocNumber + "." + documentBO.DocType;
                string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                ancViewCopy.HRef = url;
                fileuploadcopy();
            }

            else
            {
                lblSpecimenNum.Text = "Specimen Number Invalid";
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

        protected void btnSpecimenNum_Click(object sender, EventArgs e)
        {
            getSpecimen(txtSpecimenNum.Text);
            //AssayInfo.PopulateSpecimenAssay(Convert.ToInt32(hSpecimenID.Value));
        }

        protected void aModalPatientInfo_ServerClick(object sender, EventArgs e)
        {
            popSpecimen();
            dvPatientEdit.Visible = true;
            dvPatientView.Visible = false;
            dvGuardianView.Visible = false;
            aModalPatientInfo.Visible = false;
            ActivePatient();
        }

        protected void aReqPhysicianDetails_ServerClick(object sender, EventArgs e)
        {
            dvPhysicianEdit.Visible = true;
            dvPhysicianView.Visible = false;
            aReqPhysicianDetails.Visible = false;
            ActivePhysician();
        }

        protected void aSpecimenInformation_ServerClick(object sender, EventArgs e)
        {
            dvSpecimenEdit.Visible = true;
            dvSpecimenView.Visible = false;
            aSpecimenInformation.Visible = false;
            btnSave.Visible = false;
            ActiveTestSpecimen();
        }

        protected void aresults_ServerClick(object sender, EventArgs e)
        {
            dvResultEdit.Visible = true;
            dvResultView.Visible = false;
            aresults.Visible = false;
            dvResultMsg.Visible = false;
            ActiveTestSpecimen();
        }

        protected void ancRecResults_ServerClick(object sender, EventArgs e)
        {
            TestResultsBO resultBO = new TestResultsDA().getResults(Convert.ToInt32(Session["OrgID"]), int.Parse(hSpecimenID.Value));
            if (ancRecResults.InnerText == "Click here for recording results")
            {
                Response.Redirect("~/pages/ResultRecording?si=" + hSpecimenID.Value + "&" + "mode=edit" + "&" + "sn=" + ltrSpecimenNumber.Text);
            }
            else
            {
                Response.Redirect("~/pages/ResultRecording?ri=" + resultBO.ResultID + "&" + "si=" + hSpecimenID.Value + "&" + "mode=view" + "&" + "sn=" + ltrSpecimenNumber.Text);
            }
        }

        protected void aPaymentInformation_ServerClick(object sender, EventArgs e)
        {
            aPaymentInformation.Visible = false;
            ActivePaymentMode();
            dvUpdate.Visible = true;
            switch (ddlPaymentMode.SelectedValue)
            {
                case "Cash":
                    dvcashEdit.Visible = true;
                    dvCashView.Visible = false;
                    dvUpdate.Visible = true;
                    string[] money = lblCash.Text.Trim().Split(' ');
                    if(money[0]!="")
                        ddlCurrency.SelectedValue = money[0];
                    if (money[1] != "")
                        txtCash.Text = money[1];
                    txtTransDate.Text = lblTransactionDate.Text;
                    txtCashDescription.Text = lblDescription.Text;
                    break;

                case "CreditCard":
                    dvUpdate.Visible = false;
                    aPaymentInformation.Visible = true;
                    dvCreditCardDelete.Visible = true;
                    dvCreditCardSecure.Visible = false;
                    dvCreditCardDelete.Attributes.Add("class", "modal fade in");
                    dvCreditCardDelete.Attributes.Add("style", "display:block");
                    btnUpdatePayment.Attributes.Add("data - toggle", "modal");
                    btnUpdatePayment.Attributes.Add("data - target", "#dvCreditCardDelete");
                    break;

                case "Cheque":
                    dvChequeEdit.Visible = true;
                    dvChequeView.Visible = false;
                    //btnUpdatePayment.Text = "Update";
                    if (lblBankName.Text != "Bank Name")
                        txtBankName.Text = lblBankName.Text;
                    if (lblBranchName.Text != "Branch Name")
                        txtBranchName.Text = lblBranchName.Text;
                    if (lblCheqNo.Text != "Cheq No#")
                        txtCheqNo.Text = lblCheqNo.Text;
                    if (lblCheqDate.Text != "Cheque Date")
                        txtCheqDate.Text = lblCheqDate.Text;
                    if (lblRoutNo.Text != "Routing No#")
                        txtRoutNo.Text = lblRoutNo.Text;
                    if (lblAcctNo.Text != "Account No#")
                        txtAcctNo.Text = lblAcctNo.Text;

                    dvUpdate.Visible = true;
                    break;

                case "Insurance":
                    dvInsuranceEdit.Visible = true;
                    dvInsuranceView.Visible = false;
                    if (lblInsuranceType.Text != "Insurance Type")
                        txtInsuranceType.Text = lblInsuranceType.Text;
                    if (lblInsuranceCompany.Text != "Insurance Company")
                        txtInsuranceCompany.Text = lblInsuranceCompany.Text;
                    if (lblInsuranceNumber.Text != "Insurance Number")
                        txtInsuranceNumber.Text = lblInsuranceNumber.Text;
                    if (lblMemberName.Text != "Member Name")
                        txtMemberName.Text = lblMemberName.Text;
                    if (txtMemberShipNumber.Text != "MemberShip Number")
                        txtMemberShipNumber.Text = lblMemberShipNumber.Text;
                    if (lblGroupNumber.Text != "Group Number")
                        txtGroupNumber.Text = lblGroupNumber.Text;
                    if (lblPreAuthCode.Text != "PreAuthCode")
                        txtPreAuthCode.Text = lblPreAuthCode.Text;

                    dvUpdate.Visible = true;
                    break;

                case "Free":
                    dvUpdate.Visible = false;
                    break;

                case "Charge to physician office":
                    dvUpdate.Visible = false;
                    break;
            }
        }

        protected void aCreditCardSecure_ServerClick(object sender, EventArgs e)
        {
            ActivePaymentMode();
            dvCreditCardSecure.Visible = true;
            dvCreditCardDelete.Visible = false;
            dvCreditCardSecure.Attributes.Add("class", "modal fade in");
            dvCreditCardSecure.Attributes.Add("style", "display:block");
            btnUpdatePayment.Attributes.Add("data-toggle", "modal");
            btnUpdatePayment.Attributes.Add("data-target", "#dvCreditCardSecure");

            PaymentBO payBO = GetPayment();
            if (payBO.PaymentID > 0)
            {
                lblCardTypeSec.Text = payBO.CreditDetails.CardType;
                lblCardNoSec.Text = KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.CardNumber.ToString());
                lblCardHolderSec.Text = payBO.CreditDetails.HolderName;
                lblCvvSec.Text = KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.CVVNumber);
                lblExpireDateSec.Text = KoSoft.Utility.KoCrypt.Decrypt(payBO.CreditDetails.ExpireDate);
            }
        }

        protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
        {        
            if (chkSameAddress.Checked)
            {                
                txtGuardianStreet.Text = txtstreet.Text;
                txtGuardianCity.Text = txtCity.Text;
                txtGuardianState.Text = txtState.Text;
                txtGuardianCountry.Text = txtCountry.Text;
                txtGuardianZip.Text = txtZip.Text;                
                txtGuardianStreet.ReadOnly = true;
                txtGuardianCity.ReadOnly = true;
                txtGuardianState.ReadOnly = true;
                txtGuardianCountry.ReadOnly = true;
                txtGuardianZip.ReadOnly = true;
            }
            else
            {
                //txtGuardianFirstName.Text = "";
                //txtGuardianLastName.Text = "";
                txtGuardianStreet.Text = "";
                txtGuardianCity.Text = "";
                txtGuardianState.Text = "";
                txtGuardianCountry.Text = "";
                txtGuardianZip.Text = "";
                txtGuardianStreet.ReadOnly = false;
                txtGuardianCity.ReadOnly = false;
                txtGuardianState.ReadOnly = false;
                txtGuardianCountry.ReadOnly = false;
                txtGuardianZip.ReadOnly = false;
            }
        }

        protected void btnFileClose_Click(object sender, EventArgs e)
        {
            dvUploadFile.Visible = true;
            btnFileClose.Visible = false;
            dvViewFile.Visible = false;
            dvDocument.Attributes.Add("class", "in");
        }

        protected void btnPatientBack_Click(object sender, EventArgs e)
        {            
            dvPatientView.Visible = true;
            dvGuardianView.Visible = true;
            dvPatientEdit.Visible = false;
            aModalPatientInfo.Visible = true;
            ActivePatient();
        }

        protected void btnPhysicianBack_Click(object sender, EventArgs e)
        {
            dvPhysicianView.Visible = true;
            dvPhysicianEdit.Visible = false;
            aReqPhysicianDetails.Visible = true;
            ActivePhysician();
        }

        protected void btnSpecimenBack_Click(object sender, EventArgs e)
        {
            dvSpecimenView.Visible = true;
            dvSpecimenEdit.Visible = false;
            aSpecimenInformation.Visible = true;
            ActiveTestSpecimen();
            if (lblCurrentSpecimenStatus.Text == "Received")
                btnSave.Visible = true;
        }

        protected void btnResultBack_Click(object sender, EventArgs e)
        {
            ActiveTestSpecimen();
            dvResultEdit.Visible = false;
            aresults.Visible = true;                     
            if (ltrResultMsg.Text == "")
            {
                dvResultView.Visible = true;
                dvResultMsg.Visible = false;
            }
            else
            {
                dvResultMsg.Visible = true;                
            }
        }

        protected void btnDocumentBack_Click(object sender, EventArgs e)
        {
            dvUploadFile.Visible = false;
            dvViewFile.Visible = true;
            btnFileClose.Visible = true;
            ActiveDocument();
        }

        protected void btnSVVerificationInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = string.Empty;
                string status = string.Empty;
                string sReasons = string.Empty;
                //if (chkConsentProvided.Checked || chkRejection.Checked || chkAcceptTest.Checked || chkOthers.Checked)
                //    status = "Received";

                //if (!chkConsentProvided.Checked && !chkRejection.Checked && !chkAcceptTest.Checked &&!chkOthers.Checked)
                //    status = "Received";

                if (chkRejection.Checked== true)
                {
                    ltrCurrentSpecimenStatus.Text = status = "Rejected";
                    
                    for (int idx = 0; idx < ddlRejectReason.Items.Count; idx++)
                    {
                        if (ddlRejectReason.Items[idx].Selected)
                        {
                            if (ddlRejectReason.Items[idx].Value == "Others")
                                sReasons += txtOtherRejectReason.Text + ", ";
                            else
                                sReasons += Convert.ToString(ddlRejectReason.Items[idx].Value) + ",";
                        }
                        if (sReasons != "")
                        {
                            sReasons = sReasons.Remove(sReasons.Length - 1);
                        }
                    }
                }
                else
                {
                    status = "Received";
                }
                //if (chkAcceptTest.Checked && chkConsentProvided.Checked && chkRejection.Checked && !chkOthers.Checked)
                //{
                //    status = "Received";
                //    dvalertdanger.Disabled = true;
                //}
                if (!chkOthers.Checked)
                {
                    txtPendingOther.Text = "";
                }

                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = chkConsentProvided.Checked,
                    IsRejection = chkRejection.Checked,
                    IsSpecimenAccept = chkAcceptTest.Checked,
                    PendingReasons = txtPendingOther.Text.Trim(),
                    ReactivateReason = txtPendingtoRec.Text.Trim(),
                    SpecimenStatus = status,
                    RejectReasons = sReasons,
                });
                if (Convert.ToString(PatientID) != String.Empty)
                {

                    //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(status);
                    lblCurrentSpecimenStatus.Text = "Received";
                    btnSave.Visible = true;
                }
                CheckInfo();

                string SpecimenStatus = "";
                if (chkOthers.Checked)
                {
                     SpecimenStatus ="Verification Updated with Exception";
                }
                else
                {
                    SpecimenStatus = "Verification Updated";
                }
                
                SaveAuditLog(SpecimenStatus);
                viewSpecimenAuditlog();
                popSpecimen();
                Reasion();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        protected void BtnMitDownload_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + hDocNumber.Value + ".pdf");
            Response.TransmitFile(Server.MapPath("/Docs/Results/" + hDocNumber.Value + ".pdf"));
            Response.End();
        }

        protected void lbtnReasonUpdate_Click(object sender, EventArgs e)
        {
            if (lbtnReasonUpdate.Text.Trim()=="Reactivate")
            {
                try
                {
                    string strMsg = string.Empty;
                    string status = string.Empty;
                    string sReasons = string.Empty;
                    //if (!chkConsentProvided.Checked || !chkRequisition.Checked || chkOthers.Checked)
                    //    status = "Pending";
                    if (!chkAcceptTest.Checked)
                    {
                        status = "Rejected";
                        for (int idx = 0; idx < ddlRejectReason.Items.Count; idx++)
                        {
                            if (ddlRejectReason.Items[idx].Selected)
                            {
                                if (ddlRejectReason.Items[idx].Value == "Others")
                                    sReasons += txtOtherRejectReason.Text + ", ";
                                else
                                    sReasons += Convert.ToString(ddlRejectReason.Items[idx].Value) + ",";
                            }
                            if (sReasons != "")
                            {
                                sReasons = sReasons.Remove(sReasons.Length - 1);
                            }
                        }
                    }
                    if (chkAcceptTest.Checked && chkConsentProvided.Checked && chkRejection.Checked && !chkOthers.Checked)
                     {
                        status = "Received";
                        dvalertdanger.Disabled = true;
                    }

                    int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                    {
                        SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                        TenantID = Convert.ToInt32(Session["OrgID"]),
                        IsConsent = chkConsentProvided.Checked,
                        IsRejection = chkRejection.Checked,
                        IsSpecimenAccept = chkAcceptTest.Checked,
                        PendingReasons = txtPendingOther.Text.Trim(),
                        ReactivateReason = txtPendingtoRec.Text.Trim(),
                        SpecimenStatus = status,
                        RejectReasons = sReasons,
                    });
                    if (Convert.ToString(PatientID) != String.Empty)
                    {
                       
                        //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(status);
                        lblCurrentSpecimenStatus.Text = "Received";
                        //if (!chkConsentProvided.Checked || !chkRequisition.Checked)
                        //    lblCurrentSpecimenStatus.Text = "Pending";
                        btnSave.Visible = true;
                    }
                    popSpecimen();
                    Reasion();
                    lbtnReasonUpdate.Text = "Edit";
                    txtPendingtoRec.Enabled = false;
                                   }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }

            else
            {
                lbtnReasonUpdate.Text = "Reactivate";
                txtPendingtoRec.Enabled = true;
            }
        }

        protected void lbtnRejectReason_Click(object sender, EventArgs e)
        {
             try
             {
                string strMsg = string.Empty;
                string status = string.Empty;
                string sReasons = string.Empty;
                //if (!chkConsentProvided.Checked || !chkRequisition.Checked || chkOthers.Checked)
                //    status = "Pending";
                if (!chkAcceptTest.Checked)
                {
                    status = "Rejected";
                    for (int idx = 0; idx < ddlRejectReason.Items.Count; idx++)
                    {
                        if (ddlRejectReason.Items[idx].Selected)
                        {
                            if (ddlRejectReason.Items[idx].Value == "Others")
                                sReasons += txtOtherRejectReason.Text + ", ";
                            else
                                sReasons += Convert.ToString(ddlRejectReason.Items[idx].Value) + ",";
                        }
                        if (sReasons != "")
                        {
                            sReasons = sReasons.Remove(sReasons.Length - 1);
                        }
                    }

                    //if (sReasons == String.Empty)
                    //{
                    //    strMsg = strMsg + (strMsg != "" ? "<br/>" : "") + "If specimen is not acceptable for testing, please select reason for rejection";
                    //}
                    //else
                    //{
                    //    sReasons = sReasons.Remove(sReasons.Length - 1);
                    //}

                }
                if (chkAcceptTest.Checked && chkConsentProvided.Checked && chkRejection.Checked && !chkOthers.Checked)
                {
                    status = "Received";
                    dvalertdanger.Disabled = true;
                }

                int PatientID = (new SpecimenDA()).SaveVerificationInfo(new SpecimenInfoBO
                {
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    IsConsent = chkConsentProvided.Checked,
                    IsRejection = chkRejection.Checked,
                    IsSpecimenAccept = chkAcceptTest.Checked,
                    PendingReasons = txtPendingOther.Text.Trim(),
                    ReactivateReason = txtPendingtoRec.Text.Trim(),
                    SpecimenStatus = status,
                    RejectReasons = sReasons,
                });
                if (Convert.ToString(PatientID) != String.Empty)
                {
                        
                    //btnSave.Text = Common.Constant.GetToUpdateSpecimentStatus(status);
                    lblCurrentSpecimenStatus.Text = "Received";
                    //if (!chkConsentProvided.Checked || !chkRequisition.Checked)
                    //    lblCurrentSpecimenStatus.Text = "Pending";
                    btnSave.Visible = true;
                }
                popSpecimen();
                Reasion();
               
                txtPendingOther.Enabled = false;
             }
             catch (Exception ex)
             {
                errMsg = ex.Message;
             }
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBillInfo.Text = "";
            ActivePaymentMode();
            dvUpdate.Visible = true;
            PaymentType(ddlPaymentMode.SelectedValue.Trim());
            EditPayment(ddlPaymentMode.SelectedValue.Trim());
            if (Convert.ToInt32(hPaymentID.Value) > 0)
                ViewPayment(hPaymentMode.Value);

            PaymentBO payBO = GetPayment();
            if (hPaymentMode.Value == ddlPaymentMode.SelectedValue && payBO.PaymentID > 0)
            {
                dvUpdate.Visible = false;
                aPaymentInformation.Visible = true;
                //btnUpdatePayment.Text = "Edit Details";                
                ddlPaymentStaus.SelectedValue = payBO.PaymentStatus.Trim();
            }
            else
            {
                ClearPayment(ddlPaymentMode.SelectedValue.Trim());
                if(ddlPaymentMode.SelectedValue != "Free" && ddlPaymentMode.SelectedValue != "Charge to physician office")
                    dvUpdate.Visible = true;
                aPaymentInformation.Visible = false;
                //btnUpdatePayment.Text = "Update";
                ddlPaymentStaus.SelectedIndex = 0;
            }
        }

        private void PaymentType(string PaymentValue)
        {
            switch (PaymentValue)
            {
                case "Cash":
                    dvCashDetails.Visible = true;
                    dvCreditCardDetails.Visible = false;
                    dvChequeDetails.Visible = false;
                    dvInsuranceDetails.Visible = false;
                    dvFreeDetails.Visible = false;
                    dvChargeToPhysicianDetails.Visible = false;
                    ddlPaymentMode.Focus();
                    break;

                case "CreditCard":
                    dvCashDetails.Visible = false;
                    dvCreditCardDetails.Visible = true;
                    dvChequeDetails.Visible = false;
                    dvInsuranceDetails.Visible = false;
                    dvFreeDetails.Visible = false;
                    dvChargeToPhysicianDetails.Visible = false;
                    ddlPaymentMode.Focus();
                    break;

                case "Cheque":
                    dvCashDetails.Visible = false;
                    dvCreditCardDetails.Visible = false;
                    dvChequeDetails.Visible = true;
                    dvInsuranceDetails.Visible = false;
                    dvFreeDetails.Visible = false;
                    dvChargeToPhysicianDetails.Visible = false;
                    ddlPaymentMode.Focus();
                    break;

                case "Insurance":
                    dvCashDetails.Visible = false;
                    dvCreditCardDetails.Visible = false;
                    dvChequeDetails.Visible = false;
                    dvInsuranceDetails.Visible = true;
                    dvFreeDetails.Visible = false;
                    dvChargeToPhysicianDetails.Visible = false;
                    ddlPaymentMode.Focus();
                    break;

                case "Free":
                    dvCashDetails.Visible = false;
                    dvCreditCardDetails.Visible = false;
                    dvChequeDetails.Visible = false;
                    dvInsuranceDetails.Visible = false;
                    dvFreeDetails.Visible = true;
                    dvChargeToPhysicianDetails.Visible = false;
                    ddlPaymentMode.Focus();
                    break;

                case "Charge to physician office":
                    dvCashDetails.Visible = false;
                    dvCreditCardDetails.Visible = false;
                    dvChequeDetails.Visible = false;
                    dvInsuranceDetails.Visible = false;
                    dvFreeDetails.Visible = false;
                    dvChargeToPhysicianDetails.Visible = true;
                    ddlPaymentMode.Focus();
                    break;
            }
        }

        private PaymentBO GetPayment()
        {
            PaymentBO payInp = new PaymentBO();
            payInp.SpecimenDetails = new SpecimenInfoBO();
            payInp.SpecimenDetails.TenantID = Convert.ToInt32(Session["OrgID"]);
            payInp.SpecimenDetails.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
            PaymentBO paymBO = new PaymentDA().getPaymentDetails(payInp);
            return paymBO;
        }

        private PaymentBO SavePayment(string payType)
        {
            PaymentBO payBO = new PaymentBO();
            payBO.CashDetails = new CashBO();
            payBO.CreditDetails = new CreditCardBO();
            payBO.chequeDetails = new ChequeBO();
            payBO.InsuranceDetails = new InsuranceBO();
            payBO.SpecimenDetails = new SpecimenInfoBO();            
            payBO.PaymentID = Convert.ToInt32(hPaymentID.Value);
            payBO.SpecimenDetails.TenantID = Convert.ToInt32(Session["OrgID"]);
            payBO.SpecimenDetails.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
            payBO.PaymentMode = ddlPaymentMode.SelectedValue.Trim();
            lblPaymentStatus.Text = payBO.PaymentStatus = ddlPaymentStaus.SelectedValue.Trim();
            switch (payType)
            {
                case "Cash":                    
                    if(txtCash.Text!="")
                            payBO.CashDetails.Cash = decimal.Parse(txtCash.Text.Trim());
                        lblTransactionDate.Text = payBO.CashDetails.TransactionDate = txtTransDate.Text.Trim();
                        lblCash.Text = payBO.CashDetails.Currency = ddlCurrency.SelectedValue.Trim();
                        lblCash.Text += " " + txtCash.Text.Trim();
                        lblDescription.Text = payBO.CashDetails.Description = txtCashDescription.Text.Trim();
                    
                    break;

                case "CreditCard":
                    lblCardType.Text = payBO.CreditDetails.CardType = ddlCardType.SelectedValue.Trim();
                    payBO.CreditDetails.CardNumber = KoSoft.Utility.KoCrypt.Encrypt(txtCardNo1.Text.Trim() + " " + txtCardNo2.Text.Trim() + " " + txtCardNo3.Text.Trim() + " " + txtCardNo4.Text.Trim());
                    lblCardNo1.Text = "xxxx"; lblCardNo2.Text = "xxxx"; lblCardNo3.Text = "xxxx"; lblCardNo4.Text = txtCardNo4.Text.Trim();
                    lblHolderName.Text = payBO.CreditDetails.HolderName = txtHolderName.Text.Trim();
                    payBO.CreditDetails.CVVNumber = KoSoft.Utility.KoCrypt.Encrypt(txtCVV.Text.Trim());
                    lblCVV.Text = "XXX";
                    lblExpireDate.Text = "MM/YY";
                    payBO.CreditDetails.ExpireDate = KoSoft.Utility.KoCrypt.Encrypt(ddlCreditDate.SelectedValue.Trim() + "/" + ddlCreditYear.SelectedValue.Trim());
                    break;

                case "Cheque":
                    lblBankName.Text = payBO.chequeDetails.BankName = txtBankName.Text.Trim();
                    lblBranchName.Text = payBO.chequeDetails.BranchName = txtBranchName.Text.Trim();
                     if(txtCheqNo.Text=="")
                     {
                        payBO.chequeDetails.ChequeNumber = 0;
                     }
                    else
                    {
                        payBO.chequeDetails.ChequeNumber = Convert.ToInt32(txtCheqNo.Text.Trim());
                    }
                    
                    lblCheqNo.Text = txtCheqNo.Text.Trim();
                    lblCheqDate.Text = payBO.chequeDetails.ChequeDate = txtCheqDate.Text.Trim();
                    lblAcctNo.Text = payBO.chequeDetails.AccountNumber = txtAcctNo.Text.Trim();
                    lblRoutNo.Text = payBO.chequeDetails.RoutingNumber = txtRoutNo.Text.Trim();
                    break;

                case "Insurance":
                    lblInsuranceType.Text = payBO.InsuranceDetails.InsuranceType = txtInsuranceType.Text.Trim();
                    lblInsuranceCompany.Text = payBO.InsuranceDetails.InsuranceCompany = txtInsuranceCompany.Text.Trim();
                    lblInsuranceNumber.Text = payBO.InsuranceDetails.InsuranceNumber = txtInsuranceNumber.Text.Trim();                    
                    lblMemberName.Text = payBO.InsuranceDetails.MemberName = txtMemberName.Text.Trim();
                    lblMemberShipNumber.Text = payBO.InsuranceDetails.MemberShipNumber = txtMemberShipNumber.Text.Trim();
                    lblGroupNumber.Text = payBO.InsuranceDetails.GroupNumber = txtGroupNumber.Text.Trim();
                    lblPreAuthCode.Text = payBO.InsuranceDetails.PreAuthCode = txtPreAuthCode.Text.Trim();
                    payBO.InsuranceDetails.PreInsuranceNo = Convert.ToInt32(hPreInsurance.Value);
                    break;

                case "Free":
                    dvUpdate.Visible = false;
                    break;

                case "Charge to physician office":
                    dvUpdate.Visible = false;
                    break;
            }
            PaymentDA payDA = new PaymentDA();
            PaymentBO paymBO = payDA.savePaymentDetails(payBO);
            if(paymBO.PaymentID >0)
                hPaymentID.Value = paymBO.PaymentID.ToString();
            return paymBO;
        }

        private void EditPayment(string payment)
        {
            dvUpdate.Visible = true;            
            aPaymentInformation.Visible = false;                       
            switch (payment)
            {
                case "Cash":
                    dvcashEdit.Visible = true;
                    dvCashView.Visible = false;
                    break;

                case "CreditCard":
                    dvCreditCardEdit.Visible = true;
                    dvCreditCardView.Visible = false;
                    break;

                case "Cheque":
                    dvChequeEdit.Visible = true;
                    dvChequeView.Visible = false;
                    break;

                case "Insurance":
                    dvInsuranceEdit.Visible = true;
                    dvInsuranceView.Visible = false;
                    break;

                case "Free":
                    dvUpdate.Visible = false;
                    break;

                case "Charge to physician office":
                    dvUpdate.Visible = false;
                    break;
            }
        }

        private void ViewPayment(string payment)
        {
            dvUpdate.Visible = false;
            aPaymentInformation.Visible = true;
            
            switch (hPaymentMode.Value)
            {
                case "Cash":
                    dvcashEdit.Visible = false;
                    dvCashView.Visible = true;
                    break;

                case "CreditCard":
                    dvCreditCardEdit.Visible = false;
                    dvCreditCardView.Visible = true;
                    if(lblPaymentStatus.Text  != "Paid")
                    {
                        dvUpdate.Visible = true;
                    }
                    break;

                case "Cheque":
                    dvChequeEdit.Visible = false;
                    dvChequeView.Visible = true;
                    break;

                case "Insurance":
                    dvInsuranceEdit.Visible = false;
                    dvInsuranceView.Visible = true;
                    break;

                case "Free":
                    dvUpdate.Visible = false;
                    break;

                case "Charge to physician office":
                    dvUpdate.Visible = false;
                    break;
            }
        }

        private void ClearPayment(string payment)
        {
            switch (hPaymentMode.Value)
            {
                case "Cash":
                    clearCashInput();
                    break;

                case "CreditCard":
                    clearCreditCardInput();
                    break;

                case "Cheque":
                    clearChequeInput();
                    break;

                case "Insurance":
                    clearInsuranceInput();
                    break;

                case "Free":
                    clearFreeInput();
                    break;

                case "Charge to physician office":
                    clearChargetophysicianInput();
                    break;
            }
        }

        protected void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            ActivePaymentMode();
            if (btnUpdatePayment.Text == "Update")
            {
                //btnUpdatePayment.Text = "Edit Details";
                aPaymentInformation.Visible = true;
                dvUpdate.Visible = false;
                lblPaymentStatus.Text = ddlPaymentStaus.SelectedValue.Trim();
                if (lblPaymentStatus.Text != "Paid")
                    dvUpdate.Visible = true;
                //else
                //{
                //    PaymentBO paymentBO = new PaymentBO();
                //    paymentBO.SpecimenDetails.TenantID = Convert.ToInt32(Session["OrgID"]);
                //    paymentBO.SpecimenDetails.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
                //    new PaymentDA().deletePaymentDetails(paymentBO);
                //}
                    
                switch (ddlPaymentMode.SelectedValue.Trim())
                {
                    case "Cash":
                        dvcashEdit.Visible = false;
                        dvCashView.Visible = true;
                        break;

                    case "CreditCard":
                        dvCreditCardEdit.Visible = false;
                        dvCreditCardView.Visible = true;
                        break;

                    case "Cheque":
                        dvChequeEdit.Visible = false;
                        dvChequeView.Visible = true;
                        break;

                    case "Insurance":
                        dvInsuranceEdit.Visible = false;
                        
                        dvInsuranceView.Visible = true;
                        break;

                    case "Free":
                        dvUpdate.Visible = false;
                        break;

                    case "Charge to physician office":
                        dvUpdate.Visible = false;
                        break;
                }
                PaymentBO paymBO = SavePayment(ddlPaymentMode.SelectedValue.Trim());

                SpecimenInfoBO specimenInfoBO = new SpecimenInfoBO()
                {
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    SpecimenID = Convert.ToInt32(hSpecimenID.Value),
                    PaymentMode = ddlPaymentMode.SelectedValue
                };

                new SpecimenDA().UpdatePaymentMode(specimenInfoBO);
                string SpecimenStatus = "Payment Mode Updated";
                SaveAuditLog(SpecimenStatus);
                popSpecimen();
                viewSpecimenAuditlog();
            }

            //else if(btnUpdatePayment.Text == "Edit Details")
            //{
            //    ddlPaymentStaus.Visible = true;
            //    switch (ddlPaymentMode.SelectedValue)
            //    {
            //        case "Cash":
            //            dvcashEdit.Visible = true;
            //            dvCashView.Visible = false;
            //            btnUpdatePayment.Text = "Update";
            //            string[] money= lblCash.Text.Split(' ');
            //            ddlCurrency.SelectedValue = money[0];
            //            txtCash.Text = money[1];                        
            //            txtTransDate.Text = lblTransactionDate.Text;
            //            txtCashDescription.Text = lblDescription.Text;
            //            break;

            //        case "CreditCard":
            //            dvCreditCardDelete.Attributes.Add("class", "modal fade in");
            //            dvCreditCardDelete.Attributes.Add("style", "display:block");
            //            btnUpdatePayment.Attributes.Add("data - toggle", "modal");
            //            btnUpdatePayment.Attributes.Add("data - target", "#dvCreditCardDelete");
            //            break;

            //        case "Cheque":
            //            dvChequeEdit.Visible = true;
            //            dvChequeView.Visible = false;
            //            btnUpdatePayment.Text = "Update";
            //            if(lblBankName.Text != "Bank Name")
            //                txtBankName.Text = lblBankName.Text;
            //            if (lblBranchName.Text != "Branch Name")
            //                txtBranchName.Text = lblBranchName.Text;
            //            if(lblCheqNo.Text != "Cheq No#")
            //                txtCheqNo.Text = lblCheqNo.Text;
            //            if(lblCheqDate.Text != "Cheque Date")
            //                txtCheqDate.Text = lblCheqDate.Text;
            //            if (lblRoutNo.Text != "Routing No#")
            //                txtRoutNo.Text = lblRoutNo.Text;
            //            if (lblAcctNo.Text != "Account No#")
            //                txtAcctNo.Text = lblAcctNo.Text;

            //            btnUpdatePayment.Text = "Update";
            //            break;

            //        case "Insurance":
            //            dvInsuranceEdit.Visible = true;
            //            dvInsuranceView.Visible = false;
            //            if (lblInsuranceType.Text != "Insurance Type")
            //                txtInsuranceType.Text = lblInsuranceType.Text;
            //            if (lblInsuranceCompany.Text != "Insurance Company")
            //                txtInsuranceCompany.Text = lblInsuranceCompany.Text;
            //            if (lblInsuranceNumber.Text != "Insurance Number")
            //                txtInsuranceNumber.Text = lblInsuranceNumber.Text;
            //            if (lblMemberName.Text != "Member Name")
            //                txtMemberName.Text = lblMemberName.Text;
            //            if (txtMemberShipNumber.Text != "MemberShip Number")
            //                txtMemberShipNumber.Text = lblMemberShipNumber.Text;
            //            if (lblGroupNumber.Text != "Group Number")
            //                txtGroupNumber.Text = lblGroupNumber.Text;
            //            if (lblPreAuthCode.Text != "PreAuthCode")
            //                txtPreAuthCode.Text = lblPreAuthCode.Text;

            //            btnUpdatePayment.Text = "Update";
            //            break;

            //        case "Free":
            //            dvUpdate.Visible = false;
            //            break;

            //        case "Charge to physician office":
            //            dvUpdate.Visible = false;
            //            break;
            //    }                
            //}
        }

        private void clearCashView()
        {
            lblCash.Text = string.Empty; ddlCurrency.SelectedIndex = 0;
            lblTransactionDate.Text = string.Empty; lblDescription.Text = string.Empty;
        }

        private void clearCashInput()
        {
            txtCash.Text = "";
            ddlCurrency.SelectedIndex = 0;
            txtTransDate.Text = "";
            txtCashDescription.Text = "";
        }

        private void clearCreditCardView()
        {
            lblCardNo1.Text = "xxxx"; lblCardNo2.Text = "xxxx";
            lblCardNo3.Text = "xxxx"; lblCardNo4.Text = "xxxx";
            lblCardType.Text = "Card Type"; lblExpireDate.Text = "MM/YY";
            lblHolderName.Text = "Holder Name"; lblCVV.Text = "XXX";
        }

        private void clearCreditCardInput()
        {
            txtCardNo1.Text = string.Empty;
            txtCardNo2.Text = string.Empty;
            txtCardNo3.Text = string.Empty;
            txtCardNo4.Text = string.Empty;
            ddlCreditDate.SelectedValue = string.Empty;
            ddlCreditYear.SelectedValue = string.Empty;
            ddlCardType.SelectedValue = string.Empty;
            txtHolderName.Text = string.Empty;
            txtCVV.Text = string.Empty;
        }
        
        private void clearChequeView()
        {
            lblBankName.Text = "Bank Name"; lblBranchName.Text = "Branch Name";
            lblCheqNo.Text = "Cheq No#"; lblCheqDate.Text = "Cheque Date";
            lblRoutNo.Text = "Routing No#"; lblAcctNo.Text = "Account No#";            
        }

        private void clearChequeInput()
        {
            txtBankName.Text = ""; txtBranchName.Text = "";
            txtCheqNo.Text = ""; txtCheqDate.Text = "";
            txtRoutNo.Text = ""; txtAcctNo.Text = "";
        }

        private void clearInsuranceView()
        {
            lblInsuranceType.Text = "Insurance Type"; lblInsuranceNumber.Text = "Insurance Number";
            lblInsuranceCompany.Text = "Insurance Company"; lblMemberName.Text = "Member Name";
            lblMemberShipNumber.Text = "MemberShip Number"; lblGroupNumber.Text = "Group Number";
            lblPreAuthCode.Text = "PreAuthCode";
        }

        private void clearInsuranceInput()
        {
            txtInsuranceType.Text = ""; txtInsuranceNumber.Text = "";
            txtInsuranceCompany.Text = ""; txtMemberName.Text = "";
            txtMemberShipNumber.Text = ""; txtGroupNumber.Text = "";
            txtPreAuthCode.Text = "";
        }

        private void clearFreeView()
        {
            //Data will be add in future
        }

        private void clearFreeInput()
        {
            //Data will be add in future
        }

        private void clearChargetophysicianView()
        {
            //Data will be add in future
        }

        private void clearChargetophysicianInput()
        {
            //Data will be add in future
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            aPaymentInformation.Visible = false;
            PaymentDA payDA = new PaymentDA();
            PaymentBO payBO = new PaymentBO();
            payBO.CashDetails = new CashBO();
            payBO.CreditDetails = new CreditCardBO();
            payBO.chequeDetails = new ChequeBO();
            payBO.InsuranceDetails = new InsuranceBO();
            payBO.SpecimenDetails = new SpecimenInfoBO();

            payBO.SpecimenDetails.TenantID = Convert.ToInt32(Session["OrgID"]);
            payBO.SpecimenDetails.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
            payDA.deletePaymentDetails(payBO);
            dvUpdate.Visible = true;
            //btnUpdatePayment.Text = "Update";
            dvCreditCardDelete.Visible = false;
            dvCreditCardDelete.Attributes.Add("class", "modal fade");
            dvCreditCardDelete.Attributes.Add("style", "display:none");
            dvCreditCardEdit.Visible = true;
            dvCreditCardView.Visible = false;
            ddlPaymentMode.Focus();
            clearCreditCardInput();
        }

        private void BillAddDisable()
        {
            txtBillStr.ReadOnly = true; txtBillCity.ReadOnly = true;
            txtBillState.ReadOnly = true; txtBillCountry.ReadOnly = true;
            txtBillZip.ReadOnly = true;
        }

        private void clearBillAdd()
        {
            txtBillStr.Text = string.Empty; txtBillCity.Text = string.Empty;
            txtBillState.Text = string.Empty; txtBillCountry.Text = string.Empty;
            txtBillZip.Text = string.Empty;
        }

        private void BillAddEnable()
        {
            txtBillStr.ReadOnly = false; txtBillCity.ReadOnly = false;
            txtBillState.ReadOnly = false; txtBillCountry.ReadOnly = false;
            txtBillZip.ReadOnly = false;
            txtBillStr.Focus();
        }

        private OrderBO getOrderDetails()
        {
            SpecimenInfoBO spBO = new SpecimenInfoBO();
            spBO.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
            OrderBO orderBO = new OrderBO()
            {
                specimenInfoBO = spBO,
            };
            SpecimenDA specimenDA = new SpecimenDA();
            OrderBO rtnOrderBO =  specimenDA.GetOrder(orderBO);
            return rtnOrderBO;
        }

        protected void ddlBillAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBillAddress.Focus();
            btnUpdateAdd.Visible = true;
            dvBillAddEdit.Visible = true;
            dvBillAddEdit.Focus();            
            lblBillInfo.Text = "";
            ActivePaymentMode();
            
            switch (ddlBillAddress.SelectedValue.Trim())
            {
                case "Patient Address":                    
                    if(ltrLocation.Text.Trim() != "")
                    {
                        BillAddDisable();
                        string[] add = ltrLocation.Text.Trim().Split(',');
                        if (add[0] != "" && add[0] != " ")
                            txtBillStr.Text = add[0];
                        if (add[1] != "" && add[1] != " ")
                            txtBillCity.Text = add[1];
                        if (add[2] != "" && add[2] != " ")
                            txtBillState.Text = add[2];
                        if (add[3] != "" && add[3] != " ")
                            txtBillZip.Text = add[3];
                        if (add[4] != "" && add[4] != " ")
                            txtBillCountry.Text = add[4];

                        if (hBillAddSameAs.Value != "Patient Address")
                            btnUpdateAdd.Text = "Billing Updates";
                    }
                    else
                    {
                        lblBillInfo.Text = ddlBillAddress.SelectedValue + " is already empty";
                        lblBillInfo.Style.Add("color", "Red");
                        dvBillAddEdit.Visible = false;
                        btnUpdateAdd.Visible = false;
                    }
                    break;

                case "Guardian Address":
                    if (ltrGuardianAddress.Text.Trim() != "")
                    {                     
                        BillAddDisable();
                        string[] add = ltrGuardianAddress.Text.Trim().Split(',');
                        if (add[0] != "" && add[0] != " ")
                            txtBillStr.Text = add[0];
                        if (add[1] != "" && add[1] != " ")
                            txtBillCity.Text = add[1];
                        if (add[2] != "" && add[2] != " ")
                            txtBillState.Text = add[2];
                        if (add[3] != "" && add[3] != " ")
                            txtBillZip.Text = add[3];
                        if (add[4] != "" && add[4] != " ")
                            txtBillCountry.Text = add[4];

                        if (hBillAddSameAs.Value != "Guardian Address")                        
                            btnUpdateAdd.Text = "Billing Updates";
                    }
                    else
                    {
                        lblBillInfo.Text = ddlBillAddress.SelectedValue + " is already empty";
                        lblBillInfo.Style.Add("color", "Red");                        
                        dvBillAddEdit.Visible = false;
                        btnUpdateAdd.Visible = false;
                    }
                    break;

                case "Physician Address":
                    if (ltrReqAddress.Text != "")
                    {
                        BillAddDisable();
                        string[] add = ltrReqAddress.Text.Trim().Split(',');
                        if (add[0] != "" && add[0] != " ")
                            txtBillStr.Text = add[0];
                        if (add[1] != "" && add[1] != " ")
                            txtBillCity.Text = add[1];
                        if (add[2] != "" && add[2] != " ")
                            txtBillState.Text = add[2];
                        if (add[3] != "" && add[3] != " ")
                            txtBillZip.Text = add[3];
                        if (add[4] != "" && add[4] != " ")
                            txtBillCountry.Text = add[4];

                        if (hBillAddSameAs.Value != "Guardian Address")                        
                            btnUpdateAdd.Text = "Billing Updates";
                    }
                    else
                    {
                        lblBillInfo.Text = ddlBillAddress.SelectedValue + " is already empty";
                        lblBillInfo.Style.Add("color", "Red");
                        dvBillAddEdit.Visible = false;
                        btnUpdateAdd.Visible = false;
                    }
                    break;

                case "Others":
                    BillAddDisable();
                    if (hBillAddSameAs.Value != "Others")
                    {
                        clearBillAdd();
                        BillAddEnable();
                    }
                    else
                    {
                        btnUpdateAdd.Text = "Edit";
                    }
                    break;
            }
        }

        protected void btnUpdateAdd_Click(object sender, EventArgs e)
        {            
            lblBillInfo.Text = "";
            ActivePaymentMode();
            if(btnUpdateAdd.Text == "Billing Updates")
            {
                if (txtBillCity.Text == "")
                {
                    BillAddEnable();
                    lblBillInfo.Text = "Fill the Address";
                    lblBillInfo.Style.Add("color", "Red");
                }
                else
                {
                    SpecimenDA specimenDA = new SpecimenDA();
                    SpecimenInfoBO spBO = new SpecimenInfoBO();
                    spBO.SpecimenID = Convert.ToInt32(hSpecimenID.Value);
                    OrderBO orderBO = new OrderBO()
                    {
                        specimenInfoBO = spBO,
                        BillAddress1 = txtBillStr.Text,
                        BillCity = txtBillCity.Text,
                        BillState = txtBillState.Text,
                        BillCountry = txtBillCountry.Text,
                        BillZipCode = txtBillZip.Text,
                        BillAddSameAs = ddlBillAddress.SelectedValue.Trim(),
                    };

                    specimenDA.UpdateOrder(orderBO);
                    BillAddDisable();
                    lblBillInfo.Text = "Address saved successfully";
                    lblBillInfo.Style.Add("color", "Green");
                    btnUpdateAdd.Text = "Edit";
                }
            }
            else if(btnUpdateAdd.Text == "Edit")
            {
                btnUpdateAdd.Text = "Billing Updates";
                BillAddEnable();
            }
        }

        protected void aResultsPreview_ServerClick(object sender, EventArgs e)
        {                      
            TestResultsBO resultBO = new TestResultsDA().getResults(Convert.ToInt32(Session["OrgID"]), int.Parse(hSpecimenID.Value));
            if (ancRecResults.InnerText == "Click here for recording results")
            {
                Response.Redirect("~/pages/ResultRecording?si=" + hSpecimenID.Value + "&" + "mode=edit" + "&" + "sn=" + ltrSpecimenNumber.Text);
            }
            else
            {
                Response.Redirect("~/pages/ResultRecording?ri=" + resultBO.ResultID + "&" + "si=" + hSpecimenID.Value + "&" + "mode=view" + "&" + "sn=" + ltrSpecimenNumber.Text);
            }
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + hDocNumber.Value + ".pdf");
            Response.TransmitFile(Server.MapPath("/Docs/Results/" + hDocNumber.Value + ".pdf"));
            Response.End();
        }

        protected void ddlRequestedTest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rpSpecimenAuditlog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AuditLogBO oaudit = (AuditLogBO)e.Item.DataItem;
                Literal ltr = (Literal)e.Item.FindControl("ltrActionName");
                Literal ltric = (Literal)e.Item.FindControl("ltricon");
                if (oaudit.EntityType== "Specimen")
                {
                    if (oaudit.ActionName == "Specimen Information Updated" ||oaudit.ActionName == "Specimen Created with Exception" || oaudit.ActionName == "Specimen Created" || oaudit.ActionName == "Specimen Created with Exception")
                    {
                        ltr.Text =oaudit.ActionName;
                        ltric.Text = "<i class=\"fa fa-flask\" style=font-size:18px; color:red;></i>&nbsp;";
                    }

                    if (oaudit.ActionName == "Physician Information Updated")
                    {
                        ltr.Text = oaudit.ActionName;
                        ltric.Text = "<i class=\"fa fa-user-md\" style=font-size:18px; color:#414144bf;></i>&nbsp;";

                    }
                    if (oaudit.ActionName == "Patient Information Updated")
                    {
                        ltr.Text = oaudit.ActionName;
                        ltric.Text = "<i class=\"fa fa-user\" style=font-size:18px; color:#414144bf;></i>&nbsp;";
                    }
                    if (oaudit.ActionName == "Payment Mode Updated")
                    {
                        ltr.Text = oaudit.ActionName;
                        ltric.Text = "<i class=\"fa fa-dollar\" style=font-size:18px;></i>&nbsp;";
                    }
                    if (oaudit.ActionName == "Verification Updated with Exception" || oaudit.ActionName == "Verification Updated")
                    {
                        
                        ltr.Text = oaudit.ActionName;
                        ltric.Text = "<i class=\"fa fa-check\" style=font-size:18px;></i>&nbsp;";
                    }


                }
                
                 //ltr.Text = "<i class=\"fa fa-flask\"></i>&nbsp;" + oaudit.ActionName;

                Literal ltron = (Literal)e.Item.FindControl("ltrActionByon");

                ltron.Text = "<span style=margin-left:-2px;>" + oaudit.ActionOn+"<br/>" + "by :"  + oaudit.ActionBy;
               

            }
        }

        protected void rptAssayDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AssayGroupBO AssayBO = (AssayGroupBO)e.Item.DataItem;
                LinkButton lbtnAssayNo = (LinkButton)e.Item.FindControl("lbtnAssayNumber");
                Literal ltrAssay = (Literal)e.Item.FindControl("ltrAssayType");
                Literal ltrSpeCount = (Literal)e.Item.FindControl("ltrSpecimenCount");
                Literal ltrAssStatus = (Literal)e.Item.FindControl("ltrAssayStatus");                
            }
        }

        protected void rptAssayDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ActiveAssay();
                LinkButton lbtnAssNo = (LinkButton)e.Item.FindControl("lbtnAssayNumber");

                if (e.CommandName == "AssayNo")
                {
                    Response.Redirect("~/pages/assaydetail?ai=" + e.CommandArgument.ToString() + "&&an=" + lbtnAssNo.Text);
                }
            }
                
        }

        protected void btnAssayName_Click(object sender, EventArgs e)
        {
            dvAssayName.Style.Add("class", "modal fade");
            dvAssayName.Attributes.Add("style", "display:none");
            //SpecimenDA specDA = new SpecimenDA();
            //specDA.AddAssayName(Convert.ToInt32(Session["OrgID"]), txtAssayName.Text, "Current");
            int AssayID = (new SpecimenDA()).AddSpecimenToAssay(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hSpecimenID.Value), ddlAssayType.SelectedValue.Trim(), txtAssayName.Text);
            hAssayID.Value = Convert.ToString(AssayID);
            txtAssayName.Text = string.Empty;
            popSpecimen();
        }

        protected void chkPreAuthorization_CheckedChanged(object sender, EventArgs e)
        {
            
                if (txtInsuranceNumber.Text != "")
                {
                    PreInsurance preinsur = (new CustomerDA().GetPreInsurance(txtInsuranceNumber.Text.ToString()));
                      
                    
                    if (preinsur.InsuranceCard_IDno != null)
                    {
                      hPreInsurance.Value = preinsur.PreInsuranceNo.ToString();
                        lblPreInsurance.Text = "Pre-Authorization form has been submitted";
                      lblPreInsurance.Attributes.Add("style", "color:#438e43");
                    }
                    else
                    {
                        lblPreInsurance.Text = "Pre-Authorization form not submitted";
                    chkPreAuthorization.Checked = false;
                }
                }
                else
                {
                    lblPreInsurance.Text = "Please Enter the Insurance Number ";
                     chkPreAuthorization.Checked = false;
                }
            
        }

        //private void EmailToken()
        //{
        //    DocumentBO objdoc = new DocumentBO();
        //    objdoc.DocID = Convert.ToInt32(hDocID.Value);
        //    objdoc.TokenID = Guid.NewGuid().ToString();
        //    objdoc.DocNumber = ancPdfGen.HRef;
        //    objdoc.TenantID = Convert.ToInt32(Session["OrgID"]);
        //    DocumentDA objDA = new DocumentDA();
        //    hEmailToken.Value= objDA.UpdateDocNum(objdoc);
        //}

    }
}
