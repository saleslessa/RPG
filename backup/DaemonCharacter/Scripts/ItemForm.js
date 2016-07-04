$(document).ready(function () {

    $('input[id=AddAttributeButton]').click(function () {
        var x = GetNextAttributeToAdd();
        var url = '/ItemAttribute/Create/?id=' + x;

        $.ajax({

            url: url,
            type: "get",
            success: function (response) {
                $(ItemAttributeContainer).append(response);
            }

        });

    });

    $('input[id=RemoveAllAttributeButton]').click(function () {
        
        $('.RemoveItemAttribute').each(function (index, element) {

            $(element).parent().fadeOut("slow", function () {
                $(this).remove();
            });

        });

    });

});

function GetNextAttributeToAdd() {
    var x = 0;
    $('.RemoveItemAttribute').each(function () {
        x++;
    });

    return x + 1;
}