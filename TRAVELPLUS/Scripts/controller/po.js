$("#insert-customer").select2();
$("#insert-partner").select2();
$("#insert-paymentmethod").select2();
$("#insert-currency").select2();
$("#insert-bank").select2();

var table = $('.dataTable').DataTable({
    order: [],
    columnDefs: [{ orderable: false, targets: [0] }],
    "footerCallback": function (row, data, start, end, display) {
        var api = this.api(), data;
        // Remove the formatting to get integer data for summation
        var intVal = function (i) {
            return typeof i === 'string' ? i.replace(/[\$,.]/g, '') * 1 : typeof i === 'number' ? i : 0;
        };

        // total_salary over all pages
        total_salary = api.column(3).data().reduce(function (a, b) {
            return intVal(a) + intVal(b);
        }, 0);

        // total_page_salary over this page
        total_page_salary = api.column(3, { page: 'current' }).data().reduce(function (a, b) {
            return intVal(a) + intVal(b);
        }, 0);

        total_page_salary = parseFloat(total_page_salary);
        total_salary = parseFloat(total_salary);
        // Update footer
        $('#totalSalary').html(checkMoney(total_page_salary));
    },
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
                { type: "text" },
                { type: "text" },
                { type: "text" }]
});

$("#btnAddInvoice").click(function () {
    var num = $('.rowItem').length,
        newNum = new Number(num + 1),
        newElem = $('#rowItem' + num).clone().attr('id', 'rowItem' + newNum).removeAttr('style').fadeIn('slow');
    newElem.find('.removeItem').attr('id', 'removeItem' + newNum).removeAttr("disabled");
    newElem.find('.titleInvoice').attr('id', 'titleInvoice' + newNum).text(newNum);
    newElem.find('.descInvoice').attr('id', 'descInvoice' + newNum).attr('name', 'DescriptionInvoice' + newNum).val("");
    newElem.find('.quantityInvoice').attr('id', 'quantityInvoice' + newNum).attr('name', 'QuantityInvoice' + newNum).attr('onchange', 'calculatorInvoice(' + newNum + ')').val("");
    newElem.find('.priceInvoice').attr('id', 'priceInvoice' + newNum).attr('name', 'PriceInvoice' + newNum).attr('onchange', 'calculatorInvoice(' + newNum + ')').val("");
    newElem.find('.totalInvoice').attr('id', 'totalInvoice' + newNum).attr('name', 'TotalInvoice' + newNum).val("");
    newElem.find('.noteInvoice').attr('id', 'noteInvoice' + newNum).attr('name', 'NoteInvoice' + newNum).val("");
    newElem.find('.removeItem').attr('onclick', 'removeItem(' + newNum + ')');
    $(".rowItem").find('.removeItem').removeAttr("disabled");
    $('#rowItem' + num).after(newElem);
    $("#NumberInvoice").val(newNum);
    $("#NumberInvoiceForDelete").val(newNum);
})
function calculatorInvoice(i) {
    var dg = parseFloat($("#priceInvoice" + i).val() == null || $("#priceInvoice" + i).val() == "" ? 0 : $("#priceInvoice" + i).val());
    var sl = parseFloat($("#quantityInvoice" + i).val() == null || $("#quantityInvoice" + i).val() == "" ? 0 : $("#quantityInvoice" + i).val());
    var tt = dg * sl;
    $("#totalInvoice" + i).val(checkMoney(tt));
}
function removeItem(id) {
    $('#rowItem' + id).slideUp('quickly', function () {
        $(this).remove();
    });
    $("#NumberInvoiceForDelete").val($("#NumberInvoiceForDelete").val() - 1);
    if ($("#NumberInvoiceForDelete").val() == 1) {
        $(".rowItem").find(".removeItem").attr("disabled", "disabled");
    }
}
// chọn khách hàng --> danh sách ngân hàng
$("#insert-customer").change(function () {
    $("#txtAccountDetail").val("");
    $.getJSON('/POFormManage/GetBanking?id=' + $('#insert-customer').val() + '&i=0', function (data) {
        var items = '<option>-- Chọn ngân hàng --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-bank').html(items);
        $('#insert-bank').select2();
    });
})

// chọn đối tác --> danh sách ngân hàng
$("#insert-partner").change(function () {
    $("#txtAccountDetail").val("");
    $.getJSON('/POFormManage/GetBanking?id=' + $('#insert-partner').val() + '&i=1', function (data) {
        var items = '<option>-- Chọn ngân hàng --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-bank').html(items);
        $('#insert-bank').select2();
    });
})

// get thông tin tài khoản ngân hàng
$("#insert-bank").change(function () {
    var dataPost = { id: $("#insert-bank").val() };
    $.ajax({
        type: "POST",
        url: '/POFormManage/GetDetailBank',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#txtAccountDetail").val(data);
        }
    });
})

