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

        <script type = "text/javascript" >
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
                <div runat="server" class="container" style="position: absolute; left: 0; right: 0; top: 50%; transform: translateY(-50%); -ms-transform: translateY(-50%); -moz-transform: translateY(-50%); -webkit-transform: translateY(-50%); -o-transform: translateY(-50%);">
                    <div class="row d-flex d-xl-flex justify-content-center justify-content-xl-center">
                        <div class="col-sm-12 col-lg-10 col-xl-9 col-xxl-7 bg-white shadow-lg" style="border-radius: 5px;">
                            <div class="p-5">
                                <div class="d-flex justify-content-xxl-center align-items-xxl-center">
                                    <img class="img-fluid" src="assets/img/324620533_2986377338333052_6109802263453641588_n.png" width="300">
                                </div>
                                <div class="text-center">
                                    <h4 class="text-dark mb-4" style="font-weight: bold;">Update Account!</h4>
                                </div>
                                <div class="user" style="margin-bottom: 34px;">
                                    <div class="mb-3">
                                        <asp:TextBox runat="server" Class="form-control form-control-user" type="text" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_F" placeholder="First Name" required="*" Style="width: 434.609px;" title="First Name" />
                                    </div>
                                    <div class="mb-3">
                                        <asp:TextBox runat="server" Class="form-control form-control-user" type="text" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_M" placeholder="Middle Name" Style="width: 434.609px;" title="Middle Name" />
                                    </div>
                                    <div class="mb-3">
                                        <asp:TextBox runat="server" Class="form-control form-control-user" type="text" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_L" placeholder="Last Name" required="*" Style="width: 434.609px;" title="Last Name" />
                                    </div>
                                    <div class="mb-3">
                                        <asp:TextBox runat="server" Class="form-control form-control-user" type="email" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_EMAIL" placeholder="Email Address" required="*" Style="width: 434.609px;" inputmode="email" title="Email Address" />
                                    </div>
                                    <div class="mb-3">
                            
                                <asp:TextBox runat="server" Class="form-control form-control-user" type="password" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_PASS" placeholder="Password" required="" inputmode="Password" Style="width: 434.609px;" title="Password" />
                            
                            </div>
                        <div class="mb-3">
                                <asp:TextBox runat="server" Class="form-control form-control-user" type="password" data-bs-toggle="tooltip" data-bss-tooltip="" ID="UPD_RPASS" placeholder="Confirm Password" required="" inputmode="RepeatPassword" Style="width: 434.609px;" title="Repeat Password" />
                        </div>
                                    <div class="row mb-3">
                                        <p id="emailErrorMsg" class="text-danger" style="display: none;">Paragraph</p>
                                        <p id="passwordErrorMsg" class="text-danger" style="display: none;">Paragraph</p>
                                    </div>
                                    <asp:Button runat="server" ID="UpdateBtn" Class="btn btn-primary d-block btn-user w-100" Text="Update" UseSubmitBehavior="true" Style="background: rgb(119,40,32);" OnClick="UpdateBtn_Click" />
                                    <hr>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="text-center"><a class="small" href="USER_BLOGPOST.aspx">Go Back</a></div>
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
