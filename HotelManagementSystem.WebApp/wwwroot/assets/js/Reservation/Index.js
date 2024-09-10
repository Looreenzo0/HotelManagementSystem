$(function () {
    loadReservation();

    $('#ReservationTable').on('click', '#BtnDetail', function (e) {
        e.preventDefault();
        const currentRow = $(this).closest("tr");
        const data = $('#ReservationTable').DataTable().row(currentRow).data();

        $('#RefNumView').text(data['refNum']);
        $('#NameView').text(data['name']);
        $('#ContactNumberView').text(data['contactNumber']);
        $('#AddressView').text(data['address']);
        $('#PaxView').text(data['pax']);
        $('#CheckInView').text(formatShortDate(data['checkIn']));
        $('#CheckOutView').text(formatShortDate(data['checkOut']));
        $('#RoomTypeView').text(data['roomType']);
        var formattedRoomRate = formatMoney((data['roomRate']), 2, "₱ ");
        $("#RoomRateView").text(formattedRoomRate);

        const a = document.getElementById("ModalReservationDetail");
        a.style.display = "block";

    });
    $("#closeDetail").on('click', function () {
        const a = document.getElementById("ModalReservationDetail");
        a.style.display = "none";
    });

    $("#closeModalDetail").on('click', function () {
        const a = document.getElementById("ModalReservationDetail");
        a.style.display = "none";
    });

});
function loadReservation() {
    const client = $("#ReservationTable").DataTable({
        language: {
            processing: '<i class="fa fa-spinner fa-spin" style="font-size:24px;color:rgb(75, 183, 245);"></i>'
        },
        async: true,
        processing: true,
        serverSide: true,
        orderMulti: false,
        autoWidth: false,
        dom: '<"top"i>rt<"bottom"lp><"clear">',
        ajax: {
            url: "/Reservation/LoadReservation",
            type: "POST",
            datatype: "json"
        },
        columnDefs: [{
            targets: [0],
            visible: false,
            searchable: false
        }],
        columns: [
            { data: "id", name: "id", autoWidth: true },
            {
                title: "",
                width: "3%",
                className: "text-center",
                data: 'status',
                render: function () {
                    return `
                      <div class="btn-group-sm">
                          <a class="btn btn-info" type="button" id="BtnDetail">
                                    <i class="fas fa-info-circle text-white"></i>
                                </a></div>`;
                }
            },
            {
                data: "refNum",
                title: "Ref. No.",
                autoWidth: true,
                render: function (data) {
                    return `<span class='fw-bolder text-primary'> ${data} </span>`;
                }
            },
            { data: "name", name: "name", title: "Customer", autoWidth: true },
            { data: "contactNumber", name: "contactNumber", title: "Contact No.", autoWidth: true },
            { data: "pax", name: "pax", title: "No. of Pax", autoWidth: true },
            { data: "roomType", name: "roomType", title: "Room Type", autoWidth: true },
            {
                data: 'checkIn',
                title: "Arrival",
                render: function (data) {
                    return `<span> ${formatDateOnly(data)} </span>`;
                }
            },
            {
                data: 'checkOut',
                title: "Departure",
                render: function (data) {
                    return `<span> ${formatDateOnly(data)} </span>`;
                }
            },
            { data: "numberOfNights", name: "numberOfNights", title: "No. of Nights", autoWidth: true },
            //{
            //    data: 'status',
            //    title: "Status",
            //    render: function (data) {
            //        const statusMap = {
            //            "Reservation Submitted": "badge bg-info text-white text-center",
            //            "Ongoing Evaluation": "badge bg-success text-white text-center",
            //            "Approved": "badge bg-primary text-white text-center",
            //            "Declined": "badge badge-primary-lighten",
            //            "Canceled": "badge bg-danger text-white text-center",
            //            "Reserved": "badge bg-primary text-white text-center"
            //        };
            //        return `<span class="${statusMap[data] || 'font-weight-bold text-info'}"> ${data || '- - -'} </span>`;
            //    }
            //},
            {
                data: 'createdAt',
                title: "Date Created",
                render: function (data) {
                    return `<span> ${formatLongDate(data)} </span>`;
                }
            }
        ]
    });

    function applySearch() {
        client.columns(0).search($('#SearchText').val().trim());
        client.columns(1).search($('#docstatus-select').val().trim());
        client.columns(2).search($('#DateFrom').val().trim());
        client.columns(3).search($('#DateTo').val().trim());
        client.draw();
    }

    $("#btnSearch, #DateFrom, #DateTo, #docstatus-select").on("click change", applySearch);
    applySearch(); // Initial search
}

function formatShortDate(dateString) {
    const date = new Date(dateString);
    const options = {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric'
    };
    return date.toLocaleDateString('en-US', options);
}

function formatLongDate(dateString) {
    const date = new Date(dateString);
    const options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric'
    };
    return date.toLocaleDateString('en-US', options);
}

function formatDateOnly(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [month, day, year].join('/');

}

function formatMoney(amount, decimalPlaces, currencySymbol) {
    var formattedAmount = amount.toLocaleString('en-US', {
        minimumFractionDigits: decimalPlaces,
        maximumFractionDigits: decimalPlaces,
    });
    if (currencySymbol) {
        formattedAmount = currencySymbol + formattedAmount;
    }
    return formattedAmount;
}