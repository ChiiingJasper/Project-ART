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
            $('#logo').attr('src', "https://cdn.discordapp.com/attachments/980368132365488159/1031586185887354940/black.png");
        } else {
            $('#logo').attr('src', "https://media.discordapp.net/attachments/980368132365488159/1031586185425985676/White1.png");
        }
    });


})(jQuery);

