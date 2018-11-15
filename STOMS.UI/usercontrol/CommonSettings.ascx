<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonSettings.ascx.cs" Inherits="STOMS.UI.usercontrol.CommonSettings" %>
<style type="text/css">
  
</style>

<div data-toggle="tooltip" id="tpSpecimen">
    <span>
        <h3>
            Specimen Number Generation
        </h3>
        <asp:CheckBox ID="chkSpecAutoGenerate" OnCheckedChanged="chkSpecAutoGenerate_CheckedChanged" AutoPostBack="true"  Text="Do you want generate specimen number manualy" runat="server" />
        <i class="fa fa-question-circle-o"  ></i>
    </span>
</div>
 
<script>
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $('#tpSpecimen').tooltip(
            {
                title: "You can create specimen number manualy and you can assign specimen number form already generated list ",
                animation: true
            }
        );
    });
   
</script>