<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="PhysicionOrderList.aspx.cs" Inherits="STOMS.UI.pages.PhysicionOrderList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <link href="../style/css/select2/dist/css/select2.css" rel="stylesheet" />
    <link href="../style/js/plugins/datetimepicker/bootstrap/css/bootstrap-datetimepicker.css" rel="stylesheet" />

    <script src="../style/js/plugins/datetimepicker/jquery/jquery-1.8.3.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/bootstrap-datetimepicker.min.js"></script>
    <script src="../style/js/plugins/datetimepicker/jquery/locales/bootstrap-datetimepicker.fr.js"></script>

   <%-- <asp:Label Text="text" ID="lblid" runat="server" />--%>

    <div class="row">
        <div class="col-lg-10" >
             <div class="small-box bg-aqua" id="dvOrder" >
                                    <div class="inner">
                                        <h4>
                                            <asp:Literal ID="ltrSampReceiveCount" runat="server" Text=" Order List"></asp:Literal>
                                            </h4>
                                    </div>
                                </div>

             <asp:Repeater ID="rpPhyOrderList" runat="server" OnItemCommand="rpPhyOrderList_ItemCommand" OnItemDataBound="rpPhyOrderList_ItemDataBound">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row rowDetail rowIndGreen">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Ordert Number</label>
                        <br />
                        <asp:LinkButton CommandArgument='<%# Eval("OrderID") %>' CommandName="Detail" Text='<%# Eval("OrderNumber") %>' ID="lbtnOrderNumber" runat="server"></asp:LinkButton>
                    </div>
                    <div class="col-lg-3">
                        <label>Specimen Number</label>
                        <br />
                        <asp:Label ID="lblSpecimenNumber" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-3">
                        <label>Created On</label>
                        <br />
                        <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-3">
                        <label>Status</label>
                        <br />
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-1">
                        <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                        <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                        <asp:HiddenField id="hSpID" runat="server" Value="0" />
                    </div>
                </div>
                
                <%--<asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                <asp:Literal ID="ltrAssayStatus" runat="server"></asp:Literal>
                <br />
                <div style="color: #272626; display:inline-block">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                    </small>
                </div>
                <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                 <asp:HiddenField id="hSpID" runat="server" Value="0" />--%>
            </div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div class="row rowDetail rowIndOrange">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Ordert Number</label>
                        <br />
                         <asp:LinkButton CommandArgument='<%# Eval("OrderID") %>' CommandName="Detail" Text='<%# Eval("OrderNumber") %>' ID="lbtnOrderNumber" runat="server"></asp:LinkButton>
                    </div>
                    <div class="col-lg-3">
                        <label>Specimen Number</label>
                        <br />
                        <asp:Label ID="lblSpecimenNumber" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-3">
                        <label>Created On</label>
                        <br />
                        <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-3">
                        <label>Status</label>
                        <br />
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-1">
                        <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false" runat="server" />
                        <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                        <asp:HiddenField ID="hSpID" runat="server" Value="0" />
                    </div>
                </div>
                <%--<div class="row rowDetail rowIndOrange">
                <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                 <asp:Literal ID="ltrAssayStatus" runat="server" ></asp:Literal>
                <br />
                <div style="color: #272626;display:inline-block">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                    </small>
                </div>
                <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" data-toggle="modal" data-target="#model-sp-Confirm" 
                    CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                 <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                <asp:HiddenField id="hSpID" runat="server" Value="0" />
            </div>--%>
            </div>
        </AlternatingItemTemplate>
    </asp:Repeater>  

        </div>

    </div>

   

    <asp:HiddenField ID="hPhyCustID" runat="server" Value="0" />

</asp:Content>