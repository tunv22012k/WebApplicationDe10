(function($) {
      "use strict"; // Start of use strict

    // dropdow
    $(document).on('click', '#navbarDropdown', function (e) {
        e.stopPropagation();
        if ($('.dropdown_menu_user').css('display') == 'none') {
            $('.dropdown_menu_user').show();
        } else {
            $('.dropdown_menu_user').hide();
        }
    });

    // hide
    $(document).on('click', 'body', function (e) {
        $('.dropdown_menu_user').hide();
    });

    // click add to cart
    $(document).on('click', '.add_to_cart', function (e) {
        // get data cart
        let data = $(this).data();

        // get total cart product
        let dataStorageCart = sessionStorage.getItem("dataStorageCart");

        if (!dataStorageCart) {
            sessionStorage.setItem("dataStorageCart", JSON.stringify([data]));
        } else {
            let parseDataStorageCart = JSON.parse(dataStorageCart);

            //  filter
            const filterData = parseDataStorageCart.filter(v => v.masanpham == data.masanpham);

            if (filterData.length == 0) {
                // push data
                parseDataStorageCart.push(data);

                // set data to cart
                sessionStorage.setItem("dataStorageCart", JSON.stringify(parseDataStorageCart));
            };
        }

        // set data cart all
        getDataStorageCart();
    });

    getDataStorageCart();
    function getDataStorageCart() {
        // get total cart product
        let dataStorageCart = sessionStorage.getItem("dataStorageCart");

        if (dataStorageCart) {
            let parseDataStorageCart = JSON.parse(dataStorageCart);

            $(".total_cart_product").text(parseDataStorageCart.length);
        } else {
            $(".total_cart_product").text("0");
        }
    }

    $(document).on('click', '.btn_show_page_cart', function (e) {
        // Navigate to the constructed URL
        location.href = "/GioHang/Index"
    });
    
})(jQuery); // End of use strict
