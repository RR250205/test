<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleList.ascx.cs" Inherits="STOMS.UI.usercontrol.SampleList" %>
<%--<%@ Register Src="~/usercontrol/ModelSpecimenToReceived.ascx" TagPrefix="uc1" TagName="SPtoRec" %>--%>
<asp:PlaceHolder ID="phSummary" runat="server">
    <style>
        #ContentPlaceHolder1_SampPending_txtReasonForReactivate {
            display: inline;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 600;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="ASPSnippets_Pager.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        GetSamplelist(1);
    });
    $("[id*=txtSearch]").live("keyup", function () {
        GetSamplelist(parseInt(1));
    });
    $(".Pager .page").live("click", function () {
        GetSamplelist(parseInt($(this).attr('page')));
    });
    function SearchTerm() {
        return jQuery.trim($("[id*=txtSearch]").val());
    };
    function GetCustomers(pageIndex) {
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetSamplelist",
            data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
    var row;
    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Samplelist = xml.find("Samplelist");
        if (row == null) {
            row = $("[id*=gvSampleList] tr:last-child").clone(true);
        }
        $("[id*=gvSampleList] tr").not($("[id*=gvSampleList] tr:first-child")).remove();
        if (customers.length > 0) {
            $.each(customers, function () {
                var customer = $(this);
                $("td", row).eq(0).html($(this).find("FirstName").text());
                $("td", row).eq(1).html($(this).find("Gender").text());
                $("td", row).eq(2).html($(this).find("DOB").text());
                $("td", row).eq(2).html($(this).find("Facility").text());
                 $("td", row).eq(2).html($(this).find("RequesterType").text());
                $("[id*=gvSampleList]").append(row);
                row = $("[id*=gvSampleList] tr:last-child").clone(true);
            });
            var pager = xml.find("Pager");
            $(".Pager").ASPSnippets_Pager({
                ActiveCssClass: "current",
                PagerCssClass: "pager",
                PageIndex: parseInt(pager.find("PageIndex").text()),
                PageSize: parseInt(pager.find("PageSize").text()),
                RecordCount: parseInt(pager.find("RecordCount").text())
            });
 
            $(".ContactName").each(function () {
                var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
            });
        } else {
            var empty_row = row.clone(true);
            $("td:first-child", empty_row).attr("colspan", $("td", row).length);
            $("td:first-child", empty_row).attr("align", "center");
            $("td:first-child", empty_row).html("No records found for the search criteria.");
            $("td", empty_row).not($("td:first-child", empty_row)).remove();
            $("[id*=gvSampleList]").append(empty_row);
        }
    };
</script>
    <script type="text/javascript">
  jQuery(document).ready(function () {
   $("#gvSampleList").tablesorter({ debug: false, widgets: ['zebra'], sortList: [[0, 0]] });
  });
</script>
    <script type="text/javascript" src=http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js > </script>
<script type="text/javascript" src="Scripts/jquery.tablesorter-2.0.3.js"></script>
<link type="text/css" rel="stylesheet" href="Scripts/style1.css" />
    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    table
    {
        border: 1px solid #ccc;
    }
    table th
    {
        background-color: #F7F7F7;
        color: #333;
        font-weight: bold;
    }
    table th, table td
    {
        padding: 5px;
        border-color: #ccc;
    }
    .Pager span
    {
        color: #333;
        background-color: #F7F7F7;
        font-weight: bold;
        text-align: center;
        display: inline-block;
        width: 20px;
        margin-right: 3px;
        line-height: 150%;
        border: 1px solid #ccc;
    }
    .Pager a
    {
        text-align: center;
        display: inline-block;
        width: 20px;
        border: 1px solid #ccc;
        color: #fff;
        color: #333;
        margin-right: 3px;
        line-height: 150%;
        text-decoration: none;
    }
    .highlight
    {
        background-color: #FFFFAF;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
 <ContentTemplate>  
        <asp:GridView ID="gvSampleList" runat="server" OnDataBound="gvSampleList_DataBound" OnRowCommand="gvSampleList_RowCommand" AutoGenerateColumns="false" class="table table-striped"
        Width="100%" AllowPaging="true"  OnSorting="gvSampleList_Sorting"
        OnPageIndexChanging="gvSampleList_PageIndexChanging" PageSize="10">                         
        <Columns>
            <%--<asp:TemplateField HeaderText="Specimen Number" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    <asp:Literal ID="ltrAssayStatus" runat="server"></asp:Literal>
                    <br />
                    <div style="color: #272626; display: inline-block">
                        <small>
                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                        </small>
                    </div>
                    <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false" runat="server" />
                    <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                    <asp:HiddenField ID="hSpID" runat="server" Value="0" />
                </ItemTemplate>

                <AlternatingItemTemplate>
                    <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    <asp:Literal ID="ltrAssayStatus" runat="server"></asp:Literal>
                    <br />
                    <div style="color: #272626; display: inline-block">
                        <small>
                            <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                        </small>
                        <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" data-toggle="modal" data-target="#model-sp-Confirm"
                            CssClass="btn btn-sm modal-trigger" Visible="false" runat="server" />
                        <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                        <asp:HiddenField ID="hSpID" runat="server" Value="0" />
                    </div>
                </AlternatingItemTemplate>

            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Name" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName")%>' ></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                   <asp:Label ID="lblName" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName")%>' ></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>
            

            <asp:TemplateField HeaderText="Gender" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Facility" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblFacility" runat="server" Text='<%#Eval("Facility") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblFacility" runat="server" Text='<%#Eval("Facility") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Specimen Number" ItemStyle-Width="150" >
                <ItemTemplate>
                    <asp:Label ID="lblSpecimenNumber" runat="server" Text='<%#Eval("SpecimenNumber") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblSpecimenNumber" runat="server" Text='<%#Eval("SpecimenNumber") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Requester Type" ItemStyle-Width="150" SortExpression="Requester Type">
                <ItemTemplate>
                    <asp:Label ID="lblRequesterType" runat="server" Text='<%#Eval("RequesterType") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblRequesterType" runat="server" Text='<%#Eval("RequesterType") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Specimen Status" ItemStyle-Width="150" SortExpression="Specimen Status">
                <ItemTemplate>
                    <asp:Label ID="lblSpecimenStatus" runat="server" Text='<%#Eval("SpecimenStatus") %>'></asp:Label>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <asp:Label ID="lblSpecimenStatus" runat="server" Text='<%#Eval("SpecimenStatus") %>'></asp:Label>
                </AlternatingItemTemplate>
            </asp:TemplateField>
           
            
        </Columns>
    </asp:GridView>
    </div>           
     </ContentTemplate>
