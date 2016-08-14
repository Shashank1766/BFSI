$(function () {
    $('#btnRegister').on('click', function () {
        window.location = window.location.protocol + "//" + window.location.host + "/Users/Register";
    });

    $('#btnLogin').on('click', function () {
        var txtusername = $('#txtUserName').val();
        var txtpassword = $('#txtPassword').val();
        if (txtusername == '' || txtpassword == '') {
            ShowMessage('Please enter login information!', "Info", "error", true);
            return false;
        } else {
            var jsonData = JSON.stringify({
                username: txtusername,
                password: txtpassword
            });
            $.ajax({
                type: "POST",
                url: '/Home/LoginUser',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,
                success: function (data) {
                    if (data != 0) {
                        window.location = window.location.protocol + "//" + window.location.host + "/Users/UsersMain";
                    } else {
                        ShowMessage('Invalid login credentials, pelase contact bidKaro Team or click register.', "Info", "error", true);
                    }
                },
                error: function (msg) {
                    return true;
                }
            });
        }
    });
});