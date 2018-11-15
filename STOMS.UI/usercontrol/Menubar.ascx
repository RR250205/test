<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menubar.ascx.cs" Inherits="STOMS.UI.usercontrol.Menubar" %>
<script language="javascript">
    function getPage(srvID) {
        document.location = "/redirect.aspx?ID=" + srvID;
    }
</script>
<div id="sidebar">
        <asp:Literal ID="ltrstomMenu" runat="server"></asp:Literal>
    <!--/.nav-list-->
    <%-- <script type="text/javascript" language="javascript">
         function getPage(srvID) {
             document.location = "/redirect.aspx?ID=" + srvID;
         }
</script>--%>

   
</div>