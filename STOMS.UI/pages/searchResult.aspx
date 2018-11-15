<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="searchResult.aspx.cs" Inherits="STOMS.UI.pages.searchResult" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/usercontrol/OrderList.ascx" TagPrefix="uc1" TagName="OrderList" %>
<%@ Register Src="~/usercontrol/search.ascx" TagPrefix="uc1" TagName="search" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div class="row">
            <div class="col-sm-12">
                <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px; min-height: 650px;">
                    <div style="margin: 10px 10px 10px 10px;">
                        <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                            <div class="col-sm-9">
                                <uc1:OrderList runat="server" ID="OrderList" />
                            </div>
                            <div class="col-sm-3">
                                <uc1:search runat="server" ID="search" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
