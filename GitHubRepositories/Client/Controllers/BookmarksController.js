var BookmarksController = function ($scope, BookmarksService) {

    $scope.bookmarks = [];

    $scope.getAllBookmarks = function () {

        BookmarksService.getBookmarks()
            .then(function (response) {

                $scope.bookmarks = response.data;

            }, function () {

                $scope.bookmarks = [];

                alert("An error occurred");
            });
    }

    $scope.getAllBookmarks();
}

BookmarksController.$inject = ['$scope', 'BookmarksService']; 