<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ReportList.aspx.cs" Inherits="STOMS.UI.reports.ReportList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>    

    <style>
        .a td span {
            padding-left: 10px;
        }

        a
        {
            padding-left:10px;            
        }
    </style>
   
    <div id="page-content">
        <div class="col-sm-12">
            <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px; min-height: 650px;">
                <div style="margin: 10px 10px 10px 10px;">
                    <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                        <div class="col-sm-12">
                            <div class="well" style="padding-left: 20px !important; margin-right: 5px; margin-top: 15px; min-height: 200px;">                                

                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs">
                                        <li id="specimenTab" class="active" runat="server" clientidmode="static">
                                            <a href="#tabSpecimenReporting" data-toggle="tab" aria-expanded="true">Specimen Reporting</a>
                                        </li>
                                        <li id="ActiveClientTab" runat="server" clientidmode="static">
                                            <a href="#tabActiveClients" data-toggle="tab" aria-expanded="false">Active Clients</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content">

                                        <%--Specimen reporting Tab--%>
                                        <div class="tab-pane active" id="tabSpecimenReporting" runat="server" clientidmode="static">

                                            <%-- Heading--%>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 style="text-align: center">Specimen Reporting details</h3>
                                                </div>
                                            </div>

                                            <%-- Date --%>
                                            <div class="row">
                                                <div class="col-lg-4 col-lg-offset-4">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <label>From Date</label>
                                                        </div>
                                                        <div class="col-lg-9 input-group">
                                                            <asp:TextBox ID="txtFromDate" Width="101%" CssClass="date-picker GenInput" placeHolder="From Date" Style="margin-top: 0px; margin-bottom: 0px" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar" id="calFromDate"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <label>To Date</label>
                                                        </div>
                                                        <div class="col-lg-9 input-group">
                                                            <asp:TextBox ID="txtToDate" Width="101%" CssClass="date-picker GenInput" placeHolder="To Date" Style="margin-top: 0px; margin-bottom: 0px" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar" id="calToDate"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />

                                            <%--Status--%>
                                            <div class="row">
                                                <div class="col-lg-4 col-lg-offset-4">
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <label>Types Of Status</label>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <asp:DropDownList Style="width: 131%; margin-left: -40px; height: 32px;" ID="ddlStatus" runat="server">
                                                                <asp:ListItem disabled="disabled" Value="0">Select Status</asp:ListItem>
                                                                 <asp:ListItem Text="Received" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Assigned to Assay" Value="1"></asp:ListItem>
                                                                 <asp:ListItem Text="Result Recorded" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="Ready for Assay" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Rejected" Value="4"></asp:ListItem>                                                               
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />

                                            <%--Report Buttons--%>
                                            <div class="row">
                                                <div class="col-lg-6 col-lg-offset-3">
                                                    <div class="col-lg-6">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Button ID="btnGetSpecimenReport" CssClass="btn btn-success" Text="Get Report" Style="float: right" OnClick="btnGetSpecimenReport_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Button ID="btnSpecimenExportToExcel" Visible="false" CssClass="btn btn-info" Text="Export To Excel" OnClick="btnSpecimenExportToExcel_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />

                                            <%--Error Report message--%>
                                            <div class="row">
                                                <div class="col-lg-4 col-lg-offset-4">
                                                    <asp:Label ID="lblReport" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <%--Specimen Report in Gridview--%>
                                            <div class="a">
                                                <asp:GridView CssClass="table table-striped table-bordered" PagerStyle-CssClass="table" PagerStyle-Font-Size="12" PageSize="10" ID="gdvwSpecimenReport" PagerStyle-HorizontalAlign="Right" BackColor="white" AutoGenerateColumns="false" OnPageIndexChanging="gdvwSpecimenReport_PageIndexChanging" AllowPaging="true" runat="server">
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="specimenBO.SpecimenNumber" HeaderText="Specimen Number" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="specimenBO.SpecimentType" HeaderText="Specimen Type" />
                                                        <%--<asp:BoundField ItemStyle-Width="150px" DataField="patientBO.PatientName" HeaderText="Patient Name" />--%>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="customerBO.CustomerName" HeaderText="Physician Name" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="patientBO.Gender" HeaderText="Gender" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="specimenBO.SpecimenStatus" HeaderText="Specimen Status" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <%--Active Clients Tab--%>
                                        <div class="tab-pane" id="tabActiveClients" runat="server" clientidmode="static">
                                            <div class="row">
                                                <div class="col-lg-12" style="text-align: center">
                                                    <h3>Active Clients Report</h3>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-lg-4 col-lg-offset-4">
                                                    <div class="col-lg-6">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Button ID="btnGetActiveclient" CssClass="btn btn-success" Text="Get Active Client" OnClick="btnGetActiveclient_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Button ID="btnActiveExportToExcel" Visible="false" CssClass="btn btn-info" Text="Export To Excel" OnClick="btnActiveExportToExcel_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-6" style="text-align: right; color: blueviolet;">
                                                    <b><asp:Literal ID="ltrTotalClients" Text="Total Active Clients" Visible="false" runat="server"></asp:Literal></b>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:Literal ID="ltrTotalCount" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <br />
                                            <%--Active Client Report in Gridview--%>
                                            <div class="a">
                                                <asp:GridView CssClass="table table-striped table-bordered" PagerStyle-CssClass="table" ID="gdvwActiveClient" PagerStyle-HorizontalAlign="Right" PagerStyle-Font-Size="12" PageSize="10" BackColor="white" AutoGenerateColumns="false" OnPageIndexChanging="gdvwActiveClient_PageIndexChanging" AllowPaging="true" runat="server">
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="FirstName" HeaderText="Client Name" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Phone" HeaderText="Contact Number" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email ID" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="RequesterType" HeaderText="Requester Type" />
                                                        <asp:BoundField ItemStyle-Width="200px" DataField="FullAddress" HeaderText="Address" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--<div class="col-sm-3">
                            <div class="alert alert-success" style="padding-left: 20px !important; margin-top: 15px; min-height: 300px;">
                                <h4 style="height: 35px; border-bottom: solid 1px; margin-bottom: 30px;">Stats</h4>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {

            $("#ContentPlaceHolder1_txtFromDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+0",
                autoclose: true
            });

            $('#calFromDate').click(function () {

                //alert('clicked');
                $('#ContentPlaceHolder1_txtFromDate').datepicker('show');
            });

            $("#ContentPlaceHolder1_txtToDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-120:+0",
                autoclose: true
            });

            $('#calToDate').click(function () {

                //alert('clicked');
                $('#ContentPlaceHolder1_txtToDate').datepicker('show');
            });

        });
    </script>
</asp:Content>
