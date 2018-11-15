<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Specimen.aspx.cs" MasterPageFile="~/themes/mainMaster.Master" Inherits="STOMS.UI.pages.Specimen" %>

<%@ Register Src="~/usercontrol/SampleList.ascx" TagPrefix="uc1" TagName="SampleList" %>
<%@ Register Src="~/usercontrol/showMessage.ascx" TagPrefix="uc1" TagName="showMessage" %>
<%@ Register Src="~/usercontrol/PendingSampleList.ascx" TagPrefix="uc1" TagName="PendingSampleList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />   
    <link href="../style/css/select2/dist/css/select2.css" rel="stylesheet" />

    <style>
        #ContentPlaceHolder1_rdoPaymentMode label {
            font-weight: 400;
        }

        .input140 {
            width: 175% !important;
        }

        .well {
            padding: 0px;
        }

        .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 10px;
        }


   label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 400;
        }

       p label{
          font-weight: 400;
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

        /*legend.scheduler-border {
            font-size:22px !important;
            text-align: left !important;
            border: none;
        }

        fieldset.scheduler-border {
            border: 1px solid #b04442 !important;
            padding: 0 1.4em 1.4em 0 !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }*/

        /*.label {
            font-weight: normal;
            font-size: 20px;
        }*/
    </style>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    
    <div class="pace  pace-inactive">
        <div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
            <div class="pace-progress-inner"></div>
        </div>
        <div class="pace-activity">
        </div>
    </div>
    <div class="">       
        <div class="row" style="margin-left:2px">
            <div class="col-lg-12">
                <!--style="border-bottom:1px solid #dbdbdb;margin-bottom:12px;"-->
                <div class="row" >
                    <div class="row" style="margin-left: 5px; color: rgb(12, 156, 18);font-size:18px;">
                        <span id="spMsg" runat="server"></span>
                    </div>
                    <br />
                    <%--<div class="col-lg-6">
                        <span style="margin-left:4px !important;"> <asp:Button ID="btnGenNum" OnClick="btnGenNum_Click" CssClass="btn btn-primary pull-right" runat="server" Text="Generate Specimen Number(s)" /></span>
                        <span style="margin-left:4px !important;"><asp:Button ID="btnApplication" CssClass="btn btn-success pull-right" OnClick="btnApplication_Click" runat="server" Text="Specimen Tracking" /></span>
                    </div>--%>
                </div>

                <div class="row">
                    <div class="">
                        <div class="box-body">
                            <div class="row">
                                <uc1:showMessage runat="server" ID="showMessage" />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <%--<div class="row">
                                        <div style="float:left; margin-left:18px;">
                                            <a class="btn btn-primary" id="ancRetriveAssigned" href="#modal-retrive" data-toggle="modal">Retrieve Assigned #</a>
                                            <asp:Button ID="btnSpecimenNum" CssClass="btn btn-primary" OnClick="btnSpecimenNum_Click" runat="server" Text="New Specimen Number" />                                            
                                        </div>
                                        <div style="float: right; margin-right: 20px; margin-bottom: 5px;">
                                           <!-- <asp:Button ID="btnPrintLbl" CssClass="btn btn-danger" runat="server" Text="Print label" />-->
                                        </div>
                                        <div style="clear: right;"></div>
                                    </div>
                                    <br />--%>
                                    <div style="display:none" class="">
                                        <%-- border: 1px dashed #aaa;
                                            background-color: beige;
                                            --%>
                                        <div id="dvltrSpeNum"    runat="server" style="text-align: center; font-size: 48px; height: 70px;  text-align: center; padding: 5px 5px 5px 5px; ">
                                            <asp:Literal ID="ltrSpecimenNumber" Text="Sample #" runat="server"></asp:Literal>
                                        </div>
                                        <div id="dvbtnNewSpecimen" runat="server">
                                            <asp:Button ID="btnSpecimenNum" style="height:70px;width:100%" CssClass="btn btn-primary" OnClick="btnSpecimenNum_Click" runat="server" Text="new" />
                                        </div>                                        
                                    </div>
                                    
                                    <%--<div class="">
                                        <div class="alert alert-warning" style="height: 180px;">
                                            <div class="row" style="text-align:center">                                        
                                               <input type="checkbox" id="chkSpecimenReceivedKit" runat="server" /> <label for="ContentPlaceHolder1_chkSpecimenReceivedKit">Specimen Received by the Kit</label>
                                            </div>
                                            <br />
                                            <div id="ShowHidediv" runat="server" style="display:none">
                                                <div class="row">
                                                    <div class="col-lg-8 col-lg-offset-2">
                                                        <label>Kit Number</label>                                          
                                                        <asp:TextBox CssClass="accordion-style1" ValidationGroup="vKitNumber" ID="txtKitNumber" runat="server"></asp:TextBox>
                                                        <span id="spKitNumber" style="color:red;display:none"> Field is Required</span>                                                        
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row" style="text-align:center">
                                                    <p><input class="ace ace-switch ace-switch-5" type="checkbox" id="chkReuseCountKit" runat="server" /><span class="lbl"></span>&nbsp;Can be Reuse the Kit?</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />--%>
                                     <div>
                                        
                                       <h4>  <asp:CheckBox id="chkClosePatient" runat="server" Style="margin-left: 8px;" /> <span class="lbl"></span>&nbsp;
                                         <label style="font-size:16px;font-weight:400;" for="ContentPlaceHolder1_chkClosePatient">Undisclosed Patient Information</label></h4>
                                     </div>


                                    <div class="well">
                                       <div style="margin: 15px; padding: 5px;">
                                         <span style="font-size:22px">Patient Information</span>
                                            <div class="row rw2" style="margin-top: 9px;">
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name (mandatory)" ValidationGroup="client" CssClass="GenInput input240"></asp:TextBox>
                                                    <br />
                                                    <span id="spFirstName" style="color:red;display:none"></span>
                                                    <%--<asp:RegularExpressionValidator ValidationGroup="vgInformation" ID="rgfirstname" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtfirstname" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                                </div>
                                                 <div class="col-lg-6">
                                                   <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name (mandatory)" ValidationGroup="client" CssClass="GenInput input240"></asp:TextBox>
                                                     <br />
                                                     <span id="spLastName" style="color:red;display:none">Last name is mandatory</span>
                                                   <%--<asp:RegularExpressionValidator ValidationGroup="vgInformation" ID="rglastname" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtlastname" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                               </div>
                                           </div>
                                           <div class="row rw2">
                                           </div>
                                            <div class="row rw2">
                                                <div class="col-lg-6">
                                                    <div class="row input-group" id="dvDateOfBirth" style="margin-bottom:5px; margin-left: 0px";>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDOB" runat="server" Style="margin-top: 0px;
                                                                margin-bottom: 0px;" CssClass="date-picker GenInput input200" 
                                                                placeholder="Date of Birth (mandatory)"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-calendar" id="DateOfBirth"></span>
                                                            </span>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="reqDatabirth" ValidationGroup="vgInformation"
                                                            ControlToValidate="txtDOB" ForeColor="Red" ErrorMessage="Date of birth is mandatory " runat="server"></asp:RequiredFieldValidator>                                                         
                                                    </div>
                                                </div>                                                
                                                <div class="col-lg-6" id="dvrbtnsex"  style=" margin-top: 5px;">
                                                     <asp:RadioButton ID="optM" GroupName="sex" CssClass="radio-inline" style="font-weight:600" Text="Male" runat="server" />&nbsp;&nbsp;
                                                     <asp:RadioButton ID="optF" GroupName="sex" CssClass="radio-inline" style="font-weight:600"  Text="Female" runat="server" />&nbsp;&nbsp;
                                                     <asp:RadioButton ID="optUn" GroupName="sex" CssClass="radio-inline" style="font-weight:600"  Text="Unknown" runat="server" />&nbsp;&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="well">
                                        <div style="margin: 15px; padding: 5px;">
                                           <span style="font-size:22px">Specimen Information</span>
                                                                                       
                                            <div class="row rw2">
                                                <div class="col-lg-7">
                                                    <label style="font-weight:600">Requested Test</label>
                                                    <div class="row input-group" style="margin-bottom:5px; margin-left: 0px";>
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="ddlRequest" CssClass="form-control" style="height: 38px;
                                                                width: 110%" 
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRequest_SelectedIndexChanged" runat="server"> 
                                                                <asp:ListItem disabled="disabled" Value="0">Select Test</asp:ListItem>
                                                                <asp:ListItem Text="FRAT" Value="Frat"></asp:ListItem>
                                                                <asp:ListItem Text="Mitochondrial Dysfunction Test" Value="Mitochondrial Dysfunction Test"></asp:ListItem>
                                                            </asp:DropDownList> 
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5" runat="server" style="display:none" id="dvSpecimenType">
                                                    <label style="font-weight:600">Specimen Type</label>
                                                      <div class="row input-group" style="margin-bottom:5px; margin-left: 0px";>
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="ddlSpecimenType" CssClass="form-control" style="height: 38px;"  runat="server"> 
                                                              <%--<asp:ListItem disabled="disabled" Value="0">Choose Specimen</asp:ListItem>--%>
                                                              <%--  <asp:ListItem Text="Blood" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Mouth Swab" Value="2"></asp:ListItem>--%>
                                                            </asp:DropDownList> 
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div runat="server" id="dvSpecimenData" style="display:none">
                                            <div class="row rw2">
                                                <div class="col-lg-6">
                                                    <div class="" id="dvVolumeReceived" visible="false" runat="server">
                                                        <label style="font-weight: 600">Volume Received</label><br />
                                                        <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVolReceived" runat="server" CssClass="input130 GenInput" placeholder="Volume Received"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlVolReceivedML" runat="server" Style="height: 38px; border-left: none;">
                                                                    <asp:ListItem Text="ML" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="MG" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<span  class="input-group-addon">ML</span> --%>
                                                            </div>
                                                        </div>
                                                        <span id="spError" style="color: red; visibility: visible"></span>
                                                    </div>
                                                    <div class="" id="dvNoOfSwabCount" visible="false" runat="server">
                                                        <label style="font-weight: 600;">Swab Count</label><br />
                                                        <div class=" input-group" style="margin-bottom: 5px; margin-left: 0px;">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtNoOfSwab" runat="server" CssClass="input130 GenInput" placeholder="Swab Count"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="rgNoOfSwab" ControlToValidate="txtNoOfSwab" ValidationExpression="\d+" Display="Dynamic" EnableClientScript="true" ErrorMessage="Allow numbers only" ForeColor="Red" runat="server" />
                                                            </div>
                                                        </div>
                                                        <%--<span id="spError" style="color:red;visibility:visible"></span> --%>
                                                    </div>
                                                </div>
                                                <%--<div class="col-lg-7">                                                      
                                                    <div class="input-group" style="margin-bottom:5px;">
                                                         <label style="font-weight:600;">Sample Received Date & Time</label>
                                                        <div class="input-group" style="margin-top: 2px;">
                                                            <asp:TextBox ID="txtSampleReceiveDate" runat="server" CssClass="date-picker input110 GenInput" style="width:150px!important;margin-top: 0px; margin-bottom: 0px;"  placeholder="Sample Received Date"></asp:TextBox> 
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-calendar" id="SampleReceiveDate"></span>
                                                            </span>
                                                            <asp:TextBox ID="txtReceivedTime" runat="server" placeholder="Received Time" ValidationGroup="client" CssClass="GenInput input110"></asp:TextBox>
                                                        </div>
                                                        <label style="font-weight:600" id="lblReceivedTime" runat="server">&nbsp;</label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                </div>--%>
                                            </div>                       

                                                <div class="row rw2">
                                                <div class="col-lg-5">
                                                    <label style="font-weight:600" id="Label1" runat="server">Sample Received Date & Time</label> <br />
                                                    <div class="row input-group" style="margin-bottom: 5px; margin-left: 0px;margin-top:2px">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSampleReceiveDate" runat="server" style="margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker input110 GenInput" placeholder=" Date"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-calendar" id="SampleReceiveDate"></span>
                                                            </span>                                                            
                                                        </div>
                                                    </div>                                                    
                                                </div>                                               
                                                <div class="col-lg-2" style="left:-11px">
                                                    <label style="font-weight:600" id="lblReceivedTime" runat="server">&nbsp;</label><br />
                                                    <asp:TextBox ID="txtReceivedTime" runat="server" Style="margin-left: -61px;border-left:0px" placeholder="Time" ValidationGroup="client" CssClass="GenInput input110"></asp:TextBox>
                                                </div>                                                
                                            </div>

                                            <div class="row rw2">
                                                <div class="col-lg-4">
                                                    <label style="font-weight:600" id="lblDrawnDate" runat="server">Drawn Date</label> <br />
                                                    <div class="row input-group" style="margin-bottom: 5px; margin-left: 0px;margin-top:2px">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDrawndate" runat="server" style="margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker input110 GenInput" placeholder=" Date"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-calendar" id="Drawndate"></span>
                                                            </span>                                                            
                                                        </div>
                                                    </div>                                                    
                                                </div>                                               
                                                <div class="col-lg-3" style="left:-11px">
                                                    <label style="font-weight:600" id="lblDrawnTime" runat="server">&nbsp;</label><br />
                                                    <asp:TextBox ID="txtDrawnTime" runat="server" Style="margin-left: -18px;" placeholder="Time" ValidationGroup="client" CssClass="GenInput input110"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <label style="font-weight:600">Transit Time (Hrs)</label><br />
                                                    <asp:TextBox ID="txtTransitTime" runat="server" CssClass="input120 GenInput" placeholder="Transit Time"></asp:TextBox>
                                                </div>
                                                  
                                            </div>
                                                <asp:Label ID="lblTransitError" ForeColor="Red" runat="server"></asp:Label>
                                            <div class="row rw2">
                                                <div class="col-lg-5">
                                                    <label style="font-weight:600">Temperature During Transit</label><br />
                                                    <%--<asp:TextBox ID="txtTransitTemp" runat="server" Text="" placeholder="Temperature During Transit" ValidationGroup="client" CssClass="GenInput input260"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlTransitTemp" runat="server" Style="height: 38px; width:90%;" >
                                                    <asp:ListItem Text="Cold" Value="Cold"></asp:ListItem>
                                                    <asp:ListItem Text="Frozen" Value="Frozen"></asp:ListItem>
                                                    <asp:ListItem Text="Room Temperature" Value="Room Temperature"></asp:ListItem>
                                                    <asp:ListItem Text="Warm" Value="Warm"></asp:ListItem>
                                                
                                                </asp:DropDownList>
                                                
                                                
                                                </div>
                                               
                                            </div>
                                            <div class="row">
                                                <%--<div class="col-lg-7">
                                                    <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgTransitTemp" runat="server" ForeColor="Red" Display="Dynamic"  ControlToValidate="txtTransitTemp" ValidationExpression="^((\d+)((\.\d{1,2})?))$" ErrorMessage="Allow numbers and decimal numbers only" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                </div>--%>
                                                <div class="col-lg-7">
                                                    <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgvolumereceived" runat="server" ForeColor="Red" Display="Dynamic"  ControlToValidate="txtvolreceived" ValidationExpression="^((\d+)((\.\d{1,2})?))$" ErrorMessage="Allow numbers and decimal numbers only" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <label style="font-weight:600">Comments</label><br />
                                                    <asp:TextBox ID="txtPotInterfer" runat="server" TextMode="MultiLine" Rows="15" CssClass="GenInput" Style="padding-top: 13px; width: 100%; height: 55px;"></asp:TextBox><br />                                                    
                                                    <asp:RegularExpressionValidator ID="regPotInterfer" runat="server" ErrorMessage="Comments should be within 30 characters only" ControlToValidate="txtPotInterfer" ForeColor="Red" ValidationExpression=".{0,30}"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="well" style="margin-bottom: 20px;">
                                       <div style="margin: 15px; padding: 5px;">
                                            <span style="font-size:22px">Payment Mode</span>
                                            <div class="row rw2">
                                                <div class="col-lg-12">
                                                    <asp:RadioButtonList runat="server" ID="rdoPaymentMode" >
                                                        <asp:ListItem Text="&nbsp;Cash" Value="Cash" />
                                                        <asp:ListItem Text="&nbsp;Credit Card" Value="CreditCard" />
                                                        <asp:ListItem Text="&nbsp;Cheque" Value="Cheque" />
                                                        <asp:ListItem Text="&nbsp;Insurance"  Value="Insurance"/>
                                                        <asp:ListItem Text="&nbsp;Free" Value="Free" />
                                                        <asp:ListItem Text="&nbsp;Charge to physician office" Value="Charge to physician office" />
                                                    </asp:RadioButtonList>   
                                                </div>
                                           </div>
                                         </div>  
                                    </div>

                                    <div class="alert-danger">
                                        <div style="margin: 15px; padding: 5px;">
                                            <h4><b>Verification</b></h4>
                                            <p style="margin-top: 5px; padding-left: 47px;">
                                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkConsentProvided" runat="server" /><span class="lbl"></span>&nbsp;
                                                <label for="ContentPlaceHolder1_chkConsentProvided">Consent Provided ?</label>
                                            </p>
                                            <p style="margin-top: 5px; padding-left: 47px;">
                                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkAcceptTest" runat="server" /><span class="lbl"></span>&nbsp;
                                                <label for="ContentPlaceHolder1_chkAcceptTest">Specimen acceptable for testing ? </label>
                                            </p>
                                                                                      
                                                                                        
                                            <div style="margin: 15px;">

                                                <p style="margin-top: 5px; padding-left: 32px;">
                                                    <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkRejection" runat="server" /><span class="lbl"></span>&nbsp;
                                                <label for="ContentPlaceHolder1_chkRejection">Rejection</label>
                                                </p>

                                                <div style="margin-left: 60px; margin-bottom: 5px; display:none;" id="dvRejectReason">
                                                    <asp:ListBox ID="ddlRejectReason" SelectionMode="Multiple" multiple="" Width="100%" CssClass="select2" data-placeholder="Reason for rejection..." runat="server">
                                                        <asp:ListItem Text="QNS" Value="QNS"></asp:ListItem>
                                                        <asp:ListItem Text="Spill" Value="Spill"></asp:ListItem>
                                                        <asp:ListItem Text="Wrong Specimen Type" Value="Wrong Specimen Type"></asp:ListItem>
                                                        <asp:ListItem Text="Hemolyzed" Value="Hemolyzed"></asp:ListItem>
                                                        <asp:ListItem Text="Others"></asp:ListItem>
                                                    </asp:ListBox>

                                                    <asp:TextBox ID="txtOtherRejectReason" runat="server" CssClass="GenInput input240"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="reqOtherRejectReason" ControlToValidate="txtOtherRejectReason" Style="color:red" ErrorMessage="Required Reason"></asp:RequiredFieldValidator>--%>
                                                    <span id="spRejectReason" style="color: red; display: none">Required Reason
                                                    </span>
                                                </div>

                                            </div>

                                            <p style="margin-top: 5px; padding-left: 47px;">
                                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkOthers" runat="server" /><span class="lbl"></span>&nbsp;
                                                <label for="ContentPlaceHolder1_chkOthers">Other Exception </label>
                                            </p>
                                            <div id="dvother" style="display: none; margin-left: 35px; margin-bottom: 10px;">
                                                <asp:TextBox runat="server" ID="txtPendingOthers" CssClass="GenInput" placeholder="Reason for Exception ..." Style="width: 87%; margin-left:44px; height: 40px; border-radius: 5px;" />
                                                <span id="spnOtherpending" style="display: none; color: red">Exception is Required  </span>
                                                <%--<asp:RequiredFieldValidator ID="reqPendingOthers" ValidationGroup="vgInformation" ControlToValidate="txtPendingOthers" ForeColor="Red" ErrorMessage="Enter other Reason" runat="server"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>                                      
                                    <br />

                                   <div class="row" style="margin-left:4px">
                                        <label><i>No.of request entered today:</i></label>
                                        <a id="ancNoOfReqToday" runat="server" onserverclick="ancNoOfReqToday_ServerClick"></a>
                                    </div>                                    
                                </div>

                                <%--<div class="col-md-6" runat="server" visible="false">
                                    <div class="row">
                                        <div style="margin-left: 18px;">
                                            <h4>Choose Test Profile</h4>
                                        </div>
                                        <div id="dvTestProfile" style="margin-right: 20px; margin-bottom: 5px; margin-left: 17px;">
                                            <asp:DropDownList ID="ddlTestProfiles" multiple=""  CssClass="width-60 chosen-select form-control Test_Profiles" data-placeholder="Test Profiles" runat="server">
                                            </asp:DropDownList>   
                                            <asp:Label ID="lblTest" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>--%>

                                <div class="col-md-6" runat="server">
                                    <div class="row">
                                        <div style="margin-left: 18px;">
                                            <h4 style="margin-top:0px">Upload Test Request Form Copy</h4>
                                        </div>
                                        <div style="margin-right: 20px;margin-bottom:5px; margin-left: 17px;">
                                            <!-- <div class="ace-file-input">
                                                <input type="file" id="id-input-file-1">
                                                <label class="ace-file-container" data-title="Choose">
                                                    <span class="ace-file-name" data-title="No File ...">
                                                        <i class=" ace-icon fa fa-upload"></i></span></label>
                                            </div>-->
                                            <input id="inSampleHardCopy" class="form-control" style="width: 40%; display: inline" placeholder="Choose File" disabled="disabled" />
                                            <div class="fileUpload btn btn-primary" style="border: none">
                                                <span>Choose</span>
                                                <asp:FileUpload ID="flSampleHardCopy" runat="server" CssClass="upload" />
                                                <!--<input id="flSampleHardCopy1" runat="server" type="file" class="upload" accept="image/x-png,image/jpeg" />-->
                                                <asp:HiddenField ID="fileUploadHandler" runat="server" OnValueChanged="fileUploadHandler_ValueChanged" Value="" />
                                            </div>
                                        </div>
                                        <div style="clear: right;"></div>
                                    </div>
                                    <div id="dvPrevImg" style="text-align: center; min-height: 940px; border: 1px dashed #aaa; text-align: center; padding: 5px 5px 5px 5px; margin-left: 5px; margin-right: 8px; background-color: beige; width: 94%;">
                                        <div onclick="clearPreview()" id="dvRemoveImage" class="badge close" style="border: none; background-color:#fff; opacity:1.5; position:absolute; top:88px; left:87%; font-size: 25px;">
                                            <span aria-hidden="true">&times;</span>
                                        </div>
                                        <div id="dvPreview">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" style="margin-right:25px;float:right">
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" ValidationGroup="vgInformation" data-toggle="modal" data-target="#dvConfirmation" runat="server" Text="Save Request Info" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <%-- <div class="col-lg-3">
                <div class="row" >
                      <div class="row" >
                    <div class="alert alert-info" style="height: 650px;">
                        <h4>Last Created Specimen Records</h4>
                        <div class="s
               crollDiv" style= "position:relative; overflow:auto; width:auto;height:600px">
                            <div id="dvRightPanel1">
                                <uc1:SampleList runat="server" ID="SampleList1" IsLoad="true" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="alert alert-danger" style="height:450px;">
                        <h4>Last Pending Specimen Records</h4>
                        <div class="slimScrollDiv" style="position: relative; overflow:auto; width: auto; height: 400px;">
                            <div id="dvRightPanel2">
                                <uc1:PendingSampleList runat="server" ID="PendingSample" IsLoad="true" />
                            </div>
                        </div>
                    </div>
                </div> 
                </div>  
            </div>--%>
        </div>
    </div>
    
    <!-- Begin: Model window -->
    <div id="modal-retrive" class="modal" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">                    
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <span><asp:Button ID="btnGenNum" OnClick="btnGenNum_Click" CssClass="btn btn-primary pull-right" Style="margin-right:30px" runat="server" Text="Generate Specimen Number(s)" /></span>
                    <h4 class="blue bigger">List of Assigned Specimen #s</h4>
                    <asp:HiddenField ID="hPatientID" runat="server" />  
                    <asp:HiddenField ID="hSpecimenID" runat="server" />
                </div>
                <div class="modal-body overflow-visible">                    
                    <asp:Repeater ID="rpGenSpecimenNos" runat="server" OnItemCommand="rpGenSpecimenNos_ItemCommand">
                        <ItemTemplate>
                            <div class="row">
                                <h5>
                                    <asp:LinkButton ID="lbtnGenSpNo" runat="server" CommandArgument='<%# Eval("SpecimenNumber") %>' CommandName="SetSpNumber" Text='<%# Eval("SpecimenNumber") %>'></asp:LinkButton>
                                </h5>
                                <div class="alert alert-danger" id="dvHasnoData" visible="false" runat="server">
                                    No data
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-danger" data-dismiss="modal">
                        <i class="icon-remove"></i>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
       <asp:HiddenField ID="hDocId" Value="0" runat="server" />
    </div>

    <div class="modal fade" id="dvConfirmation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <b>Following things are missing!
                        <br />
                        Are you sure want to proceed specimen Request</b>
                </div>
                <div class="modal-body" id="modalBody">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnModalProceed" class="btn btn-default" runat="server" Text="Proceed" OnClick="btnSave_Click" />
                    <button id="btnModalCancel" type="button" class="btn btn-danger btn-ok" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End: Model window -->

    <div class="modal fade" id="dvSaveMsg" clientidmode="static" runat="server" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
               <div class="modal-header">
                    <label style="color:forestgreen">Success <i class="fa fa-check-circle" aria-hidden="true"></i></label>
                </div>
                <div class="modal-body" id="modal">
                    <span >Specimen <asp:Literal ID="ltrGeneratedSpecimenNumber" runat="server" /> added successfully.
                        <br />
                        Do you want to add another specimen ?</span>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="Yes" OnClick="btnOK_Click" />
                    <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Update more info" OnClick="btnCancel_Click" />
                    <%--<button id="btnCancel" type="button" class="btn btn-danger btn-ok" runat="server" onclick="btnCancel_Click" data-dismiss="modal">Cancel</button>--%>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">

        $(function () {
            $("#ContentPlaceHolder1_chkOthers").click(function () {
                if ($(this).is(":checked")) {
                    $("#ContentPlaceHolder1_txtPendingOthers").val('');
                    $("#dvother").show();

                } else {
                    $("#dvother").hide();
                }
            });
        });

        $(function () {
            $("#ContentPlaceHolder1_chkClosePatient").click(function () {
                if ($(this).is(":checked")) {
                    document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "Patient Code(mandatory)";
                    $("#ContentPlaceHolder1_txtLastName").hide();
                    $("#spLastName").hide();
                    $("#spFirstName").hide();
                    $("#ContentPlaceHolder1_reqDatabirth").hide();
                    //$("#ContentPlaceHolder1_txtDOB").hide();
                    $("#dvDateOfBirth").hide();
                    $("#dvrbtnsex").hide();
                }
                else {
                    document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "First Name(mandatory)";
                    $("#ContentPlaceHolder1_txtLastName").show();
                    $("#dvDateOfBirth").show();
                    $("#dvrbtnsex").show();
                    $("#spLastName").hide();
                    $("#spFirstName").hide();
                    $("#ContentPlaceHolder1_reqDatabirth").hide();
                }
            });



        });
        function nameValidation()
        {
            $('#ContentPlaceHolder1_txtFirstName').on('keyup paste', checkFirstName);
            $('#ContentPlaceHolder1_txtLastName').on('keyup paste', checkLastName);

            

                function checkFirstName() {
                    if ($('#ContentPlaceHolder1_txtFirstName').val() != '') {
                        $("#spFirstName").hide();
                    }
                    else {
                        $("#spFirstName").show();
                        
                    }
            }
       
                function checkLastName() {


                        if ($('#ContentPlaceHolder1_txtLastName').val() != '') {
                            $("#spLastName").hide();
                        }
                        else {
                            $("#spLastName").show();
                            
                        }
                    
                }
            }
      

        $(document).ready(function () {
            
            //nameValidation();
            if ($('#ContentPlaceHolder1_chkRejection').is(":checked")) {
                $('#dvRejectReason').attr('hidden', 'true');
            }             
            //$(".chosen-select").chosen()
            $('#<%= txtOtherRejectReason.ClientID %>').hide();
            $("#" + "<%=ddlRejectReason.ClientID%>").select2({
                // placeholder: "Select a Member",
            });
            $("#" + "<%=ddlRejectReason.ClientID%>").select2("val", "");
            // $('.select2-selection__choice__remove').hide();
            $('.select2 .select2-container .select2-container--default').css("width", 'auto');
            $("#" + "<%=ddlRejectReason.ClientID%>").on("select2:select", function (e) {
                rejectReasonShowHide();
            });
            $("#" + "<%=ddlRejectReason.ClientID%>").on('select2:unselect', function (e) {
                rejectReasonShowHide();

            });
            function rejectReasonShowHide() {
                var selectedData = ($("#" + "<%=ddlRejectReason.ClientID%>").select2("val"));

                var txtData = $('#<%= txtOtherRejectReason.ClientID %>');
                txtData.val("");
                if ($.inArray("Others", selectedData) > -1) {
                    txtData.show();
                }
                else {
                    txtData.hide();
                    $('#spRejectReason').hide();
                }
            }
        });

        $(function () {

            $('#ancRetriveAssigned').click(function () {
                $('#ContentPlaceHolder1_spMsg').text('');
            });
            $('#ContentPlaceHolder1_btnSave').click(function () {
                  
                    if ($('#<%=lblTransitError .ClientID %>').text() == " Drawn date should be before the Received date") {
                        $('#ContentPlaceHolder1_txtTransitTime').focus();
                        return false;
                    }
                
                var selectedData = ($("#" + "<%=ddlRejectReason.ClientID%>").select2("val"));

                var txtData = $('#<%= txtOtherRejectReason.ClientID %>');
                if ($.inArray("Others", selectedData) > -1) {
                    if (txtData.val() == "" || txtData.val() == undefined || txtData.val() == null) {
                        $('#spRejectReason').show();
                        return false;
                    }
                }

                var msg = "";
                var flag = 0;
                var consent = $('#chkConsentProvided')
                if ($('#ContentPlaceHolder1_chkConsentProvided').is(":checked")) {

                }
                else {
                    flag = 1;
                    msg = msg.concat("<ul><li>Consent Request Missing</li></ul>");
                }
                //if ($('#ContentPlaceHolder1_chkRequisition').is(":checked")) {

                //}
                //else {
                //    flag = 1;
                //    msg = msg.concat("<ul><li> Request Completion Missing</li></ul>");
                //}
               
                if ($('#ContentPlaceHolder1_chkClosePatient').is(":checked")) {

                    //alert("First name is mandatory");
                    $("#spFirstName").text("Patient Code is mandatory");

                }
                else {
                    $("#spFirstName").text("First name is mandatory")
                    //alert("is mandatory");
                }


                if ($('#ContentPlaceHolder1_chkClosePatient').is(":not(:checked)")) {

                    if ($('#ContentPlaceHolder1_txtFirstName').val() == "") {
                        flag = 1;
                        $("#spFirstName").show();
                        $("#ContentPlaceHolder1_txtFirstName").focus();

                    }
                    else {
                        $("#spFirstName").hide();
                    }


                    if ($('#ContentPlaceHolder1_txtLastName').val() == "") {
                        $("#spLastName").show();
                        $("#ContentPlaceHolder1_txtLastName").focus();
                        flag = 1;
                       

                    }
                    else {
                        $("#spLastName").hide();
                    }

                    

                   
                    if ($('#ContentPlaceHolder1_txtDOB').val() == "") {
                        flag = 1;
                        $("#ContentPlaceHolder1_reqDatabirth").show();
                        // $("#ContentPlaceHolder1_txtDOB").focus();

                    }
                 
                    
                     }
                else {
                    if ($('#ContentPlaceHolder1_txtFirstName').val() == "") {
                        $("#spFirstName").show();
                        $("#ContentPlaceHolder1_txtFirstName").focus();
                        return false;
                   }
                }

                if ($('#ContentPlaceHolder1_chkClosePatient').is(":not(:checked)"))
                {
                    if ($('#<%=txtFirstName.ClientID %>').val() == "") {
                        
                        $('#ContentPlaceHolder1_txtFirstName').focus();
                        return false;
                    }

                    if ($('#<%=txtLastName.ClientID %>').val() == "") {

                        $('#ContentPlaceHolder1_txtLastName').focus();
                        return false;
                    }
                    if ($('#<%=txtDOB.ClientID %>').val() == "") {

                        $('#ContentPlaceHolder1_txtDOB').focus();
                        return false;
                    }
                }

               






                if ($('#ContentPlaceHolder1_chkClosePatient').is(":not(:checked)")) {
                    
                    if ($('#<%=txtLastName.ClientID %>').val() == "") {

                        $('#ContentPlaceHolder1_txtLastName').focus();
                        return false;
                    }
                }
                 if ($('#ContentPlaceHolder1_chkRejection').is(":checked")) {
                    if ($('#ContentPlaceHolder1_ddlRejectReason').val() == "") {
                        $("#spRejectReason").show();
                        $("#ContentPlaceHolder1_ddlRejectReason").focus();
                       
                        return false;
                    }

                    else {
                        $("#spRejectReason").hide();
                    }
                }


                if ($('#ContentPlaceHolder1_chkOthers').is(":checked"))
                {               
                    if ($('#ContentPlaceHolder1_txtPendingOthers').val() == "")
                    {
                        $("#spnOtherpending").show();
                        $("#ContentPlaceHolder1_txtPendingOthers").focus();
                        return false;
                    }

                    else {
                        $("#spnOtherpending").hide();
                    }
                }

                if (flag == 0) {
                    console.log("s")
                    //$('#ContentPlaceHolder1_btnSave').removeAttr('data-toggle');
                    $("#<%= btnModalProceed.ClientID %>").click();
                        $('#ContentPlaceHolder1_btnSave').val("Processing");
                        $("#ContentPlaceHolder1_btnSave").attr("disabled", "disabled");
                    }
                else
                    {
                        $('#modalBody').empty();
                        $('#modalBody').append(msg);
                        $('#dvConfirmation').addClass('in');
                        $('#dvConfirmation').show();
                        return false;
                    }

                //console.log($('#<%=regPotInterfer.ClientID %>').text())
                //if ($('#ContentPlaceHolder1_regPotInterfer').text() == "Comments should be within 30 characters only") {
                //    $('#ContentPlaceHolder1_txtPotInterfer').focus();
                //    return false;
                //}
            });
            $('#btnModalCancel').click(function () {
                $('#dvConfirmation').removeClass('in');
                $('#dvConfirmation').hide();
            });

            $('#ContentPlaceHolder1_chkRejection').on('click', function () {
                if ($(this).is(":checked")) {
                    $('#dvRejectReason').show();
                    $('#ContentPlaceHolder1_chkAcceptTest').prop('checked', false);

                }
                else {
                    $('#dvRejectReason').hide();
                }
            });

            $('#ContentPlaceHolder1_chkAcceptTest').on('click', function () {
                if ($(this).is(":checked")) {
                    $('#ContentPlaceHolder1_chkRejection').prop('checked', false);
                    $('#dvRejectReason').hide();
                }
                else {

                }
            });




            <%--$('#ContentPlaceHolder1_chkAcceptTest').on('click', function () {

                if ($(this).is(":checked")) {
                    $('#dvRejectReason').attr('hidden', 'true');
                    $("#" + "<%=ddlRejectReason.ClientID%>").val(null).trigger("change");
                    var txtData = $('#<%= txtOtherRejectReason.ClientID %>');
                    txtData.val("");
                    txtData.hide();
                }
                else {
                    $('#dvRejectReason').removeAttr('hidden');
                }
            });--%>

            //$('#ContentPlaceHolder1_txtDrawnTime').timepicker({
            //    timeFormat: 'h:mm p',
            //    interval: 30,
            //    minTime: '12',
            //    maxTime: '12:00pm',
            //    defaultTime: '',
            //    startTime: '12.00am',
            //    dynamic: false,
            //    dropdown: true,
            //    scrollbar: true
            //});
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,

                autoclose: true,
                maxDate: 0
            });
            //$('#ContentPlaceHolder1_txtSampleReceiveDate').datepicker()
            //    .on("hide", function (e) {
            //        var x = $(this).val();
            //        var dt = new Date();
            //        var h = dt.getHours(), m = dt.getMinutes();
            //        var _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');
            //        $(this).val(x + " " + _time);
            //        // `e` here contains the extra attributes
            //    });
            //$("#ContentPlaceHolder1_txtSampleReceiveDate").focusout(function () {
            //    var x = $(this).val();
            //    var dt = new Date();
            //    var h = dt.getHours(), m = dt.getMinutes();
            //    var _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');
            //    $(this).val(x + " " + _time);
            //});

            $("#ContentPlaceHolder1_txtDOB").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+0",
                autoclose: true,
                maxDate: new Date
            });

            $('#DateOfBirth').click(function () {
                //alert('clicked');
                $('#ContentPlaceHolder1_txtDOB').datepicker('show');
            });
            //$(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd hh:ii' });

            $("#ContentPlaceHolder1_txtSampleReceiveDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+0",
                autoclose: true,
                maxDate: new Date(),
            });

            $('#SampleReceiveDate').click(function () {
                // alert('clicked');
                $('#ContentPlaceHolder1_txtSampleReceiveDate').datepicker('show');
            });

            $('#Drawndate').click(function () {
                //alert('clicked');
                $('#ContentPlaceHolder1_txtDrawndate').datepicker('show');
            });
        });
        $('#ContentPlaceHolder1_txtReceivedTime').timepicker();
        $('#ContentPlaceHolder1_txtDrawnTime').timepicker();

        $(document).ready(function () {
            //this calculates values automatically 

            $('#<%=txtTransitTime .ClientID %>').on("focus", function () {
                txtTransitTime();
            });
            $("#dvRemoveImage").hide();
        });

        function txtTransitTime() {
            // Get Variables
            var txtSampleReceiveDate = $('#<%=txtSampleReceiveDate .ClientID %>').val();
            var txtReceivedTime = $('#<%=txtReceivedTime .ClientID %>').val();
            //var h = txtReceivedTime.getHours(), m = txtReceivedTime.getMinutes();
            //var _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');
            txtSampleReceiveDate = new Date(txtSampleReceiveDate + " " + txtReceivedTime);
            var txtDrawndate = $('#<%=txtDrawndate .ClientID %> ').val();
            var txtDrawnTime = $('#<%=txtDrawnTime .ClientID %> ').val();
            txtDrawndate = new Date(txtDrawndate + " " + txtDrawnTime);
            var hours = (txtSampleReceiveDate - txtDrawndate) / 36e5;
            // hours = hours.toFixed(2);
            console.log(hours.toFixed(2));
            /* Add Years to Date
              var txtTransitTime = txtDrawndate - TxtSamReceiDate ;

             var diffSeconds = txtTransitTime / 1000;
              var HH = Math.floor(diffSeconds / 3600);
             var MM = Math.floor(diffSeconds % 3600) / 60;

             var txtTransitTime = ((HH < 10) ? ("0" + HH) : HH) + ":" + ((MM < 10) ? ("0" + MM) : MM)
              $('#result').val(formatted);

             Send Result on */

            if (hours < 0) {
                $('#<%=lblTransitError .ClientID %>').text(" Drawn date should be before the Received date");
                $('#<%=txtTransitTime .ClientID %>').val('');
                //$("#ContentPlaceHolder1_btnSave").attr("disabled", "disabled");
            }
            else {
                $('#<%=txtTransitTime .ClientID %>').val(hours.toFixed(2));
                $('#<%=lblTransitError .ClientID %>').text("");
                $("#ContentPlaceHolder1_btnSave").removeAttr("disabled");
            }
        }
        //  var TxtSamReceiDate = document.getElementById('#<%=txtSampleReceiveDate .ClientID %>').value;
        //var txtDrawndate  = document.getElementById('#<%=txtDrawndate .ClientID %>').value;
        $(function () {
            $("#<%= flSampleHardCopy.ClientID%>").change(function () {
                $("#dvPreview").html("");
                //var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png|.bmp)$/;
                //if (regex.test($(this).val().toLowerCase())) 

                var fileName = $(this).val();
                var extention = (fileName.substr(fileName.lastIndexOf('.') + 1)).toLowerCase();

                if (extention == 'jpg' || extention == 'png' || extention == 'jpeg' || extention == 'bmp' || extention == 'pdf'|| extention == 'gif') {
                    if (typeof (FileReader) != "undefined") {
                        $("#dvPreview").show();
                        $("#dvRemoveImage").show();
                        $("#dvPreview").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $("#dvPreview img").attr("src", e.target.result);
                            $("#dvPreview img").attr("width", '100%');
                        }
                        reader.readAsDataURL($(this)[0].files[0]);
                        $('#inSampleHardCopy').val($(this)[0].files[0].name);
                        $('#<%= fileUploadHandler.ClientID %>').val($(this)[0].files[0].name);
                       // document.getElementById('<%= fileUploadHandler.ClientID%>').value = "upload";
                        //__doPostBack('<%= fileUploadHandler.ClientID%>');
                    }
                    else {
                        alert("This browser does not support FileReader.");
                    }
                }
                else {
                    alert("Please upload a valid image file.");
                }
            });
        });

        function clearPreview() {
            console.log("remove")
            $("#dvPreview").html("");

            $("#<%=flSampleHardCopy.ClientID%>").val('');
            $("#inSampleHardCopy").val('');
            $('#<%= fileUploadHandler.ClientID %>').val("delete");
            $("#dvRemoveImage").hide();

        }
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                console.log("inside client click")
                // disable the button
                myButton.disabled = true;
                myButton.className = "btn-inactive";
                myButton.value = "processing...";
            }

            <%--$("#ddlTestProfiles").on('select2:selecting', function () {
                var st = ($(this).select2('val'));
                $('#<%=lblTest.ClientID %>').val(st);
                console.log(st);
                alert(st);
            }--%>

            <%--var chk = $("#<%=chkSpecimenReceivedKit.ClientID%>");
            if (chk[0].checked)
            {
                var x = $("#<%=txtKitNumber.ClientID%>").val();
                if (x == "")
                {
                    $('#spKitNumber').css("display", "block");
                    return false;
                }
                else
                {
                    $('#spKitNumber').css("display", "none");
                    return true;
                }              
            }
            else
                $("#<%=ShowHidediv.ClientID%>").fadeOut('slow');
            return true;--%>
        }

     //  $('.remove').on('click',')

        <%--$(document).ready(function () {
            var chk = $("#<%=chkSpecimenReceivedKit.ClientID%>");
            if (chk[0].checked) {
                $("#<%=ShowHidediv.ClientID%>").fadeIn('slow');
            }
            else {
                $("#<%=ShowHidediv.ClientID%>").fadeOut('slow');
            }
            $("#<%=chkSpecimenReceivedKit.ClientID%>").change(function () {
                if (this.checked) {
                    $("#<%=ShowHidediv.ClientID%>").fadeIn('slow');
                    $('#<%= txtKitNumber.ClientID%>').focus();
                }
                else {
                    $("#<%=ShowHidediv.ClientID%>").fadeOut('slow');
                }
            });
        });--%>
        //function Showhidediv(chkSpecimenReceivedKit) {
        //    var ShowHidediv = document.getElementById("ShowHidediv");
        //    ShowHidediv.style.display = chkSpecimenReceivedKit.checked ? "block" : "none";
        //}
    </script>
   
</asp:Content>
