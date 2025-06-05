function validateSession(loginSessionID) {
    $.post("/Login/ValidateSession", {
        sessionID: loginSessionID
    }).done(function (result) {
        if (result.statusCode != 200) {
            window.location.href = "/Login";
        }
    });
}