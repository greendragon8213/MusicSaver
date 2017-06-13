function downloadAll() {
    $("#error").addClass("hide");

    var downloadFileUrl = window.location.origin + "/Home/DownloadAll";//"?signedRequest=" + $("#signed_request").val();
    window.location = downloadFileUrl;
    //$.get("/Home/DownloadAll");
    //{
    //    tg: $("#Tg").val(),
    //    tu: $("#Tu").val(),
    //    t: $("#T").val(),
    //    thetaCoefficient: $("#thetaCoefficient").val(),
    //    n: $("#N").val(),
    //    type: type
    //},
    //function (data) {
    //    console.log(data);
    //console.log(data.IsValid);
    //if (!data.IsValid) {
    //    $("#error").toggle();
    //    $("#error")[0].textContent = "You entered wrong data. All values must be doubles";
    //    $("#error").removeClass("hide");
    //    return;
    //}
//}
    //);
}