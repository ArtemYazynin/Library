(function(angular) {
	"use strict";

	angular.module("RentsModule")
		.controller("RentsController", ["$scope", "rentsService", "subscribersService", "booksService", "$ngConfirm", function ($scope, rentsService, subscribersService, booksService, $ngConfirm) {
		$scope.dateOptions = {
			changeYear: true,
			changeMonth: true,
			dateFormat: 'dd.mm.yy'
		};
		rentsService.get(function(response) {
			$scope.Rents = response;
		});

		$scope.actions = (function () {
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

				subscribersService.get(function(response) { $scope.Subscribers = response; });
				booksService.getAll(function(response) { $scope.Books = response; });

				$scope.createDialog = $ngConfirm({
					title: "Rent",
					scope: $scope,
					contentUrl: 'src/rentDetails.html',
					backgroundDismiss: true,
					type: 'blue',
					closeIcon: true,
					theme: 'modern',
					columnClass: 'col-lg-12'
				});
			}

			function _activateOrDeactivate(rent) {
				rent.IsActive = !rent.IsActive;
				if (rent.IsActive) {
					rentsService.deactivate(rent,
						function(response) {
							var index = $scope.Rents.indexOf(response);
							$scope.Rents[index] = response;
						});
				} else {
					rentsService.activate(rent,
						function (response) {
							var index = $scope.Rents.indexOf(response);
							$scope.Rents[index] = response;
						});
				}
			}
			return {
				save: _save,
				details: _details,
				activateOrDeactivate: _activateOrDeactivate
			}
		})();
	}]);
})(angular);