
// for portfoli filter jquary

$(document).ready(function () {
	
	$("#status").fadeOut(); // will first fade out the loading animation
	$("#preloader").delay(350).fadeOut("slow"); // will fade out the white DIV that covers the website.	

});

	// Somth page scroll
    $('#navigation a, .smooth, a[href^="#theCouple"]').on( "click", function() {
    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
      if (target.length) {
        $('html,body').animate({
          scrollTop: target.offset().top
        }, 1000);
        return false;
      }
    }
    });

    $('.goToTop').on("click", function () {
        window.scrollTo(0, 0);
    });

