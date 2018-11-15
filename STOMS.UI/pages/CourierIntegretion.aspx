<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="CourierIntegretion.aspx.cs" Inherits="STOMS.UI.pages.CourierIntegretion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-3">
             <b><asp:Literal ID="ltrName" runat="server"></asp:Literal></b><br />
            <asp:Literal ID="ltrAddress1" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltrCity" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltrState" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltrCountry" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltrZip" runat="server"></asp:Literal><br />
            <asp:Literal ID="ltrTelephone" runat="server"></asp:Literal><br />
            </div>
    </div>

</asp:Content>