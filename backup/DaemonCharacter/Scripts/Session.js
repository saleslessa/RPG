$(document).ready(function () {

    $('select[id=SelectNPC]').change(function () {

        var url = '/Session/_GridNonPLayer/';

        $.ajax(
        {
            url: url,
            type: 'GET',
            data: "idNPC=" + this.value,
            datatype: "html",

            success: function (obj) {

                $('#GridNPC tbody').append(obj);

            },
            error: function (xhr) {
                alert(xhr.statusText);
                return null;
            }
        });

    });

});