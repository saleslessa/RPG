$(document).ready(function () {

    $('#TableSearch').on('click', '.clickable-row', function (event) {
        $('#TableSelected').DataTable().row.add({

            "0": '<span id="' + $(this).attr('id') + '" data-description="' + $($($(this).parent().parent()).children()[1]).text() + '">' + $(this).text() + '</span>',
            "1": '<input type="number" name="CharacterAttributeValue" class="CharacterAttributeValue textbox-small" min="0" class="form-control" />&nbsp;<input type="button" class="btn btn-default button-table btn-3" value="+3" />&nbsp;<input type="button" class="btn btn-default button-table btn-5" value="+5" />',
            "2": '<a href="javascript:void(0);" class="button btn btn-danger clickable-row button-table"> - </a>'
        }).draw();

        $('#TableSearch').DataTable().row($(this).parent().parent()).remove().draw();
    });


    $('#TableSelected').on('click', '.btn-5', function (event) {
        var val = $(this).siblings('.CharacterAttributeValue').val();
        $(this).siblings('.CharacterAttributeValue').val(parseInt((val == "" ? 0 : val)) + 5);
    });

    $('#TableSelected').on('click', '.btn-3', function (event) {
        var val = $(this).siblings('.CharacterAttributeValue').val();
        $(this).siblings('.CharacterAttributeValue').val(parseInt((val == "" ? 0 : val)) + 3);
    });


    $('#TableTalents').on('click', '.clickable-row', function (event) {
        $(this).closest('tr').toggleClass("active");
    });


    $('#TableSelected').on('click', '.clickable-row', function (event) {
        $('#TableSelected').DataTable().row($(this).parents()).remove().draw();
        $('#TableSearch').DataTable().row.add({

            "0": "<span id='" + jQuery($(this).parent().parent().children()[0].getElementsByTagName('SPAN')[0]).attr("id") + "' class='clickable-row' style='cursor:pointer;'>" + $(this).parent().parent().children()[0].innerText + "</span>",
            "1": jQuery($(this).parent().parent().children()[0].getElementsByTagName('SPAN')[0]).attr("data-description")

        }).draw();
    });


    $('input[name=ItemQtd]').on('change', null, function (event) {
        var price = GetPricePlayerItem($($($(this).parent().parent().children()[5]).children()[0]).attr('id'));
        var qtd = parseInt($(this).val() == "" ? 0 : $(this).val());

        if (price == 0 || qtd == 0)
            $($($(this).parent().parent().children()[4]).children()[0]).val("");
        else
            $($($(this).parent().parent().children()[4]).children()[0]).val(price * qtd);

    });

    $('#table-item-available').on('click', '.btnIncludeItem', function (event) {

        var table = $('#table-item-selected').DataTable();

        var index = table
            .column(0)
            .data()
            .indexOf(GetItemNamePlayerItem($(this).attr('id')));

        if (index == -1 && GetQtdPlayerItem($(this).attr('id')) > 0) {
            
            $('#table-item-selected').DataTable().row.add({
                "0": GetItemNamePlayerItem($(this).attr('id')),
                "1": GetPricePlayerItem($(this).attr('id')),
                "2": GetQtdPlayerItem($(this).attr('id')),
                "3": GetPricePlayerItem($(this).attr('id')) * GetQtdPlayerItem($(this).attr('id')),
                "4": "<a href='javascript:void(0);' id='" + $(this).attr('id') + "' class='btnRemoveItem button btn btn-danger button-table'> - </a>"
            }).draw();

        } else {
            if (GetQtdPlayerItem($(this).attr('id')) > 0) {
                SetSelectedItem($(this).attr('id'));

            }
        }
        //        GetTotalInvested();
    });

    $('#table-item-selected').on('click', '.btnRemoveItem', function (event) {
        $('#table-item-selected').DataTable().row($(this).parents('tr')).remove().draw();
        GetTotalInvested();
    });

    $("a[id=btnCreatePlayer]").on('click', null, function (event) {

        if (confirm('Are you sure you want to finalize the creation of the player?')) {
            var model = MountBasicInformation();
            model.SelectedAttributes = MountAttributes();
            model.SelectedItems = MountItems();

            ResetMessageSummary();

            SaveModel(model, '/Player/Create/');
        }

        //$.SmartMessageBox({
        //    title: "",
        //    content: "Do you confirm this operation?",
        //    buttons: "[Cancelar][Confirmar]"
        //    }, function (ButtonPress) {
        //        if (ButtonPress === "Confirmar") {
        //            console.log('teste sucesso');
        //        }
        //    });

        //var model = MountBasicInformation();
        //model.SelectedAttributes = MountAttributes();
        //model.SelectedItems = MountItems();

        //ResetMessageSummary();

        //SaveModel(model, '/Player/Create/');
    });

    $('#PlayerMoney').on('change', null, function (event) {
        $('#input-total-money').val($(this).val());
        VerifyTotalInvested();
    });


    $('#table-item-selected').DataTable().draw();
    $('#table-item-available').DataTable().draw();
});

function MountBasicInformation() {
    var objBasicInfo = new Object();
    objBasicInfo.SelectedCampaignId = $('#SelectedCampaignId').val();
    objBasicInfo.CharacterName = $('#CharacterName').val();
    objBasicInfo.CharacterMaxLife = $('#CharacterMaxLife').val();
    objBasicInfo.CharacterRemainingLife = $('#CharacterMaxLife').val();
    objBasicInfo.PlayerAge = $('#PlayerAge').val();
    objBasicInfo.PlayerExperience = $('#PlayerExperience').val();
    objBasicInfo.PlayerLevel = $('#PlayerLevel').val();
    objBasicInfo.PlayerMoney = $('#PlayerMoney').val();
    objBasicInfo.CharacterRace = $('#CharacterRace').val();
    objBasicInfo.CharacterGender = $('#CharacterGender').val();
    objBasicInfo.PlayerBackground = $('#PlayerBackground').val();

    return objBasicInfo;
}

