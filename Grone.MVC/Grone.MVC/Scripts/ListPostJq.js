﻿function ToggleCommentSummary(id) {
    if (id.length > 10 && $('.listOfComments', '#' + id).is( ":hidden" )) {
        $('.listOfComments', '#' + id).slideToggle();
    }
}

$(document).ready(function () {
 
    var postForm = $('#AddPostForm');

    postForm.submit(function (e) {
        e.preventDefault();
        AddPost(postForm, $(this).parent().parent().parent().parent());
    })
   
})

function AddPost(form, div) {
        $.ajax({
            type: "POST",
            url: "/Post/Add",
            data: new FormData(form[0]),
            success: function (data) {
                form[0].reset();
                angular.element($("#GroneAppController")).scope().GetPosts();
                angular.element($("#GroneAppController")).scope().$apply();
                div.modal('hide');
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
        
        $.ajax({
            type: "POST",
            url: "/Comment/AddComment",
            data: new FormData(form[0]),
            success: function (data) {
                form[0].reset();
                angular.element($("#GroneAppController")).scope().GetPosts();
                angular.element($("#GroneAppController")).scope().$apply();
                div.modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
            },

            error: function (e) {
                console.log('no');
            },
            processData: false,
            contentType: false
        });
    })

}
