$('form').submit(function (e) {
    e.preventDefault();

    var songsArray = GetSongsList();

    $(this).append($.map(songsArray, function (song) {
        return $('<input>', {
            type: 'hidden',
            name: 'songsList',
            value: song
        });
    }));

    this.submit();
});

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