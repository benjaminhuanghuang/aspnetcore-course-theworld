// tripsController.js
(function () {
    "use strict";

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                // success
                angular.copy(response.data, vm.trips);
                console.log(vm.trips);
            }, function () {
                // Failure
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Create a new trip    
        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    // success
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                    // Test
                    // vm.trips.push({name:vm.newTrip.name, created:new Date()});
                }, function () {
                    vm.errorMessage = "Failed to save new trip. " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });


        }   
    }
})();