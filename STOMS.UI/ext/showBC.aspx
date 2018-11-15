<%@ Page Title="" Language="C#" MasterPageFile="~/blankMaster.Master" AutoEventWireup="true" CodeBehind="showBC.aspx.cs" Inherits="STOMS.UI.ext.showBC" %>
<%@ Register Src="~/usercontrol/barCode.ascx" TagPrefix="uc1" TagName="barCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:barCode runat="server" ID="barCode" />
</asp:Content>
