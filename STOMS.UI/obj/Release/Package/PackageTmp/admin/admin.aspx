<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="STOMS.UI.admin.admin" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/usercontrol/EmailConfiguration.ascx" TagPrefix="uc1" TagName="EmailConfiguration" %> 
<%@ Register src="~/usercontrol/CourierConfiguration.ascx" TagPrefix="uc1" TagName="CourierConfiguration" %>
<%@ Register src="~/usercontrol/KitTypeConfiguration.ascx" TagPrefix="uc1" TagName="KitTypeConfiguration" %>
<%@ Register src="~/usercontrol/ActiveClientConfiguration.ascx" TagPrefix="uc1" TagName="ActiveClientConfiguration" %>
<%@ Register Src="~/usercontrol/UserGroupMgmt.ascx" TagPrefix="uc1" TagName="UserGroupManagement" %>
<%@ Register Src="~/usercontrol/UserManagement.ascx" TagPrefix="uc1" TagName="UserManagement" %>
<%@ Register Src="~/usercontrol/TestProfileManagement.ascx" TagPrefix="uc1" TagName="TestProfileManagement" %>
<%--<%@ Register Src="~/usercontrol/CommonSettings.ascx" TagPrefix="uc1" TagName="CommonSettings" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="pace  pace-inactive"><div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
  <div class="pace-progress-inner"></div>
