﻿(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.controller("BookCreateController", ["$scope", "$filter", "$location", "editionsService", "publishersService", "authorsService",
		"genresService", "booksService", "$routeParams", "Notification",
	function ($scope, $filter, $location, editionsService, publishersService, authorsService,
				genresService, booksService, $routeParams, notification) {
		$scope.actions = (function () {
			function _save() {
				if ($routeParams.bookId) {
					booksService.update($scope.vm, function (response) {
						notification.success("Book <strong>" + $scope.vm.Name + "</strong> was updated");
					});
				} else {
					booksService.create($scope.vm, function () {
						$location.path("/");
						notification.success("Book <strong>" + $scope.vm.Name + "</strong> was created");
					});
				}

			}
			function _back() {
				$location.path("/books");
			}
			return {
				save: _save,
				back: _back
			}
		})();
		$scope.loadManager = (function () {
			function _loadEditions() {
				editionsService.getAll(function(data) {
					$scope.Editions = data;
				});
			}
			function _loadPublishers() {
				publishersService.get(function (data) {
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
			function _loadBook() {
				if ($routeParams.bookId) {
					booksService.get($routeParams.bookId, function (data) {
						$scope.vm = data;
					});
				}
			}
			function _init() {
				_loadEditions();
				_loadPublishers();
				_loadAuthors();
				_loadGenres();
				_loadBook();
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