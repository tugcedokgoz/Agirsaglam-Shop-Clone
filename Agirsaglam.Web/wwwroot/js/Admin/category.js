function GetCategory() {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/api/Category/GetCategories`,
        /*  dataType: "application/json; charset=utf-8",*/

        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                    <th>Id</th>
                    <th>Category Name</th>
                    <th>Parent Category</th>
                    <th>Status</th>
 
               
                    </tr>`;


                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].id}</td>`;
                    html += `<td>${arr[i].name}</td>`;
                    html += `<td>${arr[i].parentCategory}</td>`;
                    html += `<td>${arr[i].status}</td>`;

                    //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
                    html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteProduct(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divCategories").html(html);
            }
            else {

                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}
$(document).ready(function () {
    GetCategory();
});