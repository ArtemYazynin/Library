(function(angular) {
	"use strict";

	angular.module("AuthorsModule")
	.controller("AuthorDetailsController", ["$location", "$routeParams", "authorsService", "$ngConfirm", function ($location, $routeParams, authorsService, $ngConfirm) {
		var self = this;
		function Model() {
			this.Lastname = "";
			this.Firstname = "";
			this.Middlename = "";
		}
		Model.prototype.Fio = function() {
			var result = this.Lastname + " " + this.Firstname;
			if (this.Middlename) {
				result += " "+this.Middlename;
			}
			return result;
		}
		function stepBack() {
			$location.path("/authors");
		}
		this.actions = {
			save: function () {
				if ($routeParams.authorId) {
					authorsService.update(self.vm, function() {
						$ngConfirm("Author <strong>"+self.vm.Fio()+"</strong> was updated");
					});
				} else {
					authorsService.create(self.vm, function() {
						stepBack();
						$ngConfirm("Author <strong>" + self.vm.Fio() + "</strong> was created");
					});
				}

			},
			back: function() {
				stepBack();
			},
			init: function () {
				if ($routeParams.authorId) {
					authorsService.get($routeParams.authorId, function(data) {
						self.vm = data;
					});
				} else {
					self.vm = new Model();
				}
			}
		}
		this.actions.init();
	}])
	.controller("AuthorsController", ["$scope", "$location", "authorsService", "$ngConfirm", function ($scope, $location, authorsService, $ngConfirm) {
		$scope.actions = (function () {
			function _init() {
				authorsService.getAll(function (response) { $scope.Authors = response; });
			}
			function _details(author) {
				$location.path("/authors/" + author.Id + "/edit");
			}
			function _remove(author) {
				function removeInternal(author) {
					var fio = author.Fio;
					authorsService.remove(author, function (deletedAuthor) {
						var index = $scope.Authors.indexOf(deletedAuthor);
						$scope.Authors.splice(index, 1);
						$ngConfirm("Author <strong>" + fio + "</strong> was deleted");
					});
				}
				function removeConfirm(author) {
					$ngConfirm({
						title: 'Confirm!',
						content: 'Are you sure you want to delete this author?',
						scope: $scope,
						buttons: {
							ok: {
								text: 'Ok',
								btnClass: 'btn-blue',
								action: function () {
									removeInternal(author);
								}
							},
							close: {
								text: "Cancel"
							}
						}
					});
				}
				authorsService.getRelatedBooks(author.Id).then(function(data) {
					if (data.length === 0) {
						removeConfirm(author);
					} else {
						var booksStr = data.join(", ");
						$ngConfirm({
							title: 'Confirm!',
							content: 'Author <b style="color:green;">' + author.Fio + '</b> has next related books:<b style="color:red;">' + booksStr + '</b>. <br />Continue?',
							scope: $scope,
							buttons: {
								ok: {
									text: 'Ok',
									btnClass: 'btn-blue',
									action: function () {
										removeInternal(author);
									}
								},
								close: {
									text: "Cancel"
								}
							}
						});
					}
				});

			}
			return {
				init: _init,
				details: _details,
				remove: _remove
			}
		})();
		$scope.actions.init();
	}]);
})(angular);