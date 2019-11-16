var App = angular.module('App', ['ngRoute']); 

App.controller('MainController', MainController); 
App.controller('SearchController', SearchController); 
App.controller('BookmarksController', BookmarksController); 
App.controller('ResultsController', ResultsController); 

App.service('BookmarksService', BookmarksService);
App.service('DataShareService', DataShareService);
App.service('SearchService', SearchService);

var routeConfig = function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/search', {
            templateUrl: 'Client/Views/Search.html',
            controller: 'SearchController'
        })
        .when('/bookmarks', {
            templateUrl: 'Client/Views/Bookmarks.html',
            controller: 'BookmarksController'
        })
        .when('/results', {
            templateUrl: 'Client/Views/Results.html',
            controller: 'ResultsController'
        })
        .otherwise({
            redirectTo: '/search'
        });

    $locationProvider.html5Mode({
        enebaled: true,
        requireBase: false
    });

    $locationProvider.hashPrefix('');
}

routeConfig.$inject = ['$routeProvider', '$locationProvider'];

App.config(routeConfig);

