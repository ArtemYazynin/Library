(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "$location", "booksService", "$ngConfirm",
		function ($scope, $location, booksService, $ngConfirm) {
			function init() {
				booksService.getAll(function (response) { $scope.Books = response; });
			}
			$scope.actions = (function () {
				function _search() {
					booksService.search($scope.filters).then(function (data) {
						$scope.Books = data;
					});
				}
				function _remove(book) {
					$ngConfirm({
						title: 'Confirm!',
						content: 'Are you sure you want to delete this book?',
						scope: $scope,
						buttons: {
							ok: {
								text: 'Ok',
								btnClass: 'btn-blue',
								action: function (scope, button) {
									booksService.remove(book, function(deletedBook) {
										var index = scope.Books.indexOf(book);
										scope.Books.splice(index, 1);
										this.close();
									});
									
								}
							},
							close: {
								text: "Cancel"
							}
						}
					});
				}
				function _details(book) {
					$location.path("/Books/" + book.Id);
				}
				return {
					search: _search,
					remove: _remove,
					details: _details
				}
			})();
			init();
		}
	]);
})(angular);