function GetAdress() {
    Get("Order/GetOrder", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            </th><th>User Name</th>
            </th><th>User Surname</th>
            </th><th>User City</th>
            </th><th>User District</th>
            </th><th>User PostCode</th>
            <th></th>
            </tr>`;
        var arr = data;
        console.log(data)
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].bill.name}</td>`;
            html += `<td>${arr[i].bill.surname}</td>`;
            html += `<td>${arr[i].bill.adress.city}</td>`;
            html += `<td>${arr[i].bill.adress.district}</td>`;
            html += `<td>${arr[i].bill.adress.postCode}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteOrder(${arr[i].id})'>Delete</button>
                                     &nbsp;

                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divAdress").html(html);
    });
}

function DeleteAdress(id) {
    Delete(`Adress/Delete?id=${id}`, (data) => {
        GetAdress();
    });
};
$(document).ready(function () {
    GetAdress();

   
});
