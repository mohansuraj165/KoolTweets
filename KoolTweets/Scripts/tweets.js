var isInitialized
$(function () {
    if (isInitialized != true) {
        init()
    }
});

//function init() {
    
//    $("#startDate").datepicker();
//    $("#endDate").datepicker();
//}
//init()

function init() {
    $("#startDate").jqxDateTimeInput({ width: '270px', height: '25px' });
    $("#endDate").jqxDateTimeInput({ width: '270px', height: '25px' });
    isInitialized=true
    
}
    
function getTweets() {
    ResetDisplay()
    var start = $("#startDate").val()
    var end = $("#endDate").val()
    $.ajax({
        url: 'Home/GetAllTweets',
        async: 'false',
        type: 'GET',
        data: { "start": start, "end": end },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.success) {
                if (data.data.length == 0)
                    DisplayError("No records found for this time frame")
                else
                    PopulateTweetData(data.data)
            }
            else {
                DisplayError(data.error)
            }
        },
        error: function (xhr, status, error) {

            alert(error);
        }

    });

}

function PopulateTweetData(data) {
    ResetDisplay()
    var tableDiv = $("#tweetsTable")
    var table = $("<table></table>").addClass("table table-bordered table-striped")
    var thead = $("<thead></thead>")
    thead.append("<td>ID</td><td>Time Stamp</td><td>Message</td>")
    table.append(thead)
    var tbody = $("<tbody></tbody>")

    data.forEach(function (d) {
        var tr = $("<tr></tr>")
        tr.append("<td>" + d.id + "</td>")
        tr.append("<td>" + d.stamp + "</td>")
        tr.append("<td>" + d.text + "</td>")
        tbody.append(tr)
    });
    table.append(tbody);
    tableDiv.append(table);
}

function DisplayError(msg) {
    ResetDisplay()
    var tableDiv = $("#tweetsTable")
    var div = ("<div><h2>"+msg+"</h2></div>")
    tableDiv.append(div)
}

function ResetDisplay() {
    $("#tweetsTable").empty()
    $("#userMessage").empty()
}
