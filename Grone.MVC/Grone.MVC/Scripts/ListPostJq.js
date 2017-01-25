function ToggleCommentSummary(id) {
    var div = $('.listOfComments', '#' + id);

    if (div.hasClass("full")) {
        div.slideToggle();
        div.toggleClass("full");
    }

    if (typeof id !== 'undefined') {
        div.slideToggle();
        div.toggleClass("preview")
    }
}

function ToggleComments(id) {
    var div = $('.listOfComments', '#' + id);
    
    if (div.hasClass("preview")) {
        div.slideToggle();
        div.toggleClass("preview");
    }

    if (typeof id !== 'undefined') {
        div.slideToggle();
        div.toggleClass("full");
    }
}

function ScrollAndTogglePost(id) {
    var div = $('.listOfComments', '#' + id);

    console.log('toggel');
    if (div.hasClass("preview")) {
        div.slideToggle();
        div.toggleClass("preview");
    }

    if (!div.hasClass("full")) {
        div.slideToggle();
        div.toggleClass("full");
    }
}

function BlinkDiv(id) {
    var div = $('#' + id);

    div.toggleClass('blink');
    div.toggleClass('hoverColor');

    setTimeout(function () {
        div.toggleClass('blink');
        div.toggleClass('hoverColor');
    }, 1000);
}

$(document).ready(function () {
 
    var postForm = $('#AddPostForm');

    postForm.submit(function (e) {
        e.preventDefault();
        AddPost(postForm, $(this).parent().parent().parent().parent());
    })

})

function AddPost(form, div) {
    StartPostLoad();
        $.ajax({
            type: "POST",
            url: "/Post/Add",
            data: new FormData(form[0]),
            success: function (data) {
                setTimeout(function () {
                angular.element($("#GroneAppController")).scope().sortBy.item = '-Date';
                angular.element($("#GroneAppController")).scope().UpdateScrollItem(data);
                angular.element($("#GroneAppController")).scope().$apply();
                form[0].reset();
                StopPostLoad();
                angular.element($("#GroneAppController")).scope().GetPosts();
                angular.element($("#GroneAppController")).scope().$apply();
                div.modal('hide');
                }, 1000);
            },

            error: function (e) {
                console.log('no');
            },
            processData: false,
            contentType: false
        });
}

function BindCommentForm() {
    $(".commentForm").each(function (index, element) {
        SetSubmit($(this).children())
    })
}

function BindPostCommentForm() {
    $(".commentCommentForm").each(function (index, element) {
        SetSubmit($(this).children())
    }) 
}

function SetSubmit(form) {
    var div = form.parent().parent().parent();
    

    form.submit(function (e) {
        e.preventDefault();
        
        StartCommentLoad();

        $.ajax({
            type: "POST",
            url: "/Comment/AddComment",
            data: new FormData(form[0]),
            success: function (data) {
                setTimeout(function () {
                angular.element($("#GroneAppController")).scope().UpdateScrollItem(data);
                angular.element($("#GroneAppController")).scope().$apply();
                form[0].reset();
                StopCommentLoad();

                angular.element($("#GroneAppController")).scope().GetPosts();
                angular.element($("#GroneAppController")).scope().$apply();
                div.modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                }, 1000);
            },

            error: function (e) {
                console.log('no');
            },
            processData: false,
            contentType: false
        });
    })

}

function StartBigLoad() {
    $('#postContent').hide();
    
    $('#postContent').fadeOut("slow", function () {
        $('#bigLoaderContent').fadeIn("slow");
        
    });
}

function StopBigLoad() {
    
    $('#bigLoaderContent').fadeOut("slow", function () {
        $('#postContent').fadeIn("slow");
       
    });
}

function StartPostLoad() {
    $('#postLoader').fadeIn("slow").css("display", "inline-block");
}

function StopPostLoad() {
    $('#postLoader').fadeOut("slow").css("display", "none");
}

function StartCommentLoad() {
    $('.commentLoader').each(function (e) {
        $(this).css("display", "inline-block");

    });
}

function StopCommentLoad() {
    $('.commentLoader').each(function (e) {
        $('#postLoader').fadeOut("slow").css("display", "none");
    });
}

function StartGetAllCommentLoad(id) {
    var div = $('#' + id);
    var loader = $($(div.children()[0]).children()[6]);
    loader.fadeIn("slow");
}

function StopGetAllCommentLoad(id) {
    var div = $('#' + id);
    var loader = $($(div.children()[0]).children()[6]);
    loader.fadeOut();
}