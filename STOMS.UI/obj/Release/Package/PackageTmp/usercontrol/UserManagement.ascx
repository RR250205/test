<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.ascx.cs" Inherits="STOMS.UI.usercontrol.UserManagement" %>

<style type="text/css">
    .auto-style1 {
        width: 100%;
    }

    .auto-style3 {
        width: 162px;
    }

    table {
        border-collapse: collapse;
    }

    table, td, th {
        border: 1px solid grey;
    }

    th {
        background-color: #cdd2d8;
        color: #080707;
    }
</style>


<div class="" id="dvAddUser" runat="server" visible="false">

    <div class="col-md-6" style="margin-left: 20%; margin-bottom: 10px;">
        <asp:Label ID="lblNewUserInformation" Font-Bold="true" Font-Size="14px" ClientIDMode="Static" runat="server"></asp:Label>
        <asp:Button CssClass="btn btn-primary" Style="float: right; margin-right: 7px;" ID="btnViewUsers" runat="server" Text="View Users" OnClick="btnViewUsers_Click" />
    </div>
    <br />

    <div id="dvNewuser" runat="server" visible="true">
        <div class="row">
            <div class="col-md-6" style="margin-left: 20%">
                <div class="profile-user-info profile-user-info-striped">

                    <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            First Name
                                      &nbsp;
                                    <b><i style="color: red; font-size: 18px;">*</i></b>&nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                <asp:TextBox ID="txtFirstName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ValidationGroup="vusercreation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFirstName" ID="rfdFirstName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vusercreation" ID="rgFirstName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                    </div>

                    <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            Last Name
                                    &nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                <asp:TextBox ID="txtLastName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator ValidationGroup="vusercreation" ID="rgLastName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                    </div>

                    <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            Email ID
                                    &nbsp;
                                        <b><i style="color: red; font-size: 18px;">*</i></b>&nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                <asp:TextBox ID="txtEmailId" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ValidationGroup="vusercreation" ForeColor="Red" ControlToValidate="txtEmailId" Display="Dynamic" ID="rfdEmailId" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="vusercreation" ID="rgEmailId" runat="server" ForeColor="Red" ErrorMessage="Enter valid Email" ControlToValidate="txtEmailId" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                    </div>

                    <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            Password
                                    &nbsp;
                                        <b><i style="color: red; font-size: 18px;">*</i></b>&nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" Width="81%" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="vusercreation" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword" ID="rfdpassword" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator ValidationGroup="vusercreation" ID="rgpassword" runat="server" ForeColor="Red" ErrorMessage="Allow numbers and some characters Only" ControlToValidate="txtPassword" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                            </span>
                        </div>
                    </div>

                     <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            User Type
                                    &nbsp;
                                        <b><i style="color: red; font-size: 18px;">*</i></b>&nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                 <select name="forma" class="form-control dropdown"   style="width: 72%; margin-left: 1px;" id="stlabUser" runat="server">
                                 <option value="0">Select User</option>
                                  <option value="labuser" data-icon="fa fa-heart">Lab User</option>
                                   <option value="physician">Physician</option>
                                  <option value="Insuranceagent">Insurance Agent</option>
                                  </select>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="stlStromsUser" ForeColor="Red" InitialValue="0" runat="server" ErrorMessage="Please select user"></asp:RequiredFieldValidator>--%>
                            </span>
                        </div>
                    </div>


                    <div class="profile-info-row" style="height: 60px;">
                        <div class="profile-info-name">
                            Contact No
                                        &nbsp;
                        </div>
                        <div class="profile-info-value">
                            <span>
                                <asp:TextBox ID="txtTelephone" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ValidationGroup="vusercreation" ID="rgTelephone" runat="server" ForeColor="Red" ErrorMessage="Allow numbers only" ControlToValidate="txtTelephone" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </span>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row" style="margin-left:120px">
                    <%--<div class="" style="float: right">--%>
                        <asp:Button CssClass="btn btn-success" ValidationGroup="vusercreation" ID="btnSubmit" Text="Add" runat="server" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button CssClass="btn btn-danger" ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click"/>&nbsp;&nbsp;&nbsp;
                        <asp:Button CssClass="btn btn-warning" ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" />                        
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="" id="dvListUser" runat="server" visible="true">
    
    <div>
        <asp:Label ID="lblUserInformation" Font-Bold="true" Font-Size="14px" ClientIDMode="Static" Text="" runat="server"></asp:Label>
        <asp:Button CssClass="btn btn-primary" Style="float: right;" ValidationGroup="vusercreation" ID="btnNewUser" Text="New User" runat="server" OnClick="btnNewUser_Click" />
    </div>

    <div class="" style="padding-top: 50px;">

        <asp:Repeater ID="rptUserInfo" OnItemDataBound="rptUserInfo_ItemDataBound" OnItemCommand="rptUserInfo_ItemCommand" runat="server">
              
            <HeaderTemplate>
                <table class="table table-striped table-bordered" style="width: 100%; text-align: center;">
                    <tr>
                        <th scope="col" style="width: 100px; text-align: center;">First Name
                        </th>
                        <th scope="col" style="width: 100px; text-align: center;">Last Name
                        </th>
                        <th scope="col" style="width: 150px; text-align: center;">Email ID
                        </th>
                        <th scope="col" style="width: 100px; text-align: center;">Contact
                        </th>
                        <th scope="col" style="width: 100px; text-align: center;">Status
                        </th>                        
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                         <asp:LinkButton Style="color: #72afd2;" runat="server" ID="lbtnFirstName" Text='<%# Eval("FirstName") %>' CommandArgument='<%# Eval("AppUserID") %>' CommandName='<%# Eval("FirstName") %>'></asp:LinkButton>

                      <%--  <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'/>--%>
                       <asp:HiddenField ID="hFirstname" runat="server" Value='<%#Eval("FirstName") %>'  />
                        <asp:HiddenField ID="hAppUserID" runat="server" Value='<%#Eval("AppUserID") %>' />
                      
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblAppUserName" runat="server" Text='<%# Eval("AppUserName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblContact" runat="server" Text='<%# Eval("ContactPhone") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" Visible="true" runat="server" Text='<%# Eval("UserStatus") %>' />
                        <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Visible="false" runat="server">
                            <asp:ListItem Text="Active" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>        
    </div>
</div>
