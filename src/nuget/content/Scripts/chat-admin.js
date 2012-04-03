$(function () {
    if (document.location.href.indexOf('/chat/go/') > -1) {
        window.setTimeout(function () { document.location.href = document.location.href.replace('-status', ''); }, 12345);
    }
});