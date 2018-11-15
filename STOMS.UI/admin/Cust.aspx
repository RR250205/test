<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="Cust.aspx.cs" Inherits="STOMS.UI.admin.Cust" %>

<%@ Register Src="~/usercontrol/custList.ascx" TagPrefix="uc1" TagName="custList" %>
<%--<div class="pace  pace-inactive">
    <div class="pace-progress" data-progress-text="100%" data-progress="99" style="width: 100%;">
  <div class="pace-progress-inner"></div>
</div>
<div class="pace-activity"></div>

</div>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-content">
        <div class="row">
            <div class="col-sm-12">
                <div style="border: 1px solid #c5d0dc; background-color: #ffffff; margin-bottom: 10px; min-height: 650px;">
                    <div style="margin: 10px 10px 10px 10px;">
                        <div class="row" style="margin-left:29px;"><h4>Customer Management</h4></div>
                        <div class="row" style="padding: 5px 5px 5px 10px; margin-right: 15px; vertical-align: middle;">
                            <div class="col-sm-9">
                                <uc1:custList runat="server" id="custList" />
                            </div>
                            <div class="col-sm-3">
          
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
