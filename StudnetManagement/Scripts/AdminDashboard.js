
jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
jQuery.BasePathNameSpace.WebServicePath = "";


var StudentViewModel = {

    studentDetails: ko.observableArray([]),
    courseList: ko.observableArray([]),

    pageSize: ko.observable(5),
    studentCount: ko.observable(0),
    currentPage: ko.observable(0),
    totalPage: ko.observable(0),
    studentStatus: ko.observable(),

    newCourse: ko.observable(''),

    //isDelete: ko.observable(false),
    //isReject: ko.observable(false),
    //isAccept: ko.observable(false),
    //btnClicked: ko.observable(),

    statusHeading: ko.observable(''),
    totalSCount: ko.observable(0),
    pendingSCount: ko.observable(0),
    acceptedSCount: ko.observable(0),
    rejectedSCount: ko.observable(0),
    coursesCount: ko.observable(0),
};


function pageLoadFunction(webServicePath) {

    jQuery.BasePathNameSpace.WebServicePath = webServicePath;
    console.log(jQuery.BasePathNameSpace.WebServicePath);

    LoadSession();
    LoadStudentCount();
    jQuery("#studentTable").hide();
    jQuery("#courseTable").hide();
    jQuery("#ManageCourses").hide();
    LoadCourses();
    
    ko.applyBindings(StudentViewModel);
    //knockOutBinding();
    //ko.applyBindings(StudentViewModel, document.getElementById("AdminContent"));
    //ko.applyBindings(StudentViewModel, document.getElementById("ManageCourses"));

}

function LoadSession() {

    let role = sessionStorage.getItem("Role");

    if (role === null) {
        window.location.href = "LoginPage.aspx";
    }
}

function LoadStudentCount() {

    //console.log("Service path: ", jQuery.BasePathNameSpace);

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/GetCountOfStudent",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let studentsCount = response.d;

            StudentViewModel.totalSCount = studentsCount.TotalStudentCount;
            StudentViewModel.pendingSCount = studentsCount.PendingStudentCount;
            StudentViewModel.acceptedSCount = studentsCount.AcceptedStudentCount;
            StudentViewModel.rejectedSCount = studentsCount.RejectedStudentCount;
            StudentViewModel.coursesCount = studentsCount.TotalCourseCount;

            jQuery("#TotalStudentbtn").text("Total Students ( " + StudentViewModel.totalSCount + " )");
            jQuery("#PendingStudentbtn").text("Pending Students ( " + StudentViewModel.pendingSCount + " )");
            jQuery("#AcceptedStudentbtn").text("Accepted Students ( " + StudentViewModel.acceptedSCount + " )");
            jQuery("#RejectedStudentbtn").text("Rejected Students ( " + StudentViewModel.rejectedSCount + " )");
            jQuery("#ManageCoursesbtn").text("Manage Courses (" + StudentViewModel.coursesCount + " )");

            //EnableDisableBtn();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });

}

function LoadStudents(Status, Offset) {

    //UpdateValue();
    //EnableDisableBtn();
    jQuery("#lblMsg").text("");
    jQuery("#errorMsg").text("");
    jQuery("#courseLblMsg").text("");
    jQuery("#courseErrorMsg").text("");

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/GetDataFromUsertbl",
        data: JSON.stringify({ student: { status: Status }, offSet: Offset, limit: StudentViewModel.pageSize() }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            let adminProp = response.d;
            let StudentData = adminProp.StudentData;
            jQuery("#RecordMsg").text("");
            
            if (StudentData.length === 0) {
                jQuery("#studentTable").hide();
                jQuery("#RecordMsg").text("No Record Found!!");
                return;
            }
            jQuery("#studentTable").show();
            
            //self.studentDetails(StudentData);
            StudentViewModel.studentDetails(StudentData);

            if (Status === "P") {
                jQuery(".AcceptbtnType").show();
                jQuery(".RejectbtnType").show();
                jQuery(".DeletebtnType").show();
            }
            else if (Status === "A") {
                jQuery(".AcceptbtnType").hide();
                jQuery(".RejectbtnType").show();
                jQuery(".DeletebtnType").show();
            }
            else if (Status === "R") {
                jQuery(".AcceptbtnType").show();
                jQuery(".RejectbtnType").hide();
                jQuery(".DeletebtnType").show();
            }
            else {
                jQuery(".AcceptbtnType").hide(); 
                jQuery(".RejectbtnType").hide(); 
                jQuery(".DeletebtnType").show(); 
            }
            EnableDisableBtn();
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });
}

