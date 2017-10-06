(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule"])
	.config(["$routeProvider", "$locationProvider", "$httpProvider", function ($routeProvider, $locationProvider, $httpProvider) {
		$httpProvider.interceptors.push(function() {
			return {
				response: function (response) {
					if ((typeof response.data) === "string") return response;

					function removeReferenceindicator(data) {
						/*
						[DataContract(IsReference = true)]
						EntityDto
						*/
						var removedProperty = "$id";
						for (var prop in data) {
							if (data.hasOwnProperty(prop)) {
								if (data[prop] === null || data[prop] === undefined) continue;
								if (prop === removedProperty) {
									delete data[prop];
									continue;
								}
								if ((typeof data[prop]) === "object") {
									if (data[prop] instanceof Array) {
										removeReferenceindicator(data[prop]);
									} else {
										delete data[prop][removedProperty];
									}
								}
								
							}
							
						}
					}
					removeReferenceindicator(response.data);
					return response;
				}
			}
		});
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
			controller: "BookCreateController"
		});
		//$routeProvider.otherwise({
		//	redirectTo: function () {
		//		window.location = "/";
		//	}
		//});
	}]);

})(angular);