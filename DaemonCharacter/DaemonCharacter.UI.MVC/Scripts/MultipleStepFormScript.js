$(document).ready(function () {

    $('#TableSearch').on('click', '.clickable-row', function (event) {
        //var row = '<tr id="' + $(this).attr('id') + '">';

        //row += '<td>' + this.cells[0].innerText + '</td>';
        //row += '<td class="TdAttributeValue"><input type="number" name="CharacterAttributeValue" class="CharacterAttributeValue" min="0" class="form-control" /></td>';
        //row += '<td><input type="button" class="clickable-row btn btn-danger" value=" - " />';

        //row += '</tr>';

        $('#TableSelected').DataTable().row.add({
            
            "0": '<span data-id="" data-description="' + this.cells[1].innerText + '">' + this.cells[0].innerText + '</span>',
            "1" : '<input type="number" name="CharacterAttributeValue" class="CharacterAttributeValue" min="0" class="form-control" />',
            "2" : '<input type="button" class="clickable-row btn btn-danger" value=" - " />'
        }).draw();

        $('#TableSearch').DataTable().row($(this)).remove().draw();
    });

    $('#TableTalents').on('click', '.clickable-row', function (event) {
        $(this).closest('tr').toggleClass("active");
    });


    $('#TableSelected').on('click', '.clickable-row', function (event) {
        $('#TableSelected').DataTable().row($(this).parents('tr')).remove().draw();

        $('#TableSearch').DataTable().row.add({

            "0": $(this).parent().parent().children()[0].innerText,
            "1": jQuery($(this).parent().parent().children()[0].getElementsByTagName('SPAN')[0]).attr("data-description"),
           
        }).draw();
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
            //InitTable('table-item-selected');
        }
        else {
            if (GetQtdPlayerItem($(this)) > 0)
                SetSelectedItem($(this), GetItemNamePlayerItem($(this)));
        }

        GetTotalInvested();
    });

    $('#table-item-selected').on('click', '.btnRemoveItem', function (event) {
        $(this).parent().parent().remove();
        //InitTable('table-item-selected');
        GetTotalInvested();
    });

    $("input[name=btnCreatePlayer]").on('click', null, function (event) {
        var model = MountBasicInformation();
        model.SelectedAttributes = MountAttributes();
        model.SelectedItems = MountItems();

        ResetMessageSummary();

        SaveModel(model, '/Player/Create/');
    });

    $('#PlayerMoney').on('change', null, function (event) {
        $('#input-total-money').val($(this).val());
        VerifyTotalInvested();
    });
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
    objBasicInfo.PlayerPointsToDistribute = $('#PlayerPointsToDistribute').val();
    objBasicInfo.CharacterRace = $('#CharacterRace').val();
    objBasicInfo.CharacterGender = $('#CharacterGender').val();
    objBasicInfo.PlayerBackground = $('#PlayerBackground').val();

    return objBasicInfo;
}

function InitTable(targetTable) {

    var table = jQuery('#' + targetTable);

    /* Fixed header extension: http://datatables.net/extensions/scroller/ */

    var oTable = table.dataTable({
        "dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // datatable layout without  horizobtal scroll
        "scrollY": "300",
        "deferRender": true,
        "order": [
            [0, 'asc']
        ],
        "lengthMenu": [
            [5, 15, 20, -1],
            [5, 15, 20, "All"] // change per page values here
        ],
        "pageLength": 5 // set the initial value
    });


    //var tableWrapper = jQuery('#' + targetTable); // datatable creates the table wrapper by adding with id {your_table_jd}_wrapper
    //tableWrapper.find('.dataTables_length select').select2(); // initialize select2 dropdown
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

    $("#TableTalents > tbody > tr").each(function (index, element) {
        if ($(this).attr('class').indexOf('active') != -1) {
            var item = {};
            item['AttributeId'] = element.id;
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

    $('#TableSearch').DataTable({
        ajax: url
    });

    //$('#TableSearch tbody').empty();

    //$.getJSON(url, function (data) {

    //    for (var i = 0; i < data.length; i++) {
    //        var item = JSON.parse(JSON.stringify(data[i]));

    //        if (!VerifySelectedId(item.AttributeId)) {
    //            var row = "<tr class='clickable-row' style='cursor:pointer' id='" + item.AttributeId + "'>";
    //            row += "<td>" + item.AttributeName + "</td>";
    //            row += "<td>" + item.AttributeDescription + "</td>";
    //            row += "</tr>";

    //            $('#TableSearch tbody').append(row);
    //        }
    //    }
    //});

    //InitTable('TableSearch');
}

function VerifySelectedId(value) {
    var result = false;
    $.each($('#TableSelected tbody tr'), function (index, element) {
        if (value == element.id) result = true;

    });

    return result;
}

