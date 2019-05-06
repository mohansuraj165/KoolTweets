var isInitialized
const MaxRecordSize=100
$(function () {
    if (isInitialized != true) {
        init()
    }
});

/**
 * Initialize the script on load
 */
function init() {
    $("#startDate").jqxDateTimeInput({ width: '270px', height: '25px' });
    $("#endDate").jqxDateTimeInput({ width: '270px', height: '25px' });
    isInitialized=true
    
}

/*
 *Called from Index.cshtml
 * Gets tweets data displays it.
 */ 
function getTweets() {
    ResetDisplay()
    var startDate = $("#startDate").val()
    var endDate = $("#endDate").val()
    AddHeaderToTable()
    GetTweetsAPI(startDate, endDate)
}

/**
 * 
 * @param  start - start date
 * @param  end - end date
 * function makes call to controller
 * recursively calls controller to fetch data
 * Displays appropriate message on failure or error.
 */
function GetTweetsAPI(startDate, endDate) {
    $.ajax({
        url: 'Home/GetAllTweets',
        async: 'false',
        type: 'GET',
        data: { "start": startDate, "end": endDate },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.success) {
                GetTweetsSuccessAction(data, endDate)  
            }
            else {
                DisplayError(data.error)
            }
        },
        error: function (xhr, status, error) {
            DisplayError(error);
        }
    });
}

/**
 * 
 * @param data - data returned from ajax call
 * @param endDate - time frame end date for tweets fetch
 * Performs the succes actions of 'GetAllTweets' call
 */
function GetTweetsSuccessAction(data, endDate) {
    if (data.data.length == 0)
        DisplayError("No records found for this time frame")
    else {
        PopulateTweetData(data.data)

        if (data.data.length == MaxRecordSize) {
            var newStart = data.data[MaxRecordSize - 1].stamp;
            GetTweetsAPI(newStart, endDate)
        }
    }
}

/**
 * @param data - list of tweets in JSON format
 * Creates a table list of tweets
 */
function PopulateTweetData(data) {
    $("#tweetsTable").removeClass("hidden");
    var table = $("#tweetsTable table")
    var tbody = $("<tbody></tbody>")
    data.forEach(function (d) {
        var tr = $("<tr></tr>")
        tr.append("<td>" + d.id + "</td>")
        tr.append("<td>" + d.stamp + "</td>")
        tr.append("<td>" + d.text + "</td>")
        tbody.append(tr)
    });
    table.append(tbody);
}

/**
 * 
 * @param msg - message/error string
 * Displays error message sent as string
 */
function DisplayError(msg) {
    ResetDisplay()
    var tableDiv = $("#userMessage")
    var div = ("<div><h2>"+msg+"</h2></div>")
    tableDiv.append(div)
}

/**
 * Clears the HTML in the div given by ID
 */
function ResetDisplay() {
    $("#tweetsTable").empty().addClass("hidden")
    $("#userMessage").empty()
}

/**
 * Adds headers to the table that displays tweets
 * Since the tweets are dynamically added to the table, the header should be added just once
 */
function AddHeaderToTable() {
    var tableDiv = $("#tweetsTable")
    var table = $("<table></table>").addClass("table table-bordered table-striped")
    var thead = $("<thead></thead>")
    thead.append("<td>ID</td><td>Time Stamp</td><td>Message</td>")
    table.append(thead)
    tableDiv.append(table);
}