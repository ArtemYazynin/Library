
(function (angular) {
	"use strict";

	angular.module("GenresModule", ["ngRoute", "ngResource", "oi.select", "cp.ngConfirm", "ui.tree"])
	.factory("genresService", ["$resource", "$http", function ($resource, $http) {
		var baseUrl = "api/Genres";
		var config = {
			update: {
				method: 'PUT' // this method issues a PUT request
			}
		}
		var genresResource = $resource(baseUrl + "/:id", { id: "@Id" }, config);

		function _getModel() {
			
		}
		function _getAll(successCallback) {
			genresResource.query(successCallback);
		}
		function _getTree() {
			var url = baseUrl + "/GetTree";
			return $http.get(url);
		}
		function _get() {

		}
		function _save(vm, successCallback) {
			var newGenres = new genresResource(vm);
			newGenres.$save(null, successCallback);
		}
		function _update(vm, successCallback) {
			if (!vm) return;
			if (!vm.$update) {
				vm = new genresResource(vm);
			}
			vm.$update(successCallback);
		}
		function _remove(vm, successCallback) {
			if (!vm) return;
			if (!vm.$delete) {
				vm = new genresResource(vm);
			}

			vm.$delete(null, successCallback);
		}

		return {
			getModel: _getModel,
			getAll: _getAll,
			getTree: _getTree,
			get: _get,
			save: _save,
			update: _update,
			remove: _remove
		}
	}]);
})(angular);