$(function () {
    var roomId = 0;

    window.onclick = function () {
        var a = document.getElementById("dropRoom");
        a.style.display = "none";
    }

    $("#SearchRoom").on("input", function () {
        var a = $("#SearchRoom").val(),
            x = document.getElementById("dropRoom");
        if (a == "") {
            x.style.display = "none";
            ClearData();
        }
        else if (a !== "none") {
            x.style.display = "block";
            x.style.border = "1px solid #CCC";
            loadSearchRoom();
        } else if (a == "") {
            x.style.display = "none";
            ClearData();
        } else {
            x.style.display = "none";
            ClearData();
        }
    });

    $("#RoomTable").on('click', '#BtnSelect', function (e) {
        e.preventDefault();
        var currentRow = $(this).closest("tr");
        var data = $('#RoomTable').DataTable().row(currentRow).data();

        roomId = (data['id']);

        $("#SearchRoom").val(data['name']);
        $("#RoomId").val(data['id']);

        var a = document.getElementById("dropRoom");
        a.style.display = "none";

        $("#SearchRoom").prop('readonly', true);
        $("#btnReselect").text('Change');
    });


    $("#RoomTable").on('click', 'tr', function (e) {
        e.preventDefault();
        var currentRow = $(this).closest("tr");
        var data = $('#RoomTable').DataTable().row(currentRow).data();

        roomId = (data['id']);

        $("#SearchRoom").val(data['name']);
        $("#RoomId").val(data['id']);

        var a = document.getElementById("dropRoom");
        a.style.display = "none";

        $("#SearchRoom").prop('readonly', true);
        $("#btnReselect").text('Change');
    });

    $("#btnReselect").on('click', function () {
        const clear = '';
        const fields = [
            '#RoomId', '#SearchRoom'
        ];

        //const viewFields = [
        //    'PhaseSelectView', 'ModelSelectView', 'BlockSelectView', 'LotSelectView', 'LotAreaSelectView',
        //    'FloorAreaSelectView', 'TotalSelectView', 'ReservationSelectView', 'PhaseCal'
        //];

        fields.forEach(field => $(field).val(clear));

        //viewFields.forEach(field => {
        //    $(`#${field}`).text(clear);
        //});

        $("#SearchRoom").removeAttr('readonly');
        $("#btnReselect").text('Clear');
    });

    $("#btnSave").on('click', function (event) {
        event.preventDefault();
        const Name = $("#Name").val(),
            Address = $("#Address").val(),
            ContactNumber = $("#ContactNumber").val(),
            Pax = $("#Pax").val(),
            RoomId = $("#RoomId").val();

        if (RoomId == "") {
            Swal.fire(
                'Oooppss!',
                'Please select your room!',
                'warning'
            )
            return false;
        }
        else if (Name == "") {
            Swal.fire(
                'Oooppss!',
                'Full Name is required!',
                'warning'
            )
            return false;
        }
        else if (Address == "") {
            Swal.fire(
                'Oooppss!',
                'Address is required!',
                'warning'
            )
            return false;
        }
        else if (ContactNumber == "") {
            Swal.fire(
                'Oooppss!',
                'Contact Number is required!',
                'warning'
            )
            return false;
        }
        else if (Pax == "") {
            Swal.fire(
                'Oooppss!',
                'No. of Pax is required!',
                'warning'
            )
            return false;
        }
        else {
            AddReservation();
        }
      
    });


    function loadSearchRoom() {
        var room = $("#RoomTable").DataTable({
            "language": {
                "processing": '<i class="fa fa-spinner fa-spin" style="font-size:24px;color:rgb(75, 183, 245);"></i>'
            },
            "async": true,
            "processing": true,
            "serverSide": true,
            "orderMulti": false,
            "scrollY": "150px",
            "scrollX": true,
            "scrollCollapse": true,
            "retrieve": true,
            "paging": false,
            "ordering": false,
            "info": false,
            "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "keys": {
                "keys": [13 /* ENTER */, 38 /* UP */, 40 /* DOWN */]
            },
            "ajax": {
                "url": "/HotelRoom/SearchAllRooms",
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs": [{
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
            "columns": [

                { "data": "id", "name": "id", "autoWidth": true },
                {
                    title: "",
                    width: "4%",
                    className: "text-center",
                    data: 'status',
                    render: function (data) {
                        return `
                         <div class="btn-group btn-group-sm text-center" role="group">
                             <a class="btn btn-primary" id="BtnSelect" type="button"  aria-expanded="false">
                            <i class="mdi mdi-check-bold"></i>
                            </a>                                       
                         </div>
                         `;
                    }
                },
                { "data": "name", "name": "name", "title": "Room Description", "autoWidth": true },
                {
                    data: 'status',
                    title: 'Status',
                    render: function (data) {
                        switch (data) {
                            case "Available":
                                return `<span class="badge bg-info text-white text-center"> ${data} </span>`;
                            case "Occupied":
                                return `<span class="badge bg-success text-white text-center"> ${data} </span>`;
                            case "HouseUse":
                                return `<span class="badge bg-primary text-white text-center"> ${data} </span>`;
                            case "OutofOrder":
                                return `<span class="badge badge-primary-lighten"> ${data} </span>`;
                            default:
                                return `<span class='font-weight-bold text-info'> - - - </span>`;
                        }
                    }
                },
            ]

        })

        room.columns(0).search($('#SearchRoom').val().trim());
        room.draw();

        $('#RoomTable').on('key-focus.dt', function (e, datatable, cell) {
            $(room.row(cell.index().row).node()).addClass('selected');
        });

        $('#RoomTable').on('key-blur.dt', function (e, datatable, cell) {
            $(room.row(cell.index().row).node()).removeClass('selected');
        });

        $('#RoomTable').on('key.dt', function (e, key, cell) {
            e.preventDefault();
            if (key === 13) {
                var data = house.row(cell.index().row).data();
                roomId = (data['id']);

                $("#SearchRoom").val(data['name']);
                $("#RoomId").val(data['id']);
   
                var a = document.getElementById("dropRoom");
                a.style.display = "none";
            }
        });
    }

    function AddReservation() {

        const ReservationData = {
            Name: $("#Name").val(),
            Address: $("#Address").val(),
            ContactNumber: $("#ContactNumber").val(),
            Pax: $("#Pax").val(),
            CheckIn: $("#CheckIn").val(),
            CheckOut: $("#CheckOut").val(),
            RoomId: $("#RoomId").val() || null,
        };

        const json = JSON.stringify(ReservationData);
        $("#loadSpinnerss").show();
        $.ajax({
            async: true,
            type: 'POST',
            dataType: 'JSON',
            contentType: 'application/json; charset=utf=8',
            url: '/Reservation/NewReservation',
            data: json,
            success: function (result) {
                if (result.success) {
                    connection.invoke("SendReservation", ReservationData.Name).catch(err => console.error(err.toString()));
                    $("#loadSpinnerss").hide();
                    Swal.fire({
                        position: "top-center",
                        icon: "success",
                        title: "Your reservation has been submitted.",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    localStorage.setItem('reservation', JSON.stringify(ReservationData));
                    window.location.href = `/Reservation/ReservationSuccess?refNum=${result.refNum}`;
                } else {
                    Swal.fire('Error!', result.responseText, 'warning');
                    $("#loadSpinnerss").hide();
                }
            },
            error: function (xhr) {
                Swal.fire('Error!', xhr.responseText, 'error');
                $("#loadSpinnerss").hide();
            }
        });
    }

    function ClearData() {
        const clear = '';
        const fields = [
            '#RoomId',
          
        ];

        //const viewFields = [
        //    'Name', 'Address', 'BlockSelectView', 'LotSelectView', 'LotAreaSelectView',
        //    'FloorAreaSelectView', 'TotalSelectView', 'ReservationSelectView'
        //];

        fields.forEach(field => $(field).val(clear));

        //viewFields.forEach(field => {
        //    $(`#${field}`).text(clear);
        //});

    }

});