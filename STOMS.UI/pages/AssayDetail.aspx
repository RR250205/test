<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="AssayDetail.aspx.cs" Inherits="STOMS.UI.pages.AssayDetail" %>

<%@ Register Src="~/usercontrol/AssayInfo.ascx" TagPrefix="uc1" TagName="AssayInfo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../style/css/bootstrap-toggle.min.css" rel="stylesheet" />
    <script src="https://s.codepen.io/assets/libs/modernizr.js" type="text/javascript"></script>
    
    <div class="pace  pace-inactive">
        <div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
            <div class="pace-progress-inner"></div>
        </div>
        <div class="pace-activity"></div>
    </div>
    <div class="page-content">
        <div class="row">
                <div class="col-lg-9">
                      <asp:TextBox ID="txtAssayNum" runat="server"  placeholder="Enter the Assay Number" style="height: 40px; text-align: center; width: 41%;"  ValidationGroup="vdgAssayNum"></asp:TextBox>
                      <asp:Button ID="btnAssayNum" CssClass="btn btn-success" runat="server" Text="View"  OnClick="btnAssayNum_Click" style="height:40px; text-align: center;width: 13%; margin-left: 24px; "  ValidationGroup="vdgAssayNum" />
                      <asp:RequiredFieldValidator ValidationGroup="vdgAssayNum" ID="reqAssayNum" ControlToValidate="txtAssayNum" runat="server" ForeColor="Red" errormessage="Please enter Assay Number!"></asp:RequiredFieldValidator>                     
                    <asp:label ID="lblAssayNum" CssClass="label" runat="server" Style="font-size:21px; color:red; margin-left:37px;"></asp:label>
                  <hr />  

                <div class="row" id="dvAssayDetails" runat="server">
                    <div class="alert alert-warning" style="color:red" id="dvError" runat="server" visible="false">
                        <span id="errorText" runat="server"></span>
                    </div>
                   
                    <div class="alert alert-success" style="margin-right: 10px;">
                        <div style="background-color: #ffffff; padding: 10px;">
                            <div class="row" style="margin-bottom: 10px;">
                                <div class="col-lg-4">
                                    <p class="p1">Assay #</p>
                                    <asp:Label ID="lblAssayNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <p class="p1">Test Start Date</p>
                                    <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;" runat="server" id="dvStartDate">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtStartDate" runat="server" style="margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker input120 GenInput"></asp:TextBox>
                                            <span class="input-group-addon" style="height:30px; width:30px;">
                                                <span class="fa fa-calendar" id="iconStartDate" ></span>
                                            </span>
                                        </div>
                                    </div>
                                    <asp:Literal ID="ltrTestStart" runat="server"></asp:Literal>&nbsp;
                                </div>
                                <div class="col-lg-2">
                                    <p class="p1">Specimen Count</p>
                                    <asp:Literal ID="ltrSpecimenCount" runat="server"></asp:Literal>&nbsp;
                                </div>
                                <div class="col-lg-2">
                                    <p class="p1">Assay Type</p>
                                    <asp:Literal ID="ltrAssayType" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <p class="p1">Description</p>
                                    <asp:Literal ID="ltrAssayDesc" runat="server"></asp:Literal>&nbsp;
                                </div>
                                <div class="col-lg-4">
                                    <p class="p1">Test Complete Date</p>
                                    <div class="row input-group" style="margin-bottom: 5px; margin-left: 2px;" runat="server" id="dvCompDate">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCompDate" runat="server" style="margin-top: 0px; margin-bottom: 0px;" CssClass="date-picker input120 GenInput"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="fa fa-calendar" id="iconCompDate"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <asp:Literal ID="ltrTestComplete" runat="server"></asp:Literal>&nbsp;
                                </div>
                                <div class="col-lg-4">
                                    <p class="p1">Assay Status</p>
                                    <asp:Literal ID="ltrAssayStatus" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-lg-offset-3" style="text-align:center;color:red">
                                    <asp:Literal ID="ltrError" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top: 20px;">
                            <asp:Button ID="btnUpdateAssay" runat="server"  OnClick="btnUpdateAssay_Click" CssClass="btn btn-primary" Text="Close & Load for Testing" />
                            <asp:Button ID="btnConformLoad" runat="server" CssClass="btn btn-warning" Text="Confirm & load" Visible="false" />
                        </div>
                        
                        <div class="col-lg-6">
                            <asp:Label ID="lblLoadForTestError" Text="Test Start date should be before the Complete date" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    
                    <h4>Specimen in the Assay</h4>
                    <div class="alert alert-warning" style="margin-right: 10px;">
                        <div style="margin-right: 15px;">
                            <asp:Repeater ID="rpSpecimenList" runat="server" OnItemDataBound="rpSpecimenList_ItemDataBound" OnItemCommand="rpSpecimenList_ItemCommand">
                                <HeaderTemplate>
                                    <div class="row" style="background-color: #32608d; color: #ffffff; padding-top: 9px; padding-bottom: 9px;">
                                        <div class="col-sm-2">Specimen #</div>
                                        <div class="col-sm-3">Patient Name</div>
                                        <div class="col-sm-2">Physician Name</div>
                                        <div class="col-sm-1" style="text-align: center">Type</div>
                                        <div class="col-sm-2" style="text-align: center">Binding</div>
                                        <div class="col-sm-2" style="text-align: center">Blocking</div>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row" style="background-color: #ffffff; border-bottom: solid #b7b6b6 1px; padding-top: 5px; padding-bottom: 5px;">
                                        <div class="col-sm-2">
                                            <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Literal ID="ltrPhyName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Literal ID="ltrSpecimenType" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2" style="text-align: center">
                                            <asp:Literal ID="ltrBIN" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2" style="text-align: right">
                                            <asp:Literal ID="ltrBL" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="row" style="background-color: #e0dfdf; border-bottom: solid #b7b6b6 1px; padding-top: 5px; padding-bottom: 5px;">
                                        <div class="col-sm-2">
                                            <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Literal ID="ltrPhyName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Literal ID="ltrSpecimenType" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2" style="text-align: center">
                                            <asp:Literal ID="ltrBIN" runat="server"></asp:Literal>
                                        </div>
                                        <div class="col-sm-2" style="text-align: right">
                                            <asp:Literal ID="ltrBL" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3" style="background-color: #c3dcee;" runat="server" id="dvCurrent" visible="false">                
                <div style="padding: 3px; color: rgb(255, 255, 255); margin-right: -15px; margin-left: -15px; background-color: rgb(7, 0, 94);">
                    <div style="float: left; padding-left: 5px;">
                        <h4>Current Assay Samples</h4>
                    </div>
                    <div style="float: right; padding-top: 5px; padding-right: 3px;">
                        <asp:Button ID="btnAssay" runat="server" Text="Assay Tracker" OnClick="btnAssay_Click" CssClass="btn btn-sm btn-danger" />
                    </div>
                    <div style="clear: both"></div>
                </div>

                <div id="dvRPanel11">
                    <uc1:AssayInfo runat="server" ID="CurrentAssayInfo" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hAssayID" runat="server" />
    <script type="text/javascript">
        var eTop = $('#dvRPanel11').offset().top;
        $('#dvRec').height($(window).height() - 175);
        $('#dvAssay').height($(window).height() - 175);
        $('#dvResult').height($(window).height() - 175);
        $('#dvRPanel1').height($(window).height());
    </script>
    <script type="text/javascript" src="../resource/js/form-elements.js"></script>
        <script type="text/javascript">
            $(function ()
            {

                $("#ContentPlaceHolder1_txtStartDate").datepicker({
                    changeMonth: true,
                    autoclose: true
                });

                $('#iconStartDate').click(function () {

                    //alert('clicked');
                    $('#ContentPlaceHolder1_txtStartDate').datepicker('show');
                });

                $("#ContentPlaceHolder1_txtCompDate").datepicker({
                    changeMonth: true,
                    autoclose: true
                });

                $('#iconCompDate').click(function () {

                    //alert('clicked');
                    $('#ContentPlaceHolder1_txtCompDate').datepicker('show');
                });


               $(document).ready(function () {
                    //this calculates values automatically 

                    $('#<%=btnUpdateAssay.Text=="Testing Complete" %>').on("focus", function () {

                        btnUpdateAssay();
                    });
                });

               function btnUpdateAssay_Click()
               {
                    // Get Variables
                    var txtStartDate = $('#<%=txtStartDate .ClientID %>').val();
                    var txtCompDate = $('#<%=txtCompDate .ClientID %> ').val();
           

                    txtStartDate = new Date(txtStartDate);
                    txtCompDate = new Date(txtCompDate);
                    var hours = (txtCompDate - txtStartDate) / 36e5;
                    console.log(hours.toFixed(2));            

                    if (hours < 0)
                    {
                         $('#<%=ltrError.ClientID %>').text("Test Start date should be before the Complete date");
                    }

                    else
                    {               
                        $('#<%=ltrError.ClientID %>').text("");
                    }
               }
         });
    </script>
</asp:Content>