function TotalStudentBtnClick() {

    //jQuery("#studentTable").show();
    StudentViewModel.currentPage(0);
    StudentViewModel.studentCount(StudentViewModel.totalSCount); 

    StudentViewModel.studentStatus(null);
    LoadStudents(null, 0);

}

function PendingStudentbtnClick() {
    //jQuery("#studentTable").show();
    //tableToggle();
    StudentViewModel.currentPage(0);
    StudentViewModel.studentCount(StudentViewModel.pendingSCount);
    
    StudentViewModel.studentStatus("P");
    LoadStudents("P", 0);
}

function AcceptedStudentbtnClick() {
    //jQuery("#studentTable").show();
    //tableToggle();
    StudentViewModel.currentPage(0);
    StudentViewModel.studentCount(StudentViewModel.acceptedSCount);
    
    StudentViewModel.studentStatus("A");
    LoadStudents("A", 0);

}

function RejectedStudentbtnClick() {
    //jQuery("#studentTable").show();
    //tableToggle();
    StudentViewModel.currentPage(0);
    StudentViewModel.studentCount(StudentViewModel.rejectedSCount);
    
    StudentViewModel.studentStatus("R");
    LoadStudents("R", 0);

}

function ManageCoursesbtnClick() {

    jQuery("#lblMsg").text("");
    jQuery("#errorMsg").text("");
    jQuery("#courseLblMsg").text("");
    jQuery("#courseErrorMsg").text("");

    jQuery("#ManageCourses").toggle();
}

//self.loadStudents();

function ChangeStudentStatus(studentInfo, status) {

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/UpdateStudentStatus",
        data: JSON.stringify({ studentData: { StudentId: studentInfo.StudentId, status: status } }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            let isDataUpdated = response.d;
            //let studentStatus = status;

            if (isDataUpdated) {

                LoadStudentCount();
                //StudentViewModel.currentPage(0);

                if (status === "A") {
                    //alert("Do you want to Accept " + studentInfo.Fullname);
                    //LoadStudentCount();
                    //StudentViewModel.currentPage(0);
                    //LoadStudents("A", 0);
                    AcceptedStudentbtnClick();
                    jQuery("#lblMsg").text("Student Accepted Successfully.").css("color", "green");
                }
                else if (status === "R") {
                    //alert("Do you want to Reject " + studentInfo.Fullname);
                    //LoadStudentCount();
                    //StudentViewModel.currentPage(0);
                    //LoadStudents("R", 0);
                    RejectedStudentbtnClick();
                    jQuery("#lblMsg").text("Student Rejected Successfully.").css("color", "green");
                }
                else if (status === "I") {
                    //alert("Student Deleted Successfully ");
                    //LoadStudentCount();
                    //StudentViewModel.currentPage(0);
                    //LoadStudents(null, 0);
                    TotalStudentBtnClick();
                    jQuery("#lblMsg").text("Student Deleted Successfully.").css("color", "green");
                }
            }
            else {
                //alert("Failed to Update data. Please try again!!");
                jQuery("#lblMsg").text("Failed to Change the Status. Please try again!!").css("color", "red");
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    })
}

//function DeleteStudentData(studentInfo) {

