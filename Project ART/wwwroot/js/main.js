(function ($) {
    "use strict";

    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });



    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 0) {
            $('.navbar').addClass('nav-sticky');
        } else {
            $('.navbar').removeClass('nav-sticky');
        }
    });

    // Logo Change
    $(window).scroll(function () {
        if ($(this).scrollTop() > 0) {
            $('#logo').attr('src', 'img/black.png');
        } else {
            $('#logo').attr('src', 'img/White1.png');
        }
    });


})(jQuery);

