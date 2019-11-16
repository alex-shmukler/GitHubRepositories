var ResultsController = function ($scope, $rootScope, DataShareService, BookmarksService) {

    $scope.repositories = DataShareService.results;

    $scope.bookmark = function (repository) {

        BookmarksService.createBookmark(repository)
            .then(function (response) {

                if (response && response.status === 200) {

                    $rootScope.$emit("enable-bookmarks", true);
                    alert("Added to Bookmarks");

                }
            }, function (response) {

                    if (response && response.status === 409) {

                        alert("Repository already Bookmarked");

                    } else {

                        alert("An error occurred");
                }
            });
    }
}

ResultsController.$inject = ['$scope', '$rootScope', 'DataShareService', 'BookmarksService']; 