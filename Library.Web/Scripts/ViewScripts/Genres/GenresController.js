(function (angular) {
	"use strict";

	angular.module("GenresModule")
	.controller("GenresController", ["genresService", function (genresService) {
		var self = this;
		this.actions = (function () {
			function _remove() {
				
			}
			function _details() {
				
			}
			return {
				remove: _remove,
				details: _details
			}
		})();

		genresService.getAll(function (data) {
			self.Genres = data;
		});
	}]);
})(angular);