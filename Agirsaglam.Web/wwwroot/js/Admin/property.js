let properties = [];
let uniqueGroupNames = [];

function GetAllPropertiesForProperty() {
    Get("Property/GetAllProperties", (data) => {
        arr = data;
        uniqueGroupNames = [...new Set(arr.map(property => property.groupName))];
        GetAllProperty();
        $('#inputPropertyGroupName').empty();

        $.each(uniqueGroupNames, function (i, groupName) {
            $('#inputPropertyGroupName').append($('<option>', {
                value: groupName,
                text: groupName
            }));
        });
    });
}

function GetAllProperty() {
    var html = `<table class="table table-hover">` +
        `<tr>
            <th>Property Name</th>
            <th>Group Name</th>
            <th>Actions</th>
        </tr>`;

    for (var i = 0; i < arr.length; i++) {
        html += `<tr>`;
        html += `<td>${arr[i].name}</td>`;
        html += `<td>${arr[i].groupName}</td>`;
        html += `<td>
                     <button type="button" class="btn btn-danger btn-sm m-2" onclick='DeleteProperty(${arr[i].id})'>Delete</button>
                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#propertyEditModal" onclick='SetPropertyIdForEditModal(${arr[i].id})'>Edit</button>
                 </td>`;
        html += `</tr>`;
    }

    html += `</table>`;

    $("#divProperty").html(html);
}



function SaveProperty() {
    var property = {
        Id: 0,
        Name: $("#inputpropertyName").val(),
        GroupId: $("#inputPropertyGroupName").val()
    };

    Post("Property/Save", property, (data) => {
        GetAllPropertiesForProperty();
        $("#propertyModal").modal("hide");
    });
}

$(document).ready(function () {
    GetAllPropertiesForProperty();

    $("#propertyForm").submit(function (event) {
        event.preventDefault();
        SaveProperty();
    });
});
