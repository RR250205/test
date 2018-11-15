<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingSampleList.ascx.cs" Inherits="STOMS.UI.usercontrol.PendingSampleList" %>
<asp:PlaceHolder ID="phSummary" runat="server">
    <asp:Repeater ID="rpPendingSampleList" runat="server" OnItemDataBound="rpPendingSampleList_ItemDataBound" OnItemCommand="rpPendingSampleList_ItemCommand">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row rowDetail rowIndGreen">
                <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnPenSampleNo" runat="server"></asp:LinkButton>
                <asp:Literal ID="ltrPenStatus" runat="server"></asp:Literal>
                <div style="color: #272626;">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                </div>
                 <div style="color: #F62459;">
                    <small>
                        <asp:Literal ID="ltrPendingDetails" runat="server"></asp:Literal></small>
                </div>
            </div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div class="row rowDetail rowIndOrange">
                <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnPenSampleNo" runat="server"></asp:LinkButton>
                <asp:Literal ID="ltrPenStatus" runat="server"></asp:Literal>
                <div style="color: #272626;">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                </div>
                 <div style="color: #F62459;">
                    <small>
                        <asp:Literal ID="ltrPendingDetails" runat="server"></asp:Literal></small>
                </div>
            </div>
        </AlternatingItemTemplate>
    </asp:Repeater>
    <div class="row rowDetail rowIndGreen" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
        <br />
        No Record...
    </div>
    <asp:HiddenField ID="hPenStatus" runat="server" Value="" />
  
    <asp:HiddenField ID="hPenSpecimenCount" runat="server" Value="" />
</asp:PlaceHolder>