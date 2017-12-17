(function(angular) {
	"use strict";

	angular.module("RentsModule")
		.controller("RentsController", ["$scope", "rentsService", "$ngConfirm", function ($scope, rentsService, $ngConfirm) {
		rentsService.get(function(response) {
			$scope.Rents = response;
		});

		$scope.actions = (function () {
			function _remove(rent) {
				rentsService.remove(rent, function (response) {
					var index = $scope.Rents.indexOf(response);
					if (response.IsDeleted) {
						$scope.Rents[index] = response;
						
					} else {
						$scope.Rents.splice(index, 1);
					}
					$ngConfirm({
						title: "Successfully removed!",
						content: response.IsDeleted ? "Rent mark as removed, because he has Rents" : "Rent was removed",
						backgroundDismiss: true

					});
				});
			}

			function _save() {
				function clear() {
					delete $scope.selectedRent;
					delete $scope.createDialog;
				}

				function closeDialog() {
					$scope.createDialog.close();
				}
				if ($scope.selectedRent.Id) {
					rentsService.update($scope.selectedRent, function (response) {
						var index = $scope.Rents.indexOf(response);
						$scope.Rents[index] = response;

						$ngConfirm({
							title: "Successfully update!",
							content: "Rent was updated",
							backgroundDismiss: true

						});
					});
				} else {
					rentsService.create($scope.selectedRent, function (response) {
						$scope.Rents.push(response);
						closeDialog();
						clear();
					});
				}
				
			}

			function _details(rent) {
				$scope.selectedRent = rent || {};
				$scope.createDialog = $ngConfirm({
					title: $scope.selectedRent.Fio || "Create new rent",
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