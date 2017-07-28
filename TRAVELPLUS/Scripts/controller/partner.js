CKEDITOR.replace("insert-note-schedule");
CKEDITOR.replace("edit-note-schedule");
CKEDITOR.replace('insert-noteservicepartner1');
CKEDITOR.replace("insert-note-contractpartner");
$("#partner-services").select2();
$("#partner-tags").select2();
$("#insert-provincepartner").select2();
$("#insert-servicepartner").select2();
$("#insert-countrypartner").select2();
$("#insert-addresspartner").select2();
$("#insert-ngayhen-lichhen").val(moment(new Date()).format("YYYY-MM-DD") + "T08:30");
$("#insert-currency1").select2();

var table = $('.dataTable').DataTable({
    order: [],
    columnDefs: [{ orderable: false, targets: [0] }]
});

$(".dataTable").dataTable().columnFilter({
    sPlaceHolder: "head:after",
    aoColumns: [null,
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" }]
});

$('a.toggle-vis').on('click', function (e) {
    e.preventDefault();
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $(this).addClass('selected');
    }
    // Get the column API object
    var column = table.column($(this).attr('data-column'));
    // Toggle the visibility
    column.visible(!column.visible());
});

function OnSuccessPartner() {
    $("#modal-insert-note").modal("hide");
    $("#modal-edit-note").modal("hide");
    $("#modal-insert-contractpartner").modal("hide");
    $("#modal-edit-contractpartner").modal("hide");
    $("#modal-insert-servicepartner").modal("hide");
    $("#modal-edit-servicepartner").modal("hide");
    $("#modal-insert-evaluation").modal("hide");
    $("#modal-edit-evaluation").modal("hide");
    $('form').trigger("reset");
    CKupdate();
}

function OnFailurePartner() {
    alert("Lỗi, vui lòng kiểm tra lại!");
    $("#modal-insert-document").modal("hide");
    $("#modal-edit-document").modal("hide");
    $("#modal-insert-contractpartner").modal("hide");
    $("#modal-edit-contractpartner").modal("hide");
    $("#modal-insert-servicepartner").modal("hide");
    $("#modal-edit-servicepartner").modal("hide");
    $("#modal-insert-evaluation").modal("hide");
    $("#modal-edit-evaluation").modal("hide");
    $('form').trigger("reset");
    CKupdate();
}

function OnFailureDocument() {
    alert("Lỗi, vui lòng kiểm tra lại!");
    $("#modal-insert-document").modal("hide");
    $('form').trigger("reset");
    $("#modal-edit-document").modal("hide");
    CKupdate();
}

function OnSuccessDocument() {
    $("#modal-insert-document").modal("hide");
    $("#modal-edit-document").modal("hide");
    $('form').trigger("reset");
    CKupdate();
}

///*** duplicate form add service partner ***/
$(function () {
    $('#btnAdd').click(function () {
        var num = $('.clonedInput').length, // how many "duplicatable" input fields we currently have
            newNum = new Number(num + 1),      // the numeric ID of the new input field being added
            newElem = $('#entry' + num).clone().attr('id', 'entry' + newNum).fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
        // manipulate the name/id values of the input inside the new element

        newElem.find('.servicename').attr('name', 'PartnerServiceName' + newNum).val('');
        newElem.find('.serviceprice').attr('name', 'PartnerServicePrice' + newNum).val('');
        newElem.find('.servicenote').attr('id', 'insert-noteservicepartner' + newNum).attr('name', 'PartnerServiceNote' + newNum).val('');
        newElem.find('.servicecurrency').attr('id', 'insert-currency' + newNum).attr('name', 'PartnerServiceCurrency' + newNum);

        // insert the new element after the last "duplicatable" input field
        $('#entry' + num).after(newElem);
        CKEDITOR.replace("insert-noteservicepartner" + newNum);
        $("#insert-currency" + newNum).select2();

        for (var i = 1; i < newNum; i++) {
            $("#entry" + newNum).find("#cke_insert-noteservicepartner" + i).remove();
            $("#entry" + newNum + " #select2-insert-currency" + i + "-container").parent().parent().parent().remove();
        }

        // enable the "remove" button
        $('#btnDel').attr('disabled', false);

        // count service
        $("#countService").val(newNum);
    });

    $('#btnDel').click(function () {
        // confirmation
        var num = $('.clonedInput').length;
        // how many "duplicatable" input fields we currently have
        $('#entry' + num).slideUp('slow', function () {
            $(this).remove();
            // if only one element remains, disable the "remove" button
            if (num - 1 === 1)
                $('#btnDel').attr('disabled', true);
            // enable the "add" button
            $('#btnAdd').attr('disabled', false).prop('value', "add section");
        });
        return false;

        $('#btnAdd').attr('disabled', false);
        // count service
        $("#countService").val(num);
    });
    //$('#btnDel').attr('disabled', true);
});



