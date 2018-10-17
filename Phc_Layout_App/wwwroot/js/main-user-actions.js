$(document).ready(function () {
    $("#app-menu-links").click(function (e) {
        e.preventDefault();
        $("#iFrame-window").attr("src", $(this).attr("href"));
    })
});
