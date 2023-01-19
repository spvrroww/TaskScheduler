// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showHideModal(id){
        var modal = document.getElementById(id);
    if (!modal.classList.contains("hidemodal")) {
        modal.classList.add("hidemodal");
    }
    else {
        modal.classList.remove("hidemodal");
    }
     
}

function populatemodal(info) {
    const start = document.getElementById("startdate");
    const end = document.getElementById("enddate");
    info.start.setDate(info.start.getDate() + 1);
    start.value = info.start.toISOString().substring(0, 16);
    end.value = info.end.toISOString().substring(0, 16);
    
}

function GetData(start, end) {
    var events = [];
 
    $.get("/appointment/getallevents", { startDate: start.toISOString().substring(0, 16), endDate: end.toISOString().substring(0, 16) }, function (data, textStatus, jqXHR) {
        console.log("processing data");
        if (textStatus === "success") {
            data.forEach(function (current) {
                events.push(
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
        }
        console.log("data proccessed");
        
    });

    console.log("data proccessed");
    return events;

}

function calculateDuration(newDate, el) {
    const now = new Date().getTime();
    const futureDate = new Date(newDate).getTime();
    const timeleft = futureDate - now;

    const days = Math.floor(timeleft / (1000 * 60 * 60 * 24));
    const hours = Math.floor((timeleft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((timeleft % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((timeleft % (1000 * 60)) / 1000);
    var duration = "";
    if (days > 0) {
        el.innerHTML = 'starts in ' + days + ' days ' + hours + ' hours ' + minutes + ' minutes ';
    }
    else if (hours > 0) {
        el.innerHTML = 'starts in ' + hours + ' hours ' + minutes + ' minutes ';
    }

    else if (minutes > 0) {
        el.innerHTML = 'starts in ' +  minutes + ' minutes ';
    }

    else {
        el.innerHTML = "expired";
    }

    
    
}

function delay() {
    window.onload = function () {
        setTimeout(function () {
            document.getElementById("allmyalert").style.display = "none";
        }, 3000);
    }
}

delay();


$(document).ready(function () {
    const start = document.getElementById("startdate");
    const end = document.getElementById("enddate");

    start.addEventListener("input", (event) => {
        if (end.value < start.value) {
            end.value = start.value;
        } 
    });

    end.addEventListener("input", (event) => {
        if (end.value < start.value) {
            end.value = start.value;
        }
    });

});


function drawVisor() {
    const canvas = document.getElementById('visor');
    const ctx = canvas.getContext('2d');

    ctx.beginPath();
    ctx.moveTo(5, 45);
    ctx.bezierCurveTo(15, 64, 45, 64, 55, 45);

    ctx.lineTo(55, 20);
    ctx.bezierCurveTo(55, 15, 50, 10, 45, 10);

    ctx.lineTo(15, 10);

    ctx.bezierCurveTo(15, 10, 5, 10, 5, 20);
    ctx.lineTo(5, 45);

    ctx.fillStyle = '#2f3640';
    ctx.strokeStyle = '#f5f6fa';
    ctx.fill();
    ctx.stroke();
}

const cordCanvas = document.getElementById('cord');
const ctx = cordCanvas.getContext('2d');

let y1 = 160;
let y2 = 100;
let y3 = 100;

let y1Forward = true;
let y2Forward = false;
let y3Forward = true;

function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, innerWidth, innerHeight);

    ctx.beginPath();
    ctx.moveTo(130, 170);
    ctx.bezierCurveTo(250, y1, 345, y2, 400, y3);

    ctx.strokeStyle = 'white';
    ctx.lineWidth = 8;
    ctx.stroke();


    if (y1 === 100) {
        y1Forward = true;
    }

    if (y1 === 300) {
        y1Forward = false;
    }

    if (y2 === 100) {
        y2Forward = true;
    }

    if (y2 === 310) {
        y2Forward = false;
    }

    if (y3 === 100) {
        y3Forward = true;
    }

    if (y3 === 317) {
        y3Forward = false;
    }

    y1Forward ? y1 += 1 : y1 -= 1;
    y2Forward ? y2 += 1 : y2 -= 1;
    y3Forward ? y3 += 1 : y3 -= 1;
}

drawVisor();
animate();


