function ToggleCommentSummary(id) {
    console.log(id);
    if (id.length > 10) {
        $('.listOfComments', '#' + id).slideToggle();
    }
}