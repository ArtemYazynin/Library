(function(angular) {
	"use strict";

	angular.module("RentsModule")
		.controller("RentsController", ["$q", "rentsService", "subscribersService", "booksService", "$ngConfirm", "Notification", function ($q, rentsService, subscribersService, booksService, $ngConfirm, notification) {
			var self = this;
			self.dateOptions = { changeYear: true, changeMonth: true, dateFormat: 'dd.mm.yy' };
			self.gridOptions = {
				appScopeProvider: self,
				rowHeight: "40",
				enableColumnResizing: true,
				columnDefs: [
					{
						name: "Date",
						cellClass: function () { return "dataCell"; },
						cellFilter: 'date:"dd.MM.yyyy"', 
						filterCellFiltered: 'true',
						type: 'date',
						width: "10%"
					},
					{
						name: "Book",
						field: "Book.Name",
						width: "39%",
						cellClass: function () { return "dataCell"; },
						cellTooltip: function(row,col) {
							return row.entity.Book.Name;
						}
					},
					{
						 name: "Subscriber", 
						 field: "Subscriber.Fio",
						 width: "25%", 
						 cellClass: function () { return "dataCell"; },
						 cellTooltip: function (row, col) {
						 	return row.entity.Subscriber.Fio;
						 }
					},
					{ name: "Count", field: "Count", width: "7%" ,cellClass: function () { return "dataCell"; }},
					{ name: "Active", field: "IsActive", width: "7%" ,cellClass: function () { return "dataCell"; }},
					{
						name: " ",
						width: "12%",
						enableSorting: false,
						cellClass: function () { return "operationCell"; },
						cellTemplate:
						'<button type="button" class="btn btn-primary" ng-click="grid.appScope.actions.activateOrDeactivate(row.entity)">' +
							'<span>{{row.entity.IsActive ? "Deactivate": "Activate"}}</span>' +
						'</button>'
					}
				]
			};
			
			rentsService.get(function(response) {
				self.gridOptions.data = response;
			});

			self.actions = (function () {
				function _save() {
					function clear() {
						delete self.selectedRent;
						delete self.createDialog;
					}

					function closeDialog() {
						self.createDialog.close();
					}
					if (self.selectedRent.Id) {
						rentsService.update(self.selectedRent, function (response) {
							var index = self.gridOptions.data.indexOf(response);
							self.gridOptions.data[index] = response;

							notification.success('Successfully updated');
						});
					} else {
						rentsService.create(self.selectedRent, function (response) {
							self.gridOptions.data.push(response);
							closeDialog();
							clear();
							notification.success('Successfully created');
						});
					}
				
				}

				function _details(rent) {
					self.selectedRent = rent || {};

					$q.all([
						subscribersService.get(),
						booksService.getAllPromise()
					]).then(function (results) {
						self.Subscribers = results[0];
						self.Books = results[1];

						self.createDialog = $ngConfirm({
							title: "Rent",
							contentUrl: 'src/rentDetails.html',
							backgroundDismiss: true,
							theme: 'light',
							closeIcon: true,
							columnClass: 'col-lg-12',
							onScopeReady: function (scope) {
								scope.actions = self.actions;
								scope.selectedRent = self.selectedRent;
								scope.Books = self.Books;
								scope.Subscribers = self.Subscribers;
								scope.dateOptions = self.dateOptions;
							}
						});
					});

					
				}

				function _activateOrDeactivate(rent) {
					rent.IsActive = !rent.IsActive;
					if (rent.IsActive) {
						rentsService.deactivate(rent,
							function(response) {
								var index = self.gridOptions.data.indexOf(response);
								self.gridOptions.data[index] = response;
								notification.success('Deactivated');
							});
					} else {
						rentsService.activate(rent,
							function (response) {
								var index = self.gridOptions.data.indexOf(response);
								self.gridOptions.data[index] = response;
								notification.success('Activated');
							});
					}
				}
				return {
					save: _save,
					details: _details,
					activateOrDeactivate: _activateOrDeactivate
				}
			})();
	}]);
})(angular);