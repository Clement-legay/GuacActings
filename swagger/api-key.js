$(function () {
    var apiKey = window.localStorage.getItem('authorization');
    if (apiKey) {
        $('#input_apiKey').val(apiKey);
    }
    $('#input_apiKey').change(function () {
        var apiKey = $('#input_apiKey').val();
        if (apiKey && apiKey.trim() !== '') {
            window.localStorage.setItem('authorization', apiKey);
        } else {
            window.localStorage.removeItem('authorization');
        }
    });
});