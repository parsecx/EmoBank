function validateUser() {
    Review.GetReviews();

}


function randomString() {
    var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
    var string_length = 4;
    var randomstring = '';
    for (var i = 0; i < string_length; i++) {
        var rnum = Math.floor(Math.random() * chars.length);
        randomstring += chars.substring(rnum, rnum + 1);
    }
    return randomstring;
}

var Review = {

    GetReviews: function() {

        if ($('#txtUsername').val() == '') {
            $("#msg").html("User Name should not be empty.");

        }
        else if ($('#txtPassword').val() == '') {
            $("#msg").html("User Password should not be empty.");
        }           
        else {
            var pkey = $('#txtKey').val().split(',');
            rsa = new RSAKey();
            rsa.setPublic(pkey[1], pkey[0]);
            var username = rsa.encrypt($('#txtUsername').val());
            var pass = rsa.encrypt($('#txtPassword').val());
            var strStar = ''; 
            for (i = 0; i < pass.length -10; i++) {strStar = strStar + '*';}  
            $('#txtPassword').val(strStar);
            $('#chk').val(pass);
            ajaxcall.data = "{encUsername:'" + username + "',encPassword:'" + pass + "'}";
            ajaxcall.url = 'Login.aspx/ValidateUser'
            ajaxcall.callbackfunction = function(response) {
                $("#hdnloginstatus").val(response.d);
                
            }
            ajaxcall.Call();
        }
    }
}

