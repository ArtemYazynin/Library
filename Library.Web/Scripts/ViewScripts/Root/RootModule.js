(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule", "oi.select", "cp.ngConfirm"])
	.config(["$routeProvider", "$locationProvider", "$httpProvider", "$ngConfirmProvider", function ($routeProvider, $locationProvider, $httpProvider, $ngConfirmProvider) {
		$httpProvider.interceptors.push("$q", function ($q) {
			return {
				responseError: function (rejection) {
					$ngConfirmProvider.$get()({
						title: rejection.statusText,
						content: rejection.data.ExceptionMessage,
						type: 'red',
						typeAnimated: true,
						buttons: {
							close: function () {
							}
						}
					});
					return $q.reject(rejection);
				},
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
		$routeProvider.when("/books/new", {
			templateUrl: "/LibraryView/BookDetails",
			controller: "BookCreateController"
		});
		$routeProvider.when("/books/:bookId/edit", {
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