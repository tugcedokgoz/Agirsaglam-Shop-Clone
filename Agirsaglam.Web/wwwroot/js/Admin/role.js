function GetRoles() {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/Role/GetRoles`,
        /*  dataType: "application/json; charset=utf-8",*/

        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr><th style="width:50px"Id></th><th>Role Name</th><th></th></tr>`;

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td><td>${arr[i].name}</td>`;
                    //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
                    html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteRole(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                     <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#roleEditModal" onclick='SetRoleIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divRoles").html(html);
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
function SaveRole() {
    var role = {
        Id: 0,
        Name: $("#inputRoleName").val()
    };

    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/api/Role/Save`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(role),
        success: function (response) {
            if (response.success) {
                // Eğer başarılı bir yanıt dönerse, rolleri yeniden almak veya başka bir işlem yapmak için GetRoles() fonksiyonunu çağırabilirsiniz.
                GetRoles();
                $("#roleModal").modal("hide"); // Modalı kapatma işlemi
            } else {
                // Başarısız yanıt durumunda kullanıcıya hata mesajı gösterilebilir.
                console.log(response)
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // Hata durumunda kullanıcıya detaylı hata bilgisi gösterilebilir.
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function DeleteRole(id) {
    if (confirm("Kaydı silmek istediğinizden emin misiniz?")) {
        $.ajax({
            type: "POST",
            url: `${BASE_API_URI}/api/Role/Delete?id=${id}`,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    GetRoles();

                }
                else {
                    console.log(response)
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
            }
        });
    }
}

function SetRoleIdforEditModal(id) {
    $("#EditRoleId").val(parseInt(id))
}
function UpdateRole() {
    var role = {
        Id: $("#EditRoleId").val(),
        Name: $("#inputEditRoleName").val()
    };
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/api/Role/Save`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(role),
        success: function (response) {
            if (response.success) {
                GetRoles();
                $("#roleEditModal").modal("hide");
            } else {
                console.log(response)
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function GetRolesByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/Role/GetRolesByName?name=${name}`, // name parametresini ekliyoruz
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr><th style="width:50px">Id</th><th>Role Name</th><th></th></tr>`;

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td><td>${arr[i].name}</td>`;
                    html += `<td>
                                 <button type="button" class="btn btn-danger" onclick='DeleteRole(${arr[i].id})'>Delete</button>
                                 &nbsp;
                                 <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#roleEditModal" onclick='SetRoleIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divRoles").html(html);
            } else {
              
                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

$(document).ready(function () {
    GetRoles();
    $("#roleForm").submit(function (event) {
        event.preventDefault();
        SaveRole();
    });
    $("#roleEditForm").submit(function (event) {
        event.preventDefault();
        UpdateRole();
    });
    $("#searchButton").click(function () {
        var roleName = $("#roleIdInput").val(); // roleName değişkeni olarak alınacak
        if (roleName !== "") {
            GetRolesByName(roleName);
        } else {
            GetRoles();
        }
    });
});
