<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourierConfiguration.ascx.cs" Inherits="STOMS.UI.usercontrol.CourierConfiguration" %>


<style>
   h1, .h1{
        font-size: 23px;
        margin-left:29px;
    }
   
   .breadcrumb {
    padding: 8px 15px;
    margin-bottom: 20px;
    list-style: none;
    background-color: #f5f5f5;
    border-radius: 4px;
    top: 2px !important;
    float: right;
    margin-top: -47px;
}
.right-side  .content-header {
    position: relative;
    padding: 15px 15px 10px 20px;
}

 .row {
        margin-right: 0px;
    }

    #ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs-custom > .nav-tabs > li.active {
        border-top-color: #3c8dbc !important;
        border-left: none;
    }
    #ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs-custom > .nav-tabs > li {
        border-top: 3px solid transparent;
        margin-bottom: -2px;
        margin-right: 5px;
    }
    #ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs-custom > .tab-pane{
        background:white;
    }

</style>
<div class="row">
    
    <div class="">
        <div class="">
            <div class="" style="min-height: 600px;">
                
                
                <div class="col-lg-3">
                    <div class="alert alert-info" style="height: 477px; width: 100%;" role="alert">



                        <div class="list-group">
                            <asp:Repeater OnItemDataBound="rptcourierConfiguration_ItemDataBound" OnItemCommand="rptcourierConfiguration_ItemCommand" runat="server" ID="rptcourierConfiguration">
                                <HeaderTemplate>
                                    <div class="col-lg-12">
                                        <asp:Label Text="Courier Types" Font-Size="20px" runat="server"></asp:Label>
                                    </div>
                                    </br>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%--<asp:HiddenField runat="server" Value='<%# Eval("CourierTenantID") %>' ID="hCourierTenantId"/>--%>
                                    <%--                     <asp:HiddenField runat="server" Value='<%# Eval("CourierID") %>' ID="hCourierID" />--%>

                                    <%--<asp:HiddenField runat="server" Value='<%# Eval("EmailEnablementTypeID") %>' ID="hEmailEnablementTypeID" />--%>
                                    <%--<div class="list-group-item-text" padding: 10px;">--%>
                                    <div class="row">

                                        <asp:CheckBox ID="chkCourierType" Text='<%# Eval("CourierName") %>'
                                            
                                                runat="server" />
                                        <asp:HiddenField runat="server"  ID="hCourierID" Value='<%# Eval("CourierID") %>' />
                                        <asp:HiddenField runat="server" Value="0" ID="hCourierTenantId"/>

                                        
                                    </div>
                                    <div>
                                        
                                        
                                    </div>
                                    <%--  </div>--%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <br />
                    </div>
                </div>

                <div>
                    <asp:Label Text="choose your courier service" Style="font-size: 19px; margin-left: 25px;" ForeColor="#669900" runat="server" ID="lblinstruct" Visible="true"></asp:Label>
                </div>
                <div class="row">
                     <div class="col-lg-6">
                    <div id="dvCourierTypes" runat="server">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" id="nvtbcourier" runat="server">
                            </ul>


                            <div class="tab-pane" id="tabFedEx" style="display: none">
                                <div class="row">
                                    <div class="col-lg-6" >
                                      

                                           <div class="profile-user-info profile-user-info-striped"   style="width: 150%" >
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Meter No 
                                                    <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtMeterno" CssClass="form-control" Width="92%" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdMeterno" Display="Dynamic" ValidationGroup="vcourierConfig" ControlToValidate="txtMeterno" ForeColor="Red" ErrorMessage="Field is required" runat="server"> </asp:RequiredFieldValidator>
                                                         <asp:RegularExpressionValidator ValidationGroup="vcourierConfig" ID="rgValidation" runat="server" ForeColor="Red" ErrorMessage="Allow numbers Only" ControlToValidate="txtMeterno" ValidationExpression="^[0-9]*$" EnableClientScript="true"></asp:RegularExpressionValidator>  

                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Account No
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>

                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtFedexAcno" CssClass="form-control" Width="92%" runat="server"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="rfdFedexAcno" Display="Dynamic"  ValidationGroup="vcourierConfig" ControlToValidate="txtFedexAcno" runat="server" ForeColor="Red" ErrorMessage="Field is required"> </asp:RequiredFieldValidator>
                                                         <asp:RegularExpressionValidator ValidationGroup="vcourierConfig" ID="rgFedexAcno" runat="server" ForeColor="Red" ErrorMessage="Allow numbers Only" ControlToValidate="txtFedexAcno" ValidationExpression="^[0-9]*$" EnableClientScript="true"></asp:RegularExpressionValidator>  

                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    User Key 
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtUserkey" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdUserkey" Display="Dynamic"  ValidationGroup="vcourierConfig" runat="server" ForeColor="Red" ControlToValidate="txtUserkey" ErrorMessage="Field is required"></asp:RequiredFieldValidator> 
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    User PassWord
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtUserPassword" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdUserPassword" Display="Dynamic" runat="server" ValidationGroup="vcourierConfig" ControlToValidate="txtUserPassword" ForeColor="Red" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Parent Key
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtParentKey" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdParentKey" Display="Dynamic" runat="server" ValidationGroup="vcourierConfig" ControlToValidate="txtParentKey" ForeColor="Red" ErrorMessage="Field is required"></asp:RequiredFieldValidator>

                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Parent Password
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>

                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtParentPassword" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfdParentPassword" Display="Dynamic" runat="server" ValidationGroup="vcourierConfig" ControlToValidate="txtParentPassword" ForeColor="Red" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Default Weight
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtDefaltWeight" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ValidationGroup="vcourierConfig" ForeColor="Red" ControlToValidate="txtDefaltWeight" ID="rfdtxtDefaltWeight" runat="server" ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name" runat="server" visible="false">
                                                    Need Signature on delivery
                                                     <asp:Label Text="*" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                </div>
                                                <div class="profile-info-value" style="height: 53px">
                                                    <span>
                                                        <asp:CheckBox ID="chkSignature" runat="server" Enabled="false" />
                                                        <asp:HiddenField    runat="server" ID="hCourierConfigID"  Value="0"/>
                                                    </span>
                                                </div>
                                            </div>

                                            <div style="margin-top: 10px; text-align: center; margin-bottom: 10px;">
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-success" Width="94px" Text=" Submit"  OnClick="btnsubmit_Click"  ValidationGroup="vcourierConfig" />
                                                <asp:Button ID="btnview" runat="server" CssClass="btn btn-info" Text="View" Visible="false" OnClick="btnview_Click" />
                                            </div>

                                        </div>
                                      
                                        
                                    </div>

                                </div>

                            </div>
                        </div>
                        </div>
                        <%--courier view--%>

                        <div class="row">
                            <div  id="dvCourierconfigurations" runat="server" visible="false">
                                <div class="profile-user-info profile-user-info-striped" style="width: 94.5%;margin-left: 15px;">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            Meter No
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditMeterno" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            Account No
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditFedexAcno" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            User key
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditUserkey" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            UserPassword
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditUserPassword" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            ParentKey
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditParentKey" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            Parent Password                                            
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditParentPassword" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            Default Weight
                                        </div>
                                        <div class="profile-info-value">
                                            <span>
                                                <asp:Literal ID="litEditDefaltWeight" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>


                                    <div class="profile-info-row">
                                        <div class="profile-info-name">
                                            Need Signature on delivery
                                        </div>
                                        <div class="profile-info-value" style="height: 53px">
                                            <span>
                                                <asp:Literal ID="litEditSignature" runat="server"></asp:Literal><br />
                                            </span>
                                        </div>
                                    </div>


                                    <div style="margin-top: 10px; text-align: center; margin-bottom: 10px;">
                                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-success" Text="Edit" OnClick="btnEdit_Click" />

                                    </div>

                                </div>
                            </div>
                        </div>

                         <%--edit--%>
                         <%--<div class="col-lg-6" id="dvcourierEdit" runat="server" visible="false" >

                                        <div class="profile-user-info profile-user-info-striped"    style="width: 150%" >
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Meter No 
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtMeterno1" CssClass="form-control" Width="92%" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Account No
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtFedexAcno1" CssClass="form-control" Width="92%" runat="server"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    User Key 
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtUserkey1" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    User PassWord
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtUserPassword1" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Parent Key
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtParentKey1" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>

                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Parent Password

                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtParentPassword1" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Default Weight
                                                </div>
                                                <div class="profile-info-value">
                                                    <span>
                                                        <asp:TextBox ID="txtDefaltWeight1" runat="server" CssClass="form-control" Width="92%"></asp:TextBox>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="profile-info-row">
                                                <div class="profile-info-name">
                                                    Need Signature on delivery
                                                </div>
                                                <div class="profile-info-value" style="height: 53px">
                                                    <span>
                                                        <asp:CheckBox ID="txtSignature1" runat="server" />
                                                    </span>
                                                </div>
                                            </div>

                                            <div style="margin-top: 10px; text-align: center; margin-bottom: 10px;">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Width="94px" Text=" Submit" OnClick="btnsubmit_Click" />
                                               
                                            </div>

                                        </div>
                                    </div>--%>


                    </div>
                </div>
               
                </div>
            </div>


            <div class="tab-pane" id="tabUSPS" style="display: none">
                <div class="row">
                    <div class="col-lg-6">
                        saef
                                             <asp:Label ID="Label1" runat="server" Text="Hello"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
  <!-- Begin: Model window -->
    <div class="modal fade" id="dvDeleteConfirmation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header text-danger" >
                                                <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>Oops!
                                                </div>
                                                <div class="modal-body" id="modalBody">
                 You are trying to delete <b><span id="spCourierName"></span></b> Courier Service Configuration  <br />
                                                    Once you delete, the Configuration data is not recoverable 
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:LinkButton Text="proceed" CssClass="btn btn-default"  ID="lbtnProcess" OnClick="chkCourierType_CheckedChanged" runat="server" />
                                                   <input type="hidden" id="chkClientValue" />
                                                     <button id="btnModalCancel" type="button" class="btn btn-danger btn-ok" data-dismiss="modal">Cancel</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



