(function(angular) {
	"use strict";

	angular.module("InvoicesModule")
		.controller("InvoicesDetailsController", ["$location", "booksService", "invoicesService", "Notification", function ($location, booksService, invoicesService, notification) {
			var self = this;
			this.booksFn = function () {
				if (!self.Books || !self.vm.IncomingBooks) return self.Books;

				var result = [];
				self.Books.forEach(function (current, index) {
					var found = false;
					for (var i = 0; i < self.vm.IncomingBooks.length; i++) {
						if (current.Id === self.vm.IncomingBooks[i].Book.Id) {
							found = true;
						}
					}
					if (!found) {
						result.push(current);
					}
				});


				return result;
			}
			booksService.getAll(function(response) {
				self.Books = response;
			});
			this.actions = (function () {
				function _back() {
					$location.path("/invoices");
				}

				function _add() {
					var incomingBook = {
						Count: self.Count,
						Book: self.Book
					}
					self.vm.IncomingBooks.push(incomingBook);
					delete self.Count;
					delete self.Book;
				}

				function _save() {
					invoicesService.create(self.vm, function () {
						_back();
						notification.success("Invoice was created");
					});
				}

				function _remove(incomingBook) {
					var index = self.vm.IncomingBooks.indexOf(incomingBook);
					self.vm.IncomingBooks.splice(index, 1);
				}
				return {
					back: _back,
					save: _save,
					add: _add,
					remove: _remove
				}
			})();
			this.vm = {
				IncomingBooks:[]
			}
		}]);
})(angular);