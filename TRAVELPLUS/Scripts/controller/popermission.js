var table = $('.dataTable').DataTable({
    order: [],
    columnDefs: [{ orderable: false, targets: [0] }]
});

$(".dataTable").dataTable().columnFilter({
    sPlaceHolder: "head:after",
    aoColumns: [null,
                { type: "text" },
                { type: "text" },
                { type: "text" }]
});

$("#btnEdit").click(function () {
    var dataPost = {
        id: $("input[type='checkbox']:checked").val()
    };

    $.ajax({
        type: "POST",
        url: '/POApprovalPermission/Edit',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-popermission").html(data);
            $("#modal-edit-popermission").modal("show");
            // khai báo datatable - filter column
            var tableStaff = $('#tableStaff').DataTable({
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }]
            });

            $("#tableStaff").dataTable().columnFilter({
                sPlaceHolder: "head:after",
                aoColumns: [null,
                            { type: "text" },
                            { type: "text" },
                            { type: "text" }]
            });
            // sự kiện click checkbox lấy ID
            $("#tableStaff").on("change", "#allstaff", function () {
                var $this = $(this);
                $("#listItemIdAddStaff").val('');
                var currentlistItemID = $("#listItemIdAddStaff").val();
                var ItemID = "";
                if ($this.prop("checked")) {
                    $(".checkAddStaff").each(function () {
                        ItemID = $(this).val();
                        if ($(this).parent().hasClass('text-danger') == false) {
                            $(this).prop("checked", true);
                            currentlistItemID += ItemID + ",";
                            $("#listItemIdAddStaff").val(currentlistItemID);
                        }
                    });
                } else {
                    $(".checkAddStaff").prop("checked", false);
                    $("#listItemIdAddStaff").val("");
                }
            });

            $("#tableStaff").on("change", ".checkAddStaff", function () {
                var ItemID = $(this).val();
                var currentlistItemID = $("#listItemIdAddStaff").val();
                var stringBranchID = "";
                if ($(this).prop('checked')) {
                    currentlistItemID += ItemID + ",";
                    $("#listItemIdAddStaff").val(currentlistItemID);
                } else {
                    $("#listItemIdAddStaff").val(currentlistItemID.replace(ItemID + ",", ""));
                }
            });
        }
    })
});
