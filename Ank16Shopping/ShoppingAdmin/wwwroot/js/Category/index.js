function DeleteItem(id) {

    var aTag = $("#remove-link-" + id);
    var removeUrl = aTag.data("url");

    window.location = removeUrl;
}