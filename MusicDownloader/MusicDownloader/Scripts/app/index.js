var index = (function () {

    function initialize() {
    }
    
    function downloadSongs() {
        console.log($.localize.data.application.downloadArchive.loading.title);
        $('#songs-download-box').block({
            message: '<i class="fa fa-spinner fa-spin fa-5x fa-fw" style="color: white"></i><div style="color: white; font-size:16px">' +
                '<br>' + $.localize.data.application.downloadArchive.loading.title + '<br>' +
                $.localize.data.application.downloadArchive.loading.details,
            css: { 'background-color': 'transparent', 'border': 'none' }
        });

        $.ajax({
            url: urls.formArchiveUrl,
            data: { songsList: getSongsList() },
            dataType: "json",
            type: "POST",
            success: function (result) {

                var downloadCreatedArchiveUrl = urls.downloadArchiveUrl + "?fileName=" + result.fileName;
                window.location = downloadCreatedArchiveUrl;

                $("#songs-download-box").css('display', 'none');
                $("#songs-download-error-result").css('display', 'none');
                $("#songs-download-ok-result").css('display', 'block');

                $("#download-should-start-message").html(
                    '<div class="alert alert-success" role="alert">' +
                    '<i class="fa fa-check" aria-hidden="true"></i> ' +
                    $.localize.data.application.downloadArchive.response.downloadShouldStart +
                    '<a href="' + downloadCreatedArchiveUrl + '"> ' +
                    $.localize.data.application.downloadArchive.response.here +
                    '</a>' +
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
                    '<span class="sr-only">' +
                    $.localize.data.application.downloadArchive.response.error +
                    '</span>' +
                    JSON.parse(result.responseText).errorMessage +
                    '</div>'
                );
            },
            complete: function (result) {
                $('#songs-download-box').unblock();
            }
        });
    }

    function getSongsList() {
        $($('input[name=songsList]')).remove();

        //ToDo distinct

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

    return {
        initialize: function () {
            return initialize();
        },
        downloadSongs: function() {
            return downloadSongs();
        }
    }
}());

index.initialize();