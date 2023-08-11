function GetProducts() {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/Product/GetProducts`,
        /*  dataType: "application/json; charset=utf-8",*/

        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                    <th>Id</th>
                    <th>Product Name</th>
                    <th>Product Price</th>
                    <th>Product Discount Price</th>
                    <th>Product Amount</th>
                    <th>Product Description</th>
                    <th>Product Image</th>
               
                    </tr>`;
           

                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td>`;
                    html += `<td>${arr[i].name}</td>`;
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
            }
            else {
                alert(response.message);
                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}


function SaveProduct() {
    var product = {
        Id: 0,
        Name: $("#inputProductName").val(),
        Price: $("#inputPrice").val(),
        DiscountPrice: $("#inputDiscountPrice").val(),
        Amount: $("#inputAmount").val(),
        Description: $("#inputDescription").val(),
        Image: $("#inputImage").val(),
    };

    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/api/Product/Save`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(product),
        success: function (response) {
            if (response.success) {

                GetProducts();
                $("#productModal").modal("hide"); // Modalı kapatma işlemi
            } else {
                // Başarısız yanıt durumunda kullanıcıya hata mesajı gösterilebilir.
                alert(response.message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // Hata durumunda kullanıcıya detaylı hata bilgisi gösterilebilir.
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function DeleteProduct(id) {
    if (confirm("Kaydı silmek istediğinizden emin misiniz?")) {
        $.ajax({
            type: "POST",
            url: `${BASE_API_URI}/api/Product/Delete?id=${id}`,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    GetProducts();

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
}


function GetProductsByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/Product/GetProductsByName?name=${name}`, // name parametresini ekliyoruz
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                    <th>Id</th>
                    <th>Product Name</th>
                    <th>Product Price</th>
                    <th>Product Discount Price</th>
                    <th>Product Amount</th>
                    <th>Product Description</th>
                    <th>Product Image</th>
               
                    </tr>`;


                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td>`;
                    html += `<td>${arr[i].name}</td>`;
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

function SetProductIdforEditModal(id) {
    $("#EditProductId").val(parseInt(id))
}
function UpdateProduct() {
    var product = {
        Id: $("#EditProductId").val(),
        Name: $("#inputEditProductName").val(),
        Price: $("#inputEditPrice").val(),
        DiscountPrice: $("#inputEditDiscountPrice").val(),
        Amount: $("#inputEditAmount").val(),
        Description: $("#inputEditDescription").val(),
        Image: $("#inputEditImage").val(),
    };
    $.ajax({
        type: "POST",
        url: `${BASE_API_URI}/api/Product/Save`,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(product),
        success: function (response) {
            if (response.success) {
                GetProducts();
                $("#productEditModal").modal("hide");
            } else {
                console.log(response)
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}



$(document).ready(function () {
    GetProducts();
    $("#productForm").submit(function (event) {
        event.preventDefault();
        SaveProduct();
    });
    $("#searchButton").click(function () {
        var productName = $("#productNameInput").val(); // roleName değişkeni olarak alınacak
        if (productName !== "") {
            GetProductsByName(productName);
        } else {
            GetProducts();
        }
    });
    $("#productEditForm").submit(function (event) {
        event.preventDefault();
        UpdateProduct();
    });
});