
$(document).ready(function () {

    var options = {
        legend: false,
        responsive: false
    };

    function CreateChart(chartData) {
        new Chart(document.getElementById("canvas1"), {
            type: 'doughnut',
            tooltipFillColor: "rgba(51, 51, 51, 0.55)",
            data: {
                labels:
                  chartData.Labels
                ,
                datasets: [{
                    data: chartData.Dataset.Data,
                    backgroundColor: chartData.Dataset.BackgroundColor,
                    hoverBackgroundColor: chartData.Dataset.HoverBackgroundColor
                }]
            },
            options: options
        });
    }
    bookPublisherData();
    function bookPublisherData() {
        $.ajax({
            type: "POST",
            url: "/Dashboard.aspx/AsyncGetBookDomains",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (partialViewResult) {
                return CreateChart(partialViewResult.d);
            },
            failure: function (response) {
                alert("Eroare de server, te rugăm contactează administratorul de sistem");
            }
        })
    }
});