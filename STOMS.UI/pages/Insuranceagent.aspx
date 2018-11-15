<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insuranceagent.aspx.cs" MasterPageFile="~/themes/mainMaster.Master" Inherits="STOMS.UI.pages.Insuranceagent" %>

<%@ Register Src="~/UserControl/DashboardStats.ascx" TagName="DashboardStats" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/OrderList.ascx" TagPrefix="uc1" TagName="OrderList" %>
<%@ Register Src="~/usercontrol/search.ascx" TagPrefix="uc1" TagName="search" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../style/js/plugins/morris.js/morris.css" rel="stylesheet" />
     <%--<uc1:DashboardStats ID="DashboardStats1" runat="server" />--%>
      <div class="row">
    <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-aqua" >
            <div class="inner">
                <h3>
                    <asp:Literal ID="ltrOrderTotal" runat="server"></asp:Literal>
                </h3>
                <p>
                    Total Orders (Current Period)   
                </p>
                <br />
                <br />
                <br />
            </div>
            <div class="icon">
                <i class="ion ion-bag"></i>
            </div>
            <a href="javascript:setAction('TO');" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
   <%-- <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-red" >
            <div class="inner">
                <h3><asp:Literal ID="ltrTestCount" runat="server"></asp:Literal> 
                </h3>
                <p>Sample in Testing</p>
                 <br />
                <br />
                <br />
            </div>
            <div class="icon">
                <i class="ion ion-stats-bars"></i>
            </div>
            <a href="javascript:setAction('TT');" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>--%>
    <!-- ./col -->
    <div class="col-lg-4 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-yellow" >
            <div class="inner">
                <h3><asp:Literal ID="ltrCliCount" runat="server"></asp:Literal> </h3>
                <p>No. of Clients</p>
                 <br />
                <br />
                <br />
            </div>
            <div class="icon">
                <i class="ion ion-person-add"></i>
            </div>
            <a href="javascript:setAction('TC');" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
    <%--<div class="col-lg-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-green">
            <div class="inner">
                <h3><asp:Literal ID="ltrInvCount" runat="server"></asp:Literal></h3>
                <p>Outstanding Invoices</p>
            </div>
            <div class="icon">
                <i class="ion ion-pie-graph"></i>
            </div>
            <a href="javascript:setAction('OI');" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>--%>
    <!-- ./col -->
</div>
<!-- /.row -->
<asp:HiddenField ID="hAct" runat="server" />
<asp:LinkButton ID="lbtnAction" runat="server" Text="" OnClick="lbtnAction_Click"></asp:LinkButton>
<script>
    function setAction(val) {
        document.getElementById('<%= hAct.ClientID %>').value = val;
        __doPostBack('ctl00$ContentPlaceHolder1$DashboardStats1$lbtnAction', '');
    }
</script>
  
</asp:Content>



