<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BB_Request_Survey.aspx.cs" Inherits="LifePoints.BB_Request_Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Blood Request Survey</title>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i&amp;display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Alegreya+Sans" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Black+Han+Sans" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Capriola" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Francois+One" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Gafata" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Gayathri" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Gothic+A1" />
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css" />
    <link rel="stylesheet" href="assets/fonts/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/fonts/fontawesome5-overrides.min.css" />
    <link rel="stylesheet" href="assets/css/Google-Style-Login.css" />
    <link rel="stylesheet" href="assets/css/login-full-page-bs4.css" />
    <link rel="stylesheet" href="assets/css/Modal-Login-form.css" />
    <link rel="stylesheet" href="assets/css/Table-With-Search-1.css" />
    <link rel="stylesheet" href="assets/css/Table-With-Search.css" />
</head>
<body id="page-top">
    <form runat="server" id="wrapper">
        <nav class="navbar navbar-dark align-items-start sidebar sidebar-dark accordion bg-gradient-primary p-0" style="background: rgb(119,40,32);">
            <div class="container-fluid d-flex flex-column p-0">
                <img style="font-size: 12px; line-height: 23px; border-width: -14px; height: 97px; margin-top:35px;" src="assets/img/vsmmclogo1.png"><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span style="font-size: 30px;">VSMMC</span></div>
                </a>
                <hr class="sidebar-divider my-0">
                <ul class="navbar-nav text-light" id="accordionSidebar">


                    <li class="nav-item"><a class="nav-link" href="BB_Dashboard.aspx"><i class="fas fa-tachometer-alt" style="font-size: 20px;"></i><span style="font-size: 15px;">Dashboard</span></a></li>
                    <li class="nav-item"><a class="nav-link active" href="BB_BloodTransaction.aspx"><i class="fa fa-tint" style="font-size: 20px;"></i><span style="font-size: 15px;">Blood Transaction</span></a></li>

                    <li class="nav-item"><a class="nav-link" href="BB_ActionLogs.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Action Logs</span></a></li>
                </ul>
                <div class="text-center d-none d-md-inline"></div>
            </div>
        </nav>
        <div class="d-flex flex-column" id="content-wrapper">
            <div id="content" style="background: linear-gradient(rgb(249,243,243) 28%, white), #ffffff;">
                <nav class="navbar navbar-light navbar-expand bg-white shadow mb-4 topbar static-top">
                    <div class="container-fluid">
                        <button class="btn btn-link d-md-none rounded-circle mr-3" id="sidebarToggleTop" type="button"><i class="fas fa-bars"></i></button>
                        <div class="form-inline d-none d-sm-inline-block mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
                            <input class="form-control-plaintext" type="text" value="" readonly="" style="font-size: 40px;">
                        </div>
                        <ul class="navbar-nav flex-nowrap ml-auto">
                            <li class="nav-item dropdown no-arrow mx-1">
                                <div class="nav-item dropdown no-arrow">
                                    <a class="dropdown-toggle nav-link dropleft" aria-haspopup="true" aria-expanded="false" data-toggle="dropdown" href="#"><span class="badge bg-danger badge-counter" runat="server" id="UnreadCount"></span><i class="fas fa-bell fa-fw"></i></a>
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
                                        <a class="dropdown-item text-center small text-gray-500" href="BB_Notification.aspx">Show All Notifications</a>
                                    </div>
                                </div>
                            </li>
                            <div class="d-none d-sm-block topbar-divider"></div>
                               <li class="nav-item dropdown no-arrow">
                                <div class="nav-item dropdown no-arrow">
                                    <a class="dropdown-toggle nav-link" data-toggle="dropdown" aria-expanded="false" href="#"><span class="d-none d-lg-inline mr-2 text-gray-600 small" runat="server" id="username"></span>
                                        <img class="border rounded-circle img-profile" src="assets/img/user.png" /></a>
                                    <div class="dropdown-menu shadow dropdown-menu-end animated--grow-in">
                                        <a class="dropdown-item" href="BB_Profile.aspx"><i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>&nbsp;Update Password</a>
                                        <div class="dropdown-divider"></div>
                                        <a class="dropdown-item" runat="server" id="BtnLogout" autopostback="true" onserverclick="BtnLogout_ServerClick"><i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>&nbsp;Logout</a>

                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </nav>
                <div class="container-fluid d-flex" style="justify-content: center; align-items: center;">
                    <div class="card text-center" style="max-height: 700px; height: 700px; width: 80%;">
                        <div class="card-header">
                            <h2>BLOOD REQUEST FORM</h2>
                          
                        </div>
                        <div class="card-body">
                            <div style="max-height: 500px; overflow: auto;">
                                <p style="font-size: 25px; font-style: bold; margin-left: -30%">
                                    Please complete this form
                                </p>
                                <table style="text-align: left; width: 77%; margin: auto" runat="server" id="Survey">
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
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>First name: </td>
                                        <td class="d-flex flex-column">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="FName" ErrorMessage="Contains Invalid Characters" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                            <asp:TextBox runat="server" oninput="CheckFName()" ClientIDMode="Static" Class="form-control" type="text" ID="FName" name="firstname" required="" /></td>
                                    </tr>
                                    <tr>
                                        <td>Middle name: </td>
                                        <td>
                                            <asp:TextBox runat="server" oninput="CheckMName()" ClientIDMode="Static" Class="form-control" type="text" ID="MName" name="midname" /></td>
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
                                        <td colspan="2">
                                            <br />
                                            <strong>Contact Information</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Home:</td>
                                        <td class="d-flex flex-column">
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Invalid Contact No. (e.g. 09xxx...)" Style="color: red; display: flex; justify-content: end;" ValidationGroup="Register" ControlToValidate="Home" Type="String" MaximumValue="099999999999" MinimumValue="090000000000"></asp:RangeValidator>
                                            <asp:TextBox runat="server" Class="form-control" type="number" ID="Home" ClientIDMode="Static" oninput="CheckHome()" name="home" required=""></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Mobile:</td>
                                        <td class="d-flex flex-column">
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Invalid Contact No. (e.g. 09xxx...)" Style="color: red; display: flex; justify-content: end;" ValidationGroup="Register" ControlToValidate="Mobile" Type="String" MaximumValue="099999999999" MinimumValue="090000000000"></asp:RangeValidator>
                                            <asp:TextBox runat="server" Class="form-control" type="number" ID="Mobile" ClientIDMode="Static" oninput="CheckMobile()" name="mobile" required=""></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Email Address:</td>
                                        <td class="d-flex flex-column">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Style="color: red; display: flex; justify-content: end;" ValidationExpression='^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$' ControlToValidate="Email" ErrorMessage="Email Address is Invalid" Font-Italic="True" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                            <asp:TextBox runat="server" Class="form-control" type="email" ID="Email" ClientIDMode="Static" oninput="CheckEmail()" name="email" required=""></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table style="text-align: left; width: 77%; margin: auto" runat="server" id="DConsent">
                                    <tr>
                                        <td>
                                            <strong>Doctor's Consent</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image runat="server" ID="DoctorsConsent" ImageAlign="Middle" ImageUrl="" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="card-footer text-muted">
                            <div runat="server" id="SurveyGroup" style="display: none;">
                                <asp:Button runat="server" OnClick="ApproveSurveyBtn_Click" ID="ApproveSurveyBtn" CssClass="btn btn-primary  btn-signin" Style="background: rgb(119,40,32);" type="button" Text="Approve Request Form" />
                                <asp:Button runat="server" OnClick="RejectSurveyBtn_Click" ID="RejectSurveyBtn" CssClass="btn btn-primary  btn-signin" Style="background: rgb(119,40,32);" type="button" Text="Reject Request Form" />
                            </div>
                            <div runat="server" id="BloodGroup" style="display: none;">
                                <asp:Button runat="server" OnClick="ApproveBloodBtn_Click" ID="ApproveBloodBtn" CssClass="btn btn-primary  btn-signin" Style="background: rgb(119,40,32);" type="button" Text="Approve Blood Transaction" />
                                <asp:Button runat="server" OnClick="RejectBloodBtn_Click" ID="RejectBloodBtn" CssClass="btn btn-primary  btn-signin" Style="background: rgb(119,40,32);" type="button" Text="Reject Blood Transaction" />
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-primary  btn-signin" Style="background: rgb(119,40,32); margin-top: 10px;" ID="BackButton" OnClick="BackButton_Click" Text="Back" type="reset" UseSubmitBehavior="false" AutoPostBack="true" />
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
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/login-full-page-bs4.js"></script>
    <script src="assets/js/login-full-page-bs4-1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.js"></script>
    <script src="assets/js/Table-With-Search.js"></script>
    <script src="assets/js/theme.js"></script>
</body>

</html>
