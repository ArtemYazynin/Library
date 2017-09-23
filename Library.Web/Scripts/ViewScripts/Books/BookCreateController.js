(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BookCreateController", ["$scope", "editionsService", "publishersService", "authorsService", "genresService",
	function ($scope, editionsService, publishersService, authorsService, genresService) {
		$scope.actions = (function () {
			function _create() {
				
			}
			return {
				create: _create
			}
		})();
		$scope.loadManager = (function () {
			function _loadEditions() {
				editionsService.getAll(function(data) {
					$scope.Editions = data;
				});
			}
			function _loadPublishers() {
				publishersService.getAll(function (data) {
					$scope.Publishers = data;
				});
			}
			function _loadAuthors() {
				authorsService.getAll(function (data) {
					$scope.Authors = data;
				});
			}
			function _loadGenres() {
				genresService.getAll(function (data) {
					$scope.Genres = data;
				});
			}
			function _init() {
				_loadEditions();
				_loadPublishers();
				_loadAuthors();
				_loadGenres();
			}
			return {
				loadEditions: _loadEditions,
				loadPublishers: _loadPublishers,
				loadAuthors: _loadAuthors,
				loadGenres: _loadGenres,
				init: _init
			}
		})();
		(function() {
			$scope.vm = {};
			$scope.loadManager.init();
		})();
	}]);
})(angular);