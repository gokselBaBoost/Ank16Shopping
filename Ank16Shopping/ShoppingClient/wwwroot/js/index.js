$.ajax({
    url: "https://localhost:7035/api/Categories",
    success: function (data) {
        console.log(data);
    }
});

function PutCatgory(id) {
    $.ajax({
        url: `https://localhost:7035/api/Categories/${id}`,
        method: "PUT",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ name: "Kategori Güncel", description: "Kategori Açıklama Güncel" }),
        success: function (data) {
            console.log(data);
        }
    });
}

function PostCatgory() {
    $.ajax({
        url: `https://localhost:7035/api/Categories`,
        method: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ name: "Kategori Yeni", description: "Kategori Yeni", appUserId: 1 }),
        success: function (data) {
            console.log(data);
        }
    });
}

function GetCatgory(id) {
    $.ajax({
        url: `https://localhost:7035/api/Categories/${id}`,
        method: "GET",
        success: function (data) {
            console.log(data);
        }
    });
}

function DeleteCatgory(id) {
    $.ajax({
        url: `https://localhost:7035/api/Categories/${id}`,
        method: "DELETE",
        success: function (data) {
            console.log(data);
        }
    });
}