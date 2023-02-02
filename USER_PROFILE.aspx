<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USER_PROFILE.aspx.cs" Inherits="LifePoints.USER_PROFILE1" EnableEventValidation="false" %>

<!DOCTYPE html>

<html style="background: #772820;">

<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Update Profile | LifePoints</title>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i&amp;display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Almarai&amp;display=swap" />
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css" />
    <link rel="stylesheet" href="assets/fonts/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/fonts/fontawesome5-overrides.min.css" />
    <link rel="stylesheet" href="assets/css/Blog---Recent-Posts-styles.css" />
    <link rel="stylesheet" href="assets/css/Blog---Recent-Posts.css" />
    <link rel="stylesheet" href="assets/css/Bootstrap-Chat.css" />
    <link rel="stylesheet" href="assets/css/Button-Outlines---Pretty.css" />
    <link rel="stylesheet" href="assets/css/Chat.css" />
    <link rel="stylesheet" href="assets/css/custom-buttons.css" />
    <link rel="stylesheet" href="assets/css/Floating-Button.css" />
    <link rel="stylesheet" href="assets/css/Ludens-basic-login.css" />
    <link rel="stylesheet" href="assets/css/Ludens-Users---1-Login.css" />
    <link rel="stylesheet" href="assets/css/Simple-Bootstrap-Chat.css" />

    <script type="text/javascript">
        let email = document.getElementById("email")
        let password = document.getElementById("password")
        let verifyPassword = document.getElementById("verifyPassword")
        let submitBtn = document.getElementById("submitBtn")
        let emailErrorMsg = document.getElementById('emailErrorMsg')
        let passwordErrorMsg = document.getElementById('passwordErrorMsg')

        function displayErrorMsg(type, msg) {
            if (type == "email") {
                emailErrorMsg.style.display = "block"
                emailErrorMsg.innerHTML = msg
                submitBtn.disabled = true
            }
            else {
                passwordErrorMsg.style.display = "block"
                passwordErrorMsg.innerHTML = msg
                submitBtn.disabled = true
            }
        }

        function hideErrorMsg(type) {
            if (type == "email") {
                emailErrorMsg.style.display = "none"
                emailErrorMsg.innerHTML = ""
                submitBtn.disabled = true
                if (passwordErrorMsg.innerHTML == "")
                    submitBtn.disabled = false
            }
            else {
                passwordErrorMsg.style.display = "none"
                passwordErrorMsg.innerHTML = ""
                if (emailErrorMsg.innerHTML == "")
                    submitBtn.disabled = false
            }
        }
        function showHidePassword() {
            var x = document.getElementById("Password");
            var eye = document.getElementsByClassName("fa-eye")[0];
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        function showHidePassword() {
            var x = document.getElementById("Password");
            var icon = document.querySelector('.fa-eye');
            if (x.type === "password") {
                x.type = "text";
                icon.classList.add("view");
            } else {
                x.type = "password";
                icon.classList.remove("view");
            }
        }

        // Validate password upon change
        password.addEventListener("change", function () {

            // If password has no value, then it won't be changed and no error will be displayed
            if (password.value.length == 0 && verifyPassword.value.length == 0) hideErrorMsg("password")

            // If password has a value, then it will be checked. In this case the passwords don't match
            else if (password.value !== verifyPassword.value) displayErrorMsg("password", "Passwords do not match")

            // When the passwords match, we check the length
            else {
                // Check if the password has 8 characters or more
                if (password.value.length >= 8)
                    hideErrorMsg("password")
                else
                    displayErrorMsg("password", "Password must be at least 8 characters long")
            }
        })

        verifyPassword.addEventListener("change", function () {
            if (password.value !== verifyPassword.value)
                displayErrorMsg("password", "Passwords do not match")
            else {
                // Check if the password has 8 characters or more
                if (password.value.length >= 8)
                    hideErrorMsg("password")
                else
                    displayErrorMsg("password", "Password must be at least 8 characters long")
            }
        })

        // Validate email upon change
        email.addEventListener("change", function () {
            // Check if the email is valid using a regular expression (string@string.string)
            if (email.value.match(/^[^@]+@[^@]+\.[^@]+$/))
                hideErrorMsg("email")
            else
                displayErrorMsg("email", "Invalid email")
        });
    </script>

    <style>
        label[for="show-password"] {
            margin-top: 10px;
        }

        .mb-3 {
            padding-top: 10px;
        }
    </style>

    <script type="text/javascript">
        //Alphabet and space only regex


        function CheckLName() {
            var reg = '^[a-zA-Z ]*$';
            let item = document.getElementById("<%= LName.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckFName() {
            var reg = '^[a-zA-Z ]*$';
            let item = document.getElementById("<%= FName.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckMName() {
            var reg = '^[a-zA-Z ]*$';
            let item = document.getElementById("<%= MName.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        // Add a change event listener to the input
        function CheckDOB() {
            var today = new Date();

            // Subtract 18 years from the current date
            var minDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());

            // Get the input element
            var birthdate = document.getElementById("DOB");

            // Get the selected date
            var selectedDate = birthdate.valueAsDate;

            // Compare the selected date with the minimum allowed date
            if (selectedDate > minDate) {
                birthdate.classList.add('is-invalid');
                birthdate.classList.remove('is-valid');
                alert("You must be at least 18 years old to use this service.");
                birthdate.value = "";
            }
            else {
                birthdate.classList.remove('is-invalid');
                birthdate.classList.add('is-valid');
            }
        }

        function CheckBType() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Bloodtype.ClientID %>");

            if (item.value != "Select Blood Type") {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckStreet() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Street.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckBaranggay() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Baranggay.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckCity() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= City.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckProvince() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Province.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckZip() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Zip.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (item.value.length >= 4) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckHome() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Home.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (item.value.length == 11) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckMobile() {
            var reg = '^[a-zA-Z0-9, .-]*$';
            let item = document.getElementById("<%= Mobile.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (item.value.length == 11) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckEmail() {
            var email = document.getElementById("<%=Email.ClientID%>");

            var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (email.value.length == 0) {
                email.classList.remove("is-invalid");
                email.classList.remove("is-valid");
            }
            else if (new RegExp(reg).test(email.value)) {
                email.classList.remove("is-invalid");
                email.classList.add('is-valid');
            }
            else {
                email.classList.add('is-invalid');
                email.classList.remove("is-valid");
            }
        }

        function CheckPassword() {
            var item = document.getElementById("<%=Password.ClientID%>");

            var reg = '^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#\$%\^&\*])(?=.{8,})';

            if (item.value.length == 0) {
                item.classList.remove("is-invalid");
                item.classList.remove("is-valid");
            }
            else if (new RegExp(reg).test(item.value)) {
                item.classList.remove("is-invalid");
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove("is-valid");
                alert("Password must contain atleast 1 Lowercase and Uppercase Letter, 1 Special Character, and must be 8 characters long or more.");
            }
        }

        function CheckRPassword() {
            var item1 = document.getElementById("<%=Password.ClientID%>");
            let item = document.getElementById("<%= RepeatPassword.ClientID %>");

            if (item.value.length == 0) {
                item.classList.remove('is-invalid');
                item.classList.remove('is-valid');
            }
            else if (item.value == item1.value) {
                item.classList.remove('is-invalid');
                item.classList.add('is-valid');
            }
            else {
                item.classList.add('is-invalid');
                item.classList.remove('is-valid');
            }
        }

        function CheckInputs() {
            const items = document.getElementsByTagName("input");
            let isValid = true;
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (item.classList.contains("is-invalid")) {
                    isValid = false;
                    break;
                }
            }
            if (isValid) {
                document.getElementById('<%= UpdateBtn.ClientID %>').click();
            }
        }

    </script>
</head>

<body style="background: rgb(119,40,32);">
    <form runat="server" id="wrapper">
        <nav class="navbar navbar-dark align-items-start sidebar sidebar-dark accordion bg-gradient-primary p-0" style="background: rgb(119,40,32); color: var(--bs-red);">
            <div class="container-fluid d-flex flex-column p-0">
                <img src="assets/img/321479999_548324667206662_5830804446592810955_n.png" width="92" height="92" style="margin-top: 30px;" /><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span>LIFEPOINTS</span></div>
                </a>
                <hr class="sidebar-divider my-0" />
                <ul class="navbar-nav text-light" id="accordionSidebar">
                    <li class="nav-item"><a class="nav-link" href="USER_BLOGPOST.aspx"><i class="fas fa-tachometer-alt"></i><span>Blog Post</span></a></li>
                    
                    <li class="nav-item"><a class="nav-link" href="USER_REQUEST_A_BLOOD.aspx"><i class="fa fa-tint"></i><span>Request a Blood</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="USER_BECOMEADONOR.aspx"><i class="fa fa-heart"></i><span>Become a Blood Donor</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="USER_CHAT.aspx"><i class="fa fa-heart"></i><span>Chat</span></a></li>

                </ul>
                <div class="text-center d-none d-md-inline"></div>
            </div>
        </nav>
        <div class="d-flex flex-column" id="content-wrapper">
            <div id="content">
                <nav class="navbar navbar-light navbar-expand bg-white shadow mb-4 topbar static-top">
                    <div class="container-fluid">
                        <button class="btn btn-link d-md-none rounded-circle me-3" id="sidebarToggleTop" type="button"><i class="fas fa-plus"></i></button>
                        <ul class="navbar-nav flex-nowrap ms-auto">
                            <li class="nav-item dropdown d-sm-none no-arrow"><a class="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#"><i class="fas fa-search"></i></a>
                                <div class="dropdown-menu dropdown-menu-end p-3 animated--grow-in" aria-labelledby="searchDropdown">
                                    <form class="me-auto navbar-search w-100">
                                        <div class="input-group">
                                            <input class="bg-light form-control border-0 small" type="text" placeholder="Search for ...">
                                            <div class="input-group-append">
                                                <button class="btn btn-primary py-0" type="button"><i class="fas fa-search"></i></button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </li>
                            <li class="nav-item dropdown no-arrow mx-1">
                                <div class="nav-item dropdown no-arrow">
                                    <a class="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#"><span class="badge bg-danger badge-counter" runat="server" id="UnreadCount"></span><i class="fas fa-bell fa-fw"></i></a>
                                    <div class="dropdown-menu dropdown-menu-end dropdown-list animated--grow-in">
                                        <h6 class="dropdown-header" style="background: rgb(119,40,32);">NOTIFICATIONS</h6>
                                        <div class="d-flex" style="flex-direction: column; max-height: 250px; overflow: auto; width: 100%;">
                                            <asp:Repeater runat="server" ID="NotificationNavList" OnItemCommand="NotificationNavList_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="ViewNotifBtn" CommandName="ViewNotif" CommandArgument='<%# Eval("NTF_ID") %>' CssClass="dropdown-item d-flex align-items-center">
                                                        <div class="me-3">
                                                            <div class="bg-primary icon-circle" style="background: var(--bs-indigo); border-color: var(--bs-blue);"><i class="fas fa-envelope-open text-white"></i></div>
                                                        </div>
                                                        <div>
                                                            <span class="small text-gray-500"><%# Eval("NTF_DATE") %></span>
                                                            <p><%# Eval("NTF_SUBJECT") %></p>
                                                        </div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <a class="dropdown-item text-center small text-gray-500" href="USER_Notification.aspx">Show All Notifications</a>
                                    </div>
                                </div>
                            </li>
                           
                            <div class="d-none d-sm-block topbar-divider"></div>
                            <li class="nav-item dropdown no-arrow">
                                <div class="nav-item dropdown no-arrow">
                                    <a class="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#"><span class="d-none d-lg-inline me-2 text-gray-600 small" runat="server" id="Username"></span>
                                        <img class="border rounded-circle img-profile" src="assets/img/avatars/icons8-user-60.png" /></a>
                                    <div class="dropdown-menu shadow dropdown-menu-end animated--grow-in">
                                        <a class="dropdown-item" href="USER_PROFILE.aspx"><i class="fas fa-user fa-sm fa-fw me-2 text-gray-400"></i>&nbsp;Profile</a>
                                        <a class="dropdown-item" runat="server" id="BtnLogout" autopostback="true" onserverclick="BtnLogout_ServerClick"><i class="fas fa-sign-out-alt fa-sm fa-fw me-2 text-gray-400"></i>&nbsp;Logout</a>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </nav>

              <div class="container-fluid d-flex" style="justify-content: center; align-items: center;">
                    <div class="card text-center" style="max-height: 100%; height: 100%; width: 70%;">
                        <div class="card-header">
                            <div class="d-flex justify-content-xxl-center align-items-xxl-center">
                                    <img class="img-fluid" src="assets/img/324620533_2986377338333052_6109802263453641588_n.png" width="300">
                                </div>
                                <div class="text-center">
                                    <h4 class="text-dark mb-4" style="font-weight: bold;">Update Account!</h4>
                                </div>
                            
                        </div>
                        <div class="card-body">
                            <div style="max-height: 700px; overflow: auto;">
                
            
                    <table style="text-align: left; width: 80%; margin: auto">
                        <tr>
                            <td colspan="2"><strong>Personal Information</strong></td>
                        </tr>
                        <tr>
                            <td>Family name: </td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="LName" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" oninput="CheckLName()" ClientIDMode="Static" Class="form-control" type="text" ID="LName" name="familyname" required="" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td>First name: </td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="FName" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" oninput="CheckFName()" ClientIDMode="Static" Class="form-control" type="text" ID="FName" name="firstname" required="" /></td>
                        </tr>
                        <tr>
                            <td>Middle name: </td>
                            <td class="d-flex flex-column">
                                <asp:TextBox runat="server" oninput="CheckMName()" ClientIDMode="Static" Class="form-control" type="text" ID="MName" name="midname" /></td>
                        </tr>
                         <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Gender:</td>
                            <td class="d-flex flex-column">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" Style="color: red; display: flex; justify-content: end;" ControlToValidate="Gender" Font-Italic="True"></asp:RequiredFieldValidator>
                                <asp:RadioButtonList runat="server" ID="Gender" RepeatDirection="Horizontal" RepeatColumns="2">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Date of birth:</td>
                            <td class="d-flex flex-column">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" Style="color: red; display: flex; justify-content: end;" ControlToValidate="DOB" Font-Italic="True" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" ID="DOB" CssClass="form-control" required="" type="date" ClientIDMode="Static" onchange="CheckDOB()" /></td>
                        </tr>
                        <tr>
                            <td>Blood Type Request:</td>
                            <td class="d-flex flex-column">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" Style="color: red; display: flex; justify-content: end;" ControlToValidate="Bloodtype" Font-Italic="True" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="Bloodtype" runat="server" Class="form-control" onchange="CheckBType()" required="">
                                </asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <strong>Address</strong></td>
                        </tr>
                        <tr>
                            <td>Street/Sector: </td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z0-9, .-]*$" ControlToValidate="Street" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" Class="form-control" oninput="CheckStreet()" type="text" ID="Street" name="resaddress" required=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Barangay:</td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z0-9, .-]*$" ControlToValidate="Baranggay" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" Class="form-control" oninput="CheckBaranggay()" type="text" ID="Baranggay" name="posaddress" required=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>City:</td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z0-9, .-]*$" ControlToValidate="City" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" Class="form-control" oninput="CheckCity()" type="text" ID="City" name="city" required=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Province:</td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z0-9, .-]*$" ControlToValidate="Province" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" Class="form-control" oninput="CheckProvince()" type="text" ID="Province" name="posaddress" required="" /></td>
                        </tr>
                        <tr>
                            <td>ZIP Code:</td>
                            <td class="d-flex flex-column">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" Style="color: red; display: flex; justify-content: end;" ControlToValidate="Zip" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" Class="form-control" oninput="CheckZip()" TextMode="Number" type="number" ID="Zip" ClientIDMode="Static" name="posaddress" required=""></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <strong>Contact Information</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Home:</td>
                            <td class="d-flex flex-column">
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Invalid Contact No. (e.g. 09xxx...)" Style="color: red; display: flex; justify-content: end;" ValidationGroup="Register" ControlToValidate="Home" Type="String" MaximumValue="099999999999" MinimumValue="090000000000"></asp:RangeValidator>
                                <asp:TextBox runat="server" Class="form-control" type="number" ID="Home" ClientIDMode="Static" oninput="CheckHome()" name="home" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Mobile:</td>
                            <td class="d-flex flex-column">
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Invalid Contact No. (e.g. 09xxx...)" Style="color: red; display: flex; justify-content: end;" ValidationGroup="Register" ControlToValidate="Mobile" Type="String" MaximumValue="099999999999" MinimumValue="090000000000"></asp:RangeValidator>
                                <asp:TextBox runat="server" Class="form-control" type="number" ID="Mobile" ClientIDMode="Static" oninput="CheckMobile()" name="mobile" required=""></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <strong>Account Information</strong>
                            </td>
                        </tr>
                         <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Email Address:</td>
                            <td class="d-flex flex-column">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression='^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$' ControlToValidate="Email" ErrorMessage="Email Address is Invalid" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" Class="form-control" type="email" ID="Email" ClientIDMode="Static" oninput="CheckEmail()" name="email" required=""></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Password</td>
                            <td class="d-flex flex-column">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Required" Style="color: red; display: flex; justify-content: end;" ControlToValidate="Password" Font-Italic="True"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" Class="form-control form-control-user" type="password" onchange="CheckPassword()" data-bs-toggle="tooltip" data-bss-tooltip="" ID="Password" ClientIDMode="Static" required="" Style="width: 434.609px;" title="Password" /></td>
                        </tr>
                        <tr>
                            <td>Repeat Password</td>
                            <td class="d-flex flex-column">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password does not match" Style="color: red; display: flex; justify-content: end;" ValidationGroup="Register" ControlToValidate="RepeatPassword" ControlToCompare="Password" Font-Italic="True"></asp:CompareValidator>
                                <asp:TextBox runat="server" Class="form-control form-control-user" type="password" oninput="CheckRPassword()" data-bs-toggle="tooltip" data-bss-tooltip="" ID="RepeatPassword" ClientIDMode="Static" required="" Style="width: 434.609px;" title="Repeat Password" /></td>
                        </tr>
                    </table>
                  </div>
                </div>

                    <div class="card-footer text-muted" >
                    <div class=" d-flex w-100 justify-content-center align-items-center" style ="margin-top:20px;margin-bottom:20px">
                        <asp:Button runat="server" ID="UpdateBtn" OnClick="UpdateBtn_Click" CssClass="btn btn-primary btn-user" ClientIDMode="Static" Text="Update Account" AutoPostBack="false" Style="background: rgb(119,40,32); width: 40%;" />
                    </div>
                    </div>
                    
                    
                </div>
            </div>
        </div>


            <footer class="bg-white sticky-footer">
                <div class="container my-auto">
                    <div class="text-center my-auto copyright"><span>Copyright © TechySavor 2022</span></div>
                </div>
            </footer>
        </div>
        <a class="border rounded d-inline scroll-to-top" href="#page-top"><i class="fas fa-angle-up"></i></a>
    </form>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/bs-init.js"></script>
    <script src="assets/js/theme.js"></script>
</body>

</html>
