function ToggleCommentSummary(id) {
    if (id.length > 10) {
        $('.listOfComments', '#' + id).slideToggle();
    }
}