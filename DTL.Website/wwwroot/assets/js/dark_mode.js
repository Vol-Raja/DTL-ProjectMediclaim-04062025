    $(document).ready(function () {
        var theam = parseInt(sessionStorage.getItem("theam"));
        if (theam === 1) {
            contrast();
        }
        if (theam === 0) {
            normal();
        }
        if (theam === 2) {
            orange();
        }
        if (theam === 3) {
            red();
        }
    });

    var theam;
    function contrast() {
        $('html *:not(script, style, noscript)').each(function () {
            $("body").css("background-color", "#343434");
            $("section#welcome_text").css("background-color", "#6abcbe");
            $(".content_heading").css("background-color", "#343434");
            $("#bottom_content").css("background-color", "#343434");
            $("#middle_part .news_section .content_heading").css("background-color", "#4abdac");
            $("footer .social_visitor").css("background-color", "#4abdac");
            $("#middle_part .news_section").css("background-color", "#4abdac");
            $(".live_data h6").css("background-color", "#4abdac");
            $(".row.table_data").css("border-color", "#4abdac");
            $("#facility .facility").css("background-color", "#767676");
            $("#important_notice .links").css("background-color", "#767676");
            $(".table_data .col-md-6").css("background-color", "#767676");
            $("#bottom_content .item").css("background-color", "#767676");
            $(".navbar .dropdown-menu").css("background-color", "#767676");
            $(".table_data .col-md-6").css("color", "#dddddd");
            $("ul li").css("color", "#dddddd");
            $("table td").css("color", "#dddddd");
            $("p").css("color", "#dddddd");
            $("a").css("color", "#dddddd");
            $("h1").css("color", "yellow");
            $("h2").css("color", "yellow");
            $("h3").css("color", "yellow");
            $("h4").css("color", "yellow");
            $("h5").css("color", "yellow");
            $("h6").css("color", "yellow");
            $("header .navbar-light .navbar-nav .nav-link").css("color", "white");
            $("header .navbar-light .navbar-nav .nav-link.active").css("color", "yellow");
            $("header .sub_header").css("background-color", "#095169");
            $("footer .main_footer").css("background-color", "#095169");
            $(".content_heading").removeClass("orange");
            $(".content_heading").removeClass("red");
            $(".content_heading").addClass("blue");
            $(".news_section .content_heading").css("color", "#000066");
            $(".news_section .news_div .other p").css("color", "#000066");
            $(".social_visitor .social_links h4").css("color", "#000066");
            $(".social_visitor .numbers h5").css("color", "#000066");
            $("#bottom_content .live_data h6").css("color", "#000066");
            $(".sidebar .tab a.active").css("background-color", "#095169");
            $("table thead").css("background-color", "#095169");
            $("#main_page .contact_info").css("background-color", "#095169");
            $(".btn-submit").css("background-color", "#095169");
            $(".navbar .dropdown-menu .dropdown-item").mouseenter(function () {
                $(this).css("color", "#212529");
            });

            $(".navbar .dropdown-menu .dropdown-item").mouseleave(function () {
                $(this).css("color", "#dddddd");
            });


        });
        sessionStorage.setItem("theam", 1);
    }