<script src="../style/js/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        if ($('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes ul[class="nav nav-tabs"] li').hasClass("active")){
            var ele_A = $($('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes ul[class="nav nav-tabs"] li[class="active"] a[data-toggle="tab"]'));
            
            var tabs = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .tab-pane');
            var tabName = ele_A[0].hash.replace("#", '');

            for (var i = 0; i < tabs.length; i++) {
                if (tabs[i].id == tabName) {
                    $(ele_A[0].hash).css("display", "block");
                }
                else {
                    $('#' + tabs[i].id).css("display", "none");
                }

            }
        }
      

        $('input').on('ifChecked', function (e) {
            console.log((e.currentTarget.id));
            if (e.currentTarget.id != "ContentPlaceHolder1_ucCourierConfiguration_chkSignature") {
                __doPostBack("ctl00$ContentPlaceHolder1$ucCourierConfiguration$lbtnProcess", '');
            }
           
        });
        $('input').on('ifUnchecked', function (e) {
            console.log((e.currentTarget.id));
            $('#spCourierName').text($("label[for=" + e.currentTarget.id).text());
            $('#dvDeleteConfirmation').modal('toggle');
            $('#chkClientValue').val(e.currentTarget.id);
            //console.log($("label[for=" + e.currentTarget.id).text())
           // console.log("u")
            // __doPostBack(e.currentTarget, '');
            // $(this).trigger("click");
        });
       
        $('#dvDeleteConfirmation').on('hidden.bs.modal', function () {
            $('#' + $('#chkClientValue').val()).prop("checked", "checked");
            $('#' + $('#chkClientValue').val())[0].parentNode.classList.add("checked");
        })
   
    });
    $("#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes a[data-toggle='tab']").click(function () {

        var ele_A = $(this);
        console.log(ele_A)
        var tabs = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .tab-pane');
        var tabName = ele_A[0].hash.replace("#", '');
        var tabsLength= tabs.length;
        for (var i = 0; i < tabsLength; i++) {
            if (tabs[i].id == tabName) {
                $(ele_A[0].hash).css("display", "block");
            }
            else {
                $('#' + tabs[i].id).css("display", "none");
                console.log(ele_A.parent()[0].removeClass("active"))
            }

        }



        //  console.log(ele_A[0].hash)
    });
   

    $(document).ready(function () {
        var tabContent = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs-custom .tab-content');
        tabContent.css({
            'margin-left': 0,
        })
    });
    var tabsFn = (function () {

        function init() {
            setHeight();
        }

        function setHeight() {
            var $tabPane = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .tab-pane'),
                tabsHeight = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs').height();
            tabWidth = $('#ContentPlaceHolder1_ucCourierConfiguration_dvCourierTypes .nav-tabs').width();
        }

        $(init);
    })();
</script>

