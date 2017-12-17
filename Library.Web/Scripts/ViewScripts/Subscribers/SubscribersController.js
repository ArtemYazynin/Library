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
					if (response.IsDeleted) {
						$scope.Subscribers[index] = response;
						
					} else {
						$scope.Subscribers.splice(index, 1);
					}
					$ngConfirm({
						title: "Successfully removed!",
						content: response.IsDeleted ? "Subscriber mark as removed, because he has Rents" : "Subscriber was removed",
						backgroundDismiss: true

					});
				});
			}

			function _save() {
				function clear() {
					delete $scope.selectedSubscriber;
					delete $scope.createDialog;
				}

				function closeDialog() {
					$scope.createDialog.close();
				}
				if ($scope.selectedSubscriber.Id) {
					subscribersService.update($scope.selectedSubscriber, function (response) {
						var index = $scope.Subscribers.indexOf(response);
						$scope.Subscribers[index] = response;

						$ngConfirm({
							title: "Successfully update!",
							content: "Subscriber was updated",
							backgroundDismiss: true

						});
					});
				} else {
					subscribersService.create($scope.selectedSubscriber, function (response) {
						$scope.Subscribers.push(response);
						closeDialog();
						clear();
					});
				}
				
			}

			function _details(subscriber) {
				$scope.selectedSubscriber = subscriber || {};
				$scope.createDialog = $ngConfirm({
					title: $scope.selectedSubscriber.Fio || "Create new subscriber",
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