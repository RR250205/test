<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="myprofile.aspx.cs" Inherits="STOMS.UI.pages.myprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div class="col-sm-12">
            <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px; min-height: 650px;">
                <div style="margin: 10px 10px 10px 10px;">
                    <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                        <div class="col-sm-9">
                            <div style="padding-left: 20px !important; margin-right: 5px; margin-top: 15px;">
                                <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 30px;">
                                    <strong>My Profile</strong>
                                </div>
                                <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="GenInput input420"></asp:TextBox>
                                </div>
                                <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                    <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" CssClass="GenInput input420"></asp:TextBox>
                                </div>
                                <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                    <asp:TextBox ID="txtMobileNumber" runat="server" placeholder="Contact Number" CssClass="GenInput input420"></asp:TextBox>
                                </div>
                                <div class="row" style="margin-top: 15px; margin-bottom: 15px; margin-left:15px;">
                                    <asp:Button ID="btnSaveProfile" runat="server" Text="Save My Info" CssClass="btn btn-primary" OnClick="btnSaveProfile_Click" />
                                    &nbsp;&nbsp;<asp:Literal ID="ltrMsg1" runat="server"></asp:Literal>
                                </div>
                                 <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-top:15px; margin-right: 30px;">
                                    <strong>Change Password</strong>
                                </div>
                                <div class="row" style="margin-top: 15px; margin-left: 10px; margin-bottom: 5px;">
                                    <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" placeholder="New Password" CssClass="GenInput input420"></asp:TextBox>
                                </div>
                                <div class="row" style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
                                    <asp:TextBox ID="txtConfirmNewPassword" TextMode="Password" runat="server" placeholder="Confirm New Password" CssClass="GenInput input420"></asp:TextBox>
                                </div>
                                <div style="margin-top: 15px; margin-bottom: 15px; margin-left:15px;">
                                    <asp:Button ID="btnChangePass" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnChangePass_Click" />
                                    &nbsp;&nbsp;<asp:Literal ID="ltrMsg2" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