</div>
<div class="pace-activity"></div></div>
    
    <div id="page-content">        
       <style>
           .nav-tabs-custom {
               background: #eee;
           }
           .dvAdminTabs .nav-tabs-custom > .nav-tabs > li{
               border-left: 3px solid transparent;
    margin-bottom: -2px;
    margin-right: 5px;
           }
          .dvAdminTabs .nav-tabs-custom > .nav-tabs > li.active {
               border-left-color: #3c8dbc;
               border-top-color:white;
               border-top:none;
           }
       </style>
       
        <div id="dvSubMenu" class="dvAdminTabs" runat="server" visible="true">
            <div id="dvAdminTabs" class=" nav-tabs-custom">
                <ul class="nav nav-tabs" style="float: left;">
                    <li id="userManagementTab" style="float: none; margin: 0;" class="active" runat="server" clientidmode="static" onclick="showActiveTabPane('tabUserManagement')">
                        <a href="#tabUserManagement" data-toggle="tab" aria-expanded="true">User Management</a>
                    </li>
                    <li id="userGrpManagementTab" style="float: none; margin: 0;" runat="server" clientidmode="static" onclick="showActiveTabPane('tabUserGrpManagement')">
                        <a href="#tabUserGrpManagement" data-toggle="tab" aria-expanded="false">User Group Management</a>
                    </li>
                    <li id="emailConfigurationTab" style="float: none; margin: 0;" runat="server" clientidmode="static" onclick="showActiveTabPane('tabEmailConfiguration')">
                        <a href="#tabEmailConfiguration" data-toggle="tab" aria-expanded="false">Email Configuration</a>
                    </li>
                    <li id="courierConfigurationTab" style="float: none; margin: 0;" runat="server" clientidmode="static" onclick="showActiveTabPane('tabCourierConfiguration')">
                        <a href="#tabCourierConfiguration" data-toggle="tab" aria-expanded="false">Courier Configuration</a>
                    </li>
                    <li id="kitTypeConfigurationTab" style="float: none; margin: 0;" runat="server" clientidmode="static" onclick="showActiveTabPane('tabKitTypeConfiguration')">
                        <a href="#tabKitTypeConfiguration" data-toggle="tab" aria-expanded="false">Kit Type Configuration</a>
                    </li>
                    <li id="activeClientConfigurationTab" style="float: none; margin: 0;" runat="server" clientidmode="static" onclick="showActiveTabPane('tabActiveClientConfiguration')">
                        <a href="#tabActiveClientConfiguration" data-toggle="tab" aria-expanded="false">Active Client Configuration</a>
                    </li>
                    <li id="testProfileManagementTab" style="float: none; margin: 0; display:none" runat="server" clientidmode="static" onclick="showActiveTabPane('tabTestProfileManagement')">
                        <a href="#tabTestProfileManagement" data-toggle="tab" aria-expanded="false">Test Profile Management</a>
                    </li>
                    <%--<li id="commonSettingsTab" style="float: none; margin: 0; " runat="server" clientidmode="static" onclick="showActiveTabPane('tbaCommonSettings')">
                        <a href="#tbaCommonSettings" data-toggle="tab" aria-expanded="false">Common Settings</a>
                    </li>--%>
                </ul>

                <div class="tab-content">
                    <%--User Mgmt Tab--%>
                    <div class="tab-pane active" id="tabUserManagement" runat="server" clientidmode="static">
                        <uc1:UserManagement runat="server" id="ucUserManagement" />               
                    </div>

                    <%--User Group mgmt Tab--%>
                    <div class="tab-pane" id="tabUserGrpManagement" runat="server" clientidmode="static">
                        <uc1:UserGroupManagement runat="server" ID="ucUserGroupManagement" />
                    </div>

                    <%--Email Config Tab--%>
                    <div class="tab-pane" id="tabEmailConfiguration" runat="server" clientidmode="static">                        
                        <uc1:EmailConfiguration runat="server" ID="ucEmailConfiguration"  />
                    </div>

                    <%--Courier Config Tab--%>
                    <div class="tab-pane" id="tabCourierConfiguration" runat="server" clientidmode="static">
                        <uc1:CourierConfiguration runat="server" ID="ucCourierConfiguration" />         
                    </div>
                    <%--Kit type Config Tab--%>
                    <div class="tab-pane" id="tabKitTypeConfiguration" runat="server" clientidmode="static">
                        <uc1:KitTypeConfiguration runat="server" ID="ucKitTypeConfiguration" />
                    </div>

                    <%--Active client Config Tab--%>
                    <div class="tab-pane" id="tabActiveClientConfiguration" runat="server" clientidmode="static">
                        <uc1:ActiveClientConfiguration runat="server" ID="ucActiveClientConfiguration" />
                    </div>

                    <%--Test Profile Management Tab--%>
                    <div class="tab-pane" id="tabTestProfileManagement" runat="server" style="display:none" clientidmode="static">
                        <uc1:TestProfileManagement runat="server" ID="ucTestProfileManagement" />
                    </div>
                     <%--Common Settings--%>
                    <%--<div class="tab-pane" id="tbaCommonSettings" runat="server" style="" clientidmode="static">
                        <uc1:CommonSettings runat="server" ID="ucCommonSettings" />
                    </div>--%>
                </div>              
            </div>
        </div>

        <!-- Temp Permission-->
        <asp:Repeater runat="server" ID="rptPermissions">
            <HeaderTemplate>
                <table class="table">
                    <th>Function Name
                    </th>
                    <th>Read                 
                    </th>
                    <th>Write                 
                    </th>
                    <th>Execute                
                    </th>
                    <th>Export                 
                    </th>
                    <th>Role Specific                 
                    </th>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td>
                        <b><%# Eval("FunctionName") %></b>
                        <asp:HiddenField runat="server" ID="hFunID" Value='<%# Eval("FunctionID") %>' />
                    </td>
                    <div id="dvUpdateOptions" runat="server">
                        <td>
                            <asp:CheckBox ID="chkRead" runat="server" />
                        </td>
                        <td>writee
                        </td>
                        <td>execute
                        </td>
                        <td>export
                        </td>
                        <td>role specific
                        </td>
                    </div>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <!-- End Temp Permission-->

        <div style="margin: 10px;" runat="server" id="dvAdministaion" visible="false">
            <div class="col-sm-12">
                <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px;">
                    <div style="margin: 40px 30px 40px 30px; min-height: 500px;">
                        <ul class="nav nav-tabs" runat="server" id="nvtbAdmin">
                            <li class="active" runat="server" id="tbCust">
                                <a href="#cust-tab" data-toggle="tab">Customer Management</a>
                            </li>
                            <li runat="server" id="tbUser">
                                <a href="#user-tab" data-toggle="tab">User Administration</a>
                            </li>
                            <li runat="server" id="tbCalc">
                                <a href="#calc-tab" data-toggle="tab">Result Reference Scale</a>
                            </li>
                            <li runat="server" id="tbCountry">
                                <a href="#country-tab" data-toggle="tab">Reference Country</a>
                            </li>
                        </ul>
                        <div class="tab-content" style="margin: 5px 0px 10px 0px;">
                            <div id="cust-tab" class="tab-pane active">
                                <div class="alert alert-info" style="margin-left: 2px !important;">
                                    <div class="row">
                                        <div class="col-lg-10">
                                            <h4 class="header headline">
                                                <asp:Literal ID="ltrListCap" runat="server" Text="List of Customers"></asp:Literal></h4>
                                        </div>
                                        <div class="col-lg-2" style="margin-bottom: 5px !important;">
                                            <div style="float: right !important;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <telerik:RadGrid ID="tgrdCust" runat="server" AllowPaging="True" AllowSorting="True"
                                                ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                                                BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdCust_ItemDataBound">
                                                <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                                                    Width="100%">
                                                    <NoRecordsTemplate>
                                                        <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                                                            <center>No record to display</center>
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Client Name" />
                                                        <telerik:GridBoundColumn DataField="Address1" HeaderText="Address" />
                                                        <telerik:GridBoundColumn DataField="City" HeaderText="City" />
                                                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" />
                                                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <span runat="server" id="spOrderStatus"></span>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="120px">
                                                            <ItemTemplate>
                                                                <div class="hidden-phone visible-desktop btn-group">
                                                                    <button class="btn btn-info btn-sm" onclick="javascript:setAction('CE','<%# Eval("CustomerID") %>');return false;">
                                                                        <i class="fa fa-edit bigger-120"></i>
                                                                    </button>
                                                                    <button class="btn btn-danger btn-sm" onclick="javascript:setAction('CD','<%# Eval("CustomerID") %>');return false;">
                                                                        <i class="fa fa-recycle bigger-120"></i>
                                                                    </button>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="user-tab" class="tab-pane">
                                <div class="alert alert-info" style="margin-left: 2px !important;">
                                    <asp:PlaceHolder ID="phUserList" runat="server">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <h4 class="header headline">
                                                    <asp:Literal ID="Literal1" runat="server" Text="List of User Accounts"></asp:Literal></h4>
                                            </div>
                                            <div class="col-lg-2" style="margin-bottom: 5px !important;">
                                                <div style="float: right !important;">
                                                    <asp:Button ID="btnAddUser" runat="server" Text="New User" OnClick="btnAddUser_Click" CssClass="btn btn-info" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <telerik:RadGrid ID="tgrdUser" runat="server" AllowPaging="True" AllowSorting="True"
                                                    ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                                                    BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdUser_ItemDataBound">
                                                    <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                                                        Width="100%">
                                                        <NoRecordsTemplate>
                                                            <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                                                                <center>No record to display</center>
                                                            </div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" />
                                                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" />
                                                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" />
                                                            <telerik:GridBoundColumn DataField="LastLoginTime" HeaderText="Last Login" />
                                                            <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <span runat="server" id="spUserStatus"></span>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="140px">
                                                                <ItemTemplate>
                                                                    <div class="hidden-phone visible-desktop btn-group">
                                                                        <button class="btn btn-info btn-sm" onclick="javascript:setAction('UE','<%# Eval("AppUserID") %>');return false;">
                                                                            <i class="fa fa-edit bigger-120"></i>
                                                                        </button>
                                                                        <button class="btn btn-danger btn-sm" onclick="javascript:setAction('UD','<%# Eval("AppUserID") %>');return false;">
                                                                            <i class="fa fa-recycle bigger-120"></i>
                                                                        </button>
                                                                        <button class="btn btn-info btn-sm" onclick="javascript:setAction('UP','<%# Eval("AppUserID") %>');return false;">
                                                                            <i class="fa fa-key bigger-120"></i>
                                                                        </button>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="phUserAddEdit" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h4 class="header headline">Add / Edit User Account</h4>
                                                <asp:Button ID="btnUpdateUser" runat="server" Text="Save User" OnClick="btnUpdateUser_Click" CssClass="btn btn-info" />
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                </div>
                            </div>
                            <div id="calc-tab" class="tab-pane">
                                <div class="alert alert-info" style="margin-left: 2px !important;">
                                    <div class="row">
                                        <div class="col-lg-10">
                                            <h4 class="header headline">
                                                <asp:Literal ID="Literal2" runat="server" Text="Result reference scale"></asp:Literal></h4>
                                        </div>
                                        <div class="col-lg-2" style="margin-bottom: 5px !important;">
                                            <div style="float: right !important;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="background-color: #ffffff; margin-bottom: 5px; margin-right: 15px;">
                                        <div class="col-lg-4">
                                            <p style="margin-top: 3px; margin-bottom: 7px;">Normal</p>
                                            <div class="ui-slider ui-slider-horizontal ui-widget ui-widget-content ui-corner-all" id="slider1" aria-disabled="false">
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-range ui-widget-header ui-corner-all ui-slider-range-min" style="width: 25%"></div>
                                                <a class="ui-slider-handle ui-state-default ui-corner-all" style="left: 25%;" href="#"></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="background-color: #ffffff; margin-bottom: 5px; margin-right: 15px;">
                                        <div class="col-lg-4">
                                            <p style="margin-top: 3px; margin-bottom: 7px;">Borderline</p>
                                            <div class="ui-slider ui-slider-horizontal ui-widget ui-widget-content ui-corner-all" id="slider2" aria-disabled="false">
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-range ui-widget-header ui-corner-all ui-slider-range-min" style="width: 25%"></div>
                                                <a class="ui-slider-handle ui-state-default ui-corner-all" style="left: 25%;" href="#"></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="background-color: #ffffff; margin-bottom: 5px; margin-right: 15px;">
                                        <div class="col-lg-4">
                                            <p style="margin-top: 3px; margin-bottom: 7px;">Abnormal</p>
                                            <div class="ui-slider ui-slider-horizontal ui-widget ui-widget-content ui-corner-all" id="slider3" aria-disabled="false">
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment" style="margin-left: 25%;"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-segment"></div>
                                                <div class="ui-slider-range ui-widget-header ui-corner-all ui-slider-range-min" style="width: 25%"></div>
                                                <a class="ui-slider-handle ui-state-default ui-corner-all" style="left: 25%;" href="#"></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 10px;">
                                        <asp:Button ID="btnSaveCalc" runat="server" CssClass="btn btn-primary" Text="Save Thersholds" />
                                    </div>
                                </div>
                            </div>
                            <div id="country-tab" class="tab-pane">
                                <div class="alert alert-info" style="margin-left: 2px !important;">
                                    <div class="row">
                                        <div class="col-lg-10">
                                            <h4 class="header headline">
                                                <asp:Literal ID="Literal3" runat="server" Text="Refernce country"></asp:Literal></h4>
                                        </div>
                                        <div class="col-lg-2" style="margin-bottom: 5px !important;">
                                            <div style="float: right !important;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div style="margin: 10px 10px 10px 10px;">
                                            <h4 class="smaller lighter blue">
                                                <i class="icon-th-large"></i>&nbsp;Supported Country List
                                            </h4>
                                            <div id="dvMessage" class="alert alert-block alert-success" runat="server" style="margin-left: 10px; margin-right: 10px;" visible="false">
                                                <asp:Literal ID="ltrMsg" Text="Selected Country(s) Updated Successfully" runat="server"></asp:Literal>
                                            </div>
                                            <div style="margin-top: 25px; margin-left: 10px; margin-right: 30px;">
                                                <select multiple="" class="width-80 chosen-select" datatextfield="CountryName" datavaluefield="CountryID"
                                                    id="frmCountry" data-placeholder="Choose country to suport from the list below..." runat="server">
                                                </select>
                                            </div>
                                            <div style="margin-top: 15px; margin-left: 10px;">
                                                <asp:Button ID="btnSaveCountry" runat="server" Text="Save" OnClick="btnSaveCountry_Click" CssClass="btn btn-small btn-success" />&nbsp;&nbsp;
                                           
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
    <asp:HiddenField ID="hAct" runat="server" />
    <asp:HiddenField ID="hID" runat="server" />
    <asp:HiddenField ID="hActiveTab" runat="server" Value="" OnValueChanged="hActiveTab_ValueChanged" />
    <script src="../style/js/jquery.min.js"></script>
    <script type="text/javascript">
      

        function showActiveTabPane(ele) {
            console.log(ele)
            if (ele != '') {
                $('#<%= hActiveTab.ClientID %>').val(ele);
              //  __doPostBack();
            }
        }


        $(document).ready(function () {
            setHeight();
        });
        var tabsFn = (function () {

            function init() {
                setHeight();
            }

            function setHeight() {
                var $tabPane = $('.dvAdminTabs .tab-pane'),
                    tabsHeight = $('.dvAdminTabs .nav-tabs').height();
                tabWidth = $('.dvAdminTabs .nav-tabs').width();
                var tabContent = $('.dvAdminTabs .tab-content');
                var tabContentHeight = $('.tab-pane .active .tab-pane-content').height();
               
                $tabPane.css({
                    height: tabsHeight+800,
                   
                });
                tabContent.css({
                    'margin-left': tabWidth
                });
                var dvNewGroup_tabContent = $('.dvNewGroup .nav-tabs-custom .tab-content');
                dvNewGroup_tabContent.css({
                    'margin-left': 0,
                    'height': 700
                });
            }

            $(init);
        })();
        function setAction(act, ID) {

        }
        function setHeight() {
            var tabContentHeight = $('.tab-pane .active .tab-pane-content');
            var $tabPane = $('.dvAdminTabs .tab-pane');
            $tabPane.css({
                height: tabContentHeight,

            });
            console.log(tabContentHeight)
        }
    </script>
</asp:Content>
