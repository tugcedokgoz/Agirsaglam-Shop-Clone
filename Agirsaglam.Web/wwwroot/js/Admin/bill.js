function GetBills() {
    Get("Order/GetOrder", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
            
            </th><th>User Name</th>
            </th><th>User Surname</th>
            </th><th>User Email</th>
            </th><th>User Phone</th>
            </th><th>User Tc</th>
            </th><th>User City</th>
            </th><th>User District</th>
            </th><th>User PostCode</th>
             </th><th>Order Amount</th>
            </th><th>Product Price</th>
            </th><th>Product Name</th>
            <th></th>
            </tr>`;
        var arr = data;
        console.log(data)

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].bill.name}</td>`;
            html += `<td>${arr[i].bill.surname}</td>`;
            html += `<td>${arr[i].bill.email}</td>`;
            html += `<td>${arr[i].bill.phoneNo}</td>`;
            html += `<td>${arr[i].bill.tcNo}</td>`;
            html += `<td>${arr[i].bill.adress.city}</td>`;
            html += `<td>${arr[i].bill.adress.district}</td>`;
            html += `<td>${arr[i].bill.adress.postCode}</td>`;
            html += `<td>${arr[i].orderAmount}</td>`;
            html += `<td>${arr[i].product.price * arr[i].orderAmount}</td>`;
            html += `<td>${arr[i].product.name}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteOrder(${arr[i].id})'>Delete</button>
                                     &nbsp;

                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divBill").html(html);
    });
}

function GetBillsByUserId(userId) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Bill/GetBillsByUserId/${userId}`, 
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
            <th>User Id</th>
            <th>User Name</th>
            <th>User Surname</th>
            <th>User Email</th>
            <th>User Phone</th>
            <th>User TC</th>
            <th>User Adress city</th>
            <th>User Adress district</th>
            <th>User Adress postcode</th>
                    </tr>`;

                var arr = response.data;



                for (var i = 0; i < arr.length; i++) {
                    html += `<tr>`;
                    html += `<td>${arr[i].userId}</td>`;
                    html += `<td>${arr[i].name}</td>`;
                    html += `<td>${arr[i].surname}</td>`
                    html += `<td>${arr[i].email}</td>`;
                    html += `<td>${arr[i].phoneNo}</td>`;
                    html += `<td>${arr[i].tcNo}</td>`;
                    html += `<td>${arr[i].adress.city}</td>`;
                    html += `<td>${arr[i].adress.district}</td>`;
                    html += `<td>${arr[i].adress.postCode}</td>`;
                    html += `<td>
                                 <button type="button" class="btn btn-danger" onclick='DeleteRole(${arr[i].id})'>Delete</button>
                                 &nbsp;
                                 <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#roleEditModal" onclick='SetRoleIdforEditModal(${arr[i].id})'>Edit</button>
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;
                $("#divBill").html(html);
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
    GetBills();

    $("#searchButton").click(function () {
        var userNo = $("#billIdInput").val();
        console.log(userNo)
        if (userNo !== "") {
            GetBillsByUserId(userNo);
        } else {
            GetBills();
        }
    });
});
