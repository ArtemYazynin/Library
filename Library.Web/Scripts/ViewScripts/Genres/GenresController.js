(function (angular) {
	"use strict";

	angular.module("GenresModule")
	.controller("GenresController", ["$scope", "$q", "$compile", "genresService", "$ngConfirm", function ($scope, $q, $compile, genresService, $ngConfirm) {
		$scope.actions = (function () {
			function _getAll() {
				genresService.getTree().then(function (response) {
					$scope.Genres = response.data;
				});
			}

			function postSaveConfirm(genreName) {
				$ngConfirm({
					title: "Successfully created!",
					content: "Genre <strong>" + genreName + "</strong> was created",
					backgroundDismiss: true
				});
			}

			function _init() {
				var emptyGenre = {
					Name: "New Genre"
				};
				genresService.save(emptyGenre, function (response) {
					$scope.Genres.push(response);
					postSaveConfirm(response.Name);
				});
				
			}

			function _toogle(scope) {
				scope.toggle();
			}

			function _add(scope) {
				var nodeData = scope.$modelValue;
				var newGenre = {
					Id: nodeData.Id * 10 + nodeData.Children.length,
					Name: nodeData.Name + '.' + (nodeData.Children.length + 1),
					Parent: { Id: nodeData.Id, Name: nodeData.Name },
					Children: []
				};
				genresService.save(newGenre, function (response) {
					nodeData.Children.push(response);
					postSaveConfirm(response.Name);
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
								$ngConfirm({
									title: "Successfully updated!",
									content: "Genre <b>" + genre.Name + "</b> was updated",
									backgroundDismiss: true
								});
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
					backgroundDismiss: true,
					scope: $scope,
					buttons: {
						ok: {
							text: 'Ok',
							btnClass: 'btn-blue',
							action: function () {
								var safeGenre = genre;
								genresService.remove(genre, function (response) {
									recursivellyDelete($scope.Genres, response.Id);
									$ngConfirm({
										title: "Successfully removed!",
										content: "Genre <b>" + safeGenre.Name + "</b> with nested nodes was deleted",
										backgroundDismiss: true
									});
								});
							}
						},
						close: {
							text: "Cancel"
						}
					}
				});
			}


			function _hasUnsaved() {
				function recurcivelyFindUnsavedGenres(children) {
					if (!children || children.length === 0) {
						return false;
					}
					for (var i = 0, len = children.length; i < len; i++) {
						var found = recurcivelyFindUnsavedGenres(children[i].Children);
						if (found) {
							return true;
						}
					}
				}
				var result = recurcivelyFindUnsavedGenres($scope.Genres);
				return result;
			}
			function _save() {
				var unsavedGenres = (function() {
					var result = [];
					function recursivelyFindUnsavedGenres(children) {
						for (var i = 0, len = children.length; i < len; i++) {
							recursivelyFindUnsavedGenres(children[i].Children);
						}
					}
					recursivelyFindUnsavedGenres($scope.Genres);
					return result;
				})();
				genresService.save(unsavedGenres, function () {
					$ngConfirm("Genres <strong>" + self.vm.Fio() + "</strong> was created");
				});
				
			}
			return {
				init: _init,
				getAll: _getAll,
				add: _add,
				remove: _remove,
				rename: _rename,
				toogle: _toogle,
				hasUnsaved: _hasUnsaved,
				save: _save
			}
		})();

		$scope.actions.getAll();
	}]);
})(angular);