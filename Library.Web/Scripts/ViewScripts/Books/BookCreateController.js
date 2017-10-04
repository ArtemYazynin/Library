(function (angular) {
	"use strict";
	angular.module("BooksModule")
	.filter("byIdGroupFilter", [function () {
		return function (listObjects, idGroup, projection) {
			var result = [];
			if (!listObjects || !idGroup) return result;
			if (!(listObjects instanceof Array) || !(idGroup instanceof Array)) return result;

			function convert(item) {
				if (projection) {
					var objByProjection = {};
					for (var prop in projection) {
						if (projection.hasOwnProperty(prop)) {
							objByProjection[prop] = item[prop];
						}
					}
					return objByProjection;
				} else {
					return item;
				}
			}

			listObjects.forEach(function (item) {
				for (var i = 0, len = idGroup.length; i < len; i++) {
					if (item.Id === idGroup[i]) {
						var obj = convert(item);
						result.push(obj);
					}
				}
			});
			return result;
		}
	}])
	.controller("BookEditController", ["$scope", "$routeParams", "booksService", function ($scope, $routeParams, booksService) {
		
		$scope.loadManager = (function () {
			function _get() {
				booksService.get($routeParams.bookId, function(data) {
					$scope.vm = data;
				});
			}
			return {
				get: _get
			}
		})();

		(function init() {
			$scope.vm = {};
			$scope.loadManager.get();
		})();
	}])
	.controller("BookCreateController", ["$scope", "$filter", "$location", "editionsService", "publishersService", "authorsService",
		"genresService", "booksService", "$ngConfirm",
	function ($scope, $filter, $location, editionsService, publishersService, authorsService, genresService, booksService, $ngConfirm) {
		$scope.actions = (function () {
			function _create() {
				var data = {
					Name: $scope.vm.Name,
					Isbn: $scope.vm.Isbn,
					Description: $scope.vm.Description,
					Edition: (function () {
						var projection = {
							Id: "",
							Name: "",
							Year:""
						}
						return $filter("byIdGroupFilter")($scope.Editions, [$scope.vm.Edition], projection)[0];
					})(),
					Publisher: (function () {
						var projection = {
							Id: "",
							Name: ""
						}
						return $filter("byIdGroupFilter")($scope.Publishers, [$scope.vm.Publisher], projection)[0];
					})(),
					Genres: (function () {
						var projection = {
							Id: "",
							Name: ""
						}
						return $filter("byIdGroupFilter")($scope.Genres, $scope.vm.Genres, projection);
					})(),
					Authors: (function () {
						var projection = {
							Id: "",
							Lastname: "",
							Firstname: "",
							Middlename: ""
						}
						return $filter("byIdGroupFilter")($scope.Authors, $scope.vm.Authors, projection);
					})()
				}
				booksService.create(data, function() {
					$location.path("/");
					$ngConfirm("Book <strong>{{vm.Name}}</strong> created", $scope);
				});
			}
			function _back() {
				$location.path("/Books");
			}
			return {
				create: _create,
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