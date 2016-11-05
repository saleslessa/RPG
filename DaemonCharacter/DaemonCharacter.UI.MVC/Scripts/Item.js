$(document).ready(function () {
    $("input[name=CreateItem]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(GetModel(), '/Item/Create');
    });

    $("input[name=SaveItem]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(GetModel(), '/Item/Edit');
    });
});

function GetModel() {
    var model = MountObject();
    model.ItemAttribute = MountItemAttribute();
    return model;
}

function MountObject() {
    var obj = new Object();
    obj.ItemId = $('#ItemId').val();
    obj.ItemName = $('#ItemName').val();
    obj.ItemEffect = $('#ItemEffect').val();
    obj.ItemPrice = $('#ItemPrice').val();
    obj.UniqueUse = $('#UniqueUse').children('input')[0].checked;
    obj.ItemCategory = $('#ItemCategory').val();

    return obj;
}

function MountItemAttribute() {
    var obj = [];

    $("#TableBonus tbody tr").each(function (index, element) {
        var item = {};
        item['Attribute'] = new Object();
        item['Attribute']['AttributeId'] = jQuery(element).attr('id');
        item['ItemAttributeValue'] = jQuery(element).children('.tdAttributeValue').children('.ItemAttributeValue').val();

        obj.push(item);
    });
    console.log(obj);
    return obj;
}