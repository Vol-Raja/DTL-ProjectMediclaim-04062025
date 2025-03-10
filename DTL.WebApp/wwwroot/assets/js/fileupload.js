$(document).ready(function () {


    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        } else {
            $('.profile-pic').attr('src', '/assets/images/defultimage.jpg');
        }
    }


    $(".file-upload").on('change', function () {
        readURL(this);
        $(".delete-image-button")?.show();
        $(".upload-button")?.hide();
        $(".file-required")?.val(0);
       
    });

    $(".upload-button").on('click', function () {
        $(".file-upload").click();

    });
    $(".delete-image-button").on('click', function () {
        $(".file-upload").val('');
        $(".upload-button").show();
        $(".delete-image-button").hide();
        $(".file-required")?.val(1);
        readURL(this);
    });
});