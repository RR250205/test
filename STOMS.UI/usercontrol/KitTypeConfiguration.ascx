<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KitTypeConfiguration.ascx.cs" Inherits="STOMS.UI.usercontrol.KitTypeConfiguration" %>

<div class="row">
    <div class="" style="min-height: 600px;">
        <div class="col-lg-4">
            <div class="alert alert-info" style="height: 477px; width: 100%;" role="alert">

                <div class="list-group">
                    <asp:Repeater ID="rptkitTypeConfiguration" OnItemDataBound="rptkitTypeConfiguration_ItemDataBound" runat="server">
                        <HeaderTemplate>
                            <div class="col-lg-12">
                                <asp:Label Text="Kit Types" Font-Size="20px" runat="server"></asp:Label>
                            </div>
                            </br>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <asp:CheckBox ID="chkKitType" Text=' <%# Eval("KitName") %>' runat="server" />
                                <asp:HiddenField ID="hKitID" runat="server" Value='<%# Eval("KitID") %>' />
                                <asp:HiddenField ID="hKitTenantID" runat="server" Value="0" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="row">
                                <asp:Button ID="btnUpdate" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" runat="server" />
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <br />
            </div>
        </div>
        <div class="col-lg-6">
        </div>
    </div>
</div>
