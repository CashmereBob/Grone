var app = angular.module('groneApp', []);

app.directive('postRepeatDirective', function ($location, groneAppFactory, $timeout, $anchorScroll) {
    return function (scope, element, attrs) {
        if (scope.$last) {
            BindCommentForm();
            $timeout(function () {

                if (groneAppFactory.getParams == true  && typeof $location.search().post !== 'undefined') {
                    console.log('handling params')
                    groneAppFactory.LoadCommentsForPost($location.search().post);
                    ToggleComments($location.search().post);

                        $location.hash($location.search().post);
                        $anchorScroll();
                        BlinkDiv($location.search().post);
                        if (typeof $location.search().comment == 'undefined') { groneAppFactory.getParams = false; }
                    
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

                if (groneAppFactory.getParams == true && typeof $location.search().comment !== 'undefined') {
                        $location.hash($location.search().comment);
                        $anchorScroll();
                        BlinkDiv($location.search().comment);
                   
                    groneAppFactory.getParams = false;
                }

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

    factory.CountDownTime = function () {
        console.log('counting');

        angular.forEach(posts, function (value, key) {
            
            value.TimeLeft = value.TimeLeft - 1;
            
            if (value.TimeLeft == 0) {
                posts.splice(key, 1);
            }
        });

    }

    factory.UpdatePostsObject = function () {
        StartBigLoad();
        $http({
            method: 'GET',
            url: '/Post/GetAllPosts'
        }).then(function successCallback(response) {
            posts.length = 0;
            angular.forEach(response.data, function (value, key) {
                value.Comments = [];
                posts.push(value);
            })
            StopBigLoad();
        }, function errorCallback(response) {
            console.log('fail')
        });
    };

    factory.LoadPreviewCommentsForPost = function (id) {
        StartGetAllCommentLoad(id);
        $http({
            method: 'GET',
            url: '/Post/GetPreviewCommentsByPosts',
            params: { Id: id }
        }).then(function successCallback(response) {
            StopGetAllCommentLoad(id);
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
        StartGetAllCommentLoad(id);
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
            StopGetAllCommentLoad(id);
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

    $scope.sortBy = {};
    $scope.sortBy.item = '-Date';

    $scope.OrderList = [
        { id: '-Date', name: "DATE desc" },
        { id: '+Date', name: "DATE asc" },
        { id: '-TimeLeft', name: "TIME LEFT desc" },
        { id: '+TimeLeft', name: "TIME LEFT asc" },
        { id: '-TimeAdded', name: "LIFETIME desc" },
        { id: '+TimeAdded', name: "LIFETIME asc" }];

    

    setInterval(function () { groneAppFactory.CountDownTime(); $scope.$apply()  }, 60000);
    


});

