<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="StudnetManagement.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <script src="<%= ConfigurationManager.AppSettings["jqueryPath"] %>"></script>
    <script src="<%=ConfigurationManager.AppSettings["knockOutJsPath"] %>"></script>
    <script src="<%= ConfigurationManager.AppSettings["loginPageJsPath"].TrimEnd('/') %>/LoginPage.js"></script>

    <%--<script type="text/javascript">
        jQuery.BasePathNameSpace = jQuery.BasePathNameSpace || {};
        jQuery.BasePathNameSpace.WebServicePath = '<%= System.Configuration.ConfigurationManager.AppSettings["WebServicePath"] %>';
    </script>--%>

    <div>
        <h1>welcome to github</h1>
        <center>
            <h1><%=GetGlobalResourceObject("LoginPage_Resource","loginPageHeading") %></h1>
        </center>
        <br />
        <br />
        <center>
            <label id="lblEmailId" class="lblControl"><%=GetGlobalResourceObject("LoginPage_Resource", "EmailIdTxtbox") %></label>
            &nbsp;&nbsp;&nbsp;
            <input type="text" id="txtEmailId" class="txtboxControl" data-bind="value: LoginPageViewModel.EmailId" />
            <span id="EmailValidationMsg" style="color: red"></span>
            <br />
            <br />
        </center>
        <center>
            <label id="lblPassword" class="lblControl"><%=GetGlobalResourceObject("LoginPage_Resource", "PasswordTxtbox") %></label>
            &nbsp;&nbsp;&nbsp;
                <input type="password" id="txtPassword" class="txtboxControl" data-bind="value: LoginPageViewModel.Password" />
            <span id="PasswordValidationMsg" style="color: red"></span>
        </center>
        <br />
        <br />
        <center>
            <%--<input id="btnLogin" type="button" value="Login" onclick="Click()"/>--%>
            <button id="btnLogin" <%--onclick="Click()"--%> type="button" data-bind="click: LoginBtnClick">Login</button>
        </center>
        <br />
        <br />
        <center>
            <label id="lblRegister" class="lblControl" style="color: magenta"><%=GetGlobalResourceObject("LoginPage_Resource", "lblRegisterMsg") %></label>
            <div id="SignUpbtn" <%--onclick="SignUpClick()"--%> style="color: blue; cursor: pointer" data-bind="click: SignUpClick"><%=GetGlobalResourceObject("LoginPage_Resource", "SignUpDiv") %></div>
            <br />
            <br />
            <center>
                <label id="lblMsg" class="lblControl" ></label>
                <span id="errorMsg"></span>
            </center>
        </center>
    </div>

</asp:Content>
