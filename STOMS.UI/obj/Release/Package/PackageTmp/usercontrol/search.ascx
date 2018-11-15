<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="search.ascx.cs" Inherits="STOMS.UI.usercontrol.search" %>
<div class="well">
    <h4 class="blue"><asp:Literal ID="ltrSearchCap" runat="server" Text="Search Order or Test Record"></asp:Literal></h4>
    <div class="form-group">
        <asp:TextBox CssClass="form-control" ID="txtInput" runat="server" placeholder="Enter..."></asp:TextBox>
    </div>
    <div style="margin-top:5px;">
        <button class="btn btn-primary" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$search$lbtn1','');">
            <i class="fa fa-search"></i>&nbsp;&nbsp;Find
        </button>
        <asp:LinkButton ID="lbtn1" runat="server" Text="" OnClick="lbtn1_Click"></asp:LinkButton>
        <asp:HiddenField ID="hSearchMode" runat="server" />
    </div>
</div>
