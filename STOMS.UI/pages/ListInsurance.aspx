<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="ListInsurance.aspx.cs" Inherits="STOMS.UI.pages.ListInsurance" %>

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
    <style>
        th{
            color:saddlebrown;
            text-align:left;
        }
    </style>

    <div class="row">
        <div class="col-lg-12" >
            <div class="row">

                <div class="col-lg-8" >
                    <div style="margin:20px;">
                        <h4>List of Associated  Labs</h4>

                         <asp:DropDownList runat="server" ID="ddlInsurancelist" AutoPostBack="true" style="margin-top:6px;" OnSelectedIndexChanged="ddlInsurancelist_SelectedIndexChanged">
                     
                  </asp:DropDownList>
                    </div>   
                    </div>
                 
                <div class="col-lg-3">
                 <div>
                     <asp:Button Text="Add" ID="btnInsuranceAdd" OnClick="btnInsuranceAdd_Click" Style="float:right;margin-top: 27px;" CssClass="btn btn-success" runat="server" />
            </div>
                </div>

            </div>
            
                  
            

            <div class="" style="padding-top: 10px;">
                
                <asp:Label Text="There is no pre-Authorization form found " Style="text-align:center;font-size:20px; color:crimson;" ID="lblpreAuthorization" Visible="false"  runat="server" />

          </div>
                              
             <div class="" style="padding-top: 10px;">

         <asp:Repeater ID="rpInsuranceOrderList" runat="server" OnItemCommand="rpInsuranceOrderList_ItemCommand" OnItemDataBound="rpInsuranceOrderList_ItemDataBound">
              
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover" style="width: 100%; text-align: center;">
                    <tr>
                        <th scope="col" style="width: 100px;">Patient Name
                        </th>
                        <th scope="col" style="width: 100px;">Primary Ins. Name
                        </th>
                        <th scope="col" style="width: 150px;">Create on
                        </th>
                        <th scope="col" style="width: 100px;">Insurance CardID
                        </th>
                        <th scope="col" style="width: 100px;">Policy Name
                        </th>    
                        <th scope="col" style="width: 100px; text-align: center;">Status
                        </th>                        
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align:left;">
                        <asp:LinkButton CommandArgument='<%# Eval("PreInsuranceNo") %>' CommandName="Detail" Text='<%# Eval("PatientName") %>' ID="lbtnPreInsuranceNo" runat="server"></asp:LinkButton>

                      <%--  <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'/>--%>
                      <%-- <asp:HiddenField ID="hFirstname" runat="server" Value='<%#Eval("FirstName") %>'  />
                        <asp:HiddenField ID="hAppUserID" runat="server" Value='<%#Eval("AppUserID") %>' />--%>
                      
                    </td>
                    <td style="text-align:left;">
                         <asp:Label ID="lblPrimaryInsName" runat="server"></asp:Label>

                    </td>
                    <td style="text-align:left;">
                        <asp:Label ID="lblCreatedOn" runat="server" ></asp:Label>
                    </td>
                    <td style="text-align:left;">
                <asp:Label ID="lblInsuranceCard_IDno" runat="server" ></asp:Label>
                    </td>

                    <td style="text-align:left;">

                        <asp:Label ID="lblPolicyName" runat="server" ></asp:Label>
                    </td>
                    <td>
                        
                         <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                    </td>
                    <%--<td>
                        <asp:Label ID="lblStatus" Visible="true" runat="server" Text='<%# Eval("UserStatus") %>' />
                        <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Visible="false" runat="server">
                            <asp:ListItem Text="Active" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="InActive" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>  --%>                  
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>        
    </div>


            
   

        </div>

    </div>

   

    <asp:HiddenField ID="hPhyCustID" runat="server" Value="0" />

</asp:Content>
