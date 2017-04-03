

function openBookDetailsPopup(url, data) {

    $.ajax({
        url: url,
        data: { bookId: data },

        success: function (data) {
            if (data != '') {
                $.colorbox({ html: data, scrolling: false, opacity: 0.85 });
            }
            else {
                alert(url);
                window.location = url;
            }
        }

    });

    return false;
}

