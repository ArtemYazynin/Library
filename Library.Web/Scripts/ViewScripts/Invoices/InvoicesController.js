(function(angular) {
	"use strict";

	angular.module("InvoicesModule")
		.controller("InvoicesController", ["invoicesService", "Notification", function (invoicesService, notification) {
		var self = this;

		invoicesService.get(function(response) {
			self.Invoices = response;
		});

		this.actions = (function () {
			function _remove(invoice) {
				invoicesService.remove(invoice, function (response) {
					var index = self.Invoices.indexOf(response);
					self.Invoices.splice(index, 1);
					notification.success("Invoice was removed");
				});
			}

			function _create() {
				
			}
			return {
				remove: _remove,
				create: _create
			}
		})();
	}]);
})(angular);