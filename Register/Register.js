$(function () {
    $('#form-register').validationEngine();
    $("#form-register").clearForm();
    $("#txtMobileNumber").mask("+91 1234567890");
    if ($('#hidViewTitle').val() != '') {
        if ($('#hidViewTitle').val() == "Record Inserted Successfully!") {
            ShowMessage($('#hidViewTitle').val(), "Success!", "success", true);
        } else {
            ShowMessage($('#hidViewTitle').val(), "Info", "error", true);
        }
    }
    $('#btnRegister').on('click', function () {
        var isValid = jQuery("#frmCreateCategory").validationEngine({ returnIsValid: true });
        if (isValid) {
            if (!$('#chkAgree').prop('checked')) {
                ShowMessage('Please Agree the terms and condition', "Info", "error", true);
                return false;
            }   
        }
    });

    $('#btnReset').on('click', function () {
        $.validationEngine.closePrompt(".formError", true);
        $("#form-register").clearForm();
        return false;
    });
});

function disableBackButton() {
    window.history.forward();
}

var ValidateMobile = function () {
    if ($('#Phone').val() != '') {
        var jsonData = JSON.stringify({
            phonenumber: $('#Phone').val()
        });
        $.ajax({
            type: "POST",
            url: '/Home/ValidateMobile',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                if (data) {
                    ShowMessage('Mobile number already registered please contact Bidkaro team!', "Info", "error", true);
                    $('#Phone').val('');
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}

var ValidateMobile = function () {
    if ($('#UserId').val() != '') {
        var jsonData = JSON.stringify({
            userid: $('#UserId').val()
        });
        $.ajax({
            type: "POST",
            url: '/Home/ValidateUserId',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                if (data) {
                    ShowMessage('User Id already exists, please try another!', "Info", "error", true);
                    $('#UserId').val('');
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}

var ValidateEmail = function () {
    if ($('#Email').val() != '') {
        var jsonData = JSON.stringify({
            email: $('#Email').val()
        });
        $.ajax({
            type: "POST",
            url: '/Home/ValidateEmail',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                if (data) {
                    ShowMessage('Email id already exists in our system, please contact Bidkaro team!', "Info", "error", true);
                    $('#Email').val('');
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}

var ValidateUserId = function () {
    if ($('#UserId').val() != '') {
        var jsonData = JSON.stringify({
            userid: $('#UserId').val()
        });
        $.ajax({
            type: "POST",
            url: '/Home/ValidateUserId',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                if (data) {
                    ShowMessage('User Id is already taken, please try another user Id!', "Info", "error", true);
                    $('#UserId').val('');
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}