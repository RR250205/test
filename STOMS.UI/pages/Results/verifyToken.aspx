<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verifyToken.aspx.cs" Inherits="STOMS.UI.pages.Results.verifyToken" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
    <link href="../../assets/css/jquery-ui-1.10.3.full.min.css" rel="stylesheet" />
    <link href="../../assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/css/ace.min.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">


        <div class="row">
            <div class="col-lg-12">

                <div id="navbar" class="navbar navbar-default          ace-save-state">
                    <div class="navbar-container ace-save-state" id="navbar-container">
                        <div class="navbar-header pull-left">
                             <a href="../pages/dashboard" class="navbar-brand">
                        <i class=" icon-beaker white"></i>
                        <span class="white">LIMS</span>
                    </a>
                        </div>
                    </div>
                </div>

                <div class="main-content">
                    <div class="main-content-inner" style="margin-right: 176px;">
                        <div class="page-content">
                            <div class="page-header">
                               <asp:Label ID="lblDateExper" style="color: red; font-size: 20px;" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

             
            </div>

        </div>

        
    </form>

    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../assets/js/jquery-2.0.3.min.js"></script>
    <script src="../assets/js/ace.min.js"></script>
</body>


</html>





