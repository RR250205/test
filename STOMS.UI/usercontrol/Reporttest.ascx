<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporttest.ascx.cs" Inherits="STOMS.UI.usercontrol.Report" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="alert alert-info" style="margin-left: 2px !important;">
    <div class="row">
        <div class="col-lg-8">
            <h4 class="header headline">
                <asp:Literal ID="ltrListCap" runat="server" Text="Order Trending Report"></asp:Literal></h4>
        </div>
        <div class="col-lg-4" style="margin-bottom: 5px !important;">
            <div style="float: right !important;">
                <div class="btn-group">
                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                        Order Trending Report:
                <asp:Label ID="lblStatusValue" runat="server"></asp:Label>&nbsp;<i class="icon-caret-down icon-on-right"></i></button>
                    <ul class="dropdown-menu">
                        <li><a href="javascript:getOrder('O', 'Delivered','');">Delivered</a></li>
                        <%--<li><a href="javascript:getOrder('O', 'Week','');">Week</a></li>
                        <li><a href="javascript:getOrder('O', 'Month','');">Month</a></li>
                        <li><a href="javascript:getOrder('O', 'Quarter','');">Quarter</a></li>--%>
                    </ul>
                </div>
               <%-- <asp:Button ID="btnTopCLients" class="btn btn-success" Text="Top 10 Clients" runat="server" Visible="true" OnClick="btnTopCLients_Click" />--%>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <telerik:RadGrid ID="tgrdReport" runat="server" AllowPaging="True" AllowSorting="True"
                ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdReport_ItemDataBound">
                <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                    Width="100%">
                    <NoRecordsTemplate>
                        <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                            <center>No record to display</center>
                        </div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="OrderNumber" HeaderText="Order Number" />
                        <telerik:GridTemplateColumn HeaderText="Order Date" UniqueName="ChkColumn1">
                            <ItemTemplate>
                                <span runat="server" id="spOrderDate"></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Client Name" />
                        <telerik:GridBoundColumn DataField="SampleCount" HeaderText="No. Of Samples" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="OrderAmount" HeaderText="Order Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <span runat="server" id="spOrderStatus"></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="140px">
                            <ItemTemplate>
                                <div class="hidden-phone visible-desktop btn-group">
                                    <%--<button class="btn btn-info btn-sm" onclick="javascript:getOrder('E','<%# Eval("OrderStatus") %>','<%# Eval("OrderID") %>');return false;">
                                        <i class="fa fa-edit bigger-120"></i>
                                    </button>
                                    <button class="btn btn-danger btn-sm" onclick="javascript:getOrder('T','<%# Eval("OrderStatus") %>','<%# Eval("OrderID") %>');return false;">
                                        <i class="fa fa-recycle bigger-120"></i>
                                    </button>--%>
                                    <button class="btn btn-success btn-sm" onclick="javascript:getOrder('I','<%# Eval("OrderStatus") %>','<%# Eval("OrderID") %>');return false;">
                                        <i class="fa fa-bank bigger-120" title="Invoice"></i>
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
</div>

<asp:HiddenField ID="hAct" runat="server" />
<asp:HiddenField ID="hOrderID" runat="server" Value="0" OnValueChanged="hOrderID_ValueChanged" />
<asp:HiddenField ID="hOrderStatus" runat="server" OnValueChanged="hOrderStatus_ValueChanged" />


<script type="text/javascript">

    function getOrder(act, status, ID) {
        document.getElementById('<%= hAct.ClientID %>').value = act;
                document.getElementById('<%= hOrderStatus.ClientID %>').value = status;
                document.getElementById('<%= hOrderID.ClientID %>').value = ID;

                var hValueID = "<%= hOrderID.ClientID %>";
                __doPostBack(hValueID, "");
            }


</script>
