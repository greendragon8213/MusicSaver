var formArchiveUrl = window.location.origin + "/Song/FormMusicArchive";
var downloadArchiveUrl = window.location.origin + "/Song/DownloadMusicArchive";

function downloadSongs() {
    $('#songs-download-box').block({
        message: '<i class="fa fa-spinner fa-spin fa-5x fa-fw" style="color: white"></i><div style="color: white; font-size:16px"><br>Preparing files... <br>This process may take a few minutes. <br>Please wait.</div>',
        css: { 'background-color': 'transparent', 'border': 'none' }
    });

    $.ajax({
        url: formArchiveUrl,
        data: { songsList: GetSongsList() },
        dataType: "json",
        type: "POST",
        success: function (result) {

            window.location = downloadArchiveUrl + "?fileName=" + result.fileName;

            $("#songs-download-box").css('display', 'none');
            $("#songs-download-error-result").css('display', 'none');
            $("#songs-download-ok-result").css('display', 'block');

            $("#download-should-start-message").html(
                '<div class="alert alert-success" role="alert">' +
                '<i class="fa fa-check" aria-hidden="true"></i> ' +
                'Download should start automatically. <br>If download has not started please click '+
                '<a href="' + downloadArchiveUrl + "?fileName=" + result.fileName + '">here</a>' +
                '</div>');
        },
        error: function (result) {
            $("#songs-download-box").css('display', 'none');
            $("#songs-download-ok-result").css('display', 'none');
            $("#songs-download-error-result").css('display', 'block');

            //if (result.status == 500)...

            $("#error-message").html(
                '<div class="alert alert-danger" role="alert">' +
                '<i class="fa fa-times" aria-hidden="true"></i> ' +
                '<span class="sr-only">Error:</span>' +
                JSON.parse(result.responseText).errorMessage +
                '</div>'
            );
        },
        complete: function (result) {
            $('#songs-download-box').unblock();
        }
    });
}

function GetSongsList() {
    $($('input[name=songsList]')).remove();

    var songsString = $("#songs-names-list").val();
    var songsArray = songsString.split("\n");

    songsArray = removeEmptyElements(songsArray);
    songsArray = removeTimeSpanElements(songsArray);

    return songsArray;
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