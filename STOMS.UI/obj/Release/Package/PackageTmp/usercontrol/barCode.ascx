<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="barCode.ascx.cs" Inherits="STOMS.UI.usercontrol.barCode" %>

<link rel="stylesheet" href="/jplugin/barcode/barcode.css" />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="/jplugin/barcode/jquery-barcode.js"></script>
<script type="text/javascript">

    function generateBarcode() {
        var value = document.getElementById('<%= barcodeValue.ClientID %>').value;
        var btype = document.getElementById('<%= btype.ClientID %>').value;
        var renderer = 'css';

        var quietZone = false;

        var settings = {
            output: renderer,
            bgColor: '#FFFFFF',
            color: '#000000',
            barWidth: '1',
            barHeight: '50',
            moduleSize: '5',
            posX: '2',
            posY: '2',
            addQuietZone: 1
        };
        //if ($("#rectangular").is(':checked') || $("#rectangular").attr('checked')) {
        //    value = { code: value, rect: true };
        //}
        if (renderer == 'canvas') {
            clearCanvas();
            $("#barcodeTarget").hide();
            $("#canvasTarget").show().barcode(value, btype, settings);
        } else {
            $("#canvasTarget").hide();
            $("#barcodeTarget").html("").show().barcode(value, btype, settings);
        }
    }

    function showConfig1D() {
        $('.config .barcode1D').show();
        $('.config .barcode2D').hide();
    }

    function showConfig2D() {
        $('.config .barcode1D').hide();
        $('.config .barcode2D').show();
    }

    function clearCanvas() {
        var canvas = $('#canvasTarget').get(0);
        var ctx = canvas.getContext('2d');
        ctx.lineWidth = 1;
        ctx.lineCap = 'butt';
        ctx.fillStyle = '#FFFFFF';
        ctx.strokeStyle = '#000000';
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.strokeRect(0, 0, canvas.width, canvas.height);
    }

    $(function () {
        $('input[name=btype]').click(function () {
            if ($(this).attr('id') == 'datamatrix') showConfig2D(); else showConfig1D();
        });
        $('input[name=renderer]').click(function () {
            if ($(this).attr('id') == 'canvas') $('#miscCanvas').show(); else $('#miscCanvas').hide();
        });
        generateBarcode();
    });

    </script>

<div id="barcodeTarget" class="barcodeTarget"></div>
<canvas id="canvasTarget" width="150" height="150"></canvas>
<asp:HiddenField ID="barcodeValue" Value="1234567891012" runat="server" />
<asp:HiddenField ID="btype" Value="ean13" runat="server" />
<asp:HiddenField ID="renderer" Value="css" runat="server" />
<asp:HiddenField ID="bgColor" Value="#FFFFFF" runat="server" />
<asp:HiddenField ID="color" Value="#000000" runat="server" />
<asp:HiddenField ID="barWidth" Value="1" runat="server" />
<asp:HiddenField ID="barHeight" Value="50" runat="server" />
<asp:HiddenField ID="moduleSize" Value="5" runat="server" />
<asp:HiddenField ID="posX" Value="10" runat="server" />
<asp:HiddenField ID="posY" Value="20" runat="server" />
<asp:HiddenField ID="quietZoneSize" Value="1" runat="server" />