<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="SpecimenDetail.aspx.cs" Inherits="STOMS.UI.pages.SpecimenDetail" %>

<%@ Register Src="~/usercontrol/AssayInfo.ascx" TagPrefix="uc1" TagName="AssayInfo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <%-- Updated Date and Time Stylesheets Start --%>
    <link href="../style/js/plugins/datetimepicker/bootstrap/css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <%-- Updated Date and Time Stylesheets Start --%>
    <style>
        .ui-timepicker-container .ui-timepicker-standard {
            z-index: 99999 !important;
        }
          
   .fileUpload {
    position: relative;
    overflow: hidden;
    margin: 10px;
}

    .fileUpload input.upload {
        position: absolute;
        top: 0;
        right: 0;
        margin: 0;
        padding: 0;
        font-size: 20px;
        cursor: pointer;
        opacity: 0;
        filter: alpha(opacity=0);
    }
        /*.label {
            font-weight: normal;
            font-size: 20px;
        }*/

    </style>
    <script src="../style/js/plugins/datetimepicker/jquery/jquery-1.8.3.min.js"></script>
    <%--<script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>

    <%-- Updated Date and Time Scripts Start --%>
<%--    <script src="../style/js/plugins/datetimepicker/bootstrap/js/bootstrap.js"></script>--%>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.min.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/locales/bootstrap-datetimepicker.fr.js"></script>
    <%-- Updated Date and Time Scripts Start --%>

    <div class="row">
  <asp:TextBox ID="txtSpcimenNum" runat="server"  placeholder="Enter the Specimen Number" style="height: 50px; text-align: center; width: 36%; margin-left:41px" ValidationGroup="vdgSpcimenNum"></asp:TextBox>
            <asp:Button ID="btnSpcimenNum" CssClass="btn btn-success"  runat="server" Text="View" OnClick="btnSpcimenNum_Click" style="height:50px; text-align: center;width: 15%; margin-left: 20px;" ValidationGroup="vdgSpcimenNum" />
            <asp:RequiredFieldValidator ValidationGroup="vdgSpcimenNum" ID="reqSpcimenNum" ControlToValidate="txtSpcimenNum" runat="server" ForeColor="Red" errormessage="Please enter Specimen Number!"></asp:RequiredFieldValidator>
        <br />
                <asp:Label ID="lblSpcimenNum" runat="server" Style="font-size:21px; color:red; margin-left:100px;" ></asp:Label>
        <%--<div class="col-lg-6">
            <asp:Button ID="btnNewSpecimen" CssClass="btn btn-success btn-lg pull-right" OnClick="btnNewSpecimen_Click" runat="server" Text="New Specimen" />
        </div>--%>
    </div>
   
    <div class="row" runat="server" >
        <div class="col-lg-12" style="float:left; color:red;font-size:20px;">
            <asp:Literal ID="ltrEmailErrormsg" runat="server"></asp:Literal>
        </div>
        <div class="col-lg-12" style="float:left;color: rgb(12, 156, 18);font-size:20px;">
            <asp:Literal ID="ltrEmailSuccessmsg" runat="server"></asp:Literal>
        </div>
    </div>
   
    <asp:HiddenField ID="hSpecimenID" Value="0" runat="server" />
    <div class="row" id="dvSpecimenDetail"  runat="server">
        <div class="col-lg-9">
            <div class="row" style="margin-left: 10px;">
                <div class="box box-info">
                    <div class="box-body" style="min-height: 720px;">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row" style="text-align: center; font-size: 48px; height: 90px; border: 1px dashed #aaa; text-align: center; padding: 5px 5px 5px 5px; margin-left: 5px; margin-right: 5px; margin-top: 15px; background-color: beige;">
                                    <asp:Literal ID="ltrSpecimenNumber" Text="Sample #" runat="server"></asp:Literal>
                                </div>

                                <div style="margin-left: 3px;">
                                    <h4>Patient Information&nbsp;&nbsp;
                                        <a href="#modal-Patient" runat="server" id="aModalPatientInfo" role="button" class="red" data-toggle="modal"><i class="fa fa-edit"></i></a>
                                    </h4>
                                </div>
                                <div class="profile-user-info profile-user-info-striped">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Patient Name </div>
                                        <div class="profile-info-value">
                                            <span> 
                                                <asp:Label ID="lblFirstName" runat="server"></asp:Label>&nbsp;
                                                <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Location </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrLocation" runat="server"></asp:Literal>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Gender / Age </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrAgeGender" runat="server"></asp:Literal>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row" style="height:50px">
                                        <div class="profile-info-name">Party/Guardian Name </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrGuardianName" runat="server"></asp:Literal>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row" style="height: 50px;">
                                        <div class="profile-info-name">Party/Guardian Address </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrGuardianAddress" runat="server"></asp:Literal>&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <div style="margin-left: 3px;">
                                    <h4>Verification&nbsp;&nbsp;
                                           <a href="#modal-Verify" role="button" id="aModalVerification" runat="server" class="red" data-toggle="modal"><i class="fa fa-edit"></i></a>
                                    </h4>
                                </div>
                                <div class="profile-user-info profile-user-info-striped">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name" id="YesConsent" visible="false" runat="server">&nbsp;<i class="fa fa-check-circle green"></i>&nbsp;</div>
                                        <div class="profile-info-name" id="NoConsent" visible="true" runat="server">&nbsp;<i class="fa fa-crosshairs red"></i>&nbsp;</div>
                                        <div class="profile-info-value">
                                            <span>&nbsp;Consent provided?
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">&nbsp;<i class="fa fa-check-circle-o"></i>&nbsp;</div>

                                        <div class="profile-info-name" id="YesReqComp" visible="false" runat="server">&nbsp;<i class="fa fa-check-circle green"></i>&nbsp;</div>
                                        <div class="profile-info-name" id="NoReqComp" visible="true" runat="server">&nbsp;<i class="fa fa-crosshairs red"></i>&nbsp;</div>
                                        <div class="profile-info-value">
                                            <span>&nbsp;Requisition complete?
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name" id="YesAccept" visible="false" runat="server">&nbsp;<i class="fa fa-check-circle green"></i>&nbsp;</div>
                                        <div class="profile-info-name" id="NoAccept" visible="true" runat="server">&nbsp;<i class="fa fa-crosshairs red"></i>&nbsp;</div>

                                        <div class="profile-info-value">
                                            <span>&nbsp;Specimen acceptable for testing?</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row" runat="server" id="dvReason" visible="false">
                                        <div class="profile-info-name">&nbsp;&nbsp;</div>

                                        <div class="profile-info-value" style="padding-left: 30px;">
                                            <span><b>Reason for rejection -
                                                <asp:Label ID="lblReasonRej" runat="server"></asp:Label></b></span>
                                        </div>
                                    </div>
                                </div>

                                <div style="margin-left: 3px;" runat="server" id="dvResCap" visible="false">
                                    <h4>Results&nbsp;&nbsp;
                                          <a href="#modal-Result" role="button" class="red" data-toggle="modal" runat="server" id="ResLink"><i class="fa fa-edit"></i></a>
                                    </h4>
                                </div>
                                <div class="profile-user-info profile-user-info-striped" runat="server" id="dvResInput" visible="false">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Remaining Type</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblRemainType" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Remaining Vol</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblRemainVol" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Binding Value</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrBIN" runat="server"></asp:Literal>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Blocking Value</div>

                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrBlocking" runat="server"></asp:Literal>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Result Report</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:LinkButton ID="lnkGenRpt" runat="server" OnClick="lnkGenRpt_Click" Visible="false" Text="Generate Report"></asp:LinkButton>
                                                <a id="ancPdfGen" target="_blank" runat="server"></a>
                                                <asp:LinkButton ID="lnkbtnRegenerate" Text="&nbsp;&nbsp;Regenerate" Visible="false" OnClick="lnkbtnRegenerate_Click" runat="server"></asp:LinkButton>&nbsp;                                                                                              
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Repeated?</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrRetested" runat="server"></asp:Literal>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Repeat No.</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrRepeatNo" runat="server"></asp:Literal>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <br />
                                   
                                    <div class="row">
                                         <%--<div class="col-lg-6" style="text-align: center">
                                            <asp:LinkButton ID="lnkviewhardcopy" runat="server" Visible="true" OnClick="lnkviewhardcopy_Click" Text="Received Form"></asp:LinkButton>
                                                <a id="ancViewCopy" runat="server"></a>
                                        </div>--%>

                                        <div class="col-lg-12" style="text-align:center">
                                            <asp:Button ID="btnVerifyAndSendEmail" CssClass="btn btn-info"  Text="Verify and send Email" OnClick="btnVerifyAndSendEmail_Click" runat="server" />
                                        </div>                              
                                    </div>
                                    <br />                                    
                                </div>
                                <div runat="server" id="dvResultMsg" style="margin-right: 10px; margin-left: 5px" class="alert alert-danger">
                                    <asp:Literal ID="ltrResultMsg" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="col-md-6" runat="server">
                                <div style="margin-left: 3px;">
                                    <h4>Requesting Physician / Institution&nbsp;&nbsp;
                                            <a href="#modal-Requester" role="button" id="aReqPhysicianDetails" runat="server" class="red" data-toggle="modal"><i class="fa fa-edit"></i></a>
                                    </h4>
                                </div>
                                <div class="profile-user-info profile-user-info-striped">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Physician </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblPhyName" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Diagnosis </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblDiagnosis" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Diagnosis Code </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblDiagnosisCode" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Prefering Result Type </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblResultType" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Address </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrReqAddress" runat="server"></asp:Literal>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Contact </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="ltrReqContact" runat="server"></asp:Literal>&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <div style="margin-left: 3px;">
                                    <h4>Specimen Information&nbsp;&nbsp;
                                            <a href="#modal-SpecInfo" role="button" id="aSpecimenInformation" runat="server" class="red" data-toggle="modal"><i class="fa fa-edit"></i></a></h4>
                                </div>
                                <div class="profile-user-info profile-user-info-striped">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Received On</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblReceivedOn" runat="server"></asp:Label>&nbsp;(date & time)
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Drawn Date</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lbldrawnDatetime" runat="server"></asp:Label>&nbsp;(date & time)
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Transit Time</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblTransitTime" runat="server"></asp:Label>&nbsp;
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Temperature </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblTransitTemp" runat="server"></asp:Label>&nbsp;(during transit)
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Vol. Received </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblVolRec" runat="server"></asp:Label>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Comments </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblPotInter" runat="server"></asp:Label>&nbsp;(Potential Substances)</span>
                                           
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Specimen Type </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lbltype" runat="server"></asp:Label>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Blood Type </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblBloodType" runat="server"></asp:Label>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Current Status </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Label ID="lblCurrentSpecimenStatus" runat="server"></asp:Label>&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row" id="specimenform" runat="server">
                                        <div class="profile-info-name" >Specimen Form </div>
                                        <div class="profile-info-value">
                                            <span>
                                      <%--<asp:LinkButton ID="lnkviewhardcopy" runat="server" Visible="true" OnClick="lnkviewhardcopy_Click" Text="Received Form"></asp:LinkButton>--%>
                                                <a id="ancViewCopy" runat="server"></a>&nbsp;</span>

                                        </div>
                                    </div>
                                     <div class="profile-info-row" id="Div1" runat="server">
                                        <div class="profile-info-name" >Specimen Form Upload </div>
                                        <div class="profile-info-value">
                                            
                                <div style="margin-left: 3px;">
                                    
                                </div>
                                <div>

                                    <input id="inSampleHardCopy" class="form-control" style="display: inline" placeholder="Choose File" disabled="disabled"/>
                                   <%-- <asp:RequiredFieldValidator ID="rfinSampleHardCopy" runat="server" ControlToValidate="inSampleHardCopy" ForeColor="Red" ErrorMessage="Please Choose File"></asp:RequiredFieldValidator>--%>
                                    <br /><div class="fileUpload btn btn-primary" style="border: none">
                                        <span>Choose</span>
                                        <asp:FileUpload ID="flSampleHardCopy" runat="server" CssClass="upload" />

                                        <asp:HiddenField ID="fileUploadHandler" runat="server" Value="" />
                                    </div>
                                    &nbsp;&nbsp;
                                    
                                   <asp:Button ID="btnupload" CssClass="btn btn-success" OnClick="btnUpload_Click" runat="server" Text="Upload" />
                                </div>
                                        </div>
                                    </div>

                                </div>

                                
                                <div class="profile-user-info profile-user-info-striped" style="margin-top: 15px;">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">Test Requested</div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:CheckBox ID="chkBinding" Checked="true" runat="server" />&nbsp;Folate Receptor A Binding Antibodies<br />
                                                <asp:CheckBox ID="chkBlocking" Checked="true" runat="server" />&nbsp;Folate Receptor A Blocking Antibodies
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-left: 5px; margin-top: 10px;">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" AutoPostBack="true" OnClick="btnSave_Click" runat="server" Visible="false" Text="Save Request Info" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3">
            <%--            <div style="background-color: #07005e; margin-left: 15px; padding-left: 15px; padding-top: 5px; padding-bottom: 5px; color: #ffffff;">
                <h4>Current Assay Samples</h4>
            </div>--%>

            <div style="padding: 3px; color: rgb(255, 255, 255); margin-left: 15px; background-color: rgb(7, 0, 94);">
                <div style="float: left; padding-left: 5px;">
                    <h4>Current Assay Samples</h4>
                </div>

                <div style="float: right; padding-top: 5px; padding-right: 3px;">
                    <%--<button class="btn btn-sm btn-danger" id="btnAssay" onclick="javascript:showAssay();"></button>--%>
                    <asp:Button ID="btnAssay" runat="server" Text="Assay Tracker" OnClick="btnAssay_Click" CssClass="btn btn-sm btn-danger" />
                </div>

                <div style="clear: both"></div>
            </div>

            <div class="alert alert-info" style="height: 600px;">
                <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 550px">
                    <uc1:AssayInfo runat="server" ID="AssayInfo" />
                </div>
            </div>

            <asp:HiddenField ID="hAssayID" runat="server" />
            <asp:HiddenField ID="hSpecimenStatus" runat="server" />
            <asp:HiddenField ID="hAssaySpecimenID" runat="server" />
            <asp:HiddenField ID="hDocID" runat="server" />
        </div>


    </div>
        
    <!-- BEGIN: Modal forms -->
    <div id="modal-Patient" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="blue bigger">Patient Information</h4>
                    <asp:HiddenField ID="hPatientID" runat="server" />
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>First Name<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="GenInput input420"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFirstName" ID="rfPatientName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgPatientName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Last Name<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="input420 GenInput" placeholder="Last Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtLastName" ID="rfLastName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgLastName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Street / City / Zip / Country<i style="color:red;font-size:18px;">*</i></b><br />

                            <asp:TextBox ID="txtstreet" runat="server" CssClass="input110 GenInput" placeholder="Street"></asp:TextBox>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="input110 GenInput" placeholder="City"></asp:TextBox>
                            <asp:TextBox ID="txtZip" runat="server" CssClass="input80 GenInput" placeholder="Zip"></asp:TextBox>
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="input110 GenInput" placeholder="Country"></asp:TextBox>
                        </div>

                        <div class="row">
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtstreet" ID="rfStreet" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>                                
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCity" ID="rfCity" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtCity" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtZip" ID="rfZip" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgZip" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Numbers Only" ControlToValidate="txtZip" ValidationExpression="[0-9]\d*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCountry" ID="rfCOuntry" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgCountry" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Gender</b>
                            <asp:RadioButton ID="optM" GroupName="sex" CssClass="radio-inline"  Checked="true" Text="" style="position:unset; margin-left:10px; margin-top:-21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-male blue"></i>&nbsp;Male
                            <asp:RadioButton ID="optF" GroupName="sex" CssClass="radio-inline" Text="" style="position:unset; margin-left:10px; margin-top:-21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-female pink"></i>&nbsp;Female
                            
                        </div>

                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Date Of Birth<i style="color:red;font-size:18px;">*</i></b>
                            <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="date-picker GenInput" placeholder="Date of Birth" Style="margin-top:0px;margin-bottom:0px;"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar" id="DateOfBirth"></i>
                                    </span>
                                    <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDOB" ID="rfDateOfBirth" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <h4>Responsible Party / Guardian</h4>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>First Name<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtGuardianFirstName" runat="server" placeholder="First Name" CssClass="GenInput input420"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianFirstName" ID="rfGuardianFirstName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianFirstName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianFirstName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>

                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Last Name<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtGuardianLastName" Width="100%" runat="server" CssClass="GenInput input420" placeholder="Last Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianLastName" ID="rfGuardianLastName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianLastName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>

                        <%-- <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                           <asp:CheckBox ID="chkaddress" AutoPostBack="true"  runat="server" OnCheckedChanged="chkaddress_CheckedChanged" Text="if same as patient address" />&nbsp;<br />
                        </div>--%>

                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Street / City / Zip / Country<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtGuardianStreet" runat="server" CssClass="input110 GenInput" placeholder="Street"></asp:TextBox>
                            <asp:TextBox ID="txtGuardianCity" runat="server" CssClass="input110 GenInput" placeholder="City"></asp:TextBox>
                            <asp:TextBox ID="txtGuardianZip" runat="server" CssClass="input80 GenInput" placeholder="Zip"></asp:TextBox>
                            <asp:TextBox ID="txtGuardianCountry" runat="server" CssClass="input110 GenInput" placeholder="Country"></asp:TextBox>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianStreet" ID="rfGuardianStreet" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>                                
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianCity" ID="rfGuardianCity" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianCity" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianZip" ID="rfGuardianZip" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianZip" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Numbers Only" ControlToValidate="txtGuardianZip" ValidationExpression="[0-9]\d*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-3">
                                <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianCountry" ID="rfGuardianCountry" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianCountry" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button class="btn btn-primary" data-dismiss="modal">
                            <i class="icon-remove"></i>
                            Cancel
                        </button>
                        &nbsp;
                    <asp:Button ID="btnPatientSave" ValidationGroup="vPatDetails" CssClass="btn btn-primary" OnClick="btnPatientSave_Click" runat="server" Text="Save Patient Info" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modal-Verify" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="blue bigger">Specimen Verification</h4>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <p style="margin-top: 5px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkConsentProvided" runat="server" /><span class="lbl"></span>&nbsp;Consent Provided?
                            </p>
                            <p style="margin-top: 5px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkRequisition" runat="server" /><span class="lbl"></span>&nbsp;Requisition complete?
                            </p>
                            <p style="margin-top: 5px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" disabled="disabled" id="chkAcceptTest" runat="server" /><span class="lbl"></span>&nbsp;Specimen acceptable for testing?
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button class="btn btn-primary" data-dismiss="modal">
                    <i class="icon-remove"></i>
                    Cancel
                </button>
                &nbsp;
                    <asp:Button ID="btnVerificationInfo" CssClass="btn btn-primary" OnClick="btnVerificationInfo_Click" runat="server" Text="Save Verification Info" />
            </div>
        </div>
    </div>

    <div id="modal-Requester" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="blue bigger">Ordering Physician/Company Information</h4>
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <asp:DropDownList ID="ddNew1" runat="server" Visible="true" CssClass="input100 GenDD">
                                <asp:ListItem Text="New" Value="New" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Existing" Value="Exist"></asp:ListItem>
                            </asp:DropDownList>&nbsp;
                                <asp:DropDownList ID="ddPhy1" runat="server" Visible="false" CssClass="input300 GenDD" DataTextField="CustomerName" DataValueField="CustomerID">
                                </asp:DropDownList>&nbsp;<asp:HiddenField ID="hCustID" Value="0" runat="server" />
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span7">
                                <asp:TextBox ID="txtPhyName" runat="server" placeholder="Physician Name" CssClass="GenInput input420"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyName" ID="rfdPhyName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgphyName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable">
                                <asp:TextBox ID="txtDiagnosis" runat="server" placeholder="Diagnosis" CssClass="GenInput input420"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDiagnosis" ID="rfdDiagonosis" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgDiagonosis" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtDiagnosis" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable">
                                <asp:TextBox ID="txtDiagnosisCode" runat="server" placeholder="Diagnosis Code" CssClass="GenInput input420"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDiagnosisCode" ID="rfDiagonosisCode" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable">Preferred Method for Receiving Results
                                 <asp:DropDownList ID="ddlResultType" runat="server" CssClass="input100 GenDD">
                                     <asp:ListItem Text="Blocking" Value="Blocking" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Binding" Value="Binding"></asp:ListItem>
                                 </asp:DropDownList>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span1">
                                <asp:TextBox ID="txtPhyAddress1" runat="server" CssClass="input420 GenInput" placeholder="Address" ValidationGroup="client"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyAddress1" ID="rfPhyAddress" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span3">
                                <asp:TextBox ID="txtPhyCity" runat="server" CssClass="input420 GenInput" placeholder="City" ValidationGroup="client"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyCity" ID="rfPhyCity" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyCity" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span4">
                                <span>
                                    <asp:TextBox ID="txtPhyState" runat="server" CssClass="input289 GenInput" placeholder="State / Province"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyState" ID="rfPhyState" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyState" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyState" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtPhyPCode" runat="server" CssClass="input120  GenInput" placeholder="Postal Code"></asp:TextBox></span>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyPCode" ID="rfPhyCode" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyCode" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Numbers and special character(-) Only" ControlToValidate="txtPhyPCode" ValidationExpression="([0-9\-\ 0-9])*$" EnableClientScript="true"></asp:RegularExpressionValidator>

                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span5">
                                <asp:DropDownList ID="ddCountry" runat="server" Visible="true" CssClass="input300 GenDD chosen-select">
                                    <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span6">
                                <asp:TextBox ID="txtPhyEmail" runat="server" CssClass="input420 GenInput" placeholder="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyEmail" ID="rfPhyEmail" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyEmail" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyEmail" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <span class="editable" id="Span8">
                                <asp:TextBox ID="txtPhyPhone" placeholder="Phone number" CssClass="input210 GenInput" runat="server" />
                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyPhone" ID="rfPhyPhone" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyPhone" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and some characters(+ / -) Only" ControlToValidate="txtPhyPhone" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                &nbsp;&nbsp;                
                                <asp:TextBox ID="txtPhyFax" placeholder="Fax number" CssClass="input200 GenInput" runat="server" />
                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyFax" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and some characters(+ / -) Only" ControlToValidate="txtPhyFax" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" data-dismiss="modal">
                        <i class="icon-remove"></i>
                        Cancel
                    </button>
                    &nbsp;
                    <asp:Button ID="btnSaveReq" CssClass="btn btn-primary" ValidationGroup="vPhyDetails" OnClick="btnSaveReq_Click" runat="server" Text="Save Requesting Party" />
                </div>
            </div>
        </div>
    </div>

    <div id="modal-Result" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="blue bigger">Specimen Results</h4>
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">
                        <b style="color: red">
                            <asp:Literal Text="" ID="ltrAssayError" runat="server" /></b>
                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Remaining Specimen Type</b><br />
                            <asp:RadioButton ID="optRemainSerum" GroupName="spType" runat="server" />&nbsp;&nbsp;Serum&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="optRemainBlood" GroupName="spType" runat="server" />&nbsp;&nbsp;Blood&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddBloodType" runat="server">
                                        <asp:ListItem Text="A -ve" Value="A -ve"></asp:ListItem>
                                        <asp:ListItem Text="A +ve" Value="A +ve"></asp:ListItem>
                                        <asp:ListItem Text="B -ve" Value="B -ve"></asp:ListItem>
                                        <asp:ListItem Text="B +ve" Value="B +ve"></asp:ListItem>
                                        <asp:ListItem Text="AB -ve" Value="AB -ve"></asp:ListItem>
                                        <asp:ListItem Text="AB +ve" Value="AB +ve"></asp:ListItem>
                                        <asp:ListItem Text="O -ve" Value="O -ve"></asp:ListItem>
                                        <asp:ListItem Text="O +ve" Value="O +ve"></asp:ListItem>
                                    </asp:DropDownList>
                        </div>
                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <b>Remaining Volume<i style="color:red;font-size:18px;">*</i></b><br />
                            <asp:TextBox ID="txtRemainVol" placeholder="Remaining Volume" CssClass="input200 GenInput" runat="server" />
                            <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtRemainVol" ID="rfRemainingVolume" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgRemainingVolume" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtRemainVol" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <b>Binding Value<i style="color:red;font-size:18px;">*</i></b><br />
                                <asp:TextBox ID="txtBindValue" placeholder="Binding Value" CssClass="input200 GenInput" runat="server" />
                            </div>  
                            <div class="col-lg-6">
                                <b>Comments</b><br />
                                <asp:TextBox ID="txtBindComment" placeholder="Comments" TextMode="MultiLine" Rows="15" CssClass="input200 GenInput" runat="server" />
                            </div>                     
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBindValue" ID="rfBindValue" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBindValue" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtBindValue" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <%--<div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBindComment" ID="rfBindComment" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBindComment" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtBindComment" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>--%>
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <b>Blocking Value<i style="color:red;font-size:18px;">*</i></b><br />
                                <asp:TextBox ID="txtBlockValue" placeholder="Blocking Value" CssClass="input200 GenInput" runat="server" />
                            </div>  
                            <div class="col-lg-6">
                                <b>Comments</b><br />
                                <asp:TextBox ID="txtBlockComment" placeholder="Comments" TextMode="MultiLine" Rows="15" CssClass="input200 GenInput" runat="server" />
                            </div>                     
                        </div>
                        
                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBlockValue" ID="rfBlockValue" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBlockValue" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtBlockValue" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <%--<div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBlockComment" ID="rfBlockComment" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBlockComment" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtBlockComment" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>--%>
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <br />
                            <asp:CheckBox ID="chkNeedToRetest" Text="Need Re-Test?" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" data-dismiss="modal">
                        <i class="icon-remove"></i>
                        Cancel
                    </button>
                    &nbsp;
                    <asp:Button ID="btnResults" ValidationGroup="vResults" CssClass="btn btn-primary" OnClick="btnResults_Click" runat="server" Text="Save Results" />
                </div>
            </div>
        </div>
    </div>

    <div id="modal-SpecInfo" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h4 class="blue bigger">Specimen Information</h4>
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">

                        <div class="row rw2" style="margin-top: 5px; margin-left: 25px; margin-bottom: 5px;">
                            <b>Specimen Type</b><br />
                            <asp:RadioButton ID="rdSpecimenserum" GroupName="Specimengroup" runat="server" />&nbsp;&nbsp;Serum
                                <asp:RadioButton ID="rdSpecimenblood" GroupName="Specimengroup" runat="server" />&nbsp;&nbsp;Blood&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddSpecimentype" runat="server">
                                        <asp:ListItem Text="A -ve" Value="A -ve"></asp:ListItem>
                                        <asp:ListItem Text="A +ve" Value="A +ve"></asp:ListItem>
                                        <asp:ListItem Text="B -ve" Value="B -ve"></asp:ListItem>
                                        <asp:ListItem Text="B +ve" Value="B +ve"></asp:ListItem>
                                        <asp:ListItem Text="AB -ve" Value="AB -ve"></asp:ListItem>
                                        <asp:ListItem Text="AB +ve" Value="AB +ve"></asp:ListItem>
                                        <asp:ListItem Text="O -ve" Value="O -ve"></asp:ListItem>
                                        <asp:ListItem Text="O +ve" Value="O +ve"></asp:ListItem>
                                    </asp:DropDownList>
                        </div>

                        <div class="row rw2" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-5">                                
                                    <%--<div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                        <div class="control-group">
                                            <label class="control-label">Received Date and Time</label>
                                            <div class="controls input-append date form_datetime" style="padding-right:10px; border:1px solid rgb(128, 128, 128);" data-date-format="dd MM yyyy - HH:ii p" data-link-field="txtReceivedOn">
                                                <asp:TextBox ID="txtTime" data-provide="timepicker" CssClass="form-control timepicker" runat="server"></asp:TextBox>
                                                <input style="width: 180px;" class="textBoxWidth" size="16" type="text" value=""  />
					                            <span class="add-on"><i style="padding:0 13px 0 5px;" class="fa fa-calendar"></i></span>
                                            </div>
				                            <input type="hidden" runat="server" value="" /><br/>
                                        </div>
                                    </div>--%>
                                <b>Received Date & Time<i style="color:red;font-size:18px;">*</i></b><br />
                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtReceivedOn" runat="server" Width="160px" CssClass="date-picker GenInput" placeholder="Received Date" style="margin-top:0px;margin-bottom:0px"></asp:TextBox>
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar" id="receivedDate"></i>                                            
                                        </span>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-lg-4">
                                <b>Drawn Date<i style="color:red;font-size:18px;">*</i></b><br />
                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDrawndate" runat="server" Style="width: 100px;margin-top:0px;margin-bottom:0px;" CssClass="date-picker GenInput" placeholder="Drawn Date" ></asp:TextBox>
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar" id="drawnDate"></i>                                         
                                        </span>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <b>Drawn Time<i style="color:red;font-size:18px;">*</i></b><br />
                                <asp:TextBox ID="txtDrawnTime" runat="server" placeholder="Drawn Time" ValidationGroup="client" CssClass="GenInput input100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtReceivedOn" ID="rfReceivedOn" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDrawndate" ID="rfDrawndate" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row rw2" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <b>Transit Time<i style="color:red;font-size:18px;">*</i></b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:TextBox ID="txtTransitTime" runat="server" CssClass="input140 GenInput" placeholder="Transit Time"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">
                                <div class="row">
                                    &nbsp;
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblTransitError" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTransitTime" ID="rfTransitTime" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgtxtTransitTime" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtTransitTime" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>

                        <div class="row rw2" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <b>Temperature during transit<i style="color:red;font-size:18px;">*</i></b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:TextBox ID="txtTransitTemp" runat="server" placeholder="Temperature during transit" ValidationGroup="client" CssClass="GenInput input240"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <b>Volume received<i style="color:red;font-size:18px;">*</i></b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:TextBox ID="txtVolReceived" runat="server" CssClass="input140 GenInput" placeholder="Volume received"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTransitTemp" ID="rfTransitTemp" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgTransitTemp" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtTransitTemp" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-6">
                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtVolReceived" ID="rfVolReceived" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgVolReceived" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtVolReceived" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row rw2" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <div class="col-lg-12">
                                <b>Comments</b><br />
                                <asp:TextBox ID="txtPotInterfer" runat="server" TextMode="MultiLine" MaxLength="1" CssClass="GenInput input240"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="regPotInterfer" runat="server" ErrorMessage="Comments with in 30 characters only" ControlToValidate="txtPotInterfer" ForeColor="Red" ValidationExpression="^[a-zA-Z\s]{0,30}$"/>
                            </div>
                        </div>
                        <%-- <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                            <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPotInterfer" ID="rfPotInterfer" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgPotInterfer" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtPotInterfer" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" data-dismiss="modal">
                            <i class="icon-remove"></i>
                            Cancel
                        </button>
                        &nbsp;
                    <asp:Button ID="btnSpecimen" CssClass="btn btn-primary" ValidationGroup="vSpecimenInformation" AutoPostBack="true" OnClick="btnSpecimen_Click" runat="server" Text="Save Specimen Info" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- END: Modal forms-->

    <%--    <script src="../style/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=chkaddress.ClientID%>').change(function () {
                console.log("fdf")
            });
           console.log( )
            $('#ContentPlaceHolder1_chkaddress').click(function () {
                console.log('check')
            })
          
            $('#<%=chkaddress.ClientID %>').click(
        function () {
            console.log("checked.. noww");
        });
            if ($('#<%=chkaddress.ClientID%>').is(':checked')) {
                console.log("checked noww");
            }
            $("#<%= chkaddress.ClientID%>").click(function () {
                if ($(this).prop("checked") == true) {
                }
                    alert("Checkbox is checked.");
                else if ($(this).prop("checked") == false) {
                    alert("Checkbox is unchecked.");
                }
              //  $('#<%=txtstreet.ClientID%>')

                /*$("#txtstreet").val();
                $("#txtstreet1").val("txtstreet");
                $("#txtCity").val();
                $("#txtcity1").val("txtCity");
                $("#txtZip").val();
                $("#txtzip1").val("txtZip");
                $("#txtCountry").val();
                $("#txtcountry1").val("txtCountry");*/
                console.log('checked');
            });
        });
    </script>--%>
    
   <script src="https://cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= flSampleHardCopy.ClientID%>").change(function () {

                var fileName = $(this).val();
                var extention = (fileName.substr(fileName.lastIndexOf('.') + 1)).toLowerCase();
                if (extention == 'jpg' || extention == 'png' || extention == 'jpeg' || extention == 'bmp') {

                $('#inSampleHardCopy').val(fileName.substr(fileName.lastIndexOf('\\') + 1));
                $('#<%= fileUploadHandler.ClientID %>').val(fileName);
                      <%-- // document.getElementById('<%= fileUploadHandler.ClientID%>').value = "upload";
                      //__doPostBack('<%= fileUploadHandler.ClientID%>');--%>

              
                    <%--if (document.getElementById("<%=inSampleHardCopy.ClientID%>").value == "") {
                             alert("Name Feild can not be blank");
                             document.getElementById("<%=inSampleHardCopy.ClientID%>").focus();
                             return false;
                         }--%>
             

                }
                else {
                  
                    alert("Please upload a valid image file.");
                    $("#<%=flSampleHardCopy.ClientID%>").val('');
                    $("#inSampleHardCopy").val('');
                    $('#<%= fileUploadHandler.ClientID %>').val("delete");
                }

                if ($("#ContentPlaceHolder1_inSampleHardCopy").val=="")
            {
                    alert("Name Feild can not be blank");
            }


            });

        });

        $('.form_datetime').datetimepicker({
            //language:  'fr',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            forceParse: 0,
            showMeridian: 1,
            maxDate: new Date()
        });
        $(function () {
            /* function click ()
             {
                 if (document.forms_ContentPlaceHolder1_chkaddress.chkaddress.checked) {
                     document.forms_ContentPlaceHolder1_chkaddress.txtstreet1.value = document.forms_ContentPlaceHolder1_chkaddress.txtstreet.value;
                     document.forms_ContentPlaceHolder1_chkaddress.txtcity1.value = document.forms_ContentPlaceHolder1_chkaddress.txtCity.value;
                     document.forms_ContentPlaceHolder1_chkaddress.txtzip1.value = document.forms_ContentPlaceHolder1_chkaddress.txtZip.value;
                     document.forms_ContentPlaceHolder1_chkaddress.txtcountry1.value = document.forms_ContentPlaceHolder1_chkaddress.txtCountry.value;
                 }
                 else {
                     document.forms_ContentPlaceHolder1_chkaddress.txtstreet1.value = "";
                     document.forms_ContentPlaceHolder1_chkaddress.txtcity1.value = "";
                     document.forms_ContentPlaceHolder1_chkaddress.txtzip1.value = "";
                     document.forms_ContentPlaceHolder1_chkaddress.txtcountry1.value = "";
                 }
             }*/

            $("#ContentPlaceHolder1_txtDOB").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+2",
                autoclose: true,
                maxDate: new Date()
            });

            $('#DateOfBirth').click(function () {

                //alert('clicked');
                $('#ContentPlaceHolder1_txtDOB').datepicker('show');
            });

            $("#ContentPlaceHolder1_txtReceivedOn").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+2",
                autoclose: true,
                maxDate: new Date()
            });

            $('#receivedDate').click(function () {

                //alert('clicked');
                $('#ContentPlaceHolder1_txtReceivedOn').datepicker('show');
            });

            $("#ContentPlaceHolder1_txtDrawndate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+2",
                autoclose: true,
                maxDate: new Date()
            });

            $('#drawnDate').click(function () {

                //alert('clicked');
                $('#ContentPlaceHolder1_txtDrawndate').datepicker('show');
            });

            $('#ContentPlaceHolder1_txtDrawnTime').timepicker();

            $(document).ready(function () {
                //this calculates values automatically 

                $('#<%=txtTransitTime .ClientID %>').on("focus", function () {

                    txtTransitTime();
                });
            });

            $("body").delegate("#<%= txtDrawnTime.ClientID%>", "focusin", function () {
                //$(this).datepicker();
                $(".ui-timepicker-container").css("z-index", "99999 !important");
                console.log(this)
            });
        });

       

        function txtTransitTime() {
            // Get Variables
            var txtReceivedOn = $('#<%= txtReceivedOn.ClientID %>').val();
            var txtDrawndate = $('#<%=txtDrawndate .ClientID %> ').val();
            var txtDrawnTime = $('#<%=txtDrawnTime .ClientID %> ').val();


            //txtReceivedOn = new Date(txtReceivedOn);
            //txtDrawndate = new Date(txtDrawndate + " " + txtDrawnTime);
            //var hours = (txtDrawndate - txtReceivedOn) / 36e5;
            //console.log(hours.toFixed(2));
            // Add Years to Date
            //  var txtTransitTime = txtDrawndate - TxtSamReceiDate ;
            txtReceivedOn = new Date(txtReceivedOn);
            txtDrawndate = new Date(txtDrawndate + " " + txtDrawnTime);
            var hours = (txtReceivedOn - txtDrawndate) / 36e5;
            console.log(hours.toFixed(2));

            if (hours < 0) {
                $('#<%=lblTransitError.ClientID %>').text("Drawn date should be before Received date");
                $('#<%=txtTransitTime.ClientID %>').val("");
            }

            else {
                $('#<%=txtTransitTime.ClientID %>').val(hours);
                $('#<%=lblTransitError.ClientID %>').text("");
            }
        }

        $(document).ready(function ()
        {
            $('#ContentPlaceHolder1_txtDrawnTime').timepicker({
                timeFormat: 'h:mm p',
                interval: 30,
                minTime: '12',
                maxTime: '12:00pm',
                defaultTime: '12',
                startTime: '12:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true,
              

            });
        });

    
            //function showfilename(input) {
            //    var file = $("#flsamplehardcopy").val();

            //   document.getelementbyid("txtfile").value = file;
            //}
   


     

     

       <%-- function clearPreview() {
            console.log("remove")
            $("#dvPreview").html("");
            $("#<%=flSampleHardCopy.ClientID%>").val('');
            $("#inSampleHardCopy").val('');
            $('#<%= fileUploadHandler.ClientID %>').val("delete");

        }--%>
        
        
    </script>

  

</asp:Content>
