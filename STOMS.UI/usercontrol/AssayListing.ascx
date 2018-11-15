<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssayListing.ascx.cs" Inherits="STOMS.UI.usercontrol.AssayListing" %>
<div class="alert alert-warning">
    <div style="margin-right: 15px;">
        <asp:Repeater ID="rpAssayList" runat="server" OnItemDataBound="rpAssayList_ItemDataBound" OnItemCommand="rpAssayList_ItemCommand">
            <HeaderTemplate>
                <div class="row" style="background-color: #32608d; color: #ffffff; padding-top: 9px; padding-bottom: 9px;">
                    <div class="col-sm-2">Assay Number</div>
                    <div class="col-sm-3">Description</div>
                    <div class="col-sm-2">Test Start Date & Time</div>
                    <div class="col-sm-2">Test Complete Date & Time</div>
                    <div class="col-sm-2" style="text-align: center"># of Specimens</div>
                    <div class="col-sm-1" style="text-align: right">Status</div>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row" style="background-color: #ffffff; border-bottom: solid #b7b6b6 1px; padding-top: 5px; padding-bottom: 5px;">
                    <div class="col-sm-2">
                        <asp:LinkButton runat="server" ID="lbtnTask" CommandArgument='<%# Eval("AssayID") %>' Text='<%# Eval("AssayBIN") %>' CommandName="lnk"></asp:LinkButton>
                    </div>
                    <div class="col-sm-3">
                        <%# Eval("AssayDesc") %>
                    </div>
                    <div class="col-sm-2">
                         <asp:Literal ID="ltrStartDate" runat="server" Text='<%# Eval("AssayLoadDateTime") %>' ></asp:Literal>
                         
                    </div>
                    <div class="col-sm-2">
                       <asp:Literal ID="ltrCompleteDate" runat="server" Text='<%# Eval("AssayCompleteDateTime") %>' ></asp:Literal>
                    </div>
                    <div class="col-sm-1" style="text-align: center">
                        <%# Eval("SampleCount") %>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    </div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div class="row" style="background-color: #e0dfdf; border-bottom: solid #b7b6b6 1px; padding-top: 5px; padding-bottom: 5px;">
                    <div class="col-sm-2">
                        <asp:LinkButton runat="server" ID="lbtnTask" CommandArgument='<%# Eval("AssayID") %>' Text='<%# Eval("AssayBIN") %>' CommandName="lnk"></asp:LinkButton>
                    </div>
                    <div class="col-sm-3">
                        <%# Eval("AssayDesc") %>
                    </div>
                    <div class="col-sm-2">
                        <asp:Literal ID="ltrStartDate" runat="server" Text='<%# Eval("AssayLoadDateTime") %>' ></asp:Literal>
                    </div>
                    <div class="col-sm-2">
                         <asp:Literal ID="ltrCompleteDate" runat="server" Text='<%# Eval("AssayCompleteDateTime") %>' ></asp:Literal>
                    </div>
                    <div class="col-sm-1" style="text-align: center">
                        <%# Eval("SampleCount") %>
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    </div>
                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </div>
    <div class="row rowDetail rowIndGreen" style="background-color: #ffffff; border-bottom: solid #b7b6b6 1px; 
        margin-right: 1px; margin-left: -15px;padding-top: 5px; padding-bottom: 5px; text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
        <br />
        No Record...
    </div>
</div>

<asp:HiddenField ID="hAssayStatus" runat="server" />
