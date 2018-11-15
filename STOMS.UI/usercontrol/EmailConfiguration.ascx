<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailConfiguration.ascx.cs" Inherits="STOMS.UI.usercontrol.EmailConfiguration" %>
<style>
    h1, .h1 {
        font-size: 23px;
        margin-left: 29px;
    }

    a {
        color: #272424;
    }

    .breadcrumb {
        padding: 8px 15px;
        margin-bottom: 20px;
        list-style: none;
        background-color: #f5f5f5;
        border-radius: 4px;
        top: 2px !important;
        float: right;
        margin-top: -47px;
    }
</style>

<div class="row">
    <div class="col-lg-11 col-md-11 col-sm-12 col-xs-12">
        <div class="">
            <div class="" style="min-height: 720px;">

                <div class="row">
                    <div class="col-md-8">
                        <div class="list-group">
                            <asp:Repeater runat="server" ID="rptEmailEnablementTypes" OnItemDataBound="rptEmailEnablementTypes_ItemDataBound" OnItemCommand="rptEmailEnablementTypes_ItemCommand">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="list-group-item" style="background-color: #5ac8ce; padding-right: 0px; padding-left: 0px; padding-bottom: 0px;">
                                        <h4 class="list-group-item-heading" style="padding-left: 20px; padding-bottom: 20px; padding-right: 10px;"><%# Eval("EmailEnablementType") %>
                                            <asp:LinkButton CssClass="btn btn-danger pull-right  btn-sm" Text="View Template" CommandName="ViewTemplate" runat="server" />
                                        </h4>
                                        <asp:HiddenField runat="server" Value='<%# Eval("EmailEnablementTypeID") %>' ID="hEmailEnablementTypeID" />
                                        <div class="list-group-item-text" style="background-color: #c9d3d3; padding-top: 10px;">
                                            <div class="row">
                                                <asp:CheckBox ID="chkToEndUser" CssClass="col-lg-3" runat="server" Text="To Enduser" OnCheckedChanged="chkToEndUser_CheckedChanged" AutoPostBack="true" />
                                                <asp:CheckBox ID="chkToTenant" CssClass="col-lg-3" runat="server" Text="To Tenant" OnCheckedChanged="chkToEndUser_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox runat="server" ToolTip="Add emails with ','" placholder="Add emails with ','" ID="txtToTenantEmails" CssClass=" col-lg-5" Visible="false" />
                                                <asp:LinkButton Style="float: right; margin-right: 10px; color: #009688" runat="server" ID="lbtnUpdate">
                                                        <i class="fa fa-arrow-circle-o-up fa-2" style="font-size:26px" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <span id="spError" runat="server" style="display: inline-block; color: red; padding-bottom: 25px;" visible="false"></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="col-md-4" id="dvEmailContent" runat="server" visible="false">
                        <div class="post" id="dvEndUserTempalte" runat="server">
                            <div class="user-block" style="font-size: large">                                
                                    <asp:Literal ID="ltrEmailEnablementType" runat="server" />
                                <br />
                                <b><span class="description">End user template</span></b>
                                <br />
                            </div>
                            <!-- /.user-block -->
                            <p>
                                <asp:Literal ID="ltrEndUserTemplate" runat="server" />
                            </p>

                            <%--<form class="form-horizontal">
                                <div class="form-group margin-bottom-none">
                                    <div class="col-sm-8">
                                        &nbsp;
                                    </div>
                                    <div class="col-sm-4">
                                        <button class="btn btn-danger pull-right btn-block btn-sm" disabled>Edit</button>
                                    </div>
                                </div>
                            </form>--%>
                        </div>
                        <div class="post" id="dvTenantTemplate" runat="server">
                            <div class="user-block" style="font-size: large">                                
                                    <asp:Literal ID="ltrEmailEnablementTypeTenant" runat="server" />
                                <br />
                                <b><span class="description">Tenant template</span></b>
                                <br />

                            </div>
                            <!-- /.user-block -->
                            <p>
                                <asp:Literal ID="ltrTenantTemplate" runat="server" />
                            </p>

                            <%--<form class="form-horizontal">
                                <div class="form-group margin-bottom-none">
                                    <div class="col-sm-8">
                                        &nbsp;
                                    </div>
                                    <div class="col-sm-4">
                                        <button class="btn btn-danger pull-right btn-block btn-sm" disabled>Edit</button>
                                    </div>
                                </div>
                            </form>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>

    function callAlert() {
        $(".alert .alert-success .dismissible").fadeTo(2000, 500).slideUp(500, function () {
            $("#success-alert").slideUp(500);
        });
    }
</script>



