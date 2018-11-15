<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="mysettings.aspx.cs" Inherits="STOMS.UI.pages.mysettings" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="page-content">
        <div class="page-header">
            <h1>My Settings
            </h1>
        </div>
        <h4 class="header smaller lighter blue"><i class="icon-anchor"></i>&nbsp;Shortcut Settings</h4>
        <div class="profile-user-info profile-user-info-striped">
            <div class="profile-info-row">
                <div class="profile-info-name">
                    Short cut 1
                    <button class="btn btn-sm btn-success" id="btnSC1">
                        <i class="icon-signal"></i>
                    </button>
                </div>
                <div class="profile-info-value">
                    <span class="editable" id="Span1">
                        <asp:DropDownList ID="ddSC1" runat="server">
                        </asp:DropDownList>
                    </span>
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">
                    Short cut 2
                    <button class="btn btn-sm btn-info" runat="server" id="btnSC2">
                        <i class="icon-pencil"></i>
                    </button>
                </div>
                <div class="profile-info-value">
                    <span class="editable" id="Span2">
                        <asp:DropDownList ID="ddSC2" runat="server">
                        </asp:DropDownList>
                    </span>
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">
                    Short cut 3
                    <button class="btn btn-sm btn-warning" runat="server" id="btnSC3">
                        <i class="icon-group"></i>
                    </button>
                </div>
                <div class="profile-info-value">
                    <span class="editable" id="Span3">
                        <asp:DropDownList ID="ddSC3" runat="server">
                        </asp:DropDownList>
                    </span>
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">
                    Short cut 4
                    <button class="btn btn-sm btn-danger" runat="server" id="Button3">
                        <i class="icon-signal"></i>
                    </button>
                </div>
                <div class="profile-info-value">
                    <span class="editable" id="Span4">
                        <asp:DropDownList ID="ddSC4" runat="server">
                        </asp:DropDownList>
                    </span>
                </div>
            </div>
        </div>
        <div style="margin-top: 15px; margin-left: 10px;">
            <button runat="server" id="btnCSubject" class="btn btn-small btn-success" onclick="javascript:addAction('SC');return false;">
                Update<i class="icon-arrow-right icon-on-right bigger-110"></i>
            </button>
        </div>
<%--        <div class="vspace-8"></div>
        <h4 class="header smaller lighter blue"><i class="icon-building"></i>&nbsp;Default Entity</h4>
        <div class="profile-user-info profile-user-info-striped">
            <div class="profile-info-row">
                <div class="profile-info-name">Default Entity</div>
                <div class="profile-info-value">
                    <asp:DropDownList ID="ddEntity" runat="server" DataTextField="EntityName" DataValueField="EntityID">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div style="margin-top: 15px; margin-left: 10px;">
            <button runat="server" id="Button1" class="btn btn-small btn-success" onclick="javascript:addAction('SB');return false;">
                Update<i class="icon-arrow-right icon-on-right bigger-110"></i>
            </button>
        </div>--%>

        <asp:LinkButton ID="lbtnGridAction" runat="server" Text="" OnClick="lbtnGridAction_Click"></asp:LinkButton>
        <asp:HiddenField ID="hActionType" runat="server" OnValueChanged="hActionType_ValueChanged" />
        <script type="text/javascript">
            function addAction(s) {
                <%--document.getElementById('<%= hActionType.ClientID %>').value = s;
                __doPostBack('ctl00$ContentPlaceHolder1$lbtnGridAction', '');--%>

                document.getElementById('<%= hActionType.ClientID %>').value = s;
                var hValueID = "<%= hActionType.ClientID %>";
                __doPostBack(hValueID, "");
                return false;
            }
        </script>
    </div>
</asp:Content>
