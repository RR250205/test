<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.ascx.cs" Inherits="STOMS.UI.usercontrol.SearchResult" %>
<asp:PlaceHolder ID="phSummary" runat="server">
    <div class="bg-gray" style="min-height: 260px; margin-left: -20px; padding: 12px 12px 12px 12px;">
        <h4>
            <asp:Literal ID="ltrSearchType" runat="server"></asp:Literal>
        </h4>
        <div id="dvSpecimenRec">
            <div class="inner">
                <div class="slimScrollDiv" style="position: relative; overflow-y: auto; overflow-x: hidden; width: auto; height: 222px">
                    <div class="row">
                        <asp:Repeater ID="rpSearchResult" runat="server" OnItemCommand="rpSearchResult_ItemCommand" OnItemDataBound="rpSearchResult_ItemDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <div class="row rowDetail rowIndGreen">
                                        <asp:LinkButton CommandArgument='<%# Eval("MainTitle") %>' CommandName="Detail" Text='<%# Eval("MainTitle") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>

                                        <div style="color: #272626;">
                                            <small>
                                                <asp:Literal ID="ltrSpecDetails" runat="server"></asp:Literal>
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="row rowDetail rowIndGreen" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
                            <br />
                            No Record...
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hStatus" runat="server" Value="" />
        <asp:HiddenField ID="hAssayID" runat="server" Value="" />
        <asp:HiddenField ID="hSpecimenCount" runat="server" Value="" />
    </div>
</asp:PlaceHolder>

