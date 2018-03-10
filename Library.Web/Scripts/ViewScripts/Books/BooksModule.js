(function (angular) {
	"use strict";

	angular.module("BooksModule", ["ngRoute", "ngResource", "oi.select", "cp.ngConfirm", "ui.grid", "ui.grid.pagination"])
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
		var config = {
			update: {
				method: 'PUT' // this method issues a PUT request
			}
		}
		var bookResource = $resource(baseUrl + "/:id", { id: "@Id" }, config);

		function _get(id, successCallback) {
			bookResource.get({ Id: id }, successCallback);
		}
		function _getAll(skip, take, successCallback) {
			bookResource.query({ skip: skip, take: take }, successCallback);
		}
		function _getAllPromise() {
			return bookResource.query().$promise;
		}
		function _search(filters) {
			var url = baseUrl + "/Search";
			var request = {
				params: filters
			}
			return $http.get(url, request).then(function (response) { return response.data; });
		}
		function _create(vm, successCallback) {
			var newBook = new bookResource(vm);
			newBook.$save(null, successCallback);
		}
		function _update(vm, successCallback) {
			vm.$update(successCallback);
		}
		function _remove(vm, successCallback) {
			if (!vm) return;
			if (!vm.$delete) {
				vm = new bookResource(vm);
			}
			
			vm.$delete(null, successCallback);
		}
		return {
			get: _get,
			getAll: _getAll,
			getAllPromise: _getAllPromise,
			search: _search,
			create: _create,
			update:_update,
			remove: _remove
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