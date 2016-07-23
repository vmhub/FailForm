$(document).ready(function () {
    $("#getJson").on("click", function (e) {
        e.preventDefault();
        $.getJSON("/Home/logDb", function (data) {
            console.log(data)
        });
    });
});