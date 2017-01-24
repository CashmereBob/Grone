var app = angular.module('groneApp', []);

app.directive('postRepeatDirective', function ($location, groneAppFactory, $timeout, $anchorScroll) {
    return function (scope, element, attrs) {
        if (scope.$last) {
            BindCommentForm();
            $timeout(function () {

                if (typeof groneAppFactory.getParams !== 'undefined' && typeof $location.search().post !== 'undefined') {
                    console.log('handling params')
                    groneAppFactory.LoadCommentsForPost($location.search().post);
                    ToggleComments($location.search().post);

                    if (typeof $location.search().comment !== 'undefined') {
                        $location.hash($location.search().comment);
                        $anchorScroll();
                        BlinkDiv($location.search().comment);
                    } else {
                        $location.hash($location.search().post);
                        $anchorScroll();
                        BlinkDiv($location.search().post);
                    }
                    groneAppFactory.getParams = false;
                }

                if (typeof groneAppFactory.ScrollObject.PostEntityModelId !== 'undefined') {
                    console.log('handling comment')
                    groneAppFactory.LoadCommentsForPost(groneAppFactory.ScrollObject.PostEntityModelId);
                    ScrollAndTogglePost(groneAppFactory.ScrollObject.PostEntityModelId);

                } else if (typeof groneAppFactory.ScrollObject.Id !== 'undefined') {
                    console.log('handling post')
                    $location.hash(groneAppFactory.ScrollObject.Id);
                    $anchorScroll();
                    BlinkDiv(groneAppFactory.ScrollObject.Id);
                    groneAppFactory.ScrollObject = {};
                }

            }, 0);
        }
        
    };
});

app.directive('postCommentRepeatDirective', function ($location, groneAppFactory, $timeout, $anchorScroll) {
    return function (scope, element, attrs) {
        if (scope.$last) {
            BindPostCommentForm();

            $timeout(function () {

                if (typeof groneAppFactory.ScrollObject.PostEntityModelId !== 'undefined') {
                    console.log('handling SCROLLING')
                    $location.hash(groneAppFactory.ScrollObject.Id);
                    $anchorScroll();
                    BlinkDiv(groneAppFactory.ScrollObject.Id);
                    groneAppFactory.ScrollObject = {};

                }


            }, 0);
        }
    };
});



app.factory('groneAppFactory', function ($http, $location) {

    var posts = [];

    var factory = {};

    factory.GetPosts = function () {
        return posts;
    };

    factory.ScrollObject = {};

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


app.controller('groneAppController', function ($scope, groneAppFactory, $location, $anchorScroll) {

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

    $scope.UpdateScrollItem = function (object) {
        groneAppFactory.ScrollObject = object;
    }

    $scope.ScrollToAnchor = function (event) {
        BlinkDiv(event.target.attributes['data-parentId'].value);
        $location.hash(event.target.attributes['data-parentId'].value);
        $anchorScroll();
    }

});

