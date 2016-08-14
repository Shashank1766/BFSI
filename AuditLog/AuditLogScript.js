$(function () {
    $("#AuditLogFormDiv").validationEngine();
});

function SaveAuditLog(id) {
    var isValid = jQuery("#AuditLogFormDiv").validationEngine({ returnIsValid: true });
    if (isValid == true) {
             var txtID = $("#txtID").val();
             var txtUserId = $("#txtUserId").val();
             var dtCreatedDate = $("#dtCreatedDate").val();
             var txtTableName = $("#txtTableName").val();
             var txtFieldName = $("#txtFieldName").val();
             var txtPrimaryKey = $("#txtPrimaryKey").val();
             var txtOldValue = $("#txtOldValue").val();
             var txtNewValue = $("#txtNewValue").val();
             var txtPrimaryId = $("#txtPrimaryId").val();
             var txtSecondaryId = $("#txtSecondaryId").val();
             var txtEventType = $("#txtEventType").val();
             var txtExtValue1 = $("#txtExtValue1").val();
             var txtExtValue2 = $("#txtExtValue2").val();
             var txtExtValue3 = $("#txtExtValue3").val();
        var jsonData = JSON.stringify({
             ID: txtID
             UserId: txtUserId
             CreatedDate: dtCreatedDate
             TableName: txtTableName
             FieldName: txtFieldName
             PrimaryKey: txtPrimaryKey
             OldValue: txtOldValue
             NewValue: txtNewValue
             PrimaryId: txtPrimaryId
             SecondaryId: txtSecondaryId
             EventType: txtEventType
             ExtValue1: txtExtValue1
             ExtValue2: txtExtValue2
             ExtValue3: txtExtValue3
            //AuditLogId: id,
            //AuditLogMainPhone: txtAuditLogMainPhone,
            //AuditLogFax: txtAuditLogFax,
            //AuditLogLicenseNumberExpire: dtAuditLogLicenseNumberExpire,
            // 2MAPCOLUMNSHERE - AuditLog
        });
        $.ajax({
            type: "POST",
            url: '/AuditLog/SaveAuditLog',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                ClearAll();
                var msg = "Records Saved successfully !";
                if (id > 0)
                    msg = "Records updated successfully";

                ShowMessage(msg, "Success", "success", true);
            },
            error: function (msg) {

            }
        });
    }
}

function EditAuditLog(id) {
    var jsonData = JSON.stringify({
             ID: txtID
             UserId: txtUserId
             CreatedDate: dtCreatedDate
             TableName: txtTableName
             FieldName: txtFieldName
             PrimaryKey: txtPrimaryKey
             OldValue: txtOldValue
             NewValue: txtNewValue
             PrimaryId: txtPrimaryId
             SecondaryId: txtSecondaryId
             EventType: txtEventType
             ExtValue1: txtExtValue1
             ExtValue2: txtExtValue2
             ExtValue3: txtExtValue3
        Id: id
    });
    $.ajax({
        type: "POST",
        url: '/AuditLog/GetAuditLog',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: jsonData,
        success: function (data) {
            $('#AuditLogFormDiv').empty();
            $('#AuditLogFormDiv').html(data);
            $('#collapseAuditLogAddEdit').addClass('in');
            $("#AuditLogFormDiv").validationEngine();
        },
        error: function (msg) {

        }
    });
}

function DeleteAuditLog(id) {
    if (confirm("Do you want to delete this record? ")) {
        var txtAuditLogId = id;
        var jsonData = JSON.stringify({
             ID: txtID
             UserId: txtUserId
             CreatedDate: dtCreatedDate
             TableName: txtTableName
             FieldName: txtFieldName
             PrimaryKey: txtPrimaryKey
             OldValue: txtOldValue
             NewValue: txtNewValue
             PrimaryId: txtPrimaryId
             SecondaryId: txtSecondaryId
             EventType: txtEventType
             ExtValue1: txtExtValue1
             ExtValue2: txtExtValue2
             ExtValue3: txtExtValue3
            Id: id
        });
        $.ajax({
            type: "POST",
            url: '/AuditLog/DeleteAuditLog',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: jsonData,
            success: function (data) {
                if (data) {
                    BindAuditLogGrid();
                    ShowMessage("Records Deleted Successfully", "Sucess", "success", true);
                }
                else {
                    return false;
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}

function BindAuditLogGrid() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/AuditLog/BindAuditLogList",
        dataType: "html",
        async: true,
        data: null,
        success: function (data) {
            $("#AuditLogListDiv").empty();
            $("#AuditLogListDiv").html(data);
        },
        error: function (msg) {

        }

    });
}

function ClearForm() {
    
}

function ClearAll() {
    $("#AuditLogFormDiv").clearForm();
    $('#collapseAuditLogAddEdit').removeClass('in');
    $('#collapseAuditLogList').addClass('in');
    $("#AuditLogFormDiv").validationEngine();
    $.validationEngine.closePrompt(".formError", true);
    $.ajax({
        type: "POST",
        url: '/AuditLog/ResetAuditLogForm',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: null,
        success: function (data) {
            if (data) {
                $('#AuditLogFormDiv').empty();
                $('#AuditLogFormDiv').html(data);
                $('#collapseAuditLogList').addClass('in');
                BindAuditLogGrid();
            }
            else {
                return false;
            }
        },
        error: function (msg) {


            return true;
        }
    });

}




