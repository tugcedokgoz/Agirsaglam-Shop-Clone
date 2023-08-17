let categories = [];
function GetCategory() {
    Get("Category/GetAllCategory", (data) => {
        var arr = data
        $('#inputParentCategoryName').empty();

        $.each(arr, function(i, item){
            $('#inputParentCategoryName').append($('<option>',{
                value:item.id,
                text:item.name
            }));
        });
        $.each(arr, function (i, item) {
            $('#inputEditParentCategoryName').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });
}
function GetAdminCategory() {

    Get("Category/CategoryAdminLists", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
                 
                    <th>Category Name</th>
                    <th>Parent Category</th>
                    <th>Status</th>         
            </tr>`;

        var arr = data;
        categories = arr;
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].parentCategoryName}</td>`;
            html += `<td>${arr[i].status}</td>`;

            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteCategory(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#categoryEditModal" onclick='SetCategoryIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;

        $("#divCategory").html(html);
    });

}

function SaveCategory() {
    var status = $("#toggleSwitch").prop("checked") ? 1 : 0;

    var category = {
        Id: 0,
        Name: $("#inputCategoryName").val(),
        ParentCategoryId: $("#inputParentCategoryName").val(),
        Status: status
    };

    Post("Category/Save", category, (data) => {

        GetAdminCategory();
        $("#categoryModal").modal("hide");
    });
}

function DeleteCategory(id) {
    Delete(`Category/Delete?id=${id}`, (data) => {
        GetAdminCategory();
    });
}

function SetCategoryIdforEditModal(id) {
    GetCategory();
    $("#EditCategoryId").val(parseInt(id))



}
function UpdateCategory() {
    var status = $("#toggleSwitch").prop("checked") ? 1 : 0;
    var category = {
        Id: $("#EditCategoryId").val(),
        Name: $("#inputEditCategoryName").val(),
        ParentCategoryId: parseInt($("#inputEditParentCategoryName").val()),
        Status: status
    };

    Post("Category/Save", category, (data) => {
        GetAdminCategory();
        $("#categoryEditModal").modal("hide");
    });
}

function GetCategoryByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Category/GetCategoryByName?name=${name}`, // name parametresini ekliyoruz
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                 
                    <th>Category Name</th>
                    <th>Parent Category</th>
                    <th>Status</th>         
            </tr>`;

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].name}</td>`;
                    html += `<td>${arr[i].parentCategoryName}</td>`;
                    html += `<td>${arr[i].status}</td>`;
                    html += `<td>
                                 <button type="button" class="btn btn-danger" onclick='DeleteRole(${arr[i].id})'>Delete</button>
                                 &nbsp;
                                 <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#roleEditModal" onclick='SetRoleIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divCategory").html(html);
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
    GetAdminCategory();
    GetCategory();
    $("#categoryForm").submit(function (event) {
        event.preventDefault(); // Form gönderimini engelle

        SaveCategory(); // SaveCategory fonksiyonunu burada çağırın
    });
    $("#categoryEditForm").submit(function (event) {
        event.preventDefault();
        UpdateCategory();
    });
    $("#searchButton").click(function () {
        var categoryName = $("#categoryIdInput").val();
        if (categoryName !== "") {
            GetCategoryByName(categoryName);
        } else {
            GetAdminCategory();
        }
    });
});