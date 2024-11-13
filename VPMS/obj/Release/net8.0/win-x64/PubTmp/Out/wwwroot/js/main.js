// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//setting for idle logout
// Set timeout variables. 1000 = 1 second.
var timoutWarning = 300000;
var timoutNow = 600000;
//var timoutWarning = 3000;
//var timoutNow = 6000;
var logoutUrl = '/Login/Logout?autoLogout=true'; // URL to logout page.

var warningTimer;
var timeoutTimer;

// Start timers.
function StartTimers() {
    clearTimeout(warningTimer);
    clearTimeout(timeoutTimer);
    warningTimer = setTimeout("IdleWarning()", timoutWarning);
    timeoutTimer = setTimeout("IdleTimeout()", timoutNow);
}

// Reset timers.
function ResetTimers() {
    clearTimeout(warningTimer);
    clearTimeout(timeoutTimer);
    StartTimers();

    $("#alert").slideUp(500, function () {
        $("#alert")[0].style.setProperty('display', 'none', 'important');
    });
}

// Show idle timeout warning dialog.
function IdleWarning() {
    $("#alert").slideDown(500);
}

// Logout the user.
function IdleTimeout() {
    window.location = logoutUrl;
}