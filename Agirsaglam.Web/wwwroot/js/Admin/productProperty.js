let uniqueProductNames = [];
let uniquePropertyNames = [];

function GetProductProperty() {
    Get("ProductProperty/GetAll", (data) => {
        arr = data;
        uniqueProductNames = [...new Set(arr.map(productProperty => productProperty.productName))];
        uniquePropertyNames = [...new Set(arr.map(productProperty => productProperty.propertyName))];
        GetAllProductProperty();
        $('#inputProductName').empty();
        $('#inputPropertyName').empty();

        $.each(uniqueProductNames, function (i, productName) {
            $('#inputProductName').append($('<option>', {
                value: productName,
                text: productName
            }));
        });
        $.each(uniquePropertyNames, function (i, propertyName) {
            $('#inputPropertyName').append($('<option>', {
                value: propertyName,
                text: propertyName
            }));
        });

        $.each(uniqueProductNames, function (i, productName) {
            $('#inputEditproductName').append($('<option>', {
                value: productName,
                text: productName
            }));
        });
        $.each(uniquePropertyNames, function (i, propertyName) {
            $('#inputEditPropertyName').append($('<option>', {
                value: propertyName,
                text: propertyName
            }));
        });
    });
}
function GetAllProductProperty() {
    var html = `<table class="table table-hover">` +
        `<tr>

            </th><th>Product Name</th>
            </th><th>Property Name</th>
            <th></th>
            </tr>`;

    for (var i = 0; i < arr.length; i++) {
        html += `<tr>`;
        html += `
            <td>${arr[i].productName}</td>
            <td>${arr[i].propertyName}</td>`;
        //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
        html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteProductProperty(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                     <button type="button" class="btn btn-warning"  data-bs-toggle="modal" data-bs-target="#productPropertyEditModal" onclick='SetProductPropertyIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
        html += `</tr>`
    }
    html += `</table>`;
    $("#divProductProperty").html(html);
}

function SaveProductProperty() {
    var productProperty = {
        Id: 0,
        ProductId: arr.find(productProperty => productProperty.productName == $("#inputProductName").val()).productId,/*$("#inputProductName").val(),*/
        PropertyId: arr.find(productProperty => productProperty.propertyName == $("#inputPropertyName").val()).propertyId/*$("#inputProductName").val(),*/
      /*  PropertyId: *//*$("#inputPropertyName").val(),*/

    };
    Post("ProductProperty/Save", productProperty, (data) => {
        GetProductProperty();
        $("#productPropertyModal").modal("hide");
    });
}

function SetProductPropertyIdforEditModal(id) {
    GetAllProductProperty();
    $("#EditProductpropertyId").val(parseInt(id))




}
function UpdateProductProperty() {

    var productProperty = {
        Id: $("#EditProductpropertyId").val(),
        ProductId: arr.find(productProperty => productProperty.productName == $("#inputProductName").val()).productId,/*$("#inputProductName").val(),*/
        PropertyId: arr.find(productProperty => productProperty.propertyName == $("#inputPropertyName").val()).propertyId/*$("#inputProductName").val(),*/
    };


    Post("ProductProperty/Save", productProperty, (data) => {

        if (data.success) {
            GetProductProperty();
            $("#productPropertyEditModal").modal("hide");

        } else {
            alert("Property could not be saved.");
        }
    });
}
function DeleteProductProperty(id) {
    Delete(`ProductProperty/Delete?id=${id}`, () => {
        GetAllProductProperty();
    });
}



$(document).ready(function () {
    GetProductProperty();
    $("#productPropertyForm").submit(function (event) {
        event.preventDefault();
        SaveProductProperty();
    });

    $("#productPropertyEditForm").submit(function (event) {
        event.preventDefault();
        UpdateProductProperty();
    });
});
