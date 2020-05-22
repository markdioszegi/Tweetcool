// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

btnTop = document.getElementById("btnTop");

window.onscroll = function () { checkScrollFromTop() };

function checkScrollFromTop() {
    if (window.pageYOffset > 80) {
        btnTop.classList.add("tc-btn-top-show");
    }
    else {
        btnTop.classList.remove("tc-btn-top-show");
    }
}

function goToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function changeAvatar(ele) {
    var image = document.getElementById('change-avatar').click();
    ele.src = image;
}
