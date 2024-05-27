function GetCategories() {
    $.ajax({
        url: "https://localhost:7035/api/Categories",
        success: function (data) {
            console.log(data);
        }
    });
}

function PostCategories() {
    $.ajax({
        url: "https://localhost:7035/api/Categories/Post",
        method: "POST",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            console.log(data);
        },
    });
}

function PutCategories() {
    $.ajax({
        url: "https://localhost:7035/api/Categories/Put",
        method: "PUT",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            console.log(data);
        }
    });
}

function DeleteCategories() {
    $.ajax({
        url: "https://localhost:7035/api/Categories/Delete",
        method: "DELETE",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            console.log(data);
        },
    });
}