using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace STOMS.UI.pages
{
    public partial class Specimen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                clearAll();
                Session["PgContentTitle"] = "New Specimen Entry Form";
                if (Session["SpecimenNo"] != null)
                {
                    ltrSpecimenNumber.Text = Convert.ToString(Session["SpecimenNo"]);
                    Session.Remove("SpecimenNo");
                }
                rpGenSpecimenNos.DataSource = (new SpecimenDA()).GetGeneratedSpecimenNos(Convert.ToInt32(Session["OrgID"]));
                rpGenSpecimenNos.DataBind();
                //getAutoGenerateSpNum();
                getTodayRequests();
                lblDrawnTime.InnerText = "Drawn Time";
                lblDrawnDate.InnerText = "Drawn Date";
                setTestType();                
            }
        }

        private void  setTestType()
        {
            if (Convert.ToInt32(Session["OrgID"]) == 2)
            {
                ddlRequest.SelectedValue = "Frat";
                ddlRequest.Items.Remove(new ListItem()
                {
                    Text = "Mitochondrial Dysfunction Test",
                    Value = "Mitochondrial Dysfunction Test",
                });
                TriggerDDLRequest();

            }
            else if (Convert.ToInt32(Session["OrgID"]) == 4)
            {
                ddlRequest.SelectedValue = "Mitochondrial Dysfunction Test";
                ddlRequest.Items.Remove(new ListItem()
                {
                    Text = "FRAT",
                    Value = "Frat",
                });
                TriggerDDLRequest();
            }
        }
       
        private void getAutoGenerateSpNum()
        {
            btnSpecimenNum.Visible = false;
            ConfigurationBO configBO = new ActiveClientConfigurationDA().GetConfiguration(Convert.ToInt32(Session["OrgID"]), "AutoGenerateSpecimenNumbers");
            if(configBO.ConfigValue == "Yes")
            {
                generateSpecNo();
                dvltrSpeNum.Attributes.Add("class", "well");
                dvltrSpeNum.Attributes.Add("width", "72%");
                dvbtnNewSpecimen.Attributes.Add("width", "col-lg-0");
            }
            else
            {
                btnSpecimenNum.Visible = true;
                ltrSpecimenNumber.Text = "Sample #";
                dvltrSpeNum.Attributes.Add("class", "well");
                dvltrSpeNum.Attributes.Add("width", "72%");
                dvbtnNewSpecimen.Attributes.Add("class", "col-lg-3");
            }   
        }

        private void generateSpecNo()
        {
            showMessage.Visible = false;
            spMsg.InnerText = "";
            List<SpecimenInfoBO> osNumber = (new SpecimenDA()).GetNextSpecimenNo(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["UserID"]));
            if (osNumber.Count > 0)
            {
                if (Convert.ToInt32(Session["OrgID"]) == 4)
                {
                    //MTP170001
                    //2017MC00001
                    String s = osNumber[0].SpecimenNumber.Remove(0, 4);
                    s = s.Remove(4,1);
                    s = s.Insert(3, "17");
                    osNumber[0].SpecimenNumber = s;
                }
                ltrSpecimenNumber.Text = osNumber[0].SpecimenNumber;
            }
        }

        protected void btnSpecimenNum_Click(object sender, EventArgs e)
        {
            showMessage.Visible = false;
            spMsg.InnerText = ""; 
            List<SpecimenInfoBO> osNumber = (new SpecimenDA()).GetNextSpecimenNo(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["UserID"]));
            if (osNumber.Count > 0)
                ltrSpecimenNumber.Text = osNumber[0].SpecimenNumber;
        }

        //protected void btnApplication_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/pages/st");
        //}           

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            SpecimenDA oSpecimenDA = new SpecimenDA();
            string strMsg = string.Empty;
            string status = string.Empty;
            showMessage.setErrorMsg(strMsg);
            //if (ltrSpecimenNumber.Text == "" || ltrSpecimenNumber.Text == "Sample #")
            //    strMsg = "Specimen # is not generated";
            if (txtFirstName.Text.Trim() == "" && txtLastName.Text.Trim() == "")
                strMsg = strMsg + (strMsg != "" ? "<br/>" : "") + "Patient's First Name and Last Name cannot be empty";

            string sReasons = string.Empty;
            //if (chkConsentProvided.Checked || chkRequisition.Checked || chkOthers.Checked || chkAcceptTest.Checked)
            // {
            //    status = "Pending";
            //}

            //if(!chkConsentProvided.Checked ||  !chkOthers.Checked || !chkRejection.Checked)
            //{
            //    status = "Received";
            //}

            //if (!chkConsentProvided.Checked ||!chkRequisition.Checked||chkOthers.Checked)
            //    status = "Pending";            
            if (chkRejection.Checked==true)
            {
                status = "Rejected";
                for (int idx = 0; idx < ddlRejectReason.Items.Count; idx++)
                {
                    if (ddlRejectReason.Items[idx].Selected)
                    {
                        if (ddlRejectReason.Items[idx].Value == "Others")
                            sReasons += "Others: " + txtOtherRejectReason.Text + ", ";
                        else
                            sReasons += Convert.ToString(ddlRejectReason.Items[idx].Value) + ",";
                    }
                }

                if (sReasons == String.Empty)
                {
                    strMsg = strMsg + (strMsg != "" ? "<br/>" : "") + "If specimen is not acceptable for testing, please select reason for rejection";
                }
                else
                {
                    sReasons = sReasons.Remove(sReasons.Length - 1);
                }
            }

            else
            {
                status = "Received";
            }

            //if (chkRejection.Checked && chkConsentProvided.Checked  && !chkOthers.Checked)
            //{
            //    status = "Received";
            //}

            //if (!chkConsentProvided.Checked)
            //    strMsg = strMsg + (strMsg != "" ? "<br/>" : "") + "Without consent request cannot be processed";
            //if (!chkRequisition.Checked)
            //    strMsg = strMsg + (strMsg != "" ? "<br/>" : "") + "Confirm if request is complete";

            if (strMsg != "")
            {
                showMessage.setErrorMsg(strMsg);
                btnSave.Enabled = true;
                showMessage.Visible = true;
            }
            else
            {
                showMessage.Visible = false;
                string _Gender = "";
                if (optM.Checked)
                    _Gender = "M";
                else if (optF.Checked)
                    _Gender = "F";
                else if (optUn.Checked)
                    _Gender = "UnKnown";

                int PatientID = oSpecimenDA.SavePatientInfoDA(new PatientBO
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Gender = _Gender,
                    //Gender = (optF.Checked ? "F" : "M"),
                    DOB = txtDOB.Text.Trim(),
                    PatientID = 0,
                    CreatedBy = Convert.ToInt32(Session["UserID"]),
                    TenantID = Convert.ToInt32(Session["OrgID"]),
                    GuardianRelationship = ""
                });

                /* 
                 * Upload FileMeta to Database 
                 * Upload HardCopy to Storage
                 */
                //Begning
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
                    flSampleHardCopy.SaveAs(Server.MapPath("~/Docs/RequestForm/") + DocName + "." + fileName.Substring(fileName.LastIndexOf('.') + 1));
                }
                //End
                //SpecimenStatus = status==string.Empty?(chkAcceptTest.Checked ? "Received" : "Rejected"):"Pending",
                getAutoGenerateSpNum();
                SpecimenInfoBO specDetails = new SpecimenInfoBO();
                specDetails.PatientID = PatientID;
                hPatientID.Value = specDetails.PatientID.ToString();
                specDetails.SpecimenNumber = ltrSpecimenNumber.Text;
                if (ddlRequest.SelectedIndex >0)
                {
                    specDetails.SampleReceiveDate = txtSampleReceiveDate.Text;
                    specDetails.ReceivedTime = txtReceivedTime.Text;
                    specDetails.DateDrawn = txtDrawndate.Text;
                    specDetails.TimeDrawn = txtDrawnTime.Text;
                    specDetails.TransitTime = txtTransitTime.Text;                    
                    specDetails.TransitTemperature = ddlTransitTemp.SelectedItem.Text;
                    if (ddlSpecimenType.SelectedItem.Text == "Blood")
                        specDetails.VolumeReceived = txtVolReceived.Text != string.Empty ? txtVolReceived.Text : string.Empty;
                    else
                        specDetails.VolumeReceived = txtNoOfSwab.Text != string.Empty ? txtNoOfSwab.Text : string.Empty;
                    specDetails.unit = ddlVolReceivedML.SelectedItem.Text;
                    specDetails.InterSubstance = txtPotInterfer.Text;
                    specDetails.SpecimentType = ddlSpecimenType.SelectedValue;
                    specDetails.TestType = ddlRequest.SelectedValue;
                }
                else
                {
                    specDetails.SampleReceiveDate = "";
                    specDetails.ReceivedTime = "";
                    specDetails.DateDrawn = "";
                    specDetails.TimeDrawn = "";
                    specDetails.TransitTime = "";
                    //var x = txtTransitTemp;
                    specDetails.TransitTemperature = "";
                    specDetails.VolumeReceived = "";
                   // if (ddlSpecimenType.SelectedItem.Text == "Blood")
                     //   specDetails.VolumeReceived = txtVolReceived.Text != string.Empty ? txtVolReceived.Text : string.Empty;
                    //else
                      //  specDetails.VolumeReceived = txtNoOfSwab.Text != string.Empty ? txtNoOfSwab.Text : string.Empty;
                    specDetails.unit = "";
                    specDetails.InterSubstance ="";
                    specDetails.SpecimentType = "";
                }
               
                specDetails.IsConsent = chkConsentProvided.Checked;
                //specDetails.IsReqComplete = chkAcceptTest.Checked;
                specDetails.IsSpecimenAccept = chkAcceptTest.Checked;
                specDetails.IsRejection = chkRejection.Checked;
                specDetails.SpecimenStatus = status;
                specDetails.RejectReasons = sReasons;
                specDetails.PendingReasons = txtPendingOthers.Text.Trim();
                specDetails.ReqFormCopyID = Convert.ToInt32(hDocId.Value);
                specDetails.CreatedBy = Convert.ToInt32(Session["UserID"]);
                specDetails.TenantID = Convert.ToInt32(Session["OrgID"]);
                specDetails.PaymentMode = rdoPaymentMode.SelectedValue;
                SpecimenInfoBO SpErrorMes = oSpecimenDA.CreateSpecimenInfo(specDetails);
                {
                    hSpecimenID.Value = SpErrorMes.SpecimenID.ToString();
                }
                SpecimenInfoBO objOrder = new SpecimenInfoBO();
                {
                    objOrder.TenantID = Convert.ToInt32(Session["OrgID"]);
                    objOrder.PatientID= Convert.ToInt32(hPatientID.Value);
                    objOrder.SpecimenID = Convert.ToInt32(hSpecimenID.Value);                    
                    SpecimenDA objSpecimenDA = new SpecimenDA();
                    objSpecimenDA.SaveOrder(objOrder);
                }
                SaveAuditLog();
                //SampleList1.popSampleList("");
                //SampleList1.IsLoad = true;
                //SampleList1.popAssaySamples();
                btnSave.Enabled = true;
                getTodayRequests();
                
                if (SpErrorMes.SpecimenError == "")
                {
                    //spMsg.InnerText = "Information Saved Successfully";
                    dvSaveMsg.Attributes.Add("class", "modal fade in");
                    dvSaveMsg.Attributes.Add("style", "display:block");
                    ltrGeneratedSpecimenNumber.Text = ltrSpecimenNumber.Text;
                    clearAll();
                    ltrSpecimenNumber.Text = "Sample #";
                }
                else
                {
                    spMsg.InnerText = SpErrorMes.SpecimenError;
                }
               
                //Response.Redirect("~/pages/receiving");
                //Specimen received from kit
                //if (chkSpecimenReceivedKit.Checked)
                //{
                //    KitOrderBO kitordBO = new SpecimenReceivedKitDA().getSpecimenReceivedKitDetails(Convert.ToInt32(Session["OrgID"]), txtKitNumber.Text);
                //    string kitno = kitordBO.KitNumber;
                //    if (chkReuseCountKit.Checked)
                //    {
                //        int reusevalue = kitordBO.ReUseCount + 1;
                //        new SpecimenReceivedKitDA().UpdateStausandReuseCount(Convert.ToInt32(Session["OrgID"]), txtKitNumber.Text.Trim(), "InStock", reusevalue);
                //    }
                //    else
                //    {
                //        new SpecimenReceivedKitDA().UpdateDestroyStaus(Convert.ToInt32(Session["OrgID"]), txtKitNumber.Text.Trim(), "Destroyed", DateTime.Now);
                //    }                //}
                //chkSpecimenReceivedKit.Checked = false;
                //txtKitNumber.Text = "";
                //chkReuseCountKit.Checked = false;
            }            
        }

        public void SaveAuditLog()
        {
            string SpecimenStatus = "";
            if (chkOthers.Checked)
            {
              SpecimenStatus = "Specimen Created with Exception";
            }
            else
            {
              SpecimenStatus = "Specimen Created";
            }

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
        private void clearAll()
        {
            ltrSpecimenNumber.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtDOB.Text = "";
            txtDrawndate.Text = "";
            txtDrawnTime.Text = "";
            txtTransitTime.Text = "";
            ddlTransitTemp.Text = "";
            txtVolReceived.Text = "";
            txtPotInterfer.Text = "";            
            txtSampleReceiveDate.Text = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year; //DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            txtReceivedTime.Text = "";
            chkConsentProvided.Checked = false;
            chkAcceptTest.Checked = false;
            chkRejection.Checked = false;
            chkOthers.Checked = false;
            ddlRejectReason.ClearSelection();
            rdoPaymentMode.ClearSelection();
            txtPendingOthers.Text = "";
            chkClosePatient.Checked = false;
        }

        protected void rpGenSpecimenNos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SetSpNumber")
            {
                ltrSpecimenNumber.Text = Convert.ToString(e.CommandArgument);
                e.Item.Visible = false;
                spMsg.InnerText = "";
                showMessage.Visible = false;
            }
            else
            {
            }
        }

        protected void btnGenNum_Click(object sender, EventArgs e)
        {
           Response.Redirect("~/pages/GenNumbers");
        }

        protected void txtVolReceived_TextChanged(object sender, EventArgs e)
        {
            if (txtVolReceived.Text != "")
            {
                if (Regex.IsMatch((txtVolReceived.Text).Trim(), @"^\d*\.?\d*$") || Regex.IsMatch((txtVolReceived.Text).Trim(), @"^\d*\.?\d*ML$") || Regex.IsMatch((txtVolReceived.Text).Trim(), @"^\d*\.?\d*ml$") || Regex.IsMatch((txtVolReceived.Text).Trim(), @"^\d*\.?\d*Ml$"))
                {
                    showMessage.FindControl("ContentPlaceHolder1_showMessage_dvErrorDetail").Visible = false;
                }
                else
                {
                    showMessage.setErrorMsg("Volumn Received should be in MLs", "D");
                }
            }
        }

        protected void fileUploadHandler_ValueChanged(object sender, EventArgs e)
        {
        }

        protected void ddlSpecimenType_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(ddlSpecimenType.SelectedItem.Text == "Blood")
           {
                dvVolumeReceived.Visible = true;
                dvNoOfSwabCount.Visible = false;
                lblDrawnTime.InnerText = "Drawn Time";
                lblDrawnDate.InnerText = "Drawn Date";
            }
           else if(ddlSpecimenType.SelectedItem.Text == "Mouth Swab")
           {
                dvNoOfSwabCount.Visible = true;
                dvVolumeReceived.Visible = false;
                lblDrawnTime.InnerHtml = "&nbsp;";
                lblDrawnDate.InnerText = "Swab Date & Time";
            }
            ddlSpecimenType.Focus();
        }

        protected void ancNoOfReqToday_ServerClick(object sender, EventArgs e)
        {
            if(ancNoOfReqToday.InnerText != "0")
            {
                Session["TodayRequests"] = true;
                Response.Redirect("~/pages/st");
            }
        }

        private int getTodayRequests()
        {
            int todayRequests;
            SpecimenDA spDetDA = new SpecimenDA();
             todayRequests = spDetDA.getTodayRequests(Convert.ToInt32(Session["OrgID"]));
            
            if(todayRequests==0)
            {
                ancNoOfReqToday.InnerText ="000" + todayRequests;
                ancNoOfReqToday.Attributes.Add("style", "pointer-events:none");
                ancNoOfReqToday.Attributes.Add("cursor", "default");
                //ancNoOfReqToday.Attributes.Add("class", "tooltips");
                //ancNoOfReqToday.Attributes.Add("title", "No requests came");
                //ancNoOfReqToday.Attributes.Add("data-placement", "right");
                //ancNoOfReqToday.Attributes.Add("data-trigger", "hover");
            }
            else
            {
                ancNoOfReqToday.Attributes.Add("style", "pointer-events:auto");
                ancNoOfReqToday.Attributes.Add("cursor", "default");

                if (todayRequests < 10)
                    ancNoOfReqToday.InnerText = "000" + todayRequests;
                else if (todayRequests < 100)
                    ancNoOfReqToday.InnerText = "00" + todayRequests;
                else if (todayRequests < 1000)
                    ancNoOfReqToday.InnerText = "0" + todayRequests;
            }
            return todayRequests;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            clearAll();
            dvSpecimenData.Attributes.Add("style", "display:none");
            dvSpecimenType.Attributes.Add("style", "display:none");
            ddlRequest.SelectedIndex = 0;
            dvSaveMsg.Attributes.Add("class", "modal fade");
            dvSaveMsg.Attributes.Add("style", "display:none");
            setTestType();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Specimen?sn="+ltrGeneratedSpecimenNumber.Text);
        }

        protected void ddlRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerDDLRequest();
        }

        private void TriggerDDLRequest()
        {
            ListItemCollection Fratcollection = new ListItemCollection()
            {
                 new ListItem("Blood","Blood"),
                 new ListItem("Serum","Serum"),
                 new ListItem("Plasma","Plasma"),
                 new ListItem("Unknown","Unknown")
            };

            ListItemCollection Swabcollection = new ListItemCollection()
            {
                 new ListItem("Mouth Swab","MouthSwab"),
                 new ListItem("Unknown","Unknown")

            };
            dvSpecimenType.Attributes.Add("style", "display:block");
            ddlSpecimenType.Focus();
            dvSpecimenData.Attributes.Add("style", "display:block");
            txtNoOfSwab.Text = "";
            txtVolReceived.Text = "";
            if (ddlRequest.SelectedItem.Text == "FRAT")
            {
                ddlSpecimenType.DataSource = Fratcollection;
                ddlSpecimenType.DataBind();
                dvVolumeReceived.Visible = true;
                dvNoOfSwabCount.Visible = false;
                //ddlSpecimenType.Items.Add("Blood");
                //ddlSpecimenType.Items.Add("Serum");
                //ddlSpecimenType.Items.Remove("Mouth Swab");
                lblDrawnTime.InnerText = "Drawn Time";
                lblDrawnDate.InnerText = "Drawn Date";
            }
            else
            {
                ddlSpecimenType.DataSource = Swabcollection;
                ddlSpecimenType.DataBind();
                //ddlSpecimenType.Items.Remove("Blood");
                //ddlSpecimenType.Items.Remove("Serum");
                //ddlSpecimenType.Items.Add("Mouth Swab");
                dvNoOfSwabCount.Visible = true;
                dvVolumeReceived.Visible = false;
                lblDrawnTime.InnerHtml = "&nbsp;";
                lblDrawnDate.InnerText = "Swab Date & Time";
            }
        }
    }
}
