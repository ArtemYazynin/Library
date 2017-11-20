(function(angular) {
	"use strict";

	angular.module("PublishersModule")
		.controller("PublishersController", ["$scope","$compile", "publishersFactory", "$ngConfirm", function ($scope, $compile, publishersFactory, $ngConfirm) {
		$scope.actions = (function () {
			function _remove(publisher) {
				$ngConfirm({
					title: 'Confirm!',
					content: 'Are you sure you want to delete this publisher?',
					buttons: {
						ok: {
							text: 'Ok',
							btnClass: 'btn-blue',
							action: function () {
								publishersFactory.remove(publisher, function (deletedPublisher) {
									var index = $scope.Publishers.indexOf(deletedPublisher);
									$scope.Publishers.splice(index, 1);
									$ngConfirm("Publisher <strong>" + deletedPublisher.Name + "</strong> was deleted");
								});
							}
						},
						close: {
							text: "Cancel"
						}
					}
				});
				
			}

			function _enableEdit(e, publisher) {
				if (e.currentTarget.children.length !== 1) {
					return;
				}
				var el = angular.element("<input class='form-control' type='text' ng-model='editingPublisher.value' ng-keypress='editingPublisher.keypress($event)' ng-blur='editingPublisher.blur($event)' />");
				$scope.editingPublisher = {
					value: publisher.Name,
					keypress: function (event) {
						if (event.which === 13) {
							
						}
						event.preventDefault();
					},
					blur: function (event) {
						event.currentTarget.previousElementSibling.style.display = "";
						event.currentTarget.remove();
						delete $scope.editingPublisher;
						event.preventDefault();
					}
				}
				$compile(el)($scope);
				e.currentTarget.firstElementChild.style.display = "none";
				e.currentTarget.appendChild(el[0]);
				e.currentTarget.children[1].focus();
			}
			return {
				remove: _remove,
				enableEdit: _enableEdit
			}
		})();

		publishersFactory.get(function(response) {
			$scope.Publishers = response;
		});

	}]);
})(angular);