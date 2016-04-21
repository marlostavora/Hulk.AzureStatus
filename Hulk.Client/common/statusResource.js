(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("statusResource",
            ["$resource", "appSettings", "currentUser", statusResource])

    function statusResource($resource, appSettings, currentUser) {
        return $resource(appSettings.serverPath + "/api/home/GetServiceStatus", null,
            {
                'get': {
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },
            });
    }
})();