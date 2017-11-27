(function(angular) {
	"use strict";

	angular.module("PublishersModule")
		.controller("PublishersController", ["$scope","$compile", "publishersService", "$ngConfirm", function ($scope, $compile, publishersService, $ngConfirm) {
			var inputhtml = "<input class='form-control' type='text' ng-model='editingPublisher.beginValue' ng-keypress='editingPublisher.keypress($event)' ng-blur='editingPublisher.blur($event)' />";

			function EditingPublisher(entity) {
				this.entity = entity;
				this.beginValue = entity.Name;
			}
			EditingPublisher.prototype.keypress = function (event) {
				if (event.which === 13) {
					var vm = angular.extend(this.entity, {
						Name: this.beginValue
					});
					var self = this;
					publishersService.update(vm, function(response) {
						self.blur(event);
					});
				}
			}
			EditingPublisher.prototype.blur = function (event) {
				event.currentTarget.previousElementSibling.style.display = "";
				event.currentTarget.remove();
				event.preventDefault();
			}
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
								publishersService.remove(publisher, function (deletedPublisher) {
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
				$scope.editingPublisher = new EditingPublisher(publisher);
				var el = angular.element(inputhtml);
				$compile(el)($scope);
				e.currentTarget.firstElementChild.style.display = "none";
				e.currentTarget.appendChild(el[0]);
				e.currentTarget.children[1].focus();
			}

			function _addNew(e) {
				_enableEdit(e, {});

			}
			return {
				remove: _remove,
				enableEdit: _enableEdit,
				addNew: _addNew
			}
		})();

		publishersService.get(function(response) {
			$scope.Publishers = response;
		});

	}]);
})(angular);