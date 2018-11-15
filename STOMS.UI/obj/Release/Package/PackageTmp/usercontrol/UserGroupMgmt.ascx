<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserGroupMgmt.ascx.cs" Inherits="STOMS.UI.usercontrol.UserGroupMgmt" %>

<div id="page-content">
    <style>
        .nav-tabs-custom {
            background: #eee;
        }

        .dvNewGroup .nav-tabs-custom > .nav-tabs > li.active {
            border-top-color: #3c8dbc !important;
            border-left: none;
        }

        .a td span {
            padding-left: 0px;
        }

        a {
            padding-left: 0px;
        }
    </style>

    <script src="../style/js/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../style/css/select2/dist/css/select2.css" rel="stylesheet" />

    <div class="col-sm-12 tab-pane-content">
        <div class="row" style="padding: 0px; vertical-align: middle;">
            <div class="col-sm-12">
                <div class="">
                    <div class="row">
                        <div>
                            <asp:Label ID="lblGroupName" style="font-size: 20px;font-weight:400;padding-left:11px"  Text="" runat="server"></asp:Label>
                            <asp:Button CssClass="btn btn-warning" Style="float: right" ID="btnNewGroup" Text="New Group" OnClick="btnNewGroup_Click" runat="server" />
                        </div>
                    </div>
                    <br />

                    <%--List Of Groups--%>
                    <div id="dvListOfUserGroup" runat="server" visible="true">
                        <div class="">
                            <asp:GridView CssClass="table table-striped table-bordered" PagerStyle-CssClass="table" PagerStyle-Font-Size="12" ID="gdvwUserGroup" OnRowCommand="gdvwUserGroup_RowCommand" OnRowDataBound="gdvwUserGroup_RowDataBound" PagerStyle-HorizontalAlign="Right" BackColor="white" AutoGenerateColumns="false" OnPageIndexChanging="gdvwUserGroup_PageIndexChanging" AllowPaging="true" runat="server">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:LinkButton Style="color: #72afd2;width:150px" runat="server" ID="lbtnUserGrpName" Text='<%#Eval("UserGroup") %>' CommandArgument='<%#Eval("UserGroupID") %>' CommandName="GroupName"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="UserGroupDesc" HeaderText="Description" />
                                    <asp:BoundField ItemStyle-Width="100px" DataField="MemberCount" HeaderText="No.Of Members" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="CreatedBy" HeaderText="Created By" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="CreatedOn" HeaderText="Created On" />
                                    <asp:BoundField ItemStyle-Width="100px" DataField="UserGroupStatus" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton Style="color: #72afd2;width:150px" runat="server" ID="lnkUserGrpID" Text="Remove" CommandArgument='<%#Eval("UserGroupID") %>' CommandName="RemoveGroup"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:HiddenField ID="hUserGroupID" runat="server" Value="0" />
                        <asp:HiddenField ID="hAppUserID" runat="server" Value="0" />
                    </div>

                    <%--New Group--%>
                    <div id="dvNewGroup" class="dvNewGroup" runat="server" visible="false">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li id="profileTab" style="border-top: 3px solid transparent; margin-bottom: -2px; margin-right: 5px;" class="active" runat="server" clientidmode="static">
                                    <a href="#tabProfile" data-toggle="tab" aria-expanded="true">Profile</a>
                                </li>
                                <li id="permissionsTab" style="border-top: 3px solid transparent; margin-bottom: -2px; margin-right: 5px;" runat="server" clientidmode="static">
                                    <a href="#tabPermissions" data-toggle="tab" aria-expanded="false">Permissions</a>
                                </li>
                                <li id="membersTab" style="border-top: 3px solid transparent; margin-bottom: -2px; margin-right: 5px;" runat="server" clientidmode="static">
                                    <a href="#tabMembers" data-toggle="tab" aria-expanded="false">Members</a>
                                </li>
                            </ul>

                            <div class="tab-content">
                                <%--Profile Tab--%>

                                <%--  ViewProfile--%>

                                <div class="tab-pane active" id="tabProfile" runat="server" clientidmode="static">

                                    <div id="dvsaveMessage" runat="server" style="font-size: 15px; margin-left: 15px; display: inline; color: #0da753">
                                        <asp:Literal ID="ltrInformation" runat="server" Text=""></asp:Literal>
                                    </div>
                                    <div class="col-lg-8" style="float: right">
                                        <asp:Button ID="btnEditMembers" runat="server" Text="Edit" CssClass="btn btn-success" Style="margin-left: 8%; margin-bottom: 15px" Visible="false" OnClick="btnEditMembers_Click" />
                                    </div>
                                    <br />
                                    <div class="row" runat="server" id="dvMemberfileView" visible="false">
                                        <div class="col-md-6">
                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">
                                                        Group Name
                                                    </div>
                                                    <div class="profile-info-value">
                                                        <span>
                                                            <asp:Label ID="lblEdtGroupName" runat="server" />&nbsp;
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">
                                                        Description
                                                    </div>
                                                    <div class="profile-info-value">
                                                        <span>
                                                            <asp:Label ID="lblEdtDescription" runat="server" />&nbsp;
                                                        </span>
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">
                                                        IsActive                                    
                                                    </div>
                                                    <div class="profile-info-value">
                                                        <span>
                                                            <asp:Label ID="lblEdtIsActive" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">
                                                        IsStandard                                    
                                                    </div>
                                                    <div class="profile-info-value">
                                                        <span>
                                                            <asp:Label ID="lblIsStandard" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hEditUserGroupID" Value="0" runat="server" />
                                            </div>
                                        </div>
                                    </div>


                                    <%--   EditMembers--%>
                                    <div class="row" style="margin-left: 30px;" runat="server" id="dvAddGroup">

                                        <div class="col-lg-8">
                                            <asp:Label runat="server" Text="Group Name" Style="font-size: 18px;" />
                                            <i style="color: red; font-size: 18px;">*</i>
                                            <br />

                                            <asp:TextBox ID="txtGroupName" runat="server" Style="width: 50%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqGroupName" runat="server" ForeColor="Red" ControlToValidate="txtGroupName" ValidationGroup="vgGroup" Display="Dynamic" ErrorMessage="Enter the GroupName "></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regcharacters" runat="server" ErrorMessage="GroupName within 30 characters " ControlToValidate="txtGroupName" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" />
                                            <asp:RegularExpressionValidator ID="regGroupName" runat="server" ErrorMessage="Supports  ()_ @.,/#&+-" ControlToValidate="txtGroupName" ForeColor="Red" ValidationExpression="^[ A-Za-z0-9()_@./,#&+-]{0,30}$" />
                                            <%-- <asp:RangeValidator ID="rggroupname" ErrorMessage="GroupName with in 30 " ForeColor="Red"  ControlToValidate="txtGroupName" MinimumValue="0" MaximumValue="20" runat="server"></asp:RangeValidator>--%>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Label runat="server" Text="Description" Style="font-size: 18px;" />
                                            <i style="color: red; font-size: 18px;">*</i>
                                            <br />
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Style="width: 50%; height: 90px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqDescription" ErrorMessage="Enter the Description " ControlToValidate="txtDescription" runat="server" ValidationGroup="vgGroup" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Label ID="lblIsActive" runat="server" Text="IsActive" Style="font-size: 18px;"></asp:Label>
                                            <asp:CheckBox ID="chkIsActive" Checked="true" runat="server" />

                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Label Text="IsStandard" runat="server" Style="font-size: 18px;" />
                                            <asp:CheckBox ID="chkIsStandard" Checked="true" runat="server" />
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Button ID="btnUserGroup" ValidationGroup="vgGroup" runat="server" Text="Save" CssClass="btn btn-success" Style="margin-left: 25px; height: 29px; margin-top: 13px;" OnClick="btnUserGroup_Click" />
                                        </div>
                                    </div>
                                </div>

                                <%--Permissions Tab--%>
                                <div class="tab-pane" id="tabPermissions" runat="server" clientidmode="static">
                                    <div id="dvPermissions" runat="server">
                                        <asp:Repeater runat="server" ID="rptPermissions" OnItemDataBound="rptPermissions_ItemDataBound">
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
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="ltrFunctionName" runat="server" />
                                                        <asp:HiddenField runat="server" ID="hEntServiceID" Value='<%# Eval("entServBO.ServiceID") %>' />
                                                        <asp:HiddenField runat="server" ID="hsrvUserGroupID" Value="0" />                                                        
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkRead" runat="server" OnCheckedChanged="chkRead_CheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkWrite" runat="server" OnCheckedChanged="chkRead_CheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkExecute" runat="server" OnCheckedChanged="chkRead_CheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkExport" runat="server" OnCheckedChanged="chkRead_CheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        </div>
                                    </div>

                                <%--Members Tab--%>
                                <div class="tab-pane" id="tabMembers" runat="server" clientidmode="static">

                                    <div id="dvNewMembers" runat="server" style="float: right; margin-bottom: 15px;">
                                        <asp:Button ID="btnAddMembers" Text="Add Members" runat="server" CssClass="btn btn-primary" OnClick="btnAddMembers_Click" />
                                    </div>
                                    <div id="dvAddMembers" runat="server">
                                        <asp:ListBox runat="server" OnDataBound="ddlMembers_DataBound" ID="ddlMembers" SelectionMode="Multiple" CssClass="form-control select2" Style="width: 100%;"></asp:ListBox>
                                    </div>
                                    <div id="dvListOfUserGrpMembers" runat="server" visible="true">
                                        <div class="a">
                                            <asp:GridView CssClass="table table-striped table-bordered" PagerStyle-CssClass="table" PagerStyle-Font-Size="12" PageSize="10" ID="gdvwUserGroupMembers" OnRowDataBound="gdvwUserGroupMembers_RowDataBound" OnRowCommand="gdvwUserGroupMembers_RowCommand" PagerStyle-HorizontalAlign="Right" BackColor="white" AutoGenerateColumns="false" OnPageIndexChanging="gdvwGroupMembers_PageIndexChanging" AllowPaging="true" runat="server">
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="250px" DataField="FullName" HeaderText="User Name" />
                                                    <asp:BoundField ItemStyle-Width="250px" DataField="Email" HeaderText="Email ID" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UserBO.AppUserStatus" HeaderText="Status" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Style="color: #72afd2;" runat="server" ID="lnkRemoveGrpMem" Text="Remove" CommandArgument='<%#Eval("UserBO.AppUserID") %>' CommandName="RemoveGroupMember"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnMemberSave" Text="Save" CssClass="btn btn-success" runat="server" OnClick="btnMemberSave_Click" Visible="false" Style="float: right; margin-top: 13px;" />
                                    </div>
                                    <div id="dvNoRecord" runat="server" visible="false">
                                        <asp:Literal Text="No Records Found" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>  

        $(document).ready(function () {
            //$(".chosen-select").chosen()
            $("#" + "<%=ddlMembers.ClientID%>").select2({
                placeholder: "Select a Member",

            });
            $('.select2-selection__choice__remove').hide();
            $('.select2 .select2-container .select2-container--default').css("width", 'auto');
        });

        $(document).ready(function () {
            var tabContent = $('#ContentPlaceHolder1_UserGroupManagement_dvNewGroup .nav-tabs-custom .tab-content');
            tabContent.css({
                'margin-left': 0,
            })
        });
        var tabsFn = (function () {

            function init() {
                setHeight();
            }

            function setHeight() {
                var $tabPane = $('.dvNewGroup .tab-pane'),
                    tabsHeight = $('.dvNewGroup .nav-tabs').height();
                tabWidth = $('.dvNewGroup .nav-tabs').width();
            }

            $(init);
        })();
    </script>

</div>
