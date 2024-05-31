function DeleteItem(id) {

    var aTag = $("#remove-link-" + id);
    var removeUrl = aTag.data("url");

    window.location = removeUrl;
}

function GetImage() {
    $.ajax({
        url: "https://localhost:7035/api/Products/1/Picture",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization","Basic YWRtaW5AbWFpbC5jb206QXoqMTIzNDU2")
        },
        success: function (data) {
            //console.log(data);
            $("#product-img-1").attr("src", "data:Image/*;base64," + data);
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}