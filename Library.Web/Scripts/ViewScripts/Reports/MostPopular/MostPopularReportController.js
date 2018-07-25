(function() {
	"use strict";

	angular.module("MostPopularReportModule")
	.controller("MostPopularReportController", ["paginationService", "mostPopularReportService", function (paginationService, mostPopularReportService) {
		var self = this;
		self.dateOptions = { changeYear: true, changeMonth: true, dateFormat: 'dd.mm.yy' };

		self.load = function() {
			mostPopularReportService.getAll(self.filters.from, self.filters.to).then(function (data) {
				self.gridOptions.data = data;

			});
		}

		self.gridOptions = (function() {
			var options = {
				columnDefs: [
					{ name: 'BookName', enableSorting: false },
					{ name: 'Count', enableSorting: false }
				]
			}
			var result = paginationService.getGridOptions(options, self.load);
			return result;
		})();
	}]);
})(angular);