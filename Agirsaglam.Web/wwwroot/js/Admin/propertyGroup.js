function GetPropertyGroup() {
    Get("PropertyGroup/GetAll", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            <th style="width:50px"Id>
            </th><th>Property Group Name</th>
            <th></th>
            </tr>`;
        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].id}</td>
            <td>${arr[i].name}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeletePropertyGroup(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                     <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#propertyGroupEditModal" onclick='SetPropertyGroupIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divPropertyGroup").html(html);
    });
}
function SavePropertyGroup() {
    var group = {
        Id: 0,
        Name: $("#inputpropertyGroupName").val()
    };

    Post("PropertyGroup/Save", group, (data) => {
        GetPropertyGroup();
        $("#propertyGroupModal").modal("hide");
    });
}

function DeletePropertyGroup(id) {
    Delete(`PropertyGroup/Delete?id=${id}`, (data) => {
        GetPropertyGroup();
    });
};

function SetPropertyGroupIdforEditModal(id) {
    $("#EditPropertyGroupId").val(parseInt(id))
}
function UpdatePropertyGroup() {
    var group = {
        Id: $("#EditPropertyGroupId").val(),
        Name: $("#inputEditpropertyGroupName").val()
    };

    Post("PropertyGroup/Save", group, (data) => {
        GetPropertyGroup();
        $("#propertyGroupEditModal").modal("hide");
    });
}

function GetGroupByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/PropertyGroup/GetByName?name=${name}`, 
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr><th style="width:50px">Id</th><th>Property Group Name</th><th></th></tr>`;

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td><td>${arr[i].name}</td>`;
                    html += `<td>
                                 <button type="button" class="btn btn-danger" onclick='DeletePropertyGroup(${arr[i].id})'>Delete</button>
                                 &nbsp;
                                    <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#propertyGroupEditModal" onclick='SetPropertyGroupIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divPropertyGroup").html(html);
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
    GetPropertyGroup();

    $("#propertyGroupForm").submit(function (event) {
        event.preventDefault();
        SavePropertyGroup();
    });
    $("#propertyGroupEditForm").submit(function (event) {
        event.preventDefault();
        UpdatePropertyGroup();
    });
    $("#searchButton").click(function () {
        var groupName = $("#propertyGroupIdInput").val();
        console.log(groupName)
        if (groupName !== "") {
            GetGroupByName(groupName);
        } else {
            GetPropertyGroup();
        }
    });
});