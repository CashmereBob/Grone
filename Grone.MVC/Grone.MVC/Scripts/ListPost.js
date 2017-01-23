var app = angular.module('groneApp', []);

app.directive('postRepeatDirective', function ($location, groneAppFactory, $timeout) {
    return function (scope, element, attrs) {
        if (scope.$last) {
            BindCommentForm();
            $timeout(function () {
                if (groneAppFactory.getParams) {
                    groneAppFactory.LoadCommentsForPost($location.search().post);
                    ToggleComments($location.search().post);
                    groneAppFactory.getParams = false;
                }
            }, 0);
        }
        
    };
});

app.directive('postCommentRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            BindPostCommentForm();
        }
    };
});



app.factory('groneAppFactory', function ($http) {

    

    var posts = [];

    var factory = {};

    factory.GetPosts = function () {
        return posts;
    };

    factory.getParams = true;

    factory.UpdatePostsObject = function () {
       
        $http({
            method: 'GET',
            url: '/Post/GetAllPosts'
        }).then(function successCallback(response) {
            posts.length = 0;
            angular.forEach(response.data, function (value, key) {
                value.Comments = [];
                posts.push(value);
            })
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    factory.LoadPreviewCommentsForPost = function (id) {
        $http({
            method: 'GET',
            url: '/Post/GetPreviewCommentsByPosts',
            params: { Id: id }
        }).then(function successCallback(response) {
            angular.forEach(posts, function (post, key) {
                if (post.Id == id) {
                    post.Comments.length = 0;
                    angular.forEach(response.data, function (comment, index) {
                        if (comment.CommentId == "00000000-0000-0000-0000-000000000000") {
                            comment.CommentId = "";
                        }
                        post.Comments.push(comment);
                    })
                }
            })
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    factory.LoadCommentsForPost = function (id) {
        $http({
            method: 'GET',
            url: '/Post/GetCommentsBypost',
            params: { Id: id }
        }).then(function successCallback(response) {
            angular.forEach(posts, function (post, key) {
                if (post.Id == id) {
                    post.Comments.length = 0;
                    angular.forEach(response.data, function (comment, index) {
                        if (comment.CommentId == "00000000-0000-0000-0000-000000000000") {
                            comment.CommentId = "";
                        }
                        post.Comments.push(comment);
                    })
                }
            })
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    return factory;

});


app.controller('groneAppController', function ($scope, groneAppFactory, $location) {

    var getParams = true;

    $scope.posts = groneAppFactory.GetPosts();
    $scope.PreviewShowComments = function (event) {

        groneAppFactory.LoadPreviewCommentsForPost(event.target.attributes['data-parentId'].value);

        ToggleCommentSummary(event.target.attributes['data-parentId'].value);
    };
    $scope.ShowComments = function (event) {

        groneAppFactory.LoadCommentsForPost(event.target.attributes['data-parentId'].value);
        ToggleComments(event.target.attributes['data-parentId'].value);
    };
    $scope.GetPosts = function () {
        groneAppFactory.UpdatePostsObject();
        
    }



});

