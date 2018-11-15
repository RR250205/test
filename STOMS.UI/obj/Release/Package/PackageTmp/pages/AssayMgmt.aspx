<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="AssayMgmt.aspx.cs" Inherits="STOMS.UI.pages.AssayMgmt" %>

<%@ Register Src="~/usercontrol/AssayInfo.ascx" TagPrefix="uc1" TagName="AssayInfo" %>
<%@ Register Src="~/usercontrol/AssayListing.ascx" TagPrefix="uc1" TagName="AssayListing" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <div class="page-content">
        <%-- <div style="margin: 10px 10px 10px 0px;">  </div>--%>
        <div class="row">
            <div class="col-sm-9">
                <h4>Current Assay - Loading Specimen</h4>
                <uc1:AssayListing runat="server" ID="AssayLoading" AssayStatus="Current" />

                <h4>Assay - Ready For test</h4>
                 <uc1:AssayListing runat="server" ID="AssayReady" AssayStatus="Ready for Testing" />

                <h4>Assay - In testing</h4>
                <uc1:AssayListing runat="server" ID="AssayCurrent" AssayStatus="In testing" />

                <h4>Assay - Test Completed</h4>
                <uc1:AssayListing runat="server" ID="AssayTestComplete" AssayStatus="Test Completed" />
            </div>
            <div class="col-sm-3" style="background-color: #c3dcee;">
                <div style="padding: 3px; color: rgb(255, 255, 255); margin-right: -15px; margin-left: -15px; background-color: rgb(7, 0, 94);">
                    <div style="float: left; padding-left: 5px;">
                        <h4>Current Assay Samples</h4>
                    </div>
                    <div style="float: right; padding-top: 5px; padding-right: 3px;">
                        <%--<button class="btn btn-sm btn-danger" id="btnAssay" onclick="javascript:showAssay();"></button>--%>
                        <asp:Button ID="btnAssay" runat="server" Text="Assay Tracker" Visible="false" OnClick="btnAssay_Click" CssClass="btn btn-sm btn-danger" />
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div id="dvRPanel1">
                    <uc1:AssayInfo runat="server" ID="CurrentAssayInfo" />
                </div>
            </div>
        </div>

    </div>
    <script type="text/javascript">

        var eTop = $('#dvRPanel1').offset().top;

        $('#dvRec').height($(window).height() - 175);
        $('#dvAssay').height($(window).height() - 175);
        $('#dvResult').height($(window).height() - 175);
        $('#dvRPanel1').height($(window).height());

    </script>

</asp:Content>
