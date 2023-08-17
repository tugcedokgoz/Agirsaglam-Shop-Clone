let comments = [];
function GetComments() {
    Get("Comment/GetComment", (data) => {
        var arr = data

    });
}
function GetAdminComment() {

    Get("Comment/GetAdminComments", (data) => {
        var html = `<table class="table table-hover">` +
            `<tr>

                    <th>ProductName</th>
                    <th>Explanation</th>
                    <th>UserName</th>
                    <th>Date</th>
                    <th>Point</th>
                    <th>Answer</th>
                    <th>Status</th>
                    <th>Status Date</th>
                 
             
               
                    </tr>`;


        var arr = data; // Burada arr yerine data kullanılmalı
        console.log(data)

        for (var i = 0; i < arr.length; i++) {

            html += `<td>${arr[i].productName}</td>`;
            html += `<td>${arr[i].explanation}</td>`;
            html += `<td>${arr[i].userName}</td>`;
            html += `<td>${arr[i].date}</td>`;
            html += `<td>${arr[i].point}</td>`;
            html += `<td>${arr[i].answer}</td>`;
            html += `<td>${arr[i].status}</td>`;
            html += `<td>${arr[i].statusDate}</td>`;

            //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
            html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteComment(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
            html += `</tr>`
        }
        html += `</table>`;


        $("#divComment").html(html);
    });

}

function DeleteComment(id) {
    Delete(`Comment/Delete?id=${id}`, (data) => {
        GetAdminComment();
    });
}

function GetCommentByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Comment/GetCommentByName?name=${name}`, // name parametresini ekliyoruz
        success: function (response) {
            if (response.success) {
                var html = `<table class="table table-hover">` +
                    `<tr>

                    <th>ProductName</th>
                    <th>Explanation</th>
                    <th>UserName</th>
                    <th>Date</th>
                    <th>Point</th>
                    <th>Answer</th>
                    <th>Status</th>
                    <th>Status Date</th>
                 
             
               
                    </tr>`;


                var arr = response.data;

                for (var i = 0; i < arr.length; i++) {

                    html += `<td>${arr[i].productName}</td>`;
                    html += `<td>${arr[i].explanation}</td>`;
                    html += `<td>${arr[i].userName}</td>`;
                    html += `<td>${arr[i].date}</td>`;
                    html += `<td>${arr[i].point}</td>`;
                    html += `<td>${arr[i].answer}</td>`;
                    html += `<td>${arr[i].status}</td>`;
                    html += `<td>${arr[i].statusDate}</td>`;

                    //html += `<td><i class="fa fa-trash text-danger" onclick='DeleteRole(${arr[i].id})'></i><i class="fa-pencil-square" onclick='EditRole(${arr[i]})'></i></td>`;
                    html += `<td class="d-flex flex-row ">
                    
                                     <button type="button" class="btn btn-danger btn-sm m-2"  onclick='DeleteComment(${arr[i].id})'>Delete</button>
                                    
                                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#productEditModal" onclick='SetProductIdforEditModal(${arr[i].id})'>Edit</button>
                                    
                             </td>`;
                    html += `</tr>`
                }
                html += `</table>`;


                $("#divComment").html(html);
               
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
    GetAdminComment();
    GetComments();
    $("#searchButton").click(function () {
        var commentName = $("#commentIdInput").val();
        if (commentName !== "") {
            GetCommentByName(commentName);
        } else {
            GetAdminComment();
        }
    });
});