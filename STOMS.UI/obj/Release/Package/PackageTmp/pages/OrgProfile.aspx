<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="OrgProfile.aspx.cs" Inherits="STOMS.UI.pages.OrgProfile" %>

<%@ Register Src="~/usercontrol/FileUpload.ascx" TagPrefix="uc1" TagName="OrgImageUpload" %>



<asp:Content ID="Orgnization" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content" id="dvOrgEdit" runat="server"  visible="false">
    <div class="row">
         <div class="col-lg-9" id="dvKitOrderManual" runat="server">
            
             <div class="box box-info">
                <div class="box-body" style="min-height: 720px;">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Orgn. Name:
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Literal ID="ltrOrgName" runat="server"></asp:Literal>

                                          <%--  <asp:TextBox ID="txtOrgName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" Display="Dynamic" ControlToValidate="txtOrgName" ID="rfdOrgName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>--%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name">
                                        Orgn. A/C No:
                                      
                                    </div>
                                    <div class="profile-info-value">
                                        <span>

                                            <asp:Literal ID="ltrtOrgACNoEdit" runat="server"></asp:Literal>


                                           <%-- <asp:TextBox ID="txtOrgACNo" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" ControlToValidate="txtOrgACNo" Display="Dynamic" ID="rfdOrgACNo" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        --%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                       Orgn. Reg No:
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                            <asp:TextBox ID="txtOrgnRegNo" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" Display="Dynamic" ForeColor="Red" ControlToValidate="txtOrgnRegNo" ID="rfdOrgnRegNo" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Address :
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtAddress" CssClass="form-control" Width="81%" runat="server" TextMode="MultiLine" Style="height: 37px;"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" Display="Dynamic" ForeColor="Red" ControlToValidate="txtAddress" ID="rfdAddress" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Country :
                                       
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                           
                                            <asp:Literal ID="ltrEditCountry" runat="server"></asp:Literal>
                                            
                                           <%-- <asp:TextBox ID="txtCountry" CssClass="form-control" Width="81%" runat="server" maxlength="30"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountry" ID="rfdCountry" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vOrgInf" ID="rgCountry" runat="server" ForeColor="Red" ErrorMessage="Allow Characters Only" ControlToValidate="txtCountry" ValidationExpression="^[a-zA-Z ]*$" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Primary Contact No :
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtPrimaryContNo" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrimaryContNo" ID="rfdPrimaryContNo" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="rgPrimaryContNo" ForeColor="Red" runat="server" ValidationGroup="vOrgInf" ErrorMessage="Enter valid Phone number" ControlToValidate="txtPrimaryContNo" ValidationExpression="([0-9\(\)\/\+ \-]*)$"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                       Secondary Contact No :
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtSecContNo" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSecContNo" ID="rfdSecContNo" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="rgSecondary" runat="server" ForeColor="Red" ValidationGroup="vOrgInf" ErrorMessage="Enter valid Phone number" ControlToValidate="txtSecContNo" ValidationExpression="([0-9\(\)\/\+ \-]*)$"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                              <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Email
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtEmail" Width="81%" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ID="rfdEmail" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vOrgInf" ID="rgEmail" ForeColor="Red" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" runat="server" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" style="height:94px;">
                                    <div class="profile-info-name">
                                        Orgn. Logo:
                                        
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                             



                                            <uc1:OrgImageUpload  runat="server" ID="OrgImage" SetStatus="OrgImageUpload"/>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                        Subscription Info :
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Literal ID="ltrSubScriptionEdit" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                               <div class="profile-info-row" >
                                    <div class="profile-info-name">
                                    Fax Number
                                        <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>&nbsp;
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:TextBox ID="txtFaxNumber" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFaxNumber" ID="rfdFaxNumber" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ValidationGroup="vOrgInf" ID="rgFaxNumber" runat="server" ForeColor="Red" ErrorMessage="Allow numbers and some characters Only" ControlToValidate="txtFaxNumber" ValidationExpression="([0-9\(\)\/\+ \-]*)$" EnableClientScript="true"></asp:RegularExpressionValidator>
                                        </span>
                                    </div>
                                </div>

                                <asp:HiddenField ID="hOrgLogo" runat="server" Value="0" />
                               
                              
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-6" style="text-align: right">
                            <asp:Button CssClass="btn btn-success" ValidationGroup="vOrgInf" ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
                        </div>
                        <div class="col-lg-6" style="text-align: left">
                            <asp:Button CssClass="btn btn-danger" ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <br />

                  
                </div>
            </div>
        </div>


   </div>









    </div>


     <div class="page-content" id="dvorgView" runat="server">
          <div class="row">
              <div class="col-lg-8">
                  <asp:Button ID="btnOrgEdit" CssClass="btn btn-success" runat="server" OnClick="btnOrgEdit_Click" Text="Edit" Style="float:right; margin-bottom: 10px; width:9%;font-size: 14px;" />

              </div>


              </div>

          <div id="dvsaveMessage" runat="server" visible="false" style="font-size:15px; color:#0da753" >
                 <asp:Literal ID="ltrsaveMessage" runat="server" Text=" Organization profile has been saved successfully"></asp:Literal>
          </div>

    <div class="row">
         <div class="col-lg-9" >
            <div class="box box-info">
                <div class="box-body" style="min-height: 720px;">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Orgn. Name:
                                  
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Literal ID="ltrOrgNameView" runat="server"></asp:Literal>

                                          <%--  <asp:TextBox ID="txtOrgName" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" Display="Dynamic" ControlToValidate="txtOrgName" ID="rfdOrgName" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>--%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Orgn. A/C No:
                                       
                                    </div>
                                    <div class="profile-info-value">
                                        <span>

                                            <asp:Literal ID="ltrOrgnACNo" runat="server"></asp:Literal>


                                           <%-- <asp:TextBox ID="txtOrgACNo" CssClass="form-control" Width="81%" runat="server"></asp:TextBox>&nbsp;
                                            <asp:RequiredFieldValidator ValidationGroup="vOrgInf" ForeColor="Red" ControlToValidate="txtOrgACNo" Display="Dynamic" ID="rfdOrgACNo" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                        --%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                       Orgn. Reg No:
                                       
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Literal ID="ltrOrgnRegNo" runat="server"></asp:Literal>
                                            </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Address :
                                      
                                    </div>
                                    <div class="profile-info-value">
                                        <span>

                                              <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>

                                           </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Country :
                                        
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                             <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                       Primary Contact No :
                                     
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            
                                           <asp:Literal ID="ltrPrimaryCont" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Secondary Contact No :
                                        
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                               
                                           <asp:Literal ID="ltrSecContNo" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                              <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                        Email
                                        
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                             <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="profile-user-info profile-user-info-striped">

                                <div class="profile-info-row" style="height: 120px;">
                                    <div class="profile-info-name">
                                        Orgn. Logo:
                                        
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                             <asp:Image ID="imgLogo"  style="width:100px; height:100px;"   runat="server" />

                                                                                       <%--<asp:Literal ID="ltrlogo" runat="server"></asp:Literal>--%>
                                        </span>
                                    </div>
                                </div>

                                <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                         Subscription Info :
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                          <asp:Literal ID="ltrSubScriptionview" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                               <div class="profile-info-row" style="height: 50px;">
                                    <div class="profile-info-name">
                                    Fax Number
                                       
                                    </div>
                                    <div class="profile-info-value">
                                        <span>
                                            <asp:Literal ID="ltrFaxNumber" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>

                               
                              
                            </div>
                        </div>
                    </div>
                    <br />

                

                  
                </div>
            </div>
        </div>


   </div>









    </div>

</asp:Content>