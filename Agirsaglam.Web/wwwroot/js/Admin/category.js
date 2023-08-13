let categories = [];
function GetCategory() {
    Get("Category/GetAllCategory", (data) => {
        var arr = data;
        $('#selectParentCategory').empty();
        $.each(arr, function(i, item){
            $('#selectParentCategory').append($('<option>',{
                value:item.id,
                text:item.name
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
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#categoryEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;

        $("#divCategory").html(html);
    });

}



$(document).ready(function () {
    GetAdminCategory();
    GetCategory();
});