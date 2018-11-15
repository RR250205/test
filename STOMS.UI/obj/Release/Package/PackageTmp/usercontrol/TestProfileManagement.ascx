<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestProfileManagement.ascx.cs" Inherits="STOMS.UI.usercontrol.TestProfileManagement" %>
<div class="row">
    <div style="min-height: 600px;">
        <div class="col-lg-4">
            <div class="alert alert-info" style="height: 477px; width: 100%;" role="alert">
                <h4>Test Profiles</h4>
                <div class="row">
                    <asp:CheckBoxList ID="chklstTestProfiles" AutoPostBack="true" OnSelectedIndexChanged="chklstTestProfiles_SelectedIndexChanged" runat="server">
                        <asp:ListItem Text="&nbsp;Frat Test" Value="t001"></asp:ListItem>
                        <asp:ListItem Text="&nbsp;Swab Test" Value="t002"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
        </div>
        <div class="col-lg-8">
        </div>
    </div>
</div>