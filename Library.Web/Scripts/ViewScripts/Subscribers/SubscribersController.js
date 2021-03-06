﻿(function(angular) {
	"use strict";

	angular.module("SubscribersModule")
		.controller("SubscribersController", ["subscribersService", "$ngConfirm", "Notification", "paginationService", function (subscribersService, $ngConfirm, notification, paginationService) {
			var self = this;

			self.actions = (function () {
				function _remove(subscriber) {
					subscribersService.remove(subscriber, function (response) {
						var index = self.gridOptions.data.indexOf(response);
						if (response.IsDeleted) {
							self.gridOptions.data[index] = response;
						
						} else {
							self.gridOptions.data.splice(index, 1);
						}
						notification.success(response.IsDeleted ? "Subscriber mark as removed, because he has Rents" : "Subscriber was removed");
					});
				}
				function _save() {
					function clear() {
						delete self.selectedSubscriber;
						delete self.createDialog;
					}

					function closeDialog() {
						self.createDialog.close();
					}
					if (self.selectedSubscriber.Id) {
						subscribersService.update(self.selectedSubscriber, function (response) {
							var index = self.gridOptions.data.indexOf(response);
							self.gridOptions.data[index] = response;
							closeDialog();
							notification.success("Subscriber was updated");
						});
					} else {
						subscribersService.create(self.selectedSubscriber, function (response) {
							self.gridOptions.data.push(response);
							closeDialog();
							clear();
							notification.success("Subscriber was created");
						});
					}
				
				}
				function _details(subscriber) {
					self.selectedSubscriber = subscriber || {};
					self.createDialog = $ngConfirm({
						title: self.selectedSubscriber.Fio || "Create new subscriber",
						contentUrl: 'src/subscriberDetails.html',
						backgroundDismiss: true,
						closeIcon: true,
						theme: 'light',
						onScopeReady: function (scope) {
							scope.actions = self.actions;
							scope.selectedSubscriber = self.selectedSubscriber;
						}
					});
				}
				function _init(paginationOptions) {
					var pagingModel = paginationService.getPagingModel(paginationOptions);
					subscribersService.get(pagingModel, function (response,getHeaderFn) {
						self.gridOptions.data = response;
						self.gridOptions.totalItems = parseInt(getHeaderFn("totalItems"));
					});
				}
				return {
					remove: _remove,
					save: _save,
					details: _details,
					init: _init
				}
			})();
			self.gridOptions = (function () {
				var options = {
					columnDefs: [
						{ name: "Lastname", cellClass: function () { return "dataCell"; } },
						{ name: "Firstname", cellClass: function () { return "dataCell"; } },
						{ name: "Middlename", cellClass: function () { return "dataCell"; } },
						{ name: "Deleted", field: "IsDeleted" },
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