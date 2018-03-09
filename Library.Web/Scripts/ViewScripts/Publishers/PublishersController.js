(function(angular) {
	"use strict";

	angular.module("PublishersModule")
		.controller("PublishersController", ["$compile", "$scope", "publishersService", "$ngConfirm", "Notification", function ($compile, $scope, publishersService, $ngConfirm, notification) {
			var inputhtml = "<input class='form-control' type='text' ng-model='editingPublisher.beginValue' ng-keypress='editingPublisher.keypress($event)' ng-blur='editingPublisher.blur($event)' />";

			$scope.gridPublishers = {
				appScopeProvider: $scope,
				rowHeight: "40px",
				enableCellEdit: true,
				columnDefs: [
					{
						name: "Publisher",
						cellClass: function () { return "dataCell"; },
						width: "90%",
						field: "Name",
						cellTemplate:
						'<div class="ui-grid-cell-contents ng-binding ng-scope" ng-click="grid.appScope.actions.enableEdit($event,row.entity)">' +
							'<span ng-bind="row.entity.Name"></span>' +
						'</div>'
					},
					{
						name: ' ',
						enableSorting: false,
						cellClass: function () { return "operationCell"; },
						cellTemplate:
						'<grid-row-operations remove="grid.appScope.actions.remove(row.entity)">' +
						'</grid-row-operations>'
					}
				]
			};

			function EditingPublisher(entity) {
				this.entity = entity;
				this.beginValue = entity.Name;
			}

			EditingPublisher.prototype.keypress = function(event) {
				if (event.which === 13) {
					var vm = angular.extend(this.entity, {
						Name: this.beginValue
					});
					var self = this;
					publishersService.update(vm, function(updatedPublisher) {
						var index = $scope.gridPublishers.data.indexOf(updatedPublisher);
						$scope.gridPublishers.data[index] = updatedPublisher;
						self.blur(event);
						notification.success("Publisher <strong>" + updatedPublisher.Name + "</strong> was updated");
					});
				}
			};
			EditingPublisher.prototype.blur = function (event) {
					if (!event.currentTarget.previousElementSibling.style) {
						event.preventDefault();
						return;
					}
					event.currentTarget.previousElementSibling.style.display = "";
					event.currentTarget.remove();
					event.preventDefault();
				}
			$scope.actions = (function () {
				function _remove(publisher) {
					$ngConfirm({
						title: 'Confirm!',
						content: 'Are you sure you want to delete this publisher?',
						backgroundDismiss: true,
						buttons: {
							ok: {
								text: 'Ok',
								btnClass: 'btn-blue',
								action: function () {
									publishersService.remove(publisher, function (deletedPublisher) {
										var index = $scope.gridPublishers.data.indexOf(deletedPublisher);
										$scope.gridPublishers.data.splice(index, 1);
										notification.success("Publisher <strong>" + deletedPublisher.Name + "</strong> was deleted");
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
					publisher = publisher || {};
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
				function _showAddForm() {
					$scope.selectedRent = {};
					$scope.createDialog = $ngConfirm({
						title: "Publisher",
						scope: $scope,
						contentUrl: 'src/publisherDetails.html',
						backgroundDismiss: true,
						theme: 'light',
						closeIcon: true,
						columnClass: 'col-lg-4'
					});
				}
				function _save() {
					publishersService.create($scope.selectedRent, function (createdPublisher) {
						$scope.gridPublishers.data.push(createdPublisher);
						$scope.createDialog.close();
						notification.success("Publisher <strong>" + createdPublisher.Name + "</strong> was created");
					});
				}
				return {
					remove: _remove,
					enableEdit: _enableEdit,
					showAddForm: _showAddForm,
					save: _save
				}
			})();

			publishersService.get(function(response) {
				$scope.gridPublishers.data = response;
			});
	}]);
})(angular);