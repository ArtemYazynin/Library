(function(angular) {
	"use strict";

	angular.module("AuthorsModule")
		.controller("AuthorsController", ["$location", "authorsService", "$ngConfirm", "Notification", "paginationService",
			function ($location, authorsService, $ngConfirm, notification,paginationService) {
			var self = this;

			self.actions = (function () {
				function _init(paginationOptions) {
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
					authorsService.getAll(pagingModel, function (response, getHeaderFn) {
						self.gridOptions.data = response;
						self.gridOptions.totalItems = parseInt(getHeaderFn("totalItems"));
					});
				}
				function _details(author) {
					$location.path("/authors/" + author.Id + "/edit");
				}
				function _remove(author) {
					function removeInternal(author) {
						var fio = author.Fio;
						authorsService.remove(author, function (deletedAuthor) {
							var index = self.gridOptions.data.indexOf(deletedAuthor);
							self.gridOptions.data.splice(index, 1);
							notification.success("Author <strong>" + fio + "</strong> was removed");
						});
					}
					function removeConfirm(author) {
						$ngConfirm({
							title: 'Confirm!',
							content: 'Are you sure you want to delete this author?',
							scope: self,
							buttons: {
								ok: {
									text: 'Ok',
									btnClass: 'btn-blue',
									action: function () {
										removeInternal(author);
									}
								},
								close: {
									text: "Cancel"
								}
							},
							backgroundDismiss: true
						});
					}
					authorsService.getRelatedBooks(author.Id).then(function(data) {
						if (data.length === 0) {
							removeConfirm(author);
						} else {
							var booksStr = data.join(", ");
							$ngConfirm({
								title: 'Confirm!',
								content: 'Author <b style="color:green;">' + author.Fio + '</b> has next related books:<b style="color:red;">' + booksStr + '</b>. <br />Continue?',
								scope: self,
								buttons: {
									ok: {
										text: 'Ok',
										btnClass: 'btn-blue',
										action: function () {
											removeInternal(author);
										}
									},
									close: {
										text: "Cancel"
									}
								},
								backgroundDismiss: true
							});
						}
					});

				}
				function _sort() {
				
				}
				return {
					init: _init,
					details: _details,
					remove: _remove,
					sort: _sort
				}
			})();

			self.gridOptions = (function() {
				var options = {
					columnDefs: [
						{ name: "Lastname" },
						{ name: "Firstname" },
						{ name: "Middlename" },
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
					],
					appScopeProvider: self
				}
				var result = paginationService.getGridOptions(options, self.actions.init);
				return result;
			})();
			self.actions.init();
	}]);
})(angular);