$(function () {
    $('#btnAddBank').click(function () {
        var num = $('.clonedInputBank').length, // how many "duplicatable" input fields we currently have
            newNum = new Number(num + 1),      // the numeric ID of the new input field being added
            newElem = $('#bankClone' + num).clone().attr('id', 'bankClone' + newNum).fadeIn('slow');

        newElem.find('.bankName').attr('name', 'TenNganHang' + newNum).val('');
        newElem.find('.bankAccount').attr('name', 'SoTaiKhoan' + newNum).val('');
        newElem.find('.bankNote').attr('name', 'BankNote' + newNum).val('');

        newElem.find('.bankNameClass').attr('id', 'lblname' + newNum).text("Tên ngân hàng " + newNum);
        $('#lblname1').text("Tên ngân hàng 1");


        newElem.find('.bankAccClass').attr('id', 'lblaccount' + newNum).text("Số tài khoản " + newNum);
        $('#lblaccount1').text("Số tài khoản 1");


        newElem.find('.bankNoteClass').attr('id', 'lblbankNote' + newNum).text("Chủ tài khoản " + newNum);
        $('#lblbankNote1').text("Chủ tài khoản 1");

        $('#bankClone' + num).after(newElem);

        $('#btnDelBank').attr('disabled', false);
        $("#countSTK").val(newNum);
    });

    $('#btnDelBank').click(function () {
        var num = $('.clonedInputBank').length;
        $('#bankClone' + num).slideUp('slow', function () {
            $(this).remove();
            if (num - 1 === 1) {
                $('#lblname1').text("Tên ngân hàng");
                $('#lblaccount1').text("Số tài khoản");
                $('#lblbankNote1').text("Chủ tài khoản");
                $('#btnDelBank').attr('disabled', true);
            }
            $('#btnAddBank').attr('disabled', false).prop('value', "add section");
        });
        return false;

        $('#btnAddBank').attr('disabled', false);

    });

});


///****** Sửa thông tin ******/
$("#btnEdit").click(function () {
    $('#btnAddE').attr('disabled', false);
    var dataPost = {
        id: $("input[type='checkbox']:checked").val()
    };

    $.ajax({
        type: "POST",
        url: '/PartnerManage/PartnerInfomation',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#edit-partner").html(data);
            //CKEDITOR.replace('edit-noteservicepartnerE1');
            $("#edit-servicepartner").select2();
            $("#edit-countrypartner").select2();
            $("#edit-provincepartner").select2();
            $("#edit-addresspartner").select2();
            for (var i = 1 ; i <= $('#countServiceE').val() ; i++) {
                CKEDITOR.replace('edit-noteservicepartnerE' + i);
                $('#edit-currencyE' + i).select2();
            }
            $("#modal-edit-partner").modal("show");

            ///*** duplicate form add service partner ***/
            $(function () {
                $('#btnAddE').click(function () {
                    var num = $('.clonedInputE').length, // how many "duplicatable" input fields we currently have
                        newNum = new Number(num + 1),      // the numeric ID of the new input field being added
                        newElem = $('#entryE' + num).clone().attr('id', 'entryE' + newNum).fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
                    // manipulate the name/id values of the input inside the new element

                    newElem.find('.servicenameE').attr('name', 'PartnerServiceNameE' + newNum).val('');
                    newElem.find('.servicepriceE').attr('name', 'PartnerServicePriceE' + newNum).val('');
                    newElem.find('.servicenoteE').attr('id', 'edit-noteservicepartnerE' + newNum).attr('name', 'PartnerServiceNoteE' + newNum).val('');
                    newElem.find('.servicecurrencyE').attr('id', 'edit-currencyE' + newNum).attr('name', 'PartnerServiceCurrencyE' + newNum).val('');

                    // insert the new element after the last "duplicatable" input field

                    $('#entryE' + num).after(newElem);
                    CKEDITOR.replace("edit-noteservicepartnerE" + newNum)


                    //for (var i = 1; i < newNum; i++) {
                    newElem.find("#cke_edit-noteservicepartnerE" + num).remove();
                    $("#entry" + newNum + " #select2-edit-currencyE" + i + "-container").parent().parent().parent().remove();
                    $("#edit-currencyE" + newNum).select2();

                    // enable the "remove" button
                    $('#btnDelE').attr('disabled', false);

                    // count service
                    $("#countServiceE").val(newNum);
                });

                $('#btnDelE').click(function () {
                    // confirmation
                    var num = $('.clonedInputE').length;
                    // how many "duplicatable" input fields we currently have
                    $('#entryE' + num).slideUp('slow', function () {
                        $(this).remove();
                        // if only one element remains, disable the "remove" button
                        if (num - 1 === 1)
                            $('#btnDelE').attr('disabled', true);
                        // enable the "add" button
                        $('#btnAddE').attr('disabled', false).prop('value', "add section");
                    });
                    return false;

                    $('#btnAddE').attr('disabled', false);
                    // count service
                    $("#countServiceE").val(num);
                });
            });
        }
    });
});

