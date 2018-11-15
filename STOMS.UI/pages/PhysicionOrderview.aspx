<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/themes/mainMaster.Master" CodeBehind="PhysicionOrderview.aspx.cs" Inherits="STOMS.UI.pages.PhysicionOrderview" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    &nbsp;&nbsp;&nbsp;    
    <style>

        .table-bordered thead tr th, 
        .table-bordered tbody tr th,
        .table-bordered tfoot tr th,
        .table-bordered thead tr td,
        .table-bordered tbody tr td,
        .table-bordered tfoot tr td {
    border: 1px solid #dad1d1;
    padding: 13px;
      }

        th{
              padding: 13px;
              
        }

        td{
            
                 padding: 13px;
        }

        tr{
             padding: 13px;

        }

    </style>
 

    <div class="row">
         <div style="margin-left: 3px;">
                                        <h3 class="blue bigger">Order View &nbsp;&nbsp;</h3>
                                           
             <%-- <a id="ContentPlaceHolder1_aSpecimenInformation" role="button" class="red" href='javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$ContentPlaceHolder1$aSpecimenInformation", "", true, "", "", false, true))'><i class="fa fa-edit" style="float: right; margin-right: 6px;"></i></a></h4>--%>
             
            
             </div>


    <div class="col-md-12" style="background-color:#c7c7c7;">
                                   

        <div class="row">
            <div class="col-md-6">
                     <div id="dvOrderView" runat="server">
              <%--  <b style="font-size: 16px">Order View </b>--%>
                <div class="table-responsive"  style="margin-top:30px;">
                    <table class="table table-bordered table-condensed" >
                        
                        <tr>
                            <th>Patient Name
                            </th>
                            <td>
                               <asp:Literal id="ltrPhyPatientName" runat="server" />
                            </td>
                           
                        </tr>


                        <tr>
                            <th>Customer Name
                            </th>
                            <td>
                                <asp:Literal ID="ltrCustomerName" runat="server"></asp:Literal>
                            </td>
                           
                        </tr>

                         <tr>
                            <th>Patient Email

                            </th>
                            <td>
                                <asp:Literal ID="ltrPatientEmail" runat="server"></asp:Literal>
                            </td>
                           
                        </tr>


                        <tr>
                            <th>Order Number  

                            </th>
                            <td>
                                <asp:Literal id="ltrPhyOrderNumber" runat="server"/>
                            </td>
                            

                        </tr>
                      
                        <tr>
                            <th>Payment Mode
                            </th>
                            <td>
                                <asp:Literal ID="ltrPaymentMode" runat="server"></asp:Literal>
                            </td>
                           
                        </tr>

                         <tr>
                            <th>Payment Status
                            </th>
                            <td>
                                <asp:Literal ID="lblPaymentStatus" runat="server"></asp:Literal>
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

            <div class="col-md-6">
                <div class="table-responsive">
                    <table class="table table-bordered table-condensed" style="margin-top:30px;" >
                          <tr>
                            <th>Specimen Number      
                            </th>
                            <td>
                               <asp:Literal id="ltrPhySpecimenNumber" runat="server" />    
                            </td>
                            
                        </tr>
                          <tr>
                            <th>Specimen Type
                            </th>
                            <td>
                                <asp:Literal ID="ltrSpecimenType" runat="server"></asp:Literal>
                            </td>
                           
                        </tr>
                         
                        
                         <tr>
                            <th>Specimen Test Type
                            </th>
                            <td>
                                <asp:Literal ID="ltrTestType" runat="server"></asp:Literal>
                                
                            </td>
                            
                        </tr>

                      
                          
                        <tr>
                            <th>Specimen Status 
                            </th>
                            <td><span class="label label-sm label-success arrowed arrowed-right">
                                <asp:Literal ID="ltrPhyOrderStatus" runat="server"></asp:Literal></span>
                            </td>
                            
                        </tr>

                        <tr>
                            <th>Specimen Result
                            </th>
                            <td>
                                 <a style="padding: 10px 10px 10px 0px" Visible="false" id="aMitResultView" runat="server"></a>
                              <asp:Literal ID="ltrSpecimenResult" runat="server" Visible="false"></asp:Literal>
                            </td>
                           
                        </tr>

                        <tr>
                            <th>Specimen Request Copy
                            </th>
                            <td>
                              <div id="dvViewFile" runat="server">
                                            <a id="ancReqViewCopy" runat="server"></a>&nbsp;
                                       <%-- <asp:LinkButton ID="btnFileClose" Text="x" CssClass="badge close" Style="background-color: #e0e0dc; opacity: 1.5; font-size: 16px; position: absolute;" runat="server" OnClick="btnFileClose_Click"></asp:LinkButton>--%>
                                        </div>
                            </td>
                           
                        </tr>


                        </table>
                    </div>


            </div>

        </div>

                              
                                    

                                    
                                </div>

         <asp:Button runat="server" ID="Resultremove" OnClick="Resultremove_Click" Text="Back" CssClass="btn btn-success" Style="float:right;margin-right:24px; margin-top: 16px;" />
        <div>

        </div>
      


    </div>

</asp:Content>
