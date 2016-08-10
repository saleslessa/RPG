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
            $target.fadeIn("slow");
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
        var row = '<tr id="' + $(this).attr('id') + '">';

        row += '<td>' + this.cells[0].innerText + '</td>';
        row += '<td class="TdAttributeValue"><input type="number" name="CharacterAttributeValue" class="CharacterAttributeValue" min="0" class="form-control" /></td>';
        row += '<td><input type="button" class="clickable-row btn btn-default" value=" - " />';

        row += '</tr>';

        $('#TableSelected tbody').append(row);

        $(this).remove();
    });


    $('#TableSelected').on('click', '.clickable-row', function (event) {
        SearchAvailable($('#AttributeTypeSearch').val());
        $(this).closest('tr').remove();
    });


    $('#AttributeTypeSearch').on('change', null, function (event) {
        SearchAvailable($(this).val());
    });

    $('input[name=ItemQtd]').on('change', null, function (event) {
        var price = GetPricePlayerItem($(this));
        var qtd = $(this).val();

        if (price == 0 || qtd == 0)
            $(this).parent().siblings(".TdItemTotal").children().val("");
        else
            $(this).parent().siblings(".TdItemTotal").children().val(price * qtd);
    });

    $('#table-item-available').on('click', '.btnIncludeItem', function (event) {
        var selectedItem = FindItemName(GetItemNamePlayerItem($(this)));

        if (selectedItem == null && GetQtdPlayerItem($(this)) > 0) {
            var row = "<tr id='" + $(this).attr('id') + "'>";

            row += "<td class='TdItemName'>" + GetItemNamePlayerItem($(this)) + "</td>";
            row += "<td class='TdItemPrice'>" + GetPricePlayerItem($(this)) + "</td>";
            row += "<td class='TdItemQtd'>" + GetQtdPlayerItem($(this)) + "</td>";
            row += "<td class='TdItemTotal'>" + GetPricePlayerItem($(this)) * GetQtdPlayerItem($(this)) + "</td>";
            row += "<td><input type='button' class='btnRemoveItem btn btn-danger' value=' - ' alt='Remove Item' /></td>";

            row += "</tr>";

            $("#table-item-selected tbody").append(row);
        }
        else {
            if (GetQtdPlayerItem($(this)) > 0)
                SetSelectedItem($(this), GetItemNamePlayerItem($(this)));
        }

        GetTotalInvested();
    });

    $('#table-item-selected').on('click', '.btnRemoveItem', function (event) {
        $(this).parent().parent().remove();
        GetTotalInvested();
    });

    $("input[name=btnCreatePlayer]").on('click', null, function (event) {
        var model = MountBasicInformation();
        var objAttributes = MountAttributes();
        var objItems = MountItems();

        model.SelectedAttributes = objAttributes;
        model.SelectedItems = objItems;

        $("#errorSummary").hide();
        $("#errorSummary").empty();

        $.ajax(
        {
            dataType: 'JSON',
            url: '/Player/Create/',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ model: model }),

            success: function (response) {
                if (response.error == "")
                    $("#errorSummary").append('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button><h3>' + response.message + '</h3></div>');

                if (response.error == "ModelStateError")
                    MountModelStateErrorMessage(response.model);

                if (response.error == "ValildationResultError")
                    MountValildationResultError(response.model);

                $("#errorSummary").slideToggle('slow');
            },
            error: function (xhr) {
                alert("An error occured when trying to create your character. Plase try again later");
            }
        });

        
    });

});

function MountValildationResultError(model) {
    var div = '<div class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">×</button><h3>Oops! something goes wrong:</h3><ul>';

    for (var i = 0; i < model.Erros.length; i++) {
        div += '<li>' + model.Erros[i].Message + '</li>';
    }


    div += '</ul></div>';
    $("#errorSummary").append(div);
}

function MountModelStateErrorMessage(model) {
    var div = '<div class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">×</button><h3>Oops! something goes wrong:</h3><ul>';

    for (var i = 0; i < model.length; i++) {
        div += '<li>' + model[i].Key.replace("model.", "") + ': ' + model[i].Value + '</li>';
    }

    div += '</ul></div>';
    $("#errorSummary").append(div);
}

