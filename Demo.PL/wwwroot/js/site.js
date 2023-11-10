// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var searchInput = document.getElementById("searchInp");

searchInput.addEventListener("keyup", function () {
    let xhr = new XMLHttpRequest();

    let url = `https://localhost:44346/Employee/Index?searchInp=${searchInput.value}`;
    xhr.open("POST", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    xhr.send();
})