/** Xoa du lieu **/
$("#btnRemove").on("click", function () {
    if ($("#listItemId").val() == "") {
        alert("Vui lòng chọn mục cần xóa !");
        return false;
    }
    var $this = $(this);
    var $tableWrapper = $("#tableDictionary-Wrapper");
    var $table = $("#tableDictionary");

    DeleteSelectedItem($this, $tableWrapper, $table, function (data) {

    });
    return false;
});

$("#tableDictionary").on("change", ".cbItem", function () {
    var ItemID = $(this).val();
    var currentlistItemID = $("#listItemId").val();
    var stringBranchID = "";
    if ($(this).prop('checked')) {
        currentlistItemID += ItemID + ",";
        $("#listItemId").val(currentlistItemID);
    } else {
        $("#listItemId").val(currentlistItemID.replace(ItemID + ",", ""));
    }
});

$("#tableDictionary").on("change", "#allcb", function () {
    var $this = $(this);
    $("#listItemId").val('');
    var currentlistItemID = $("#listItemId").val();
    var ItemID = "";
    if ($this.prop("checked")) {
        $(".cbItem").each(function () {
            ItemID = $(this).val();
            if ($(this).parent().hasClass('text-danger') == false) {
                $(this).prop("checked", true);
                currentlistItemID += ItemID + ",";
                $("#listItemId").val(currentlistItemID);
            }
        });
    } else {
        $(".cbItem").prop("checked", false);
        $("#listItemId").val("");
    }
});

function DeleteSelectedItem(selector, tableWrapper, table, callBack) {
    if (!confirm("Bạn thực sự muốn xóa những mục đã chọn ?")) {
        return false;
    }
    var $form = selector.next("form");
    var options = {
        url: $form.attr("action"),
        type: $form.attr("method"),
        data: $form.serialize(),
    };

    tableWrapper.append("<div class='layer'></div>");

    $.ajax(options).done(function (data) {
        tableWrapper.find(".layer").remove();
        if (data.Succeed) {
            alert(data.Message);
            if (data.IsPartialView) {
                table.replaceWith(data.View);
            }
            else {
                if (data.RedirectTo != null && data.RedirectTo != "") {
                    window.location.href = data.RedirectTo;
                }
            }
        }
        else {
            alert(data.Message);
        }
    });
}

