function GetContact() {
    Get("Contact/GetContact", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            </th><th>Name</th>
            </th><th>Surname</th>
            </th><th>Email</th>
            </th><th>Text</th>
            </th><th>Answer Statıus</th>
            </th><th>Aswer User Id</th>
            </th><th>Text Date</th>
            <th></th>
            </tr>`;
        var arr = data;
        console.log(data)
        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].name}</td>`;
            html += `<td>${arr[i].surname}</td>`;
            html += `<td>${arr[i].email}</td>`;
            html += `<td>${arr[i].text}</td>`;
            html += `<td>${arr[i].answerStatus}</td>`;
            html += `<td>${arr[i].answerUserId}</td>`;
            html += `<td>${arr[i].textDate}</td>`;

            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>

                                     <button type="button" class="btn btn-danger"  onclick='DeleteOrder(${arr[i].id})'>Delete</button>
                                     &nbsp;
                                         <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#categoryEditModal" onclick='SetCategoryIdforEditModal(${arr[i].id})'>Edit</button>

                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divContact").html(html);
    });
}



$(document).ready(function () {
    GetContact();
  

});
