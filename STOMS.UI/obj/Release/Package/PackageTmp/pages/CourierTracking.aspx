<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourierTracking.aspx.cs" Inherits="STOMS.UI.pages.CourierTracking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
    <link href="../assets/css/jquery-ui-1.10.3.full.min.css" rel="stylesheet" />
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/datepicker.css" rel="stylesheet" />
    <link href="../assets/css/ace.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">


        <div class="row">
            <div class="col-lg-12">

                <div id="navbar" class="navbar navbar-default          ace-save-state">
                    <div class="navbar-container ace-save-state" id="navbar-container">
                        <div class="navbar-header pull-left">
                             <a href="../pages/dashboard" class="navbar-brand">
                        <i class=" icon-beaker white"></i>
                        <span class="white">LIMS</span>
                    </a>
                        </div>
                    </div>
                </div>

                <div class="main-content">
                    <div class="main-content-inner" style="margin-right: 176px;">
                        <div class="page-content">
                            <div class="page-header">
                                <h3>Order Kit Tracking System</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <div id="page-content">
                    <div style="margin: 10px 10px 10px 10px;">
                        <div class="col-sm-12">
                            <div style="border: 1px solid #c5d0dc; margin-left: 167px; margin-right: 152px; background-color: #ffffff; margin-bottom: 10px;">
                                <div style="margin: 40px 30px 40px 30px; min-height: 500px;">


                                    <div class="row">

                                        <div class="col-lg-3">

                                            <div class="alert alert-info" style="height:477px;width:100%;" role="alert">

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label Text="Enter Tracking Number" Font-Size="20px" runat="server"></asp:Label><i style="color:red">*</i>
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtTrackingNumber" CssClass="form-control " runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="trkNumValidation" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtTrackingNumber" runat="server" ErrorMessage="Track Number Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-lg-12" style="margin-left: 106px;">
                                                        <asp:Button ID="btnTrack" ValidationGroup="trkNumValidation" OnClick="btnTrack_Click" CssClass="btn btn-success" Width="75px" Text="Track" Font-Size="Medium" runat="server" />
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-lg-9" >

                                            <div class="row">
                                                <div class="col-lg-12" style="background-color:orange">
                                                    <asp:Label runat="server"><h4 style="text-align:center">Tracking Details</h4></asp:Label>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="box col-lg-8" style="line-height:30px">
        <div class="box-header with-border">
          <h3 class="box-title"></h3>
          <div class="box-tools pull-right">
            <!-- Buttons, labels, and many other things can be placed here! -->
            <!-- Here is a label for example -->
            <%--<span class="label label-primary">Label</span>--%>
          </div><!-- /.box-tools -->
        </div><!-- /.box-header -->
        <div class="box-body">
         <asp:Literal ID="ltrTrackDetails" runat="server"></asp:Literal>
            </div><!-- /.box-body -->
        <div class="box-footer">
          <%--The footer of the box--%>
        </div><!-- box-footer -->
      </div><!-- /.box -->


                                            <asp:Repeater ID="rptTrackingDetails" runat="server">

                                                <HeaderTemplate>
                                                    <table cellspacing="0" rules="all" border="1">
                                                        <tr>
                                                            <th scope="col" style="width: 80px">Tracking Number
                                                            </th>
                                                            <th scope="col" style="width: 120px">Package Count
                                                            </th>
                                                            <th scope="col" style="width: 100px">Addres
                                                            </th>
                                                            <th scope="col" style="width: 100px">Status
                                                            </th>
                                                           
                                                        </tr>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTrackingNumber" runat="server" Text='<%# Eval("TrackingNumber") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("PackageCount") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipperCityState" runat="server" Text='<%# String.Concat( Eval("DestinationAddress.City"),", ", Eval("DestinationAddress.StateOrProvinceCode") ) %>' />
.C                                                          

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusDetail.Description") %>' />
                                                        </td>
                                                      
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>

                                            </asp:Repeater>

                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>

    </form>

    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../assets/js/jquery-2.0.3.min.js"></script>
    <script src="../assets/js/ace.min.js"></script>
</body>


</html>
