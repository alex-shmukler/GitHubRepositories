var SearchService = function ($http) {

    SearchService.search = function (value) {
        return $http(
            {
                method: 'GET',
                url: 'Home/Search?value=' + value
            });
    }

    return SearchService;
}