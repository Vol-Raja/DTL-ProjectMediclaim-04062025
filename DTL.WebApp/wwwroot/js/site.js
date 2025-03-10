// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("input[type='date']").on("change", function () {
    if (this.value != "") {
        this.setAttribute(
            "data-date",
            moment(this.value, "YYYY-MM-DD")
                .format(this.getAttribute("data-date-format")));
    } else{
        this.setAttribute("data-date",this.value);
    }
}).trigger("change")


toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function toastrSuccess(msg) {
    toastr.success(msg)
}

function toastrInfo(msg) {
    toastr.info(msg)
}

function toastrError(msg) {
    toastr.error(msg)
}

function toastrWarning(msg) {
    toastr.warning(msg)
}


function DeleteTempFile(path) {
    $.ajax({
        url: "/EmployeeRegistration/DeleteFile",
        type: "POST",
        dataType: "json",
        data: { path: path },
        success: function (data) {

        },
        error: function (err) {

        }

    })
}

//function startTimer(duration, display) {
//    var start = Date.now(),
//        diff,
//        minutes,
//        seconds;
//    function timer() {
//        // get the number of seconds that have elapsed since 
//        // startTimer() was called
//        diff = duration - (((Date.now() - start) / 1000) | 0);

//        // does the same job as parseInt truncates the float
//        minutes = (diff / 60) | 0;
//        seconds = (diff % 60) | 0;

//        minutes = minutes < 10 ? "0" + minutes : minutes;
//        seconds = seconds < 10 ? "0" + seconds : seconds;

//        display.textContent = minutes + ":" + seconds;        
//        if (diff <= 0) {
//            // add one second so that the count down starts at the full duration
//            // example 05:00 not 04:59
//            start = Date.now() + 1000;
//        }
//        if (diff == 0) {
//            window.location.reload();
//        }
//    };
//    // we don't want to wait a full second before the timer starts
//    timer();
//    setInterval(timer, 1000);
//}

//window.onload = function () {
//    var sessionMinutes = 60 * 30,
//        display = document.querySelector('#sessionTime');
//    startTimer(sessionMinutes, display);
//};