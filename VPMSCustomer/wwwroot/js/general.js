function validatePasswordFormat(strpassword) {
    var pattern = new RegExp("^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,}$");

    return pattern.test(strpassword);
}