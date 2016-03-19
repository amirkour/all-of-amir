/*
 * aoa namespace (as in: all-of-amir.)
 *
 * Provides some utility for browser-related stuff, like error handling/rendering.
 *
 * Depends on jQuery, Bootstrap, and the presence of the global JSON object 
 * (which old-school browsers won't have.)
 */
(function ($, window, document, undefined) {

    // did this script get loaded someplace else?  halt if so.
    if (window.aoa) return;

    // the layout page should have a 'global' error alert we can use
    // to 'pretty-print' generic runtime errors.
    var $globalError = $('#global-error'),
        $loadingDialog = $("div#loading-dialog").modal({
            show: false,
            keyboard: false,
            backdrop: 'static'
        }),

        // all our ajax posts should include the csrf anti-forgery token,
        // which can be included by MVC if you use @Html.AntiForgeryToken
        // in one of the cshtml files.  let's stash the token for use later:
        token_input_name = '__RequestVerificationToken',
        token = $("input[name='" + token_input_name + "'").val(),
        token_encoded = encodeURI(token);

    /*
     * helper that will ensure any/all global errors on the page are hidden
     */
    function clearGlobalErrors(){
        $globalError.hide();
    }

    /*
     * helper that will display the given error message on the page
     * in some kind of uniform way.
     */
    function displayErrorMessage(msg){
        msg = (typeof msg === 'string') ? msg : "An error occurred, but no specific message/feedback was found ... please try again";

        // if global error section is present, use it.  otherwise, just barf an alert.
        if ($globalError.length > 0)
            $globalError.text(msg).show();
        else
            alert(msg);
    }

    // here's our global aoa namespace - it'll get exposed to the rest of the page
    // at the bottom of this file
    var aoa = {

        /*
         * helper that displays an error on the page
         */
        showError: function(msg){
            if(typeof msg !== 'string') return;
            displayErrorMessage(msg);
        },

        /*
         * helper that clears all global feedback on the page
         */
        clearGlobalFeedback: function(){
            clearGlobalErrors();
        },

        /*
         * this helper will display a loading dialog on the page and prevent
         * user input from being accepted until it's hidden again.
         * the dialog is courtesy of bootstrap.
         */
        loading: function(loading){
            if (typeof loading === 'undefined' || loading === true)
                $loadingDialog.modal('show');
            else
                $loadingDialog.modal('hide');
        },

        /*
         * error handler for ajax requests - expects the given jqXHR
         * from jquery to have come back from our BaseController.OnException
         * (which implies a certain type of error formatting.)
         *
         * NOTE: 'this' is gonna be whatever jQuery sets it to (unless you modify
         *       the way this handler is called.)
         */
        ajaxErrorHandler: function (jqXHR, textStatus, errorMsg) {
            if (!jqXHR) return;

            var standardizedError = jqXHR.responseJSON ? jqXHR.responseJSON : jqXHR.responseText,
                errorMsg = null;

            // if it's responseText, turn it into an object
            if (typeof standardizedError === 'string') standardizedError = JSON.parse(standardizedError);

            // if the 'Error' property doesn't exist, this error didn't originate
            // from BaseController.OnException, and we can't handle it elegantly
            errorMsg = (typeof standardizedError.Error === 'string') ? standardizedError.Error : "Unrecognized error format";
            displayErrorMessage(errorMsg);
        },

        /*
         * this callback can be used in conjunction with $.ajaxSend to t-up
         * some functionality for ajax post requests (for example: ensuring our
         * ajax post requests include a csrf token.)
         *
         * NOTE: 'this' is gonna be whatever jQuery sets it to (unless you
         *       modify the way this handler is called.)
         */
        ajaxSendHandler: function (event, jqXHR, ajaxOptions) {

            // before every ajax request, ensure global errors are cleared away
            // so that any new ajax-related errors take precedence
            clearGlobalErrors();

            // for the moment, we'll only add csrf tokens to POST requests
            if (typeof ajaxOptions.type !== 'string') return;
            if (ajaxOptions.type.toUpperCase() !== 'POST') return;

            if (typeof ajaxOptions.data !== 'string' || ajaxOptions.data.length <= 0)
                ajaxOptions.data = token_input_name + "=" + token_encoded;
            else if (ajaxOptions.data.length > 0)
                ajaxOptions.data += "&" + token_input_name + "=" + token_encoded;
        }
    }; // end of window.aoa namespace

    // now t-up our ajaxSend helper
    $(document).ajaxSend(aoa.ajaxSendHandler);

    // and then expose our namespace to the world
    window.aoa = aoa;

})(jQuery, window, document);