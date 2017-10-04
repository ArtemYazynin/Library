(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule"])
	.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
		$locationProvider
		  .html5Mode(false).hashPrefix('');

		$routeProvider.when("/", {
			templateUrl: "/LibraryView/Books",
			controller: "BooksController"
		});
		$routeProvider.when("/Books", {
			templateUrl: "/LibraryView/Books",
			controller: "BooksController"
		});
		$routeProvider.when("/Books/Create", {
			templateUrl: "/LibraryView/BookDetails",
			controller: "BookCreateController"
		});
		$routeProvider.when("/Books/:bookId", {
			templateUrl: "/LibraryView/BookDetails",
			controller: "BookEditController"
		});
		//$routeProvider.otherwise({
		//	redirectTo: function () {
		//		window.location = "/";
		//	}
		//});
	}]);

})(angular);