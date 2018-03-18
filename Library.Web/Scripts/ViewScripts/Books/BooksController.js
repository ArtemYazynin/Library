(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "$location", "booksService", "$ngConfirm", "Notification","paginationService",
		function ($scope, $location, booksService, $ngConfirm, notification, paginationService) {
			function init(paginationOptions) {
				paginationOptions = paginationOptions || paginationService.getDefaultOptions();
				var pagingModel = {
					Skip: paginationOptions.pageNumber - 1,
					Take: paginationOptions.pageSize,
					Name: paginationOptions.name,
					OrderBy: (function () {
						switch (paginationOptions.sort) {
							case "asc":
								return window.Enums.orderBy.asc;
							case "desc":
								return window.Enums.orderBy.desc;
							default:
								return undefined;
						}
					})()
				};
				booksService.getAll(pagingModel, function (response, getHeaderFn) {
					$scope.gridOptions.data = response;
					$scope.gridOptions.totalItems = parseInt(getHeaderFn("totalItems"));
				});
			}

			$scope.gridOptions = (function() {
				var options = {
					columnDefs: [
						{ name: "Name" },
						{ name: 'Genres', field: 'GenresStr', enableSorting: false },
						{ name: 'Authors', field: 'AuthorsStr', enableSorting: false },
						{ name: 'Publisher', field: 'Publisher.Name' },
						{ name: 'Count' },
						{
							name: ' ',
							enableSorting: false,
							cellClass: function () { return "operationCell"; },
							cellTemplate:
							'<grid-row-operations ' +
								'details="grid.appScope.actions.details(row.entity)" ' +
								'remove="grid.appScope.actions.remove(row.entity)">' +
							'</grid-row-operations>'

						}
					]
				}
				var result = paginationService.getGridOptions(options, init);
				return result;
			})();

			$scope.actions = (function () {
				function _search() {
					booksService.search($scope.filters).then(function (data) {
						$scope.gridOptions.data = data;
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
									var bookname = book.Name;
									booksService.remove(book, function(deletedBook) {
										var index = scope.gridOptions.data.indexOf(deletedBook);
										scope.gridOptions.data.splice(index, 1);
										notification.success({ title: "Successfully removed", message: "Book <strong>" + bookname + "</strong> was removed" });
									});
									
								}
							},
							close: {
								text: "Cancel"
							}
						},
						backgroundDismiss: true
					});
				}
				function _details(book) {
					$location.path("/books/" + book.Id + "/edit");
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