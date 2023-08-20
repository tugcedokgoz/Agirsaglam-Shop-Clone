let uniqueCatgeoryNames = [];
let uniqueGroupNames = [];

function GetCategoryProperty() {
    Get("CategoryProperty/GetAll", (data) => {
        arr = data;
        uniqueCatgeoryNames = [...new Set(arr.map(categoryProperty => categoryProperty.categoryName))];
        uniqueGroupNames = [...new Set(arr.map(categoryProperty => categoryProperty.groupName))];
        GetAllCategoryProperty();
        $('#inputcategoryName').empty();
        $('#inputGroupName').empty();

        $.each(uniqueCatgeoryNames, function (i, categoryName) {
            $('#inputcategoryName').append($('<option>', {
                value: categoryName,
                text: categoryName
            }));
        });
        $.each(uniqueGroupNames, function (i, groupName) {
            $('#inputGroupName').append($('<option>', {
                value: groupName,
                text: groupName
            }));
        });

        //$.each(uniqueProductNames, function (i, productName) {
        //    $('#inputEditproductName').append($('<option>', {
        //        value: productName,
        //        text: productName
        //    }));
        //});
        //$.each(uniquePropertyNames, function (i, propertyName) {
        //    $('#inputEditPropertyName').append($('<option>', {
        //        value: propertyName,
        //        text: propertyName
        //    }));
        //});
    });
}
function GetAllCategoryProperty() {
    var html = `<table class="table table-hover">` +
        `<tr>

            </th><th>Category Name</th>
            </th><th>Group Name</th>
            <th></th>
            </tr>`;

    for (var i = 0; i < arr.length; i++) {
        html += `<tr>`;
        html += `
            <td>${arr[i].categoryName}</td>
            <td>${arr[i].groupName}</td>`;
        //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
        html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteCategoryProperty(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                     <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#productPropertyEditModal" onclick='SetProductPropertyIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
        html += `</tr>`
    }
    html += `</table>`;
    $("#divCategoryProperty").html(html);
}


function SaveCategoryProperty() {
    var categoryProperty = {
        Id: 0,
        CategoryId: arr.find(categoryProperty => categoryProperty.categoryName == $("#inputcategoryName").val()).categoryId,/*$("#inputProductName").val(),*/
        GroupId: arr.find(categoryProperty => categoryProperty.groupName == $("#inputGroupName").val()).groupId/*$("#inputProductName").val(),*/
        /*  PropertyId: *//*$("#inputPropertyName").val(),*/

    };
    Post("CategoryProperty/Save", categoryProperty, (data) => {
        GetCategoryProperty();
        $("#categoryPropertyModal").modal("hide");
    });
}
function DeleteCategoryProperty(id) {
    Delete(`CategoryProperty/Delete?id=${id}`, () => {
        GetAllCategoryProperty();
    });
}
$(document).ready(function () {
    GetCategoryProperty();

    $("#categoryPropertyForm").submit(function (event) {
        event.preventDefault();
        SaveCategoryProperty();
    });
});