//    jQuery.ajax({
//        type: "POST",
//        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/DeleteStudent",
//        data: JSON.stringify({ studentData: { StudentId: studentInfo.StudentId } }),
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {
//            let isDeleted = response.d;

//            if (isDeleted) {
//                alert(studentInfo.Fullname + " Deleted Successfully.");
//            }
//            else {
//                alert(" Failed to Delete the User. Please try again!!");
//            }
//            LoadStudentCount();
//            LoadStudents(null, 0);
//        },
//        error: function (xhr) {
//            console.log(xhr.responseText);
//            alert("Error: " + xhr.responseText);
//        }
//    })
//}

function AcceptBtnClick(student) {
    let isAccepted = confirm("Do you want to Accept " + student.Fullname);
    if (isAccepted)
        ChangeStudentStatus(student, "A");
}

function RejectBtnClick(student) {
    let isRejected = confirm("Do you want to Reject " + student.Fullname);
    if (isRejected)
        ChangeStudentStatus(student, "R");
}

function DeleteBtnClick(student) {
    let isDelete = confirm("Are you sure you want to delete " + student.Fullname);

    if (isDelete) {
        //DeleteStudentData(student);
        ChangeStudentStatus(student, "I");
    }
    else {
        jQuery("#lblMsg").text("Failed to delete user. Please try Again!").css("color", "red");
        //alert("Failed to delete user.Please try Again!");
    }

    //LoadStudentCount();
    //LoadStudents(null, 0);
}

function ViewBtnClick(student) {
    let role = sessionStorage.getItem("Role");
    sessionStorage.setItem("Role", role);
    sessionStorage.setItem("StudentId", student.StudentId);
    window.location.href = "StudentProfile.aspx";
}


function LoadCourses() {

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/StudentRegistrationService.asmx/LoadCourses",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            let courseList = response.d;
            StudentViewModel.courseList(courseList);
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#errorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });
}

function AddCourseBtnClick() {

    jQuery("#lblMsg").text("");
    jQuery("#errorMsg").text("");
    jQuery("#courseLblMsg").text("");
    jQuery("#courseErrorMsg").text("");
    let courseName = StudentViewModel.newCourse();

    let isValid = CourseValidation();
    if (!isValid) {
        return;
    }
    jQuery("#AddCoursesValidationMsg").text("");

    //let courseName = $("#txtAddCourses").val().trim();
    

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/InsertDataCoursetbl",
        data: JSON.stringify({ coursesData: { CourseName: courseName } }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            let errorData = response.d;

            if (errorData.ErrorCode === 1) {
                //alert(errorData.ErrorMsg);
                jQuery("#courseLblMsg").text(errorData.ErrorMsg).css("color", "red");
                StudentViewModel.newCourse('');
                //jQuery("#txtAddCourses").val("");
            }
            else {
                //alert("Course Added Successfully.");
                LoadStudentCount();
                LoadCourses();
                jQuery("#courseLblMsg").text("Course Added Successfully.").css("color", "green");
                StudentViewModel.newCourse('');
                //jQuery("#txtAddCourses").val("");
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#courseErrorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });

}

function ViewCourseBtnClick() {
    jQuery("#lblMsg").text("");
    jQuery("#errorMsg").text("");
    jQuery("#courseLblMsg").text("");
    jQuery("#courseErrorMsg").text("");

    jQuery("#courseTable").toggle();
}


function DeleteCourseBtnClick(course) {
    jQuery("#lblMsg").text("");
    jQuery("#errorMsg").text("");
    jQuery("#courseLblMsg").text("");
    jQuery("#courseErrorMsg").text("");

    let isCourseDeleted = confirm("Are You sure you want to Delete the course.");
    if (!isCourseDeleted) {
        //alert("Failed to Delete the course");
        jQuery("#courseLblMsg").text("Failed to Delete the course.").css("color", "red");
        return;
    }

    jQuery.ajax({
        type: "POST",
        url: jQuery.BasePathNameSpace.WebServicePath + "/AdminDashboardService.asmx/DeleteCourse",
        data: JSON.stringify({ coursesData: { CourseId: course.CourseId, CourseName: course.CourseName } }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            let isCourseDeleted = response.d;

            if (isCourseDeleted) {
                //alert("Course Deleted Successfully.");
                jQuery("#courseLblMsg").text("Course Deleted Successfully.").css("color", "green");
                LoadStudentCount();
                LoadCourses();
            }
            else {
                jQuery("#courseLblMsg").text("Failed to Delete the Course. Please Try Again!!").css("color", "red");
                //alert("Failed to Delete the Course. Please Try Again!!");
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            jQuery("#courseErrorMsg").text("Error: " + xhr.responseText).css("color", "red");
            //alert("Error: " + xhr.responseText);
        }
    });
}


//function TableToggle() {
//    //$("#studentTable").hide();
//    jQuery("#studentTable").toggle();
//}

function CourseValidation() {

    let courseName = StudentViewModel.newCourse();
    //let courseName = jQuery("#txtAddCourses").val().trim();
    let courseNameRegex = /^[A-Za-z ]+$/;
    let isValid = true;

    if (courseName === " ") {
        jQuery("#AddCoursesValidationMsg").text("Please Enter a Course Name.");
        isValid = false;
    }
    else if (!courseNameRegex.test(courseName)) {
        jQuery("#AddCoursesValidationMsg").text("Please Enter a Valid Course Name.");
        isValid = false;
    }
    return isValid;
}

//function NextBtnClick() {
//    let offSetVal = (StudentViewModel.currentPage() + 1) * 10;
//    let status = StudentViewModel.studentStatus();
//    let Count = 0;

//    jQuery("#PreviousBtn").prop("disabled", false);

//    StudentViewModel.currentPage(StudentViewModel.currentPage() + 1);

//    if (status === null) {
//        Count = StudentViewModel.totalSCount;
//    }
//    else if (status === "P") {
//        Count = StudentViewModel.pendingSCount;
//    }
//    else if (status === "A") {
//        Count = StudentViewModel.acceptedSCount;
//    }
//    else if (status === "R") {
//        Count = StudentViewModel.rejectedSCount;
//    }
//    StudentViewModel.studentCount(Count);

//    //if (offSetVal < Count) {
//    if ((StudentViewModel.currentPage() * 10) <= Count) {
//        loadStudents(status, offSetVal);
//    }
//    else {
//        jQuery("#NextBtn").prop("disabled", true);
//    }

//    //StudentViewModel.currentPage(StudentViewModel.currentPage() + 1);
//    UpdateValue();
//}

//function PreviousBtnClick() {
//    let offSetVal = (StudentViewModel.currentPage() - 1) * 10;
//    let status = StudentViewModel.studentStatus();

//    jQuery("#NextBtn").prop("disabled", false);

//    StudentViewModel.currentPage(StudentViewModel.currentPage() - 1);

//    //if (offSetVal < 0) {
//    if (StudentViewModel.currentPage() <= 0) {
//        jQuery("#PreviousBtn").prop("disabled", true);
//    }
//    else {
//        loadStudents(status, offSetVal);
//    }
//    //StudentViewModel.currentPage(StudentViewModel.currentPage() - 1);
//    UpdateValue();
//}

//function UpdateValue() {

//    jQuery("#NextBtn").prop("disabled", (StudentViewModel.currentPage()*10) > StudentViewModel.studentCount());
//    jQuery("#PreviousBtn").prop("disabled", StudentViewModel.currentPage() == 0);
//    //StudentViewModel.currentPage() <= StudentViewModel.studentCount()
//}

function NextBtnClick() {

    let offSet = (StudentViewModel.currentPage() + 1) * StudentViewModel.pageSize();
    let status = StudentViewModel.studentStatus();

    StudentViewModel.currentPage(StudentViewModel.currentPage() + 1);
    //if (StudentViewModel.currentPage() * StudentViewModel.pageSize() < StudentViewModel.studentCount()) {
    if (offSet < StudentViewModel.studentCount()) {
        LoadStudents(status, offSet);
    }
    else {
        EnableDisableBtn();
    }

    //StudentViewModel.currentPage(StudentViewModel.currentPage() + 1);
}

function PreviousBtnClick() {

    let offSet = (StudentViewModel.currentPage() - 1) * StudentViewModel.pageSize();
    let status = StudentViewModel.studentStatus();

    StudentViewModel.currentPage(StudentViewModel.currentPage() - 1);
    if (StudentViewModel.currentPage() >= 0) {
        LoadStudents(status, offSet);
    }
    else {
        EnableDisableBtn();
    }
    
    //StudentViewModel.currentPage(StudentViewModel.currentPage() - 1);
}

function EnableDisableBtn() {

    let totalStudents = StudentViewModel.studentCount();
    let pageSize = StudentViewModel.pageSize();
    let currentPage = StudentViewModel.currentPage();
    let totalPages = Math.ceil(totalStudents / pageSize);

    jQuery("#PreviousBtn").prop("disabled", currentPage === 0);
    jQuery("#NextBtn").prop("disabled", currentPage + 1 === totalPages);
}