
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$('.sidebar-nav li').click(function (e) {
    if ($(this).find('#imgArrow').length > 0) {
        $(this).find('ul').toggleClass('open');
        $(this).find('#imgArrow').toggleClass('Up');
    }
    else {
        $('.sidebar-nav').find('span').removeClass('selected');
        $(this).find('span').toggleClass('selected');
    }
})

$('.sidebar-nav li ul li').click(function (e) {
    $('.sidebar-nav').find('span').removeClass('selected');

    $(this).parent().toggleClass('open');
    $(this).parent().parent().find('#imgArrow').toggleClass('Up');
    $(this).parent().parent().parent().find('span').toggleClass('selected');
});