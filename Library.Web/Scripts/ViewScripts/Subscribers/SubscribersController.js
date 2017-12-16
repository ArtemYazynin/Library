(function(angular) {
	"use strict";

	angular.module("SubscribersModule")
		.controller("SubscribersController", ["$scope", "subscribersService", "$ngConfirm", function ($scope, subscribersService, $ngConfirm) {
		subscribersService.get(function(response) {
			$scope.Subscribers = response;
		});

		$scope.actions = (function () {
			function _remove(subscriber) {
				subscribersService.remove(subscriber, function (response) {
					var index = $scope.Subscribers.indexOf(response);
					$scope.Subscribers.splice(index, 1);
					$ngConfirm({
						title: "Successfully removed!",
						content: "Subscriber was removed",
						backgroundDismiss: true
						
					});
				});
			}

			function _save() {
				if ($scope.selectedSubscriber.Id) {
					subscribersService.update($scope.selectedSubscriber, function (response) {
						var s = response;
						var ms = $scope;
					});
				} else {
					subscribersService.create($scope.selectedSubscriber, function (response) {
						var s = response;
						var ms = $scope;
					});
				}
				
			}

			function _details(subscriber) {
				$scope.selectedSubscriber = subscriber;
				$ngConfirm({
					title: subscriber.Fio,
					scope: $scope,
					contentUrl: 'src/subscriberDetails.html',
					backgroundDismiss: true,
					closeIcon: true,
					theme: 'modern'
				});
			}
			return {
				remove: _remove,
				save: _save,
				details: _details
			}
		})();
	}]);
})(angular);