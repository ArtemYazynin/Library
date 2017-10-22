(function(angular) {
	"use strict";

	angular.module("AuthorsModule", ["ngRoute", "ngResource", "oi.select", "cp.ngConfirm"])
		.factory("authorsService", ["$resource", "$http", function ($resource, $http) {
			var baseUrl = "api/Authors";
			var config = {
				update: {
					method: 'PUT' // this method issues a PUT request
				}
			}
			var authorsResource = $resource(baseUrl + "/:id", { id: "@Id" }, config);

				function _getAll(successCallback) {
					authorsResource.query(successCallback);
				}
				function _get(id, successCallback) {
					authorsResource.get({ Id: id }, successCallback);
				}
				function _remove(vm, successCallback) {
					if (!vm) return;
					if (!vm.$delete) {
						vm = new authorsResource(vm);
					}

					vm.$delete(null, successCallback);
				}

				function _getRelatedBooks(id) {
					var url = baseUrl + "/RelatedBooks/"+id;
					return $http.get(url).then(function (response) { return response.data; });
				}
				function _create(vm, successCallback) {
					var newAuthor = new authorsResource(vm);
					newAuthor.$save(null, successCallback);
				}
				function _update(vm, successCallback) {
					vm.$update(successCallback);
				}
				return {
					getAll: _getAll,
					get: _get,
					create: _create,
					update: _update,
					getRelatedBooks: _getRelatedBooks,
					remove: _remove
				}
			}
		]);
})(angular);