</asp:UpdatePanel>

    <%--   <asp:Repeater ID="rpSampleList" runat="server" OnItemDataBound="rpSampleList_ItemDataBound" OnItemCommand="rpSampleList_ItemCommand">
                 <ItemTemplate>
            <div class="row rowDetail rowIndGreen">
                <asp:LinkButton CommandArgument='<%# Eval("SpecimenID") %>' CommandName="Detail" Text='<%# Eval("SpecimenNumber") %>' ID="lbtnSampleNo" runat="server"></asp:LinkButton>
                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                <asp:Literal ID="ltrAssayStatus" runat="server"  ></asp:Literal>
                <br />
                <div style="color: #272626; display:inline-block">
                    <small>
                        <asp:Literal ID="ltrPatName" runat="server"></asp:Literal>
                    </small>
                </div>
                <asp:LinkButton Text="More action" CommandName="Action" ID="lbtnFurtherAction" CssClass="btn btn-sm modal-trigger" Visible="false"  runat="server" />
                <asp:HiddenField ID="hReasons" runat="server" Value="0" />
                 <asp:HiddenField id="hSpID" runat="server" Value="0" />
            </div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div class="row rowDetail rowIndOrange">
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
            </div>
        </AlternatingItemTemplate>
         
    </asp:Repeater>--%>

    <div class="row rowDetail rowIndGreen" style="text-align: center; vertical-align: middle; height: 30px;" id="dvNoRec" runat="server" visible="false">
        <br />
        No Record...
    </div>
    <asp:HiddenField ID="hStatus" runat="server" Value="" />
    <asp:HiddenField ID="hAssayID" runat="server" Value="" />
    <asp:HiddenField ID="hSpecimenCount" runat="server" Value="" />
    <asp:HiddenField ID="hSelectedSpecimenID" runat="server" Value="" />
    <div id="model-sp-Confirm" class="modal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog Container1">
            <div class="modal-content">
                <div class="modal-header">
                    <b><span></span></b>
                </div>
                <div class="modal-body ">
                    <div id="ModelFromPending" style="display: none">
                        <p>
                            <b>All the following things to be accepted 
                            </b>
                        </p>
                        <asp:CheckBox ID="chkReq" Text="&nbsp;Requisition Complete ?" runat="server" />
                        <br />
                        <asp:CheckBox ID="chkConsent" Text="&nbsp;Consent Provided ?" runat="server" />
                        <br />
                        <asp:CheckBox ID="chkNoOther" Text="&nbsp;No other exceptions ?" runat="server" />
                        <br />
                        <div id="dvExceptions">Exceptions:<span id="spExceptions"></span></div>
                    </div>
                    <div id="ModelFromReject" style="display: none">
                        <span class="reasons"></span>
                        <br />
                        Reason for Activate<span style="color: red">*</span>:
                        <asp:TextBox runat="server" CssClass="form-control  input150" ID="txtReasonForReactivate" />
                    </div>
                </div>
                <div class="modal-footer">
                    <span class="error" style="display: none; color: red"></span>
                    <asp:Button Text="Update" CssClass="btn btn-primary" ID="btnUpdateStatus" OnClick="btnUpdateStatus_Click" runat="server" />
                    <button class="btn btn-danger" data-dismiss="modal">
                        <i class="icon-remove"></i>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:PlaceHolder>
