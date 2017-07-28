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
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" },
                { type: "text" }]
});

CKEDITOR.replace("insert-note");
$("#insert-type").select2();
$("#insert-status").select2();
$("#insert-start").select2();
$("#insert-end").select2();
$("#insert-condition").select2();
$("#insert-currency").select2();
$("#insert-customer").select2();
$("#insert-airline").select2();
$("#insert-airline-league").select2();
$("#insert-condition").select2();
$("#insert-start1-country").select2();
$("#insert-end1-country").select2();
$("#insert-start2-country").select2();
$("#insert-end2-country").select2();
$("#insert-start1-province").select2();
$("#insert-start2-province").select2();
$("#insert-end1-province").select2();
$("#insert-end2-province").select2();
$("#insert-start1-airport").select2();
$("#insert-start2-airport").select2();
$("#insert-end1-airport").select2();
$("#insert-end2-airport").select2();

// chọn quốc gia --> tỉnh thành
$("#insert-start1-country").change(function () {
    $.getJSON('/Home/ProvinceList?id=' + $('#insert-start1-country').val(), function (data) {
        var items = '<option>-- Chọn tỉnh thành --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-start1-province').html(items);
        $('#insert-start1-province').select2();
    });
})
$("#insert-start2-country").change(function () {
    $.getJSON('/Home/ProvinceList?id=' + $('#insert-start2-country').val(), function (data) {
        var items = '<option>-- Chọn tỉnh thành --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-start2-province').html(items);
        $('#insert-start2-province').select2();
    });
})
$("#insert-end1-country").change(function () {
    $.getJSON('/Home/ProvinceList?id=' + $('#insert-end1-country').val(), function (data) {
        var items = '<option>-- Chọn tỉnh thành --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-end1-province').html(items);
        $('#insert-end1-province').select2();
    });
})
$("#insert-end2-country").change(function () {
    $.getJSON('/Home/ProvinceList?id=' + $('#insert-end2-country').val(), function (data) {
        var items = '<option>-- Chọn tỉnh thành --</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-end2-province').html(items);
        $('#insert-end2-province').select2();
    });
})

// chọn tỉnh thành --> sân bay
$("#insert-start1-province").change(function () {
    $.getJSON('/Home/AirportList?id=' + $('#insert-start1-province').val(), function (data) {
        var items = '';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-start1-airport').html(items);
        $('#insert-start1-airport').select2();
    });
})
$("#insert-start2-province").change(function () {
    $.getJSON('/Home/AirportList?id=' + $('#insert-start2-province').val(), function (data) {
        var items = '';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-start2-airport').html(items);
        $('#insert-start2-airport').select2();
    });
})
$("#insert-end1-province").change(function () {
    $.getJSON('/Home/AirportList?id=' + $('#insert-end1-province').val(), function (data) {
        var items = '';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-end1-airport').html(items);
        $('#insert-end1-airport').select2();
    });
})
$("#insert-end2-province").change(function () {
    $.getJSON('/Home/AirportList?id=' + $('#insert-end2-province').val(), function (data) {
        var items = '';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-end2-airport').html(items);
        $('#insert-end2-airport').select2();
    });
})
/////////////////////////

