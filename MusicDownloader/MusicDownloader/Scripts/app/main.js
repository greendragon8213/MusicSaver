var main = (function () {

    function initialize() {
        
        //be default the most preferred language from browser (or if not defined by OS)
        var browserLanguage = window.navigator.languages[0] || window.navigator.language;
        
        //enabling jquery localization
        var opts = { language: browserLanguage, pathPrefix: "../../Scripts/app/localization" };
        $("[data-localize]").localize("application", opts);
    }

    return {
        initialize: function () {
            return initialize();
        }
    }
}());