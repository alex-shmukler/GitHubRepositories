var SearchController = function ($scope, $location, SearchService, DataShareService) {

    $scope.value = "";
    $scope.isEmpty = false;
    $scope.isLoading = false;

    $scope.searchValue = function () {
        if ($scope.value != "") {

            $scope.isEmpty = false;
            $scope.isLoading = true;

            SearchService.search($scope.value)
                .then(function (response) {

                    $scope.isLoading = false;

                    DataShareService.results = response.data;

                    if (response.data && response.data.length > 0) {

                        $location.path('/results');

                    } else {

                        $scope.isEmpty = true;
                    }
                }, function () {

                    $scope.isLoading = false;

                    DataShareService.results = [];

                    $scope.isEmpty = false;

                    alert("An error occurred");
                });
        }
    }
}

SearchController.$inject = ['$scope', '$location', 'SearchService', 'DataShareService']; 