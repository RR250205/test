<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveClientConfiguration.ascx.cs" Inherits="STOMS.UI.usercontrol.ActiveClientConfiguration" %>
<style>
    table, td, th {
    border: none;
}
</style>
<div class="row">     
            <div class="" style="min-height: 600px;">
                <div class="row">
                    <div class="col-lg-6 col-lg-offset-3">
                        <div class="row">
                            <div class="col-lg-6 col-lg-offset-3" style="color: rgb(12, 156, 18);font-size:16px">
                                <asp:Literal ID="ltrSuccessInformation" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-lg-offset-4" style="color: red;font-size:16px">
                                <asp:Literal ID="ltrErrorInformation" runat="server"></asp:Literal>
                            </div>
                        </div>                        
                        <div class="row">
                            <div class="col-lg-6 col-lg-offset-3">
                                <asp:RadioButtonList ID="rbtlActiveClient" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text="All Clients&nbsp;&nbsp;" Value="All Clients"></asp:ListItem>
                                    <asp:ListItem Text="Current Year&nbsp;&nbsp;" Value="Current Year"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-4 col-lg-offset-4">                                
                                <asp:Button ID="btnActiveClient" Text="Save" CssClass="btn btn-primary" runat="server" OnClick="btnActiveClient_Click" />                                
                            </div>
                        </div>
                        <br />
                        
                    </div>
                </div>                
            </div>  
    </div>
