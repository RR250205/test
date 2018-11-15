<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.ascx.cs" Inherits="STOMS.UI.usercontrol.OrderDetail" %>
<%@ Register Src="~/usercontrol/PatSampleTestList.ascx" TagPrefix="uc1" TagName="PatSampleTestList" %>

<div id="page-content">
    <div style="margin: 10px 10px 10px 10px;">
        <div class="col-sm-12">
            <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px;">
                <div class="active" style="margin-left: 30px; margin-right:45px; margin-top: 20px;">
                    <div class="row">
                         <div style="float: left; margin-left:10px;">
                            <img src="/images/IliadLogo.png" />
                        </div>
                        <div style="float: right;">
                            5110 Campus Drive, Suite #190<br />
                            Plymouth Meeting, PA 19462<br />
                            P: 610-441-9050 * F: 610-537-5075<br />
                            W: www.iliadneuro.com * E: info@iliadneuro.com<br />
                        </div>
                    </div>
                    <hr />
                    <div class="row" id="dvHeader">
                        <div class="alert alert-info">
                            <div style="float: left">
                                <strong>Ordering Physician: </strong><br />
                                <asp:Literal ID="ltrCustName" runat="server"></asp:Literal><br />
                                <strong>Sample Count: </strong>
                                <asp:Literal ID="ltrSampleCount" runat="server"></asp:Literal>
                            </div>
                            <div style="float: right">
                                <strong>Order Number: </strong>&nbsp;
                                <asp:Literal ID="ltrOrderNo" runat="server"></asp:Literal><br />
                                <strong>Order Date: </strong>&nbsp;
                                <asp:Literal ID="ltrOrderDate" runat="server"></asp:Literal><br />
                                <strong>Status: </strong>&nbsp;
                                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                            </div>
                            <div style="clear: both"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="well" style="padding-left: 15px !important; margin-top: 15px; margin-right: 5px; margin-left: 15px;">
                            <uc1:PatSampleTestList runat="server" ID="PatSampleTestList1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
