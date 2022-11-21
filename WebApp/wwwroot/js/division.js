let delIdDiv;

$(document).ready(function () {
    $('#dataTableDiv').DataTable({
        ajax: {
            url: 'https://localhost:7042/api/Divisions',
            type: "GET",
            dataSrc: 'data',
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return data.name;
                }
            },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="viewDataDiv(${data.id})">
                              Detail & Edit
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteModal" onclick="getIdDiv(${data.id})">
                              Delete
                            </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'colvis',
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1,]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1,]
                }
            },

        ],
    });
});

function viewDataDiv(id) {
    $.ajax({
        url: `https://localhost:7042/api/Divisions/${id}`,
        type: "GET"
    }).done((res) => {
        let temp = "";

        temp += `
                    <form>
                      <div class="form-group">
                        <label for="formGroupExampleInput">Id</label>
                       <input readonly type="number" class="form-control" id="idDept" placeholder="${res.data.id}" value="${res.data.id}">
                      </div>
                      <div class="form-group">
                        <label for="formGroupExampleInput">Name</label>
                       <input readonly type="text" class="form-control" id="nameDept" placeholder="${res.data.name}" value="${res.data.name}">
                      </div>
                    </form>
                `;

        $("#editData").html(temp);
        $("#modalLabel").html(`Detail`);
    });
}

function editOrDetailDiv() {
    var res = $('#editOrDetail').text();
    if (res == "Edit") {
        $('#idDept').removeAttr('readonly');
        $('#nameDept').removeAttr('readonly');
        $("#editOrDetail").html('Back to Detail');
        $("#modalLabel").html(`Edit`);
        $('#saveBtn').attr('hidden', true);
        $('#saveBtn').removeAttr('hidden');
    }

    if (res == "Back to Detail") {
        $("#editOrDetail").html('Edit');
        $("#modalLabel").html(`Detail`);
        $('#idDept').attr('readonly', true);
        $('#nameDept').attr('readonly', true);
        $('#saveBtn').attr('hidden', true);
    }
}

function saveEditDiv() {
    let data;
    let id = parseInt($('#idDept').val());
    let name = $('#nameDept').val()

    data = {
        "id": id,
        "name": name,
    }

    $.ajax({
        url: 'https://localhost:7042/api/Divisions/',
        type: 'PUT',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function () {
            Swal.fire(
                'Successfully save data',
                'success'
            )
            location.reload();
        }
    });
}

function getIdDiv(id) {
    delIdDiv = id;
}

function deleteDataDiv() {
    $.ajax({
        url: `https://localhost:7042/api/Divisions/?id=${delIdDiv}`,
        type: 'DELETE',
        success: function (data) {
            Swal.fire(
                'Successfully delete data',
                'success'
            )
            location.reload();
        }
    });
}

function addDataDiv() {
    let data;
    let id = 0;
    let name = $('#addNameDept').val()

    data = {
        "id": id,
        "name": name,
    };

    $.ajax({
        url: 'https://localhost:7042/api/Divisions/',
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function () {
            Swal.fire(
                'Successfully added data',
                'success'
            )
            location.reload();
        }
    });
}