function GetOrders() {
    Get("Order/GetOrder", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>
  
            </th><th>Order No</th>
            </th><th>Order Date</th>
            </th><th>Order Amount</th>
            </th><th>Cargo No</th>
            </th><th>Bill Id</th>
            </th><th>User Name</th>
            </th><th>Prdouct Name</th>
            <th></th>
            </tr>`;
        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr>`;
            html += `<td>${arr[i].orderNo}</td>`;
            html += `<td>${arr[i].orderDate}</td>`;
            html += `<td>${arr[i].orderAmount}</td>`;
            html += `<td>${arr[i].cargoNo}</td>`;
            html += `<td>${arr[i].billId}</td>`;
            html += `<td>${arr[i].user.userName}</td>`;
            html += `<td>${arr[i].product.name}</td>`;
            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td>
                                     <button type="button" class="btn btn-danger"  onclick='DeleteOrder(${arr[i].id})'>Delete</button>
                                     &nbsp;

                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divOrder").html(html);
    });
}

function GetOrderByOrderNo(orderNo) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Order/GetOrderByOrderNo?orderNo=${orderNo}`, 
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>
                  
            </th><th>Order No</th>
            </th><th>Order Date</th>
            </th><th>Order Amount</th>
            </th><th>Cargo No</th>
            </th><th>Bill Id</th>
            </th><th>User Name</th>
            </th><th>Prdouct Name</th>
            <th></th>
                    </tr>`;

                var arr = response.data;
                var a = typeof arr;
                console.log(typeof arr)


            
                    html += `<tr>`;
                    html += `<td>${arr.orderNo}</td>`;
                    html += `<td>${arr.orderDate}</td>`;
                    html += `<td>${arr.orderAmount}</td>`;
                    html += `<td>${arr.cargoNo}</td>`;
                    html += `<td>${arr.billId}</td>`;
                    html += `<td>${arr.user.userName}</td>`;
                    html += `<td>${arr.product.name}</td>`;
                    html += `<td>
                                 <button type="button" class="btn btn-danger" onclick='DeleteRole(${arr.id})'>Delete</button>

                             </td>`;
                    html += `</tr>`
                
                html += `</table>`;
                $("#divOrder").html(html);
            } else {

                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function DeleteOrder(id) {
    Delete(`Order/Delete?id=${id}`, (data) => {
        GetOrders();
    });
};

$(document).ready(function () {
    GetOrders();
    $("#searchButton").click(function () {
        var orderNo = $("#orderIdInput").val(); 
        console.log(orderNo)
        if (orderNo !== "") {
            GetOrderByOrderNo(orderNo);
        } else {
            GetOrders();
        }
    });
});