/** chọn từng dòng để hiển thị thông tin chi tiết dưới các tab **/
$("table#tableDictionary").delegate("tr", "click", function (e) {
    $('tr').not(this).removeClass('oneselected');
    $(this).toggleClass('oneselected');
    var tab = $(".tab-content").find('.active').data("id");
    var dataPost = { id: $(this).find("input[type='checkbox']").val() };
    switch (tab) {
        case 'thongtinchitiet':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoThongTinChiTiet',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#thongtinchitiet").html(data);
                }
            });
            break;
        case 'lichhen':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoLichHen',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#lichhen").html(data);
                }
            });
            break;
        case 'tourtuyen':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoTourTuyen',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#tourtuyen").html(data);
                }
            });
            break;
        case 'lichsulienhe':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoLichSuLienHe',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#lichsulienhe").html(data);
                }
            });
            break;
        case 'danhgia':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoDanhGia',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#danhgia").html(data);
                }
            });
            break;
        case 'dichvucungcap':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoDichVuCungCap',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#dichvucungcap").html(data);
                }
            });
            break;
        case 'invoice':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoInvoice',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#invoice").html(data);
                }
            });
            break;
        case 'tailieumau':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoTaiLieuMau',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#tailieumau").html(data);
                }
            });
            break;
        case 'ghichu':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoGhiChu',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#ghichu").html(data);
                }
            });
            break;
        case 'capnhatthaydoi':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoCapNhatThayDoi',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#capnhatthaydoi").html(data);
                }
            });
            break;
        case 'hopdongdoitac':
            $.ajax({
                type: "POST",
                url: '/PartnerTabInfo/InfoHopDong',
                data: JSON.stringify(dataPost),
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                    $("#hopdongdoitac").html(data);
                }
            });
            break;
    }
});

/** click chọn từng tab -> hiển thị thông tin **/
$("#tabthongtinchitiet").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoThongTinChiTiet',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#thongtinchitiet").html(data);
            }
        });
    }
});

$("#tabhopdongdoitac").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoHopDong',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#hopdongdoitac").html(data);
            }
        });
    }
});

$("#tablichhen").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoLichHen',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#lichhen").html(data);
            }
        });
    }
});

$("#tabtourtuyen").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoTourTuyen',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#tourtuyen").html(data);
            }
        });
    }
});

$("#tabtailieumau").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoTaiLieuMau',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#tailieumau").html(data);
            }
        });
    }
});

$("#tablichsulienhe").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoLichSuLienHe',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#lichsulienhe").html(data);
            }
        });
    }
});

$("#tabdanhgia").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoDanhGia',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#danhgia").html(data);
            }
        });
    }
});

$("#tabdichvucungcap").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoDichVuCungCap',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#dichvucungcap").html(data);
            }
        });
    }
});

$("#tabghichu").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoGhiChu',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#ghichu").html(data);
            }
        });
    }
});

$("#tabinvoice").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoInvoice',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#invoice").html(data);
            }
        });
    }
});

$("#tabcapnhatthaydoi").click(function () {
    if ($("table#tableDictionary").find('tr.oneselected').length === 0) {
        alert("Vui lòng chọn 1 đối tác!");
    }
    else {
        var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
        $.ajax({
            type: "POST",
            url: '/PartnerTabInfo/InfoCapNhatThayDoi',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#capnhatthaydoi").html(data);
            }
        });
    }
});

/** upload file **/
$('#FileName').change(function () {
    var data = new FormData();
    data.append('FileName', $('#FileName')[0].files[0]);
    var ajaxRequest = $.ajax({
        type: "POST",
        url: 'PartnerManage/UploadFile',
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (xhr, textStatus) {
        // Onsuccess
    });
});

/** xóa tài liệu **/
function deleteDocument(id) {
    if (confirm('Bạn thực sự muốn xóa mục này ?')) {
        var dataPost = { id: id };
        $.ajax({
            type: "POST",
            url: '/PartnerManage/DeleteDocument',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                alert("Xóa dữ liệu thành công!!!");
                $("#tailieumau").html(data);
            }
        });
    }
}

