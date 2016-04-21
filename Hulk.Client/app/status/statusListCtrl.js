(function () {
    "use strict";

    angular
          .module("azureStatus")
          .controller("StatusListCtrl",
                ["statusResource", StatusListCtrl]);

    function StatusListCtrl(statusResource) {
        var vm = this;

        statusResource.get(function (data) {
            vm.servicesStatus = data;
        },
        function (response) {
            vm.message = response.statusText + "\r\n";
            if (response.data && response.data.exceptionMessage)
                vm.message += response.data.exceptionMessage;
        });
        
        vm.getClass = function (status) {

            if (status == "Green")
                return "text-success glyphicon glyphicon-ok";
            if (status == "Yellow")
                return "text-warning glyphicon glyphicon-warning-sign";
            if (status == "Red")
                return "text-danger glyphicon glyphicon-exclamation-sign";
            if (status == "Blue")
                return "text-info glyphicon glyphicon-info-sign";
            if (status == "")
                return "";
        };
    };
})();