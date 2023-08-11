function GetUsers() {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/User/GetUsers`,
        /*  dataType: "application/json; charset=utf-8",*/

        success: function (response) {
            if (response.success) {
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

                var arr = response.data;

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
