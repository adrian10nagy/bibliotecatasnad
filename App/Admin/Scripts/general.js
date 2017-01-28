//hide after 5 seconds
function autoHideStatusLabel(key, time) {
    setTimeout(function () { document.getElementById(key).style.display = 'none'; }, time);
}