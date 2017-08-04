(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "bookService",
		function ($scope, bookService) {
			$scope.search = function () {
				bookService.search($scope.filters).then(function(data) {
					$scope.Books = data;
				});
			};
			(function init() {
				bookService.getAll(function (response) { $scope.Books = response; });
			})();
		}
	]);
})(angular);