function ToggleCommentSummary(id) {
    if (id.length > 10) {
        $('.listOfComments', '#' + id).slideToggle();
    }
}

$(document).ready(function () {
 
    var postForm = $('#AddPostForm');

    postForm.submit(function (e) {
        e.preventDefault();
        AddPost(postForm);
    })

   

})


function AddPost(form) {
        $.ajax({
            type: "POST",
            url: "/Post/Add",
            data: new FormData(form[0]),
            success: function (data) {
                form[0].reset();
                angular.element($("#GroneAppController")).scope().GetPosts();
                angular.element($("#GroneAppController")).scope().$apply();
                $(this).parent().parent().parent().parent().modal('hide')
            },

            error: function (e) {
                console.log('no');
            },
            processData: false,
            contentType: false
        });
}



