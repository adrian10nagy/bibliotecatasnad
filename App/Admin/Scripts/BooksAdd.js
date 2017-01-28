
function AsyncAddAuthorSuggestions(searchTerm) {
    var divBookAuthorsSuggestion = $('#divBookAuthorsSuggestion');

    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncUpdateBookAuthorsSearch",
        data: '{name: "' + searchTerm + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            divBookAuthorsSuggestion.html(partialViewResult.d);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function AsyncAddPublisherSuggestions(searchTerm)
{
    var divBookPublishersSuggestion = $('#divBookPublishersSuggestion');

    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncUpdatePublishersSearch",
        data: '{name: "' + searchTerm + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            divBookPublishersSuggestion.html(partialViewResult.d);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function AsyncAddDomainSuggestions(searchTerm) {
    var divBookDomainSuggestion = $('#divBookDomainSuggestion');

    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncUpdateDomainSearch",
        data: '{name: "' + searchTerm + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            divBookDomainSuggestion.html(partialViewResult.d);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function bookAuthorsAddNewAuthor(newAuthor)
{
    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncAddAuthor",
        data: '{name: "' + newAuthor + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            BookAuthorAddtoSuggestions(partialViewResult.d.Id, partialViewResult.d.Name);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function bookAddNewPublisher(newPublisher) {
    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncAddPublisher",
        data: '{name: "' + newPublisher + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            BookPublisherAddToSuggestions(partialViewResult.d.Id, partialViewResult.d.Name);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function bookAddNewBookDomain(newDomain) {
    $.ajax({
        type: "POST",
        url: "Add.aspx/AsyncAddDomain",
        data: '{name: "' + newDomain + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (partialViewResult) {
            BookDomainAddToSuggestions(partialViewResult.d.Id, partialViewResult.d.Name);
        },
        failure: function (response) {
            alert("Eroare de server, te rugăm contactează administratorul de sistem");
        }
    })
}

function removeDiv(thisDiv) {
    thisDiv.remove();
}


