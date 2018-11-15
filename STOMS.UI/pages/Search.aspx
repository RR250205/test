<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="STOMS.UI.pages.Search" %>

<%@ MasterType VirtualPath="~/themes/mainMaster.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Src="~/usercontrol/SearchResult.ascx" TagPrefix="uc1" TagName="SearchResult" %>
    <style>
        .alert-info {
            background-color: #f9f9f9;
            border-color: #dee0e0;
        }
    </style>
    <div class="row" style="padding-left: 15px;">
        <%--<div class="col-lg-3">
            <div class="alert alert-info" style="height: 600px; width: 100%; margin-left: -12px; margin-top: -19px;" role="alert">

                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label Text="Filter Search" Font-Size="20px" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row rowDetail rowIndGreen col-md-5" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
            <br />
            No record found...
        </div>
        <div class="col-lg-12">
            <uc1:SearchResult runat="server" ID="ucSpecimen" IsLoad="false" />
        </div>

        <div class="col-lg-12" style="margin-top: 15px">
            <uc1:SearchResult runat="server" ID="ucAssay" IsLoad="false" />
        </div>
    </div>
    <asp:HiddenField ID="hSearchCount" Value="0" runat="server" />


</asp:Content>
