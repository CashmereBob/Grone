var app = angular.module('groneApp', []);

app.factory('groneAppFactory', function () {

    var posts = [
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc1",
            Titel: "Vad är Lorem Ipsum?",
            Description: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            TimeLeft: 20,
            TimeAdded: 4000,
            MemberId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comments: [
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comment: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            CommentId: "",
            MemberId: "#450b1d33-4938-429e-8d25-7a3b76b5adcc",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848"
        },
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comment: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            CommentId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            MemberId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848"
        }
            ]
        },
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc2",
            Titel: "Vad är Lorem Ipsum?",
            Description: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            TimeLeft: 20,
            TimeAdded: 4000,
            MemberId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comments: [
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comment: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            CommentId: "",
            MemberId: "#450b1d33-4938-429e-8d25-7a3b76b5adcc",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848"
        },
        {
            Id: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            Comment: "Lorem Ipsum är en utfyllnadstext från tryck- och förlagsindustrin. Lorem ipsum har varit standard ända sedan 1500-talet.....",
            Date: "xxxx-xx-xx xx:xx:xxxx",
            CommentId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            MemberId: "450b1d33-4938-429e-8d25-7a3b76b5adcc",
            ImgSrc: "http://www.miataturbo.net/attachments/insert-bs-here-4/78009-random-pictures-thread-only-rule-before-after-kumquats-1682345-slide-slide-1-biz-stone-explains-how-he-turned-91-random-photos-into-movie-jpg?dateline=1370019848"
        }
            ]
        }

    ];


    var factory = {};

    factory.GetPosts = function () {
        return posts;
    };

    return factory;

});


app.controller('groneAppController', function ($scope, groneAppFactory) {
    $scope.posts = groneAppFactory.GetPosts();
    $scope.ShowComments = function (event) {
        ToggleCommentSummary(event.target.id);
    };

});