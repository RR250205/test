<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatSampleTestList.ascx.cs" Inherits="STOMS.UI.usercontrol.PatSampleTestList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadGrid ID="tgrdSampleTest" runat="server" AllowPaging="True" AllowSorting="True"
    ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
    BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="15" OnItemDataBound="tgrdSampleTest_ItemDataBound">
    <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
        Width="100%">
        <NoRecordsTemplate>
            <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                <center>No record to display</center>
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn DataField="SampleBarCode" HeaderText="Sample Number" />
            <telerik:GridTemplateColumn HeaderText="Patient Name/Number">
                <ItemTemplate>
                    <span runat="server" id="spPatName"></span>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Bindind Assay Date">
                <ItemTemplate>
                    <span runat="server" id="spBINDate"></span>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="ResultBIN" HeaderText="Binding Result" />
            <telerik:GridTemplateColumn HeaderText="Blocking Assay Date">
                <ItemTemplate>
                    <span runat="server" id="spBLODate"></span>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="ResultBL" HeaderText="Blocking Result" />
            <telerik:GridTemplateColumn HeaderText="result Sent Date">
                <ItemTemplate>
                    <span runat="server" id="spResultDate"></span>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Sample Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span runat="server" id="spSampleStatus"></span>
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
<asp:HiddenField ID="hOrderID" runat="server" Value="0" />
<asp:HiddenField ID="hTTackerID" runat="server" Value="0" OnValueChanged="hTTackerID_ValueChanged" />

<script type="text/javascript">
    
    function setTTAction(svalID) {
        document.getElementById('<%= hTTackerID.ClientID %>').value = svalID;
        var hValueID = "<%= hTTackerID.ClientID %>";
        __doPostBack(hValueID, "");
    }

</script>