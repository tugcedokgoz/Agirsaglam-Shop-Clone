var BASE_API_URI = "https://localhost:7119/api"


function Get(action, success) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/${action}`,

        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', `Bearer ${TOKEN}`);
        },

        success: function (response) {
            if (response.success) {

                success(response.data);
            }
            else {

                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

//kaydet-düzenle
function Post(action, data, success) {

    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/${action}`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (response) {
            if (response.success) {
                success(response.data);
            }
            else {
                alert(response.message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function Delete(action, success, ask = true) {
    var confirmed = true;
    if (ask) {
        confirmed = confirm("Kaydı silmek istediğinizden emin misiniz?");
    }
    if (confirmed) {
        $.ajax({
            type: "POST",
            url: `${BASE_API_URI}/${action}`,
            //beforeSend: function (xhr) {
            //    xhr.setRequestHeader('Authorization', `Bearer ${TOKEN}`);
            //},
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    success(response.data);
                }
                else {
                    alert(response.message);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
            }
        });
    }
}


