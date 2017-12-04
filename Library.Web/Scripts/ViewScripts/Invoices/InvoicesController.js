(function(angular) {
	"use strict";

	angular.module("InvoicesModule")
	.controller("InvoicesController", ["invoicesService",function (invoicesService) {
		var self = this;

		invoicesService.get(function(response) {
			self.Invoices = response;
		});
	}]);
})(angular);