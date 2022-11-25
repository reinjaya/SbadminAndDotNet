
console.log("äaaxxaa")

function login() {
    //var _email = $('#inputEmail').val();
    //var _password = $('#inputPassword').val();

    //var data = {
    //    "email": _email,
    //    "password": _password
    //}

    let data = new Object();
    data.email = $('#inputEmail').val();
    data.password = $('#inputPassword').val();

    console.log(data)
    /* https://localhost:7042/api/Account/Login/  https://localhost:7042/api/Account/Login/?email=${data.email}&password=${data.password} */

    $.ajax({
        url: `https://localhost:7042/api/Account/Login/`,
        method: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        headers: {
            'Content-Type': 'application/json'
        },
        success: function (d) {
            //$.cookie('token', d.token)
            sessionStorage.setItem("token", d.token);
            console.log($.cookie('token'))
            window.location.replace("../Departement/")
        }
    })
}

//function login() {
//    var _email = $('#inputEmail').val();
//    var _password = $('#inputPassword').val();
//    $.post("https://localhost:7042/api/Account/Login", {
//        email: _email, password: _password}
//    }).done(function (data) {
//        $.cookie('token', data.token)
//        console.log("äaaaa")
//    })
//}