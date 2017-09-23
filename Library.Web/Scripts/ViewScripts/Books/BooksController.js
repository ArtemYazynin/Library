(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "booksService",
		function ($scope, booksService) {
			$scope.actions = (function () {
				function _search() {
					booksService.search($scope.filters).then(function (data) {
						$scope.Books = data;
					});
				}
				return {
					search: _search
				}
			})();
			(function init() {
				booksService.getAll(function (response) { $scope.Books = response; });
			})();
		}
	]);
})(angular);