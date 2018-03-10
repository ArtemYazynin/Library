(function(angular) {
	"use strict";

	angular.module("AuthorsModule", ["ngRoute", "ngResource", "oi.select", "cp.ngConfirm","ui.grid", "ui.grid.pagination"])
		.factory("authorsService", ["$resource", "$http", function ($resource, $http) {
			var baseUrl = "api/Authors";
			var config = {
				update: {
					method: 'PUT' // this method issues a PUT request
				}
			}
			var authorsResource = $resource(baseUrl + "/:id", { id: "@Id" }, config);
			function _getAll(skip, take, successCallback) {
				authorsResource.query({ skip: skip, take: take }, successCallback);
			}
			function _count() {
				var url = baseUrl + "/Count";
				return $http.get(url);
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

			function Model(data) {
				this.Lastname = data ? data.Lastname : "";
				this.Firstname = data ? data.Firstname : "";
				this.Middlename = data ? data.Middlename : "";
			}
			Model.prototype.Fio = function () {
				var result = this.Lastname + " " + this.Firstname;
				if (this.Middlename) {
					result += " " + this.Middlename;
				}
				return result;
			}
			function _getModel(data) {
				return new Model(data);
			}
			return {
				getModel: _getModel,
				getAll: _getAll,
				count: _count,
				get: _get,
				create: _create,
				update: _update,
				getRelatedBooks: _getRelatedBooks,
				remove: _remove
			}
		}
	]);
})(angular);