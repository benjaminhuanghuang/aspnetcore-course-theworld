// tripsController.js
(function () {
    "use strict";

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.trips = [];
        vm.newTrip = {};

        vm.addTrip = function()
        {
            vm.trips.push({name:vm.newTrip.name, created:new Date()});
            vm.newTrip={};
        }
        $http.get("/api/trips")
        .then(function(response){
            // success
            angular.copy(response.data, vm.trips)
        }, function(){
            // Failure

        });
    }
})();