
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="SpecimenView.aspx.cs" Inherits="STOMS.UI.pages.SpecimenView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <link href="../style/css/select2/dist/css/select2.css" rel="stylesheet" />
    <link href="../style/js/plugins/datetimepicker/bootstrap/css/bootstrap-datetimepicker.css" rel="stylesheet" />

    <style>
        #ContentPlaceHolder1_dvPatientEdit label, #dvBillingDetails label {
            font-weight: 400;
        }

        .ui-timepicker-container .ui-timepicker-standard {
            z-index: 99999 !important;
        }

        .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 10px;
        }

        .profile-info-value {
            margin-left: 110px;
        }

        p label {
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

        *{
            box-sizing: border-box;
        }

/* The actual timeline (the vertical ruler) */
.timeline {
    position: relative;
    max-width: 1200px;
    margin: 0 auto;
}
/* The actual timeline (the vertical ruler) */
.timeline:before {
    content: '';
    position: absolute;
    width: 0px;
    top: 0;
    bottom: 0;
    left: 0%;
    margin-left: -3px;
}

.timeline::after {
    content: '';
    position: absolute;
    width: 2px;
    background-color: #d8d7d7;
    top: 0;
    bottom: 0;
    left: 0%;
    margin-left: -3px;
}

/* content around content */
.contents {
    padding: 20px 15px;
    background: #f9f9f9;
    height: 60px;
    padding-top:15px;
     border:1px solid #cac7c7;
     margin-left: 21px;
     
}
/* Container around content */
.container {
    padding: 4px 7px;
    position: relative;
    background-color: inherit;
    width: 96%;
}

/* The circles on the timeline */
.container::after {
    content: '';
    position: absolute;
    width: 14px;
    height: 13px;
    background-color: #f56954;
    top: 30px;
    border-radius: 50%;
     margin-left: 0px;
    z-index: 1;
}

/* Fix the circle for containers on the right side */
.right::after {
    left: -16px;
}

/* The actual content */
/*.content {
    padding: 20px 30px;
    background-color: white;
    position: relative;
}*/

</style>
    <script src="../style/js/plugins/datetimepicker/jquery/jquery-1.8.3.min.js"></script>
    <%--<script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>

    <%-- Updated Date and Time Scripts Start --%>
    <%--<script src="../style/js/plugins/datetimepicker/bootstrap/js/bootstrap.js"></script>--%>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.min.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/locales/bootstrap-datetimepicker.fr.js"></script>

    <div class="row">
        <div class="col-md-8" id="dvSpecimenDetail" runat="server">
            <div class="row" runat="server">
                <div class="col-lg-12" style="float: left; color: red; font-size: 20px;">
                    <asp:Literal ID="ltrEmailErrormsg" runat="server"></asp:Literal>
                </div>
                <div class="col-lg-12" style="float: left; color: rgb(12, 156, 18); font-size: 20px;">
                    <asp:Literal ID="ltrEmailSuccessmsg" runat="server"></asp:Literal>
                </div>
            </div>
            <asp:HiddenField ID="hSpecimenID" Value="0" runat="server" />
            <div class="alert alert-info" style="margin-left: 0px; width: 106%">
                <div class="row" style="color: #3a3836;">
                    <div class="col-lg-4">
                        <div class="row">
                            <label id="lblPatientNamecode" runat="server" style="font-weight: 600;">
                                <%-- Patient Name--%>
                            </label>
                            <br />
                            <asp:Label ID="ltrFirstName" runat="server">&nbsp;</asp:Label>
                            <asp:Label ID="ltrLastName" runat="server">&nbsp;</asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Gender/Age</label>
                            <br />
                            <asp:Literal ID="lblAgeGender" runat="server">&nbsp;</asp:Literal>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Specimen Status</label>
                            <br />
                            <span>
                                <asp:Label ID="ltrCurrentSpecimenStatus" runat="server"></asp:Label>&nbsp;</span>
                        </div>
                        <br />
                        <div class="row" runat="server" id="dvPendingReasion" visible="false">
                            <label style="font-weight: 600;">Exception</label>
                            <br />
                            <span>
                                <asp:Label ID="lblExceptionReasion" runat="server"></asp:Label>&nbsp;
                                 <%--<asp:BulletedList ID="BulletedList1" runat="server">  
                                 </asp:BulletedList> --%>
                                <%--<asp:BulletedList ID="bulPendingReasion" BulletStyle="NotSet" runat="server"></asp:BulletedList>--%>
                            </span>
                        </div>

                        <div class="row" runat="server" id="dvRejectionReasion" visible="false">
                            <label style="font-weight: 600;">Rejection Reasion</label>
                            <br />
                            <span>
                                <asp:Label ID="lblRejectionReasion" runat="server"></asp:Label>&nbsp;
                                 <%--<asp:BulletedList ID="BulletedList1" runat="server">  
                                 </asp:BulletedList> --%>
                                <%--<asp:BulletedList ID="bulPendingReasion" BulletStyle="NotSet" runat="server"></asp:BulletedList>--%>
                               
                            </span>
                            <asp:HiddenField ID="hOthersvalues" Value="0" runat="server" />
                            <asp:HiddenField ID="hvaluesget" Value="0" runat="server" />
                        </div>
                        <br />
                    </div>

                    <div class="col-lg-4">
                        <div class="row">
                            <label style="font-weight: 600;">Physician</label>
                            <br />
                            <asp:Label ID="ltrPhyName" runat="server">&nbsp;</asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Facility/Entity</label>
                            <br />
                            <asp:Literal ID="ltrFacility" runat="server">&nbsp;</asp:Literal>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Payment Status</label>
                            <br />
                            <asp:Label ID="lblPaymentStatus" runat="server">Yet to initiate</asp:Label>
                        </div>
                        <br />
                    </div>

                    <div class="col-lg-4">
                        <div class="row" style="text-align: center; margin: 35px 0 0 0; font-size: 45px;">
                            <asp:Literal ID="ltrSpecimenNumber" Text="Sample #" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="" style="margin-right: -26px;">
                    <a id="ancExpandAll" style="float: right" href="#">Expand All</a>
                </div>
            </div>

            <div class="row">
                <div class="panel-group" id="accordion" style="margin-left: 14px; width: 102%">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 data-toggle="collapse" data-parent="#accordion" href="#dvPatientDetails" class="panel-title expand">
                                <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                <a href="#" style="font-size: 14px; font-weight: 600;">Patient Details</a>
                            </h4>
                        </div>

                        <div id="dvPatientDetails" runat="server" clientidmode="static" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div style="margin-right: 7px; margin-bottom: 0px;">
                                    <a href="#dvPatientEdit" style="float: right;" runat="server" id="aModalPatientInfo" clientidmode="static" onserverclick="aModalPatientInfo_ServerClick" role="button" class="red"><i class="fa fa-edit"></i></a>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <br />

                                        <h4 class="blue bigger" style="margin-left: 6px; margin-top: -10px;">Patient Information</h4>
                                        <div class="profile-user-info profile-user-info-striped" id="dvPatientView" runat="server">

                                            <div class="profile-info-row">
                                                <div class="profile-info-name" id="dvPatientNaCo" runat="server"><%--Patient Name--%> </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Label ID="lblFirstName" runat="server"></asp:Label>&nbsp;
                                                <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row" style="min-height: 35px">
                                                <div class="profile-info-name">Location </div>
                                                <div class="profile-info-value" style="margin-left: 110px;">
                                                    <span>
                                                        <asp:Literal ID="ltrLocation" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">Gender / Age </div>
                                                <div class="profile-info-value" style="margin-left: 110px;">
                                                    <span>
                                                        <asp:Literal ID="ltrAgeGender" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">Email ID </div>
                                                <div class="profile-info-value" style="margin-left: 110px;">
                                                    <span>
                                                        <asp:Literal ID="ltrEmailID" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">Contact No </div>
                                                <div class="profile-info-value" style="margin-left: 110px;">
                                                    <span>
                                                        <asp:Literal ID="ltrContactNo" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <h4 class="blue bigger" style="margin-left: 6px;">Responsible Party / Guardian</h4>
                                        <div class="profile-user-info profile-user-info-striped" id="dvGuardianView" runat="server">

                                            <div class="profile-info-row" style="min-height: 35px;">
                                                <div class="profile-info-name" style="width: 26%;">Name </div>
                                                <div class="profile-info-value" style="margin-left: 88px;">
                                                    <span>
                                                        <asp:Literal ID="ltrGuardianName" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row" style="min-height: 35px">
                                                <div class="profile-info-name" style="width: 26%;">Relationship </div>
                                                <div class="profile-info-value" style="margin-left: 88px;">
                                                    <span>
                                                        <asp:Literal ID="lblGRelationship" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row" style="min-height: 35px;">
                                                <div class="profile-info-name" style="width: 26%;">Address </div>
                                                <div class="profile-info-value" style="margin-left: 88px;">
                                                    <span>
                                                        <asp:Literal ID="ltrGuardianAddress" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row" style="min-height: 35px;">
                                                <div class="profile-info-name" style="width: 26%;">Email ID </div>
                                                <div class="profile-info-value" style="margin-left: 88px;">
                                                    <span>
                                                        <asp:Literal ID="ltrGuardianEmailID" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row" style="min-height: 35px;">
                                                <div class="profile-info-name" style="width: 26%;">Contact No </div>
                                                <div class="profile-info-value" style="margin-left: 88px;">
                                                    <span>
                                                        <asp:Literal ID="ltrGuardianContactNo" runat="server"></asp:Literal>&nbsp;</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvClosePatient" style="display: none;">

                                    <h4>
                                        <asp:CheckBox ID="chkClosePatient" runat="server" Style="margin-left: 8px;" />
                                        <span class="lbl"></span>&nbsp;
                                         <label style="font-size: 16px; font-weight: 400;" for="ContentPlaceHolder1_chkClosePatient">Undisclosed Patient Information</label></h4>
                                </div>
                                <div id="dvPatientEdit" visible="false" runat="server">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:HiddenField ID="hPatientID" runat="server" />
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b id="bFirstNameCode" runat="server"><%--First Name--%><i style="color: red; font-size: 18px;">*</i></b><br />
                                                <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="GenInput input280"></asp:TextBox><br />
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFirstName" ID="rfPatientName" runat="server" ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgPatientName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters(.-,') Only" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="row" id="dvLastName" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Last Name</b><br />
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="input280 GenInput" placeholder="Last Name"></asp:TextBox><br />
                                                <%-- <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtLastName" ID="rfLastName" runat="server" ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>--%>

                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgLastName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters(.-,) Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="row" id="dvGender" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Gender</b>
                                                <asp:RadioButton ID="optM" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-male blue"></i>&nbsp;Male
                                                <asp:RadioButton ID="optF" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-female pink"></i>&nbsp;Female                            
                                                <asp:RadioButton ID="optUn" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-user blue"></i>&nbsp;UnKnown
                                            </div>
                                            <div class="row" id="dvDateOfBirth" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Date Of Birth</b>
                                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="date-picker GenInput" placeholder="Date of Birth" Style="margin-top: 0px; margin-bottom: 0px;"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <i class="fa fa-calendar" id="DateOfBirth"></i>
                                                        </span>
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDOB" ID="rfDateOfBirth" runat="server" ErrorMessage="DateOfBirth is required"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Email ID</b><br />
                                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="input280 GenInput" placeholder="Email ID"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
                                                    runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID" ID="erqtxtEmail" runat="server" ErrorMessage="EmailID is required"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Contact No</b><br />
                                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="input280 GenInput" placeholder="Contact No"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails"
                                                    ID="RegularExpressionValidator3" runat="server" ForeColor="Red" Display="Dynamic"
                                                    ErrorMessage="Allow numbers and some characters(+ / -) Only" ControlToValidate="txtContactNo"
                                                    ValidationExpression="([a-zA-Z0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                <%-- <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtContactNo" ID="reqtxtContactNo" runat="server" ErrorMessage="ContactNo is required"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <br />
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Address</b><br />
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtstreet" runat="server" CssClass="input280 GenInput" placeholder="Street"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtstreet" ID="rfStreet" runat="server" ErrorMessage="Street is required"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="input280 GenInput" placeholder="City"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCity" ID="rfCity" runat="server" ErrorMessage="City is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails1" ID="rgCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtCity" ValidationExpression="^[a-zA-Z\.\-\(\)+\ ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 10px; margin-bottom: 5px;">
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtState" runat="server" CssClass="input190 GenInput" placeholder="State"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtState" ID="rfState" runat="server" ErrorMessage="State is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails1" ID="rgState" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtState" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="txtZip" runat="server" CssClass="input80 GenInput" Style="margin-left: 6px;" placeholder="Zip"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtZip" ID="rfZip" runat="server" ErrorMessage="Zip is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails1" ID="rgZip" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Chars and Numeric(.-,) Only" ControlToValidate="txtZip" ValidationExpression="^[a-zA-Z0-9'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="input280 GenInput" placeholder="Country"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCountry" ID="rfCountry" runat="server" ErrorMessage="Country is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails1" ID="rgCountry" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Relationship<%--<i style="color: red; font-size: 18px;">*</i>--%></b><br />
                                                <asp:DropDownList ID="ddlGRelationship" runat="server" CssClass="GenInput input280">
                                                    <asp:ListItem Text="--Please Select Relationship--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Self" Value="Self"></asp:ListItem>
                                                    <asp:ListItem Text="Parent" Value="Parent"></asp:ListItem>
                                                    <asp:ListItem Text="Uncle" Value="Uncle"></asp:ListItem>
                                                    <asp:ListItem Text="Spouse" Value="Spouse"></asp:ListItem>
                                                    <asp:ListItem Text="Guardian" Value="Guardian"></asp:ListItem>
                                                </asp:DropDownList><br />
                                                <%-- <asp:RequiredFieldValidator InitialValue="0" ID="reqRelationship" Display="Dynamic" ValidationGroup="vPatDetails" runat="server" ForeColor="Red" ControlToValidate="ddlGRelationship" ErrorMessage="Please select Relationship"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>First Name<%--<i style="color: red; font-size: 18px;">*</i>--%></b><br />
                                                <asp:TextBox ID="txtGuardianFirstName" runat="server" placeholder="First Name" CssClass="GenInput input280"></asp:TextBox><br />
                                                <%--  <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianFirstName" ID="rfGuardianFirstName" runat="server" ErrorMessage="First Name is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianFirstName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters(.-,) Only" ControlToValidate="txtGuardianFirstName" ValidationExpression="^[a-zA-Z'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Last Name<%--<i style="color: red; font-size: 18px;">*</i>--%></b><br />
                                                <asp:TextBox ID="txtGuardianLastName" Width="100%" runat="server" CssClass="GenInput input280" placeholder="Last Name"></asp:TextBox><br />
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianLastName" ID="rfGuardianLastName" runat="server" ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianLastName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters(.-,) Only" ControlToValidate="txtGuardianLastName" ValidationExpression="^[a-zA-Z'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                            <br />
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Email ID</b><br />
                                                <asp:TextBox ID="txtGuardianEmailID" runat="server" CssClass="input280 GenInput" placeholder="Email ID"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator2"
                                                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianEmailID"
                                                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
                                                    runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                                <%-- <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianEmailID" ID="reqtxtGuardianEmailID" runat="server" ErrorMessage="EmailID is required"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Contact No</b><br />
                                                <asp:TextBox ID="txtGuardianContactNo" runat="server" CssClass="input280 GenInput" placeholder="Contact No"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ValidationGroup="vPatDetails"
                                                    ID="RegularExpressionValidator4" runat="server" ForeColor="Red" Display="Dynamic"
                                                    ErrorMessage="Allow numbers and some characters(+ / -) Only" ControlToValidate="txtGuardianContactNo"
                                                    ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianContactNo" ID="reqtxtGuardianContactNo" runat="server" ErrorMessage="ContactNo is required"></asp:RequiredFieldValidator>--%>
                                                <%--<asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="RegularExpressionValidator1" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <br />
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <asp:CheckBox Text="&nbsp;Guardian address same as patient address" ID="chkSameAddress" OnCheckedChanged="chkSameAddress_CheckedChanged" AutoPostBack="true" runat="server" />
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                                <b>Address</b><br />
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtGuardianStreet" runat="server" CssClass="input280 GenInput" placeholder="Street"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianStreet" ID="rfGuardianStreet" runat="server" ErrorMessage="Street is required"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtGuardianCity" runat="server" CssClass="input280 GenInput" placeholder="City"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianCity" ID="rfGuardianCity" runat="server" ErrorMessage="City is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianCity" ValidationExpression="^[a-zA-Z\.\-\(\)+\ ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 10px; margin-bottom: 5px;">
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtGuardianState" runat="server" CssClass="input190 GenInput" placeholder="State"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianState" ID="rfGuardianState" runat="server" ErrorMessage="Country is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianState" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianState" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="txtGuardianZip" runat="server" CssClass="input80 GenInput" Style="margin-left: 6px;" placeholder="Zip"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianZip" ID="rfGuardianZip" runat="server" ErrorMessage="Zip is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianZip" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Chars and Numeric(.-,) Only" ControlToValidate="txtGuardianZip" ValidationExpression="^[a-zA-Z0-9'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtGuardianCountry" runat="server" CssClass="input280 GenInput" placeholder="Country"></asp:TextBox><br />
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="vPatDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtGuardianCountry" ID="rfGuardianCountry" runat="server" ErrorMessage="City is required"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ValidationGroup="vPatDetails" ID="rgGuardianCountry" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtGuardianCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-offset-7">
                                            <asp:Button ID="btnPatientSave" Style="" CssClass="btn btn-primary" OnClick="btnPatientSave_Click" runat="server" CausesValidation="false" TabIndex="6" OnClientClick="return (Page_ClientValidate('vPatDetails'));" Text="Save Patient Info" />
                                            <asp:Button ID="btnPatientBack" Style="" CssClass="btn btn-danger" Text="Cancel" runat="server" OnClick="btnPatientBack_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default" style="margin-top: 8px;">
                        <div class="panel-heading">
                            <h4 data-toggle="collapse" data-parent="#accordion" href="#dvPhysicianDetails" class="panel-title expand">
                                <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                <a href="#" style="font-size: 14px; font-weight: 600;">Ordering Party Details</a>
                            </h4>
                        </div>

                        <div id="dvPhysicianDetails" runat="server" clientidmode="static" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div class="row">
                                    <div style="margin-right: 7px; margin-bottom: 20px;">
                                        <a href="#dvPhysicianEdit" role="button" id="aReqPhysicianDetails" runat="server" class="red" style="float: right" onserverclick="aReqPhysicianDetails_ServerClick"><i class="fa fa-edit"></i></a>
                                        <h4 class="blue bigger" style="margin: 5px 0 5px 10px">Physician Information</h4>
                                    </div>
                                    <div class="profile-user-info profile-user-info-striped" id="dvPhysicianView" runat="server">
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Physician </div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblPhyName" runat="server"></asp:Label>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <%--<div class="profile-info-row">
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
                                            </div>--%>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Facility </div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblFacility" runat="server"></asp:Label>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Email </div>
                                            <div class="profile-info-value" style="margin-left: 110px;">
                                                <span>
                                                    <asp:Label ID="lblPhyEmail" runat="server"></asp:Label>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <%--<div class="profile-info-row">
                                                <div class="profile-info-name">Prefering Result Type </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Label ID="lblResultType" runat="server"></asp:Label>&nbsp;
                                                    </span>
                                                </div>
                                            </div>--%>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Address </div>
                                            <div class="profile-info-value" style="margin-left: 110px;">
                                                <span>
                                                    <asp:Literal ID="ltrReqAddress" runat="server"></asp:Literal>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Contact Number</div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Literal ID="ltrReqContact" runat="server"></asp:Literal>&nbsp;</span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Fax Number</div>
                                            <div class="profile-info-value" style="margin-left: 110px;">
                                                <span>
                                                    <asp:Literal ID="ltrFaxNumber" runat="server"></asp:Literal>&nbsp;</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="dvPhysicianEdit" runat="server" visible="false" style="margin-left: 2%">
                                        <%--<div class="row" style="margin: 5px 0 5px 10px">
                                            <asp:DropDownList ID="ddNew1" runat="server" Visible="true" CssClass="input100 GenDD">
                                                <asp:ListItem Text="New" Value="New" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Existing" Value="Exist"></asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddPhy1" runat="server" Visible="false" CssClass="input300 GenDD" DataTextField="CustomerName" DataValueField="CustomerID">
                                            </asp:DropDownList>&nbsp;--%>
                                        <asp:HiddenField ID="hCustID" Value="0" runat="server" />
                                        <%--</div>--%>
                                        <div class="row" style="margin: 5px 0 5px 0px">
                                            <div class="col-lg-5">
                                                <span class="editable" id="Span7">
                                                    <b>Physician Name<i style="color: red; font-size: 18px;">*</i></b><br />
                                                    <span class="input-group-addon" style="width: 12%; height: 41px; margin-left: -6px; border-right: none; margin-bottom: 5px; padding-top: 13px; display: inline-block;">Dr</span>
                                                    <asp:TextBox ID="txtPhyName" runat="server" Style="margin-left: -3px; height: 41px;" placeholder="Physician Name" CssClass="GenInput input250"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyName" ID="rfdPhyName" runat="server" ErrorMessage="Physician Name is required"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgphyName" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyName" ValidationExpression="^[a-zA-Z'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                </span>

                                            </div>
                                            <div class="clo-lg-3" style="margin-top: 5px;">
                                                <span class="editable">
                                                    <b>Specialization</b><br />

                                                    <asp:TextBox ID="txtSpecialization" runat="server" Style="width: 14%; margin-left: 4px;" placeholder="specialization" CssClass="GenInput"></asp:TextBox>
                                                </span>
                                            </div>
                                        </div>
                                        <%--<div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable">
                                                <asp:TextBox ID="txtDiagnosis" runat="server" placeholder="Diagnosis" CssClass="GenInput input420"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDiagnosis" ID="rfdDiagonosis" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgDiagonosis" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtDiagnosis" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable">
                                                <asp:TextBox ID="txtDiagnosisCode" runat="server" placeholder="Diagnosis Code" CssClass="GenInput input420"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDiagnosisCode" ID="rfDiagonosisCode" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>--%>
                                        <%--<div class="row" style="margin: 5px 0 5px 10px">
                                        <span class="editable">Preferred Method for Receiving Results
                                            <asp:DropDownList ID="ddlResultType" runat="server" CssClass="input100 GenDD">
                                                <asp:ListItem Text="Blocking" Value="Blocking" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Binding" Value="Binding"></asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </div>--%>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable">
                                                <b>Facility</b><br />
                                                <asp:TextBox ID="txtPhyFacility" runat="server" CssClass="input420 GenInput" placeholder="Facility Name" ValidationGroup="client"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyFacility" ID="rfFacility" runat="server" ErrorMessage="Facility is required"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span1">
                                                <b>Street</b><br />
                                                <asp:TextBox ID="txtPhyAddress1" runat="server" CssClass="input420 GenInput" placeholder="Street" ValidationGroup="client"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyAddress1" ID="rfPhyAddress" runat="server" ErrorMessage="Address is required"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span3">
                                                <b>City</b><br />
                                                <asp:TextBox ID="txtPhyCity" runat="server" CssClass="input420 GenInput" placeholder="City" ValidationGroup="client"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyCity" ID="rfPhyCity" runat="server" ErrorMessage="City is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyCity" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyCity" ValidationExpression="^[a-zA-Z\.\-\(\)+\ ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span4">
                                                <span>
                                                    <b>State</b><b style="margin-left: 260px">ZipCode</b><br />
                                                    <asp:TextBox ID="txtPhyState" runat="server" CssClass="input289 GenInput" placeholder="State / Province"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyState" ID="rfPhyState" runat="server" ErrorMessage="State is required"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyState" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtPhyState" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                    &nbsp;&nbsp;                                                    
                                                </span>
                                                <asp:TextBox ID="txtPhyPCode" runat="server" CssClass="input120  GenInput" placeholder="Zip Code"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyPCode" ID="rfPhyCode" runat="server" ErrorMessage="Zipcode is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyCode" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Chars and Numeric(.-,) Only" ControlToValidate="txtPhyPCode" ValidationExpression="^[a-zA-Z0-9'.\s,-]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span5">
                                                <b>Country</b><br />
                                                <asp:DropDownList ID="ddCountry" runat="server" CssClass="input420 GenDD">
                                                    <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
                                                    <asp:ListItem Text="Canada" Value="Canada"></asp:ListItem>
                                                    <asp:ListItem Text="Japan" Value="japan"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span6">
                                                <b>Email</b><br />
                                                <asp:TextBox ID="txtPhyEmail" runat="server" CssClass="input420 GenInput" placeholder="Email"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyEmail" ID="rfPhyEmail" runat="server" ErrorMessage="Email is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyEmail" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyEmail" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <span class="editable" id="Span8">
                                                <b>Contact Number</b><b style="margin-left: 112px">Fax</b><br />
                                                <asp:TextBox ID="txtPhyPhone" placeholder="Phone number" CssClass="input210 GenInput" runat="server" />
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyPhone" ID="rfPhyPhone" runat="server" ErrorMessage="Contact number is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyPhone" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and some characters(+()/-) Only" ControlToValidate="txtPhyPhone" ValidationExpression="([a-zA-Z0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                &nbsp;&nbsp;                                    
                                                <asp:TextBox ID="txtPhyFax" placeholder="Fax number" CssClass="input200 GenInput" runat="server" />
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vPhyDetails" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhyFax" ID="rfFax" runat="server" ErrorMessage="Fax is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ValidationGroup="vPhyDetails" ID="rgPhyFax" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and some characters(+()/-) only" ControlToValidate="txtPhyFax" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin: 5px 0 5px 10px">
                                            <div class="col-md-offset-3">
                                                <asp:Button ID="btnSaveReq" CssClass="btn btn-primary" ValidationGroup="vPhyDetails" runat="server" OnClick="btnSaveReq_Click" Text="Save Requesting Party" />
                                                <asp:Button ID="btnPhysicianBack" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnPhysicianBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default" style="margin-top: 8px;">
                        <div class="panel-heading">
                            <h4 data-toggle="collapse" data-parent="#accordion" href="#dvTestSpecDetails" class="panel-title expand">
                                <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                <a href="#" style="font-size: 14px; font-weight: 600;">Test and Specimen Details</a>
                            </h4>
                        </div>

                        <div id="dvTestSpecDetails" runat="server" clientidmode="static" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div class="col-md-6">
                                    <div style="margin-left: 3px;">
                                        <h4 class="blue bigger">Specimen Information&nbsp;&nbsp;
                                            <a href="#dvSpecimenEdit" role="button" id="aSpecimenInformation" runat="server" onserverclick="aSpecimenInformation_ServerClick" class="red"><i class="fa fa-edit" style="float: right; margin-right: 6px;"></i></a></h4>
                                    </div>

                                    <div id="dvSpecimenView" class="profile-user-info profile-user-info-striped" runat="server">
                                        <div class="profile-info-row" style="height: 50px">
                                            <div class="profile-info-name">Specimen Number</div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblSpecimenNumber" runat="server"></asp:Label>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Test Type</div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblTestType" runat="server"></asp:Label>&nbsp;
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Specimen Type </div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lbltype" runat="server"></asp:Label>&nbsp;</span>
                                            </div>
                                        </div>
                                        <%-- <div class="profile-info-row" id="viewBloodType" runat="server">
                                            <div class="profile-info-name">Blood Type </div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblBloodType" runat="server"></asp:Label>&nbsp;</span>
                                            </div>
                                        </div>--%>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name">Received On</div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblReceivedOn" runat="server"></asp:Label>&nbsp;(date & time)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name" id="dvDrawnDate" runat="server">Drawn Date</div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lbldrawnDatetime" runat="server"></asp:Label>&nbsp;(date & time)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row" style="height: 50px;">
                                            <div class="profile-info-name">Transit Time(Hrs)</div>
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
                                            <div class="profile-info-name" id="viewVolReceived" runat="server">Vol. Received </div>
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
                                            <div class="profile-info-name">Current Status </div>
                                            <div class="profile-info-value">
                                                <span>
                                                    <asp:Label ID="lblCurrentSpecimenStatus" runat="server"></asp:Label></span>
                                            </div>
                                        </div>
                                        <%-- <div class="profile-info-row" id="specimenform" runat="server">
                                        <div class="profile-info-name" >Specimen Form </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <a id="a1" runat="server"></a>&nbsp;</span>
                                        </div>
                                    </div>
                                     <div class="profile-info-row" id="Div1" runat="server">
                                        <div class="profile-info-name" >Specimen Form Upload </div>
                                         <div class="profile-info-value">
                                             <div style="margin-left: 3px;">
                                             </div>
                                             <div>
                                                 <input id="inSampleHardCopy" class="form-control" style="display: inline" placeholder="Choose File" disabled="disabled" />                                                 
                                                 <br />
                                                 <div class="fileUpload btn btn-primary" style="border: none">
                                                     <span>Choose</span>
                                                     <asp:FileUpload ID="FileUpload1" runat="server" CssClass="upload" />
                                                     <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                                                 </div>
                                                 &nbsp;&nbsp;
                                            <asp:Button ID="Button1" CssClass="btn btn-success" OnClick="btnUpload_Click" runat="server" Text="Upload" />
                                             </div>
                                         </div>
                                    </div>--%>
                                    </div>

                                    <div id="dvSpecimenEdit" style="margin-top: 30px;" runat="server" visible="false">
                                        <%-- <h4 class="blue bigger">Specimen Information</h4>--%>
                                        <%--<div class="row rw2" style="margin-top: 5px; margin-left: 0px; margin-bottom: 5px;">
                                            <b>Specimen Type</b><br />
                                            <asp:RadioButton ID="rdSpecimenserum" GroupName="Specimengroup" runat="server" />&nbsp;&nbsp;Serum
                                        <asp:RadioButton ID="rdSpecimenblood" GroupName="Specimengroup" runat="server" />&nbsp;&nbsp;Blood&nbsp;&nbsp;  
                                        </div>--%>

                                        <div class="row rw2" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-7">
                                                <label style="font-weight: 600">Requested Test</label>
                                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                    <div class=" col-lg-12 input-group">
                                                        <asp:DropDownList ID="ddlRequestedTest" CssClass="form-control" Style="height: 38px; width: 124%;" OnSelectedIndexChanged="ddlRequestedTest_SelectedIndexChanged" runat="server">
                                                            <asp:ListItem disabled="disabled" Value="0">Select Test</asp:ListItem>
                                                            <%--<asp:ListItem Text="FRAT" Value="Frat"></asp:ListItem>--%>
                                                            <asp:ListItem Text="FRAT" Value="Frat"></asp:ListItem>
                                                            <asp:ListItem Text="Mitochondrial Dysfunction Test" Value="Mitochondrial Dysfunction Test"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-7">
                                                <b>Specimen Type</b><br />
                                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                    <div class=" col-lg-12 input-group">
                                                        <asp:DropDownList ID="ddlSpecimenType" CssClass="form-control" Style="height: 38px; width: 124%;" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="dvBloodType" runat="server" visible="false" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-5">
                                                    <b>Blood Type<i style="color: red; font-size: 18px;">*</i></b><br />
                                                    <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px; width: 100px;">
                                                        <div class=" col-lg-12 input-group">
                                                            <asp:DropDownList ID="ddlBloodType" Style="width: 115px;" runat="server">
                                                                <asp:ListItem Text="Choose Specimen" Value="-1" Enabled="false"></asp:ListItem>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row rw2" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-7">
                                                <b><span id="editDrawnDate" runat="server">Drawn Date</span></b><br />
                                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtDrawndate" runat="server" Style="width: 150px; margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker GenInput" placeholder="Drawn Date"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <i class="fa fa-calendar" id="drawnDate"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDrawndate" ID="rfDrawndate" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-lg-5">
                                                <b>Drawn Time</b><br />
                                                <asp:TextBox ID="txtDrawnTime" runat="server" placeholder="Drawn Time" ValidationGroup="client" CssClass="GenInput" Style="width: 115px;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row rw2" style="margin-top: 15px; margin-bottom: 5px;">
                                            <div class="col-lg-7">
                                                <b>Received Date</b><br />
                                                <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtReceivedOn" runat="server" Width="150px" CssClass="date-picker GenInput" placeholder="Received Date" Style="margin-top: 0px; margin-bottom: 0px"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <i class="fa fa-calendar" id="receivedDate"></i>
                                                        </span>
                                                    </div>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtReceivedOn" ID="rfReceivedOn" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-5">
                                                <b>Received Time</b><br />
                                                <asp:TextBox ID="txtReceivedTime" runat="server" placeholder="Received Time" ValidationGroup="client" CssClass="GenInput" Style="width: 115px;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row rw2" style="margin-top: 15px; margin-bottom: 5px;">
                                            <div class="col-lg-5">
                                                <b>Transit Time(Hrs)</b>
                                                <asp:TextBox ID="txtTransitTime" runat="server" CssClass="GenInput" Style="width: 115px;" placeholder="Transit Time"></asp:TextBox>
                                                <asp:Label ID="lblTransitError" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row rw2" style="margin-top: 15px; margin-bottom: 5px;">
                                            <div class="col-lg-7">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <b>Temperature during transit</b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <%-- <asp:TextBox ID="txtTransitTemp" runat="server" placeholder="Temperature during transit" ValidationGroup="client" CssClass="GenInput input190"></asp:TextBox>--%>

                                                        <asp:DropDownList ID="ddlViewTransitTemp" runat="server" CssClass="GenInput input180">
                                                            <asp:ListItem Text="Cold" Value="Cold"></asp:ListItem>
                                                            <asp:ListItem Text="Frozen" Value="Frozen"></asp:ListItem>
                                                            <asp:ListItem Text="Room Temperature" Value="Room Temperature"></asp:ListItem>
                                                            <asp:ListItem Text="Warm" Value="Warm"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                                <%-- <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTransitTemp" ID="rfTransitTemp" runat="server" ErrorMessage="Temperature is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgTransitTemp" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtTransitTemp" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-lg-5">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <b>Volume received</b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtVolReceived" runat="server" Style="width: 124px; margin-left: -8px;" CssClass="GenInput" placeholder="Volume received"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%-- <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtVolReceived" ID="rfVolReceived" runat="server" ErrorMessage="Volume is required"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="rgVolReceived" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtVolReceived" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="row rw2" style="margin-top: 15px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <b>Comments</b><br />
                                                <asp:TextBox ID="txtPotInterfer" runat="server" TextMode="MultiLine" MaxLength="1" CssClass="GenInput input240"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regPotInterfer" runat="server" ErrorMessage="Comments with in 30 characters only" ControlToValidate="txtPotInterfer" ForeColor="Red" ValidationExpression="^[a-zA-Z\s]{0,30}$" />
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                            <div class="col-lg-6">
                                            <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtReceivedOn" ID="rfReceivedOn" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-6">
                                             <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDrawndate" ID="rfDrawndate" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                        <div class="row rw2" style="margin-top: 0px; margin-left: 10px; margin-bottom: 5px;">
                                            <div class="col-lg-6">
                                                <div class="row">
                                                    &nbsp;
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                            <div class="col-lg-6">
                                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTransitTemp" ID="rfTransitTemp" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgTransitTemp" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtTransitTemp" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:RequiredFieldValidator ValidationGroup="vSpecimenInformation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtVolReceived" ID="rfVolReceived" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vSpecimenInformation" ID="rgVolReceived" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers and decimal numbers Only" ControlToValidate="txtVolReceived" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>--%>
                                        <div class="row" style="margin-top: -5px; margin-left: 10px; margin-bottom: 5px;">
                                            <asp:Button ID="btnSpecimen" CssClass="btn btn-primary" ValidationGroup="vSpecimenInformation" AutoPostBack="true" OnClick="btnSpecimen_Click" runat="server" Text="Save Specimen Info" />
                                            <asp:Button ID="btnSpecimenBack" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnSpecimenBack_Click" />
                                        </div>
                                    </div>

                                    <%--<div class="row" style="margin-left: 5px; margin-top: 10px;">
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" AutoPostBack="true" OnClick="btnSave_Click" runat="server" Visible="false" Text="Save Request Info" />
                                    </div>--%>
                                </div>
                                <div class="col-md-6">
                                    <div style="margin-left: 3px;" runat="server" id="dvResCap" visible="true">
                                        <h4 class="blue bigger">Results&nbsp;&nbsp;
                                          <a href="#dvResultEdit" role="button" class="red" runat="server" id="aresults" onserverclick="aresults_ServerClick"><i class="fa fa-edit" style="float: right; margin-right: 9px;"></i></a>
                                        </h4>
                                        <a id="ancRecResults" runat="server" onserverclick="ancRecResults_ServerClick">Click here for recording results</a>
                                        <a style="padding: 10px 10px 10px 0px" visible="false" id="aMitPreview" runat="server">Preview</a>
                                        <asp:LinkButton Visible="false" Text="Download" ID="BtnMitDownload" OnClick="BtnMitDownload_Click" runat="server" />
                                    </div>
                                    <div runat="server" id="dvResultView" visible="true">
                                        <div class="profile-user-info profile-user-info-striped">
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
                                                        <asp:LinkButton ID="lnkGenRpt" runat="server" Visible="false" OnClick="lnkGenRpt_Click" Text="Generate Report"></asp:LinkButton>
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
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Button ID="btnVerifyAndSendEmail" Style="margin-left: 8px" CssClass="btn btn-info" Text="Verify and send Email" OnClick="btnVerifyAndSendEmail_Click" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="dvResultMsg" style="margin-right: 10px; margin-left: 5px" class="alert alert-danger">
                                        <asp:Literal ID="ltrResultMsg" runat="server"></asp:Literal>
                                    </div>

                                    <div id="dvResultEdit" runat="server" visible="false">
                                        <b style="color: red;">
                                            <asp:Literal Text="" ID="ltrAssayError" runat="server" />
                                        </b>
                                        <div class="row" style="margin-top: 5px; margin-left: 25px; margin-bottom: 19px;">
                                            <b>Remaining Specimen Type</b><br />
                                            <asp:DropDownList ID="ddlRemSpecimenType" runat="server">
                                            </asp:DropDownList>
                                            <%--<asp:RadioButton ID="optRemainSerum" GroupName="spType" runat="server" />&nbsp;&nbsp;Serum&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="optRemainBlood" GroupName="spType" runat="server" />&nbsp;&nbsp;Blood&nbsp;&nbsp;--%>
                                            <%--<asp:DropDownList ID="ddBloodType" runat="server">
                                                <asp:ListItem Text="A -ve" Value="A -ve"></asp:ListItem>
                                                <asp:ListItem Text="A +ve" Value="A +ve"></asp:ListItem>
                                                <asp:ListItem Text="B -ve" Value="B -ve"></asp:ListItem>
                                                <asp:ListItem Text="B +ve" Value="B +ve"></asp:ListItem>
                                                <asp:ListItem Text="AB -ve" Value="AB -ve"></asp:ListItem>
                                                <asp:ListItem Text="AB +ve" Value="AB +ve"></asp:ListItem>
                                                <asp:ListItem Text="O -ve" Value="O -ve"></asp:ListItem>
                                                <asp:ListItem Text="O +ve" Value="O +ve"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </div>

                                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                            <div class="col-lg-6">
                                                <b>Remaining Volume</b><br />
                                                <asp:TextBox ID="txtRemainVol" placeholder="Remaining Volume" CssClass="input150 GenInput" runat="server" />
                                                <%--<asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtRemainVol" ID="rfRemainingVolume" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgRemainingVolume" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtRemainVol" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-lg-6">
                                                <b>Binding Value</b><br />
                                                <asp:TextBox ID="txtBindValue" placeholder="Binding Value" CssClass="input150 GenInput" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                            <div class="col-lg-6">
                                                <b>Comments</b><br />
                                                <asp:TextBox ID="txtBindComment" placeholder="Comments" TextMode="MultiLine" Rows="15" CssClass="input200 GenInput" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 0px;">
                                            <%--<div class="col-lg-6">
                                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBindValue" ID="rfBindValue" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBindValue" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtBindValue" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>--%>
                                            <%--<div class="col-lg-6">
                                            <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBindComment" ID="rfBindComment" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBindComment" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtBindComment" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>--%>
                                        </div>

                                        <div class="row" style="margin-top: 0px; margin-left: 10px; margin-bottom: 5px;">
                                            <div class="col-lg-6">
                                                <b>Blocking Value</b><br />
                                                <asp:TextBox ID="txtBlockValue" placeholder="Blocking Value" CssClass="input150 GenInput" runat="server" />
                                            </div>
                                            <div class="col-lg-6">
                                                <b>Comments</b><br />
                                                <asp:TextBox ID="txtBlockComment" placeholder="Comments" TextMode="MultiLine" Rows="15" CssClass="input150 GenInput" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                            <%--<div class="col-lg-6">
                                                <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBlockValue" ID="rfBlockValue" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBlockValue" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow numbers Only" ControlToValidate="txtBlockValue" ValidationExpression="^((\d+)((\.\d{1,2})?))$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>--%>
                                            <%--<div class="col-lg-6">
                                            <asp:RequiredFieldValidator ValidationGroup="vResults" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBlockComment" ID="rfBlockComment" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vResults" ID="rgBlockComment" runat="server" ForeColor="Red" Display="Dynamic" ErrorMessage="Allow Characters Only" ControlToValidate="txtBlockComment" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                            </div>--%>
                                        </div>

                                        <div class="row" style="margin-top: 15px; margin-left: 32px; margin-bottom: 5px;">
                                            <asp:CheckBox ID="chkNeedToRetest" Text="Need Re-Test?" runat="server" />
                                        </div>
                                        <div class="row">
                                            <div class="col=md-12">
                                                <%-- ValidationGroup="vResults"--%>
                                                <asp:Button ID="btnResults" CssClass="btn btn-primary" OnClick="btnResults_Click" Style="margin-left: 46px;" runat="server" Text="Save Results" />
                                                <asp:Button ID="btnResultBack" CssClass="btn btn-danger" Text="Cancel" runat="server" OnClick="btnResultBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dvAssay" runat="server" visible="false">
                        <div class="panel panel-default" style="margin-top: 8px;">
                            <div class="panel-heading">
                                <h4 data-toggle="collapse" data-parent="#accordion" href="#dvAssayDetails" class="panel-title expand">
                                    <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                    <a href="#" style="font-size: 14px; font-weight: 600;">Assay Details</a>
                                </h4>
                            </div>
                            <div id="dvAssayDetails" runat="server" clientidmode="static" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <h4 class="blue bigger">Assay Details
                                    <%--<a role="button" id="aAssayInformation" runat="server" class="red"><i class="fa fa-edit" style="float: right; margin-right: 6px;"></i></a>--%>
                                    </h4>
                                    <div>
                                        <div class="row rw2" id="AssayTypeEdit" runat="server">
                                            <div class="col-lg-12">
                                                <asp:DropDownList ID="ddlAssayType" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Repeater runat="server" ID="rptAssayDetails" OnItemDataBound="rptAssayDetails_ItemDataBound" OnItemCommand="rptAssayDetails_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="table">
                                                    <th>Assay Number
                                                    </th>
                                                    <th>Assay Name
                                                    </th>
                                                    <th>Assay Type
                                                    </th>
                                                    <th>Specimen Count
                                                    </th>
                                                    <th>Assay Status
                                                    </th>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnAssayNumber" runat="server" Text='<%# Eval("AssayBIN") %>' CommandArgument='<%#Eval("AssayID") %>' CommandName="AssayNo" />
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="ltrAssayName" runat="server" Text='<%# Eval("AssayName") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="ltrAssayType" runat="server" Text='<%# Eval("AssayType") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="ltrSpecimenCount" runat="server" Text='<%# Eval("SampleCount") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="ltrAssayStatus" runat="server" Text='<%# Eval("AssayStatus") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <div class="row" style="margin-left: 5px; margin-top: 10px;">
                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" AutoPostBack="true" OnClick="btnSave_Click" runat="server" Visible="false" Text="Add to Assay" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal fade" runat="server" id="dvAssayName" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <label style="color: blue; font-weight: bold">Enter The Assay Name</label>
                                                <button id="btnAssayNameClose" type="button" class="badge close" style="float: right; background-color: #e0e0dc; opacity: 1.5; font-size: 16px; width: 25px; height: 28px;" onclick="AssayNameHide()" data-dismiss="modal">x</button>
                                            </div>
                                            <div class="modal-body">
                                                <b>Assay Name: </b>
                                                <asp:TextBox ID="txtAssayName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfAssayName" runat="server" Display="Dynamic" ControlToValidate="txtAssayName" ValidationGroup="vgAssayName" ErrorMessage="Assay Name Required"></asp:RequiredFieldValidator>
                                                <br />
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnAssayName" class="btn btn-default" runat="server" Text="Save" ValidationGroup="vgAssayName" OnClick="btnAssayName_Click" />
                                                <button id="btnAssayNamelCancel" type="button" class="btn btn-danger btn-ok" onclick="AssayNameHide()" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default" style="margin-top: 8px;">
                        <div class="panel-heading">
                            <h4 data-toggle="collapse" data-parent="#accordion" href="#dvBillingDetails" class="panel-title expand">
                                <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                <a href="#" style="font-size: 14px; font-weight: 600;">Payment/Billing Info</a>
                            </h4>
                        </div>

                        <div id="dvBillingDetails" runat="server" clientidmode="static" class="panel-collapse collapse">
                            <div class="panel-body">
                                <h4 class="blue bigger">Payment Mode&nbsp;&nbsp;
                                    <a role="button" id="aPaymentInformation" runat="server" class="red" onserverclick="aPaymentInformation_ServerClick"><i class="fa fa-edit" style="float: right; margin-right: 6px;"></i></a></h4>
                                <div id="dvPaymentEdit" runat="server">
                                    <div class="row rw2" id="PaymentModeEdit" runat="server">
                                        <div class="col-lg-12">
                                            <asp:DropDownList ID="ddlPaymentMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                                <asp:ListItem Text="&nbsp;Cash" Value="Cash" />
                                                <asp:ListItem Text="&nbsp;Credit Card" Value="CreditCard" />
                                                <asp:ListItem Text="&nbsp;Cheque" Value="Cheque" />
                                                <asp:ListItem Text="&nbsp;Insurance" Value="Insurance" />
                                                <asp:ListItem Text="&nbsp;Free" Value="Free" />
                                                <asp:ListItem Text="&nbsp;Charge to physician office" Value="Charge to physician office" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:HiddenField ID="hPaymentID" Value="0" runat="server" />
                                        <asp:HiddenField ID="hPaymentMode" Value="" runat="server" />
                                        <div>
                                            <div id="dvCashDetails" runat="server">
                                                <h4 class="blue bigger">Cash Details</h4>
                                                <div id="dvcashEdit" visible="false" runat="server" style="padding: 10px 10px 10px 10px">
                                                    <div class="profile-user-info profile-user-info-striped">
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Cash<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value input-group">
                                                                <span class="editable input-group" id="spCashAmount">
                                                                    <asp:TextBox ID="txtCash" runat="server" class="input150 GenInput" placeholder="Amount #" />
                                                                    <asp:DropDownList ID="ddlCurrency" Style="height: 38px; width: 50px; border-left: 0px; border-color: #bbbbbb" runat="server">
                                                                        <asp:ListItem Text="$" Value="$"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                                <span id="spCash" style="display: none; color: red;">Cash is Required</span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Transaction Date<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtTransDate" runat="server" class="date-picker GenInput input200" Style="margin-top: 0px; margin-bottom: 0px;" placeholder="Date " />
                                                                    <%--<span class="input-group-addon">
                                                                    <i class="fa fa-calendar" id="faTransDate"></i>
                                                                </span>--%>
                                                                    <span id="spDate" style="display: none; color: red;">Date is Required</span>
                                                                </div>
                                                                <%--<asp:TextBox id="txtTransTime" runat="server" class="input90 GenInput" placeholder="Date " />--%>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="display: none">
                                                            <div class="profile-info-name">Receipt Number</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spCashReceipt">
                                                                    <asp:TextBox ID="txtCashReceiptNo" runat="server" class="input200 GenInput" placeholder="Cash receipt #" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Description</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spCashDescription">
                                                                    <asp:TextBox TextMode="MultiLine" Rows="2" cols="20" ID="txtCashDescription" runat="server" Style="height: 70px" class="input300 form-control limited GenDD" placeholder="Payment Notes"></asp:TextBox>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="dvCashView" runat="server" style="padding: 10px 10px 10px 10px">
                                                    <div class="profile-user-info profile-user-info-striped">
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Cash</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spCashViewAmount">
                                                                    <asp:Label ID="lblCash" runat="server"></asp:Label>&nbsp;
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Transaction Date</div>
                                                            <div class="profile-info-value">
                                                                <div class="input-group">
                                                                    <asp:Label ID="lblTransactionDate" runat="server"></asp:Label>&nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="display: none">
                                                            <div class="profile-info-name">Receipt Number</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spCashViewReceipt"></span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Description</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spCashViewDescription">
                                                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>&nbsp;
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="dvCreditCardDetails" visible="false" runat="server">
                                                <h4 class="blue bigger">Credit Card Details</h4>
                                                <div style="display: none; float: left; margin-left: 10px;">
                                                    <p>Capture Card Info</p>
                                                    <p>
                                                        <input class="ace ace-switch ace-switch-5" type="checkbox" checked="" /><span class="lbl"></span>
                                                    </p>
                                                    <p>
                                                        <asp:TextBox ID="txtTranRef" runat="server" class="input160 GenInput" placeholder="Trans Reference #" />
                                                    </p>
                                                    <p>
                                                        <asp:TextBox ID="TextBox6" runat="server" class="input210 GenInput" placeholder="Transaction Notes" />
                                                    </p>
                                                </div>
                                                <div id="dvCreditCardEdit" visible="false" runat="server" style="width: 505px; height: 260px; float: right;">
                                                    <div style="background-color: rgb(37,152,119); border-radius: 8px; float: right; margin-top: 20px; margin-right: 20px; width: 360px; height: 200px;">
                                                        <div style="background-color: #242424; height: 32px; margin-top: 35px;"></div>
                                                        <div style="float: right; margin-right: 40px"><b>CVV</b></div>
                                                        <div style="height: 30px; margin-top: 20px; margin-right: 20px; background-color: rgb(145, 202, 185);">
                                                            <div style="margin-top: 3px; margin-right: 5px; float: right;">
                                                                XXX&nbsp;<asp:TextBox ID="txtCVV" runat="server" class="input50" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="background-color: rgb(95,194,165); position: absolute; z-index: 500; left: 125px; border-radius: 8px; float: right; margin-top: 55px; margin-right: 20px; width: 360px; height: 235px;">
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 10px; font-size: 16px;"><b>CREDIT CARD</b></div>
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 2px; font-size: 12px;">Card Number</div>
                                                        <div style="margin-left: 20px; margin-top: 15px;">
                                                            <asp:TextBox ID="txtCardNo1" runat="server" class="input70" />&nbsp;&nbsp;&nbsp;                                                        
                                                        <asp:TextBox ID="txtCardNo2" runat="server" class="input70" />&nbsp;&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtCardNo3" runat="server" class="input70" />&nbsp;&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtCardNo4" runat="server" class="input70" />
                                                        </div>
                                                        <div style="margin-left: 95px; margin-top: 15px;">
                                                            <span>MONTH</span><span style="margin-left: 35px;">YEAR</span>
                                                            <span style="margin-left: 55px;">Card Type</span>
                                                        </div>
                                                        <div style="margin-left: 20px; margin-top: 5px;">
                                                            <span>VALID THRU</span>
                                                            <span>
                                                                <asp:DropDownList ID="ddlCreditDate" runat="server">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                                </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlCreditYear" runat="server">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                                                <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                                                <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                                <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                                <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                                <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                                                <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                                                <asp:ListItem Value="2026" Text="2026"></asp:ListItem>
                                                                <asp:ListItem Value="2027" Text="2027"></asp:ListItem>
                                                                <asp:ListItem Value="2028" Text="2028"></asp:ListItem>
                                                                <asp:ListItem Value="2030" Text="2029"></asp:ListItem>
                                                                <asp:ListItem Value="2031" Text="2031"></asp:ListItem>
                                                                <asp:ListItem Value="2032" Text="2032"></asp:ListItem>
                                                                <asp:ListItem Value="2033" Text="2033"></asp:ListItem>
                                                                <asp:ListItem Value="2034" Text="2034"></asp:ListItem>
                                                                <asp:ListItem Value="2035" Text="2035"></asp:ListItem>
                                                                <asp:ListItem Value="2036" Text="2036"></asp:ListItem>
                                                                <asp:ListItem Value="2037" Text="2037"></asp:ListItem>
                                                                <asp:ListItem Value="2038" Text="2038"></asp:ListItem>
                                                                <asp:ListItem Value="2039" Text="2039"></asp:ListItem>
                                                                <asp:ListItem Value="2040" Text="2040"></asp:ListItem>
                                                                <asp:ListItem Value="2041" Text="2042"></asp:ListItem>
                                                                <asp:ListItem Value="2043" Text="2043"></asp:ListItem>
                                                                <asp:ListItem Value="2044" Text="2044"></asp:ListItem>
                                                                <asp:ListItem Value="2045" Text="2045"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            </span>
                                                            <span style="margin-left: 15px;">
                                                                <asp:DropDownList ID="ddlCardType" runat="server" Width="30%">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="VISA" Value="VISA"></asp:ListItem>
                                                                    <asp:ListItem Text="Master card" Value="Master card"></asp:ListItem>
                                                                    <asp:ListItem Text="Amex" Value="Amex"></asp:ListItem>
                                                                    <asp:ListItem Text="Discover" Value="Discover"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </span>
                                                        </div>
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 2px; font-size: 12px;">Holder Name</div>
                                                        <div style="margin-left: 20px; margin-top: 15px;">
                                                            <asp:TextBox ID="txtHolderName" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal fade" runat="server" id="dvCreditCardDelete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <label style="color: red; font-weight: bold">Warning <i class="fa fa-check-circle" aria-hidden="true"></i></label>
                                                                <button id="btnCreditDeleteClose" type="button" class="badge close" style="float: right; background-color: #e0e0dc; opacity: 1.5; font-size: 16px; width: 25px; height: 28px;" onclick="creditHide()" data-dismiss="modal">x</button>
                                                            </div>
                                                            <div class="modal-body" id="modalBody">
                                                                <b>If you want to edit credit card details, previous data will delete.
                                                            <br />
                                                                    Are you sure want to edit ?</b>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnModalOK" class="btn btn-default" runat="server" Text="OK" OnClick="btnModalOK_Click" />
                                                                <button id="btnModalCancel" type="button" class="btn btn-danger btn-ok" onclick="creditHide()" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="dvCreditCardView" runat="server" style="width: 505px; height: 260px; float: right;">
                                                    <div style="background-color: #aaa; border-radius: 8px; float: right; margin-top: 20px; margin-right: 20px; width: 360px; height: 200px;">
                                                        <div style="background-color: #242424; height: 32px; margin-top: 35px;"></div>
                                                        <div style="float: right; margin-right: 40px"><b>CVV</b></div>
                                                        <div style="height: 30px; margin-top: 20px; margin-right: 20px; background-color: rgb(202, 206, 205);">
                                                            <div style="margin-top: 3px; margin-right: 5px; float: right;">
                                                                &nbsp;<asp:Label ID="lblCVV" runat="server" Text="XXX" class="input50" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="background-color: rgb(160, 165, 163); position: absolute; z-index: 500; left: 125px; border-radius: 8px; float: right; margin-top: 55px; margin-right: 20px; width: 360px; height: 180px;">
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 10px; font-size: 16px;">
                                                            <b>CREDIT CARD</b>
                                                        </div>
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 2px; font-size: 12px;">Card Number</div>
                                                        <div style="margin-left: 65px;">
                                                            <asp:Label ID="lblCardNo1" Style="font-size: 24px; color: floralwhite; font-family: initial" runat="server">xxxx</asp:Label>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCardNo2" Style="font-size: 24px; color: floralwhite; font-family: initial" runat="server">xxxx</asp:Label>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCardNo3" Style="font-size: 24px; color: floralwhite; font-family: initial" runat="server">xxxx</asp:Label>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblCardNo4" Style="font-size: 24px; color: floralwhite; font-family: initial" runat="server">xxxx</asp:Label>
                                                            <a id="aCreditCardSecure" style="color: #242424; font-size: 22px; margin-left: 13px;" runat="server" onserverclick="aCreditCardSecure_ServerClick"><i class="fa fa-eye" aria-hidden="true"></i></a>
                                                        </div>
                                                        <%--<div style="margin-left: 95px; margin-top: 15px;">
                                                        <span>MONTH</span><span style="margin-left: 35px;">YEAR</span>
                                                        <span style="margin-left: 55px;">Card Type</span>
                                                    </div>--%>
                                                        <div style="margin-left: 20px; margin-top: 5px;">
                                                            <span style="color: floralwhite;">VALID THRU</span>
                                                            <span>
                                                                <asp:Label ID="lblExpireDate" Style="color: floralwhite; margin-left: 15px" runat="server">MM/YY</asp:Label>
                                                            </span>
                                                            <span style="margin-left: 70px;">
                                                                <asp:Label ID="lblCardType" Style="color: floralwhite;" runat="server">Card Type</asp:Label>
                                                            </span>
                                                        </div>
                                                        <div style="color: #ffffff; margin-left: 20px; margin-top: 10px; font-size: 12px;">CardHolder</div>
                                                        <div style="margin-left: 50px;">
                                                            <asp:Label ID="lblHolderName" Style="color: floralwhite;" runat="server">Holder Name</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal fade" runat="server" id="dvCreditCardSecure" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <label style="color: red; font-weight: bold">Credit Card details</label>
                                                                <button id="btnCreditSecClose" type="button" class="badge close" style="float: right; background-color: #e0e0dc; opacity: 1.5; font-size: 16px; width: 25px; height: 28px;" onclick="creditSecHide()" data-dismiss="modal">x</button>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row" style="margin-left: 10px">
                                                                    <label style="font-weight: 700">Card Number :</label>
                                                                    <asp:Label ID="lblCardNoSec" runat="server"></asp:Label><br />
                                                                    <label style="margin-left: 15px; font-weight: 700">Valid Thiru :</label>
                                                                    <asp:Label ID="lblExpireDateSec" runat="server"></asp:Label><br />
                                                                    <label style="margin-left: 19px; font-weight: 700">Card Type :</label>
                                                                    <asp:Label ID="lblCardTypeSec" runat="server"></asp:Label><br />
                                                                    <label style="margin-left: 6px; font-weight: 700">Cvv Number :</label>
                                                                    <asp:Label ID="lblCvvSec" runat="server"></asp:Label><br />
                                                                    <label style="margin-left: 7px; font-weight: 700">Card Holder :</label>
                                                                    <asp:Label ID="lblCardHolderSec" runat="server"></asp:Label><br />
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button id="btnCreditCancel" type="button" class="btn btn-danger btn-ok" onclick="creditSecHide()" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="clear: both;"></div>
                                            </div>

                                            <div id="dvChequeDetails" visible="false" runat="server">
                                                <h4 class="blue bigger">Cheque Details</h4>
                                                <div id="dvChequeEdit" visible="false" runat="server" style="background: url(/images/chequeBg.png) no-repeat center; height: 250px; width: 100%;">
                                                    <div style="margin-top: 27px; margin-left: 105px; float: left">
                                                        <asp:TextBox ID="txtBankName" runat="server" Style="height: 21px;" class="input240" placeholder="Bank Name" />
                                                    </div>
                                                    <div style="margin-top: 27px; margin-right: 100px; float: right;">
                                                        <asp:TextBox name="txtCheqNo" type="text" ID="txtCheqNo" runat="server" Style="height: 21px;" class="input100" placeholder="Cheque No #" />
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                    <div style="margin-top: 5px; margin-left: 105px">
                                                        <asp:TextBox ID="txtBranchName" runat="server" Style="height: 21px;" class="input240" placeholder="Branch Name" />
                                                        <asp:TextBox ID="txtCheqDate" runat="server" Style="height: 21px; margin-left: 63px;" class="input140" placeholder="Cheque Date" />
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                    <div style="margin-top: 130px; margin-left: 106px;">
                                                        <span>
                                                            <asp:TextBox ID="txtRoutNo" runat="server" Style="height: 26px;" class="input130" placeholder="Routing No #" />
                                                        </span>
                                                        <span style="margin-left: 20px;">
                                                            <asp:TextBox ID="txtAcctNo" runat="server" Style="height: 26px;" class="input130" placeholder="Account No #" />
                                                        </span>
                                                    </div>
                                                </div>

                                                <div id="dvChequeView" runat="server" style="background: url(/images/chequeBg.png) no-repeat center; height: 250px; width: 100%;">
                                                    <div style="margin: 30px 0 0 130px; float: left;">
                                                        <asp:Label ID="lblBankName" Style="font-weight: 600;" runat="server">Bank Name</asp:Label>
                                                    </div>
                                                    <div style="background-color: floralwhite; margin: 25px 110px 0 0; float: right; height: 25px; padding-left: 7px; font-weight: 600; font-size: 18px; width: 12%;">
                                                        <asp:Label ID="lblCheqNo" runat="server">Cheq No#</asp:Label>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                    <div style="margin: 5px 0 0 130px">
                                                        <asp:Label ID="lblBranchName" Style="font-weight: 600;" runat="server">Branch Name</asp:Label>
                                                        <asp:Label ID="lblCheqDate" Style="font-weight: 600; margin-left: 43%;" runat="server">Cheque Date</asp:Label>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                    <div style="margin: 132px 0 0 123px">
                                                        <div class="row">
                                                            <div style="background-color: floralwhite; padding-left: 20px; float: left; font-weight: 600; font-size: 18px; width: 20%;">
                                                                <span>
                                                                    <asp:Label ID="lblRoutNo" runat="server">Routing No#</asp:Label>
                                                                </span>
                                                            </div>
                                                            <div style="background-color: floralwhite; margin-left: 153px; padding-left: 20px; font-weight: 600; font-size: 18px; width: 21%;">
                                                                <span style="">
                                                                    <asp:Label ID="lblAcctNo" runat="server">Account No#</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="dvInsuranceDetails" runat="server" visible="false" >
                                                <h4 class="blue bigger">Insurance Details</h4>

                                                <div class="row ">
                                                    <div id="dvInsuranceEdit" visible="false" runat="server" style="padding: 10px 10px 10px 10px">
                                                    <div class="col-lg-6">
                                                         
                                                    <div class="profile-user-info profile-user-info-striped">
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Insurance Type<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value input-group">
                                                                <span class="editable input-group" id="spInsuranceType">
                                                                    <asp:TextBox ID="txtInsuranceType" runat="server" class="GenInput input200" placeholder="Insurance Type" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Insurance Company<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spInsuranceCompany">
                                                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" class="GenInput input200" placeholder="Insurance Company" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Insurance Number<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spInsuranceNumber">
                                                                    <asp:TextBox ID="txtInsuranceNumber" runat="server" class="GenInput input200" Style="margin-top: 0px; margin-bottom: 0px;" placeholder="Insurance Number " />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Member Name<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spMemberName">
                                                                    <asp:TextBox ID="txtMemberName" runat="server" class="GenInput input200" placeholder="Member Name" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">MemberShip Number<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spMemberShipNumber">
                                                                    <asp:TextBox ID="txtMemberShipNumber" runat="server" class="GenInput input200" placeholder="MemberShip Number"></asp:TextBox>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">Group Number<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spGroupNumber">
                                                                    <asp:TextBox ID="txtGroupNumber" runat="server" class="GenInput input200" placeholder="Group Number"></asp:TextBox>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row">
                                                            <div class="profile-info-name">PreAuth Code<b><i style="color: red; font-size: 18px;">*</i></b></div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spPreAuthCode">
                                                                    <asp:TextBox ID="txtPreAuthCode" runat="server" class="GenInput input200" placeholder="PreAuthCode"></asp:TextBox>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                                                                      
                                                    <div class="col-lg-6">
                                                       <div id="dvPreAuthorization">
                                                            <h4 style="color:#6a9fc3;font-size:18px;">Pre-Authorization Insurance Details</h4>

                                                       Is Pre-Authorization Form Required &nbsp; &nbsp; <asp:CheckBox  runat="server" ID="chkPreAuthorization" OnCheckedChanged="chkPreAuthorization_CheckedChanged" AutoPostBack="true" Style="margin-top:10px;" />

                                                           &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <br /><asp:Label  runat="server" ID="lblPreInsurance" Style="color:red; font-size:15px;margin-top:20px;"  />
                                                       </div>

                                                       
                                                    </div>
                                                        </div>

                                                    <asp:HiddenField id="hPreInsurance" Value="0" runat="server" />

                                                </div>
                                               

                                                <div id="dvInsuranceView" runat="server" style="padding: 10px 10px 10px 10px">
                                                    <div class="profile-user-info profile-user-info-striped">
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">Insurance Type</div>
                                                            <div class="profile-info-value input-group">
                                                                <span class="editable input-group" id="spViewInsuranceType">
                                                                    <asp:Label ID="lblInsuranceType" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">Insurance Company</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewInsuranceCompany">
                                                                    <asp:Label ID="lblInsuranceCompany" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">Insurance Number</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewInsuranceNumber">
                                                                    <asp:Label ID="lblInsuranceNumber" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">Member Name</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewMemberName">
                                                                    <asp:Label ID="lblMemberName" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">MemberShip Number</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewMemberShipNumber">
                                                                    <asp:Label ID="lblMemberShipNumber" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">Group Number</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewGroupNumber">
                                                                    <asp:Label ID="lblGroupNumber" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="profile-info-row" style="height: 50px">
                                                            <div class="profile-info-name">PreAuth Code</div>
                                                            <div class="profile-info-value">
                                                                <span class="editable" id="spViewPreAuthCode">
                                                                    <asp:Label ID="lblPreAuthCode" runat="server">&nbsp;</asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="dvFreeDetails" runat="server" visible="false">
                                                <h4 class="blue bigger">Free Details</h4>
                                                Yet to Complete Free Details
                                            </div>

                                            <div id="dvChargeToPhysicianDetails" runat="server" visible="false">
                                                <h4 class="blue bigger">Charge to physician office Details</h4>
                                                Yet to Complete Charge to Physician office Details
                                            </div>

                                            <div id="dvUpdate" runat="server" class="row">
                                                <div style="margin-left: 63%;">
                                                    <asp:DropDownList ID="ddlPaymentStaus" Style="height: 35px" runat="server">
                                                        <asp:ListItem Text="Yet to initiate" Value="Yet to initiate"></asp:ListItem>
                                                        <asp:ListItem Text="Paid" Value="Paid"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnUpdatePayment" CssClass="btn btn-success" Style="margin-bottom: 4px; margin-left: 2%;" OnClick="btnUpdatePayment_Click" Text="Update" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div id="dvBillEdit" runat="server">
                                    <h4 class="blue bigger">Billing Address</h4>
                                    <div class="row rw2">
                                        <div class="col-lg-12">
                                            Billing address as same as : 
                                                    <asp:DropDownList ID="ddlBillAddress" AutoPostBack="true" OnSelectedIndexChanged="ddlBillAddress_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Text="Choose Address" Value="-1" disabled="true"></asp:ListItem>
                                                        <asp:ListItem Text="Patient Address" Value="Patient Address"></asp:ListItem>
                                                        <asp:ListItem Text="Guardian Address" Value="Guardian Address"></asp:ListItem>
                                                        <asp:ListItem Text="Physician Address" Value="Physician Address"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                            <asp:Label ID="lblBillInfo" Style="margin-left: 50px" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hOrderID" Value="0" runat="server" />
                                    <asp:HiddenField ID="hBillAddSameAs" Value="" runat="server" />
                                    <div class="well" id="dvBillAddEdit" runat="server" visible="false">
                                        <h5 class="blue bigger" style="margin-top: 0px">Address</h5>
                                        <div class="row">
                                            <div class="col-lg-12" style="margin-left: 8%">
                                                Street :
                                                        <asp:TextBox ID="txtBillStr" runat="server" class="input150 GenInput" placeholder="Street"></asp:TextBox>
                                                <span style="margin-left: 35%">City :
                                                            <asp:TextBox ID="txtBillCity" runat="server" class="input150 GenInput" placeholder="City"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfBillStr" runat="server" Display="Dynamic" ValidationGroup="vgBill" ControlToValidate="txtBillStr" ForeColor="Red" ErrorMessage="Street is Required"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfBillCity" runat="server" Display="Dynamic" ValidationGroup="vgBill" ControlToValidate="txtBillCity" ForeColor="Red" ErrorMessage="City is Required" Style="margin-left: 48%"></asp:RequiredFieldValidator>
                                                </span>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="margin-left: 63px">
                                                State :
                                                        <asp:TextBox ID="txtBillState" runat="server" class="input150 GenInput" placeholder="State"></asp:TextBox>
                                                <span style="margin-left: 220px">Country :
                                                            <asp:TextBox ID="txtBillCountry" runat="server" class="input150 GenInput" placeholder="Country"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfBillState" runat="server" Display="Dynamic" ValidationGroup="vgBill" ControlToValidate="txtBillState" ForeColor="Red" ErrorMessage="State is Required"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfBillCountry" runat="server" Display="Dynamic" ValidationGroup="vgBill" ControlToValidate="txtBillCountry" ForeColor="Red" ErrorMessage="Country is Required" Style="margin-left: 48%"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="margin-left: 41px">
                                                Zip Code :
                                                        <asp:TextBox ID="txtBillZip" runat="server" class="input150 GenInput" placeholder="Zip Code"></asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfBillZip" runat="server" Display="Dynamic" ValidationGroup="vgBill" ControlToValidate="txtBillZip" ForeColor="Red" ErrorMessage="ZipCode is Required"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Button ID="btnUpdateAdd" runat="server" CssClass="btn btn-primary" Text="Billing Updates" ValidationGroup="vgBill" Visible="false" OnClick="btnUpdateAdd_Click" Style="float: right" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default" style="margin-top: 8px;">
                        <div class="panel-heading">
                            <h4 data-toggle="collapse" data-parent="#accordion" href="#dvDocument" class="panel-title expand">
                                <span class="right-arrow pull-right" style="font-weight: bold;">+</span>
                                <a href="#" style="font-size: 14px; font-weight: 600;">Documents</a>
                            </h4>
                        </div>
                        <div id="dvDocument" runat="server" clientidmode="static" class="panel-collapse collapse">
                            <div class="panel-body">
                                <%--<h4 class="blue bigger">Requested Form Copy</h4>--%>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label id="lblReqformview" class="blue bigger" runat="server" style="font-weight: 500; font-size: 17px;">Request Form</label>

                                        <div id="dvViewFile" runat="server">
                                            <a id="ancViewCopy" runat="server"></a>&nbsp;
                                        <asp:LinkButton ID="btnFileClose" Text="x" CssClass="badge close" Style="background-color: #e0e0dc; opacity: 1.5; font-size: 16px; position: absolute;" runat="server" OnClick="btnFileClose_Click"></asp:LinkButton>
                                        </div>

                                        <div id="dvUploadFile" runat="server" visible="false">

                                            <input id="inSampleHardCopy" class="form-control" style="display: inline; width: 70%; margin-left: 5px" placeholder="Choose File" disabled="disabled" />
                                            <%-- <asp:RequiredFieldValidator ID="rfinSampleHardCopy" runat="server" ControlToValidate="inSampleHardCopy" ForeColor="Red" ErrorMessage="Please Choose File"></asp:RequiredFieldValidator>--%>
                                            <br />
                                            <asp:Button CssClass="btn btn-danger" runat="server" ID="btnDocumentBack" Style="margin-left: 5px" Text="Cancel" OnClick="btnDocumentBack_Click" />
                                            <div class="fileUpload btn btn-primary" style="border: none">
                                                <span>Choose</span>
                                                <asp:FileUpload ID="flSampleHardCopy" runat="server" CssClass="upload" />
                                                <asp:HiddenField ID="fileUploadHandler" runat="server" Value="" />
                                            </div>
                                            &nbsp;&nbsp;
                                        <asp:Button ID="btnupload" CssClass="btn btn-success" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                                        </div>

                                    </div>

                                    <div class="col-md-6" id="dvResultsPre" runat="server">
                                        <h4 class="blue bigger">Results</h4>
                                        <a id="aResultsPreview" runat="server" onserverclick="aResultsPreview_ServerClick">View Results</a><br />
                                        <br />
                                        <a style="padding: 10px 10px 10px 0px" visible="false" id="aPreview" runat="server">Preview</a>
                                        <asp:LinkButton Visible="false" Text="Download" ID="lbtnDownload" OnClick="lbtnDownload_Click" runat="server" />
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="alert-danger" id="dvalertdanger" runat="server">
                <div style="margin: 15px; padding: 5px;">
                    <h4><b>Verification</b></h4>

                    <div class="row">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-8">
                            <p style="margin-top: 20px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkConsentProvided" runat="server" /><span class="lbl"></span>&nbsp;
                                     <label for="ContentPlaceHolder1_chkConsentProvided">Consent Provided?</label>
                            </p>

                            <p style="margin-top: 5px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkAcceptTest" runat="server" /><span class="lbl"></span>&nbsp;
                                <label for="ContentPlaceHolder1_chkAcceptTest">Specimen acceptable for testing?</label>
                            </p>

                            <p style="margin-top: 10px;">
                                <input class="ace ace-switch ace-switch-5" type="checkbox" id="chkRejection" runat="server" /><span class="lbl"></span>&nbsp;
                                     <label for="ContentPlaceHolder1_chkRejection">Rejection? </label>
                            </p>


                            <div style="margin: 0px; padding: 0px;">
                                <div class="row" style="margin-bottom: 0px;">
                                    <div style="margin-left: 35px; width: 75%; margin-top: 0px; display: none; margin-bottom: 5px;" id="dvRejectReason">
                                        <asp:ListBox ID="ddlRejectReason" SelectionMode="Multiple" multiple="" Style="width: 100%;" CssClass="select2 " data-placeholder="Reason for rejection..." runat="server">
                                            <asp:ListItem Text="QNS" Value="QNS"></asp:ListItem>
                                            <asp:ListItem Text="Spill" Value="Spill"></asp:ListItem>
                                            <asp:ListItem Text="Wrong Specimen Type" Value="Wrong Specimen Type"></asp:ListItem>
                                            <asp:ListItem Text="Hemolyzed" Value="Hemolyzed"></asp:ListItem>
                                            <asp:ListItem Text="Others"></asp:ListItem>
                                        </asp:ListBox>

                                        <asp:TextBox ID="txtOtherRejectReason" runat="server" CssClass="GenInput input240" Style="display: none;"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="reqOtherRejectReason" ControlToValidate="txtOtherRejectReason" Style="color:red" ErrorMessage="Required Reason"></asp:RequiredFieldValidator>--%>

                                        <span id="spOtherRejectReason" style="color: red; display: none">Required Reject Reason </span>
                                    </div>
                                </div>

                            </div>
                            <div style="margin-left: 0px; padding-left: 0px;">
                                <p>
                                    <input class="ace ace-switch ace-switch-5 maxtickets_enable_cb" type="checkbox" id="chkOthers" runat="server" /><span class="lbl"></span>&nbsp;
                                    <label for="ContentPlaceHolder1_chkOthers" style="font-weight: 400;">Other Exception </label>
                                    <br />
                                    <span class="max_tickets" id="spnothers" style="display: none; margin-left: 30px; margin-bottom: 10px;">
                                        <asp:TextBox runat="server" ID="txtPendingOther" CssClass="GenInput" AutoPostBack="true" placeholder="Exception" Style="width: 75%; height: 40px; border-radius: 5px;" />
                                    </span>
                                </p>

                            </div>
                            <span id="spExceptions" style="color: red; display: none;">Required Exception </span>

                        </div>
                        <div class="row">
                            <div class="col-lg-11">
                                <asp:Button ID="btnSVVerificationInfo" CssClass="btn btn-primary" Style="float: right;" OnClick="btnSVVerificationInfo_Click" runat="server" Text="Update" />

                            </div>
                        </div>

                        <div id="dvPendingtoRec" runat="server" style="border-top: 1px solid; margin-left: 15px; margin-bottom: 10px;">
                            <br />
                            <span style="font-weight: 600;">Reason for Reactivate :<i style="color: red; font-size: 18px;">*</i></span>
                            <asp:TextBox runat="server" ID="txtPendingtoRec" CssClass="GenInput input250" placeholder="Reason for Reactivate..." Style="width: 100%; height: 40px; border-radius: 5px;" />
                            <asp:RequiredFieldValidator ID="reqPendingtoRec" runat="server" ValidationGroup="vgPendingtoRec" ControlToValidate="txtPendingtoRec" ForeColor="Red" ErrorMessage="Reason for Reactivate"></asp:RequiredFieldValidator>

                        </div>

                        <div>
                            <span id="spReasonUpdate" style="color: red;"></span>
                            <br />
                            <asp:LinkButton ID="lbtnReasonUpdate" Text=" Reactivate" CssClass="col-md-offset-3" ValidationGroup="vgPendingtoRec" runat="server" OnClick="lbtnReasonUpdate_Click" />
                        </div>

                    </div>
                </div>
            </div>

            <div class="alert alert-warning" runat="server" id="dvAlertWarning" style="margin-left: 0;">
                <i class="fa fa-exclamation-circle" style="position: absolute; left: 5px; top: 7px; width: 35px; height: 35px; -webkit-border-radius: 50%; -moz-border-radius: 50%; border-radius: 50%; line-height: 35px; text-align: center; background: inherit; border: inherit;" aria-hidden="true"></i>

                <%-- <span class="glyphicon glyphicon-exclamation-sign"></span>--%>
                <strong style="margin-left: 15px;">Warning</strong><br />
                <br />
                <asp:Literal ID="ltrWarning" runat="server"></asp:Literal>
            </div>
        </div>

        <div class="col-md-3 alert alert-warning" style="width: 30%; float: right; margin: 0 3px 0 0; min-height: 896px;">
        <div class="row">
            <label style="font-weight: 600; color: #63615e;">Get Specimen Info</label>
        </div>
        <div class="row">            
            <asp:TextBox ID="txtSpecimenNum" runat="server" Style="height: 38px; width: 200px; text-align: center" placeholder="Enter Specimen Number" ValidationGroup="vdgSpecimenNum"></asp:TextBox>
            <asp:Button ID="btnSpecimenNum" CssClass="btn btn-success" runat="server" Style="margin: 0 0 4px 5px; height: 38px;" Text="View" OnClick="btnSpecimenNum_Click" ValidationGroup="vdgSpecimenNum" />
            <asp:RequiredFieldValidator ValidationGroup="vdgSpecimenNum" Display="Dynamic" ID="reqSpecimenNum" ControlToValidate="txtSpecimenNum" runat="server" ForeColor="Red" ErrorMessage="Please enter Specimen Number!"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblSpecimenNum" runat="server" Style="font-size: 12px; color: red;"></asp:Label>
        </div>




        <div class="row">
            <h4 style="width: 96%;padding: 8px 8px 7px 8px;margin-top: 25px; color:white; background: #f56954;">Recent Activities</h4> 
                  
            <asp:Repeater ID="rpSpecimenAuditlog" runat="server" OnItemDataBound="rpSpecimenAuditlog_ItemDataBound">
                <HeaderTemplate>

                </HeaderTemplate>
               
                <ItemTemplate >
                   
                     <div class="timeline">
                         <div class="col-lg-1" style="top: 27px;" >
                                <asp:Literal ID="ltricon" runat="server"></asp:Literal>
                                    </div>
                          <div class="container right">
                                <div class="contents" >
                                    <div class="col-lg-6"  style="padding:0px;font-size:13px;color:black;">
                                         <asp:Literal ID="ltrActionName"  runat="server"></asp:Literal>
                                     </div>
                                     <div class="col-lg-5" style="padding:0px;font-size:11px; float: right;margin-left: 0px;color:#888888;" >
                                    <asp:Literal ID="ltrActionByon" runat="server"></asp:Literal>

                                     </div>
                                 </div></div>
                   <%-- <div class="row rowDetail rowIndGreen">
                    <div class="col-lg-7" style="padding:0px;font-size:13px;color:black">
                       

                    </div>
                    <div class="col-lg-5" style="padding:0px;font-size:11px;margin-left: 0px;color:#888888">
                        

                    </div>
                        </div>--%>
                         </div>
                </ItemTemplate>
               

            </asp:Repeater>

        </div>

        <asp:HiddenField ID="hAssayID" runat="server" />
        <asp:HiddenField ID="hSpecimenStatus" runat="server" />
        <asp:HiddenField ID="hAssaySpecimenID" runat="server" />
        <asp:HiddenField ID="hDocID" Value="0" runat="server" />
        <asp:HiddenField ID="hResultDocID" Value="0" runat="server" />
        <asp:HiddenField ID="hDocNumber" Value="0" runat="server" />
        <asp:HiddenField ID="hErrormessage" runat="server" />
        <asp:HiddenField ID="hEmailToken" Value="0" runat="server" />
    </div>
    </div>

    <script type="text/javascript">

        jQuery(document).ready(function ($) {
            $('input.maxtickets_enable_cb').change(function () {
                if ($(this).is(':checked')) $('span.max_tickets').show();
                else $('div.max_tickets').hide();
            }).change();

            //$("#ContentPlaceHolder1_lbtnReasonUpdate").click(function () {
            //    if (
            //        $('#ContentPlaceHolder1_chkConsentProvided').is(":not(:checked)")) {
            //        $('#spReasonUpdate').text("Please select consent is provided");
            //        return false;
            //    }

            //    else if ($('#ContentPlaceHolder1_chkRejection').is(":not(:checked)")) {
            //        $('#spReasonUpdate').text("Please select  requisition is complete");
            //        return false;
            //    }
            //    else if ($('#ContentPlaceHolder1_chkOthers').is(":checked")) {
            //        $('#spReasonUpdate').text("No other exceptions should not be selected ");
            //        return false;
            //    }
            //    else if ($('#ContentPlaceHolder1_chkAcceptTest').is(":not(:checked)")) {
            //        $('#spReasonUpdate').text("Please select specimen acceptable for testing");
            //        return false;
            //    }
            //    else {
            //        $('#spReasonUpdate').text("");
            //    }

            //});
        });


          $(function () {
            $("#ContentPlaceHolder1_chkOthers").click(function () {
                if ($(this).is(":checked")) {
                    $("#spnothers").show();

                } else {
                    $("#spnothers").hide();
                }
            });
        });

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



        $('#ContentPlaceHolder1_btnSVVerificationInfo').click(function () {

            if ($('#ContentPlaceHolder1_chkRejection').is(":checked")) {
                if ($('#ContentPlaceHolder1_ddlRejectReason').val() == "") {
                    $("#spOtherRejectReason").show();
                    $("#ContentPlaceHolder1_ddlRejectReason").focus();
                    return false;
                }

                else {
                    $("#spOtherRejectReason").hide();
                }
            }

            if ($('#ContentPlaceHolder1_chkOthers').is(":checked")) {
                if ($('#ContentPlaceHolder1_txtPendingOther').val() == "") {
                    $("#spExceptions").show();
                    $("#ContentPlaceHolder1_txtPendingOther").focus();
                    return false;
                }
                else {
                    $("#spExceptions").hide();
                }
            }
        });

        $(document).ready(function () {

            if ($('#ContentPlaceHolder1_chkClosePatient').is(":checked")) {
                document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "Patient Code";
                $("#dvClosePatient").show();
                $("#dvLastName").hide();
                $("#dvGender").hide();
                $("#dvDateOfBirth").hide();
                $("#ContentPlaceHolder1_bFirstNameCode").text("Patient Code");
            }
            else {
                document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "First Name(mandatory)";
                $("#dvLastName").show();
                $("#dvGender").show();
                $("#dvDateOfBirth").show();
                $("#ContentPlaceHolder1_bFirstNameCode").text("Patient Name");
            }

        });

        $(function () {
            $("#ContentPlaceHolder1_chkClosePatient").click(function () {
                if ($(this).is(":checked")) {
                    document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "Patient Code(mandatory)";
                    $("#dvLastName").hide();
                    $("#dvGender").hide();
                    $("#dvDateOfBirth").hide();
                    $("#dvClosePatient").show();
                    $("#ContentPlaceHolder1_bFirstNameCode").text("Patient Code");
                }
                else {
                    document.getElementById("ContentPlaceHolder1_txtFirstName").placeholder = "First Name(mandatory)";
                    $("#dvClosePatient").show();
                    $("#dvLastName").show();
                    $("#dvGender").show();
                    $("#dvDateOfBirth").show();
                    $("#ContentPlaceHolder1_bFirstNameCode").text("Patient Name");
                }
            });
        });

      

        $(function () {
            $("#ContentPlaceHolder1_btnUpdatePayment").click(function () {
                if ($("#ContentPlaceHolder1_txtCash").val() == "") {
                    $("#spCash").show();
                    return false;
                }
                else {
                    $("#spCash").hide();
                }

                if ($("#ContentPlaceHolder1_txtTransDate").val() == "") {
                    $("#spDate").show();
                    return false;
                }
                else {
                    $("#spDate").hide();
                }
            });
        });



        function Validate() {
            var isValid = false;
            isValid = Page_ClientValidate('vPatDetails1');
            if (isValid) {
                isValid = Page_ClientValidate('vPatDetails');
            }
            return isValid;
        }

        $('#ContentPlaceHolder1_chkSameAddress').on('click', function () {
            if ($(this).is(":checked")) {
                if ($('#ContentPlaceHolder1_txtstreet').val() == "") {
                    Page_ClientValidate('vPatDetails1');
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtCity').val() == "") {
                    Page_ClientValidate('vPatDetails1');
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtState').val() == "") {
                    Page_ClientValidate('vPatDetails1');
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtZip').val() == "") {
                    Page_ClientValidate('vPatDetails1');
                    return false;
                }
                if ($('#ContentPlaceHolder1_txtCountry').val() == "") {
                    Page_ClientValidate('vPatDetails1');
                    return false;
                }
            }
            else {

            }
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

       <%-- $('#ContentPlaceHolder1_chkAcceptTest').on('click', function () {
            if ($(this).is(":checked")) {
                $('#dvRejectReason').attr('hidden','false');
                $("#" + "<%=ddlRejectReason.ClientID%>").val(null).trigger("change");
                var txtData = $('#<%= txtOtherRejectReason.ClientID %>');
                txtData.val("");
                txtData.hide();
            }
            else {
                $('#dvRejectReason').removeAttr('hidden');
            }
        });--%>

        $(function () {
            $(".expand").on("click", function () {
                // $(this).next().slideToggle(200);
                $expand = $(this).find(">:first-child");

                if ($expand.text() == "+") {
                    $expand.text("-");
                } else {
                    $expand.text("+");
                }
            });
        });

        $("#ancExpandAll").on("click", function () {
            if ($("#ancExpandAll").text() == "Expand All") {
                $("#dvPatientDetails").removeAttr("style");
                $("#dvPhysicianDetails").removeAttr("style");
                $("#dvTestSpecDetails").removeAttr("style");
                $("#dvBillingDetails").removeAttr("style");
                $("#dvDocument").removeAttr("style");
                $("#dvPatientDetails").addClass("in").focus();
                $("#dvPhysicianDetails").addClass("in");
                $("#dvTestSpecDetails").addClass("in");
                $("#dvAssayDetails").addClass("in");
                $("#dvBillingDetails").addClass("in");
                $("#dvDocument").addClass("in");
                $("#ancExpandAll").text("Collapse All");
            }
            else if ($("#ancExpandAll").text() == "Collapse All") {
                $("#dvPatientDetails").removeClass("in").focus();
                $("#dvPhysicianDetails").removeClass("in");
                $("#dvTestSpecDetails").removeClass("in");
                $("#dvAssayDetails").removeClass("in");
                $("#dvBillingDetails").removeClass("in");
                $("#dvDocument").removeClass("in");
                $("#ancExpandAll").text("Expand All");
            }

            $expand = $(".expand").find(">:first-child");
            for (var i = 0; i < $expand.length; i++) {
                console.log($expand[i].innerText)
                if ($expand[i].innerText == "+") {
                    $expand[i].innerText = "-";
                } else {
                    $expand[i].innerText = "+";
                }
            }
        });

        $(document).ready(function () {

            //$(".chosen-select").chosen()
            //$('#<%= txtOtherRejectReason.ClientID %>').hide();
            if ($('#ContentPlaceHolder1_chkAcceptTest').is(":checked")) {
                $('#dvRejectReason').attr('hidden', 'true');

            } else {
                $('#dvRejectReason').removeAttr('hidden');
            }

            $("#" + "<%=ddlRejectReason.ClientID%>").select2
                ({
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

            if ($('#ContentPlaceHolder1_chkAcceptTest').is(":checked")) {
                $('#dvRejectReason').attr('hidden', 'true');

            } else {
                $('#dvRejectReason').removeAttr('hidden');
            }

            if ($('#ContentPlaceHolder1_chkRejection').is(":checked")) {

                $('#dvRejectReason').removeAttr('hidden');
                $('#dvRejectReason').show();
                $('#ContentPlaceHolder1_ddlRejectReason').attr('disabled', 'disabled');
                var selectedData = ($("#" + "<%=ddlRejectReason.ClientID%>").select2("val"));
                console.log(selectedData)
                var txtData = $('#<%= txtOtherRejectReason.ClientID %>');
               //$('select').select2();
               // txtData.val("");
               if ($.inArray("Others", selectedData) > -1) {
                   txtData.show();
               }
               else {
                   txtData.hide();
                   $('#spRejectReason').hide();
               }

            } else {
                $('#dvRejectReason').attr('hidden', 'true');
            }
        });


        $("#<%= flSampleHardCopy.ClientID%>").change(function () {
            $("#ContentPlaceHolder1_btnUpload").show();
            var fileName = $(this).val();
            var extention = (fileName.substr(fileName.lastIndexOf('.') + 1)).toLowerCase();
            if (extention == 'jpg' || extention == 'png' || extention == 'jpeg' || extention == 'bmp' || extention == 'pdf') {

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

                if ($("#ContentPlaceHolder1_inSampleHardCopy").val == "") {
                    alert("Name Field can not be blank");
                }
        });

       

        //$('.form_datetime').datetimepicker({
        //    //language:  'fr',
        //    weekStart: 1,
        //    todayBtn: 1,
        //    autoclose: 1,
        //    todayHighlight: 1,
        //    startView: 2,
        //    forceParse: 0,
        //    showMeridian: 1,
        //    maxDate: new Date()
        //});
        $(function () {

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

                $('#ContentPlaceHolder1_txtReceivedOn').datepicker('show');
            });
            $('#ContentPlaceHolder1_txtReceivedTime').timepicker();

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

            $("#ContentPlaceHolder1_txtCheqDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+2",
                autoclose: true,
                maxDate: new Date()
            });

           

            $("body").delegate("#<%= txtDrawnTime.ClientID%>", "focusin", function () {
                //$(this).datepicker();
                $(".ui-timepicker-container").css("z-index", "99999 !important");
                console.log(this)
            });
        });

        function creditHide() {
            $('#ContentPlaceHolder1_dvCreditCardDelete').removeClass("in");
            $('#ContentPlaceHolder1_dvCreditCardDelete').attr("style", "display:none");
            $('#ContentPlaceHolder1_dvCreditCardDelete').hide();
        }

        function AssayNameHide() {
            $('#ContentPlaceHolder1_dvAssayName').removeClass("in");
            $('#ContentPlaceHolder1_dvAssayName').attr("style", "display:none");
            $('#ContentPlaceHolder1_dvAssayName').hide();
        }

        function creditSecHide() {
            $('#ContentPlaceHolder1_dvCreditCardSecure').removeClass("in");
            $('#ContentPlaceHolder1_dvCreditCardSecure').attr("style", "display:none");
            $('#ContentPlaceHolder1_dvCreditCardSecure').hide();
            $('#ContentPlaceHolder1_lblCardNoSec').val('');
            $('#ContentPlaceHolder1_lblCardTypeSec').val('');
            $('#ContentPlaceHolder1_lblCardHolderSec').val('');
            $('#ContentPlaceHolder1_lblCvvSec').val('');
            $('#ContentPlaceHolder1_lblExpireDateSec').val('');
        }

        $(document).ready(function () {
            //this calculates values automatically 
            if ($('#<%=lblTransitError .ClientID %>').text() == " Drawn date should be before the Received date") {
                $('#ContentPlaceHolder1_txtTransitTime').focus();
                return false;
            }
            $('#<%=txtTransitTime .ClientID %>').on("focus", function () {
                 txtTransitTime();
             });
         });


        function txtTransitTime() {
            // Get Variables
            var txtReceivedOn = $('#<%= txtReceivedOn.ClientID %>').val();
            var txtReceivedTime = $('#<%= txtReceivedTime.ClientID %>').val();
            var txtDrawndate = $('#<%=txtDrawndate.ClientID %> ').val();
            var txtDrawnTime = $('#<%=txtDrawnTime.ClientID %> ').val();

            //var dt = new Date();
            //var h = dt.getHours(), m = dt.getMinutes();
            //var _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');
            //txtReceivedOn = (txtReceivedOn + " " + _time);
            //txtReceivedOn = new Date(txtReceivedOn);
            txtReceivedOn = new Date(txtReceivedOn + " " + txtReceivedTime);
            txtDrawndate = new Date(txtDrawndate + " " + txtDrawnTime);
            var hours = (txtReceivedOn - txtDrawndate) / 36e5;
            console.log(hours.toFixed(2));

            if (hours < 0) {
                $('#<%=lblTransitError.ClientID %>').text("Drawn date should be before Received date");
                $('#<%=txtTransitTime.ClientID %>').val("");
            }
            <%--else if (hours = 0)
            {
                $('#<%=txtTransitTime.ClientID %>').val(parseInt(hours));
                $('#<%=lblTransitError.ClientID %>').text("");
            }--%>

            else {
                $('#<%=txtTransitTime.ClientID %>').val(hours.toFixed(2));
                $('#<%=lblTransitError.ClientID %>').text("");
            }
        }

        $(document).ready(function () {
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

            $('#ContentPlaceHolder1_ReceivedTime').timepicker({
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

    </script>
</asp:Content>


