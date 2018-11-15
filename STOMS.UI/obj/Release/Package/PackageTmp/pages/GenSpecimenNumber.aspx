<%@ Page Title="" Language="C#" MasterPageFile="~/themes/MasterNoLeftNav.Master" AutoEventWireup="true" CodeBehind="GenSpecimenNumber.aspx.cs" Inherits="STOMS.UI.pages.GenSpecimenNumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <h3>Specimen Number Generator</h3>
        </div>
        <div class="row">
            <div class="col-lg-9">
                <div class="row">
                    <div style="float: left; margin-left: 18px;">
                        <asp:TextBox ID="txtSpNos" runat="server" CssClass="input60 GenInput2" Text="1"></asp:TextBox>
                        <asp:Button ID="btnSpecimenNum" CssClass="btn btn-primary" OnClick="btnSpecimenNum_Click" runat="server" Text="Generate New Specimen Number(s)" />

                    </div>
                    <div style="float: right; margin-right: 20px; margin-bottom: 5px;">
                        <asp:Button ID="btnTracking" CssClass="btn btn-danger" runat="server" Text="Back to Request Info Input" OnClick="btnTracking_Click" />
                    </div>
                    <div style="clear: right;"></div>
                </div>

                <div class="row" style="text-align: center; min-height: 690px; border: 1px dashed #aaa; text-align: center; padding: 5px 5px 5px 5px; margin-left: 5px; margin-top: 15px; margin-right: 8px; background-color: beige;">
                    <asp:Repeater ID="rpGenNumbers" runat="server" OnItemCommand="rpGenNumbers_ItemCommand">
                        <ItemTemplate>
                            <div class="row rowDetail" style="text-align: center; font-size: 48px;">
                            <asp:LinkButton ID="lbtnGenMubers" CommandArgument='<%# Eval("SpecimenNumber") %>' CommandName="Link" runat="server" Text='<%# Eval("SpecimenNumber") %>'></asp:LinkButton>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="alert alert-info" style="height: 840px;">
                    <h4>Created Specimen Number</h4>
                    Click any of the number to start filling other details
                    <div id="dvRightPanel1">


                        <asp:Repeater ID="rpSpecimenNos" runat="server" OnItemDataBound="rpSpecimenNos_ItemDataBound" OnItemCommand="rpSpecimenNos_ItemCommand">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="row rowDetail rowIndGreen">
                                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenNumber") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                    <div style="color: #272626;">
                                        <small>
                                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <div class="row rowDetail rowIndOrange">
                                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenNumber") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                    <div style="color: #272626;">
                                        <small>
                                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </AlternatingItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
