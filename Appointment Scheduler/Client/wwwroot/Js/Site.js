


function ShowError(id) {
    $("#"+id).show().delay(3000).fadeOut();
   
}

window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success('Operation Successful', message);
    }
    else {
        toastr.success('Operation Failed', message);
    }
};

window.CalendarInit =()=> {
    const container = document.getElementById('calendar');
    
    const options = {
        defaultView: 'month',
      
        timezone: {
            zones: [
                {
                    timezoneName: 'Asia/Seoul',
                    displayLabel: 'Seoul',
                },
                {
                    timezoneName: 'Europe/London',
                    displayLabel: 'London',
                },
            ],
        },

       

        calendars: [
            {
                id: 'cal1',
                name: 'Personal',
                backgroundColor: '#03bd9e',
            },
            {
                id: 'cal2',
                name: 'Work',
                backgroundColor: '#00a9ff',
            },
        ],
    };

    const calendar = new Calendar(container, options);
    const previous = document.getElementById("previous");
    const next = document.getElementById("next");
    const month = document.getElementById("month");
    const week = document.getElementById("week");
    const day = document.getElementById("day");
    const today = document.getElementById("today");

    calendar.on('myCustomEvent', () => {
        
    });



    previous.addEventListener("click", function () {
        calendar.prev();
    });
    next.addEventListener("click", function () {
        calendar.next();
    });
    month.addEventListener("click", function () {
        calendar.changeView('month');
    });
    week.addEventListener("click", function () {
        calendar.changeView('week');
    });
    day.addEventListener("click", function () {
        calendar.changeView('day');
    });
    today.addEventListener("click", function () {
        calendar.today();
    });


}

window.SideCalendarInit = () => {
    const container = document.getElementById('calendar');
    const options = {
        defaultView: 'month',

        timezone: {
            zones: [
                {
                    timezoneName: 'Asia/Seoul',
                    displayLabel: 'Seoul',
                },
                {
                    timezoneName: 'Europe/London',
                    displayLabel: 'London',
                },
            ],
        },



        calendars: [
            {
                id: 'cal1',
                name: 'Personal',
                backgroundColor: '#03bd9e',
            },
            {
                id: 'cal2',
                name: 'Work',
                backgroundColor: '#00a9ff',
            },
        ],
    };

    const calendar = new Calendar(container, options);
    const previous = document.getElementById("previous");
    const next = document.getElementById("next");
    previous.addEventListener("click", function () {
        calendar.prev();
    });
    next.addEventListener("click", function () {
        calendar.next();
    });

}
