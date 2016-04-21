(function () {
    "use strict";

    angular
        .module("azureStatus")
        .controller("EmailCtrl", ["emailResource", EmailCtrl]);

    function EmailCtrl(emailResource) {
        var vm = this;

        vm.message = "";
        vm.alertClass = "";

        vm.emailData = {
            name: "",
            email: "",
            message: ""
        };

        vm.send = function () {
            clearAlert();
            emailResource.email.send(vm.emailData, function(data) {
                vm.message = "Email successfully send!";
                vm.alertClass = "alert alert-success";
            },
            function (response) {
                vm.message = response.statusText + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    vm.message += response.data.exceptionMessage;
                vm.alertClass = "alert alert-danger";
            });
        };

        vm.cancel = function () {
            clearAlert();
            $modalInstance.dismiss('cancel');
        };

        function clearAlert() {
            vm.message = "";
            vm.alertClass = "";
        };
    };
})();