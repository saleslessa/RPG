function ResetMessageSummary() {
    $("#MessageSummary").hide();
    $("#MessageSummary").empty();
}


function MountModelStateErrorMessage(model) {
    var div = '<div class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">×</button><p><strong>Oops! something goes wrong:<strong></p><ul>';

    for (var i = 0; i < model.length; i++) {
        div += '<li>' + model[i].Key.replace("model.", "") + ': ' + model[i].Value + '</li>';
    }

    div += '</ul></div>';
    $("#MessageSummary").append(div);
}

function MountValildationResultError(model) {
    var div = '<div class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">×</button><p><strong>Oops! something goes wrong:</strong></p><ul>';

    for (var i = 0; i < model.Erros.length; i++) {
        div += '<li>' + model.Erros[i].Message + '</li>';
    }

    div += '</ul></div>';
    $("#MessageSummary").append(div);
}

function MountSuccessMessage(message) {
    $("#MessageSummary").append('<div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button><p><strong>' + message + '</strong></p></div>');
}


function SaveModel(model, action) {
    $.ajax(
        {
            dataType: 'JSON',
            url: action,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ model: model }),

            success: function (response) {
                if (response.error == "")
                    MountSuccessMessage(response.message);

                if (response.error == "ModelStateError")
                    MountModelStateErrorMessage(response.model);

                if (response.error == "ValildationResultError")
                    MountValildationResultError(response.model);

                $("#MessageSummary").slideToggle('slow');
            },
            error: function (xhr) {
                alert("An error occured when trying to create your character. Plase try again later");
            }
        });
}

function GetAttribute(attributeId)
{
    $.ajax(
       {
           dataType: 'JSON',
           url: '/Attribute/Get/?id=' + attributeId,
           type: 'GET',
           contentType: 'application/json',

           success: function (response) {
                   return response;
           },
           error: function (xhr) {
               alert("An error occured when trying to create your character. Plase try again later");
           }
       });
}

function MountAttributeBonus() {
    var objAttributes = [];

    $(".AttributeBonus").each(function (index, element) {
        var item = {};
        item['AttributeId'] = jQuery(element).parent().parent().attr('id');
        item['AttributeName'] = jQuery(element).parent().siblings('.tdAttributeName').text().trim();
        item['Selected'] = jQuery(element).children('input')[0].checked;

        objAttributes.push(item);
    });

    return objAttributes;
}