/** cập nhật tài liệu **/
function updateDocument(id) {
    var dataPost = { id: id };
    $.ajax({
        type: "POST",
        url: '/PartnerManage/EditInfoDocument',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#info-data-doc").html(data);
            $("#edit-tag-document").select2();
            $("#edit-document-type").select2();
            CKEDITOR.replace("edit-document-note");
            $("#modal-edit-document").modal("show");

            /**** update in tab file của khách hàng ****/
            $("#btnUpdateFile").click(function () {
                var $this = $(this);
                var $form = $("#frmUpdateFile");
                var $parent = $form.parent();
                var options = {
                    url: $form.attr("action"),
                    type: $form.attr("method"),
                    data: $form.serialize()
                };

                $.ajax(options).done(function (data) {
                    $("#modal-edit-document").modal("hide");
                    alert("Lưu dữ liệu thành công!");
                    $("#hosolienquan").html(data);
                });
                return false;
            });

            /** upload file **/
            $("#edit-file").change(function () {
                var data = new FormData();
                data.append('FileName', $('#edit-file')[0].files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: 'PartnerManage/UploadFile',
                    contentType: false,
                    processData: false,
                    data: data
                });

                ajaxRequest.done(function (xhr, textStatus) {
                    // Onsuccess
                });
            });
        }
    });
}

///**** filter tag ***/
$("#btnSearchTags").click(function () {
    var dataPost = {
        tags: $("#partner-tags").val()
    };
    $.ajax({
        type: "POST",
        url: '/PartnerManage/FilterTags',
        data: '{"tags": "' + $("#partner-tags").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-partner").html(data);
            var table = $('.dataTable').DataTable({
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }]
            });
            $(".dataTable").dataTable().columnFilter({
                sPlaceHolder: "head:after",
                aoColumns: [null,
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" }]
            });
            // kéo dài kích thước cột
            colResize();
            $('a.toggle-vis').on('click', function (e) {
                e.preventDefault();
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    $(this).addClass('selected');
                }
                // Get the column API object
                var column = table.column($(this).attr('data-column'));
                // Toggle the visibility
                column.visible(!column.visible());
            });
            $("table#tableDictionary").delegate("tr", "click", function (e) {
                $('tr').not(this).removeClass('oneselected');
                $(this).toggleClass('oneselected');
                var tab = $(".tab-content").find('.active').data("id");
                var dataPost = { id: $(this).find("input[type='checkbox']").val() };
                switch (tab) {
                    case 'thongtinchitiet':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoThongTinChiTiet',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#thongtinchitiet").html(data);
                            }
                        });
                        break;
                    case 'lichhen':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoLichHen',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#lichhen").html(data);
                            }
                        });
                        break;
                    case 'tourtuyen':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoTourTuyen',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#tourtuyen").html(data);
                            }
                        });
                        break;
                    case 'lichsulienhe':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoLichSuLienHe',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#lichsulienhe").html(data);
                            }
                        });
                        break;
                    case 'danhgia':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoDanhGia',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#danhgia").html(data);
                            }
                        });
                        break;
                    case 'dichvucungcap':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoDichVuCungCap',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#dichvucungcap").html(data);
                            }
                        });
                        break;
                    case 'invoice':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoInvoice',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#invoice").html(data);
                            }
                        });
                        break;
                    case 'tailieumau':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoTaiLieuMau',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#tailieumau").html(data);
                            }
                        });
                        break;
                    case 'ghichu':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoGhiChu',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#ghichu").html(data);
                            }
                        });
                        break;
                }
            });
        }
    });
});

///*** filter service ***/
function clickChangeService() {
    var dataPost = {
        id: $("#partner-services").val()
    };
    $.ajax({
        type: "POST",
        url: '/PartnerManage/FilterService',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#idService").val($("#partner-services").val());
            $("#data-partner").html(data);
            var table = $('.dataTable').DataTable({
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }]
            });
            $(".dataTable").dataTable().columnFilter({
                sPlaceHolder: "head:after",
                aoColumns: [null,
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" },
                            { type: "text" }]
            });
            // kéo dài kích thước cột
            colResize();
            $('a.toggle-vis').on('click', function (e) {
                e.preventDefault();
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    $(this).addClass('selected');
                }
                // Get the column API object
                var column = table.column($(this).attr('data-column'));
                // Toggle the visibility
                column.visible(!column.visible());
            });

            $("table#tableDictionary").delegate("tr", "click", function (e) {
                $('tr').not(this).removeClass('oneselected');
                $(this).toggleClass('oneselected');
                var tab = $(".tab-content").find('.active').data("id");
                var dataPost = { id: $(this).find("input[type='checkbox']").val() };
                switch (tab) {
                    case 'thongtinchitiet':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoThongTinChiTiet',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#thongtinchitiet").html(data);
                            }
                        });
                        break;
                    case 'lichhen':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoLichHen',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#lichhen").html(data);
                            }
                        });
                        break;
                    case 'tourtuyen':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoTourTuyen',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#tourtuyen").html(data);
                            }
                        });
                        break;
                    case 'lichsulienhe':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoLichSuLienHe',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#lichsulienhe").html(data);
                            }
                        });
                        break;
                    case 'danhgia':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoDanhGia',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#danhgia").html(data);
                            }
                        });
                        break;
                    case 'dichvucungcap':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoDichVuCungCap',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#dichvucungcap").html(data);
                            }
                        });
                        break;
                    case 'invoice':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoInvoice',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#invoice").html(data);
                            }
                        });
                        break;
                    case 'tailieumau':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoTaiLieuMau',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#tailieumau").html(data);
                            }
                        });
                        break;
                    case 'ghichu':
                        $.ajax({
                            type: "POST",
                            url: '/PartnerTabInfo/InfoGhiChu',
                            data: JSON.stringify(dataPost),
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (data) {
                                $("#ghichu").html(data);
                            }
                        });
                        break;
                }
            });
        }
    });
}

