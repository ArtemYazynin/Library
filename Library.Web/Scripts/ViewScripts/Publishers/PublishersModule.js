(function(angular) {
	"use strict";
	angular.module("PublishersModule", ["ngRoute", "ngResource", "cp.ngConfirm"])
		.factory("publishersService", ["$resource","$ngConfirm", function ($resource, $ngConfirm) {
		var baseUrl = "api/Publishers";
		var config = {
			update: {
				method: 'PUT' // this method issues a PUT request
			}
		}
		var resource = $resource(baseUrl + "/:id", { id: "@Id" }, config);
		function _get(successCallback) {
			resource.query(successCallback);
		}

		function _update(vm, successCallback) {
			if (!vm.$update) {
				vm = new resource(vm);
			}
			vm.$update(successCallback);
		}
		function _remove(vm, successCallback) {
			if (!vm) return;
			if (!vm.$delete) {
				vm = new resource(vm);
			}

			vm.$delete(null, successCallback);
		}
		function _create(vm, successCallback) {
			var newAuthor = new resource(vm);
			newAuthor.$save(null, successCallback);
		}


		return {
			get: _get,
			update: _update,
			remove: _remove,
			create: _create
		}
	}]);
})(angular);