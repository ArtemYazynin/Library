(function(angular) {
	"use strict";

	angular.module("PublishersModule")
	.controller("PublishersController", ["publishersFactory", "$ngConfirm", function (publishersFactory, $ngConfirm) {
		var self = this;
		this.actions = (function () {
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
									var index = self.Publishers.indexOf(deletedPublisher);
									self.Publishers.splice(index, 1);
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
			return {
				remove: _remove
			}
		})();

		publishersFactory.get(function(response) {
			self.Publishers = response;
		});

	}]);
})(angular);