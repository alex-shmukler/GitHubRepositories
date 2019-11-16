var MainController = function ($scope, $rootScope) {

    $rootScope.enableBookmarks = false;

    $rootScope.$on("enable-bookmarks", function (evt, arg) {

        $rootScope.enableBookmarks = arg;

    });
}

MainController.$inject = ['$scope', '$rootScope']; 
