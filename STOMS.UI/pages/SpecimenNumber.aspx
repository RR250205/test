<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecimenNumber.aspx.cs" MasterPageFile="~/blankMaster.Master" Inherits="STOMS.UI.pages.SpecimenNumber" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../style/css/Style.css" rel="stylesheet" />

    <%-- <script src="../style/js/jquery.min.js"></script>--%>
    <div id="dvCreateNum" runat="server" visible="true">
     <div class="row">  
            <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title"></h3>

          </div>
        
        <div class="box-body">
          <div class="row" style="margin-left:30px;">
            <div class="col-lg-4">
                    <asp:Button ID="btnSpecimenNum" CssClass="btn btn-block btn-primary btn-lg" OnClick="btnSpecimenNum_Click" runat="server" Text="New Specimen Number" Style="height: 102px; width: 225px;" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnTodaySpecimenList" CssClass="btn btn-block btn-primary btn-lg" OnClick="btnTodaySpecimenList_Click" runat="server" Text="Today Specimen List" Style="height: 102px; width: 225px;" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnApplication" CssClass="btn btn-block btn-primary btn-lg" runat="server" Text="Application " Style="height: 102px; width: 225px;" />
                </div>
           
          </div>
         
        </div>
       
        
      </div>
        </div> 
    </div>
    <div id="dvShowNumber" runat="server" visible="false" style="margin-left:30px;">
        <div class="row" runat="server">
            
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">New Specimen Number</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                           
                            <div class="col-md-6" >
                                <div class="row">
                         <h3 class="title">Specimen Number Input</h3>
                                    </div>
                        <div class="row" style="text-align: center; border: 1px dashed #aaa; text-align: center; padding: 5px 5px 5px 5px; margin-left: 5px; margin-right: 5px; background-color: beige;">
                            <strong id="stSpnum">
                                <asp:Literal ID="ltrSpecimenNumber" runat="server"></asp:Literal>
                            </strong>
                        </div>

                        <div class="row" style="margin-left: 5px; margin-right: 15px; margin-top: 15Px;">

                            <div class="col-lg-6">
                                <asp:Button ID="btnPrint" CssClass="btn btn-block btn-info btn-lg" runat="server" Text="Print label" Style="height: 47px; width: 115px;" />
                            </div>
                            <div class="col-lg-6">
                                <asp:Button ID="BtnNextSpecimen" CssClass="btn btn-block btn-info btn-lg" runat="server" Text="Next Specimen Number" OnClick="btnSpecimenNum_Click" Style="height: 47px; width: 250px;" />
                            </div>
                        </div>
                        <div class="row" >
                            <h3 class="title">Captured Specimen Image</h3>

                        </div>

                        <div class="row" style="margin-left: 5px">
                            <input id="fileupload" type="file" />
                        </div>
                        <div class="row">
                            <asp:Button ID="Button2" CssClass="btn btn-block btn-success btn-lg" runat="server" Text="Upload" Style="height: 47px; width: 115px; margin-left: 44%;" />

                        </div>
                            </div>

                            <div class="col-md-6" runat="server">
                                <div class="row">
                           <h3 class="title">Requested form Preview</h3>
                                    </div>
                             <div id="dvPreview" title="Sample"></div>

                
            </div>
                        </div>

                    </div>

                </div>

                
            
        </div>


    </div>



    <div id="dvTodaySpecimenlist" runat="server" visible="false" style="margin-left:30px;">
        <div class="row">
            <div class="alert alert-info" style="margin-left: 0px;">
                <h4>List of Today Specimen List</h4>
                <telerik:RadGrid ID="tgrdSpmlist" runat="server" AllowPaging="True" AllowSorting="True"
                    ItemStyle-CssClass="bigger-125" AlternatingItemStyle-CssClass="bigger-120" HeaderStyle-CssClass="bigger-130" HeaderStyle-Height="35"
                    BorderColor="#D7D7D7" GridLines="None" Visible="true" PageSize="10" OnPageIndexChanged="tgrdSpmlist_PageIndexChanged" OnPageSizeChanged="tgrdSpmlist_PageSizeChanged" OnItemDataBound="tgrdSpmlist_ItemDataBound" OnItemCommand="tgrdSpmlist_ItemCommand">
                    <MasterTableView AllowFilteringByColumn="False" AutoGenerateColumns="false" ShowFooter="false"
                        Width="100%">
                        <NoRecordsTemplate>
                            <div class="alert alert-info" style="margin-left: 20px; margin-right: 20px; margin-top: 20px;">
                                <center>No record to display</center>
                            </div>
                        </NoRecordsTemplate>

                        <Columns>
                            <telerik:GridBoundColumn DataField="SpecimenID" HeaderText="Specimen ID" Visible="false" />
                            <telerik:GridButtonColumn DataTextField="SpecimenNum" HeaderText="Specimen Number"
                                SortExpression="SpecimenNum" Text="Button" UniqueName="colSpecimenNum" CommandName="Create">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="CreatedON" HeaderText="Create Date" />

                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

        </div>

    </div>

    


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#fileupload").change(function () {
                $("#dvPreview").html("");
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                if (regex.test($(this).val().toLowerCase())) {


                    if (typeof (FileReader) != "undefined") {
                        $("#dvPreview").show();
                        $("#dvPreview").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $("#dvPreview img").attr("src", e.target.result);
                        }
                        reader.readAsDataURL($(this)[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }

                } else {
                    alert("Please upload a valid image file.");
                }
            });
        });
    </script>
</asp:Content>
