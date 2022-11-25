let delIdDept;

$(document).ready(function () {
    $('#dataTable').DataTable({
        ajax: {
            url: 'https://localhost:7042/api/Departements',
            dataSrc: 'data',
            headers: {
                'Authorization': "Bearer " + sessionStorage.getItem("token")
            },
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
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="viewData(${data.id})">
                              Detail & Edit
                            </button>
                            <button type="button" class="btn btn-danger" onclick="getId(${data.id})">
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

        ]
    });
});

function viewData(id) {
    $.ajax({
        url: `https://localhost:7042/api/Departements/${id}`,
        type: "GET",
        headers: {
            'Authorization': $.cookie('token')
        }
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
                      <div class="form-group">
                        <label for="formGroupExampleInput">Division Id</label>
                       <input readonly type="number" class="form-control" id="divisionDept" placeholder="${res.data.divisionId}" value="${res.data.divisionId}">
                      </div>
                    </form>
                `;

        $("#editData").html(temp);
        $("#modalLabel").html(`Detail`);
    });
}

function editOrDetail() {
    var res = $('#editOrDetail').text();
    if (res == "Edit") {
        $('#idDept').removeAttr('readonly');
        $('#nameDept').removeAttr('readonly');
        $('#divisionDept').removeAttr('readonly');
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
        $('#divisionDept').attr('readonly', true);
        $('#saveBtn').attr('hidden', true);
    }
}

function saveEdit() {
    let data;
    let id = parseInt($('#idDept').val());
    let name = $('#nameDept').val()
    let divisionId = parseInt($('#divisionDept').val());

    data = {
        "id": id,
        "name": name,
        "divisionId": divisionId
    }
        
    $.ajax({
        url: 'https://localhost:7042/api/Departements/',
        type: 'PUT',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function (data) {
            Swal.fire('Successfully saved data', '', 'success');
            location.reload();
        }
    });
}

function getId(id) {
    delIdDept = id;
    Swal.fire({
        title: 'Do you want to delete this data?',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Delete',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            deleteData();
        }
    })
}

function deleteData() {
    $.ajax({
        url: `https://localhost:7042/api/Departements/?id=${delIdDept}`,
        type: 'DELETE',
        headers: {
            'Authorization': $.cookie('token')
        },
        success: function (data) {
            Swal.fire('Deleted!', '', 'success');
            location.reload();
        }
    });
}

function addData() {
    let data;
    let id = 0;
    let name = $('#addNameDept').val()
    let divisionId = $('#addDivisionIdDept').val();
    divisionId = parseInt(divisionId);

    data = {
        "id": id,
        "name": name,
        "divisionId": divisionId
    };

    console.log(data);

    $.ajax({
        url: 'https://localhost:7042/api/Departements/',
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function () {
            Swal.fire('Successfully added data', '', 'success');
            location.reload();
        }
    });
}