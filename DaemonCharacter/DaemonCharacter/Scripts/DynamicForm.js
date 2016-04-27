$(document).ready(function () {

    /*
    Script located on http://www.formget.com/jquery-codes-for-dynamic-form/ 
    http://www.formget.com/script-preview/?preview_url=http://www.aorank.com/tutorial/createformdynamically/dynamicallyformcreation.html
    */
    /*-------------------------------------------------------------------*/
    var MaxInputs = 100; //Maximum Input Boxes Allowed
    /*-------------------------------------------------------------------
    To Keep Track of Fields And Divs Added
    -------------------------------------------------------------------*/
    var AttributeFieldCount = 0;

    var InputsWrapper = $("#InputsWrapper"); // Input Box Wrapper ID
    var x = InputsWrapper.length; // Initial Field Count
    /*--------------------------------------------------------------
    To Get Fields Button ID
    ----------------------------------------------------------------*/
    var AttributeField = $("#AttributeField");

    /*---------------------------------------------------------------
    To Add Name Field
    ----------------------------------------------------------------*/

    $(AttributeField).click(function () {
        if (x <= MaxInputs) {
            AttributeFieldCount++;

            /**********************************************
            Calling a controller method using JSON
            ***********************************************/
            var Vurl = "/Attribute/GetAttributes/";
            Vurl += x;
            var result;


            $.getJSON(Vurl, function (data) {

                result = data;

            });

            $(InputsWrapper).append(result);
            x++;
        }
        return false;
    });
    $("body").on("click", ".removeclass0", function () {
        $(this).parent('div').remove(); // To Remove Name Field
        x--;
        return false;
    });

});

function SelectAttribute(obj) {
    var val = obj.id;

    //Looking if is the first click
    if ($(':hidden#SelectedAttributes').val() != "") {
        var isNotSelected = true;
        var ids = $('input[id=SelectedAttributes]').val().split(',');
        var hiddenValue = "";

        //Looking if the attribute is already selected. If yes, remove from selection and from hidden value
        for (var i = 0; i < ids.length; i++) {

            if (ids[i] == val) {
                isNotSelected = false;
                obj.style.backgroundColor = "white";
            } else {
                if (hiddenValue.length == 0)
                    hiddenValue = ids[i];
                else
                    hiddenValue = ',' + ids[i];
            }
        }

        $('input[id=SelectedAttributes]').val(hiddenValue);

        if (isNotSelected) {
            $('input[id=SelectedAttributes]').val($('input[id=SelectedAttributes]').val() + "," + val);
            obj.style.backgroundColor = "red";
        }

    } else {
        $('input[id=SelectedAttributes]').val(val);
        obj.style.backgroundColor = "red";
    }


}