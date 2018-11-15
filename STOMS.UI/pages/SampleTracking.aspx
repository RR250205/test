<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="SampleTracking.aspx.cs" Inherits="STOMS.UI.pages.SampleTracking" %>

<%@ Register Src="~/usercontrol/SampleList.ascx" TagPrefix="uc1" TagName="SampleList" %>
<%@ Register Src="~/usercontrol/AssayInfo.ascx" TagPrefix="uc1" TagName="AssayInfo" %>
<%@ Register Src="~/usercontrol/PendingSampleList.ascx" TagPrefix="uc1" TagName="PendingSampleList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <%-- <asp:DropDownList display="Dynamic"  style="width: 160px;
    float: right;
    margin-right: 20px; height:40px;" ID="ddlspecimen" runat="server" AutoPostBack="True"
        Font-Size="10pt" Height="17px"
        OnSelectedIndexChanged="ddlspecimen_SelectedIndexChanged" Width="162px">
        <asp:ListItem Text="Received Specimens" Value="SampReceive"></asp:ListItem>
        <asp:ListItem Text="Ready for Assay" Value="SampResult"></asp:ListItem>
        <asp:ListItem Text="Assigned to Assay" Value="SampAssay"></asp:ListItem>
    </asp:DropDownList>--%>

    <div style="margin: 0px 10px 10px 10px;">
        <div class="row">
            <div class="col-sm-12">
                <div class="row">
                    <div class="" style="margin: 30px 10px 10px 10px;" runat="server" id="dvSampReceive" visible="true">
                        <div class="col-md-12">
                            <%--<div class="bg-gray" style="min-height: 500px;" id="dvRec">--%>
                            <div class="bg-gray">
                                <%--<div class="small-box bg-aqua">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampReceiveCount" runat="server"></asp:Literal>
                                            - Received Specimens</h4>
                                    </div>
                                </div>--%>

                                <uc1:SampleList runat="server" ID="SampReceive" SetStatus="Received" IsLoad="false" />

                            </div>
                        </div>
                    </div>
                    <div class="" style="margin: 30px 10px 10px 10px;" runat="server" id="dvSampResult" visible="false">
                        <div class="col-md-12">
                            <%-- <div class="bg-gray" style="min-height: 500px;" id="dvResult">--%>
                            <div class="bg-gray">
                                <%--<div class="small-box bg-green">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrReadyforAssay" runat="server"></asp:Literal>
                                            - Ready for Assay</h4>
                                    </div>
                                </div>--%>

                                <uc1:SampleList runat="server" ID="SampResult" SetStatus="Result Recorded" />

                            </div>
                        </div>
                    </div>
                    <div class="" style="margin: 30px 10px 10px 10px;" runat="server" id="dvSampAssay" visible="false">
                        <div class="col-md-12">
                            <%-- <div class="bg-gray" style="min-height: 500px;" id="dvAssay"> --%>
                            <div class="bg-gray">
                                <%--<div class="small-box bg-yellow">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampAssayCount" runat="server"></asp:Literal>
                                            - Assigned to Assay</h4>
                                    </div>
                                </div>--%>

                                <uc1:SampleList runat="server" ID="SampAssay" SetStatus="Assigned to Assay" />

                            </div>
                        </div>
                    </div>


                </div>
                <br />

                <%--<div class="row">
                     
                        <div class="col-md-4">
                            <div class="bg-gray" style="min-height: 500px;" id="PendingStatus">
                                <div class="small-box bg-blue">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampPendingCount" runat="server"></asp:Literal>
                                            - Pending</h4>
                                    </div>
                                </div>
                                <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 380px">
                                    <uc1:SampleList runat="server" ID="SampPending" SetStatus="Pending" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="bg-gray" style="min-height: 500px;" id="Rejected">
                                <div class="small-box bg-red">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampRejectedCount" runat="server"></asp:Literal>
                                            - Rejected</h4>
                                    </div>
                                </div>
                                <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 380px">
                                    <uc1:SampleList runat="server" ID="SampRejected" SetStatus="Rejected" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
            </div>
            <%-- <div class="col-sm-3" style="background-color: #c3dcee;">
                    <div style="padding: 3px; color: rgb(255, 255, 255); margin-right: -15px; margin-left: -15px; background-color: rgb(7, 0, 94);">
                        <div style="float: left; padding-left: 5px;">
                            <h4>Current Assay Samples</h4>
                        </div>
                        <div style="float: right; padding-top: 5px; padding-right: 3px;">
                           
                            <asp:Button ID="btnAssay" runat="server" Text="Assay Tracker" OnClick="btnAssay_Click" CssClass="btn btn-sm btn-danger" />
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <div id="dvRPanel2" style="height: 500px; overflow: auto">
                        <uc1:AssayInfo runat="server" ID="CurrentAssayInfo" />
                    </div>
                </div>--%>
        </div>
    </div>
    </div>
    <script type="text/javascript">

        //var eTop = $('#dvRPanel1').offset().top;

        $('#dvRec').height($(window).height() - 175);
        $('#dvAssay').height($(window).height() - 175);
        $('#dvResult').height($(window).height() - 175);
        //$('#dvRPanel2').height($(window).height() - 175);

    </script>
</asp:Content>
