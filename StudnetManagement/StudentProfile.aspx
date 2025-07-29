<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="StudnetManagement.WebForm3" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script src="<%=ConfigurationManager.AppSettings["jqueryPath"] %>"></script>
    <script src="<%=ConfigurationManager.AppSettings["knockOutJsPath"] %>"></script>

    <%--<script type="text/javascript">
        jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
        jQuery.BasePathNameSpace.WebServicePath = '<%= System.Configuration.ConfigurationManager.AppSettings["WebServicePath"] %>';
    </script>--%>

    <div>
        <center>
            <h1><%=GetGlobalResourceObject("StudentProfile_Resource", "StudentProfilePageHeading") %></h1>
        </center>
        <br />
        <br />
        <center>
            <label id="lblFullName" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","FullNamelbl") %></label>
            &nbsp;&nbsp;&nbsp;
            <input type="text" id="txtFullName" class="txtboxControl" data-bind="value: StudentProfileViewModel.FullName" />
            <span id="FullNameValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblClass" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","Classlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtClass" class="txtboxControl" data-bind="value: StudentProfileViewModel.Class" />
                <span id="ClassValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <%--<center>
            <label id="lblGender" class="lblControl"><%=GetGlobalResourceObject("StudentProfile","Genderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtGender" class="txtboxControl" />
        </center>
        <br />
        <br />--%>
        <center>
            <label id="lblGender" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","Genderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="MaleRadio" type="radio" name="Genderrbtn" value="Male" data-bind="checked: StudentProfileViewModel.Gender" />
            <label id="lblMale" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","MaleGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="FemaleRadio" type="radio" name="Genderrbtn" value="Female" data-bind="checked: StudentProfileViewModel.Gender" />
            <label id="lblFemale" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","FemaleGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="OthersRadio" type="radio" name="Genderrbtn" value="Others" data-bind="checked: StudentProfileViewModel.Gender" />
            <label id="lblOthers" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","OthersGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
            <span id="GenderValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblCourses" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","Courseslbl") %></label>
            &nbsp;&nbsp;&nbsp;
            <%--<div id="checkboxContainer" type="checkbox" >--%>    
            <div id="checkboxContainer" data-bind="foreach: Courses" >
                    <input type="checkbox" data-bind="attr: {id: 'course_' + CourseId}, value: CourseId, checked: $parent.SelectedCourses" />
                    <label data-bind="attr: {for: 'course_' + CourseId}, text: CourseName"></label>
                </div>
                <span id="CoursesValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblEmailID" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","EmailIDlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtEmailID" class="txtboxControl" data-bind="value: StudentProfileViewModel.EmailID" />
                <span id="EmailValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblPhoneNum" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","PhoneNumberlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                +91<input type="text" id="txtPhoneNum" class="txtboxControl" data-bind="value: StudentProfileViewModel.PhoneNum" />
                <span id="PhoneNumValidationMsg" style="color: red"></span>
        </center>
        <%--<br />
        <br />
        <center>
            <label id="lblUsername" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","Usernamelbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtUsername" class="txtboxControl" />
                <span id="UserNameValidationMsg" style="color: red"></span>
        </center>--%>
        <%--<br />
        <br />
        <center>
            <label id="lblPassword" class="lblControl"><%=GetGlobalResourceObject("StudentProfile_Resource","Passwordlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="password" id="txtPassword" class="txtboxControl" data-bind="value: StudentProfileViewModel.Password" />
                <span id="PasswordValidationMsg" style="color: red"></span>
        </center>--%>
        <br />
        <br />
        <center>
            <button id="btnUpdate" type="button" data-bind="click: UpdatebtnClick" ><%=GetGlobalResourceObject("StudentProfile_Resource","Updatebtn") %></button>
            <%--<input id="btnSubmit" type="button" value="Submit" />--%>
        </center>
        <br />
        <br />
        <center>
            <label id="lblmsg" class="lblControl"></label>
            <span id="errorMsg"></span>
        </center>
    </div>

    <script src="<%=ConfigurationManager.AppSettings["StudentProfileJsPath"].TrimEnd('/') %>/StudentProfile.js"></script>


</asp:Content>
