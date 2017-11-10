(function (angular) {
	"use strict";

	angular.module("GenresModule")
	.controller("GenresController", ["$scope", "$compile", "genresService", "$ngConfirm", function ($scope, $compile, genresService, $ngConfirm) {
		$scope.actions = (function () {
			function _details() {
				
			}

			function _toogle(scope) {
				scope.toggle();
			}

			function _add(scope) {
				var nodeData = scope.$modelValue;
				nodeData.Children.push({
					Id: nodeData.Id * 10 + nodeData.Children.length,
					Name: nodeData.Name + '.' + (nodeData.Children.length + 1),
					Children: []
				});
			}

			function _rename(e,genre) {
				e.target.style.display = "none";
				
				var el = angular.element("<input id='editingGenre' type='text' ng-model='editiongGenre.value' ng-keypress='editiongGenre.keypress($event)' ng-blur='editiongGenre.blur($event)' data-nodrag />");
				$scope.editiongGenre = {
					value: e.target.textContent.trim(),
					keypress: function (event) {
						if (event.which === 13) {
							e.target.innerHTML = event.target.value;
							e.target.style.display = "";
							el.remove();
							event.preventDefault();

							angular.extend(genre, {
								Name: e.target.innerHTML
							});
							genresService.update(genre, function() {
								$ngConfirm("Genre <b>" + genre.Name + "</b> was updated");
							});
						}
					},
					blur: function(event) {
						e.target.style.display = "";
						el.remove();
						event.preventDefault();
					}
				}
				$compile(el)($scope);
				e.target.parentElement.append(el[0]);
				el[0].focus();
			}

			function _renameSubmit(e) {
				
			}

			function _getAll() {
				genresService.getAll(function (data) {
					$scope.Genres = data;
				});
			}

			function recursivellyDelete(genres, id) {
				for (var i = 0, len = genres.length; i < len; i++) {
					if (genres[i].Children && genres[i].Children.length > 0) {
						recursivellyDelete(genres[i].Children, id);
					}
					(function deletefromArray(forIndex) {
						if (genres[forIndex].Id === id) {
							var index = genres.indexOf(genres[forIndex]);
							genres.splice(index, 1);
						}
					})(i);
				}
			}
			function _remove(genre) {
				$ngConfirm({
					title: 'Confirm!',
					content: 'Are you sure you want to delete this genre with nested nodes?',
					scope: $scope,
					buttons: {
						ok: {
							text: 'Ok',
							btnClass: 'btn-blue',
							action: function () {
								var safeGenre = genre;
								genresService.remove(genre, function (response) {
									recursivellyDelete($scope.Genres, response.Id);
									$ngConfirm("Genre <b>" + safeGenre.Name + "</b> with nested nodes was deleted");
								});
							}
						},
						close: {
							text: "Cancel"
						}
					}
				});


				
			}
			return {
				getAll: _getAll,
				add: _add,
				remove: _remove,
				details: _details,
				rename: _rename,
				renameSubmit: _renameSubmit,
				toogle: _toogle
			}
		})();

		$scope.actions.getAll();
	}]);
})(angular);