/** xóa ghi chú **/
function deleteNote(id) {
    if (confirm('Bạn thực sự muốn xóa mục này ?')) {
        var dataPost = { id: id };
        $.ajax({
            type: "POST",
            url: '/PartnerManage/DeleteNote',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                alert("Xóa dữ liệu thành công!!!");
                $("#ghichu").html(data);
            }
        });
    }
}

/** cập nhật ghi chú **/
function updateNote(id) {
    var dataPost = { id: id };
    $.ajax({
        type: "POST",
        url: '/PartnerManage/EditInfoNote',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#info-data-note").html(data);
            CKEDITOR.replace("edit-note-partner");
            $("#modal-edit-note").modal("show");
        }
    });
}

///****** Sửa thông tin ******/
$("#btnCreateMap").click(function () {
    var dataPost = {
        id: $("input[type='checkbox']:checked").val()
    };

    $.ajax({
        type: "POST",
        url: '/PartnerManage/EditLocation',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-map").html(data);

            $('#modal-insert-map').on('show.bs.modal', function () {
                //Must wait until the render of the modal appear, thats why we use the resizeMap and NOT resizingMap!! ;-)
                resizeMap();
            })

            function resizeMap() {
                if (typeof map == "undefined") return;
                setTimeout(function () { resizingMap(); }, 400);
            }

            function resizingMap() {
                if (typeof map == "undefined") return;
                var center = map.getCenter();
                google.maps.event.trigger(map, "resize");
                map.setCenter(center);
            }

            initialize();

            $("#modal-insert-map").modal("show");

        }
    })
});

$("#insert-check-notify").click(function () {
    if (this.checked) {
        $("#insert-nhactruoc-lichhen").removeAttr("disabled", "disabled");
        $("#insert-nhactruoc-lichhen").select2();
    }
    else {
        $("#insert-nhactruoc-lichhen").attr("disabled", "disabled");
    }
});

$("#insert-check-repeat").click(function () {
    if (this.checked) {
        $("#insert-laplai-lichhen").removeAttr("disabled", "disabled");
        $("#insert-laplai-lichhen").select2();
    }
    else {
        $("#insert-laplai-lichhen").attr("disabled", "disabled");
    }
});

/**** get code tour form insert ****/
function getcode() {
    var dataPost = { id: $("#insert-servicepartner").val() };
    $.ajax({
        type: "POST",
        url: '/PartnerManage/GetCodePartner',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#Code").val(data);
        }
    });
};

//fileImport customer
$('#fileImport').change(function () {
    var data = new FormData();
    data.append('FileName', $('#fileImport')[0].files[0]);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: '/PartnerManage/ImportFile',
        contentType: false,
        processData: false,
        data: data
    });

    ajaxRequest.done(function (xhr, textStatus) {
        // Onsuccess
    });
    ajaxRequest.success(function (data) {
        $("#listItemIdI").val("");
        $("#import-data-partner").html(data);
    })
});
// save file contract
$('#insert-contract-partner').change(function () {
    var data = new FormData();
    data.append('fileUpload', $('#insert-contract-partner')[0].files[0]);

    var ajaxRequest = $.ajax({
        type: "POST",
        url: '/PartnerOtherTab/UploadFileContract',
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        // Onsuccess
    });
});

