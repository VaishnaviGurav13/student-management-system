<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="StudnetManagement.WebForm4" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script src="<%=ConfigurationManager.AppSettings["jqueryPath"] %>"></script>
    <script src="<%=ConfigurationManager.AppSettings["knockOutJsPath"] %>"></script>

    <%--<script type="text/javascript">
        jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
        jQuery.BasePathNameSpace.WebServicePath = '<%= System.Configuration.ConfigurationManager.AppSettings["WebServicePath"] %>';
    </script>--%>

    <div id="AdminContent">
        <center>
            <h1><%=GetGlobalResourceObject("AdminDashboard_Resource", "AdminDashboardPageHeading") %></h1>
            <br />
            <br />
            <button id="TotalStudentbtn" class="btnControl" type="button" data-bind="click: TotalStudentBtnClick"></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button id="PendingStudentbtn" class="btnControl" type="button" data-bind="click: PendingStudentbtnClick"></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button id="AcceptedStudentbtn" class="btnControl" type="button" data-bind="click: AcceptedStudentbtnClick"></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button id="RejectedStudentbtn" class="btnControl" type="button" data-bind="click: RejectedStudentbtnClick"></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button id="ManageCoursesbtn" class="btnControl" type="button" data-bind="click: ManageCoursesbtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "ManageCoursesbtn") %></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </center>
        <br />
        <br />
        <center>
            <h2 id="StudentStatus" style="color: blue; font-weight: bold;"></h2>
            <div id="RecordMsg" style="font-size: x-large; color: crimson; font-weight: bold;"></div>
        </center>
        <center>
            <div id="studentTable">
                <table border="1" id="studentTable1">
                    <thead>
                        <tr>
                            <%--<th><%=GetGlobalResourceObject("AdminDashboard_Resource", "StudentIdtblHead") %></th>--%>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "StudentNametblHead") %></th>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "ClasstblHead") %></th>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "GendertblHead") %></th>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "EmailIdtblHead") %></th>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "StatustblHead") %></th>
                            <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "ActiontblHead") %></th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: StudentViewModel.studentDetails">
                        <tr>
                            <%--<td data-bind="text: StudentId"></td>--%>
                            <td data-bind="text: Fullname"></td>
                            <td data-bind="text: Class"></td>
                            <td data-bind="text: Gender"></td>
                            <td data-bind="text: EmailID"></td>
                            <td data-bind="text: Status"></td>
                            <td>
                                <center>
                                    <%--<button id="Acceptbtn" class="AcceptbtnType" type="button" data-bind="click: $parent.acceptBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "AcceptBtnTbl") %></button>--%>
                                    <button id="Acceptbtn" class="AcceptbtnType" type="button" data-bind="click: AcceptBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "AcceptBtnTbl") %></button>
                                    <button id="Rejectbtn" class="RejectbtnType" type="button" data-bind="click: RejectBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "RejectBtnTbl") %></button>
                                    <button id="Deletebtn" class="DeletebtnType" type="button" data-bind="click: DeleteBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "DeleteBtnTbl") %></button>
                                    <button id="Viewbtn" type="button" data-bind="click: ViewBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "ViewBtnTbl") %></button>
                                </center>

                                <%--, visible: Status==='R' && StudentViewModel.btnClicked
                                , visible: Status==='A' && StudentViewModel.btnClicked
                                , visible: Status==='P' && StudentViewModel.btnClicked
                                --%>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <br />
                <button type="button" id="PreviousBtn" data-bind="click: PreviousBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "PreviousBtn") %></button>
                <button type="button" id="NextBtn" data-bind="click: NextBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "NextBtn") %></button>
            </div>
        </center>
    </div>
    <br />
    <br />
    <center>
        <label id="lblMsg" class="lblControl"></label>
        <span id="errorMsg"></span>
    </center>

    <br />
    <br />
    <br />

    <center>
        <div id="ManageCourses">
            <input type="text" id="txtAddCourses" class="txtBoxControl" data-bind="value: StudentViewModel.newCourse" />
            <span id="AddCoursesValidationMsg" style="color: red"></span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button type="button" id="AddCourses" data-bind="click: AddCourseBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "AddCourseBtn") %></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button type="button" id="ViewCourses" data-bind="click: ViewCourseBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "ViewCoursesBtn") %></button>
            <br />
            <br />
            <center>
                <label id="courseLblMsg" class="lblControl"></label>
                <span id="courseErrorMsg"></span>
            </center>
            <br />
            <br />
            <table border="1" id="courseTable">
                <thead>
                    <tr>
                        <%--<th><%=GetGlobalResourceObject("AdminDashboard_Resource", "CourseIdtblHead") %></th>--%>
                        <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "CourseNametblHead") %></th>
                        <th><%=GetGlobalResourceObject("AdminDashboard_Resource", "DeleteCoursetblHead") %></th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: StudentViewModel.courseList">
                    <tr>
                        <%--<td data-bind="text: CourseId"></td>--%>
                        <td data-bind="text: CourseName"></td>
                        <td>
                            <center>
                                <button type="button" id="DeleteCourses" data-bind="click: DeleteCourseBtnClick"><%=GetGlobalResourceObject("AdminDashboard_Resource", "DeleteCourseBtnTbl") %></button>
                            </center>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </center>

    <script src="<%=ConfigurationManager.AppSettings["AdminDashboardJsPath"].TrimEnd('/') %>/AdminDashboard.js"></script>

</asp:Content>
