$(document).ready(function () {
    $("#ContentPlaceHolder1_btnMitResultSave").click(function () {

        $("#ContentPlaceHolder1_hMitTestAnalysys").val($("#mitTestAnalysys").summernote('code'));

        $("#ContentPlaceHolder1_hMitInterpretation").val($("#mitInterpretation").summernote('code'));
        //$("#ContentPlaceHolder1_hMitNotes").val($("#mitNotes").summernote('code'));
    });
    $("#ContentPlaceHolder1_hMitTestAnalysys").on('change', function () {

        $("#mitTestAnalysys ").summernote("code", $(this).val());
        setsummernoteHeight();
    }).triggerHandler('change');
    $("#ContentPlaceHolder1_hMitInterpretation").on('change', function () {

        $("#mitInterpretation").summernote("code", $(this).val());
        setsummernoteHeight();
    }).triggerHandler('change');
    //$("#ContentPlaceHolder1_hMitNotes").on('change', function () {

    //    $("#mitNotes").summernote("code", $(this).val());
    //    setsummernoteHeight();
    //}).triggerHandler('change');

    function setsummernoteHeight() {
        //$('.richtext').summernote({
        //    height: 300
        //});
        $('.note-editable').css({ "height": "300px" });
    }

    $('.modal-trigger').click(function (e) {
      
        var header = $('#model-sp-Confirm .modal-header span');
        
        //var specimen = $(e.currentTarget.previousElementSibling.previousElementSibling.previousElementSibling.previousElementSibling).text();
        var specimen = $(e.currentTarget.parentElement.parentElement.firstElementChild.children[2]).text();
       
        //if (e.currentTarget.id.indexOf("Pending") !== -1) {
        //    header.html("Do you want to make the specimen <i>" + specimen+"</i> as active ? ");
        //    $('#model-sp-Confirm .modal-body #ModelFromPending').show();
        //    var options = $(this).next().val().split(',');
        //    if (options[0] === 
        //        "True")
        //        $('#ContentPlaceHolder1_SampPending_chkConsent').attr("checked", "true");
        //    if (options[1] === "True")
        //        $('#ContentPlaceHolder1_SampPending_chkReq').attr("checked", "true");
        //    if (options[2] !== "") {
                
        //        $('#spExceptions').html(options[2]);
        //        $('#ContentPlaceHolder1_SampPending_chkNoOther').removeAttr("checked", "true");
        //    }
        //    else {
        //        $('#ContentPlaceHolder1_SampPending_chkNoOther').attr("checked", "true");
        //        $('#dvExceptions').hide();
        //    }
                
        //}
         if (e.currentTarget.id.indexOf("Rejected") !== -1) {
            header.html("Specimen  <i>" + specimen + "</i>  is rejected for following reasons. do you want to make the specimen to active? ");
            $('#model-sp-Confirm .modal-body #ModelFromReject').show();
            $('#model-sp-Confirm .modal-body #ModelFromReject .reasons').append($(this).next().val());
            $('#ContentPlaceHolder1_SampRejected_btnUpdateStatus').val("Re-active");
        }
         $('#ContentPlaceHolder1_SampRejected_txtReasonForReactivate').val("");
         $('#ContentPlaceHolder1_SampRejected_hSelectedSpecimenID').val($(this).next().next().val());
        $('#model-sp-Confirm').addClass('in');
        $('#model-sp-Confirm').show();
        return false;
    });
    $('#ContentPlaceHolder1_SampRejected_btnUpdateStatus').click(function () {
       // var header = $('#model-sp-Confirm .modal-header span');
        console.log(this);
        
        var flag = 1;
        if ($(this).val()!=="Update") {
            var resnonForReactivate = $('#ContentPlaceHolder1_SampRejected_txtReasonForReactivate');
            if (resnonForReactivate.val() === "") {
                resnonForReactivate.css({
                    'borderColor': '#f56954'
                });
               // resnonForReactivate.focus();
                return false;
            }
        }
        //else {
        //    if ($('#ContentPlaceHolder1_SampPending_chkConsent').is(':not(:checked)')
        //        || $('#ContentPlaceHolder1_SampPending_chkReq').is(':not(:checked)')
        //        || $('#ContentPlaceHolder1_SampPending_chkNoOther').is(':not(:checked)')
        //    ) flag=0;
        //}
        if (flag === 0) {
            var error = $('#model-sp-Confirm .error');
            error.text("All parameters should be accept for update");
            error.show();
            return false;
        }
        else {
            return true;
        }
        
    });

    //<chkNoOther>
    $('#ContentPlaceHolder1_SampPending_chkNoOther').on('change', function () {

        if ($('#ContentPlaceHolder1_SampPending_chkNoOther').is(':checked'))
        {
            $('#dvExceptions').hide();
            return false;
        }

        else {
            $('#dvExceptions').show();
        }
        
    });

    //chkNoOther


    $('#ContentPlaceHolder1_SampPending_txtReasonForReactivate').on('change', function () {
       
        if ($(this).val().trim() !== "")
            $(this).css({
                'borderColor': '#d5d5d5'
            });
        else
            return;
    });
    $('#ContentPlaceHolder1_SampPending_txtReasonForReactivate').on('foucs', function () {

        if ($(this).val().trim() === "")
            $(this).css({
                'borderColor': '#f56954'
            });
        else
            return;
    });

});