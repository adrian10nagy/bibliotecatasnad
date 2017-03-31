$(".testimonialText li").fadeTo("fast", 0);
$(".testimonialText li:first").fadeTo("fast", 1);
$(".imageSlide .imageBox").removeClass("activeImage");
$(".imageSlide .imageBox:first").addClass("activeImage");
$(".imageSlide .imageBox").mouseenter(function () {
    if (!$(this).hasClass("activeImage")) {
        var gi = $(this).index();
        //console.log(gi);
        $(".imageSlide .imageBox").removeClass("activeImage");
        $(this).addClass("activeImage");
        $(".testimonialText li").fadeTo("fast", 0);
        $(".testimonialText li:eq(" + gi + ")").fadeTo("fast", 1);
    }
})

function notifyNewUser() {
    $.notify(
        "Poți rezerva cârțile dorite!",
        {
            className: 'info',
            clickToHide: true,
            autoHide: true,
            autoHideDelay: 11000,
            arrowShow: true,
            showAnimation: 'slideDown',
            showDuration: 2000,
            hideAnimation: 'slideUp',
            hideDuration: 2000,
            globalPosition: 'right',
            gap: 4

        });

    $.notify(
        "Cont creat cu succes! ",
        {
            className: 'success',
            clickToHide: true,
            autoHide: true,
            autoHideDelay: 12000,
            arrowShow: true,
            showAnimation: 'slideDown',
            showDuration: 2000,
            hideAnimation: 'slideUp',
            hideDuration: 2000,
            globalPosition: 'right',
            gap: 4

        });
}