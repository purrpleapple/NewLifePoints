<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LifePoints.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Login | LifePoints</title>
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
    <script src="https://kit.fontawesome.com/0ab79b85c3.js" crossorigin="anonymous"></script>
    <script type="text/javascript">
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
            var pword = document.getElementById("<%=Password.ClientID%>");

            if (pword.value.length == 0) {
                pword.classList.remove("is-valid");
                pword.classList.remove("is-invalid");
            }
            else if (pword.value.length < 8) {
                pword.classList.add("is-invalid");
                pword.classList.remove("is-valid");
            }
            else {

                pword.classList.add("is-valid");
                pword.classList.remove("is-invalid");
            }
        }
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

<body class="d-flex justify-content-xxl-center align-items-xxl-center"
    style="background: linear-gradient(to right, rgba(126, 25, 27,0.8), rgba(255,255,255,0.8)), url(assets/img/background.jpg); background-size: cover; background-repeat: no-repeat; height: 100vh;">
    <form runat="server" class="container" style="width: 848px; opacity: 0.9;">
        <div class="card">
            <div class="card-body" style="background-color: rgb(250, 249, 246,0.5);">
                <div class="row">
                    <div class="col d-flex justify-content-xxl-center align-items-xxl-center">
                        <img src="assets/img/321479999_548324667206662_5830804446592810955_n.png" style="height: 352px;" />
                    </div>
                    <div class="col d-flex justify-content-xxl-center align-items-xxl-center">
                        <div>
                            <div class="text-center">
                                <h4 class="text-dark mb-4" style="margin-top: 10px">Welcome to LifePoints!</h4>
                            </div>
                            <div class="d-flex flex-column user" style="padding-bottom: 0px; margin-bottom: 40px;">
                                <div class="mb-3">
                                    <asp:TextBox runat="server" CssClass="form-control" oninput="CheckEmail()" type="email" ClientIDMode="Static" ID="Email" placeholder="Email Address" name="email" Style="border-radius: 5.6px !important;" required="" />
                                </div>
                                <div class="mb-3">
                                    <asp:TextBox runat="server" CssClass="form-control" oninput="CheckPassword()" type="password" ClientIDMode="Static" ID="Password" placeholder="Password" name="password" required="" />
                                    <label for="show-password">
                                        <input type="checkbox" id="show-password" onchange="showHidePassword()" />
                                        Show Password
                                    </label>
                                </div>
                                <hr style="margin-top: -12px;" />
                                <asp:Button runat="server" Text="Login" ID="LoginBtn" OnClick="LoginBtn_Click" UseSubmitBehavior="true" AutoPostBack="true" Class="btn btn-primary d-block btn-user w-100" type="submit" Style="background: rgb(126, 25, 27); width: 319.609px;" />
                                <hr style="margin-top: 16px;" />
                                <a href="USER_REGISTER.aspx">Don't have an Account? Register.</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/bs-init.js"></script>
    <script src="assets/js/theme.js"></script>
    <script>
        function showHidePassword() {
            var x = document.getElementById("Password");
            var checkbox = document.getElementById("show-password");
            if (checkbox.checked) {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>

</body>

</html>
