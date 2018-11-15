<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="order.aspx.cs" Inherits="STOMS.UI.pages.order" %>

<%@ Register Src="~/usercontrol/OrderList.ascx" TagPrefix="uc1" TagName="OrderList" %>
<%@ Register Src="~/usercontrol/search.ascx" TagPrefix="uc1" TagName="search" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-9">
            <uc1:OrderList runat="server" ID="OrderList" />
        </div>
        <div class="col-lg-3">
            <uc1:search runat="server" ID="search" />
        </div>
    </div>
</asp:Content>
