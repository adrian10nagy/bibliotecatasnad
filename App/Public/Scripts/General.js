
$(document).ready(function () {
    $("#multiFieldAuthors").select2({
        placeholder: "--- Orice autor ---",
        allowClear: true
    });

    $("#multiFieldPublishers").select2({
        placeholder: "--- Orice Editură ---",
        allowClear: true
    });

    $("#multiFieldDomains").select2({
        placeholder: "--- Orice domeniu ---",
        allowClear: true
    });


});

$('#simpleSearchText').keyup(function (e) {
    var simpleSearchText = $('#simpleSearchText');
    getSimpleSearchResults(simpleSearchText);
});

function getSimpleSearchResults(simpleSearchText) {
    var simpleSearchTextValue = simpleSearchText.val();

    var simpleSearchSuggestions = $('#simpleSearchSuggestions');

    $.ajax({
        url: "/Common/AsyncGetSimpleSearchSuggestions",
        type: "GET",
        data: { searchTerm: (simpleSearchTextValue) }
    })
         .done(function (partialViewResult) {
             simpleSearchSuggestions.html(partialViewResult);
         });
}

$(function () {
    $("[rel='tooltip']").tooltip({
        placement: "bottom"
    });
});

function notify(message, notifyType) {
    $.notify(
        message,
        {
            className: notifyType,
            clickToHide: true,
            autoHide: true,
            autoHideDelay: 11000,
            arrowShow: true,
            showAnimation: 'slideDown',
            showDuration: 2000,
            hideAnimation: 'slideUp',
            hideDuration: 2000,
            globalPosition: 'right',
            gap: 4

        });
}