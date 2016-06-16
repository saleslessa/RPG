$(document).ready(function () {

    $('input[type=checkbox]').click(function () {

        if (this.id.split('_')[0] == 'ChkAttr' && this.checked == true)
            GetMinimumValueFromAttribute(this.id);
        else
            ClearValueFromAttribute(this.id);

    });

    $('input').change(function () {
        if (document.getElementsByName('pointsToDistribute')[0].id == this.id)
            document.getElementsByName('remainingPoints')[0].value = document.getElementsByName('pointsToDistribute')[0].value;
    });

    $("select[id=campaigns]").change(function () {
        var url = '/Campaign/GetSelectedCampaign/';

        $.ajax(
        {
            //dataType: 'JSON',
            url: url,
            type: 'GET',
            data: 'idCampaign=' + this.value,

            success: function (response) {

                if (response.length == 0)
                    document.getElementById('campaign-container').innerHTML = "";
                else {
                    var r = response.split('|');
                    document.getElementById('campaign-container').innerHTML =
                     '<table cellspacing="3" class="dynamic-table-campaign" title="' + r[1] + '">' +
                        '<tr>' +
                            '<td><strong>Master:</strong></td><td>' + r[3] + '</td>' +
                            '<td><strong>Name:</strong></td><td>' + r[0] + '</td>' +
                            '<td><strong>Remaining players:</strong></td><td>' + r[2] + '</td>' +
                        '</tr>' +
                    '</table>';
                }
            },
            error: function (xhr) {
                document.getElementById('campaign-container').innerHTML = "";
                document.getElementById("messages").innerText = xhr.statusText;
            }
        });
    });


    //Submission form with attributes
    $('input[id=submitAttributes]').click(function () {
        var url = "/CharacterAttribute/CreateCharacter/";
        var form = $("#formCharacterAttributes").serialize();

        var model = document.getElementById('model').value;


        if (model == "NonPlayerModel")
            SubmitNonPlayer();


        $.ajax(
            {
                dataType: 'json',
                type: 'POST',
                url: url,
                data: form,
                success: function (response) {
                    document.getElementById("messages").innerText = response;

                    if (model != "NonPlayerModel")
                        $("#dynamic-side-right").slideToggle("slow");
                },
                error: function (xhr) {
                    document.getElementById("messages").innerText = xhr.statusText;
                }
            });
    });


    //Submission form initial
    $('input[id=submitPlayer]').click(function () {
        var url = "/Character/CreatePlayer/";
        var form = $("#formCreatePlayer").serialize();

        $.ajax(
            {
                dataType: 'json',
                type: 'POST',
                url: url,
                data: form,

                success: function (response) {
                    document.getElementById("messages").innerText = response;


                    $("#dynamic-side-right").slideToggle("slow");
                    document.getElementById("dynamic-side-right").style.display = "normal";
                    $("#formCreatePlayer").slideToggle("slow");


                },
                error: function (xhr) {
                    document.getElementById("messages").innerText = xhr.statusText;
                }
            });
    });




});

function SubmitNonPlayer()
{
    var url = "/Character/CreateNPC/";
    var form = $("#formCreateNonPlayer").serialize();

    $.ajax(
        {
            dataType: 'json',
            type: 'POST',
            url: url,
            data: form,

            success: function (response) {
                document.getElementById("messages").innerText = response;
            },
            error: function (xhr) {
                document.getElementById("messages").innerText = xhr.statusText;
            }
        });
}

function unselectAttributes() {

    $('input:checkbox').each(function (index, element) {
        if (element.checked) {
            setCheckbox(element);
        }
    });

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
            if (element.value != "")
                sumPointsUsed = sumPointsUsed + parseInt(element.value);
        }
    });

    document.getElementsByName('remainingPoints')[0].value = document.getElementsByName('pointsToDistribute')[0].value - sumPointsUsed;
}
