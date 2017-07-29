(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "bookService",
		function ($scope, bookService) {
			bookService.getAll(function(response) {
				$scope.Books = response;
			});
		}
	]);
})(angular);