// xóa dữ liệu
$("#btnRemove").on("click", function () {
    if ($("#listItemId").val() == "") {
        alert("Vui lòng chọn mục cần xóa !");
        return false;
    }
    var $this = $(this);
    var $tableWrapper = $("#tableDictionary-Wrapper");
    var $table = $("#tableDictionary");

    DeleteSelectedItem($this, $tableWrapper, $table, function (data) {});
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

// cập nhật dữ liệu
$("#btnEdit").click(function () {
    var dataPost = {
        id: $("input[type='checkbox']:checked").val()
    };

    $.ajax({
        type: "POST",
        url: '/POFormManage/Edit',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-po").html(data);
            $("#edit-customer").select2();
            $("#edit-bank").select2();
            $("#edit-partner").select2();
            $("#edit-paymentmethod").select2();
            $("#edit-currency").select2();
            $("#modal-edit-po").modal("show");
            //
            $("#btnAddInvoiceEdit").click(function () {
                var num = $('.rowItemEdit').length,
                    newNum = new Number(num + 1),
                    newElem = $('#rowItemEdit' + num).clone().attr('id', 'rowItemEdit' + newNum).removeAttr('style').fadeIn('slow');
                newElem.find('.removeItemEdit').attr('id', 'removeItemEdit' + newNum).removeAttr("disabled");
                newElem.find('.titleInvoiceEdit').attr('id', 'titleInvoiceEdit' + newNum).text(newNum);
                newElem.find('.descInvoiceEdit').attr('id', 'descInvoiceEdit' + newNum).attr('name', 'DescriptionInvoiceEdit' + newNum).val("");
                newElem.find('.quantityInvoiceEdit').attr('id', 'quantityInvoiceEdit' + newNum).attr('name', 'QuantityInvoiceEdit' + newNum).attr('onchange', 'calculatorInvoiceEdit(' + newNum + ')').val("");
                newElem.find('.priceInvoiceEdit').attr('id', 'priceInvoiceEdit' + newNum).attr('name', 'PriceInvoiceEdit' + newNum).attr('onchange', 'calculatorInvoiceEdit(' + newNum + ')').val("");
                newElem.find('.totalInvoiceEdit').attr('id', 'totalInvoiceEdit' + newNum).attr('name', 'TotalInvoiceEdit' + newNum).val("");
                newElem.find('.noteInvoiceEdit').attr('id', 'noteInvoiceEdit' + newNum).attr('name', 'NoteInvoiceEdit' + newNum).val("");
                newElem.find('.removeItemEdit').attr('onclick', 'removeItemEdit(' + newNum + ')');
                $(".rowItemEdit").find('.removeItem').removeAttr("disabled");
                $('#rowItemEdit' + num).after(newElem);
                $("#NumberInvoiceEdit").val(newNum);
                $("#NumberInvoiceForDeleteEdit").val(newNum);
            })
            // chọn khách hàng --> danh sách ngân hàng
            $("#edit-customer").change(function () {
                $("#txtEditAccountDetail").val("");
                $.getJSON('/POFormManage/GetBanking?id=' + $('#edit-customer').val() + '&i=0', function (data) {
                    var items = '<option>-- Chọn ngân hàng --</option>';
                    $.each(data, function (i, gd) {
                        items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                    });
                    $('#edit-bank').html(items);
                    $('#edit-bank').select2();
                });
            })

            // chọn đối tác --> danh sách ngân hàng
            $("#edit-partner").change(function () {
                $("#txtEditAccountDetail").val("");
                $.getJSON('/POFormManage/GetBanking?id=' + $('#edit-partner').val() + '&i=1', function (data) {
                    var items = '<option>-- Chọn ngân hàng --</option>';
                    $.each(data, function (i, gd) {
                        items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                    });
                    $('#edit-bank').html(items);
                    $('#edit-bank').select2();
                });
            })

            // get thông tin tài khoản ngân hàng
            $("#edit-bank").change(function () {
                var dataPost = { id: $("#edit-bank").val() };
                $.ajax({
                    type: "POST",
                    url: '/POFormManage/GetDetailBank',
                    data: JSON.stringify(dataPost),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#txtEditAccountDetail").val(data);
                    }
                });
            })
        }
    });
});
//
function calculatorInvoiceEdit(i) {
    var dg = parseFloat($("#priceInvoiceEdit" + i).val() == null || $("#priceInvoiceEdit" + i).val() == "" ? 0 : $("#priceInvoiceEdit" + i).val().replace('.',''));
    var sl = parseFloat($("#quantityInvoiceEdit" + i).val() == null || $("#quantityInvoiceEdit" + i).val() == "" ? 0 : $("#quantityInvoiceEdit" + i).val().replace('.', ''));
    var tt = dg * sl;
    $("#totalInvoiceEdit" + i).val(checkMoney(tt));
}
//
function removeItemEdit(id) {
    $('#rowItemEdit' + id).slideUp('quickly', function () {
        $(this).remove();
    });
    $("#NumberInvoiceForDeleteEdit").val($("#NumberInvoiceForDeleteEdit").val() - 1);
    if ($("#NumberInvoiceForDeleteEdit").val() == 1) {
        $(".rowItemEdit").find(".removeItemEdit").attr("disabled", "disabled");
    }
}
// xem thông tin chi tiết phiếu duyệt
$("table#tableDictionary tbody").delegate("tr", "click", function () {
    var dataPost = { id: $(this).find("input[type='checkbox']").val() };
    $('tr').not(this).removeClass('oneselected');
    $(this).toggleClass('oneselected');
    $.ajax({
        type: "POST",
        url: '/POFormManage/Details',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#thongtinchitiet").html(data);
        }
    });
});

function btnPreview(id) {
    console.log("btnPreview");
    var dataPost = { id: id };
    $.ajax({
        type: "POST",
        url: '/POFormManage/Preview',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-po").html(data);
            $("#modal-preview-po").modal("show");
        }
    });
}
function checkMoney(AmountValue) {
    //var AmountValue = document.getElementById('Gia').value;
    var num = parseFloat(AmountValue);
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '.' + num.substring(num.length - (4 * i + 3));
    num = (((sign) ? '' : '-') + num);

    return num;
}
//$("#totalFooter").text(checkMoney($("#totalFooter").text()));