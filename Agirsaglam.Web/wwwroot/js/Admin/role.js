﻿function GetRoles() {
    Get("Role/GetRoles", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr><th style="width:50px"Id></th><th>Role Name</th><th></th></tr>`;
        var arr = data;

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
    });
}


function SaveRole() {
    var role = {
        Id: 0,
        Name: $("#inputRoleName").val()
    };

    Post("Role/Save", role, (data) => {
        GetRoles();
        $("#roleModal").modal("hide");
    });
}

function DeleteRole(id) {
    Delete(`Role/Delete?id=${id}`, (data) => {
        GetRoles();
    });
};


function SetRoleIdforEditModal(id) {
    $("#EditRoleId").val(parseInt(id))
}
function UpdateRole() {
    var role = {
        Id: $("#EditRoleId").val(),
        Name: $("#inputEditRoleName").val()
    };

    Post("Role/Save", role, (data) => {
        GetRoles();
        $("#roleEditModal").modal("hide");
    });
}

function GetRolesByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Role/GetRolesByName?name=${name}`, // name parametresini ekliyoruz
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
        console.log(roleName)
        if (roleName !== "") {
            GetRolesByName(roleName);
        } else {
            GetRoles();
        }
    });
});
