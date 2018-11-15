<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="custList.ascx.cs" Inherits="STOMS.UI.usercontrol.custList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="alert alert-info" style="margin-left: 2px !important;">
    <div class="row">
        <div class="col-lg-10">
            <h4 class="header headline">
                <asp:Literal ID="ltrListCap" runat="server" Text="List of Customers"></asp:Literal></h4>
        </div>
        <div class="col-lg-2" style="margin-bottom: 5px !important;">
            <div style="float: right !important;">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <telerik:RadGrid ID="tgrdCust" runat="server" AllowPaging="false" AllowSorting="True"
                ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="1" OnItemDataBound="tgrdCust_ItemDataBound">
                <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                    Width="100%">
                    <NoRecordsTemplate>
                        <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                            <center>No record to display</center>
                        </div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Client Name" />
                        <telerik:GridBoundColumn DataField="Address1" HeaderText="Address" />
                        <telerik:GridBoundColumn DataField="City" HeaderText="City" />
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" />
                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <span runat="server" id="spOrderStatus"></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                      <%-- <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="120px">
                            <ItemTemplate>
                                <div class="hidden-phone visible-desktop btn-group">
                                    <button class="btn btn-info btn-sm" onclick="javascript:setAction('CE','<%# Eval("CustomerID") %>');return false;">
                                        <i class="fa fa-edit bigger-120"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm" onclick="javascript:setAction('CD','<%# Eval("CustomerID") %>');return false;">
                                        <i class="fa fa-recycle bigger-120"></i>
                                    </button>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</div>
