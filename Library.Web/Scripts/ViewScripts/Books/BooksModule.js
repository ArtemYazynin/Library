(function (angular) {
	"use strict";

	angular.module("BooksModule", ["ngRoute","ngResource"])
	.factory("bookService", ["$resource", "$location", function ($resource, $location) {
		var localLocation = $location;
		var bookResource = $resource("/api/Books/:id", { id: "@Id" });
		function _getAll(successCallback) {
			bookResource.query(successCallback);
		}
		return {
			getAll: _getAll
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