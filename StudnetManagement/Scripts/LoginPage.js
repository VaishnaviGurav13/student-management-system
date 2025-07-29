
jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
jQuery.BasePathNameSpace.WebServicePath = "";

var LoginPageViewModel = {

    EmailId: ko.observable(''),
    Password: ko.observable('')
}


function LoginPageLoad(webServicePath) {

    jQuery.BasePathNameSpace.WebServicePath = webServicePath;
    console.log("Path: " + jQuery.BasePathNameSpace.WebServicePath);

    ko.applyBindings(LoginPageViewModel);

}

function LoginBtnClick() {

    jQuery("#EmailValidationMsg").text("");
    jQuery("#PasswordValidationMsg").text("");
    jQuery("#errorMsg").text("");

    isValid = Validation();
    if (!isValid) {
        alert("Please Enter Valid Data.");
        return;
    }

    //let emailId = jQuery("#txtEmailId").val().trim();
    //let password = jQuery("#txtPassword").val().trim();
    //let lblMsg = jQuery("#lblMsg").val().trim();

    let emailId = LoginPageViewModel.EmailId();
    let password = LoginPageViewModel.Password();

    let Credentials = {};
    
    Credentials.EmailId = emailId;
    //Credentials.Password = password;
    

    $.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/LoginPageService.asmx/GetUserData",
        data: JSON.stringify({ LoginData: Credentials }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let userData = response.d;
            //if (emailId === userData.EmailID && password === userData.Password)
            if (password === userData.Password) {

                sessionStorage.setItem("Role", userData.Role);

                if (userData.Role === "Admin") {
                    window.location.href = "AdminDashboard.aspx";
                }
                else if (userData.Role === "Student") {
                    if (userData.Status === "P") {
                        jQuery("#lblMsg").show();
                        jQuery("#lblMsg").text("Your Approval is in the Pending State. Please Wait for your Approval.").css("color", "red");
                        jQuery("#lblMsg").fadeOut(5000);
                        //sessionStorage.setItem("StudentId", userData.StudentId);
                        //alert("Your Approval is in the Pending State. Please Wait for your Approval.");
                        //window.location.href = "StudentProfile.aspx";
                        //window.location.href = "StudentRegistration.aspx?EmailId=" + userData.EmailID;
                    }
                    else if (userData.Status === "R") {
                        //alert("Your Account has been Rejected. Please contact the Admin.");
                        jQuery("#lblMsg").show();
                        jQuery("#lblMsg").text("Your Account has been Rejected. Please contact the Admin.").css("color", "red");
                        jQuery("#lblMsg").fadeOut(5000);

                        //lblMsg.innerText = "Your Account has been Rejected. Please contact the Admin.";
                    }
                    else if (userData.Status === "A") {
                        sessionStorage.setItem("StudentId", userData.StudentId);
                        jQuery("#lblMsg").show();
                        jQuery("#lblMsg").text("Your Account has been Approved.").css("color", "green");
                        jQuery("#lblMsg").fadeOut(5000);

                        //alert("Your Account has been Approved.");
                        window.location.href = "StudentProfile.aspx";
                        //window.location.href = "StudentRegistration.aspx?EmailId=" + userData.EmailID;
                    }
                }
            }
            else if (userData.Password === "") {
                jQuery("#lblMsg").show();
                jQuery("#lblMsg").text("Your Account has been Deleted. Please contact the Admin.").css("color", "red");
                jQuery("#lblMsg").fadeOut(5000);
                //alert("Your Account has been Deleted. Please contact the Admin.");
            }
            else {
                jQuery("#lblMsg").show();
                jQuery("#lblMsg").text("Invalid Username or Password. Please Check.").css("color", "red");
                jQuery("#lblMsg").fadeOut(5000);
                //alert("Invalid Username or Password. Please Check.");
            }
            ClearTextbox();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });
}

function SignUpClick() {
    window.location.href = "StudentRegistration.aspx";
}

function ClearTextbox() {
    //jQuery("#txtEmailId").val(' ');
    //jQuery("#txtPassword").val(' ');

    LoginPageViewModel.EmailId('');
    LoginPageViewModel.Password('');
}

function Validation() {

    //let emailId = jQuery("#txtEmailId").val().trim();
    //let password = jQuery("#txtPassword").val().trim();

    let emailId = LoginPageViewModel.EmailId();
    let password = LoginPageViewModel.Password();

    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    let isValid = true;

    if (emailId === "") {
        jQuery("#EmailValidationMsg").text("Email ID is Required. Please Enter.");
        isValid = false;
    }
    else if (!emailPattern.test(emailId)) {
        jQuery("#EmailValidationMsg").text("Incorrect Email format.Please Check.");
        isValid = false;
    }

    if (password === "") {
        jQuery("#PasswordValidationMsg").text("Password is Required. Please Enter.");
        isValid = false;
    }
    return isValid;
}