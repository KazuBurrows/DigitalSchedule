
/** 
 * Retrieves data from controller.
 * @param {controller} string Specify what controller to use
 * @param {method} string Specify what method to use
 * @returns {}
 */
function AjaxGetPartial(controller, method, id)
{
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/' + controller + '/' + method + '/',
        data: { id },
    }).done(function (result) {
        console.log("success", result)
        $('#modalContent').html(result);
    }).fail(function (err) {
        console.log("error", err);
    });


}

