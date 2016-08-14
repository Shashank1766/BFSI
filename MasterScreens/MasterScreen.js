$(function () {
    $('#frmCreateCategory').validationEngine();
    $('#btnAddNewVehicleType').on('click', function () {
        //AddGlobalCodeCategory();
        $('#divVehicleTypeAddEdit').show();
    });
    $('#btnSaveCategory').on('click', function () {
        var isValid = jQuery("#frmCreateCategory").validationEngine({ returnIsValid: true });
        if (!isValid) {
            ShowMessage('Fill the values first', "Info", "error", true);
            return false;
        } else {
            AddGlobalCodeCategory();
        }
    });
});

var AddGlobalCodeCategory = function () {
    var jsonData = JSON.stringify({
        name: $('#txtGlobalCodeCategoryName').val(),
        value: $('#txtGlobalCodeCategoryValue').val(),
    });
    $.ajax({
        type: "POST",
        url: '/MasterScreen/AddGlobalCodeCategory',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: jsonData,
        success: function (data) {
            $('#divVehicleTypeListing').empty().html();
        },
        error: function (msg) {
            return true;
        }
    });
};