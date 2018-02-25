(function (angular) {
	"use strict";
	angular.module("RootModule", ["ngRoute", "BooksModule", "AuthorsModule", "GenresModule", "PublishersModule", "InvoicesModule", "SubscribersModule", "RentsModule", "oi.select", "cp.ngConfirm", "ui.tree"])
	.config(["$routeProvider", "$locationProvider", "$httpProvider", "$ngConfirmProvider", function ($routeProvider, $locationProvider, $httpProvider, $ngConfirmProvider) {
		$httpProvider.interceptors.push("$q", function ($q) {
			return {
				responseError: function (rejection) {
					$ngConfirmProvider.$get()({
						//title: rejection.statusText, server msg
						title: "Attention!",
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

		$routeProvider.when("/subscribers",
		{
			templateUrl: "LibraryView/Subscribers",
			controller: "SubscribersController",
			controllerAs: "ctrl"
		});

		$routeProvider.when("/rents",
		{
			templateUrl: "LibraryView/Rents",
			controller: "RentsController"
		});

		$routeProvider.when("/invoices",
		{
			templateUrl: "LibraryView/Invoices",
			controller: "InvoicesController",
			controllerAs: "ctrl"
		});

		$routeProvider.when("/invoices/new",
		{
			templateUrl: "/LibraryView/InvoiceDetails",
			controller: "InvoicesDetailsController",
			controllerAs: "ctrl"
		});

		$routeProvider.when("/authors", {
			templateUrl: "/LibraryView/Authors",
			controller: "AuthorsController",
			controllerAs: "ctrl"
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
	}])
	.directive("gridRowOperations", [function() {
		return {
			restrict: 'E',
			templateUrl:"src/gridRowOperations.html",
			scope: {
				details: "&",
				remove: "&"
			},
			link: function(scope, elements, attrs) {
				scope.showDetails = !!attrs.details;
				scope.showRemove = !!attrs.remove;
			}
		};
	}]);
})(angular);