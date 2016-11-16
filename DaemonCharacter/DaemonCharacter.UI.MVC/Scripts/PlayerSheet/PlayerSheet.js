﻿$(document).ready(function () {

    $('.attribute-bonus').mouseover(function () {
        ShowToolTipAttributeBonusInfo($(this));
    });

    $('.item-info').mouseover(function () {
        ShowToolTipItemInfo($(this));
    });

    $('.attribute-editable').on('change', null, function (event) {
        SetAttributeValue($(this).parent().parent().attr('id'), $(this).val());
        sleep(50).then(() => {
            ReloadAttributeBonus();
        });
    });

    $('#title-attribute-container').click(function (event) {
        $("#div-attribute-container").slideToggle('slow');
    });

    $('#title-talents-container').click(function (event) {
        $("#div-talents-container").slideToggle('slow');
    });

    $('#title-magics-container').click(function (event) {
        $("#div-magics-container").slideToggle('slow');
    });

    $('#title-privateAnnotations-container').click(function (event) {
        $("#div-privateAnnotations-container").slideToggle('slow');
    });

    $('#title-items-container').click(function (event) {
        $("#div-items-container").slideToggle('slow');
    });

});

function ShowToolTipItemInfo(obj) {
    var url = '/Item/GetInfo/?ItemId=' + jQuery(obj).parent().attr('id');
    var tooltip = $("div[id=tooltip_" + jQuery(obj).parent().attr('id'));
    ResetToolTip(obj);

    $.getJSON(url, function (data) {
        for (var i = 0; i < data.length; i++) {
            tooltip.append('<br>' + data[i]["Key"] + ': ' + data[i]["Value"] + '<br>');
        }
    });
}

function RecalculateTotal(obj) {
    var value = parseInt(jQuery(obj).parent().parent().children('.td-editable').children('.attribute-editable').val());
    var bonus = parseInt(jQuery(obj).parent().parent().children('.td-bonus').children('.attribute-bonus').val());
    jQuery(obj).parent().parent().children('.td-total').children('.attribute-total').val(parseInt(value + bonus));
}

function ShowToolTipAttributeBonusInfo(obj) {
    var tooltip = $("div[id=tooltip_" + jQuery(obj).attr('id').split('_')[1]);
    ResetToolTip(obj);

    var url = '/CharacterAttribute/GetBonusInfo/?CharacterId=' + $('#CharacterId').val() + '&AttributeId=' + jQuery(obj).parent().parent().attr('id');
    $.getJSON(url, function (data) {
        for (var i = 0; i < data.length; i++) {
            tooltip.append('<br>' + data[i]["Key"] + ': ' + data[i]["Value"] + '<br>');
        }
    });
}

function ResetToolTip(obj) {
    var tooltip = $("div[id=tooltip_" + jQuery(obj).attr('id').split('_')[1]);
    tooltip.empty();
}

function ResetValuesOfAttribute(obj) {
    jQuery(obj).parent().parent().children('.td-total').children('.attribute-total').val(0);
    jQuery(obj).val(0);
}

function GetBonusInfoAttributes(obj) {
    var url = '/CharacterAttribute/GetBonusInfo/?CharacterId=' + $('#CharacterId').val() + '&AttributeId=' + jQuery(obj).parent().parent().attr('id');
    $.getJSON(url, function (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i]["Value"] != "--")
                jQuery(obj).val(parseInt(jQuery(obj).val()) + parseInt(data[i]["Value"]));
        }
    });
}

function SetAttributeValue(attribute, value) {
    var snackbarContainer = document.querySelector('#update-message-container');

    $.ajax(
        {
            dataType: 'JSON',
            url: '/CharacterAttribute/SetAttributeValue',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ CharacterId: $('#CharacterId').val(), AttributeId: attribute, Value: value }),

            success: function (response) {
                if (response.status == "OK") {
                    sleep(50).then(() => {
                        'use strict';
                        snackbarContainer.MaterialSnackbar.showSnackbar({ message: "Attribute changed successfully" });
                    });
                }
                else {
                    'use strict';
                    snackbarContainer.MaterialSnackbar.showSnackbar({ message: response.error });
                }
            },

            error: function (xhr) {
                'use strict';
                snackbarContainer.MaterialSnackbar.showSnackbar({ message: "An error occured when trying to create your character. Plase try again later" });
            }
        });
}

function ReloadAttributeBonus() {
    $('.attribute-bonus').each(function (index, element) {
        ResetValuesOfAttribute(element);
        GetBonusInfoAttributes(element);
        RecalculateTotal(element);
    });
}

function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
}