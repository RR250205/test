<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="invoice.aspx.cs" Inherits="STOMS.UI.pages.invoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div class="col-sm-12">
            <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px;min-height:650px;">
                <div style="margin: 10px 10px 10px 10px;">
                    <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                        <div class="col-sm-9">
                            <div class="well" style="padding-left: 20px !important; margin-right: 5px; margin-top: 15px; min-height: 300px;">
                                <telerik:RadGrid ID="tgrdInvoice" runat="server" AllowPaging="True" AllowSorting="True"
                                    ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                                    BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdInvoice_ItemDataBound">
                                    <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                                        Width="100%">
                                        <NoRecordsTemplate>
                                            <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                                                <center>No record to display</center>
                                            </div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="InvNumber" HeaderText="Invoice Number" />
                                            <telerik:GridTemplateColumn HeaderText="Invoice Date" UniqueName="ChkColumn1">
                                                <ItemTemplate>
                                                    <span runat="server" id="spInvoiceDate"></span>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Client Name" />
                                            <telerik:GridBoundColumn DataField="OrderNumber" HeaderText="Order Number" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="InvAmount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <span runat="server" id="spInvStatus"></span>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <div class="hidden-phone visible-desktop btn-group">
                                                        <button class="btn btn-info btn-sm" onclick="javascript:getInv('E','<%# Eval("InvStatus") %>','<%# Eval("InvoiceID") %>');return false;">
                                                            <i class="fa fa-edit bigger-120"></i>
                                                        </button>
                                                        <button class="btn btn-danger btn-sm" onclick="javascript:getInv('D','<%# Eval("InvStatus") %>','<%# Eval("InvoiceID") %>');return false;">
                                                            <i class="fa fa-recycle bigger-120"></i>
                                                        </button>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="alert alert-success" style="padding-left: 20px !important; margin-top: 15px; min-height: 300px;">
                                <h4 style="height: 35px; border-bottom: solid 1px; margin-bottom: 30px;">Invoice Summary</h4>
                                <div style="margin-left: 15px;">
                                    <div class="row" style="margin-bottom: 25px;">
                                        <strong>Open Invoices:</strong>&nbsp;<asp:Literal ID="ltrOpenInv" runat="server"></asp:Literal>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <strong># of Orders without Invoice:</strong>&nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <strong>Invoices Open 90+ days:</strong>&nbsp;<asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
