
jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
jQuery.BasePathNameSpace.WebServicePath = "";

var StudentRegistrationViewModel = {

    FullName: ko.observable(""),
    Class: ko.observable(),
    Gender: ko.observable(""),
    EmailId: ko.observable(""),
    PhoneNum: ko.observable(""),
    UserName: ko.observable(""),
    Password: ko.observable(""),

    Courses: ko.observableArray([]),
    SelectedCourses: ko.observableArray([])
}


function PageLoad(webServicePath) {

    jQuery.BasePathNameSpace.WebServicePath = webServicePath;

    LoadCoursesOnPageLoad();
    PhoneNumValidation();

    ko.applyBindings(StudentRegistrationViewModel);
}

function PhoneNumValidation() {
    //const PhoneNum = document.getElementById("txtPhoneNum");

    const mobileInput = document.getElementById("txtPhoneNum");
    if (mobileInput) {
        mobileInput.addEventListener('input', function () {
            this.value = this.value.replace(/\D/g, '').slice(0, 10);
        });
    }



    //if (PhoneNum) {
    //    PhoneNum.addEventListener('input', function () {
    //        jQuery(this).val(jQuery(this).val().replace(/\D/g, ''));
    //    });
    //}
}

function LoadCoursesOnPageLoad() {

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentRegistrationService.asmx/LoadCourses",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let courses = response.d;
            StudentRegistrationViewModel.Courses(courses);

            //let $container = jQuery("#checkboxContainer");
            //$container.empty();

            //jQuery.each(courses, function (index, course) {
            //    let $checkbox = jQuery('<input>', {
            //        type: 'checkbox',
            //        name: 'courses',
            //        value: course.CourseId,
            //        id: 'course_' + course.CourseId
            //    });

            //    let $label = jQuery('<label>', {
            //        for: 'course_' + course.CourseId,
            //        text: course.CourseName
            //    });

            //    $container.append($checkbox).append($label);
            //});

            //let container = jQuery("#checkboxContainer");
            //container.empty();

            //for (let i = 0; i < courses.length; i++) {

            //    container.append(
            //        "<input type='checkbox' name='courses' value='" + courses[i].CourseId + "' id='course_" + courses[i].CourseId + "'/>" +
            //        "<label for='course_" + courses[i].CourseId + "'>" + courses[i].CourseName + "</label>"
            //    );
            //}
        },
        beforeSend: function () {
            jQuery("#loader").show();
        },
        complete: function () {
            jQuery("#loader").hide();
        },
        error: function (xhr) {
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });
}

function SubmitClick() {

    ClearErrorSpans();
    let isValid = FormValidation();

    if (!isValid) {
        alert("Please Check All the fields");
        return;
    }

    //let fullname = jQuery("#txtFullName").val();
    //let sClass = jQuery("#txtClass").val();
    //let gender = GetGender();
    //let emailID = jQuery("#txtEmailID").val();
    //let phoneNum = jQuery("#txtPhoneNum").val();
    //let username = jQuery("#txtUsername").val();
    //let password = jQuery("#txtPassword").val();
    //let courses = SetCoursesInCoursestbl();

    let fullname = StudentRegistrationViewModel.FullName();
    let sClass = StudentRegistrationViewModel.Class();
    let gender = StudentRegistrationViewModel.Gender();
    let emailID = StudentRegistrationViewModel.EmailId();
    let phoneNum = StudentRegistrationViewModel.PhoneNum();
    let username = StudentRegistrationViewModel.UserName();
    let password = StudentRegistrationViewModel.Password();
    let courses = StudentRegistrationViewModel.SelectedCourses();

    let StudentData = {};
    StudentData.Fullname = fullname;
    StudentData.Class = sClass;
    StudentData.Gender = gender;
    StudentData.EmailID = emailID;
    StudentData.PhoneNum= phoneNum;
    StudentData.Username= username;
    StudentData.Password = password;
    StudentData.CourseId = courses;
   

    $.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentRegistrationService.asmx/RegisterStudent",
        data: JSON.stringify({ student: StudentData }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let errorData = response.d;
            if (errorData.ErrorCode === 1) {
                jQuery("#lblMsg").text(errorData.ErrorMsg).css("color", "red");
                //alert(errorData.ErrorMsg);
            } else {
                jQuery("#lblMsg").text("Student registered successfully!").css("color", "green");
                //alert("Student registered successfully!");
                //alert("Registration failed. Email ID might already exist.");
            }
            ClearTxtbox();
            ClearErrorSpans();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });

}

//function GetGender() {
//    let GenderList = jQuery('[name="Genderrbtn"]');

//    for (let i = 0; i < GenderList.length; i++) {
//        if (GenderList[i].checked) {
//            return GenderList[i].value;
//        }
//    }
//    return null;
//}

