$(document).ready(function () {
    $("a[name=CreateAttribute]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(GetModel(), '/Attribute/Create');
    });

    $("select[name=AttributeType]").on('change', null, function (event) {
        SetMinimum($(this));
    });

    $("input[name=SaveAttribute]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(GetModel(), '/Attribute/Edit');
    });

    SetMinimum($("select[name=AttributeType]"));
});

function GetModel() {
    var model = MountObject();
    model.AttributeBonus = MountAttributeBonus();

    return model;
}

function SetMinimum(obj) {
    if (jQuery(obj).val() == '0' || jQuery(obj).val() == '2') {//Characteristic or Talent
        $('#div-minimum').css('display', 'none');
        $('#AttributeMinimum').val('0');
    }
    else {
        $('#div-minimum').css('display', 'block');
    }
}

function MountObject() {
    var obj = new Object();
    obj.AttributeId = $('#AttributeId').val();
    obj.AttributeName = $('#AttributeName').val();
    obj.AttributeDescription = $('#AttributeDescription').val();
    obj.AttributeType = $('#AttributeType').val();
    obj.AttributeMinimum = $('#AttributeMinimum').val();

    return obj;
}