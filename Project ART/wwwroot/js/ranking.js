$(document).ready(function () {
    var i = 1;
    $('h3.ranking').each(function () {
        var string = '#' + (i);
        if (i == 1) {
            string += ' <i class="fa-solid fa-medal " style="color:gold"></i>';
        }
        if (i == 2) {
            string += ' <i class="fa-solid fa-medal " style="color:silver"></i>';
        }
        if (i == 3) {
            string += ' <i class="fa-solid fa-medal " style="color:sandybrown"></i>';
        }
        $(this).html(string);  //or $(this).text();
        i++;
    });
});



