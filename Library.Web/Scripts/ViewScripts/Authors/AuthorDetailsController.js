(function(angular) {
	"use strict";

	angular.module("AuthorsModule")
	.controller("AuthorDetailsController", ["$location", "$routeParams", "authorsService", "$ngConfirm", function ($location, $routeParams, authorsService, $ngConfirm) {
		var self = this;
		function stepBack() {
			$location.path("/authors");
		}
		this.actions = {
			save: function () {
				if ($routeParams.authorId) {
					authorsService.update(self.vm, function (response) {
						self.vm = response;
						$ngConfirm({
							title: "Successfully updated!",
							content: "Author <strong>" + self.vm.Fio + "</strong> was updated",
							backgroundDismiss: true
						});
					});
				} else {
					authorsService.create(self.vm, function() {
						stepBack();
						$ngConfirm({
							title: "Successfully created!",
							content: "Author <strong>" + self.vm.Fio() + "</strong> was created",
							backgroundDismiss: true
						});
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
					self.vm = authorsService.getModel();
				}
			}
		}
		this.actions.init();
	}]);
})(angular);