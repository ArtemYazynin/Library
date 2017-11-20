(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule", "AuthorsModule", "GenresModule", "PublishersModule", "oi.select", "cp.ngConfirm", "ui.tree"])
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
					var removedProperty = "$id";
					if ((typeof response.data) === "string") return response;
					function internalRemove(data) {
						if (!data) return;
						if ((typeof data) === "object") {
							if (data instanceof Array) {
								removeReferenceIndicator(data);
							} else {
								delete data[removedProperty];
							}
							for (var innerProp in data) {
								if (data.hasOwnProperty(innerProp)) {
									internalRemove(data[innerProp]);
								}
							}
						}
					}
					function removeReferenceIndicator(data) {
						/*
						[DataContract(IsReference = true)]
						EntityDto
						*/
						for (var prop in data) {
							if (data.hasOwnProperty(prop)) {
								if (data[prop] === null || data[prop] === undefined) continue;
								if (prop === removedProperty) {
									delete data[prop];
									continue;
								}
								internalRemove(data[prop]);
							}
							
						}
					}
					removeReferenceIndicator(response.data);
					return response;
				}
			}
		});
		$locationProvider
		  .html5Mode(false).hashPrefix('');

		$routeProvider.when("/books", {
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

		$routeProvider.when("/genres", {
			templateUrl: "/LibraryView/Genres",
			controller: "GenresController"
		});
		$routeProvider.when("/publishers", {
			templateUrl: "/LibraryView/Publishers",
			controller: "PublishersController"
		});

		$routeProvider.when("/authors", {
			templateUrl: "/LibraryView/Authors",
			controller: "AuthorsController"
		});
		$routeProvider.when("/authors/:authorId/edit", {
			templateUrl: "/LibraryView/AuthorDetails",
			controller: "AuthorDetailsController",
			controllerAs:"authorDetails"
		});
		$routeProvider.when("/authors/new", {
			templateUrl: "/LibraryView/AuthorDetails",
			controller: "AuthorDetailsController",
			controllerAs: "authorDetails"
		});
		$routeProvider.otherwise({
			redirectTo: function () {
				window.location = "#/books";
			}
		});
	}]);
})(angular);