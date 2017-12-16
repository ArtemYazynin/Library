(function(angular) {
	"use strict";

	angular.module("SubscribersModule")
		.controller("SubscribersController", ["subscribersService", "$ngConfirm", function (subscribersService, $ngConfirm) {
		var self = this;

		subscribersService.get(function(response) {
			self.Subscribers = response;
		});

		this.actions = (function () {
			function _remove(subscriber) {
				subscribersService.remove(subscriber, function (response) {
					var index = self.Subscribers.indexOf(response);
					self.Subscribers.splice(index, 1);
					$ngConfirm({
						title: "Successfully removed!",
						content: "Subscriber was removed",
						backgroundDismiss: true
					});
				});
			}

			function _create() {
				
			}
			return {
				remove: _remove,
				create: _create
			}
		})();
	}]);
})(angular);