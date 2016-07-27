$(document).ready(function () {

    var navListItems = $('div.setup-panel div a'),
            allWells = $('.setup-content'),
            allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
                $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-success').addClass('btn-default');
            $item.addClass('btn-success');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid)
            nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-success').trigger('click');


    //custom code by @naresh for file input sep

    var fileInput = document.getElementById('sep_json');
    var fileDisplayArea = document.getElementById('sep_jsondisplay');
    var fileInput1 = document.getElementById('action_json');
    var fileDisplayArea1 = document.getElementById('action_jsondisplay');


    //fileInput.addEventListener('change', function (e) {
    //    var file = fileInput.files[0];
    //    var textType = /text.*/;

    //    if (file.type.match(textType)) {
    //        var reader = new FileReader();

    //        reader.onload = function (e) {
    //            var res = reader.result;
    //            function isJSON(something) {
    //                if (typeof something != 'string')
    //                    something = JSON.stringify(something);

    //                try {
    //                    JSON.parse(something);
    //                    return true;
    //                } catch (e) {
    //                    return false;
    //                }
    //            }
    //            if (isJSON(res)) {
    //                fileDisplayArea.innerText = JSON.stringify(res);
    //            } else {
    //                fileDisplayArea.innerText = "File content is not JSON"
    //            }
    //        }

    //        reader.readAsText(file);
    //    } else {
    //        fileDisplayArea.innerText = "File not supported!"
    //    }
    //});
    //fileInput1.addEventListener('change', function (e) {
    //    var file = fileInput1.files[0];
    //    var textType = /text.*/;

    //    if (file.type.match(textType)) {
    //        var reader = new FileReader();

    //        reader.onload = function (e) {
    //            var res = reader.result;
    //            function isJSON(something) {
    //                if (typeof something != 'string')
    //                    something = JSON.stringify(something);

    //                try {
    //                    JSON.parse(something);
    //                    return true;
    //                } catch (e) {
    //                    return false;
    //                }
    //            }
    //            if (isJSON(res)) {
    //                fileDisplayArea1.innerText = JSON.stringify(res);
    //            } else {
    //                fileDisplayArea1.innerText = "File content is not JSON"
    //            }
    //        }

    //        reader.readAsText(file);
    //    } else {
    //        fileDisplayArea1.innerText = "File not supported!"
    //    }
    //});

    //@naresh action dynamic childs
    var next = 0;
    $("#add-more").click(function (e) {
        e.preventDefault();
        var addto = "#field" + next;
        var addRemove = "#field" + (next);
        next = next + 1;
        var newIn = ' <div id="field' + next + '" name="field' + next + '"><!-- Text input--><div class="form-group"> <label class="col-md-4 control-label" for="action_id">Action Id</label> <div class="col-md-5"> <input id="action_id" name="action_id" type="text" placeholder="" class="form-control input-md"> </div></div><br><br><!-- Text input--><div class="form-group"> <label class="col-md-4 control-label" for="action_name">Action Name</label> <div class="col-md-5"> <input id="action_name" name="action_name" type="text" placeholder="" class="form-control input-md"> </div></div><br><br><!-- File Button --> <div class="form-group"> <label class="col-md-4 control-label" for="action_json">Action JSON File</label> <div class="col-md-4"> <input id="action_json" name="action_json" class="input-file" type="file"> </div></div></div>';
        var newInput = $(newIn);
        var removeBtn = '<button id="remove' + (next - 1) + '" class="btn btn-danger remove-me" >Remove</button></div></div><div id="field">';
        var removeButton = $(removeBtn);
        $(addto).after(newInput);
        $(addRemove).after(removeButton);
        $("#field" + next).attr('data-source', $(addto).attr('data-source'));
        $("#count").val(next);

        $('.remove-me').click(function (e) {
            e.preventDefault();
            var fieldNum = this.id.charAt(this.id.length - 1);
            var fieldID = "#field" + fieldNum;
            $(this).remove();
            $(fieldID).remove();
        });
    });


    $('#TableSearch').on('click', '.clickable-row', function (event) {

        var row = '<tr id="' + this.id + '">';

        row += '<td class="clickable-row" style="cursor:pointer">' + this.cells[0].innerText + '</td>';
        row += '<td><input type="number" name="CharacterAttributeValue" min="0" class="form-control" /></td>';

        row += '</tr>';

        $('#TableSelected tbody').append(row);

        $(this).remove();
    });


    $('#TableSelected').on('click', '.clickable-row', function (event) {
        SearchAvailable($('#AttributeTypeSearch').val());
        $(this).closest('tr').remove();
    });

    //Search By Attribute Type
    $('#AttributeTypeSearch').on('change', null, function (event) {
        SearchAvailable($(this).val());
    });

});

function SearchAvailable(value) {

    var url = '/Attribute/SearchByType/?type=' + value;

    $('#TableSearch tbody').empty();

    $.getJSON(url, function (data) {

        for (var i = 0; i < data.length; i++) {
            var item = JSON.parse(JSON.stringify(data[i]));

            if (!VerifySelectedId(item.AttributeId)) {
                var row = "<tr class='clickable-row' style='cursor:pointer' id='" + item.AttributeId + "'>";
                row += "<td>" + item.AttributeName + "</td>";
                row += "<td>" + item.AttributeDescription + "</td>";
                row += "</tr>";

                $('#TableSearch tbody').append(row);
            }
        }
    });

    $(this).closest('tr').remove();
}

function VerifySelectedId(value) {
    var result = false;
    $.each($('#TableSelected tbody tr'), function (index, element) {
        if (value == element.id) result = true;

    });

    return result;
}