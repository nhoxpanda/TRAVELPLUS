
function OnSuccessAppointment() {
    $("#modal-insert-appointment").modal("hide");
    $("#modal-edit-appointment").modal("hide");
    $('form').trigger("reset");
    CKupdate();

    $("#modal-insert-contacthistory").modal("hide");
    $("#modal-edit-contacthistory").modal("hide");
}

function OnFailureAppointment() {
    alert("Lỗi, vui lòng kiểm tra lại!");
    $("#modal-insert-appointment").modal("hide");
    $("#modal-edit-appointment").modal("hide");
    $('form').trigger("reset");
    CKupdate();

    $("#modal-insert-contacthistory").modal("hide");
    $("#modal-edit-contacthistory").modal("hide");
}

/** xóa nhiệm vụ **/
function deleteTask(id) {
    if (confirm('Bạn thực sự muốn xóa mục này ?')) {
        var dataPost = {
            id: id,
            idstaff: $("#txtIdStaff").val()
        };
        $.ajax({
            type: "POST",
            url: '/StaffOtherTab/DeleteTask',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                alert("Xóa dữ liệu thành công!!!");
                $("#nhiemvu").html(data);
            }
        });
    }
}

/** cập nhật nhiệm vụ **/
function updateTask(id) {
    var dataPost = {
        id: id,
        idstaff: $("#txtIdStaff").val()
    };
        $.ajax({
            type: "POST",
            url: '/StaffOtherTab/EditTask',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                if (data == 1) {
                    alert('Bạn không được chỉnh sửa nhiệm vụ này!');
                }
                else {
                    $("#info-data-task").html(data);
                    $("#edit-task-type").select2();
                    $("#edit-timetype-task").select2();
                    $("#edit-priority-task").select2();
                    $("#edit-tour-task").select2();
                    $("#edit-status-task").select2();
                    CKEDITOR.replace("edit-note-tourtask");
                    $("#modal-edit-stafftask").modal("show");
                }
            }
        });
}

/** check visa **/
$("#insert-visa-staff").change(function () {
    var dataPost = {
        text: $("#insert-visa-staff").val()
    };
    $.ajax({
        type: "POST",
        url: '/StaffManage/CheckVisa',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data == "1") { // trùng
                if (!confirm("Dữ liệu trùng lắp! Bạn có muốn lưu lại không?")) {
                    $("#insert-visa-staff").val('');
                    $("#insert-visa-staff").focus();
                }
            }
        }
    });
});
