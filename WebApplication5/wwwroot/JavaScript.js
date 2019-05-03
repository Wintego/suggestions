$(document).ready(function () {
    $("#name").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Suggestion/Create", //'@Url.Action("Create","Suggestion")',
                type: "POST",
                dataType: "json",
                data: { prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item, value: item }
                    }))
                }
            })
        },
        messages: { noResults: "", results: "" }
    });
})