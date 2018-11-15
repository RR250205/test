<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="orderMgmt.aspx.cs" Inherits="STOMS.UI.pages.orderMgmt" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/usercontrol/showMessage.ascx" TagPrefix="uc1" TagName="showMessage" %>
<%@ Register Src="~/usercontrol/PatSampleTestList.ascx" TagPrefix="uc1" TagName="PatSampleTestList" %>
<%@ Register Src="~/usercontrol/orderHeader.ascx" TagPrefix="uc1" TagName="orderHeader" %>
<%@ Register Src="~/usercontrol/InvoicePrint.ascx" TagPrefix="uc1" TagName="InvoicePrint" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div style="margin: 10px 10px 10px 10px;">
            <asp:PlaceHolder ID="phOrderType" runat="server" Visible="true">
                <div class="col-sm-12">
                    <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px;">
                        <div class="active" style="margin-left: 45px; margin-top: 20px;">
                            <asp:PlaceHolder ID="phrecInput" runat="server" Visible="true">
                                <asp:ValidationSummary ID="vsum" runat="server" ValidationGroup="client" CssClass="valSummary1" />
                                <div>
                                    <h3>Order Detail</h3>
                                </div>
                                <uc1:showMessage runat="server" ID="showMessage1" />
                                <div style="margin-bottom: 15px;">
                                    <ul class="wizard-steps" id="ulSteps" runat="server">
                                        <li class="active" style="width: 220px;" id="li1" runat="server">
                                            <span class="step">1</span>
                                            <span class="title">Ordering Party Detail</span>
                                        </li>
                                        <li style="width: 220px;" id="li2" runat="server">
                                            <span class="step">2</span>
                                            <span class="title">Order Detail</span>
                                        </li>
                                        <li style="width: 220px;" id="li3" runat="server">
                                            <span class="step">3</span>
                                            <span class="title">Result Tracking</span>
                                        </li>
                                        <li style="width: 220px;" id="li4" runat="server">
                                            <span class="step">4</span>
                                            <span class="title">Payment & Documents</span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="row" style="margin-right: 40px; margin-left: 5px;">
                                    <uc1:orderHeader runat="server" ID="orderHeader1" />
                                </div>
                                <div id="dvPhy" runat="server" visible="true">
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Ordering Physician/Company Information</strong>
                                    </div>
                                    <div class="well" style="padding-left: 20px !important; margin-right: 25px; margin-top: 15px;">
                                        <div class="row" id="dvSel" runat="server" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span11">
                                                <asp:DropDownList ID="ddNew" runat="server" Visible="true" CssClass="input100 GenDD chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddNew_SelectedIndexChanged">
                                                    <asp:ListItem Text="New" Value="New" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Existing" Value="Exist"></asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddPhy" runat="server" Visible="false" CssClass="input300 GenDD chosen-select" DataTextField="CustomerName" DataValueField="CustomerID"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddPhy_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span7">
                                                <asp:TextBox ID="txtPhyName" runat="server" placeholder="Physician Name" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CustRequired" runat="server" ControlToValidate="txtPhyName" ValidationGroup="client"
                                                    ErrorMessage="Physician name cannot be empty" ToolTip="Physician name cannot be empty" ForeColor="Red"
                                                    Display="None" Text="*"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span1">
                                                <asp:TextBox ID="txtPhyAddress1" runat="server" CssClass="input420 GenInput" placeholder="Address" ValidationGroup="client"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Address1Required" runat="server" ControlToValidate="txtPhyAddress1" ValidationGroup="client"
                                                    ErrorMessage="Address 1 cannot be empty" ToolTip="Address cannot be empty" ForeColor="Red"
                                                    Display="None" Text="*"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span3">
                                                <asp:TextBox ID="txtPhyCity" runat="server" CssClass="input420 GenInput" placeholder="City" ValidationGroup="client"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span4">
                                                <span>
                                                    <asp:TextBox ID="txtPhyState" runat="server" CssClass="input289 GenInput" placeholder="State / Province"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:TextBox ID="txtPhyPCode" runat="server" CssClass="input120  GenInput" placeholder="Postal Code"></asp:TextBox></span>
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
                                                <asp:TextBox ID="txtPhyEmail" runat="server" CssClass="input420 GenInput" placeholder="Email" ValidationGroup="client"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="txtPhyEmail" ValidationGroup="client"
                                                    ErrorMessage="Email cannot be empty" ToolTip="Email cannot be empty" ForeColor="Red"
                                                    Display="None" Text="*"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="txtPhyEmail"
                                                    Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                            <span class="editable" id="Span8">
                                                <asp:TextBox ID="txtPhyPhone" placeholder="Phone number" CssClass="input210 GenInput" runat="server" />&nbsp;&nbsp;
                                            <asp:TextBox ID="txtPhyFax" placeholder="Fax number" CssClass="input200 GenInput" runat="server" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvPat" runat="server" visible="false">
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Patient Information</strong>
                                    </div>
                                    <div class="well" style="padding-left: 35px !important; margin-right: 25px; margin-top: 15px;">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px;" runat="server" id="dvRow1">
                                            <asp:TextBox ID="txtPatName1" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender1" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB1" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag1" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID1" runat="server" Value="0" />
                                            <asp:Button ID="btnMore1" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="1" Visible="false" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px; background-color: #efefef;" runat="server" id="dvRow2" visible="false">
                                            <asp:TextBox ID="txtPatName2" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender2" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB2" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag2" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID2" runat="server" Value="0" />
                                            <asp:Button ID="btnMore2" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="2" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px;" runat="server" id="dvRow3" visible="false">
                                            <asp:TextBox ID="txtPatName3" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender3" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB3" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag3" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID3" runat="server" Value="0" />
                                            <asp:Button ID="btnMore3" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="3" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px; background-color: #efefef;" runat="server" id="dvRow4" visible="false">
                                            <asp:TextBox ID="txtPatName4" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender4" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB4" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag4" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID4" runat="server" Value="0" />
                                            <asp:Button ID="btnMore4" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="4" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px;" runat="server" id="dvRow5" visible="false">
                                            <asp:TextBox ID="txtPatName5" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender5" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB5" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag5" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID5" runat="server" Value="0" />
                                            <asp:Button ID="btnMore5" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="5" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px; background-color: #efefef;" runat="server" id="dvRow6" visible="false">
                                            <asp:TextBox ID="txtPatName6" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender6" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB6" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag6" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID6" runat="server" Value="0" />
                                            <asp:Button ID="btnMore6" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="6" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px;" runat="server" id="dvRow7" visible="false">
                                            <asp:TextBox ID="txtPatName7" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender7" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB7" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag7" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID7" runat="server" Value="0" />
                                            <asp:Button ID="btnMore7" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="7" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px; background-color: #efefef;" runat="server" id="dvRow8" visible="false">
                                            <asp:TextBox ID="txtPatName8" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender8" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB8" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag8" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID8" runat="server" Value="0" />
                                            <asp:Button ID="btnMore8" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="8" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px;" runat="server" id="dvRow9" visible="false">
                                            <asp:TextBox ID="txtPatName9" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender9" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB9" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtDiag9" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID9" runat="server" Value="0" />
                                            <asp:Button ID="btnMore9" runat="server" Text="More Patient" CssClass="btn btn-info" OnClick="btnMore_Click" CommandArgument="9" />
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px; margin-right: 30px; background-color: #efefef;" runat="server" id="dvRow10" visible="false">
                                            <asp:TextBox ID="txtPatName10" runat="server" placeholder="Name or ID" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                            &nbsp;
                                            <asp:DropDownList ID="ddGender10" runat="server" Visible="true" CssClass="input100 GenDD chosen-select">
                                                <asp:ListItem Text="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:TextBox ID="txtDOB10" runat="server" CssClass="input100 GenInput" placeholder="DOB"></asp:TextBox>
                                            &nbsp;<asp:HiddenField ID="hPatID10" runat="server" Value="0" />
                                            <asp:TextBox ID="txtDiag10" runat="server" placeholder="Diagnosis details" ValidationGroup="client" CssClass="GenInput input420"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Order Test (s)</strong>
                                    </div>
                                    <div class="well" style="padding-left: 35px !important; margin-right: 25px; margin-top: 15px;">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <span class="editable" id="Span2">
                                                <input type="checkbox" id="chkBind" runat="server" />&nbsp;&nbsp;Folate Receptor A Binding Antibodies
                                            </span>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <span class="editable" id="Span9">
                                                <input type="checkbox" id="chkBlock" runat="server" />&nbsp;&nbsp;Folate Receptor A Blocking Antibodies
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvShip" runat="server" visible="false">
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Test results</strong>
                                    </div>
                                    <div class="well" style="padding-left: 35px !important; margin-right: 25px; margin-top: 15px;">
                                        <uc1:PatSampleTestList runat="server" ID="PatSampleTestList1" />
                                    </div>
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Shipment/delivery Information</strong>
                                    </div>
                                    <div class="well" style="padding-left: 35px !important; margin-right: 25px; margin-top: 15px;">
                                        <strong>Method</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddResultDel" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddResultDel_SelectedIndexChanged" CssClass="input120 GenDD chosen-select">
                                            <asp:ListItem Text="Email" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Mail"></asp:ListItem>
                                            <asp:ListItem Text="Mail & Email"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="row" style="margin: 0px 20px 5px 2px;" id="dvDelEmail" runat="server" visible="true">
                                            <asp:TextBox ID="txtDelEmail" runat="server" CssClass="input420 GenInput" placeholder="Result Delivery Email"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDelEmail"
                                                Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="row" style="margin: 0px 20px 5px 2px;" id="dvDelMail" runat="server" visible="false">
                                            <div style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:TextBox ID="txtDelAddressName" runat="server" placeholder="Addressed to" CssClass="GenInput input420"></asp:TextBox>
                                            </div>
                                            <div style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:TextBox ID="txtDelAddress" runat="server" CssClass="input420 GenInput" placeholder="Address"></asp:TextBox>
                                            </div>
                                            <div style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:TextBox ID="txtDelCity" runat="server" CssClass="input420 GenInput" placeholder="City"></asp:TextBox>
                                            </div>
                                            <div style="margin-top: 5px; margin-bottom: 5px;">
                                                <span>
                                                    <asp:TextBox ID="txtDelState" runat="server" CssClass="input289 GenInput" placeholder="State / Province"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:TextBox ID="txtDelZip" runat="server" CssClass="input120  GenInput" placeholder="Postal Code"></asp:TextBox></span>
                                            </div>
                                            <div style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:DropDownList ID="ddDelCountry" runat="server" Visible="true" CssClass="input300 GenDD chosen-select" DataTextField="CountryName" DataValueField="CountryName">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvPayment" runat="server" visible="false">
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                        <strong>Invoice & Payment Information</strong>
                                    </div>
                                    <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                                        <div class="col-sm-9">
                                            <div class="well" style="padding-left: 20px !important; margin-right: 5px; margin-top: 15px; min-height: 300px;">
                                                <div style="border: 1px solid #c5d0dc; min-height: 300px; background-color: #ffffff; margin-bottom: 10px;">
                                                    <uc1:InvoicePrint runat="server" ID="InvoicePrint1" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="alert alert-success" style="padding-left: 20px !important; margin-top: 15px; min-height: 300px;">
                                                <h4>Payment Detail</h4>
                                                <div style="margin-left: 15px;">
                                                    <div class="row" style="margin-bottom:15px;">
                                                        <strong>Payment Status:</strong>&nbsp;<asp:Literal ID="ltrPayStatus" runat="server"></asp:Literal>
                                                    </div>
                                                    <div class="row">
                                                        Payment Mode<br />
                                                        <asp:DropDownList ID="ddPayMode" runat="server" Visible="true" CssClass="input200 GenDD chosen-select">
                                                            <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                                            <asp:ListItem Text="Check" Value="Check"></asp:ListItem>
                                                            <asp:ListItem Text="Credit Card" Value="Credit Card" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="row">
                                                        Payment Reference<br />
                                                        <asp:TextBox ID="TextBox2" runat="server" placeholder="Reference notes" ValidationGroup="client" CssClass="GenInput input300"></asp:TextBox>
                                                    </div>
                                                    <div class="row">
                                                        Comments<br />
                                                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Payment notes" ValidationGroup="client" CssClass="GenInput input300"></asp:TextBox>
                                                    </div>
                                                    <div class="row" style="margin-top:15px;">
                                                        <asp:Button ID="btnPayment" runat="server" Text="Save Payment Details" CssClass="btn btn-primary" ValidationGroup="client" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="margin-top: 15px; margin-bottom: 15px; float: left">
                                    <asp:Button ID="btnInitOrder" runat="server" Text="Save Physician/Company Info" CssClass="btn btn-primary" OnClick="btnInitOrder_Click" ValidationGroup="client" />
                                </div>
                                <div style="margin-top: 15px; margin-bottom: 15px; float: right">
                                    <asp:Button ID="btnPrev" runat="server" Text="Previous" CssClass="btn btn-primary" OnClick="btnPrev_Click" ValidationGroup="client" Visible="false" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary" OnClick="btnNext_Click" ValidationGroup="client" Visible="false" />
                                </div>
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phOrderDetail" runat="server" Visible="false"></asp:PlaceHolder>
        </div>
    </div>

    <asp:HiddenField ID="hOrderID" runat="server" Value="0" />
    <asp:HiddenField ID="hCustID" runat="server" Value="0" />
    <asp:HiddenField ID="hNewCust" runat="server" Value="0" />
    <asp:HiddenField ID="hActType" runat="server" Value="A" />
    <asp:HiddenField ID="hActValue" runat="server" OnValueChanged="hActValue_ValueChanged" />
    <asp:HiddenField ID="hStep" runat="server" Value="1" />
    <asp:HiddenField ID="hOrderAmount" runat="server" />
    <asp:HiddenField ID="hDefaultDiscount" runat="server" />
    <asp:HiddenField ID="hMaxAllowedDiscount" runat="server" />

    <asp:LinkButton ID="lnkAction" runat="server" Text="" OnClick="lnkAction_Click"></asp:LinkButton>
    <script type="text/javascript">
        function gridAction(actType, actValue, isPost) {
            document.getElementById('<%= hActType.ClientID %>').value = actType;
            document.getElementById('<%= hActValue.ClientID %>').value = actValue;
            if (isPost == 'Y') {
                var hValueID = "<%= hActValue.ClientID %>";
                __doPostBack(hValueID, "");
            }
        }
        function setAction(sTyp) {
            document.getElementById('<%=hActType.ClientID %>').value = sTyp;
            __doPostBack('ctl00$ContentPlaceHolder1$lnkAction', '');
            return false;
        }
    </script>
</asp:Content>
