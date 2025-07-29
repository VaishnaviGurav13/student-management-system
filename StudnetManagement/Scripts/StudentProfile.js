
jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
jQuery.BasePathNameSpace.WebServicePath = "";

var StudentProfileViewModel = {

    FullName: ko.observable(""),
    Class: ko.observable(0),
    Gender: ko.observable(""),
    EmailID: ko.observable(""),
    PhoneNum: ko.observable(""),
    //Username: ko.observable(""),
    //Password: ko.observable(""),

    Courses: ko.observableArray([]),
    SelectedCourses: ko.observableArray([])
};


function PageLoadFunction(webServicePath) {

    jQuery.BasePathNameSpace.WebServicePath = webServicePath;

    LoadSession();
    LoadCoursesOnPageLoad();
    LoadStudentData();
    /*SelectStudentCourses();*/

    let role = sessionStorage.getItem("Role");
    if (role === "Admin") {

        ReadOnlyData();
    }

    ko.applyBindings(StudentProfileViewModel);
}

function LoadSession() {

    let studentID = sessionStorage.getItem("StudentId");
    let role = sessionStorage.getItem("Role");

    if (studentID === null && role === null) {
        window.location.href = "LoginPage.aspx";
    }
}

function LoadStudentData() {

    let studentID = sessionStorage.getItem("StudentId");
    let role = sessionStorage.getItem("Role");

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentProfileService.asmx/GetStudentData",
        data: JSON.stringify({ studentData: { StudentId: studentID } }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let studentInfo = response.d;

            StudentProfileViewModel.FullName(studentInfo.Fullname);
            StudentProfileViewModel.Class(studentInfo.Class);
            StudentProfileViewModel.Gender(studentInfo.Gender);
            StudentProfileViewModel.EmailID(studentInfo.EmailID);
            StudentProfileViewModel.PhoneNum(studentInfo.PhoneNum);

            StudentProfileViewModel.SelectedCourses(studentInfo.CourseId);
            console.log(StudentProfileViewModel.SelectedCourses());
            //StudentProfileViewModel.Username = StudentInfo.Username;
            //StudentProfileViewModel.Password(StudentInfo.Password);

            //SelectStudentGender(StudentInfo.Gender);
            //SelectStudentCourses();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText);
            //alert("Error: " + xhr.responseText);
        }
    });

}


//function SelectStudentGender(Gender) {
//    let StudentGender = Gender;

//    let Genderbtn = jQuery('input[name="Genderrbtn"]');
//    for (let i = 0; i < Genderbtn.length; i++) {
//        if (jQuery(Genderbtn[i]).val() === StudentGender) {
//            Genderbtn[i].checked = true;
//        }
//    }
//}

function LoadCoursesOnPageLoad() {

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentRegistrationService.asmx/LoadCourses",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let courses = response.d;
            StudentProfileViewModel.Courses(courses);
            

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
        error: function (xhr) {
            jQuery("#errorMsg").text("Error: " + xhr.responseText);
            //alert("Error: " + xhr.responseText);
        }
    });

    
    //SelectStudentCourses();
}

//function SelectStudentCourses() {

//    let studentID = sessionStorage.getItem("StudentId");

//    jQuery.ajax({
//        type: "POST",
//        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentProfileService.asmx/GetSelectedCourses",
//        data: JSON.stringify({ studentData: { StudentId: studentID } }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {


//            let courseList = response.d;
//            StudentProfileViewModel.SelectedCourses(courseList);
//            console.log(StudentProfileViewModel.SelectedCourses());

//            //let CoursesCheckbox = jQuery('input[name="courses"]');
//            //for (let i = 0; i < CoursesCheckbox.length; i++) {

//            //    let Checkbox = CoursesCheckbox[i];
//            //    let CourseId = parseInt(Checkbox.value);

//            //    if (CourseList.includes(CourseId)) {
//            //        Checkbox.checked = true;
//            //    }

//            //}
//        },
//        error: function (xhr) {
//            alert("Error: " + xhr.responseText);
//        }
//    });
//}

function UpdatebtnClick() {

    ClearErrorSpans();
    let isValid = FormValidation();

    if (!isValid) {
        alert("Please Check All the fields");
        return;
    }

    //let StudentId = sessionStorage.getItem("StudentId");
    //let Fullname = jQuery("#txtFullName").val();
    //let Class = jQuery("#txtClass").val();
    //let Gender = GetGender();
    //let CourseId = GetCourses();
    //let Email = jQuery("#txtEmailID").val();
    //let PhoneNumber = jQuery("#txtPhoneNum").val();
    //let Username = jQuery("#txtUsername").val();
    //let Password = jQuery("#txtPassword").val();

    let isUpdate = confirm("Are you Sure. You want to Update " + StudentProfileViewModel.FullName() + "'s Data.");

    if (isUpdate) {

        let studentId = sessionStorage.getItem("StudentId");
        let fullname = StudentProfileViewModel.FullName();
        let sClass = StudentProfileViewModel.Class();
        let gender = StudentProfileViewModel.Gender();
        let courseId = StudentProfileViewModel.SelectedCourses();
        let email = StudentProfileViewModel.EmailID();
        let phoneNumber = StudentProfileViewModel.PhoneNum();
        //let Username = jQuery("#txtUsername").val();
        //let Password = StudentProfileViewModel.Password();

        let updatedData = {};
        
        updatedData.StudentId = studentId;
        updatedData.Fullname = fullname;
        updatedData.Class = sClass;
        updatedData.Gender = gender;
        updatedData.CourseId = courseId;
        updatedData.EmailID = email;
        updatedData.PhoneNum = phoneNumber;
        //updatedData.username = Username;
        //updatedData.password = Password;
        

        jQuery.ajax({
            type: "POST",
            url: jQuery.BasePathNameSpace.WebServicePath + "/StudentProfileService.asmx/UpdateStudentData",
            data: JSON.stringify({ studentData: updatedData }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                let isUpdated = response.d;

                if (isUpdated) {
                    jQuery("#lblmsg").text("Data Updated Successfully.").css("color", "green");
                    //alert("Data Updated Successfully.");
                }
                else {
                    jQuery("#lblmsg").text("Failed to Update the data!!").css("color", "red");
                    //alert("Failed to Update the data!!");
                }
            },
            error: function (xhr) {
                jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
                console.log(xhr.responseText);
                //alert("Error: " + xhr.responseText);
            }
        })
    }
}

