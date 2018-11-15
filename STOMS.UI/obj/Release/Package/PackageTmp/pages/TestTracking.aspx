<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="TestTracking.aspx.cs" Inherits="STOMS.UI.pages.TestTracking" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/usercontrol/orderHeader.ascx" TagPrefix="uc1" TagName="orderHeader" %>
<%@ Register Src="~/usercontrol/SampleList.ascx" TagPrefix="uc1" TagName="SampleList" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div style="margin: 10px 10px 10px 10px;">
            <div class="row">
                <div class="col-sm-9">
                    <div class="col-md-4">
                        <div class="bg-gray" style="min-height: 550px;">
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h4>17 - Received Specimens</h4>
                                </div>
                            </div>
                            <uc1:SampleList runat="server" ID="SampReceive" SetStatus="Received" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="bg-gray" style="min-height: 550px;">
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h4>12 - Assigned to Assay</h4>
                                </div>
                                <%--<a class="small-box-footer" href="#">Assigned to Assay</a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="bg-gray" style="min-height: 550px;">
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h4>7 - Results</h4>
                                </div>
                                <%--<a class="small-box-footer" href="#">Results</a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="alert alert-info" style="min-height: 600px;">
                    </div>
                </div>
            </div>
            <asp:PlaceHolder ID="phTrack" runat="server">
                <div class="col-sm-12">
                    <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px;">

                        <div class="row" style="margin: 20px 30px 20px 30px;">
                            <uc1:orderHeader runat="server" ID="orderHeader" />
                        </div>
                        <div class="row" style="margin: 20px 30px 20px 30px;">
                            <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 2px;">
                                <strong>Sample Details</strong>
                            </div>
                            <div class="row well" style="margin-left: 1px; margin-right: 1px; margin-top: 10px; padding: 5px;">
                                <div class="row" style="margin: 10px 20px 25px 2px;">
                                    <div class="col-md-2">
                                        <strong>Sample Number</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddSampleNumber" runat="server" Visible="true" DataTextField="SampleBarcode" DataValueField="TTrackID"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddSampleNumber_SelectedIndexChanged" CssClass="input120 GenDD chosen-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-10">
                                        <iframe runat="server" id="ifrmBC" style="height: 80px; width: 180px; border: none; overflow: hidden;" src="/ext/showBC.aspx?bc=1234567890123"></iframe>
                                    </div>
                                </div>
                                <div class="row" style="margin: 10px 20px 25px 2px;">
                                    <strong>Patient Name/Number</strong>&nbsp;<asp:Literal ID="ltrPatName" runat="server"></asp:Literal>&nbsp;&nbsp;
                           
                                    <strong>Gender/Age</strong>&nbsp;<asp:Literal ID="ltrDenAge" runat="server"></asp:Literal>
                                </div>
                                <div class="row" style="margin: 10px 20px 25px 2px;">
                                    <div style="float: left; padding-right: 10px;">
                                        Date drawn<br />
                                        <asp:TextBox ID="txtDrawnDate" runat="server" CssClass="input160 GenInput" placeholder="Date drawn"></asp:TextBox>
                                    </div>
                                    <div style="float: left; padding-right: 10px;">
                                        Sample Arrival Date<br />
                                        <asp:TextBox ID="txtArrDate" runat="server" CssClass="input160 GenInput" placeholder="Sample Arrival Date"></asp:TextBox>
                                    </div>
                                    <div style="float: left; padding-right: 10px;">
                                        Sample Status<br />
                                        <asp:DropDownList ID="ddSampleStatus" runat="server" Visible="true" placeholder="Sample Status" CssClass="input200 GenDD chosen-select">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Thawed-with Icepack" Value="Thawed-with Icepack"></asp:ListItem>
                                            <asp:ListItem Text="Frozen, dry ice" Value="Frozen, dry ice"></asp:ListItem>
                                            <asp:ListItem Text="Warm, no ice pack" Value="Warm, no ice pack"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; padding-top: 5px;">
                                        <br />
                                        <asp:Button ID="btnUpdateSample" runat="server" Text="Update Sample Info" CssClass="btn btn-primary" OnClick="btnUpdateSample_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 0px;">
                                        <strong>Folate Receptor - Test results</strong>
                                    </div>
                                    <div runat="server" id="dvBIN">
                                        <div class="alert alert-block alert-info" style="margin-left: 0px !important; margin-top: 10px; padding-right: 30px;">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    Date Assay Binding<br />
                                                    <asp:TextBox ID="txtBINAssay" runat="server" CssClass="input150 GenInput" placeholder="Date Assay Binding"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Binding Negative<br />
                                                    <asp:TextBox ID="txtBindNeg" runat="server" CssClass="input150 GenInput" placeholder="Binding Negative"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Binding BL<br />
                                                    <asp:TextBox ID="txtBlockNeg" runat="server" CssClass="input150 GenInput" placeholder="Binding BL"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Binding Positive<br />
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="input150 GenInput" placeholder="Binding Positive"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 15px;">
                                                <div class="col-md-3">
                                                    Date Assay Blocking<br />
                                                    <asp:TextBox ID="txtBLOAssay" runat="server" CssClass="input150 GenInput" placeholder="Date Assay Blocking"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Blocking Negative<br />
                                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="input150 GenInput" placeholder="Blocking Negative"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Blocking BL<br />
                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="input150 GenInput" placeholder="Blocking BL"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    Blocking Positive<br />
                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="input150 GenInput" placeholder="Blocking Positive"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div runat="server" id="dvBLO">
                                        <div class="highlight row bg-orange" style="height: 30px; vertical-align: middle; padding: 5px 5px 5px 10px; margin-right: 0px;">
                                            <strong>Calculation - Result</strong>
                                        </div>
                                        <div class="alert alert-block alert-success" style="margin-left: 0px !important; margin-top: 10px; padding-right: 30px;">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    Pts-Negative<br />
                                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="input150 GenInput" placeholder="Pts-Negative"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    Pts Any positive<br />
                                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="input150 GenInput" placeholder="Pts Any positive"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    Pts BL<br />
                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="input150 GenInput" placeholder="Pts BL"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 15px;">
                                                <div class="col-md-4">
                                                    Pts Both pos<br />
                                                    <asp:TextBox ID="TextBox8" runat="server" CssClass="input150 GenInput" placeholder="Pts Both pos"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    Pts p/BL<br />
                                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="input150 GenInput" placeholder="Pts p/BL"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row well" style="margin-left: 1px; margin-right: 1px; padding: 15px;">
                                <div class="col-lg-6" style="align-content: center">
                                    N&nbsp;<asp:TextBox ID="TextBox10" runat="server" CssClass="input150 GenInput"></asp:TextBox>&nbsp;&nbsp;
                                BL&nbsp;<asp:TextBox ID="TextBox11" runat="server" CssClass="input150 GenInput"></asp:TextBox>&nbsp;&nbsp;
                                P&nbsp;<asp:TextBox ID="TextBox12" runat="server" CssClass="input150 GenInput"></asp:TextBox>
                                </div>
                                <div class="col-lg-6" style="align-content: center">
                                    N %&nbsp;<asp:TextBox ID="TextBox13" runat="server" CssClass="input150 GenInput"></asp:TextBox>&nbsp;&nbsp;
                                BL %&nbsp;<asp:TextBox ID="TextBox14" runat="server" CssClass="input150 GenInput"></asp:TextBox>&nbsp;&nbsp;
                                P %&nbsp;<asp:TextBox ID="TextBox15" runat="server" CssClass="input150 GenInput"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 15px; margin-bottom: 2px;">
                                <asp:Button ID="btnSaveResult" runat="server" Text="Save Result" CssClass="btn btn-primary" OnClick="btnSaveResult_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phList" runat="server" Visible="false">
                <div class="row">
                    <div class="col-sm-9">
                        <div style="border: 1px solid #c5d0dc; background-color: #ffffff; min-height: 600px; margin-bottom: 10px; padding: 10px;">
                            <h4>List of Samples in Testing</h4>
                            <telerik:RadGrid ID="tgrdTT" runat="server" AllowPaging="True" AllowSorting="True"
                                ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                                BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdTT_ItemDataBound">
                                <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                                    Width="100%">
                                    <NoRecordsTemplate>
                                        <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                                            <center>No record to display</center>
                                        </div>
                                    </NoRecordsTemplate>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Ordered" FieldName="OrderCustomer" FormatString="{0:D}"
                                                    HeaderValueSeparator=" by: "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="OrderCustomer" SortOrder="Descending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="SampleBarCode" HeaderText="Sample Number" />
                                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" />
                                        <telerik:GridTemplateColumn HeaderText="Date drawn">
                                            <ItemTemplate>
                                                <span runat="server" id="spDateDrawn"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Date received">
                                            <ItemTemplate>
                                                <span runat="server" id="spDateReceived"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Result Sent">
                                            <ItemTemplate>
                                                <span runat="server" id="spDateSent"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Is Binding">
                                            <ItemTemplate>
                                                <span runat="server" id="spBinf"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Is Blocking">
                                            <ItemTemplate>
                                                <span runat="server" id="spBlock"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span runat="server" id="spOrderStatus"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="ChkColumn1" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <div class="hidden-phone visible-desktop btn-group">
                                                    <button class="btn btn-info btn-sm" onclick="javascript:setTTAction('<%# Eval("TTrackID") %>');return false;">
                                                        <i class="fa fa-edit bigger-120"></i>
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
                    <div class="col-sm-3">
                        <div class="alert alert-info" style="min-height: 600px;">
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:HiddenField ID="hTTackerID" runat="server" Value="0" OnValueChanged="hTTackerID_ValueChanged" />

            <script type="text/javascript">

                function setTTAction(svalID) {
                    document.getElementById('<%= hTTackerID.ClientID %>').value = svalID;
                    var hValueID = "<%= hTTackerID.ClientID %>";
                    __doPostBack(hValueID, "");
                }

            </script>
        </div>
    </div>
</asp:Content>