$("#btnEdit").click(function () {
    var dataPost = {
        id: $("input[type='checkbox']:checked").val()
    };

    $.ajax({
        type: "POST",
        url: '/TicketManage/EditInfoTicket',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#data-ticket").html(data);
            $("#edit-type").select2();
            $("#edit-status").select2();
            $("#edit-start").select2();
            $("#edit-end").select2();
            $("#edit-customer").select2();
            $("#edit-condition").select2();
            $("#edit-currency").select2();
            $("#edit-airline").select2();
            $("#edit-airline-league").select2();
            $("#edit-start1-country").select2();
            $("#edit-end1-country").select2();
            $("#edit-start2-country").select2();
            $("#edit-end2-country").select2();
            $("#edit-start1-province").select2();
            $("#edit-start2-province").select2();
            $("#edit-end1-province").select2();
            $("#edit-end2-province").select2();
            $("#edit-start1-airport").select2();
            $("#edit-start2-airport").select2();
            $("#edit-end1-airport").select2();
            $("#edit-end2-airport").select2();
            $("#modal-edit-ticket").modal("show");
            // khách hàng
            $("#edit-customer").change(function () {
                var dataPost = {
                    id: $("#edit-customer").val()
                };
                $.ajax({
                    type: "POST",
                    url: '/TicketManage/LoadCustomer',
                    data: JSON.stringify(dataPost),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#edit-phone").val(data.phone);
                        ///////////
                        $.getJSON('/TicketManage/GetAirlineLeague?id=' + $("#edit-customer").val(), function (data) {
                            var items = '<option>-- Chọn liên minh hàng không --</option>';
                            $.each(data, function (i, gd) {
                                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                            });
                            $('#edit-airline-league').html(items);
                            $('#edit-airline-league').select2();
                        });
                    }
                });
            })
            //chiết khấu
            $("#edit-airline").change(function () {
                var dataPost = { id: $("#edit-airline").val() };
                $.ajax({
                    type: "POST",
                    url: '/TicketManage/GetPercentForRose',
                    data: JSON.stringify(dataPost),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#edit-percent").val(data);
                    }
                });
                CalculatorRoseCostEdit();
            });
            // thẻ liên minh
            $('#edit-airline-league').change(function () {
                var dataPost = {
                    league: $('#edit-airline-league').val(),
                    customer: $("#edit-customer").val()
                };
                $.ajax({
                    type: "POST",
                    url: '/TicketManage/GetCardLevelNumber',
                    data: JSON.stringify(dataPost),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#edit-idcard-level").val(data.idcard);
                        $("#edit-card-level").val(data.card);
                        $("#edit-number-card").val(data.number);
                    }
                });
            })
            // quốc gia -> tỉnh thành -> sân bay
            updateLocation();
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
        if (data == 1) {
            alert('Đã xóa!');
            window.location.href = '/TicketManage';
        }
        else {
            alert('Lỗi, vui lòng xem lại!');
        }
    });
}

/** chọn từng dòng để hiển thị thông tin chi tiết dưới các tab **/
$("table#tableDictionary").delegate("tr", "click", function () {
    var dataPost = { id: $(this).find("input[type='checkbox']").val() };
    $.ajax({
        type: "POST",
        url: '/TicketManage/InfoNote',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#tab1").html(data);
        }
    });
});

$("#insert-customer").change(function () {
    var dataPost = {
        id: $("#insert-customer").val()
    };

    $.ajax({
        type: "POST",
        url: '/TicketManage/LoadCustomer',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#insert-phone").val(data.phone);
            ///////////
            $.getJSON('/TicketManage/GetAirlineLeague?id=' + $("#insert-customer").val(), function (data) {
                var items = '<option>-- Chọn liên minh hàng không --</option>';
                $.each(data, function (i, gd) {
                    items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
                });
                $('#insert-airline-league').html(items);
                $('#insert-airline-league').select2();
            });
        }
    });
})

$('#insert-airline-league').change(function () {
    var dataPost = {
        league: $('#insert-airline-league').val(),
        customer: $("#insert-customer").val()
    };
    $.ajax({
        type: "POST",
        url: '/TicketManage/GetCardLevelNumber',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#insert-idcard-level").val(data.idcard);
            $("#insert-card-level").val(data.card);
            $("#insert-number-card").val(data.number);
        }
    });
})

checkCodeTicket();

function checkCodeTicket() {
    var dataPost = { code: $("#CodeTicket").val() };
    $.ajax({
        type: "POST",
        url: '/CustomerOtherTab/CheckCodeTicket',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data == 0) {
                $("#CodeTicket").val('');
                $("#CodeTicket").focus();
                alert('Trùng Vé máy bay');
            }
        }
    });
}

