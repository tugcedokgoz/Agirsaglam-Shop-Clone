let products = [];
function GetProduct() {
    Get("Product/GetAllProducts", (data) => {
        var arr = data
        $('#inputCategoryName').empty();

        $.each(arr, function (i, item) {
            $('#inputCategoryName').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
        $.each(arr, function (i, item) {
            $('#inputEditProductName').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });
}
function GetAdminProduct() {

    Get("Product/GetAdminProducts", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>

                    <th>Product Name</th>
                    <th>Category Name</th>
                    <th>Product Price</th>
                    <th>Product Discount Price</th>
                    <th>Product Amount</th>
                    <th>Product Description</th>
                    <th>Product Image</th>
               
                    </tr>`;


        var arr = data; // Burada arr yerine data kullanılmalı
        console.log(data)

        for (var i = 0; i < arr.length; i++) {

            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].categoryName}</td>`;
            html += `<td>${arr[i].price}</td>`;
            html += `<td>${arr[i].discountPrice}</td>`;
            html += `<td>${arr[i].amount}</td>`;
            html += `<td>${arr[i].description}</td>`;
            html += `<td>${arr[i].image}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteProduct(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;


        $("#divProducts").html(html);
    });

}


function SaveProduct() {

    var product = {
        Id: 0,
        Name: $("#inputProductName").val(),
        CategoryId: $("#inputCategoryName").val(),
        Price: $("#inputPrice").val(),
        DiscountPrice: $("#inputDiscountPrice").val(),
        Amount: $("#inputAmount").val(),
        Description: $("#inputDescription").val(),
        Image: $("#inputImage").val(),
    };

    Post("Product/Save", product, (data) => {

        GetAdminProduct();
        $("#productModal").modal("hide");
    });
}



function DeleteProduct(id) {
    Delete(`Product/Delete?id=${id}`, (data) => {
        GetAdminProduct();
    });
}


function SetProductIdforEditModal(id) {
    GetProduct();
    $("#EditProductId").val(parseInt(id))



}
function UpdateProduct() {

    var category = {
        Id: $("#EditProductId").val(),
        ProductName: $("#inputEditProductName").val(),
        CategoryName: $("#inputEditParentCategoryName").val(),
        Price: $("#inputEditPrice").val(),
        DiscountPrice: $("#inputEditDiscountPrice").val(),
        Amount: $("#inputEditAmount").val(),
        Description: $("#inputEditDescription").val(),
        Image: $("#inputEditImage").val(),
 
    };

    Post("Product/Save", category, (data) => {
        GetAdminProduct();
        $("#productEditModal").modal("hide");
    });
}



function GetProductByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Product/GetProductsByName?name=${name}`,
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                        <th>Product Name</th>
                        <th>Category Name</th>
                        <th>Product Price</th>
                        <th>Product Discount Price</th>
                        <th>Product Amount</th>
                        <th>Product Description</th>
                        <th>Product Image</th>
                    </tr>`;

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].name}</td>`;
                    html += `<td>${arr[i].categoryName}</td>`;
                    html += `<td>${arr[i].price}</td>`;
                    html += `<td>${arr[i].discountPrice}</td>`;
                    html += `<td>${arr[i].amount}</td>`;
                    html += `<td>${arr[i].description}</td>`;
                    html += `<td>${arr[i].image}</td>`;
                    html += `<td class="d-flex flex-row ">
                                 <button type="button" class="btn btn-danger btn-sm m-2" onclick='DeleteProduct(${arr[i].id})'>Delete</button>
                                 <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`;
                }
                html += `</table>`;

                $("#divProducts").html(html);
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
    GetAdminProduct();
    GetProduct();
    $("#productForm").submit(function (event) {
        event.preventDefault(); // Form gönderimini engelle

        SaveProduct(); // SaveCategory fonksiyonunu burada çağırın
    });
    $("#productEditForm").submit(function (event) {
        event.preventDefault();
        UpdateProduct();
    });
    $("#searchButton").click(function () {
        var productName = $("#productIdInput").val();
        if (productName !== "") {
            GetProductByName(productName);
        } else {
            GetAdminProduct();
        }
    });
});