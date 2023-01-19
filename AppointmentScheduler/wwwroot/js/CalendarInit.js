document.addEventListener('DOMContentLoaded', function () {

    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {

        initialView: 'dayGridMonth',
        selectable: true,
        editable: true,

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
        },

        dayMaxEvents: true, // allow "more" link when too many events
        select: function (selectionInfo) {
            showHideModal('mymodal');
            populatemodal(selectionInfo);

        },
        events: function (fetchInfo, successCallback, failureCallback) {


            var evarr = [];

            $.ajax({
                type: "GET",
                url: "/appointment/getallevents",
                dataType: "json",
                data: { startDate: fetchInfo.start.toISOString().substring(0, 16), endDate: fetchInfo.end.toISOString().substring(0, 16) },
                success: function (data) {

                    data.forEach(function (current) {
                        evarr.push(
                            {
                                id: current.id,
                                start: current.start,
                                end: current.end,
                                description: current.description,
                                title: current.title,
                                category: current.category,
                                status: current.status
                            }
                        )
                    });

                    successCallback(
                        evarr
                    )


                }
            });



        },

        eventClick: function (info) {
            var d = document.getElementById('appmodal');
            var duration = document.getElementById('appmodal-duration');
            var time = document.getElementById('appmodal-time');
            var desc = document.getElementById('appmodal-description');
            var close = document.getElementById('appmodal-close');
            var edit = document.getElementById('appmodal-edit');
            var appdelete = document.getElementById('appmodal-delete');
            var alertmessage = document.getElementById('alertmessage');
            var myalert = document.getElementById('alert');

            var appformel = document.getElementById("appformedit").elements;
            appformel[0].value = info.event.id;
            appformel[1].value = info.event.start.toISOString().substring(0, 16);
            appformel[2].value = info.event.end.toISOString().substring(0, 16);
            appformel[3].value = info.event.extendedProps.category;
            appformel[4].value = info.event.title;
            appformel[5].value = info.event.extendedProps.description;
            appformel[6].value = info.event.startStr;

            close.addEventListener("click", function () {
                d.style.display = "none";
            });



            appdelete.addEventListener("click", function () {
                $.ajax({
                    type: "GET",
                    url: "/appointment/deleteevent",
                    dataType: "json",
                    data: { id: info.event.id },
                    success: function (data) {
                        myalert.style.display = "block";
                        alertmessage.innerHTML = " Task deleted Successfully";
                        calendar.refetchEvents();

                    },
                    error: function (data) {

                        alertmessage.innerHTML = " Failed to delete task";


                    }
                });
            });

            time.innerHTML = info.event.start.toISOString().substring(11, 16) + ' - ' + info.event.end.toISOString().substring(11, 16);
            desc.innerHTML = info.event.extendedProps.description;

            calculateDuration(info.event.start.toISOString().substring(0, 16), duration);
            d.style.position = "absolute";
            d.style.display = "block";
            d.style.left = info.jsEvent.pageX + 'px';
            d.style.top = info.jsEvent.pageY + 'px';

        }


    });
    calendar.render();
});


$(document).ready(function () {
    const ctx = document.getElementById('myChart');
    var exdata = [];
    $.ajax({
        type: "GET",
        url: "/appointment/getstats",
        dataType: "json",

        success: function (resdata) {
            exdata.push(resdata.personal);
            exdata.push(resdata.work);
            exdata.push(resdata.others);

            var newdata = {
                datasets: [{
                    data: exdata,

                }],

                // These labels appear in the legend and in the tooltips when hovering different arcs
                labels: [
                    'Personal',
                    'Work',
                    'Others'
                ]

            };

            const config = {
                type: 'doughnut',
                data: newdata,
                options: {
                    color: 'white'
                }


            };

            new Chart(ctx, config);


        },
        error: function (resdata) {

            alertmessage.innerHTML = " Failed to delete task";
        }
    });




});

$(document).mouseup(function (e) {
    var container = $("#appmodal");

    // if the target of the click isn't the container nor a descendant of the container
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.hide();
    }
});