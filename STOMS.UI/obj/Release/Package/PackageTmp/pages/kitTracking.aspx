<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="kitTracking.aspx.cs" Inherits="STOMS.UI.pages.kitTracking" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>

    <style>
        .table > tbody > tr > th {
            border-top: 1px solid grey;
        }
    </style>
    <div class="row">
        <div class="col-lg-7">
            <b><a href="CourierTracking.aspx">Track Courier</a></b>
        </div>
    </div>
    <hr />
    <div class="row">

        <div class="col-lg-9" id="dvKitOrderManual" runat="server">
            <asp:Label runat="server"><h2 style="text-align:center">Request Entry Form</h2></asp:Label>&nbsp;
            <asp:Label runat="server" ID="lblRequest" Style="Font-Size:18px;font-weight:600;color:rgb(12, 156, 18)"></asp:Label>
            <br />

            <div class="box box-info">
                <div class="box-body" style="min-height: 720px;">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" style="height: 100px;" id="dvCustomerNumber" runat="server">
                                    <div class="profile-info-name">
                                        Customer
                                    <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" display="Dynamic">
                                                <asp:ListItem disabled="disabled" Value="-1">Customer Type</asp:ListItem>
                                                <asp:ListItem Text="New Customer" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Existing Customer" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                        <div id="dvCustNumber" runat="server" visible="false" >
                                            <h5>Customer Number:</h5>
                                            <asp:TextBox ID="txtCustomerNumber" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnCustomerNumber" runat="server" CssClass="btn btn-success" OnClick="btnCustomerNumber_Click" Style="float: right; padding: 0px 4px 5px 7px;" Text="Search" />
                                        </div>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        First Name 
                                    <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtFirstName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFirstName" ID="rfdFirstName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgFirstName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Last Name
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtLastName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ForeColor="Red" ControlToValidate="txtLastName" Display="Dynamic" ID="rfdLastName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgLastName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Facility
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <%--<div style="margin-top: 15px">--%>
                                        <asp:TextBox ID="txtFacility" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFacility" ID="rfdFacility" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        <%--</div>--%>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Address 
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtAddress" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtAddress" ID="rfdAddress" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        City
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtCity" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCity" ID="rfdCity" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgCity" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtCity" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        State
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtState" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ForeColor="Red" Display="Dynamic" ControlToValidate="txtState" ID="rfdState" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgState" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtState" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Country
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtCountry" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountry" ID="rfdCountry" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgCountry" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Zip
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtZip" Width="81%" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtZip" ID="rfdZip" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgZip" runat="server" ForeColor="Red" ErrorMessage="Allow numbers Only" ControlToValidate="txtZip" ValidationExpression="^[0-9]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Kit Type
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:DropDownList ID="ddlKitType" CssClass="form-control" Width="81%" runat="server">
                                            </asp:DropDownList>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        No. of kits
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtNoOfKits" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;                                            
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtNoOfKits" ID="rfdtxtNoOfKits" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgNoOfKits" runat="server" ForeColor="Red" ErrorMessage="Allow positive numbers Only" ControlToValidate="txtNoOfKits" ValidationExpression="^[1-9]\d*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Requester Type
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtRequesterType" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" Display="Dynamic" ForeColor="Red" ControlToValidate="txtRequesterType" ID="rfdRequesterType" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Telephone
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtTelephone" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTelephone" ID="rfdTelephone" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgTelephone" runat="server" ForeColor="Red" ErrorMessage="Allow numbers and some characters Only" ControlToValidate="txtTelephone" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Email
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtEmail" Width="81%" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ID="rfdEmail" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgEmail" ForeColor="Red" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>
                                <%--<div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Accession Number
                                        <b><i style="color:red;font-size:18px;">*</i></b>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtAcessionno" Width="81%" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vKitOrder" ID="rfdAcNo" ForeColor="Red" Display="Dynamic" ControlToValidate="txtAcessionno" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vKitOrder" ID="rgAcNo" ForeColor="Red" ControlToValidate="txtAcessionno" Display="Dynamic" ValidationExpression="[a-zA-Z0-9]*$" runat="server"  ErrorMessage="*Valid characters: Alphabets and Numbers."></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>--%>

                                <div class="profile-info-row" style="height: 60px;">
                                    <div class="profile-info-name">
                                        Message                                         
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtMessage" Width="81%" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                        </span>
                                    </div>
                                    <%--<asp:HiddenField runat="server" Value="0" ID="hCustomerNumber" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-6" style="text-align: right">
                            <asp:Button CssClass="btn btn-success" ValidationGroup="vKitOrder" ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="col-lg-6" style="text-align: left">
                            <asp:Button CssClass="btn btn-danger" ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12" style="text-align: center">
                           <asp:Label ID="lblSubmit" runat="server" ></asp:Label>&nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-9" id="dvKitOrderShow" runat="server">

            <div class="row">
                <div class="col-lg-8">
                    <asp:Label runat="server"><h2 style="text-align:center;margin-top:0px">Requested Details</h2></asp:Label>
                </div>
                <div class="col-lg-2">
                    <asp:Button CssClass="btn btn-success" Width="100%" ID="btnNewOrder" Text="New Request" OnClick="btnNewOrder_Click" runat="server" />
                </div>
                <div class="col-lg-2">
                    <asp:Button CssClass="btn btn-warning " Width="70%" ValidationGroup="vKitOrder" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" runat="server" />
                </div>
            </div>
            <div class="box box-info">
                <div class="box-body" style="min-height: 720px;">

                    <div class="widget-box widget-color-dark light-border" id="widget-box-6">
                        <div class="widget-header">
                            <h5 class="widget-title smaller ui-sortable-handle"></h5>

                            <div class="widget-toolbar">

                                <%--<span class="badge badge-danger">Alert</span>--%>
                            </div>
                        </div>

                        <div class="widget-body">
                            <div class="widget-main padding-6">
                                <%--<div class="alert alert-info"> Hello World! </div>--%>
                                <div class="row" runat="server" id="dvHasData">
                                    <div class="col-md-6">
                                        <div class="profile-user-info profile-user-info-striped">
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Customer No
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="lblCustNo" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    First Name 
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrFirstName" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Last Name                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrLastName" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Facility                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <asp:Literal ID="ltrFacility" runat="server"></asp:Literal>&nbsp;                                                    
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Address                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    City                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrCity" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    State                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrState" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Country                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Zip                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrZip" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6" runat="server">
                                        <div class="profile-user-info profile-user-info-striped">

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Request Number                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrOrderNumber" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Kit Type                                       
                                                </div>
                                                <div class="profile-info-value" style="height: 35px">
                                                    <span>
                                                        <asp:Literal ID="ltrKitType" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    No. of kits                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrNoOfKits" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Requester Type                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrRequesterType" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Telephone                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrTelephone" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Email                                       
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Message                                        
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Requested On                                         
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrDate" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Current Status                                      
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:Literal ID="ltrCurrentStatus" runat="server"></asp:Literal>&nbsp;
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="alert alert-danger" id="dvHasnotData" runat="server">
                                    No data
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row" runat="server" id="dvCourierSuccess" visible="false">
                        <div style="margin-left: 3px;">
                            <h4>Tracking Details&nbsp;&nbsp;
                            </h4>
                        </div>
                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Track Numer / View Label </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:HyperLink ID="hyTrackNumber" runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Delivery On </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Label ID="lblDelivery" runat="server"></asp:Label>&nbsp;
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Label Generated On</div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Label ID="lblLabelGeneratedOn" runat="server"></asp:Label>
                                            &nbsp;
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Picked Up On </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Label ID="lblPickedUpOn" runat="server"></asp:Label>&nbsp;
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Is PickedUp </div>
                                    <div class="profile-info-value">
                                        <div class="row input-group" style="margin-bottom: 5px; margin-left: 1px;">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtIsPicked" runat="server" Style="width: 95px;" CssClass="date-picker GenInput"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <i class="fa fa-calendar" runat="server" id="picked"></i>
                                                </span>&nbsp
                                                       <asp:Button ID="btnIsPicked" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnIsPicked_Click" />
                                            </div>
                                        </div>
                                        <%-- <div class="input-group">
                                                            <asp:TextBox ID="txtIsPicked" runat="server"  style="margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker GenInput" placeholder="Date of Birth"></asp:TextBox>
                                                            <span class="input-group-addon">                                                  
                                                            <span class="icon-calendar" id="IsPicked"></span>                                                      
                                                           </span>
                                             </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-left: 3px;">
                        <div class="col-lg-6">
                            
                        <div runat="server" id="dvCourierInterface">

                            <div class="row">
                                <%-- Please Choose Dhipping Service based on Region
                                <asp:CheckBoxList ID="chkFedexServiceList" runat="server">
                                    <asp:ListItem Value="PRIORITY_OVERNIGHT" Text="FedEx Express (U.S)" />
                                    <asp:ListItem Value="INTERNATIONAL_PRIORITY" Text="Fedex Express International" />
                                </asp:CheckBoxList>--%>
                                <%-- Currently Fedex Service suport U.S Region only  --%>

                                <div>
                                    <asp:Button CssClass="btn btn-danger" ID="btnCheckServiceAvailability" Text="Check Service Availability With Configured Courier Services" OnClick="btnCheckServiceAvailability_Click" runat="server" />
                                </div>
                            </div>

                            <div runat="server" id="dvAddAvlResult" visible="false">
                                <div class="row">
                                    <div class="col-md-4 alert alert-danger" runat="server" visible="false" id="dvNoCourierError" style="margin-left: 24px;">
                                        <asp:Literal Text="You have to configured atleast one Carrier" runat="server" />
                                    </div>
                                    <asp:Repeater runat="server" ID="rptCouriers" OnItemDataBound="rptCouriers_ItemDataBound" OnItemCommand="rptCouriers_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-6 alert alert-info" style="float: right; margin-right: 28px; width: 93%">
                                                <asp:Literal ID="ltrCourierName" runat="server"></asp:Literal>
                                                <hr />
                                                <asp:Literal ID="ltrAddrsAvailability" runat="server"></asp:Literal>
                                                <br />
                                                <br />
                                                <div id="dvProceed" runat="server" visible="false">
                                                    <div class="input-group">
                                                        Total Weight:
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCourierWeight" aria-describedby="spWeightAddon" />
                                                        <span style="border: 1px solid #d5d5d5; border-left: none; background-color: #fff; width: 20px; position: absolute; z-index: 10; margin: 20px 20px 0px 177px; padding: 6px 25px 6px 10px;" id="spWeightAddon">LB</span>
                                                    </div>
                                                    <span class="text-danger">* Weight should be in Pounds</span>
                                                    <br />
                                                    Choose a Service:
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFedexParcelType">
                                                    <asp:ListItem Text="FEDEX 10KG BOX" Value="FEDEX_10KG_BOX" />
                                                    <asp:ListItem Text="FEDEX 25KG BOX" Value="FEDEX_25KG_BOX" />
                                                    <asp:ListItem Text="FEDEX BOX" Value="FEDEX_BOX" />
                                                    <asp:ListItem Text="FEDEX EXTRA LARGE_BOX" Value="FEDEX_EXTRA_LARGE_BOX" />
                                                    <asp:ListItem Text="FEDEX  LARGE BOX" Value="FEDEX_LARGE_BOX" />
                                                    <asp:ListItem Text="FEDEX MEDIUM BOX" Value="FEDEX_MEDIUM_BOX" />
                                                    <asp:ListItem Text="FEDEX PAK" Value="FEDEX_PAK" />
                                                    <asp:ListItem Text="FEDEX SMALL BOX" Value="FEDEX_SMALL_BOX" />
                                                </asp:DropDownList>
                                                    <br />
                                                    <asp:Literal runat="server" ID="ltrCourierNotifications" />
                                                    <br />
                                                    <span id="spCourierError" visible="false" class="text-danger" runat="server"></span>
                                                    <br />
                                                    <asp:LinkButton ID="lbtnProceed" Text="Proceed" CssClass="btn btn-primary" runat="server" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                            </div>
                        <div class="col-lg-3">
                            &nbsp;
                        </div>
                        <div class="col-lg-3">
                            <%--<asp:HiddenField ID="hiddenVal" runat="server" />--%>
                            <asp:LinkButton ID="lnkbtnAssignkits" CssClass="btn btn-primary" Text="Assign Kits" OnClick="lnkbtnAssignkits_Click" runat="server" data-uk-modal=""></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3">

            <div class="row">
                <div class="alert alert-info" style="height: 750px;">
                    <div>
                        <b style="font-size: 24px">Request Status:</b>
                    </div>
                    <div>
                        <asp:DropDownList CssClass="form-control" AutoPostBack="true" ID="ddlOrderKitStatus" OnSelectedIndexChanged="ddlOrderKitStatus_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="Kit Requested" Value="Kit Requested" />
                            <asp:ListItem Text="Kit Assigned" Value="Kit Assigned" />
                            <asp:ListItem Text="Label Generated" Value="Label Generated" />
                            <asp:ListItem Text="Request Dispatched" Value="Request Dispatched">
                            </asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:HiddenField ID="hCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hRequestID" runat="server" Value="0" />
                    </div>
                    <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 600px">
                        <div id="dvRightPanel1">
                            <asp:Repeater ID="rptviewOrderRequest" runat="server" OnItemCommand="rptviewOrderRequest_ItemCommand" OnItemDataBound="rptviewOrderRequest_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row rowDetail rowIndGreen">
                                        <asp:LinkButton CommandArgument='<%# Eval("RequestID")%>' CommandName="RequestView" Text='<%# "( "+Eval("NoOfKits")+" ) "+ Eval("FirstName") %>' ID="lbtnNoOfKits" runat="server"></asp:LinkButton>
                                        <br />
                                        <small>
                                            <%# Eval("RequesterType") %> &nbsp;-&nbsp;
                                        <%# Eval("RequestNumber") %>
                                        </small>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:HiddenField ID="hfOrderID" runat="server" Value="0" />--%>
    </div>

    <%--<div class="input-append date form_datetime">
        <input size="16" type="text" value="" readonly="true" />
        <span class="add-on"><i class="icon-calendar"></i></span>
    </div>--%>


    <!-- BEGIN: Modal form assign kits -->
    <div id="myModel" class="modal fade" tabindex="-1" runat="server" aria-hidden="true" style="display: none;">
        <div class="modal-dialog container1">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Button ID="btnclose" runat="server" Text="x" OnClick="btnTopClose_Click" Style="float: right;" />

                    <%--<button type="button" class="close" runat="server" data-dismiss="modal" aria-hidden="true"> <i class="icon-remove">× </i>  </button>  --%>
                    <h4 class="blue bigger">kit details</h4>
                    <asp:HiddenField ID="hpatientid" Value="0" runat="server" />
                </div>
                <div class="modal-body overflow-visible">
                    <div class="well">
                        <div>
                            <div runat="server" visible="false" id="dvNoKits" class="alert alert-danger">
                                There is No kits to Assign with this Request.
                            </div>
                            <asp:GridView Visible="false" ID="gdvwassignkit" BackColor="#f7ecb6" AutoGenerateColumns="false" AllowPaging="true" runat="server">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last"/>
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="KitID" HeaderText="KitID" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Kitnumber" HeaderText="Kit Number" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Reusecount" HeaderText="Re-use Count" />
                                </Columns>
                            </asp:GridView>
                            <asp:GridView Visible="false" ID="gvAssignedView" BackColor="#f7ecb6" AutoGenerateColumns="false" AllowPaging="true" runat="server">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="100px" DataField="KitID" HeaderText="KitID" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Kitnumber" HeaderText="Kit Number" />
                                    <asp:BoundField ItemStyle-Width="100px" DataField="Reusecount" HeaderText="Re-use Count" />
                                    <asp:HyperLinkField ItemStyle-Width="150px" DataNavigateUrlFields="LabelNumber" DataNavigateUrlFormatString="../Docs/CourierLabels/FedEx/{0}.pdf" DataTextFormatString="{0:c}" Target="_blank" HeaderText="Label Number" DataTextField="LabelNumber" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnassign" CssClass="btn btn-success" runat="server" Text="assign" OnClick="btnassign_Click" />
                    <asp:Button Text="close" ID="btnbottomclose" OnClick="btnbottomclose_Click" CssClass="btn btn-primary" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#ContentPlaceHolder1_txtIsPicked").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+0",
                autoclose: true,
                maxDate: new Date()
            });

            $('#ContentPlaceHolder1_picked').click(function () {

                //alert('clicked');
                $("#ContentPlaceHolder1_txtIsPicked").datepicker('show');
            });
            $(".form_datetime").datetimepicker({
                format: "dd MM yyyy - hh:ii"
            });
        });        

    </script>

</asp:Content>
