$(function () {
    // Thiết lập mặc định cho jQuery Validation
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).addClass('error');
            $(element).closest('.tooltip-error').addClass('error');
        },
        unhighlight: function (element) {
            $(element).removeClass('error');
            $(element).closest('.tooltip-error').removeClass('error');
        },
        errorElement: 'span',
        errorClass: 'text-danger',
        errorPlacement: function (error, element) {
            // Hiển thị lỗi ngay sau phần tử input
            error.insertAfter(element);
        }
    });
});