function InitTable(targetTable, fixedQtd) {

    var table = jQuery('#' + targetTable);

    /* Fixed header extension: http://datatables.net/extensions/scroller/ */

    if (fixedQtd == true) {
        table.DataTable({
            "dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // datatable layout without  horizobtal scroll
            "deferRender": false,
            "order": [
                [0, 'asc']
            ],
            "lengthMenu": [
                [5],
                [5] // change per page values here
            ],
            "pageLength": 5, // set the initial value
            "responsive": true,
            "bLengthChange": false,
            "bAutoWidth": true
        });
    } else {
        table.DataTable({
            "dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // datatable layout without  horizobtal scroll
            "deferRender": true,
            "order": [
                [0, 'asc']
            ],
            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],
            "pageLength": 5, // set the initial value
            "responsive": true,
            "bAutoWidth": true
        });
    }

    
}

function MountAttributes() {
    var objAttributes = [];

    if ($('#TableSelected').DataTable().rows().count() > 0) {
        $("#TableSelected > tbody > tr").each(function(index, element) {
            var value = jQuery($(this).children()[1].getElementsByTagName('INPUT')[0]).val();
            if (value > 0) {
                var item = {};
                item['AttributeId'] = jQuery($(this).children()[0].getElementsByTagName('SPAN')[0]).attr("id");
                item['Value'] = value;
                objAttributes.push(item);
            }
        });
    }
    if ($('#TableTalents').DataTable().rows().count() > 0) {
        $("#TableTalents > tbody > tr").each(function(index, element) {
            if ($(this).attr('class').indexOf('active') != -1) {
                var item = {};
                item['AttributeId'] = element.id;
                objAttributes.push(item);
            }
        });
    }
    return objAttributes;
}

function MountItems() {
    var objItems = [];

    if ($('#table-item-selected').DataTable().rows().count() > 0) {
        $("#table-item-selected > tbody > tr").each(function(index, element) {
            var value = parseFloat(jQuery($(this).children()[3]).text());
            if (value > 0) {
                var item = {};
                item['ItemId'] = $($($(this).children()[4]).children(0)).attr('id');
                item['PlayerItemQtd'] = parseInt(jQuery($(this).children()[2]).text());
                item['PlayerItemUnitPrice'] = parseFloat(jQuery($(this).children()[1]).text());
                objItems.push(item);
            }
        });
    }
    return objItems;
}

function SetSelectedItem(itemName) {


    $("#table-item-selected > tbody > tr").each(function (index, element) {
        if ($($($(element).children()[4]).children()[0]).attr('id') == itemName) {
            $($(this).children()[2]).text(GetQtdPlayerItem(itemName) + GetQtdPlayerItemSelected($($($(element).children()[4]).children()[0]).attr('id')));
            $($(this).children()[3]).text(GetPricePlayerItem(itemName) * GetQtdPlayerItemSelected($($($(element).children()[4]).children()[0]).attr('id')));
        }
    });

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

    VerifyTotalInvested();
}

function VerifyTotalInvested() {
    if (parseInt($("#input-total-invested").val()) > parseInt($('#input-total-money').val())) {
        $("#input-total-invested").addClass('alert-danger');
        $("#label-total-invested").addClass('alert-danger');
    } else {
        $("#input-total-invested").removeClass('alert-danger');
        $("#label-total-invested").removeClass('alert-danger');
    }
}

function GetItemNamePlayerItem(itemRowId) {
    var retorno = null;
    $("#table-item-available > tbody > tr").each(function (index, element) {

        if (jQuery(element.children[5].children[0]).attr('id') == itemRowId)
            retorno = jQuery(element.children[0]).text();
    });

    return retorno;
}

function GetItemNamePlayerItemSelected(itemRowId) {
    var retorno = null;
    $("#table-item-selected > tbody > tr").each(function (index, element) {

        if (jQuery(element.children[4].children[0]).attr('id') == itemRowId)
            retorno = jQuery(element.children[0]).text();
    });
    return retorno;
}

function GetPricePlayerItem(itemRowId) {
    var retorno = 0;
    $("#table-item-available > tbody > tr").each(function (index, element) {
        if (jQuery(element.children[5].children[0]).attr('id') == itemRowId)
            retorno = jQuery(element.children[2]).text();
    });

    return parseFloat(retorno);
}

function GetTotalPlayerItem(itemRowId) {
    var retorno = null;
    $("#table-item-available > tbody > tr").each(function (index, element) {

        if (jQuery(element.children[5].children[0]).attr('id') == itemRowId)
            retorno = jQuery(element.children[4].children[0]).val();
    });

    return retorno;
}

function GetQtdPlayerItem(itemRowId) {
    var retorno = null;
    $("#table-item-available > tbody > tr").each(function (index, element) {

        if (jQuery(element.children[5].children[0]).attr('id') == itemRowId)
            retorno = jQuery(element.children[3].children[0]).val();
    });

    return parseInt(retorno);
}

function GetQtdPlayerItemSelected(itemRowId) {

    var retorno = null;
    $("#table-item-selected > tbody > tr").each(function (index, element) {

        if (jQuery(element.children[4].children[0]).attr('id') == itemRowId) {
            retorno = jQuery(element.children[2]).text();
        }
    });

    return parseInt(retorno);
}