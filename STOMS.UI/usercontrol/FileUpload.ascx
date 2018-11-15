<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs" Inherits="STOMS.UI.usercontrol.OrgImageUpload" %>


  <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />    

    <style>
   .fileUpload {
    position: relative;
    overflow: hidden;
    margin: 10px;
}

    .fileUpload input.upload {
        position: absolute;
        top: 0;
        right: 0;
        margin: 0;
        padding: 0;
        font-size: 20px;
        cursor: pointer;
        opacity: 0;
        filter: alpha(opacity=0);
    }

        /*.label {
            font-weight: normal;
            font-size: 20px;
        }*/


    </style>

 <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
   

<div class="row">
      <div class="col-md-12" runat="server">
                                    <div class="row">
                                       <%-- <div style="margin-left: 18px;">
                                            <h4>Upload Test Request  form copy</h4>
                                        </div>--%>
                                        <div style="margin-right: 20px; margin-bottom: 5px; margin-left: 17px;">
                                            <!-- <div class="ace-file-input">
                                                <input type="file" id="id-input-file-1">
                                                <label class="ace-file-container" data-title="Choose">
                                                    <span class="ace-file-name" data-title="No File ...">
                                                        <i class=" ace-icon fa fa-upload"></i></span></label>
                                             

                                            </div>-->
                                            <input id="inSampleHardCopy" class="form-control" style="width: 90%; display: inline" placeholder="Choose File" disabled="disabled" />
                                           
                                             <div class="row">
                                               <div class="col-lg-6">

                                            <div class="fileUpload btn btn-primary" style="border: none">
                                                <span>Choose</span>
                                               <asp:FileUpload ID="flSampleHardCopy" runat="server" CssClass="upload" />
                                                <!--<input id="flSampleHardCopy1" runat="server"   type="file" class="upload" accept="image/x-png,image/jpeg" />-->
                                                <asp:HiddenField ID="fileUploadHandler" runat="server" OnValueChanged="fileUploadHandler_ValueChanged" Value=""  />
                                                
                                            </div>
                                             </div>
                                                  <div class="col-lg-6">

                                            <div onclick="clearPreview()" class="fileUpload btn btn-danger" style="border: none">
                                                <span>Remove</span>
                                            </div>

                                                  </div>
                                                 </div>
                                        </div>
                                        <div style="clear: right;"></div>
                                    </div>
                                   


          <div class="row">
               <div class="col-md-12" runat="server">

                  

                   </div>


          </div>


                                </div>


     <asp:HiddenField ID="hDocId" runat="server" />
    <asp:HiddenField ID="hDocNumber"  runat="server" />


    </div>



    
   




<script>

    $(function ()
    {
        $("#<%= flSampleHardCopy.ClientID%>").change(function () {
           
            $("#dvPreview").html("");
           // var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png|.bmp)$/;
            var fileName = $(this).val();
            var extention = (fileName.substr(fileName.lastIndexOf('.') + 1)).toLowerCase();

            if (extention == 'jpg' || extention == 'png' || extention == 'jpeg' || extention == 'bmp' || extention == 'gif') {


                if (typeof (FileReader) != "undefined") {
                    $("#dvPreview").show();
                    $("#dvPreview").append("<img />");
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("#dvPreview img").attr("src", e.target.result);
                        $("#dvPreview img").attr("width", '100%');
                    }
                    reader.readAsDataURL($(this)[0].files[0]);
                    $('#inSampleHardCopy').val($(this)[0].files[0].name);
                    $('#<%= fileUploadHandler.ClientID %>').val($(this)[0].files[0].name);
                       // document.getElementById('<%= fileUploadHandler.ClientID%>').value = "upload";
                        //__doPostBack('<%= fileUploadHandler.ClientID%>');


                    } else {
                        alert("This browser does not support FileReader.");
                    }

                } else {
                    alert("Please upload a valid image file.");
                }
        });

    });

    function clearPreview() {
        console.log("remove")
        $("#dvPreview").html("");
        $("#<%=flSampleHardCopy.ClientID%>").val('');
            $("#inSampleHardCopy").val('');
            $('#<%= fileUploadHandler.ClientID %>').val("delete");

    }
    function ClientSideClick(myButton) {
        // Client side validation
        if (typeof (Page_ClientValidate) == 'function') {
            if (Page_ClientValidate() == false)
            { return false; }
        }

        //make sure the button is not of type "submit" but "button"
        if (myButton.getAttribute('type') == 'button') {
            // disable the button
            myButton.disabled = true;
            myButton.className = "btn-inactive";
            myButton.value = "processing...";
        }


            <%--var chk = $("#<%=chkSpecimenReceivedKit.ClientID%>");
            if (chk[0].checked)
            {
                var x = $("#<%=txtKitNumber.ClientID%>").val();
                if (x == "")
                {
                    $('#spKitNumber').css("display", "block");
                    return false;
                }
                else
                {
                    $('#spKitNumber').css("display", "none");
                    return true;
                }              
            }
            else
            {
                $("#<%=ShowHidediv.ClientID%>").fadeOut('slow');
               
            }
            
            return true;--%>
        } 

</script>