function normal() {
    sessionStorage.setItem("theam", 0);
        $('html *:not(script, style, noscript)').each(function () {
            $("body").css("background-color", "white");

            $("section#welcome_text").css("background-color", "rgb(5 56 107)");
            $(".title_bg ").css("background-color", "rgb(5 56 107)");
            $(".title_bg ").css("color", "white");
            $("#bottom_content").css("background-color", "#f3f3f3");
            $("#middle_part .news_section .content_heading").css("background-color", "#ffffff");
            $("footer .social_visitor").css("background-color", "rgb(5 56 107)");
            $(".row.table_data").css("border-color", "rgb(5 56 107)");
            $(".LiveDataBox h4, p").css("color", "#ffffff");
          $("#middle_part .news_section").css("background-color", "rgb(5 56 107)");
          $("#middle_part .news_section h5").css("color", "#813772 !IMPORTANT");
            $(".sidebar .tab a.active").css("background-color", "rgb(5 56 107)");

          $("#middle_part .news_section").css("background-image", "none");
          //  $("#facility .facility_tab a:hover").css("background-color", "rgb(5 56 107)");
            $("#important_notice .links").css("background-color", "white");
            $(".table_data .col-md-6").css("background-color", "white");
            $("#bottom_content .item").css("background-color", "white");
            $(".navbar .dropdown-menu").css("background-color", "white");
            $(".table_data .col-md-6").css("color", "#212529");
            $("ul li").css("color", "#212529");
            $("table td").css("color", "#212529");
      
            $("header .navbar-light .navbar-nav .nav-link").css("color", "#000066");
            $("header .navbar-light .navbar-nav .nav-link.active").css("color", "black");
            $("header .sub_header").css("color", "white");
           // $("footer .main_footer h4").css("color", "white");
           // $("footer .main_footer h2").css("color", "white");
          //  $("footer .main_footer h5").css("color", "white");
           // $("footer .main_footer p").css("color", "white");
            $(".social_links .links a").css("color", "white");
            $("#middle_part .news ul li .news_div .other p").css("color", "white !important");
          //  $("footer .main_footer li a").css("color", "white");
          //  $("footer .social_visitor h4").css("color", "white");
          //  $("footer .social_visitor h5").css("color", "white");
            $("#welcome_text h5").css("color", "white");
            $("#welcome_text h1").css("color", "white");
            $("#welcome_text p").css("color", "white");
            $("#welcome_text p a").css("color", "white");
            $("header .sub_header ul li a").css("color", "white");
            $(".sidebar .tab a.active").css("color", "white");
            $("header .sub_header ul li").css("color", "white");
            $("#main_page .contact_div h3").css("color", "white");
            $("#main_page .contact_div ul li").css("color", "white");
            $("#main_page .contact_div ul li h4").css("color", "white");
           $("#main_page .contact_div ul li p").css("color", "");
            $("#middle_part a.btn").css("color", "white");
            $("#important_notice .links .controls a").css("color", "white");
            $("header .sub_header").css("background-color", "rgb(5 56 107)");
            $("#header_section p ").css("color", "#ffffff");
            $("#header_section p a").css("color", "#ffffff");
          //  $("footer .main_footer").css("background-color", "rgb(9 9 9)");
            $(".content_heading").removeClass("orange");
            $(".content_heading").removeClass("red");
            $(".content_heading").addClass("blue");
            $(".news_section h5").css("color", "#ffffff");
            $(".news_section .whatNewSection p").css("color", "#ffffff");
            // new addition start
            $(".aim_vision .content_div").css(" border-color", "#813772");
            $(".aim_vision .content_div p").css("color", "black");
            //end
            $(".news_section h5").css("color", "#ffffff");
            $(".news_section .whatNewSection p").css("color", "#ffffff");
            $(".news_section .content_heading").css("color", "#ffffff");
            $(".news_section .content_heading").css("background", "none");
            $(".news_section .news_div .other p").css("color", "#ffffff");
            $("#bottom_content .live_data h6").css("color", "#000066");
            $(".social_visitor .social_links h4").css("color", "#ffffff");
            $(".social_visitor .numbers h5").css("color", "#ffffff");

            $("table thead").css("background-color", "rgb(5 56 107)");
            $("#main_page .contact_info").css("background-color", "rgb(5 56 107)");
            $(".btn-submit").css().css("background-color", "#000066");

        });
     
    }

    function orange() {
        sessionStorage.setItem("theam", 2);
        $('html *:not(script, style, noscript)').each(function () {

            $("body").css("background-color", "white");

            $("section#welcome_text").css("background-color", "#813772");
            $(".title_bg ").css("background-color", "#813772");
            $(".title_bg ").css("color", "white");
            $("#bottom_content").css("background-color", "#f3f3f3");
            $("#middle_part .news_section .content_heading").css("background-color", "#813772");
            $("footer .social_visitor").css("background-color", "#813772");
            $(".row.table_data").css("border-color", "#813772 ");
            $(".LiveDataBox h4, p").css("color", "#ffffff");
            $("#middle_part .news_section").css("background-color", "#813772 ");
            $("#middle_part .news_section h5").css("color", "#813772 !IMPORTANT");
            $("#middle_part .news_section").css("background-image", "none");
          //  $("#facility .facility_tab a:hover").css("background-color", "#813772");
            $("#important_notice .links").css("background-color", "white");
            $(".table_data .col-md-6").css("background-color", "white");
            $("#bottom_content .item").css("background-color", "white");
            $(".navbar .dropdown-menu").css("background-color", "white");
            $(".table_data .col-md-6").css("color", "#212529");
            $("ul li").css("color", "#212529");
            $("table td").css("color", "#212529");

       
            $("header .navbar-light .navbar-nav .nav-link").css("color", "#000066");
            $("header .navbar-light .navbar-nav .nav-link.active").css("color", "black");
            $("header .sub_header").css("color", "white");
         //   $("footer .main_footer h4").css("color", "white");
          //  $("footer .main_footer h2").css("color", "white");
          //  $("footer .main_footer h5").css("color", "white");
          //  $("footer .main_footer p").css("color", "white");
            $(".social_links .links a").css("color", "white");
            //$("#middle_part .news ul li .news_div .other p").css("color", "white");
         //   $("footer .main_footer li a").css("color", "white");
         //   $("footer .social_visitor h4").css("color", "white");
           // $("footer .social_visitor h5").css("color", "white");
            $("#welcome_text h5").css("color", "white");
            $("#welcome_text h1").css("color", "white");
            $("#welcome_text p").css("color", "white");
            $("#welcome_text p a").css("color", "white");
            $("header .sub_header ul li a").css("color", "white");
            $(".sidebar .tab a.active").css("color", "white");
            $(".Inner_section_ev p").css("color", "white")
            $("header .sub_header ul li").css("color", "white");
          //  $("#main_page .contact_div h3").css("color", "white");
           // $("#main_page .contact_div ul li").css("color", "white");
          //  $("#main_page .contact_div ul li h4").css("color", "white");
          // $("#main_page .contact_div ul li p").css("color", "white");
            $("#middle_part a.btn").css("color", "white");
            $("#important_notice .links .controls a").css("color", "white");
            $("header .sub_header").css("background-color", "#813772");
            $("#header_section p ").css("color", "#ffffff");
            $("#header_section p a").css("color", "#ffffff");
         //  $("footer .main_footer").css("background-color", "rgb(9 9 9)");
            $(".content_heading").removeClass("blue");
            $(".content_heading").removeClass("red");
            $(".content_heading").addClass("orange");
            $(".news_section h5").css("color", "#ffffff");
         //   $(".news_section .whatNewSection p").css("color", "#ffffff");
            // new addition start
            $(".aim_vision .content_div").css(" border-color","#813772");
            $(".aim_vision .content_div p").css("color","black");
            //end

            $("#bottom_content .live_data h6").css("color", "#ffffff");
            $(".social_visitor .social_links h4").css("color", "white");
            $(".social_visitor .numbers h5").css("color", "white");
            $(".sidebar .tab a.active").css("background-color", "#813772 ");
            $("table thead").css("background-color", "#813772");
            $("#main_page .contact_info").css("background-color", "#813772 ");
            $(".btn-submit").css().css("background-color", "#813772 ");

        });
    
    }

    function red() {
        sessionStorage.setItem("theam", 3);
        $('html *:not(script, style, noscript)').each(function () {

            $("body").css("background-color", "white");
            $(".title_bg ").css("background-color", "#84ab11");
            $(".title_bg ").css("color", "white");
            $("section#welcome_text").css("background-color", "#84ab11");
            $("table thead").css("background-color", "#84ab11");
            $("#bottom_content").css("background-color", "#84ab11");
            $("#middle_part .news_section .content_heading").css("background-color", "#84ab11");
            $(".row.table_data").css("border-color", "#84ab11");
            $("#middle_part .news_section").css("background-color", "#84ab11");
            $("footer .social_visitor").css("background-color", "#84ab11");
            $(".innerBox h4").css("color", "#ffffff !important");
            $("#middle_part .news_section").css("background-image", "none");

            $("#important_notice .links").css("background-color", "white");
            $(".table_data .col-md-6").css("background-color", "white");
            $("#bottom_content .item").css("background-color", "white");
            $(".navbar .dropdown-menu").css("background-color", "white");
            $(".table_data .col-md-6").css("color", "#212529");
            $("ul li").css("color", "#212529");
            $("table td").css("color", "#212529");
            //
            $("header .navbar-light .navbar-nav .nav-link").css("color", "#000066");
            $("header .navbar-light .navbar-nav .nav-link.active").css("color", "black");
            $("header .sub_header").css("color", "white");
           // $("footer .main_footer h4").css("color", "white");
            //$("footer .main_footer h2").css("color", "white");
          // $("footer .main_footer h5").css("color", "white");
           // $("footer .main_footer p").css("color", "white");
            $(".social_links .links a").css("color", "white");
            $("#middle_part .news ul li .news_div .other p").css("color", "var(--textclr)");
           //$("footer .main_footer li a").css("color", "white");
           //$("footer  h4").css("color", "White");
            $("footer .social_visitor h5").css("color", "var(--textclr)");
            $("#welcome_text h5").css("color", "white");
            $("#welcome_text h1").css("color", "white");
            $("#welcome_text p").css("color", "white");
            $("#welcome_text p a").css("color", "white");
            $("header .sub_header ul li a").css("color", "white");
            $(".sidebar .tab a.active").css("color", "white");
            $(".sidebar .tab a.active").css("background-color", "#84ab11");
            $("header .sub_header ul li").css("color", "white");
            $("#main_page .contact_div h3").css("color", "white");
            $("#main_page .contact_div ul li").css("color", "white");
            $("#main_page .contact_div ul li h4").css("color", "white");
            $("#main_page .contact_div ul li p").css("color", "white");
            $("#middle_part a.btn").css("color", "white");
            $("#important_notice .links .controls a").css("color", "white");
            $("header .sub_header").css("background-color", "#1e1e1e");
           // $("footer .main_footer").css("background-color", "#000000");
            $(".content_heading").removeClass("orange");
            $(".content_heading").removeClass("blue");
            $(".content_heading").addClass("red");

            $(".news_section .content_heading").css("color", "white");
            $(".news_section .content_heading").css("background", "#84ab11");
            $(".news_section .news_div .other p").css("color", "white");
            $(".social_visitor .numbers h5").css("color", "white");
            $("#bottom_content .live_data h6").css("color", "white");
            $(".social_visitor .social_links h4").css("color", "white");
            $(".sidebar .tab a.active").css("background-color", "#84ab11");
            $(".sidebar .tab a.active").hover("background-color", "#84ab11");
            $("table thead").css("background-color", "#84ab11");
            $("#main_page .contact_info").css("background-color", "#84ab11");
            $(".btn-submit").css().css("background-color", "#84ab11");
            $(".news_section h5").css("color", "#ffffff");
            $(".news_section .whatNewSection p").css("color", "#ffffff");
            // new addition start
            $(".aim_vision .content_div").css(" border-color", "#813772");
            $(".aim_vision .content_div p").css("color", "black");

            $(".tablinks").hover(function () {
                $(this).css("background-color", "#ff0e7d !important");
            })
        });
   
    }

    $('#bright_contrast').click(function () {
        contrast();
    });

    $('#blue_theme').click(function () {
        normal();
    });

    $('#orange_theme').click(function () {
        orange();
    });

    $('#red_theme').click(function () {
 
        red();
    });