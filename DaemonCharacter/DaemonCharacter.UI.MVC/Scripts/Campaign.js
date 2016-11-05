$(document).ready(function () {

    $("input[name=CreateCampaign]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(MountObject(), '/Campaign/Create');
    });

    $("input[name=SaveCampaign]").on('click', null, function (event) {
        ResetMessageSummary();
        SaveModel(MountObject(), '/Campaign/Edit');
    });

});


function MountObject() {
    var obj = new Object();
    obj.CampaignName = $('#CampaignName').val();
    obj.CampaignShortDescription = $('#CampaignShortDescription').val();
    obj.CampaignBriefing = $('#CampaignBriefing').val();
    obj.CampaignStartYear = $('#CampaignStartYear').val();
    obj.CampaignMaxPlayers = $('#CampaignMaxPlayers').val();
    obj.CampaignImg = $('#CampaignImg').val();
    obj.CampaignStatus = $('#CampaignStatus').val();
    //console.log(obj);
    return obj;
}

function GetUserMaster(id) {
    var url = '/Session/GetUserMaster/';

    $.ajax(
    {
        url: url,
        type: 'GET',
        data: id,

        success: function (response) {
            return response;
        },
        error: function (xhr) {
            alert(xhr.statusText);
            return null;
        }
    });

}

function HideSessionOptions() {
    $('.MySession').each(function (index, element) {
        element.style.display = "none";
    });
}