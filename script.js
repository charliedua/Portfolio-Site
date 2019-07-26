$(document).ready(function() {

    // process the form
    $('form').submit(function(event) {


        // get the form data
        // there are many ways to get this data using jQuery (you can use the class or id also)
        var formData = {
            'Name'      : $('input[name=name]').val(),
            'Email'     : $('input[name=email]').val(),
            'Comment'   : $('textarea[name=Comment]').val()
        };

        // process the form
        $.ajax({
            type        : 'POST', // define the type of HTTP verb we want to use (POST for our form)
            url         : 'https://localhost:5001/api/form/', // the url where we want to POST
            data        : formData, // our data object
            dataType    : 'json', // what type of data do we expect back from the server
            encode      : true
        })
        // using the done promise callback
        .done(function() {
            $(".form-container span").show();
            // here we will handle errors and validation messages
        });

        // stop the form from submitting the normal way and refreshing the page
        event.preventDefault();
    });
});