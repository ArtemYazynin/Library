(function() {
	"use strict";

	angular.module("MostPopularReportModule", ["ngRoute", "ngResource"])
	.factory("mostPopularReportService", ["$http", function ($http) {
			function _getAll(from, to) {
				var request = {
					params: {
						from: from,
						to:to
					}
				}
				return $http.get("api/Reports/MostPopular", request).then(function(response) { return response.data; });
			}

			return {
			getAll: _getAll
		}
	}]);
})(angular);