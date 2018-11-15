<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exceptions.aspx.cs" 
    ValidateRequest = "false" MasterPageFile="~/themes/mainMaster.Master" Inherits="STOMS.UI.pages.Exceptions" %>
<%@ Register Src="~/usercontrol/SampleList.ascx" TagPrefix="uc1" TagName="SampleList" %>
<%@ Register Src="~/usercontrol/SampleListExpand.ascx" TagPrefix="uc1" TagName="SampleListExpand" %>
<%@ Register Src="~/usercontrol/AssayInfo.ascx" TagPrefix="uc1" TagName="AssayInfo" %>
<%@ Register Src="~/usercontrol/PendingSampleList.ascx" TagPrefix="uc1" TagName="PendingSampleList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <div class="">
        <div style="margin: 10px 10px 10px 10px;">
            <div class="row">
                <div class="col-sm-12">
                  <div class="row">
                      <%--<div class="col-md-1">
                          </div>
                        <div class="col-md-4">
                            <div class="bg-gray" style="min-height: 500px;" id="PendingStatus">
                                <div class="small-box bg-blue">
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampPendingCount" runat="server"></asp:Literal>
                                            - Pending</h4>
                                    </div>
                                </div>
                                <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 431px">
                                    <uc1:SampleList runat="server" ID="SampPending" SetStatus="Pending" />
                                </div>
                            </div>
                        </div>--%>
                      <%--<div class="col-md-1">
                          </div>--%>
                        <div class="col-md-12">
                            <div class="bg-gray" style="min-height: 500px;" id="Rejected">
                                <div class="small-box bg-red">
                                    <div class="inner">
                                        <h4 style="text-align:center">
                                            <asp:Literal ID="ltrSampRejectedCount" runat="server"></asp:Literal>
                                            - Rejected</h4>
                                    </div>
                                </div>
                                <div class="slimScrollDiv" style="position: relative; overflow: auto; width: auto; height: 431px">
                                    <uc1:SampleListExpand runat="server" ID="SampRejected" SetStatus="Rejected" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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

