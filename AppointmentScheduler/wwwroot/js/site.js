// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showHideModal(){
        var modal = document.getElementById("mymodal");
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
    start.value = info.startStr.substring(0, 10);
    end.value = info.endStr.substring(0, 10);
    
}