//////////// dịch vụ của đối tác ///////////////
/** add service **/
function createService(id) {
    var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
    $.ajax({
        type: "POST",
        url: '/PartnerOtherTab/InsertService',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#edit-partner").html(data);
            $("#insert-currency-service").select2();
            CKEDITOR.replace("insert-note-servicepartner");
            $("#modal-insert-servicepartner").modal("show");
        }
    });
}

/** update service **/
function updateService(id) {
    var dataPost = { id: id };
    $.ajax({
        type: "POST",
        url: '/PartnerOtherTab/EditService',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#edit-partner").html(data);
            $("#edit-currency-service").select2();
            CKEDITOR.replace("edit-note-servicepartner");
            $("#modal-edit-servicepartner").modal("show");
        }
    });
}
/** xóa dịch vụ **/
function deleteService(id) {
    if (confirm('Bạn thực sự muốn xóa mục này ?')) {
        var dataPost = { id: id };
        $.ajax({
            type: "POST",
            url: '/PartnerOtherTab/DeleteServicePartner',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                alert("Xóa dữ liệu thành công!!!");
                $("#dichvucungcap").html(data);
            }
        });
    }
}
///////// đánh giá đối tác
/** thêm mới đánh giá **/
function btnCreateEvaluation() {
    var dataPost = { id: $("table#tableDictionary").find('tr.oneselected').find("input[type='checkbox']").val() };
    $.ajax({
        type: "POST",
        url: '/PartnerOtherTab/InsertEvaluation',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#edit-partner").html(data);
            $("#insert-tour-evaluation").select2();
            $("#insert-area-evaluation").select2();
            $("#insert-province-evaluation").select2();
            $("#insert-evaluation-ep1").select2();
            $("#modal-insert-evaluation").modal("show");
            AddDuplicate();
            $("#insert-area-evaluation").change(function () {
                $.getJSON('/EvaluationPartnerManage/ProvinceList?id=' + $('#insert-area-evaluation').val(), function (data) {
                    var items = '<option value=0>Chọn tỉnh thành</option>';
                    $.each(data, function (i, gd) {
                        items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                    });
                    $('#insert-province-evaluation').html(items);
                    $('#insert-province-evaluation').select2();
                });
            })

        }
    });
}
/** cập nhật đánh giá **/
function updateEvaluation(id) {
    var dataPost = { id: id };
    $.ajax({
        type: "POST",
        url: '/PartnerOtherTab/EditEvaluation',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#edit-partner").html(data);
            $("#edit-tour-evaluation").select2();
            $("#edit-area-evaluation").select2();
            $("#edit-province-evaluation").select2();
            for (var i = 0; i <= $("#NumberEvaluationEdit").val() ; i++) {
                $("#edit-evaluation-ep" + i).select2();
            }
            $("#modal-edit-evaluation").modal("show");
            ////////
            $('#btnAddEditEP').click(function () {
                console.log('ok');
                var num = $('.clonedInputEditEP').length, // how many "duplicatable" input fields we currently have
                    newNum = new Number(num + 1),      // the numeric ID of the new input field being added
                    newElem = $('#entryEditEP' + num).clone().attr('id', 'entryEditEP' + newNum).removeAttr("style").fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
                // manipulate the name/id values of the input inside the new element

                newElem.find('.edit-title-ep').attr('id', 'edit-title-ep' + newNum).text('Tiêu chí đánh giá ' + newNum);
                newElem.find('.edit-evaluation-ep').attr('id', 'edit-evaluation-ep' + newNum).attr('name', 'EditEvaluationCriteriaId' + newNum).val('');
                newElem.find('.edit-point-ep').attr('id', 'edit-point-ep' + newNum).attr('name', 'EditEvaluationPoint' + newNum).val('');
                newElem.find('.edit-note-ep').attr('id', 'edit-note-ep' + newNum).attr('name', 'EditEvaluationNote' + newNum).val('');

                // insert the new element after the last "duplicatable" input field
                $('#entryEditEP' + num).after(newElem);
                $("#edit-evaluation-ep" + newNum).select2();

                for (var i = 1; i < newNum; i++) {
                    $("#entryEditEP" + newNum + " #select2-edit-evaluation-ep" + i + "-container").parent().parent().parent().remove();
                }

                // enable the "remove" button
                $('#btnDelEditEP').attr('disabled', false);

                // count service
                $("#NumberEvaluationEdit").val(newNum);
            });

            $('#btnDelEditEP').click(function () {
                // confirmation
                var num = $('.clonedInputEditEP').length;
                // how many "duplicatable" input fields we currently have
                $('#entryEditEP' + num).slideUp('slow', function () {
                    $(this).remove();
                    // if only one element remains, disable the "remove" button
                    if (num - 1 === 1)
                        $('#btnDelEditEP').attr('disabled', true);
                    // enable the "add" button
                    $('#btnAddEditEP').attr('disabled', false).prop('value', "add section");
                });
                return false;

                $('#btnAddEdit').attr('disabled', false);
                // count service
                $("#NumberEvaluationEdit").val(num);
            });
            ///// chọn vùng miền -> tỉnh thành
            $("#edit-area-evaluation").change(function () {
                $.getJSON('/EvaluationPartnerManage/ProvinceList?id=' + $('#edit-area-evaluation').val(), function (data) {
                    var items = '<option value=0>Chọn tỉnh thành</option>';
                    $.each(data, function (i, gd) {
                        items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                    });
                    $('#edit-province-evaluation').html(items);
                    $('#edit-province-evaluation').select2();
                });
            })
        }
    });
}
/** xóa đánh giá **/
function deleteEvaluation(id) {
    if (confirm('Bạn thực sự muốn xóa mục này ?')) {
        var dataPost = { id: id };
        $.ajax({
            type: "POST",
            url: '/PartnerOtherTab/DeleteEvaluation',
            data: JSON.stringify(dataPost),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                alert("Xóa dữ liệu thành công!!!");
                $("#danhgia").html(data);
            }
        });
    }
}

