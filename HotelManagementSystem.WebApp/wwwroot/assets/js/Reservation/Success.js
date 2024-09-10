$(function () {

    const roomRate = parseFloat($("#roomRate").val()) || 0;
    var formattedRoomRate = formatMoney(roomRate, 2, "₱ ");
    $("#RoomRateView").text(formattedRoomRate);
});

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

function printDetails() {

    var printContents = document.getElementById("printable-content").innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;

}