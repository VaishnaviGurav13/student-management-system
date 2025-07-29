<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="StudnetManagement.WebForm2" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script src="<%=ConfigurationManager.AppSettings["jqueryPath"] %>"></script>
    <script src="<%=ConfigurationManager.AppSettings["knockOutJsPath"] %>"></script>

    <%--<script type="text/javascript">
        jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
        jQuery.BasePathNameSpace.WebServicePath = '<%= System.Configuration.ConfigurationManager.AppSettings["WebServicePath"] %>';
    </script>--%>

    <div>
        <center>
            <h1><%=GetGlobalResourceObject("StudentRegistration_Resource","StudentRegistrationPageHeading") %></h1>
        </center>
        <br />
        <br />
        <center>
            <label id="lblFullName" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","FullNamelbl") %></label>
            &nbsp;&nbsp;&nbsp;
            <input type="text" id="txtFullName" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.FullName" />
            <span id="FullNameValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblClass" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","Classlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtClass" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.Class" />
            <span id="ClassValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblGender" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","Genderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="MaleRadio" type="radio" name="Genderrbtn" value="Male" data-bind="checked: StudentRegistrationViewModel.Gender" />
            <label id="lblMale" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","MaleGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="FemaleRadio" type="radio" name="Genderrbtn" value="Female" data-bind="checked: StudentRegistrationViewModel.Gender" />
            <label id="lblFemale" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","FemaleGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input id="OthersRadio" type="radio" name="Genderrbtn" value="Others" data-bind="checked: StudentRegistrationViewModel.Gender" />
            <label id="lblOthers" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","OthersGenderlbl") %></label>
            &nbsp;&nbsp;&nbsp;
            <span id="GenderValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblCourses" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","Courseslbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <%--<div id="checkboxContainer" type="checkbox">--%>
                <div id="checkboxContainer" data-bind="foreach: Courses">
                    <input type="checkbox" data-bind="attr: {id: 'course_' + CourseId}, value: CourseId, checked: $parent.SelectedCourses" />
                    <label data-bind="attr: {for: 'course_' + CourseId}, text: CourseName"></label>
                </div>
            <span id="CoursesValidationMsg" style="color: red"></span>
        </center>
        <center>
            <div id="loader" style="display: none;height: 50px; width: 200px; background-color: yellow; color: red">Loading..... Please wait!!</div>
        </center>
        <br />
        <br />
        <center>
            <label id="lblEmailID" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","EmailIDlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtEmailID" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.EmailId" />
            <span id="EmailValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblPhoneNum" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","PhoneNumberlbl") %></label>
            &nbsp;&nbsp;&nbsp;

                +91<input type="text" maxlength="10" pattern="\d" title="Num is required" id="txtPhoneNum" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.PhoneNum" />
            <span id="PhoneNumValidationMsg" style="color: red"></span>
           
        </center>
        <br />
        <br />
        <center>
            <label id="lblUsername" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","Usernamelbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="text" id="txtUsername" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.UserName" />
            <span id="UserNameValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <label id="lblPassword" class="lblControl"><%=GetGlobalResourceObject("StudentRegistration_Resource","Passwordlbl") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="password" id="txtPassword" class="txtboxControl" data-bind="value: StudentRegistrationViewModel.Password" />
            <span id="PasswordValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <button id="btnSubmit" type="button" data-bind="click: SubmitClick" ><%=GetGlobalResourceObject("StudentRegistration_Resource","Submitbtn") %></button>
            <%--<input id="btnSubmit" type="button" value="Submit" />--%>
        </center>
        <br />
        <br />
        <center>
            <label id="lblMsg" class="lblControl"></label>
            <span id="errorMsg"></span>
        </center>
    </div>

    <script src="<%=ConfigurationManager.AppSettings["studentRegistrationJsPath"].TrimEnd('/') %>/StudentRegistration.js"></script>
    
</asp:Content>
