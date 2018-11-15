<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultRecording.aspx.cs"
    MasterPageFile="~/themes/mainMaster.Master" Inherits="STOMS.UI.pages.Results.ResultRecording"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #ContentPlaceHolder1_dvMitoswab {
            background: white;
        }

            #ContentPlaceHolder1_dvMitoswab page {
                background: white;
                display: block;
                margin: auto;
                margin-bottom: 0.5cm;
                box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
                padding: 5% 5% 5% 8%;
            }

                #ContentPlaceHolder1_dvMitoswab page [size="A4"] {
                    width: 21cm;
                    height: 29.7cm;
                }
    </style>

    <asp:Label ForeColor="Red" ID="lblSpecimenNumError" runat="server" />
    <div id="dvMitoswab" runat="server">
        <page size="A4" />
                <asp:label ID="lblInformation" style="color: rgb(12, 156, 18);font-size:20px" runat="server"></asp:label>
            
            <div class="alert alert-info" style="margin-left: 0px;">
                <div class="row" style="color: #3a3836;">
                    <div class="col-lg-4">
                        <div class="row">
                            <label style="font-weight: 600;">
                                Patient Name
                            </label>
                            <br />
                            <asp:Label ID="ltrFirstName" runat="server">&nbsp;</asp:Label>
                            <asp:Label ID="ltrLastName" runat="server">&nbsp;</asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Gender/Age</label>
                            <br />
                            <asp:Literal ID="lblAgeGender" runat="server">&nbsp;</asp:Literal>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Specimen Status</label>
                            <br />
                            <span>
                                <asp:Label ID="ltrCurrentSpecimenStatus" runat="server"></asp:Label>&nbsp;</span>
                        </div>
                        <br />
                    </div>

                    <div class="col-lg-4">
                        <div class="row">
                            <label style="font-weight: 600;">Physician</label>
                            <br />
                            <asp:Label ID="ltrPhyName" runat="server">&nbsp;</asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Facility/Entity</label>
                            <br />
                            <asp:Literal ID="ltrFacility" runat="server">&nbsp;</asp:Literal>
                        </div>
                        <br />
                        <div class="row">
                            <label style="font-weight: 600;">Payment Status</label>
                            <br />
                            <asp:Label ID="lblPaymentStatus" Text="Yet to initiate" runat="server"></asp:Label>
                        </div>
                        <br />
                    </div>

                    <div class="col-lg-4">
                        <div class="row" style="text-align: center; margin: 35px 0 0 0; font-size: 45px;">
                            <asp:Literal ID="ltrSpecimenNumber" Text="Sample #" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
                  <asp:HiddenField id="hSpecimenNumber" runat="server" Value="0" />
                  <asp:HiddenField id="hPatientName" runat="server" Value="0" />
      

            <div id="dvMitoswabResultEdit" runat="server">
                <b style="font-size: 16px">Result Values- (Observations)</b>
                <div class="table-responsive">
                    <table class="table table-striped table-bordered table-hover table-condensed">
                        <tr>
                            <th>Activity name
                            </th>
                            <th>Value *
                            </th>
                            <th>^Control Mean ±SD
                            </th>
                            <th>^Control Range mean ± 2SD
                            </th>

                        </tr>
                        <tr>
                            <th>Total Buccal Protein yield (micrograms)

                            </th>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control GenInput input240" ID="txtTotalBuccalProtein" />
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                        <tr>
                            <th>Citrate Synthase§
                            </th>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control GenInput input240" ID="txtCitrateSynthase" />
                            </td>
                            <td>12.1 ±5.1
                            </td>
                            <td>4.4 - 22
                            </td>
                        </tr>
                        <tr>
                            <th>RC-IV (RC-IV/CS)¶
                            </th>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control GenInput input240" ID="txtRCIV" />
                            </td>
                            <td>0.31 ±0.1
                            </td>
                            <td>0.15 - 0.6
                            </td>
                        </tr>
                        <tr>
                            <th>RC-I (RC-I/CS)¶
                            </th>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control GenInput input240" ID="txtRCI" />
                            </td>
                            <td>6.8 ±2.0
                            </td>
                            <td>3.4 ±11.9
                            </td>
                        </tr>

                    </table>
                </div>
                <br />
                <p>
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
                <div class="richtext" id="mitInterpretation"></div>

                <%--<p style="font-size:14px"><b style="font-size:14px">
                Notes:
            </b></p> 
            <div  class="richtext" id="mitNotes"></div>--%>

                <b>Signed by-</b>
                <div class="row">
                    <div class="col-md-offset-1">
                        <b style="font-size: 14px">Performed by:</b>
                        <asp:TextBox runat="server" ID="txtMitPerformedBy" />
                    </div>
                </div>
            </div>

            <div id="dvMitoswabResultView" runat="server">
                <p>
                    <b>MITOswab test: - </b>
                    <i>Mitochondrial respiratory chain complexes (RC-I and RC-IV) activities 
                        and Citrate Synthase (CS) enzyme activity are measured in patient’s buccal 
                        cells to evaluate the mitochondrial function in the buccal cells sample.</i>
                </p>
                <br />
                <div class="table-responsive">
                    <table class="table table-striped table-bordered table-hover table-condensed">
                        <tr>
                            <th>Activity name
                            </th>
                            <th>Value *
                            </th>
                            <th>^Control Mean ±SD
                            </th>
                            <th>^Control Range mean ± 2SD
                            </th>

                        </tr>
                        <tr>
                            <th>Total Buccal Protein yield (micrograms)

                            </th>
                            <td>
                                <asp:Literal ID="ltrMitTotalBuccalProtein" runat="server" />
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                        <tr>
                            <th>Citrate Synthase§
                            </th>
                            <td>
                                <asp:Literal ID="ltrMitCitrateSynthase" runat="server" />

                            </td>
                            <td>12.1 ±5.1
                            </td>
                            <td>4.4 - 22
                            </td>
                        </tr>
                        <tr>
                            <th>RC-IV (RC-IV/CS)¶
                            </th>
                            <td>
                                <asp:Literal ID="ltrMitRCIV" runat="server" />
                            </td>
                            <td>0.31 ±0.1
                            </td>
                            <td>0.15 - 0.6
                            </td>
                        </tr>
                        <tr>
                            <th>RC-I (RC-I/CS)¶
                            </th>
                            <td>
                                <asp:Literal ID="ltrMitRCI" runat="server" />
                            </td>
                            <td>6.8 ±2.0
                            </td>
                            <td>3.4 ±11.9
                            </td>
                        </tr>

                    </table>
                </div>

                <p>
                    Notes-
                    <br />
                    §: Activity value as nanomoles/min/mg buccal protein
                    <br />

                    ¶: Presented as ratio of the corresponding RC activity to CS activity
                    <br />
                    *: Number in parenthesis indicates the percent of control mean activity.
                    <br />
                    ^: Based on published data.
                </p>

                <p style="font-size: 18px">
                    <b>Comments- (Explanations)</b>
                </p>
                <p><b style="font-size: 16px">MITOswab test analysis reveals -</b>  </p>
                <div id="dvmitTestAnalysysView" runat="server"></div>


                <br />
                <p>
                    <b style="font-size: 16px">Interpretation
                    </b>
                </p>
                <div id="dvmitInterpretationView" runat="server"></div>
                <br />
                <p>
                    <b style="font-size: 16px">Notes:
                    </b>
                </p>
                <p style="text-align: justify; font-size: 12px">
                    It is important to note that the buccal mitochondrial enzyme testing approach is still a work in continued improvisations. While the published data supports the evidence for the representation of skeletal muscle (skm) RC enzymes activities by buccal tissue (as found in 84% of cases studied), more work is needed to be undertaken to have a more conclusive statement. Similarly, in brain and neurologic cells, although could not be directly verified, strong correlation has been experienced between buccal cell mitochondrial analysis and some brain or neurological system related problems. 
                    <br />
                    In addition, the proportion of (ratio) of defective to normal mitochondria, which varies over a broad range, determines the presence and severity of the disease.
    Thus an accurate assessment of mitochondrial dysfunction might require more than once and independent testing, which is difficult with muscle biopsy and in fact conveniently and easily possible with buccal testing methods
                    <br />
                    It is recommended to repeat the buccal testing within six months to see if the deficiency is repeatable.
                </p>
                <br />
                <p>
                    <i>Disclaimer – Please note that this MITOswab test is a lab developed test (LDT) and not yet approved by FDA.
                    </i>
                </p>
                <br />
                <div style="font-family: monospace;">
                    <b>Signed by-</b>
                    <div class="row col-md-offset-1">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4" style="padding-right: 0;">
                                    <b style="font-size: 14px">1) Performed by</b>
                                </div>
                                <div class="col-md-6">
                                    <u style="position: absolute">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                                    <b>
                                        <asp:Literal ID="ltrMitPerformedBy" runat="server" />
                                    </b>

                                </div>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <b style="font-size: 14px">Sign and Date</b>&nbsp;&nbsp;<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                        </div>
                    </div>
                    <div class=" row col-md-offset-1">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <b style="font-size: 14px"><b style="font-size: 14px">2) Reviewed by</b></b>
                                </div>
                                <div class="col-md-6">
                                    <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <b style="font-size: 14px">Sign and Date</b>&nbsp;&nbsp;<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                        </div>
                    </div>
                    <div class="row  col-md-offset-1">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-5" style="padding-right: 0">
                                    <b style="font-size: 14px">3) Medical Director</b>
                                </div>
                                <div class="col-md-6" style="margin-left: -35px;">
                                    <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <b style="font-size: 14px">Sign and Date</b>&nbsp;&nbsp;<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="pull-left">
                <asp:Button Text="Go Back" ID="btnMitGoBack" CssClass="btn " OnClick="btnMitGoBack_Click" runat="server" />
            </div>

            <div class="pull-right">

                <asp:Button Visible="false" Text="View Record" CssClass="btn btn-default" ID="btnMitViewRecord" OnClick="btnMitViewRecord_Click" runat="server" />

                <asp:Button Visible="false" Text="Save" ID="btnMitResultSave" CssClass="btn btn-primary" OnClick="btnMitResultSave_Click" runat="server" />
                <a id="aMitPreview" style="padding: 10px;" runat="server">Preview</a>
                <asp:LinkButton Text="Download" OnClick="btnMitDownload_Click" Visible="false" ID="btnMitDownload" runat="server" />
                <asp:Button Text="Generate as PDF" ID="btnMitGenAsPDF" CssClass="btn btn-primary" Visible="false" OnClick="btnMitGenAsPDF_Click" runat="server" />

                <asp:Button Visible="false" Text="Edit Record" CssClass="btn btn-primary" ID="btnMitEditRecord" OnClick="btnMitEditRecord_Click" runat="server" />

                <asp:Button Text="Release Result" ID="btnMitReleaseRecord" CssClass="btn btn-primary" Visible="false" OnClick="btnMitReleaseRecord_Click" runat="server" />
                <%--<asp:Button Text="Cancel" ID="btnMitResultCancel" CssClass="btn btn-danger" OnClick="btnMitResultCancel_Click" runat="server" />--%>
            </div>
        </page>
        
        <asp:HiddenField runat="server" ID="hMitTestAnalysys" Value="" />
        <asp:HiddenField runat="server" ID="hMitInterpretation" Value="" />
         <asp:HiddenField runat="server" ID="hMitNotes" Value="" />
        <asp:HiddenField runat="server" ID="hPatientID" Value="0" />
        <asp:HiddenField runat="server" ID="hSpecimenID" Value="0" />
        <asp:HiddenField runat="server" ID="hTestType" Value="" />
        <asp:HiddenField runat="server" ID="hCustID" Value="0" />
        <asp:HiddenField runat="server" ID="hAssayID" Value="0" />
        <asp:HiddenField runat="server" ID="hResultID" Value="0" />
        <asp:HiddenField runat="server" ID="hAssaySpecimenID" Value="0" />
        <asp:HiddenField runat="server" ID="hDocID" Value="0" />
        <asp:HiddenField runat="server" ID="hDocNumber" Value="0" />
        <asp:HiddenField runat="server" ID="hIsReleased" Value="false" />
    </div>

</asp:Content>
