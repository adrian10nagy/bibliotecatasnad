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