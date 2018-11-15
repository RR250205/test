<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupHeader.ascx.cs" Inherits="STOMS.UI.usercontrol.GroupHeader" %>
<div class="row" id="dvHeader" runat="server" visible="false">
    <div class="alert alert-info">
        <div style="float: left">
            <strong>Group Name: </strong>
            <asp:Literal ID="ltrGroupName" runat="server"></asp:Literal><br />
            <strong>Create Date: </strong>
            <asp:Literal ID="ltrCreateDate" runat="server"></asp:Literal><br />
            <strong>Sample Count: </strong>
            <asp:Literal ID="ltrSampleCount" runat="server"></asp:Literal>
        </div>
        <div style="float: right">
            <strong>Group Number: </strong>
            <asp:Literal ID="ltrGroupNo" runat="server"></asp:Literal><br />
            <div class="btn-group" runat="server" id="dvStatusbutton">
                <button id="btnStatus" runat="server">
                    Group Status:<asp:Literal ID="ltrStatus" runat="server"></asp:Literal>&nbsp;<i class="icon-caret-down icon-on-right"></i></button>
            </div>
        </div>
        <div style="clear: both"></div>
    </div>
</div>



