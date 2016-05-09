$(document).ready(function () {

    $('input[type=checkbox]').click(function () {

        if (this.id.split('_')[0] == 'ChkAttr' && this.checked == true)
            GetMinimumValueFromAttribute(this.id);
        else
            ClearValueFromAttribute(this.id);

    });

    $('input').change(function(){
        if (document.getElementsByName('pointsToDistribute')[0].id == this.id)
            document.getElementsByName('remainingPoints')[0].value = document.getElementsByName('pointsToDistribute')[0].value;
    });


    $('input[id=submitAttributes]').click(function () {
        var url = "/CharacterAttribute/Create/";
        sessionStorage.clear();
        SelectAttributesFromSelectedCheckboxes();

        $.ajax(
            {
                dataType: 'json',
                //contenttype: 'application/json; charset=utf-8',
                url: url,
                data: 'Attributes=' + JSON.stringify(String(sessionStorage["ArrayOfSelectedAttributes"])),
                success: function (data) {
                    document.getElementById("CharacterAttributeMessage").innerText = data;
                },
                error: function (xhr) {
                    document.getElementById("CharacterAttributeMessage").innerText = xhr.statusText;
                }
            });
    });
});

function SelectAttributesFromSelectedCheckboxes() {
    var Attribute = [];
    $('input:checkbox').each(function (index, element) {
        if (element.checked) {
            var idAttribute = element.id.split('_')[1];
            AddAttributeToArray(Attribute, document.getElementById('ValAttr_' + idAttribute));
        }
    });
    sessionStorage.setItem("ArrayOfSelectedAttributes", Attribute);
}

function AddAttributeToArray(Attribute, SelectedAttribute) {
    Attribute.push(SelectedAttribute.id.split('_')[1] + '|' + SelectedAttribute.value);
}

function setCheckbox(id) {
    id.click();
}

function ClearValueFromAttribute(id) {
    var elementId = "ValAttr_" + id.split('_')[1];
    document.getElementById(elementId).value = "";
    CalculateRemainingPoints(document.getElementById(elementId));
}
function GetMinimumValueFromAttribute(id) {
    var url = "/Attribute/FindMinimum/" + id.split('_')[1];
    $.ajax(
     {
         dataType: 'json',
         type: 'get',
         url: url,
         //data: 'id=' + id,
         data: null,
         success: function (data) {
             var elementId = "ValAttr_" + id.split('_')[1];
             SetMinimumValueToSelectedInput(document.getElementById(elementId), data);
         },
         error: function (xhr) {
             $("#CharacterAttributeMessage").value = xhr.statusText;
         }
     });
}
function SetMinimumValueToSelectedInput(obj, value) {
    if (obj.value == "" || obj.value < value) {
        obj.value = value;
        obj.setAttribute("min", value);
    }

    CalculateRemainingPoints(obj);
}

function CalculateRemainingPoints(obj) {
    var sumPointsUsed = 0;
    $('input').each(function (index, element) {
                
        if (element.id.split('_')[0] == "ValAttr") {
            if(element.value != "")
                sumPointsUsed = sumPointsUsed + parseInt(element.value);
        }
    });

    document.getElementsByName('remainingPoints')[0].value = document.getElementsByName('pointsToDistribute')[0].value - sumPointsUsed;
}
