(function(angular) {
	"use strict";

	angular.module("AuthorsModule")
	.controller("AuthorDetailsController", ["$location", "$routeParams", "authorsService", "Notification", function ($location, $routeParams, authorsService, notification) {
		var self = this;
		function stepBack() {
			$location.path("/authors");
		}
		this.actions = {
			save: function () {
				if ($routeParams.authorId) {
					authorsService.update(self.vm, function (response) {
						self.vm = response;
						notification.success("Author <strong>" + self.vm.Fio + "</strong> was updated");
					});
				} else {
					authorsService.create(self.vm, function() {
						stepBack();
						notification.success("Author <strong>" + self.vm.Fio() + "</strong> was created");
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