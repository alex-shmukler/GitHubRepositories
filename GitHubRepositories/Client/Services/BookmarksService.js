var BookmarksService = function ($http) {

    BookmarksService.getBookmarks = function () {
        return $http(
            {
                method: 'GET',
                url: 'Home/Bookmarks'
            });
    }

    BookmarksService.createBookmark = function (value) {
        return $http(
            {
                method: 'POST',
                url: 'Home/Bookmark',
                data: JSON.stringify(value)
            });
    }

    return BookmarksService;
}