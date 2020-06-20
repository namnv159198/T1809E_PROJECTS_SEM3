var start = moment().subtract(29, 'days');
var end = moment();

var url_string = location.href;
var url = new URL(url_string);

var initStart = url.searchParams.get("start");
var initEnd = url.searchParams.get("end");

if (initStart != null && moment(initStart, 'YYYY-MM-DD', true).isValid()) {
    start = initStart;
    $('input[name="start"]').val(start);
}
if (initEnd != null && moment(initEnd, 'YYYY-MM-DD', true).isValid()) {
    end = initEnd;
    $('input[name="end"]').val(end);
}

$('input[name="daterange"]').daterangepicker({
    startDate: start,
    endDate: end,
    locale: {
        format: 'YYYY-MM-DD'
    },
    ranges: {
        'Today': [moment(), moment()],
        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        'This Month': [moment().startOf('month'), moment().endOf('month')],
        'Last Month': [
            moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')
        ]
    }
});
$('input[name="daterange"]').on('apply.daterangepicker',
    function (ev, picker) {
        var startDate = picker.startDate.format('YYYY-MM-DD');
        var endDate = picker.endDate.format('YYYY-MM-DD');
        $('input[name="start"]').val(startDate);
        $('input[name="end"]').val(endDate);
        $('form[name="search-form"]').submit();
    });