//////////////////////
/////*** duplicate form add evaluation partner ***/
function AddDuplicate() {
    $('#btnAddD').click(function () {
        console.log('ok');
        var num = $('.clonedInput-ep').length, // how many "duplicatable" input fields we currently have
            newNum = new Number(num + 1),      // the numeric ID of the new input field being added
            newElem = $('#entry-ep' + num).clone().attr('id', 'entry-ep' + newNum).removeAttr("style").fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
        // manipulate the name/id values of the input inside the new element

        newElem.find('.insert-title-ep').attr('id', 'insert-title-ep' + newNum).text('Tiêu chí đánh giá ' + newNum);
        newElem.find('.insert-evaluation-ep').attr('id', 'insert-evaluation-ep' + newNum).attr('name', 'EvaluationCriteriaId' + newNum).val('');
        newElem.find('.insert-point-ep').attr('id', 'insert-point-ep' + newNum).attr('name', 'EvaluationPoint' + newNum).val('');
        newElem.find('.insert-note-ep').attr('id', 'insert-note-ep' + newNum).attr('name', 'EvaluationNote' + newNum);

        // insert the new element after the last "duplicatable" input field
        $('#entry-ep' + num).after(newElem);
        $("#insert-evaluation-ep" + newNum).select2();

        for (var i = 1; i < newNum; i++) {
            $("#entry-ep" + newNum + " #select2-insert-evaluation-ep" + i + "-container").parent().parent().parent().remove();
        }

        // enable the "remove" button
        $('#btnDelD').attr('disabled', false);

        // count service
        $("#NumberEvaluation").val(newNum);
    });

    $('#btnDelD').click(function () {
        // confirmation
        var num = $('.clonedInput-ep').length;
        // how many "duplicatable" input fields we currently have
        $('#entry-ep' + num).slideUp('slow', function () {
            $(this).remove();
            // if only one element remains, disable the "remove" button
            if (num - 1 === 1)
                $('#btnDelD').attr('disabled', true);
            // enable the "add" button
            $('#btnAddD').attr('disabled', false).prop('value', "add section");
        });
        return false;

        $('#btnAddD').attr('disabled', false);
        // count service
        $("#NumberEvaluation").val(num);
    });
}