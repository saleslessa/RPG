$(document).ready(function () {

    $('.attribute-bonus').mouseover(function () {
        ShowToolTipAttributeBonusInfo($(this));
    });

    $('.item-info').mouseover(function () {
        ShowToolTipItemInfo($(this));
    });

    $('.minimizeBox').on('click', null, function () {
        $(this).parent().siblings('div').slideToggle('slow');
        $(this).toggleClass('glyphicon-resize-small');
        $(this).toggleClass('glyphicon-resize-full');
    });

    $('.closeBox').on('click', null, function () {
        $(this).parent().parent().slideToggle('slow');
    });

    $('.attribute-editable').on('change', null, function (event) {
        SetAttributeValue($(this).parent().parent().attr('id'), $(this).val());
    });

    $('.text-editable').on('change', null, function () {
        SetCharacterValue($(this), $(this).attr('id'), $(this).val());
        if ($(this).attr('id') == 'CharacterMaxLife')
            ChangeLife(0, false);//Change life bar proportions
    });

    $('.change-pvt').on('click', null, function() {
        SetCharacterValue($(this), 'PrivateAnnotations', $('#PrivateAnnotations').val());
    });

    /**
     * CONTROLLER OF LIFES AND HITS
     */
    var hitBtn3 = $('a.hit-3'),
        lifeBtn3 = $('a.life-3'),
        hitBtn5 = $('a.hit-5'),
        lifeBtn5 = $('a.life-5'),
        lifeBtn1 = $('a.life-1'),
        hitBtn1 = $('a.hit-1');


    lifeBtn3.on("click", function () { ChangeLife(3, false); });
    hitBtn3.on("click", function () { ChangeLife(-3, false); });
    lifeBtn5.on("click", function () { ChangeLife(5, false); });
    hitBtn5.on("click", function () { ChangeLife(-5, false); });
    hitBtn1.on("click", function () { ChangeLife(-1, false); });
    lifeBtn1.on("click", function () { ChangeLife(1, false); });

    ChangeLife(0, false);
    /**
     * END OF CONTROLLER OF LIFES AND HITS
     */

    $('input').on('focusin', function () {
        $(this).data('val', $(this).val());
    });


    //$(window).scroll(function (e) {
    //    var $el = $('.fixedElement');
    //    var isPositionFixed = ($el.css('position') == 'fixed');
    //    if ($(this).scrollTop() > 150 && !isPositionFixed) {
    //        $('.fixedElement').css({ 'position': 'fixed', 'top': '65px' });
    //        $el.width($el.parent().width());
    //        $('#header #topNav a.logo').css({ 'height': '25px' });
    //        $('#header').css({ 'position': 'fixed', 'top': '0px', 'height' : '60px' });
    //    }
    //    if ($(this).scrollTop() <= 150 && isPositionFixed) {
    //        $('.fixedElement').css({ 'position': 'static', 'top': '80px' });
    //        $('#header').css({ 'position': 'relative', 'top': '0px', 'z-index': '999', 'height': '75px' });
    //        $('#sideNav').css({ 'z-index': '-1' });
    //    }
    //});
});



function SetCharacterValue(obj, field, fieldValue) {
    var snackbarContainer = document.querySelector('#update-message-container');

    $.ajax(
        {
            dataType: 'JSON',
            url: '/Player/SetPlayerValue',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ characterId: $('#CharacterId').val(), field: field, value: fieldValue }),

            success: function (response) {
                if (response.status == "OK") {
                    sleep(50).then(() => {
                        'use strict';
                        snackbarContainer.MaterialSnackbar.showSnackbar({ message: response.message });
                    });
                }
                else {
                    'use strict';
                    snackbarContainer.MaterialSnackbar.showSnackbar({ message: response.error });
                    if (field == 'CharacterRemainingLife') ChangeLife(fieldValue * -1, false, true);
                    $(obj).val($(obj).data('val'));
                }
            },

            error: function (xhr) {
                'use strict';
                snackbarContainer.MaterialSnackbar.showSnackbar({ message: "An error occured when trying to create your character. Plase try again later" });
                $(obj).val($(obj).data('val'));
            }
        });
}

function Slide(div) {


    $('.player-sheet-detail').each(function () {

        switch (div) {
            case "all":
                $(this).slideDown();
                break;
            case "item-magic":
                if ($(this).attr('id') == "div-magics" || $(this).attr('id') == "div-items") {
                    $(this).slideDown();
                } else {
                    $(this).slideUp();
                }
                break;
            default:
                if ($(this).attr('id') == div) {
                    $(this).slideDown();
                } else {
                    $(this).slideUp();
                }
                break;
        }

        $(this).children('p').children('.box').removeClass('glyphicon-resize-full');
        $(this).children('p').children('.box').addClass('glyphicon-resize-small');
    });


    $('#div-attribute').slideToggle();

}

function ChangeLife(val, isAbsolute, isError) {
    var hBar = $('.health-bar'),
        bar = hBar.find('.bar'),
        hit = hBar.find('.hit'),
        total = $('#CharacterMaxLife').val(),
        value = hBar.data('value');


    var newValue = isAbsolute == true ? val : value + val;

    if (newValue > total) return;

    // calculate the percentage of the total width
    var barWidth = (newValue / total) * 100;
    var hitWidth = (val / value) * 100 + "%";

    // show hit bar and set the width
    $('#CharacterRemainingLife').text(newValue);
    hit.css('width', hitWidth);
    hBar.data('value', newValue);

    setTimeout(function () {
        hit.css({ 'width': '0' });
        bar.css('width', barWidth + "%");
    }, 500);
    //bar.css('width', total - value);

    if (val != 0 && !isError)
        SetCharacterValue($('#CharacterRemainingLife'), 'CharacterRemainingLife', newValue);
}


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

            if (data[i]["Value"] != "--") {
                var objVal = isNaN(parseInt(jQuery(obj).val())) ? 0 : parseInt(jQuery(obj).val());
                var jsonVal = isNaN(parseInt(data[i]["Value"])) ? 0 : parseInt(data[i]["Value"]);

                jQuery(obj).val(objVal + jsonVal);
            }
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
            data: JSON.stringify({ characterId: $('#CharacterId').val(), attributeId: attribute, value: value }),

            success: function (response) {
                if (response.status == "OK") {
                    sleep(50).then(() => {
                        'use strict';
                        snackbarContainer.MaterialSnackbar.showSnackbar({ message: "Attribute changed successfully" });
                        ReloadAttributeBonus();
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
