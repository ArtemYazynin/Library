(function (angular) {
	"use strict";

	angular.module("BooksModule", ["ngRoute","ngResource"])
	.factory("bookService", ["$resource", "$http", function ($resource, $http) {
		var baseUrl = "/api/Books";
		var bookResource = $resource(baseUrl + "/:id", { id: "@Id" });
		function _getAll(successCallback) {
			bookResource.query(successCallback);
		}
		function _search(filters) {
			var url = baseUrl + "/Search";
			var request = {
				params: filters
			}
			return $http.get(url, request).then(function (response) { return response.data; });
		}
		return {
			getAll: _getAll,
			search: _search
		}
	}]);

})(angular);

/*.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
		$routeProvider.when("/", {
			templateUrl: "/LibraryView/Books",
			controller: "BooksController",
			resolve: (function () {
				return {
					changeSetWidgetConfig: [function () {
						return {
							resolveUrl: window.UrlHelper.convert("~/Institutions/ResolveEducInstitutionInfo")
						}
					}]
				}
			})()
		});
	}])*/