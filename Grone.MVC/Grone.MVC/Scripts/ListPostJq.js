/// <reference path="jquery-3.1.1.js" />

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


    div.slideToggle();
    div.toggleClass("full");


    //if (typeof id !== 'undefined') {
    //    div.slideToggle();
    //    div.toggleClass("full");
    //}
}

function ScrollAndTogglePost(id) {
    var div = $('.listOfComments', '#' + id);

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
    }, 700);
}

function FileinputIsOk() {
    var fileInput = $('#photoUpload');
    var maxSize = fileInput.data('max-size');

    if (fileInput.get(0).files.length) {
        var fileSize = fileInput.get(0).files[0].size; // in bytes
        if (fileSize > maxSize) {
            //alert('file size is more then' + maxSize + ' bytes');
            return false;
        } else {
            //alert('file size is correct- ' + fileSize + ' bytes');
            return true;
        }
    }
    else
        return true;
    //else {
    //    //alert('choose file, please');
    //    return false;
    //}
}

$(document).ready(function () {

    var postForm = $('#AddPostForm');

    postForm.submit(function (e) {
        e.preventDefault();
        if (FileinputIsOk()) {
            AddPost(postForm, $(this).parent().parent().parent().parent());
        }
        else {
            $("#AddPostForm #uploadImageFormDiv :last-child").html('Filesize is larger than 2mb');
            //return false;
            //alert("filesize is more than 2mb");
        }
    })

    var addAdmin = $('#AddAdmin');

    addAdmin.submit(function (e) {
        e.preventDefault();
        AddAdminAjax(addAdmin, $(this).parent().parent().parent().parent());

    });

    var manageAccount = $('#ManageAccount');

    manageAccount.submit(function (e) {
        e.preventDefault();
        ManageAccountAjax(manageAccount, $(this).parent().parent().parent().parent());
    });

})

function ManageAccountAjax(form, div) {
    if (form.valid()) {

        StartPostLoad();

        if (form.attr('disabled') != 'disabled') {
            form.attr('disabled', true);

            $.ajax({
                type: "POST",
                url: "/Auth/Update",
                data: new FormData(form[0]),
                success: function (data) {
                    setTimeout(function () {
                        form[0].reset();
                        StopPostLoad();
                        div.modal('hide');
                        form.removeAttr('disabled');
                        $('#nameInputManage').val(data);
                    }, 1000);
                },

                error: function (e) {
                    console.log('no');
                },
                processData: false,
                contentType: false
            });
        }

    }
}


function AddAdminAjax(form, div) {
 if (form.valid()) {

        StartPostLoad();

        if (form.attr('disabled') != 'disabled') {
            form.attr('disabled', true);

            $.ajax({
                type: "POST",
                url: "/Auth/Add",
                data: new FormData(form[0]),
                success: function (data) {
                    setTimeout(function () {
                        form[0].reset();
                        StopPostLoad();
                        div.modal('hide');
                        form.removeAttr('disabled')
                        console.log('done')
                    }, 1000);
                },

                error: function (e) {
                    console.log('no');
                },
                processData: false,
                contentType: false
            });
        }

    }
}

function AddPost(form, div) {
    if (form.valid()) {

        StartPostLoad();

        if (form.attr('disabled') != 'disabled') {
            form.attr('disabled', true);

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
                        form.removeAttr('disabled')

                        $("#AddPostForm #uploadImageFormDiv :last-child").html('');
                    }, 1000);
                },

                error: function (e) {
                    console.log('no');
                },
                processData: false,
                contentType: false
            });
        }

    }
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
    $.validator.unobtrusive.parse(form);

    form.submit(function (e) {
        e.preventDefault();

        if (form.valid()) {
            StartCommentLoad();

            if (form.attr('disabled') != 'disabled') {
                form.attr('disabled', true);

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
                            form.attr('disabled', false);
                            form.removeAttr('disabled');
                        }, 1000);
                    },

                    error: function (e) {
                        console.log('no');
                    },
                    processData: false,
                    contentType: false
                });
            }
        }
    })

}

function StartBigLoad() {
    $('#postContent').hide();

    $('#postContent').fadeOut("slow", function () {
        $('#bigLoaderContent').fadeIn("slow");

    });
}

function StopBigLoad() {

    $('#bigLoaderContent').hide();
    $('#postContent').fadeIn();


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
    var loader = $($($(div.children()[0]).children()[5]).children()[0]);
    loader.fadeIn("slow");

}

function StopGetAllCommentLoad(id) {
    var div = $('#' + id);
    var loader = $($($(div.children()[0]).children()[5]).children()[0]);
    loader.fadeOut("slow");

}

function copyToClipboard(elem) {
    // create hidden text element, if it doesn't already exist
    var targetId = "_hiddenCopyText_";
    var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
    var origSelectionStart, origSelectionEnd;
    if (isInput) {
        // can just use the original source element for the selection and copy
        target = elem;
        origSelectionStart = elem.selectionStart;
        origSelectionEnd = elem.selectionEnd;
    } else {
        // must use a temporary form element for the selection and copy
        target = document.getElementById(targetId);
        if (!target) {
            var target = document.createElement("textarea");
            target.style.position = "absolute";
            target.style.left = "-9999px";
            target.style.top = "0";
            target.id = targetId;
            document.body.appendChild(target);
        }
        target.textContent = elem.textContent;
    }
    // select the content
    var currentFocus = document.activeElement;
    target.focus();
    target.setSelectionRange(0, target.value.length);

    // copy the selection
    var succeed;
    try {
        succeed = document.execCommand("copy");
    } catch (e) {
        succeed = false;
    }
    // restore original focus
    if (currentFocus && typeof currentFocus.focus === "function") {
        currentFocus.focus();
    }

    if (isInput) {
        // restore prior selection
        elem.setSelectionRange(origSelectionStart, origSelectionEnd);
    } else {
        // clear temporary content
        target.textContent = "";
    }
    return succeed;
}
