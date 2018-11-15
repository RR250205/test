<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditLog.ascx.cs" Inherits="STOMS.UI.usercontrol.AuditLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadGrid ID="tgrdAudit" runat="server" AllowPaging="True" AllowSorting="True"
    ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
    BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15">
    <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
        Width="100%">
        <NoRecordsTemplate>
            <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                <center>No record to display</center>
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn DataField="ActionOn" HeaderText="Date" />
            <telerik:GridBoundColumn DataField="ActionName" HeaderText="Action" />
            <telerik:GridBoundColumn DataField="ActionBy" HeaderText="Performed By" />
        </Columns>
    </MasterTableView>
</telerik:RadGrid>