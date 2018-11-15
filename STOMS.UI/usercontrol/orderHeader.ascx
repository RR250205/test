<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="orderHeader.ascx.cs" Inherits="STOMS.UI.usercontrol.orderHeader" %>
<div class="row" id="dvHeader" runat="server" visible="false">
    <div class="alert alert-info">
        <div style="float: left">
            <strong>Customer: </strong>
            <asp:Literal ID="ltrCustName" runat="server"></asp:Literal><br />
            <strong>Order Date: </strong>
            <asp:Literal ID="ltrOrderDate" runat="server"></asp:Literal><br />
            <strong>Sample Count: </strong>
            <asp:Literal ID="ltrSampleCount" runat="server"></asp:Literal>
        </div>
        <div style="float: right">
            <strong>Order Number: </strong>
            <asp:Literal ID="ltrOrderNo" runat="server"></asp:Literal><br />
            <div class="btn-group" runat="server" id="dvStatusbutton" visible="false">
                <button class="btn btn-danger dropdown-toggle" data-toggle="dropdown">
                    Order Current Status:
                                    <asp:Label ID="lblStatusValue" runat="server"></asp:Label>&nbsp;<i class="icon-caret-down icon-on-right"></i></button>
                <asp:Literal ID="ltrFilter" runat="server"></asp:Literal>
            </div>
            <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
        </div>
        <div style="clear: both"></div>
    </div>
</div>