function MountBasicInformation() {
    var objBasicInfo = new Object();
    objBasicInfo.SelectedCampaignId = $('#SelectedCampaignId').val();
    objBasicInfo.CharacterName = $('#CharacterName').val();
    objBasicInfo.CharacterMaxLife = $('#CharacterMaxLife').val();
    objBasicInfo.PlayerAge = $('#PlayerAge').val();
    objBasicInfo.PlayerExperience = $('#PlayerExperience').val();
    objBasicInfo.PlayerLevel = $('#PlayerLevel').val();
    objBasicInfo.PlayerMoney = $('#PlayerMoney').val();
    objBasicInfo.PlayerPointsToDistribute = $('#PlayerPointsToDistribute').val();
    objBasicInfo.CharacterRace = $('#CharacterRace').val();
    objBasicInfo.CharacterGender = $('#CharacterGender').val();
    objBasicInfo.PlayerBackground = $('#PlayerBackground').val();

    return objBasicInfo;
}

function MountAttributes() {
    var objAttributes = [];

    $("#TableSelected > tbody > tr").each(function (index, element) {
        if (parseInt(jQuery(element).children('.TdAttributeValue').children('.CharacterAttributeValue').val()) > 0) {
            var item = {};
            item['AttributeId'] = element.id;
            item['Value'] = jQuery(element).children('.TdAttributeValue').children('.CharacterAttributeValue').val();
            objAttributes.push(item);
        }
    });

    return objAttributes;
}

function MountItems() {
    var objItems = [];

    $("#table-item-selected > tbody > tr").each(function (index, element) {
        if (parseInt(GetPricePlayerItem(element)) > 0) {
            var item = {};
            item['ItemId'] = element.id;
            item['PlayerItemQtd'] = GetQtdPlayerItem(element);
            item['PlayerItemUnitPrice'] = GetPricePlayerItem(element);
            objItems.push(item);
        }
    });

    return objItems;
}

function SetSelectedItem(obj, ItemName) {
    $("#table-item-selected > tbody > tr").each(function (index, element) {
        if (GetItemNamePlayerItem(element) == ItemName) {
            $(this).children(".TdItemQtd").text(GetQtdPlayerItem(obj) + GetQtdPlayerItem(element));
            $(this).children(".TdItemTotal").text(GetPricePlayerItem(obj) * GetQtdPlayerItem(element));
        }
    });
}

function FindItemName(ItemName) {
    var result = null;

    $("#table-item-selected > tbody > tr").each(function (index, element) {
        if (GetItemNamePlayerItem(element) == ItemName)
            result = element;
    });

    return result;
}

function GetTotalInvested() {
    var total = 0;
    $("#table-item-selected > tbody > tr").each(function (index, element) {
        total += GetTotalPlayerItem(element);
    });

    if (total > 0)
        $("#input-total-invested").val(total);
    else
        $("#input-total-invested").val("");
}

function GetItemNamePlayerItem(obj) {
    if (jQuery(obj).prop("tagName") == "TR")
        return jQuery(obj).children(".TdItemName").text();
    else
        return jQuery(obj).parent().siblings(".TdItemName").text();
}

function GetPricePlayerItem(obj) {
    if (jQuery(obj).prop("tagName") == "TR")
        return parseFloat(jQuery(obj).children('.TdItemPrice').text());
    else
        return parseFloat(obj.parent().siblings(".TdItemPrice").children().val());
}

function GetTotalPlayerItem(obj) {
    if (jQuery(obj).prop("tagName") == "TR")
        return parseFloat(jQuery(obj).children(".TdItemTotal").text());
    else
        return parseFloat(jQuery(obj).find('.TdItemTotal').text());
}

function GetQtdPlayerItem(obj) {
    if (jQuery(obj).prop("tagName") == "TR")
        return parseInt(jQuery(obj).children(".TdItemQtd").text());
    else
        return parseInt(obj.parent().siblings(".TdItemQtd").children().val());
}

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

