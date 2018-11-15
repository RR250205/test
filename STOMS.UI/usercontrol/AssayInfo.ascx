<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssayInfo.ascx.cs" Inherits="STOMS.UI.usercontrol.AssayInfo" %>
<%--<%@ Register Src="~/usercontrol/SampleList.ascx" TagPrefix="uc1" TagName="SampleList" %>--%>


<asp:PlaceHolder ID="phSummary" runat="server">
    <h4>&nbsp;Assay #:
        <asp:LinkButton runat="server" ID="lbtnAssay" CommandName="ADetail" OnCommand="lbtnAssay_Command"></asp:LinkButton>
    </h4>
    <asp:HiddenField ID="hAssayID" runat="server" />
    
        <asp:Repeater ID="rpAssaySpecimens" runat="server" OnItemDataBound="rpAssaySpecimens_ItemDataBound" OnItemCommand="rpAssaySpecimens_ItemCommand">            
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row rowDetail rowIndGreen">

                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    <div style="color: #272626;">
                        <small>
                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div class="row rowDetail rowIndOrange">
                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    <div style="color: #272626;">
                        <small>
                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </AlternatingItemTemplate>                
        </asp:Repeater>
    
    <div class="row rowDetail rowIndGreen" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
        <br />
        No Record...
    </div>
</asp:PlaceHolder>
