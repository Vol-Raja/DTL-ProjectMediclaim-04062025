
    $('.sub-menu div').hide();
   $(".sub-menu a").click(function () {
	$(this).parent(".sub-menu").children("div").slideToggle("100");
	$(this).find(".right").toggleClass("fa-caret-up fa-caret-down");
});
// $('.sidebar_nav li a').click(function(){
//     $('li a').removeClass("active");
//     $(this).addClass("active");
//   });
