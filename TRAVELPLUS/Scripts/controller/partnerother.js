$("#insert-area").select2();
$("#insert-area").change(function () {
    $.getJSON('/EvaluationPartnerManage/ProvinceList?id=' + $('#insert-area').val(), function (data) {
        var items = '<option value=0>Chọn tỉnh thành</option>';
        $.each(data, function (i, gd) {
            items += "<option value='" + gd.Value + "'>" + gd.Text + "</option>";
        });
        $('#insert-provice').html(items);
        $('#insert-provice').select2();
    });
})

///*** duplicate form add service partner ***/
$('#btnAddD').click(function () {
    var num = $('.clonedInput').length, // how many "duplicatable" input fields we currently have
        newNum = new Number(num + 1),      // the numeric ID of the new input field being added
        newElem = $('#entry' + num).clone().attr('id', 'entry' + newNum).fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
    // manipulate the name/id values of the input inside the new element

    newElem.find('.insert-title').attr('id', 'insert-title' + newNum).text('Tiêu chí đánh giá ' + newNum);
    newElem.find('.insert-evaluation').attr('id', 'insert-evaluation' + newNum).attr('name', 'EvaluationCriteriaId' + newNum).val('');
    newElem.find('.insert-point').attr('id', 'insert-point' + newNum).attr('name', 'EvaluationPoint' + newNum).val('');
    newElem.find('.insert-note').attr('id', 'insert-note' + newNum).attr('name', 'EvaluationNote' + newNum);

    // insert the new element after the last "duplicatable" input field
    $('#entry' + num).after(newElem);
    $("#insert-evaluation" + newNum).select2();

    for (var i = 1; i < newNum; i++) {
        $("#entry" + newNum + " #select2-insert-evaluation" + i + "-container").parent().parent().parent().remove();
    }

    // enable the "remove" button
    $('#btnDel').attr('disabled', false);

    // count service
    $("#NumberEvaluation").val(newNum);
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
        $('#btnAddD').attr('disabled', false).prop('value', "add section");
    });
    return false;

    $('#btnAddD').attr('disabled', false);
    // count service
    $("#NumberEvaluation").val(num);
});

///*** duplicate form add service partner ***/
function EditDuplicate() {
    $('#btnAddEdit').click(function () {
        var num = $('.clonedInputEdit').length, // how many "duplicatable" input fields we currently have
            newNum = new Number(num + 1),      // the numeric ID of the new input field being added
            newElem = $('#entryEdit' + num).clone().attr('id', 'entryEdit' + newNum).fadeIn('slow'); // create the new element via clone(), and manipulate it's ID using newNum value
        // manipulate the name/id values of the input inside the new element

        newElem.find('.edit-title').attr('id', 'edit-title' + newNum).text('Tiêu chí đánh giá ' + newNum);
        newElem.find('.edit-evaluation').attr('id', 'edit-evaluation' + newNum).attr('name', 'EditEvaluationCriteriaId' + newNum).val('');
        newElem.find('.edit-point').attr('id', 'edit-point' + newNum).attr('name', 'EditEvaluationPoint' + newNum).val('');
        newElem.find('.edit-note').attr('id', 'edit-note' + newNum).attr('name', 'EditEvaluationNote' + newNum);

        // insert the new element after the last "duplicatable" input field
        $('#entryEdit' + num).after(newElem);
        $("#edit-evaluation" + newNum).select2();

        for (var i = 1; i < newNum; i++) {
            $("#entryEdit" + newNum + " #select2-edit-evaluation" + i + "-container").parent().parent().parent().remove();
        }

        // enable the "remove" button
        $('#btnDelEdit').attr('disabled', false);

        // count service
        $("#NumberEvaluationEdit").val(newNum);
    });

    $('#btnDelEdit').click(function () {
        // confirmation
        var num = $('.clonedInputEdit').length;
        // how many "duplicatable" input fields we currently have
        $('#entryEdit' + num).slideUp('slow', function () {
            $(this).remove();
            // if only one element remains, disable the "remove" button
            if (num - 1 === 1)
                $('#btnDelEdit').attr('disabled', true);
            // enable the "add" button
            $('#btnAddEdit').attr('disabled', false).prop('value', "add section");
        });
        return false;

        $('#btnAddEdit').attr('disabled', false);
        // count service
        $("#NumberEvaluationEdit").val(num);
    });
}