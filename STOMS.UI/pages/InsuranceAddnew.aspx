<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="InsuranceAddnew.aspx.cs" Inherits="STOMS.UI.pages.InsuranceAddnew" %>

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

     <div class="row" id="dvInsuranceEdit" runat="server">
            <div class="col-md-6" >
                     <div >
              <%--  <b style="font-size: 16px">Order View </b>--%>
               

                         	
				<div class="panel panel-danger"">
					<div class="panel-heading">
						<h3 class="panel-title">Add New Insurance Details </h3>
						
					</div>
					
					 <table class="table table-bordered table-condensed">
                        
                        <tr>
                            <th>Patient Name<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td>
                                <asp:TextBox CssClass="form-control"  ID="txtPatientName" runat="server" />  
                                <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPatientName" ID="rfPatientName" runat="server" ErrorMessage="Patient Name is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>
                         <tr>
                            <th>Patient DOB<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="txtPatientDOB" runat="server" /> 
                                 <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPatientDOB" ID="rfPatientDOB" runat="server" ErrorMessage="Patient DOB is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>

                        <tr>
                            <th>Gender<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td style="width:60%;">
                               <asp:RadioButton ID="optM" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-male blue"></i>&nbsp;Male
                               <asp:RadioButton ID="optF" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-female pink"></i>&nbsp;Female                            
                               <asp:RadioButton ID="optUn" GroupName="sex" CssClass="radio-inline" Text="" Style="position: unset; margin-left: 10px; margin-top: -21px;" runat="server" />&nbsp;&nbsp;<i class="fa fa-user blue"></i>&nbsp;UnKnown
                            </td>
                           
                        </tr>

                         <tr>
                            <th>Mobile No<i style="color: red; font-size: 18px;">*</i>

                            </th>
                            <td>
                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" /> 
                                <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtMobileNo" ID="rfMobileNo" runat="server" ErrorMessage="Mobile Number is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>


                        <tr>
                            <th>Insurance CardID <i style="color: red; font-size: 18px;">*</i>

                            </th>
                            <td>
                                <asp:TextBox ID="txtInsuranceCard_IDno" CssClass="form-control" runat="server" /> 
                                <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtInsuranceCard_IDno" ID="rfInsuranceCard_IDno" runat="server" ErrorMessage="Insurance CardID is required"></asp:RequiredFieldValidator>
                            </td>
                            

                        </tr>
                      
                        <tr>
                            <th>Policy Number<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPolicyNumber" CssClass="form-control" runat="server" /> 
                                 <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPolicyNumber" ID="rfPolicyNumber" runat="server" ErrorMessage="Policy Number is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>

                         <tr>
                            <th>Policy Name<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPolicyName" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPolicyName" ID="rfPolicyName" runat="server" ErrorMessage="Policy Name is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>

                         <tr>
                            <th>Primary Ins. Name<i style="color: red; font-size: 18px;">*</i>
                            </th>
                            <td>
                                <asp:TextBox ID="txtPrimaryInsName" CssClass="form-control" runat ="server" /> 
                                <asp:RequiredFieldValidator ValidationGroup="vgInsurance" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrimaryInsName" ID="rfPrimaryInsName" runat="server" ErrorMessage="Primary Insurance Name is required"></asp:RequiredFieldValidator>
                            </td>
                           
                        </tr>
                        <tr>
                            <th>Comments
                            </th>
                            <td>
                               <asp:TextBox ID="txtComments" runat="server"  TextMode="MultiLine" Rows="15" CssClass="GenInput" Style="padding-top: 13px; width: 100%; height: 55px;"></asp:TextBox><br />
                            </td>
                           
                        </tr>



                   </table>
				</div>
			
                   
                  

                   
                
                <br />
              <%--  <p>
                    <b style="font-size: 16px">Comments- (Explanations)
                    </b>
                </p>
                <p style="font-size: 14px">
                    <b style="font-size: 14px">MITOswab test analysis reveals -</b>
                </p>
                <div class="richtext" id="mitTestAnalysys"></div>



                <p style="font-size: 14px">
                    <b style="font-size: 14px">Interpretation
                    </b>
                </p>
                <div class="richtext" id="mitInterpretation"></div>--%>

                <%--<p style="font-size:14px"><b style="font-size:14px">
                Notes:
            </b></p> 
            <div  class="richtext" id="mitNotes"></div>--%>

                <%--<b>Signed by-</b>
                <div class="row">
                    <div class="col-md-offset-1">
                        <b style="font-size: 14px">Performed by:</b>
                        <asp:TextBox runat="server" ID="txtMitPerformedBy" />
                    </div>
                </div>--%>
            </div>

            </div>

        </div>

     <div class="row">

         <div class="col-md-2">
                            <asp:Button runat="server" ID="btnremoveInsurance" OnClick="btnremoveInsurance_Click" Text="Back" CssClass="btn btn-warning" Style="margin-right:24px;margin-top: 11px;" />
                        </div>

                        <div class="col-md-4">

     <div style="text-align:center;margin-bottom:10px;float:right;">
                             <asp:Button Text="Save" ID="btnSaveInsurance" OnClick="btnSaveInsurance_Click"  ValidationGroup="vgInsurance"  Style=" margin-top: 11px;" CssClass="btn btn-success" runat="server" />
                             </div>
</div></div>

    </asp:Content>