//function SetCoursesInCoursestbl() {
//    var SelectedCourseId = [];
//    //let checkboxes = document.querySelectorAll('input[name="courses"]:checked');
//    let checkboxes = jQuery('input[name="courses"]:checked');
//    //console.log("Checkboxes found:", checkboxes);
//    //console.log("Type:", typeof checkboxes);

//    for (var i = 0; i < checkboxes.length; i++) {
//        //SelectedCourseId.push(checkboxes[i].value);

//        SelectedCourseId.push({
//            CourseId: parseInt(checkboxes[i].value)
//        });
//    }
//    return SelectedCourseId;
//}

function ClearTxtbox() {

    jQuery("#txtFullName").val("");
    jQuery("#txtClass").val("");
    DeselectGenderList();
    DeselectCourseList();
    jQuery("#txtEmailID").val("");
    jQuery("#txtPhoneNum").val("");
    jQuery("#txtUsername").val("");
    jQuery("#txtPassword").val("");
}

function DeselectGenderList() {
    let genderList = StudentRegistrationViewModel.Gender("");

    //let GenderList = document.getElementsByName("Genderrbtn");
    //let GenderList = jQuery('[name="Genderrbtn"]');

    //for (let i = 0; i < GenderList.length; i++) {
    //    if (GenderList[i].checked) {
    //        GenderList[i].checked = false;
    //    }
    //}

}

function DeselectCourseList() {
    //let CourseList = jQuery('[name="courses"]');
    let courseList = StudentRegistrationViewModel.SelectedCourses([]);
    //CourseList.prop('checked', false); // jQuery way to uncheck all
}

function FormValidation() {

    //let fullname = jQuery("#txtFullName").val().trim();
    //let sClass = jQuery("#txtClass").val().trim();
    //let gender = GetGender();
    //let emailID = jQuery("#txtEmailID").val().trim();
    //let phoneNum = jQuery("#txtPhoneNum").val().trim();
    //let username = jQuery("#txtUsername").val().trim();
    //let password = jQuery("#txtPassword").val().trim();
    //let courses = SetCoursesInCoursestbl();

    let fullname = StudentRegistrationViewModel.FullName();
    let sClass = StudentRegistrationViewModel.Class();
    let gender = StudentRegistrationViewModel.Gender();
    let emailID = StudentRegistrationViewModel.EmailId();
    let phoneNum = StudentRegistrationViewModel.PhoneNum();
    let username = StudentRegistrationViewModel.UserName();
    let password = StudentRegistrationViewModel.Password();
    let courses = StudentRegistrationViewModel.SelectedCourses();

    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var phoneNumPattern = /^\d{10}$/;
    var classRegex = /^\d{1,2}/;

    let isValid = true;

    if (fullname === "") {
        jQuery("#FullNameValidationMsg").text("Fullname is Required. Please Enter.");
        isValid = false;
    }
    //else {
    //    isValid = true;
    //}

    if (sClass === undefined) {
        jQuery("#ClassValidationMsg").text("Class is Required. Please Enter.");
        isValid = false;
    }
    else if (!classRegex.test(sClass)) {
        jQuery("#ClassValidationMsg").text("Please Enter Correct Digit.");
        isValid = false;
    }

    if (gender === "") {
        jQuery("#GenderValidationMsg").text("Gender is Required. Please Enter.");
        isValid = false;
    }

    if (courses.length === 0) {
        jQuery("#CoursesValidationMsg").text("Courses are Required. Please Enter.");
        isValid = false;
    }

    if (emailID === "") {
        jQuery("#EmailValidationMsg").text("Email ID is Required. Please Enter.");
        isValid = false;
    }
    else if (!emailPattern.test(emailID)) {
        jQuery("#EmailValidationMsg").text("Incorrect Email format.Please Check.");
        isValid = false;
    }

    if (phoneNum === "") {
        jQuery("#PhoneNumValidationMsg").text("Phone Number is Required. Please Enter.");
        isValid = false;
    }
    else if (!phoneNumPattern.test(phoneNum)) {
        jQuery("#PhoneNumValidationMsg").text("Invalid Phone Number.Please Check.");
        isValid = false;
    }

    if (username === "") {
        jQuery("#UserNameValidationMsg").text("Username is Required. Please Enter.");
        isValid = false;
    }

    if (password === "") {
        jQuery("#PasswordValidationMsg").text("Password is Required. Please Enter.");
        isValid = false;
    }

    return isValid;
}

function ClearErrorSpans() {
    jQuery("#FullNameValidationMsg").text("");
    jQuery("#ClassValidationMsg").text("");
    jQuery("#GenderValidationMsg").text("");
    jQuery("#CoursesValidationMsg").text("");
    jQuery("#EmailValidationMsg").text("");
    jQuery("#PhoneNumValidationMsg").text("");
    jQuery("#UserNameValidationMsg").text("");
    jQuery("#PasswordValidationMsg").text("");

    jQuery("#errorMsg").text("");
}