//function GetGender() {
//    let GenderList = jQuery('input[name="Genderrbtn"]');

//    for (let i = 0; i < GenderList.length; i++) {
//        if (GenderList[i].checked) {
//            return jQuery(GenderList[i]).val();
//        }
//    }
//    return null;
//}

//function GetCourses() {
//    var SelectedCourseId = [];
//    let checkboxes = jQuery('input[name="courses"]:checked');

//    for (var i = 0; i < checkboxes.length; i++) {

//        SelectedCourseId.push({
//            CourseId: parseInt(checkboxes[i].value)
//        });
//    }
//    return SelectedCourseId;
//}

function ReadOnlyData() {
    jQuery("#txtFullName").prop("readonly", true);
    jQuery("#txtClass").prop("readonly", true);
    jQuery('input[name="Genderrbtn"]').prop("disabled", true);
    //jQuery("#checkboxContainer input[type='courses']").prop("disabled", true);
    //jQuery('input[name="courses"]').prop("disabled", true);
    jQuery("#txtEmailID").prop("readonly", true);
    jQuery("#txtPhoneNum").prop("readonly", true);
    //jQuery("#txtUsername").prop("readonly", true);
    //jQuery("#txtPassword").prop("readonly", true);
    jQuery("#btnUpdate").hide();
    //DisableCourses();
    //let checkBoxList = jQuery('input[name="courses"]');
    //let checkBoxList = jQuery('input[name="courses"]:checked');
    //for (let i = 0; i < checkBoxList.length; i++) {
    //    checkBoxList[i].disabled = true;
    //}
}

//function DisableCourses() {
//    //var SelectedCourseId = [];
//    let checkboxes = jQuery('input[name="courses"]');

//    for (var i = 0; i < checkboxes.length; i++) {
//        if (checkboxes[i].checked)
//            checkboxes[i].disabled = true;
//    }
    
//}

function FormValidation() {

    //let StudentId = sessionStorage.getItem("StudentId");
    //let Fullname = jQuery("#txtFullName").val();
    //let Class = jQuery("#txtClass").val();
    //let Gender = GetGender();
    //let Courses = StudentProfileViewModel.Courses();
    ////let Courses = GetCourses();
    //let Email = jQuery("#txtEmailID").val();
    //let PhoneNumber = jQuery("#txtPhoneNum").val();
    //let Username = jQuery("#txtUsername").val();
    //let Password = jQuery("#txtPassword").val();

    let fullname = StudentProfileViewModel.FullName();
    let sClass = StudentProfileViewModel.Class();
    let gender = StudentProfileViewModel.Gender();
    let courses = StudentProfileViewModel.SelectedCourses();
    let email = StudentProfileViewModel.EmailID();
    let phoneNumber = StudentProfileViewModel.PhoneNum();
    
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var phoneNumPattern = /^\d{10}$/;
    var classRegex = /^\d{1,2}/;

    let isValid = true;

    if (fullname === "") {
        jQuery("#FullNameValidationMsg").text("Fullname is Required. Please Enter.");
        isValid = false;
    }

    if (sClass === "") {
        jQuery("#ClassValidationMsg").text("Class is Required. Please Enter.");
        isValid = false;
    }
    else if (!classRegex.test(sClass)) {
        jQuery("#ClassValidationMsg").text("Please Enter Correct Digit.");
        isValid = false;
    }

    if (gender === null) {
        jQuery("#GenderValidationMsg").text("Gender is Required. Please Enter.");
        isValid = false;
    }

    if (courses.length === 0) {
        jQuery("#CoursesValidationMsg").text("Courses are Required. Please Enter.");
        isValid = false;
    }

    if (email === "") {
        jQuery("#EmailValidationMsg").text("Email ID is Required. Please Enter.");
        isValid = false;
    }
    else if (!emailPattern.test(email)) {
        jQuery("#EmailValidationMsg").text("Incorrect Email format.Please Check.");
        isValid = false;
    }

    if (phoneNumber === "") {
        jQuery("#PhoneNumValidationMsg").text("Phone Number is Required. Please Enter.");
        isValid = false;
    }
    else if (!phoneNumPattern.test(phoneNumber)) {
        jQuery("#PhoneNumValidationMsg").text("Invalid Phone Number.Please Check.");
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
}