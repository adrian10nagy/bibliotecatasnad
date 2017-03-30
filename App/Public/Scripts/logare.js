
window.onload = function () {

    $('.searchMain').hide();

}

function login(address) {
    var userEmail = $("#loginUserEmail");
    var userPass = $("#loginUserPassword");
    if (address == null)
    {
        address = window.location.href;
    }

    LoginUserPassHref(userEmail, userPass, address);

}

$(document).ready(function () {
    
    $("#btnUserRegister").on("click", function () {
        return RegisterUser();
    });

});

function LoginUserPassHref(pUserEmail, pUserPass, address) {
    pUserEmail.css("border", '');
    pUserPass.css("border", '');
    $("#loginFormDivLoginMessage").text('');

    var isValid = true;

    if (pUserEmail.val() == "") {
        pUserEmail.css("border", "2px solid red");
        isValid = false;
    }

    if (pUserPass.val() == "") {
        pUserPass.css("border", "2px solid red");
        isValid = false;
    }

    if (isValid) {
        $.ajax({
            url: "/Cont/AsyncUserLogin",
            type: "POST",
            data: { userEmail: pUserEmail.val(), userPass: pUserPass.val() },
            success: function (xhr) {
                if (xhr.response == 1) {
                    window.location.href = address;
                } else {
                    $("#loginFormDivLoginMessage").text('Combinația utilizator-parolă este greșită!');
                    pUserEmail.css("border", "2px solid red");
                    pUserPass.css("border", "2px solid red");
                }
            }
        })
    }
}

function LoginUser(pUserEmail, pUserPass) {
    pUserEmail.css("border", '');
    pUserPass.css("border", '');
    $("#loginFormDivLoginMessage").text('');

    var isValid = true;

    if (pUserEmail.val() == "") {
        pUserEmail.css("border", "2px solid red");
        isValid = false;
    }

    if (pUserPass.val() == "") {
        pUserPass.css("border", "2px solid red");
        isValid = false;
    }

    if (isValid) {
        $.ajax({
            url: "/Cont/AsyncUserLogin",
            type: "POST",
            data: { userEmail: pUserEmail.val(), userPass: pUserPass.val() },
            success: function (xhr) {
                if (xhr.response == 1) {
                    window.location.href = "/Home/Index";
                } else {
                    $("#loginFormDivLoginMessage").text('Combinația utilizator-parolă este greșită!');
                    pUserEmail.css("border", "2px solid red");
                    pUserPass.css("border", "2px solid red");
                }
            }
        })
    }
}

function RegisterUser() {
    $("#loginFormDivCreateMessage").text('');
    if (RegisterUserValidate()) {
        var userName = $("#txtUserName");
        var userSurame = $("#txtUserSurame");
        var email = $("#registerUserEmail");
        var password = $("#registerUserPassword");


        $.ajax({
            url: "/Cont/AsyncUserRegister",
            type: "POST",
            data: { userSurame: userSurame.val(), userName: userName.val(), userEmail: email.val(), userPass: password.val() },
            success: function (xhr) {
                if (xhr.result == 1) {
                    window.location.href = "/Home/Index";
                }
                else if (xhr.result == 4) {
                    $("#loginFormDivCreateMessage").text('Acest email a fost deja folosit!');
                    email.css("border", "2px solid red");
                }
                else {

                    email.val(" Email deja folosit");
                    email.css("border", "2px solid red");
                }
            }
        })
    }
}

function RegisterUserValidate() {
    var errors = [];
    var userName = $("#txtUserName");
    var userSurame = $("#txtUserSurame");
    var email = $("#registerUserEmail");
    var password = $("#registerUserPassword");
    var passwordVerify = $("#registerUserPasswordVerify");

    userName.css("border", '');
    userSurame.css("border", '');
    email.css("border", '');
    password.css("border", '');
    passwordVerify.css("border", '');

    if (userName.val() == "") {
        userName.css("border", "2px solid red");
        errors.push("userName");
    }

    if (userSurame.val() == "") {
        userSurame.css("border", "2px solid red");
        errors.push("userSurame");
    }

    if (email.val() == "") {
        email.css("border", "2px solid red");
        errors.push("email");
    }

    if (password.val() == "") {
        password.css("border", "2px solid red");
        errors.push("password");
    }
    if (passwordVerify.val() == "") {
        passwordVerify.css("border", "2px solid red");
        errors.push("passwordVerify");
    }

    if (password.val() != passwordVerify.val()) {
        password.css("border", "2px solid red");
        passwordVerify.css("border", "2px solid red");
        errors.push("passwordVerifyDiffer");
    }

    //result
    if (errors.length > 0) {
        return false;
    }

    return true;
}