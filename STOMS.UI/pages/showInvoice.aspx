<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="showInvoice.aspx.cs" Inherits="STOMS.UI.pages.showInvoice" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div style="margin: 10px 10px 10px 10px;">
            <div class="col-sm-9">
                <asp:Literal ID="ltrInv" runat="server"></asp:Literal>
            </div>
            <div class="col-sm-3">
                <div class="alert alert-success" style="padding-left: 20px !important; margin-top: 15px; min-height: 300px;">
                    <h4 style="height: 35px; border-bottom: solid 1px; margin-bottom: 30px;">Invoice Payment Details</h4>
                    <div style="margin-left: 15p    x;">
                        <div class="row" style="margin-bottom: 15px;">
                            <strong>Invoice #:</strong>&nbsp;<asp:Literal ID="ltrInvNum" runat="server"></asp:Literal>
                        </div>
                        <div class="row" style="margin-bottom: 15px;">
                            <strong>Invoice Date:</strong>&nbsp;<asp:Literal ID="ltrInvDate" runat="server"></asp:Literal>
                        </div>
                        <div class="row" style="margin-bottom: 15px;">
                            <strong>Invoice Amount:</strong>&nbsp;<asp:Literal ID="ltrAmount" runat="server"></asp:Literal>
                        </div>
                        <div class="row" style="margin-bottom: 15px;">
                            <div class="col-sm-3">
                                <strong>Payment Amount:</strong>
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtInvAmount" runat="server" CssClass="input150 GenInput"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 15px;">
                            <div class="col-sm-4">
                                Payment Type:
                            </div>
                            <div class="col-sm-8">
                                <select name="ddPayType" runat="server" id="ddType" class="GenDD input100" onchange="javascript:getPayType();">
                                    <option value="Credit Card">Credit Card</option>
                                    <option value="Check">Check</option>
                                    <option value="Cash">Cash</option>
                                </select>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 15px;" id="dvCC" runat="server">
                            <div class="row">
                                <div class="col-sm-4">Card Type</div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddCardType" runat="server">
                                        <asp:ListItem Text="Visa" Value="Visa"></asp:ListItem>
                                        <asp:ListItem Text="Mastercard" Value="Mastercard"></asp:ListItem>
                                        <asp:ListItem Text="Amex" Value="Amex"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">Card Number</div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="input150 GenInput"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">Card Name</div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="input150 GenInput"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">Expiry Date</div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="input80 GenInput"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">CVV</div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="input50 GenInput"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">Transaction Reference</div>
                                <div class="col-sm-9"></div>
                            </div>
                            <div class="row" style="margin-bottom: 15px;" id="dvCheck" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-sm-3">Bank Name</div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="input200 GenInput"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">Check Number</div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="input200 GenInput"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <h4 style="height: 35px; border-bottom: solid 1px; margin-bottom: 30px;">Invoice Copy</h4>
                            <div class="row" style="margin-bottom: 15px;">
                                <strong>Invoice file:</strong>
                            </div>
                            <div class="row" style="margin-bottom: 15px;">
                                <asp:Literal ID="ltrInvFile" runat="server"></asp:Literal>
                            </div>
                            <div class="row" style="margin-bottom: 15px;">
                                <asp:Button ID="btnGen" CssClass="btn btn-info" Text="Generate" runat="server" OnClick="btnGen_Click" />
                                <asp:Literal ID="ltrGenDate" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function getPayType() {

                }
            </script>
        </div>
</asp:Content>
