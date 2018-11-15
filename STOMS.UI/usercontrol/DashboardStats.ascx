<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardStats.ascx.cs" Inherits="STOMS.UI.usercontrol.DashboardStats" %>
<!-- Small boxes (Stat box) -->
<div class="row">
    <div class="col-lg-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-aqua">
            <div class="inner">
                <h3 style="font-weight:600;">
                    <asp:Literal ID="ltrOrderTotal" runat="server"></asp:Literal>
                </h3>
                <p style="font-size: 17px;font-weight:600;margin-bottom: 15px;">
                    Total Orders (Current Period)
                </p>
                <br>
                <br>
                <br>
            </div>
            <div class="icon">
                <i class="ion ion-bag"></i>
            </div>
            <a href="javascript:setAction('TO');" class="small-box-footer">More info
                <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-red">
          <div class="inner">
            <h3 style="font-weight:600;"><asp:Literal ID="ltrTestCount" runat="server"></asp:Literal></h3>
            <p style="margin-bottom: 2px;font-size: 20px;font-weight:600;">Specimen Tracking</p>
            <p style="margin-bottom: 2px;">Received : <span id="spReceiveSpec" style="font-size:14px;" runat="server"></span></p>
            <p style="margin-bottom: 2px;">Ready for Assay : <span id="spReadySpec" style="font-size:14px" runat="server"></span></p>
            <p style="margin-bottom: 2px;">Assign to Assay : <span id="spAssignSpec" style="font-size:14px" runat="server"></span></p>
        </div>
            <div class="icon">
                <i class="ion ion-stats-bars"></i>
            </div>
            <a href="javascript:setAction('TT');" class="small-box-footer">More info
                <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-yellow">
            <div class="inner">
                <h3 style="font-weight:600;"><asp:Literal ID="ltrCliCount" runat="server"></asp:Literal> </h3>
                <p style="font-size: 20px;font-weight:600;">No. of Clients</p>
                <br />
                <br />
                <br />
            </div>
            <div class="icon">
                <i class="ion ion-person-add"></i>
            </div>
            <a href="javascript:setAction('TC');" class="small-box-footer">More info
                <i class="fa fa-arrow-circle-right"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-green">
            <div class="inner">
                <h3 style="font-weight:600;">&nbsp; </h3>
                <p style="font-size: 20px;font-weight:600;">&nbsp;</p>
                <br>
                <br>
                <br>
            </div>
            <div class="icon">
                <i class="ion"></i>
            </div>
            <a class="small-box-footer">&nbsp;
                <i class="fa"></i>
            </a>
        </div>
    </div>
    <!-- ./col -->
</div>
    <!-- ./col -->
<!-- /.row -->
<asp:HiddenField ID="hAct" runat="server" />
<asp:LinkButton ID="lbtnAction" runat="server" Text="" OnClick="lbtnAction_Click"></asp:LinkButton>
<script>
    function setAction(val) {
        document.getElementById('<%= hAct.ClientID %>').value = val;
        __doPostBack('ctl00$ContentPlaceHolder1$DashboardStats1$lbtnAction', '');
    }
</script>