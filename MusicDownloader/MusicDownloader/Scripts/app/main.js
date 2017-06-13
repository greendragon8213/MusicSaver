function downloadSongs() {
    $("#error").addClass("hide");

    //var downloadFileUrl = window.location.origin + "/Song/DownloadSongs?songsList=" + $("#songs-names-list").value;
    //window.location = downloadFileUrl;

    var string = $("#songs-names-list").val();
    var array = string.split("\n");
    array = removeEmptyElements(array);
    array = removeTimeSpanElements(array);
    
    var downloadFileUrl = window.location.origin + "/Song/DownloadSongs";

    downloadFileUrl += "?songsList=" + array[0];
    for (var i = 1; i < array.length; i++) {
        downloadFileUrl += "&songsList=" + array[i];
    }

    window.location = downloadFileUrl;
}

function removeEmptyElements(array) {
    array = array.filter(function (entry) { return entry.trim() != ''; });
    return array;
}

function removeTimeSpanElements(array) {
    var timaSpanPattern = new RegExp("^\\d+:\\d+$");
    array = array.filter(function (entry) { return !timaSpanPattern.test(entry); });
    return array;
}