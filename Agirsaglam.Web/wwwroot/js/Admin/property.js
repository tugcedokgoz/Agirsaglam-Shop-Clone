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
        $.each(uniqueGroupNames, function (i, groupName) {
            $('#inputEditPropertyGroupName').append($('<option>', {
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
                     <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#propertyEditModal" onclick='SetPropertyIdforEditModal(${arr[i].id})'>Edit</button>
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
        GroupId: arr.find(property => property.groupName === $("#inputPropertyGroupName").val()).groupId
    };

    Post("Property/Save", property, (data) => {
        if (data.success) {
            GetAllPropertiesForProperty();
            $("#propertyModal").modal("hide");

        } else {
            alert("Property could not be saved.");
        }
    });
}

function SetPropertyIdforEditModal(id) {
    GetAllProperty();
    $("#EditpropertyId").val(parseInt(id))



}
function UpdateProperty() {

    var property = {
        Id: $("#EditpropertyId").val(),
        Name: $("#inputEditpropertyName").val(),
        GroupId: arr.find(property => property.groupName === $("#inputEditPropertyGroupName").val()).groupId
    };

    Post("Property/Save", property,  (data) => {

        if (data.success) {
            GetAllPropertiesForProperty();
            $("#propertyModal").modal("hide");

        } else {
            alert("Property could not be saved.");
        }
    });
}

function GetPropertyByName(name) {
    $.ajax({
        type: "GET",
        url: `${BASE_API_URI}/Property/GetPropertyByName/${name}`,
        success: function (response) {
            console.log(response)
            if (response.success) {
                var arr = response.data;
                var html = generatePropertyTableHtml(arr);
                $("#divProperty").html(html);
            } else {
                console.log(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest + "-" + textStatus + "-" + errorThrown);
        }
    });
}

function generatePropertyTableHtml(properties) {
    var html = `<table class="table table-hover">
        <tr>
            <th>Property Name</th>
            <th>Group Name</th>
            <th>Actions</th>
        </tr>`;

    for (var i = 0; i < properties.length; i++) {
        html += `<tr>
                    <td>${properties[i].name}</td>
                    <td>${properties[i].groupName}</td>
                    <td>
                        <button type="button" class="btn btn-danger btn-sm m-2" onclick='DeleteProperty(${properties[i].id})'>Delete</button>
                        <button type="button" class="btn btn-warning btn-sm m-2" data-bs-toggle="modal" data-bs-target="#propertyEditModal" onclick='SetPropertyIdForEditModal(${properties[i].id})'>Edit</button>
                    </td>
                 </tr>`;
    }

    html += `</table>`;
    return html;
}


function DeleteProperty(id) {
    Delete(`Property/Delete?id=${id}`, (data) => {
        GetAllPropertiesForProperty();
    });
};
$(document).ready(function () {
    GetAllPropertiesForProperty();

    $("#propertyForm").submit(function (event) {
        event.preventDefault();
        SaveProperty();
    });

    $("#propertyEditForm").submit(function (event) {
        event.preventDefault();
        UpdateProperty();
    });

    $("#searchButton").click(function () {
        var propertyName = $("#propertyIdInput").val().trim();
        if (propertyName !== "") {
            GetPropertyByName(propertyName);
        } else {
            GetAllProperty();
        }
    });
});
