var app = angular.module('groneApp', []);

app.factory('groneAppFactory', function ($http) {

    var posts = [];


    var factory = {};

    factory.GetPosts = function () {
        return posts;
    };

    factory.UpdatePostsObject = function () {
        $http({
            method: 'GET',
            url: '/Post/GetAllPosts'
        }).then(function successCallback(response) {
            angular.forEach(response.data, function (value, key) {
                posts.push(value);
            })
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    factory.LoadPreviewCommentsForPost = function (id) {
        $http({
            method: 'GET',
            url: '/Post/ViewCommentsByPosts',
            params: { Id: id }
        }).then(function successCallback(response) {
           console.log(response.data)
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    return factory;

});


app.controller('groneAppController', function ($scope, groneAppFactory) {
    $scope.posts = groneAppFactory.GetPosts();
    $scope.PreviewShowComments = function (event) {
        groneAppFactory.LoadPreviewCommentsForPost(event.target.attributes['data-parentId'].value);
        ToggleCommentSummary(event.target.attributes['data-parentId'].value);
    };
    $scope.ShowComments = function(event) {
        ToggleCommentSummary(event.target.attributes['data-parentId'].value);
    };
    $scope.GetPosts = function () {
        groneAppFactory.UpdatePostsObject();
    }

});