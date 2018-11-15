<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="myprofile.aspx.cs" Inherits="STOMS.UI.pages.myprofile1" %>

<%@ Register Src="~/UserControl/showMessage.ascx" TagPrefix="uc1" TagName="showMessage" %>
<%@ Register Src="~/usercontrol/OrgImageUpload.ascx" TagPrefix="uc1" TagName="OrgImageUpload" %>

 
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <div class="page-content">
        <%--<div class="page-header">
            <h1>My Profile</h1>
        </div>--%>
         <asp:Label ID="lblUserProfilemessage" Text="" runat="server" ForeColor="Red"  Font-Size="Large"></asp:Label>
        <uc1:showMessage runat="server" ID="showMessage" />
        <asp:MultiView ID="mvSection" runat="server">
           
           
            <asp:View ID="vwProfile" runat="server">
                <div  id="dvUserprofileHide" runat="server">

                  <asp:Label ID="lblProfileMessage" runat="server" Style="color:forestgreen" Text="Profile updated successfully..."  Visible="false"></asp:Label>
                <div class="row" >
                    
                       <div class="col-lg-4">
                         <h3 class="header smaller lighter blue"><i class="icon-briefcase"></i>Basic Information</h3>
                     </div>  
                   
                        </div>
              
                
               
                       
                 <%--<div class="row">
                   
                    <div class="col-md-6">
                        <h4>Profile Picture</h4>
                        

                      

                    </div>
                </div>--%>

               
                <div class="row" >
                    <div class="col-md-6" runat="server" id="dvProfileEditPage" visible="false">
                        <div class="profile-user-info profile-user-info-striped">
                            <div class="profile-info-row">
                                <div class="profile-info-name">First Name : 
                                      <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                </div>
                                <div class="profile-info-value">
                                    <asp:DropDownList ID="ddPrefix" CssClass="GenDD" runat="server">
                                        <asp:ListItem Text="-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                        <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                        <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                        <asp:ListItem Text="Dr" Value="Dr"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtFirstName"  runat="server" CssClass="GenInput input220"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="vProfile" ID="rfdFirstName" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFirstName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ValidationGroup="vProfile" ID="rgFirstName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name">
                                    Last Name :
                                      <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                </div>
                                <div class="profile-info-value">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="input280 GenInput"></asp:TextBox>
                                     <asp:RequiredFieldValidator ValidationGroup="vProfile" ID="rfdLastName" ForeColor="Red" Display="Dynamic" ControlToValidate="txtLastName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ValidationGroup="vProfile" ID="rglastName" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                           <%-- <div class="profile-info-row">
                                <div class="profile-info-name">Middle Name</div>
                                <div class="profile-info-value">
                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="GenInput input280"></asp:TextBox>
                                </div>
                            </div>--%>
                            <%--<div class="profile-info-row">
                                <div class="profile-info-name">Department</div>
                                <div class="profile-info-value">
                                    <asp:Label ID="lblDeptName" Text="-" runat="server"></asp:Label>&nbsp;
                                </div>
                            </div>--%>
                            <div class="profile-info-row">
                                <div class="profile-info-name">Primary Email :
                                </div>
                                <div class="profile-info-value">
                                    <asp:Label ID="lblPrimaryEmail" Text="-" runat="server"></asp:Label>&nbsp;
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name">Secondary Email :
                                      <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                </div>
                                <div class="profile-info-value">
                                    <asp:TextBox ID="txtSecEmail" runat="server" CssClass="input280 GenInput"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="vProfile" ID="rfdSecEmail" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSecEmail" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ValidationGroup="vProfile" ID="rgSecEmail" ForeColor="Red" ControlToValidate="txtSecEmail" Display="Dynamic" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name">Contact Phone # :
                                      <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                </div>
                                <div class="profile-info-value">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="input280 GenInput"></asp:TextBox>
                                     <asp:RequiredFieldValidator ValidationGroup="vProfile" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPhone" ID="rfdPhone" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="rgPhone" ForeColor="Red" runat="server" ValidationGroup="vProfile" ErrorMessage="Enter valid Phone number" ControlToValidate="txtPhone" ValidationExpression="([0-9\(\)\/\+ \-]{0,15})$"></asp:RegularExpressionValidator>

                                </div>
                            </div>

                             <div class="profile-info-row">
                                <div class="profile-info-name">User Status :</div>
                                <div class="profile-info-value">
                                   <asp:Literal ID="ltrProfileEditStatus" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>


                        </div>
                    </div>

                     <div class="col-md-6"  id="dvProfleView" runat="server">
                       
                        <div class="profile-user-info profile-user-info-striped">
                            <div class="profile-info-row">
                                <div class="profile-info-name">First Name :</div>

                                <div class="profile-info-value">
                                     <asp:Literal ID="ltrprofilePrefix" runat="server"></asp:Literal>
                                    <asp:Literal ID="ltrProfileFirst" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name">Last Name :</div>
                                <div class="profile-info-value">
                                   <asp:Literal ID="ltrprofileLast" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>
                          <%--  <div class="profile-info-row">
                                <div class="profile-info-name">Middle Name</div>
                                <div class="profile-info-value">
                                    <asp:Literal ID="ltrprofileMiddle" runat="server"></asp:Literal>
                                </div>
                            </div>--%>
                            <%--<div class="profile-info-row">
                                <div class="profile-info-name">Department</div>
                                <div class="profile-info-value">
                                    <asp:Label ID="lblDeptName" Text="-" runat="server"></asp:Label>&nbsp;
                                </div>
                            </div>--%>
                            <div class="profile-info-row">
                                <div class="profile-info-name">Primary Email :</div>
                                <div class="profile-info-value">
                                    <asp:Literal ID="ltrprofilePrimaryEmail" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name" style="padding:2px;">Secondary Email :</div>
                                <div class="profile-info-value">
                                     <asp:Literal ID="ltrprofileSecondaryEmail" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name" style="padding:2px;">Contact Phone # :</div>
                                <div class="profile-info-value">
                                    <asp:Literal ID="ltrprofilePhone" runat="server"></asp:Literal>&nbsp;
                                </div>
                            </div>
                       <div class="profile-info-row">
                                <div class="profile-info-name">User Status :</div>
                                <div class="profile-info-value">
                                    <asp:Literal ID="ltrProfileViewStatus" runat="server"></asp:Literal>&nbsp;

                                   
                                </div>
                            </div>



                        </div>
                    </div>
                    <%--Get usermanagementId in (usp QueryString)AppUserId--%>
                    
                   <asp:HiddenField ID="hAppUsermanageId" runat="server" Value="0" />

                    <div class="col-md-6" style="margin-top:-16px;" >
                        <h4 style="color: #3a87ad!important;"> Profile Picture</h4>
                        <div>
                              <img id="dvProfileavatar" runat="server" class="editable img-responsive editable-click editable-empty" alt="" src="/assets/avatars/avatarM1.png" visible="false"  />
                         <%-- <img id="dvavatar" runat="server" class="editable img-responsive editable-click editable-empty" alt="" src="/assets/avatars/avatarM1.png" style="display:none;" />
                         --%>
                            <div id="dvViewProImge" runat="server">
                    <%--   <img id="avatar" class="editable img-responsive editable-click editable-empty" alt="" src="/assets/avatars/avatarM1.png" style="display: block;" />--%>
                          
                                <asp:Image ID="imgProfileLogo"  style="width:260px; height:175px; border: 2px solid gainsboro; "   runat="server" />
                                  
                            
                            <asp:HiddenField ID="hViewProfileLogo" Value="0" runat="server" />
                          
                        </div>

                            <div id="dvProfilePreview" runat="server" style="display:none; ">
                             <div id="dvPreview" class="row"  style=" border: 1px dashed #aaa; padding: 5px 5px 5px 5px;  width:50%;" >
                                        &nbsp;
                               </div>
                           </div>

                            <div runat="server" style="margin-top:20px; width:50%;">

                                  <uc1:OrgImageUpload  runat="server" ID="ProfileLogo" SetStatus="OrgImageUpload"/>
                            
                            </div>
                            
                            
                            <asp:HiddenField ID="hProfileLogoID" Value="0" runat="server" />
                            
                            
                          
                        </div>

                       
                         <div class="col-lg-4">
                     <asp:Button ID="btnProfileEdit" runat="server"  Style="float:right;" Text="Edit" CssClass="btn btn-primary"  OnClick="btnProfileEdit_Click" OnClientClick="ProfileEdit();"  />
               </div>
                    </div>
                </div>
               <div class="row" >
                   <div class="col-lg-4" style=" margin-top: 15px; text-align:right;">
                    <asp:Button ID="btnSaveProfile" runat="server" ValidationGroup="vProfile" CssClass="btn btn-success" Text="Update Info" OnClick="btnSaveProfile_Click" Visible="false" />
                     </div>
                   <div class="col-lg-4" style=" margin-top: 15px; text-align:left;">
                       <asp:Button ID="btnProfileCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnProfileCancel_Click" />
                   </div>

                </div>

            </div>
            </asp:View>
            <asp:View ID="vwPass" runat="server">
                <h4 class="header smaller lighter blue"><i class=" icon-key"></i>Change Password</h4>
                <div class="profile-user-info profile-user-info-striped">
                    
                    <div class="profile-info-row">
                        <div class="profile-info-name">New Password</div>
                        <div class="profile-info-value">
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input180 GenInput" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name">Confirm Password</div>
                        <div class="profile-info-value">
                            <asp:TextBox ID="txtConfPassword" runat="server" CssClass="input180 GenInput" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-left: 20px; margin-top: 10px;">
                    <asp:Button ID="btnChangePass" runat="server" CssClass="btn btn-info" Text="Change Password" OnClick="btnChangePass_Click" />
                </div>
            </asp:View>
            <asp:View ID="vwRole" runat="server">
                <h4 class="header smaller lighter blue"><i class="icon-exchange"></i>&nbsp;Following are list of roles associated to</h4>
                <div class="well">

                    <asp:RadioButtonList ID="rbtnRoles" runat="server" CssClass="inputO10"
                        CellSpacing="15" CellPadding="5"
                        RepeatColumns="3" RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                </div>
                <div style="margin-top: 15px;">
                    <button class="btn btn-small btn-success" type="submit" onclick="javascript:PageAction('RS');">
                        <i class="icon-arrow-right icon-on-right bigger-110"></i>Submit
                    </button>
                    <button class="btn btn-small btn-danger" type="submit" onclick="javascript:PageAction('RC');">
                        <i class="icon-backward bigger-110"></i>Cancel
                    </button>
                </div>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField ID="hAct" runat="server" />
        <asp:LinkButton ID="lbtnAction" runat="server" Text=""></asp:LinkButton>
        <script type="text/javascript" >
            
            function PageAction(act) {
                document.getElementById('ContentPlaceHolder1_hAct').value = act;
                __doPostBack('ctl00$ContentPlaceHolder1$lbtnAction', '')
                return false;
            }

          
           </script>
    </div>
</asp:Content>
