<%@ Page Title="" Language="C#" MasterPageFile="~/DocMaster.Master" AutoEventWireup="true" CodeBehind="PrintInvoice.aspx.cs" Inherits="STOMS.UI.pages.PrintInvoice" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content" style="width:98%;">
        <div style="margin: 25px;">
            <div class="col-sm-12" style="margin:15px;">
                <asp:Literal ID="ltrInv" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
