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

function SaveCategory(e) {
    // 1. Form elemanlarını alacağız.
    var name = $("#Name").val();
    var desc = $("#Description").val();

    // 1.1 Form Elemanları Validation Yapılır.

    console.log({ name: name, description: desc, appUserId: 1 });
    console.log(typeof ({ name: name, description: desc, appUserId: 1 }));

    console.log(JSON.stringify({ name: name, description: desc, appUserId: 1 }));
    console.log(typeof (JSON.stringify({ name: name, description: desc, appUserId: 1 })));

    var isValid = true;

    if (name == "") {
        // Name Zorunlu
        $("span[data-valmsg-for='Name']").html($("#Name").data("valRequired"));
        $("span[data-valmsg-for='Name']").show();

        isValid = false;
    }

    if (desc == "") {
        // Desc Zorunlu
        $("span[data-valmsg-for='Description']").html($("#Description").data("valRequired"));
        $("span[data-valmsg-for='Description']").show();

        isValid = false;
    }


    // 2. Ajax Tanımı ve data yollanması

    if (isValid) {
        $.ajax({
            url: "https://localhost:7035/api/Categories",
            method: "POST",
            data: JSON.stringify({ name: name, description: desc, appUserId: 1 }),
            dataType: "application/json",
            contentType:"application/json",
            beforeSend: function (xhr) {
                //xhr.setRequestHeader("userName","admin");
                //xhr.setRequestHeader("password","1234567");
                xhr.setRequestHeader("yetkiKodu", "YWRtaW5AMTIzNDU2Nw==");
                $(e).hide();
                $("#loading").html('<div class="spinner-border spinner-border-sm" role="status"><span class= "visually-hidden"> Loading...</span></div>');
            },
            success: function (data, textStatus, xhr) {
                console.log(data);
            },
            error: function (xhr, textStatus, errorThrown) {

                console.log(xhr);

                if (xhr.status == 201) {
                    console.log("Kayıt başarıyla tamamlandı.");
                }

            },
            complete: function () {
                $("#loading").html('');
                $(e).show();
            }
        });
    }

}