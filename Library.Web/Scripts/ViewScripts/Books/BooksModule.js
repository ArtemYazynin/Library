(function (angular) {
	"use strict";

	angular.module("BooksModule", ["ngRoute", "ngResource", "oi.select"])
	.factory("genresService", ["$resource", function ($resource) {
		var baseUrl = "api/Genres";
		var genresResource = $resource(baseUrl + "/:id", { id: "@Id" });
		function _getAll(successCallback) {
			genresResource.query(successCallback);
		}
		return {
			getAll: _getAll
		}
	}])
	.factory("authorsService", ["$resource", function ($resource) {
		var baseUrl = "api/Authors";
		var authorsResource = $resource(baseUrl + "/:id", { id: "@Id" });
		function _getAll(successCallback) {
			authorsResource.query(successCallback);
		}
		return {
			getAll: _getAll
		}
	}])
	.factory("publishersService", ["$resource", function ($resource) {
		var baseUrl = "api/Publishers";
		var publishersResource = $resource(baseUrl + "/:id", { id: "@Id" });
		function _getAll(successCallback) {
			publishersResource.query(successCallback);
		}
		return {
			getAll: _getAll
		}
	}])
	.factory("editionsService", ["$resource", function ($resource) {
		var baseUrl = "api/Editions";
		var editionsResource = $resource(baseUrl + "/:id", { id: "@Id" });
		function _getAll(successCallback) {
			editionsResource.query(successCallback);
		}
		return {
			getAll: _getAll
		}
	}])
	.factory("booksService", ["$resource", "$http", function ($resource, $http) {
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
		function _create(vm) {
			var newBook = new bookResource(vm);
			newBook.$save();
		}
		return {
			getAll: _getAll,
			search: _search,
			create: _create
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