(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BooksController", ["$scope", "$location", "booksService", "$ngConfirm", "Notification","paginationService",
		function ($scope, $location, booksService, $ngConfirm, notification, paginationService) {
			var paginationOptions = paginationService.getDefaultOptions();
			function init() {
				var skip = paginationOptions.pageNumber - 1;
				booksService.getAll(skip, paginationOptions.pageSize, function (response,getHeaderFn) {
					$scope.gridOptions.data = response;
					$scope.gridOptions.totalItems = parseInt(getHeaderFn("totalItems"));
				});
			}
			//var getPage = function () {
			//	var url;
			//	switch (paginationOptions.sort) {
			//		case "ASC":
			//			url = '/data/100_ASC.json';
			//			break;
			//		case "DESC":
			//			url = '/data/100_DESC.json';
			//			break;
			//		default:
			//			url = '/data/100.json';
			//			break;
			//	}
			//	init();
			//};
			$scope.gridOptions = {
				rowHeight: "40px",
				paginationPageSizes: [3, 5, 10],
				paginationPageSize: 3,
				useExternalPagination: true,
				//useExternalSorting: true,
				onRegisterApi: function (gridApi) {
					$scope.gridApi = gridApi;
					gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
						paginationOptions.pageNumber = newPage;
						paginationOptions.pageSize = pageSize;
						init();
					});
					//$scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
					//	if (sortColumns.length == 0) {
					//		paginationOptions.sort = null;
					//	} else {
					//		paginationOptions.sort = sortColumns[0].sort.direction;
					//	}
					//	init();
					//});
				},
				columnDefs: [
					{ name: "Name" },
					{ name: 'Genres', field: 'GenresStr' },
					{ name: 'Authors', field: 'AuthorsStr' },
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
			};

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