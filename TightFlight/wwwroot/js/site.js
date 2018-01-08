// Write your JavaScript code.
var startDate = new Date('01/01/1900');
var FromEndDate = new Date();
var ToEndDate = new Date();

ToEndDate.setDate(ToEndDate.getDate()+365);

$('#plane-date-picker').datepicker({

    weekStart: 1,
    startDate: startDate,
    endDate: FromEndDate,
    autoclose: true
})
    .on('changeDate', function (selected) {
        if (Date.selected.date.valueOf() < startDate)
        {
            Date.selected.date.valueOf() = startDate;
        }
        startDate = new Date(selected.date.valueOf());
        startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
        $('.to_date').datepicker('setStartDate', startDate);
    }); 