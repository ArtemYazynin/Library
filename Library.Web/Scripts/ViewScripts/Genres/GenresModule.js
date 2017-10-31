﻿(function(angular) {
	"use strict";

	angular.module("GenresModule", ["ngRoute", "ngResource", "oi.select", "cp.ngConfirm"])
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
		function _get() {

		}
		function _create() {

		}
		function _update() {

		}
		function _remove() {

		}

		return {
			getModel: _getModel,
			getAll: _getAll,
			get: _get,
			create: _create,
			update: _update,
			remove: _remove
		}
	}]);
})(angular);