function updateLocation() {
    // chọn quốc gia --> tỉnh thành
    $("#edit-start1-country").change(function () {
        $.getJSON('/Home/ProvinceList?id=' + $('#edit-start1-country').val(), function (data) {
            var items = '<option>-- Chọn tỉnh thành --</option>';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-start1-province').html(items);
            $('#edit-start1-province').select2();
        });
    })
    $("#edit-start2-country").change(function () {
        $.getJSON('/Home/ProvinceList?id=' + $('#edit-start2-country').val(), function (data) {
            var items = '<option>-- Chọn tỉnh thành --</option>';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-start2-province').html(items);
            $('#edit-start2-province').select2();
        });
    })
    $("#edit-end1-country").change(function () {
        $.getJSON('/Home/ProvinceList?id=' + $('#edit-end1-country').val(), function (data) {
            var items = '<option>-- Chọn tỉnh thành --</option>';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-end1-province').html(items);
            $('#edit-end1-province').select2();
        });
    })
    $("#edit-end2-country").change(function () {
        $.getJSON('/Home/ProvinceList?id=' + $('#edit-end2-country').val(), function (data) {
            var items = '<option>-- Chọn tỉnh thành --</option>';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-end2-province').html(items);
            $('#edit-end2-province').select2();
        });
    })

    // chọn tỉnh thành --> sân bay
    $("#edit-start1-province").change(function () {
        $.getJSON('/Home/AirportList?id=' + $('#edit-start1-province').val(), function (data) {
            var items = '';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-start1-airport').html(items);
            $('#edit-start1-airport').select2();
        });
    })
    $("#edit-start2-province").change(function () {
        $.getJSON('/Home/AirportList?id=' + $('#edit-start2-province').val(), function (data) {
            var items = '';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-start2-airport').html(items);
            $('#edit-start2-airport').select2();
        });
    })
    $("#edit-end1-province").change(function () {
        $.getJSON('/Home/AirportList?id=' + $('#edit-end1-province').val(), function (data) {
            var items = '';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-end1-airport').html(items);
            $('#edit-end1-airport').select2();
        });
    })
    $("#edit-end2-province").change(function () {
        $.getJSON('/Home/AirportList?id=' + $('#edit-end2-province').val(), function (data) {
            var items = '';
            $.each(data, function (i, gd) {
                items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
            });
            $('#edit-end2-airport').html(items);
            $('#edit-end2-airport').select2();
        });
    })
    /////////////////////////
}

// tính toán giá tiền và hoa hồng
function CalculatorRoseCost() {
    var fare = $("#insert-fare").val() == null || $("#insert-fare").val() == "" ? 0 : parseFloat($("#insert-fare").val());
    var tax = $("#insert-tax").val() == null || $("#insert-tax").val() == "" ? 0 : parseFloat($("#insert-tax").val());
    var service = $("#insert-service").val() == null || $("#insert-service").val() == "" ? 0 : parseFloat($("#insert-service").val());
    $("#insert-cost").val(checkMoney(fare + tax + service));
    var percent = $("#insert-percent").val() == null || $("#insert-percent").val() == "" ? 0 : parseFloat($("#insert-percent").val());
    $("#insert-com").val(checkMoney(fare - (percent / 100) + tax + service));
    $("#insert-rose").val(checkMoney((fare + tax + service) * (percent / 100)));
}

function CalculatorRoseCostEdit() {
    var fare = $("#edit-fare").val() == null || $("#edit-fare").val() == "" ? 0 : parseFloat($("#edit-fare").val());
    var tax = $("#edit-tax").val() == null || $("#edit-tax").val() == "" ? 0 : parseFloat($("#edit-tax").val());
    var service = $("#edit-service").val() == null || $("#edit-service").val() == "" ? 0 : parseFloat($("#edit-service").val());
    $("#edit-cost").val(checkMoney(fare + tax + service));
    var percent = $("#edit-percent").val() == null || $("#edit-percent").val() == "" ? 0 : parseFloat($("#edit-percent").val());
    $("#edit-com").val(checkMoney(fare - (percent / 100) + tax + service));
    $("#edit-rose").val(checkMoney((fare + tax + service) * (percent / 100)));
}

// tính tiền hoa hồng
$("#insert-airline").change(function () {
    var dataPost = { id: $("#insert-airline").val() };
    $.ajax({
        type: "POST",
        url: '/TicketManage/GetPercentForRose',
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#insert-percent").val(data);
        }
    });
    CalculatorRoseCost();
});