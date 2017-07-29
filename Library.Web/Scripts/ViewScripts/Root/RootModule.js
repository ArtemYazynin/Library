(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule"])
	.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
		$locationProvider.html5Mode({
			enabled: true,
			requireBase: false
		});
		$locationProvider.html5Mode(true);

		$routeProvider.when("/", {
			templateUrl: "/LibraryView/Books",
			controller: "BooksController"
		});
		$routeProvider.when("/Books", {
			templateUrl: "/LibraryView/Books",
			controller: "BooksController"
		});
	}]);

})(angular);