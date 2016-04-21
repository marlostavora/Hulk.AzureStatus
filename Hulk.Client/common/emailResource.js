(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("emailResource", ["$resource", "appSettings", "currentUser", emailResource])

    function emailResource($resource, appSettings, currentUser) {
        return {
            email: $resource(appSettings.serverPath + "/api/home/SendEmail", null,
            {
                'send': {
                    method: 'POST',
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },
                
            })
        } 
    }
})();