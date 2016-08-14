function ShowMessage(msg, title, shortCutFunction, showCloseButton) {
    toastr.options = {
        "closeButton": showCloseButton,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "500",
        "hideDuration": "2000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $("#toastrOptions").text("Command: toastr["
                    + shortCutFunction
                    + "](\""
                    + msg
                    + (title ? "\", \"" + title : '')
                    + "\")\n\ntoastr.options = "
                    + JSON.stringify(toastr.options, null, 2)
    );

    var $toast = toastr[shortCutFunction](msg, title); // Wire up an event handler to a button in the toast, if it exists
    $toastlast = $toast;
}

var initTableWithSearch = function (tableId,searchtext) {
    var table = $(tableId);

    var settings = {
        "sDom": "<'table-responsive't><'row'<p i>>",
        "destroy": true,
        "scrollCollapse": true,
        "oLanguage": {
            "sLengthMenu": "_MENU_ ",
            "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
        },
        "iDisplayLength": 5
    };

    table.dataTable(settings);

    // search box for table
    $(searchtext).keyup(function () {
        table.fnFilter($(this).val());
    });
}

$(function () {
    $('#ankMyProfile').on('click', function () {
        window.location = window.location.protocol + "//" + window.location.host + "/Users/MyAccount";
        // Load user profile page
        //$.ajax({
        //    type: "POST",
        //    url: '/Users/GetUsers',
        //    async: false,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    data: null,
        //    success: function (data) {
        //        $('#divUserProfile').empty().html(data);
        //    },
        //    error: function (msg) {
        //        return true;
        //    }
        //});
    });
});