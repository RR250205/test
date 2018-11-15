<%@ Page Title="" Language="C#" MasterPageFile="~/mainMaster.Master" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="STOMS.UI.pages.OrderInfo" %>

<%@ Register Src="~/usercontrol/OrderDetail.ascx" TagPrefix="uc1" TagName="OrderDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:OrderDetail runat="server" id="OrderDetail" />
</asp:Content>
