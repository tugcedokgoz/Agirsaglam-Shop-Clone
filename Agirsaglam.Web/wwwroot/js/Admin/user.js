function GetUsers() {

    Get("User/GetUsers", (data) => {
        var table = $("<table>").addClass("table table-hover");
        var thead = $("<thead>").appendTo(table);
        var tr = $("<tr>").appendTo(thead);
        $("<th>").text("Id").appendTo(tr);
        $("<th>").text("User Name").appendTo(tr);
        $("<th>").text("Email").appendTo(tr);
        $("<th>").text("Create Date").appendTo(tr);
        $("<th>").text("Update Date").appendTo(tr);
        $("<th>").text("Email Confirm").appendTo(tr);
        $("<th>").text("Status").appendTo(tr);
        $("<th>").text("Role Id").appendTo(tr);
        $("<th>").text("Adress Id").appendTo(tr);


        var tbody = $("<tbody>").appendTo(table);

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            var row = $("<tr>").appendTo(tbody);
            $("<td>").text(arr[i].id).appendTo(row);
            $("<td>").text(arr[i].userName).appendTo(row);
            $("<td>").text(arr[i].email).appendTo(row);
            $("<td>").text(arr[i].createDate).appendTo(row);
            $("<td>").text(arr[i].updateDate).appendTo(row);
            $("<td>").text(arr[i].emailConfirm).appendTo(row);
            $("<td>").text(arr[i].status).appendTo(row);
            $("<td>").text(arr[i].roleId).appendTo(row);
            $("<td>").text(arr[i].adressId).appendTo(row);

            var actionsCell = $("<td>").addClass("d-flex flex-row").appendTo(row);
            $("<button>").addClass("btn btn-danger btn-sm m-2").text("Delete").click(function () {
                DeleteProduct(arr[i].id);
            }).appendTo(actionsCell);
            $("<button>").addClass("btn btn-warning btn-sm m-2").text("Edit").attr("data-bs-toggle", "modal").attr("data-bs-target", "#productEditModal").click(function () {
                SetProductIdforEditModal(arr[i].id);
            }).appendTo(actionsCell);
        }

        $("#divUsers").empty().append(table);
    });

}

function GetUsersByName(userName) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/User/GetUsersByName?userName=${userName}`, // name parametresini ekliyoruz
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                    <th>Id</th>
                    <th>User Name</th>
                    <th>Email</th>
                    <th>Password</th>
                    <th>Create Date</th>
                    <th>Update Date</th>
                    <th>Email Confirm</th>
                    <th>Email Confirm Date</th>
                    <th>Status</th>
               
                    </tr>`;


                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td>`;
                    html += `<td>${arr[i].userName}</td>`;
                    html += `<td>${arr[i].email}</td>`;
                    html += `<td>${arr[i].password}</td>`;
                    html += `<td>${arr[i].createDate}</td>`;
                    html += `<td>${arr[i].updateDate}</td>`;
                    html += `<td>${arr[i].emailConfirm}</td>`;
                    html += `<td>${arr[i].emailConfirmDate}</td>`;
                    html += `<td>${arr[i].status}</td>`;
                    //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
                    html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteProduct(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divUsers").html(html);
            }
            else {
                alert(response.message);
                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

$(document).ready(function () {
    GetUsers();
    $("#searchButton").click(function () {
        var userName = $("#userNameInput").val();
        if (userName !== "") {
            GetUsersByName(userName);
        } else {
            GetUsers();
        }
    });

});
