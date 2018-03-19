(function(angular) {
	"use strict";

	angular.module("RentsModule", ["ngRoute", "ngResource", "cp.ngConfirm", "ui.date", "ui.grid", "ui.grid.autoResize"])
		.factory("rentsService", ["$resource",function ($resource) {
		var baseUrl = "api/Rents";
		var config = {
			update: {
				method: 'PUT' // this method issues a PUT request
			}
		}
		var resource = $resource(baseUrl + "/:id", { id: "@Id" }, config);
		function _get(pagingModel, successCallback) {
			resource.query(pagingModel, successCallback);
		}
		function _create(vm, successCallback) {
			var invoice = new resource(vm);
			invoice.$save(null, successCallback);
		}

		function _activate(vm, successCallback) {
			if (!vm.$update) {
				vm = new resource(vm);
			}
			vm.$update(successCallback);
		}

		function _deactivate(vm, successCallback) {
			if (!vm.$update) {
				vm = new resource(vm);
			}
			vm.$update(successCallback);
		}
		return {
			get: _get,
			create: _create,
			activate: _activate,
			deactivate: _deactivate
		}
	}]);
})(angular);