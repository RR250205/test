<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleListExpand.ascx.cs" Inherits="STOMS.UI.usercontrol.SampleListExpand" %>

<asp:PlaceHolder ID="phSummary" runat="server">
    <style>
        #ContentPlaceHolder1_SampPending_txtReasonForReactivate{
            display:inline;
        }
        label 
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 600;
        }
    </style>
    <asp:Repeater ID="rpSampleListExpand" runat="server" OnItemDataBound="rpSampleListExpand_ItemDataBound" OnItemCommand="rpSampleListExpand_ItemCommand">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row rowDetail rowIndGreen">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Specimen Number</label>
                        <br />
                        <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    </div>
                    <div class="col-lg-2">
                        <label>Created By</label>
                        <br />
                        <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-2">
                        <label>Created On</label>
                        <br />
                        <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-2">
                        <label>Status</label>
                        <br />
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-1">
                        <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                        <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                        <asp:HiddenField id="hSpID" runat="server" Value="0" />
                    </div>
                </div>
                
                <%--<asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                <asp:Literal ID="ltrAssayStatus" runat="server"></asp:Literal>
                <br />
                <div style="color: #272626; display:inline-block">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                    </small>
                </div>
                <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                 <asp:HiddenField id="hSpID" runat="server" Value="0" />--%>
            </div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div class="row rowDetail rowIndOrange">
                <div class="row">
                    <div class="col-lg-3">
                        <label>Specimen Number</label>
                        <br />
                        <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    </div>
                    <div class="col-lg-2">
                        <label>Created By</label>
                        <br />
                        <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-2">
                        <label>Created On</label>
                        <br />
                        <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-2">
                        <label>Status</label>
                        <br />
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-1">
                        <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false" runat="server" />
                        <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                        <asp:HiddenField ID="hSpID" runat="server" Value="0" />
                    </div>
                </div>
                <%--<div class="row rowDetail rowIndOrange">
                <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                 <asp:Literal ID="ltrAssayStatus" runat="server" ></asp:Literal>
                <br />
                <div style="color: #272626;display:inline-block">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                    </small>
                </div>
                <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" data-toggle="modal" data-target="#model-sp-Confirm" 
                    CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                 <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                <asp:HiddenField id="hSpID" runat="server" Value="0" />
            </div>--%>
            </div>
        </AlternatingItemTemplate>
    </asp:Repeater>  

    <div class="row rowDetail rowIndGreen" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
        <br />
        No Record...
    </div>
    <asp:HiddenField ID="hStatus" runat="server" Value="" />
    <asp:HiddenField ID="hAssayID" runat="server" Value="" />
    <asp:HiddenField ID="hSpecimenCount" runat="server" Value="" />
    <asp:HiddenField ID="hSelectedSpecimenID" runat="server" Value="" />
      <div id="model-sp-Confirm" class="modal" tabindex="-1" aria-hidden="true" >
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">                    
                   <b><span></span></b> 
                </div>
                <div class="modal-body ">                    
                    <div id="ModelFromPending" style="display:none">
                        <p>
                            <b>
                                All the following things to be accepted 
                            </b>
                        </p>
                        <asp:CheckBox ID="chkReq" Text="&nbsp;Requisition Complete ?" runat="server" />
                        <br />
                        <asp:CheckBox ID="chkConsent" Text="&nbsp;Consent Provided ?" runat="server" />
                        <br />
                        <asp:CheckBox ID="chkNoOther" Text="&nbsp;No other exceptions ?" runat="server" />
                        <br />
                        <div id="dvExceptions">Exceptions:<span id="spExceptions" ></span></div>
                    </div>
                    <div id="ModelFromReject" style="display:none">
                        <span class="reasons"></span>
                        <br />
                        Reason for Activate<span style="color:red">*</span>: <asp:TextBox runat="server"  CssClass="form-control  input150" ID="txtReasonForReactivate" />                         
                    </div>
                </div>
                <div class="modal-footer">
                    <span class="error" style="display:none;color:red"></span>
                    <asp:Button Text="Update" CssClass="btn btn-primary" ID="btnUpdateStatus" OnClick="btnUpdateStatus_Click" runat="server" />
                                      
                    <asp:Button Text="Cancel" class="btn btn-danger" runat="server" />
                                   
                </div>
            </div>
        </div>
    </div>
    
</asp:PlaceHolder>