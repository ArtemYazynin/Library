﻿(function(angular) {
	"use strict";

	angular.module("SubscribersModule")
		.controller("SubscribersController", ["subscribersService", "$ngConfirm", function (subscribersService, $ngConfirm) {
			var self = this;

			self.gridOptions = (function () {
				function getDataClass() { return "dataCell"; }
				return{
					appScopeProvider: self,
					rowHeight: "40px",
					columnDefs: [
						{ name: "Lastname", cellClass: getDataClass },
						{ name: "Firstname", cellClass: getDataClass },
						{ name: "Middlename", cellClass: getDataClass },
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
					]
				};
			})();

			subscribersService.get(function(response) {
				self.gridOptions.data = response;
			});

			self.actions = (function () {
				function _remove(subscriber) {
					subscribersService.remove(subscriber, function (response) {
						var index = self.gridOptions.data.indexOf(response);
						if (response.IsDeleted) {
							self.gridOptions.data[index] = response;
						
						} else {
							self.gridOptions.data.splice(index, 1);
						}
						$ngConfirm({
							title: "Successfully removed!",
							content: response.IsDeleted ? "Subscriber mark as removed, because he has Rents" : "Subscriber was removed",
							backgroundDismiss: true

						});
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
							$ngConfirm({
								title: "Successfully update!",
								content: "Subscriber was updated",
								backgroundDismiss: true

							});
						});
					} else {
						subscribersService.create(self.selectedSubscriber, function (response) {
							self.gridOptions.data.push(response);
							closeDialog();
							clear();
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
						onScopeReady: function (scope) {
							scope.actions = self.actions;
							scope.selectedSubscriber = self.selectedSubscriber;
						}
					});
				}
				return {
					remove: _remove,
					save: _save,
					details: _details
				}
			})();
	}]);
})(angular);