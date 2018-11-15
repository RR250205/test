<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicePrint.ascx.cs" Inherits="STOMS.UI.usercontrol.InvoicePrint" %>
<!-- Begin: Invoice format -->
<div class="row">
    <div class="col-xs-12">
        <div class="space-6"></div>
        <div class="row">
            <div class="col-sm-10 col-sm-offset-1">
                <div class="widget-box transparent invoice-box">
                    <div class="widget-body">
                        <div class="widget-main padding-24">
                            <div class="row" style="margin-left: 10px; margin-top: 10px;">
                                <div style="float: left; margin-left: 10px;">
                                    <img src="/images/IliadLogo.png" />
                                </div>
                                <div style="float: right; margin-right: 20px;">
                                    5110 Campus Drive, Suite #190<br />
                                    Plymouth Meeting, PA 19462<br />
                                    P: 610-441-9050 * F: 610-537-5075<br />
                                    W: www.iliadneuro.com * E: info@iliadneuro.com<br />
                                </div>
                            </div>
                            <hr />
                            <div class="row fc-header-center">
                                <h4><strong>Invoice</strong></h4>
                            </div>
                            <div class="row">
                                <div style="float: left; margin-left: 10px;">
                                    <strong>Customer</strong><br />
                                    <asp:Literal ID="ltrInvoiceTo" runat="server"></asp:Literal><br />
                                    <strong>Reference Order #:</strong>
                                </div>
                                <div style="float: right; margin-right: 20px;">
                                    <p>
                                        <strong>Invoice #: </strong>
                                        <asp:Literal ID="ltrInvNo" runat="server"></asp:Literal><br />
                                        <strong>Date: </strong>
                                        <asp:Literal ID="ltrInvDate" runat="server"></asp:Literal><br />
                                    </p>
                                </div>
                            </div>
                            <div class="space"></div>
                            <div>
                                <table class="table table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th class="center">#</th>
                                            <th class="hidden-xs">Service Description</th>
                                            <th>No. of Units</th>
                                            <th>Unit Cost</th>
                                            <th class="hidden-480">Discount</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="center">1</td>
                                            <td class="hidden-xs">Testing for Folate Receptor A Binding Antibodies &
                                                                  Folate Receptor A Binding Antibodies
                                            </td>
                                            <td style="text-align:center;"><asp:Literal ID="ltrUnit" runat="server"></asp:Literal></td>
                                            <td style="text-align:right;"><asp:Literal ID="ltrUnitCost" runat="server"></asp:Literal></td>
                                            <td class="hidden-480">--- </td>
                                            <td style="text-align:right;"><asp:Literal ID="ltrItemCost" runat="server"></asp:Literal></td>
                                        </tr>
                                        <%--<tr>
                                            <td class="center">2</td>
                                            <td>
                                                <a href="#">yahoo.com</a>
                                            </td>
                                            <td class="hidden-xs">5 year domain registration
                                            </td>
                                            <td class="hidden-480">5% </td>
                                            <td>$45</td>
                                        </tr>

                                        <tr>
                                            <td class="center">3</td>
                                            <td>Hosting</td>
                                            <td class="hidden-xs">1 year basic hosting
                                            </td>
                                            <td class="hidden-480">10% </td>
                                            <td>$90</td>
                                        </tr>

                                        <tr>
                                            <td class="center">4</td>
                                            <td>Design</td>
                                            <td class="hidden-xs">Theme customization
                                            </td>
                                            <td class="hidden-480">50% </td>
                                            <td>$250</td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                            <div class="hr hr8 hr-double hr-dotted"></div>
                            <div class="row">
                                <div class="col-sm-5 pull-right">
                                    <h4 class="pull-right">Total amount :<span class="red"><asp:Literal ID="ltrTotalCost" runat="server"></asp:Literal></span>
                                    </h4>
                                </div>
                                <div class="col-sm-7 pull-left">Terms: <asp:Literal ID="ltrInvTerm" Text="Payment upon receipt" runat="server"></asp:Literal></div>
                            </div>
                            <div class="space-6"></div>
                            <div class="well">
                                Thank you for choosing Iliyad Testing Facilities. We believe you will be satisfied by our services.
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- End: Invoice format -->
