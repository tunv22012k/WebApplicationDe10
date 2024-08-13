$(function () {
    // Tùy chỉnh thông báo lỗi mặc định
    $.extend($.validator.messages, {
        required: "Không thể bỏ trống trường này.",
        email: "Vui lòng nhập địa chỉ email hợp lệ.",
        minlength: $.validator.format("Vui lòng nhập ít nhất {0} ký tự."),
        equalTo: "Vui lòng nhập lại giá trị tương tự.",
        number: "Vui lòng nhập số hợp lệ.",
        maxlength: $.validator.format("Vui lòng nhập không quá {0} ký tự."),
        digits: "Vui lòng chỉ nhập chữ số.",
        range: $.validator.format("Vui lòng nhập giá trị từ {0} đến {1}.")
    });
});