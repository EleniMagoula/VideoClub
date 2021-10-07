$(document).on('click', '#create-movie-form', function () {
    $('#form-outer').css('display', 'block');
})

var movieForm = movieForm ||
{
    init: function () {
        this.listeners
    },
    listeners: function () {
        $(document).on('click', '.movie-submit', function (e) {
            e.preventDefault();
            var form = $('#movie-form');
            form.submit();
        })
    },
    showResult: function () {
        $('#form-outer').hide('slow');
        $('#form-result').show('slow');
    }
}