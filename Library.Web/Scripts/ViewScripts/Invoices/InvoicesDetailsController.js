(function(angular) {
	"use strict";

	angular.module("InvoicesModule")
		.controller("InvoicesDetailsController", ["$location", "booksService", function ($location,booksService) {
			var self = this;
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
					
				}
				return {
					back: _back,
					save: _save,
					add: _add
				}
			})();
			this.vm = {
				IncomingBooks:[]
			}
		}]);
})(angular);