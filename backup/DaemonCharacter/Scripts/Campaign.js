function GetUserMaster(id)
{
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
function HideSessionOptions()
{
    $('.MySession').each(function (index, element) {
        element.style.display = "none";
    });
}