<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Donor_Survey_ViewOnly.aspx.cs" Inherits="LifePoints.Admin.Donor_Survey_ViewOnly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Donor Survey</title>
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
    <script>
        function UncheckRadioIfTextIsClicked() {
            var res = document.querySelector('input[name="rd23"]:checked').value;
            alert(res);
            document.getElementById("2.3OTH").setAttribute("required", "");

            //Uncheck Radios and Unset required
            var rds = document.getElementsByName("rd23");
            for (var i = 0; i < rds.length; i++) {
                rds[i].removeAttribute("required");
                rds[i].checked = false;
            }
        }

        function CleanInputField() {
            document.getElementById("2.3OTH").value = "";

            var rds = document.getElementsByName("rd23");
            for (var i = 0; i < rds.length; i++) {
                rds[i].setAttribute("required", "");
            }

            document.getElementById("2.3OTH").removeAttribute("required");
        }

        function SetAttributesReadOnly() {
            var items = document.getElementsByTagName("input");
            for (var i = 0; i < items.length; i++) {
                if (items[i].type !== "button" && !items[i].classList.contains("btn")) {
                    items[i].setAttribute("disabled", "");
                }
            }
        }
    </script>
</head>
<body id="page-top">
    <form runat="server" id="wrapper">
        <nav class="navbar navbar-dark align-items-start sidebar sidebar-dark accordion bg-gradient-primary p-0" style="background: rgb(119,40,32);">
            <div class="container-fluid d-flex flex-column p-0">
                <img style="font-size: 12px; line-height: 23px; border-width: -14px; height: 97px; margin-top:35px;" src="assets/img/vsmmclogo1.png" /><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span style="font-size: 30px;">VSMMC</span></div>
                </a>
                <hr class="sidebar-divider my-0"/>
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
                <div class="container-fluid d-flex" style="align-items: center; justify-content: center;">
                    <div class="card text-center" style="max-height: 100%; height: 100%; width: 70%;">
                        <div class="card-header">
                           
                            <h3>BLOOD DONOR QUESTIONNAIRE</h3>
                        </div>
                        <div class="card-body">
                            <div style="max-height: 900px; overflow: auto;">
                                <p style="font-size: 25px; font-style: bold; margin-left: -30%">
                                    Please complete this form
                                </p>
                                <table style="text-align: left; width: 50%; margin: auto">
                                    
                                    <tr>
                                        <td>Family name:</td>
                                        <td>
                                            <input type="text" id="familyname" class="form-control" name="familyname" readonly="" value=" <%=ViewState["familyname"] %>" /></td>
                                    </tr>
                                     <tr>
                                        <td>First name:</td>
                                        <td>
                                            <input type="text" id="firstname"  class="form-control" name="firstname" readonly="" value=" <%=ViewState["firstname"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>Middle name:</td>
                                        <td>
                                            <input type="text" id="middlename"  class="form-control" name="middlename" readonly="" value=" <%=ViewState["middlename"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>Gender:</td>
                                        <td>
                                            <input type="text" id="gender" class="form-control" name="gender" readonly="" value=" <%=ViewState["gender"] %>" /></td>
                                    </tr>
                                     <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Date of birth:</td>
                                        <td>
                                            <input type="text" id="month" class="form-control" name="month" readonly="" value=" <%=ViewState["dob"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>Residential Address:</td>
                                        </tr>
                                    <tr>
                                         <td>Street/Sector:</td>
                                        <td>
                                            <input type="text" id="street"  class="form-control"  name="street" readonly="" value=" <%=ViewState["street"] %>" /></td>
                                    </tr>
                                     <tr>
                                         <td>Barangay:</td>
                                        <td>
                                            <input type="text" id="barangay"  class="form-control" name="barangay" readonly="" value=" <%=ViewState["barangay"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>City:</td>
                                        <td>
                                            <input type="text" id="city"  class="form-control"  name="city" readonly="" value=" <%=ViewState["city"] %>" /></td>
                                    </tr>
                                     <tr>
                                         <td>Province:</td>
                                        <td>
                                            <input type="text" id="province"  class="form-control"  name="province" readonly="" value=" <%=ViewState["province"] %>" /></td>
                                    </tr>
                                     <tr>
                                         <td>ZIP Code:</td>
                                        <td>
                                            <input type="text" id="zip"  class="form-control"  name="zip" readonly="" value=" <%=ViewState["zip"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                           </td>
                                    </tr>
                                    <tr>
                                        <td>Home number :</td>
                                        <td>
                                            <input type="text" id="homenum"  class="form-control" name="homenum" readonly="" value=" <%= ViewState["homenum"] %>" /></td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Mobile number:</td>
                                        <td>
                                            <input type="text" id="mobilenum"  class="form-control" name="mobilenum" readonly="" value=" <%= ViewState["mobilenum"] %>" /></td>
                                    </tr>
                                    <tr>
                                        <td>Email Address:</td>
                                        <td>
                                            <input type="text" id="email"  class="form-control" name="email" readonly="" value=" <%=ViewState["email"] %>" /></td>
                                    </tr>
                                </table>
                                <p style="font-size: 25px; font-style: bold; margin-left: -30%">
                                    <br />
                                1 HEALTH ASSESSMENT
                                </p>
                                <p style="font-size: 20px; font-style: bold; margin-left: -20%">
                                    Please tick the appropriate answer to each question
                                </p>
                                <table style="text-align: left; width: 51%; margin: auto">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>YES</td>
                                        <td>NO</td>
                                    </tr>
                                    <tr>
                                        <td>1.1</td>
                                        <td>Are you feeling well and in good health today?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" type="radio" id="rd11y" GroupName="rd11" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" type="radio" id="rd11n" GroupName="rd11" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.2</td>
                                        <td>In the last 4 hours, have you had a meal or snack?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd12" type="radio" id="rd12y" name="rd12" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd12" type="radio" id="rd12n" name="rd12" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.3</td>
                                        <td>Have you already given blood in the last 16 weeks?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd13" type="radio" id="rd13y" name="rd13" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd13" type="radio" id="rd13n" name="rd13" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.4</td>
                                        <td>Have you got a chesty cough, sore throat or active cold sore?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd14" type="radio" id="rd14y" name="rd14" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd14" type="radio" id="rd14n" name="rd14" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.5</td>
                                        <td>Are you pregnant or breastfeeding?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd15" type="radio" id="rd15y" name="rd15" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd15" type="radio" id="rd15n" name="rd15" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.6</td>
                                        <td>Do you have or have you ever had:</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a &nbsp;&nbsp;&nbsp;Chest pains, heart disease/surgery or stroke?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16a" type="radio" id="rd16ay" name="rd16a" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16a" type="radio" id="rd16an" name="rd16a" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b &nbsp;&nbsp;&nbsp;Lung disease, tuberculosis or asthma?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16b" type="radio" id="rd16by" name="rd16b" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16b" type="radio" id="rd16bn" name="rd16b" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>c &nbsp;&nbsp;&nbsp;Cancer, a blood disease, an abnormal bleeding disorder,<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or a bleeding gastric ulcer or duodenal ulcer?
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16c" type="radio" id="rd16cy" name="rd16c" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16c" type="radio" id="rd16cn" name="rd16c" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>d &nbsp;&nbsp;&nbsp;Diabetes, thyroid disease, kidney disease, epilipsy (fits)?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16d" type="radio" id="rd16dy" name="rd16d" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd16d" type="radio" id="rd16dn" name="rd16d" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.7</td>
                                        <td>In the last 7 days, have you seen a doctor, dentist, or any other<br />
                                            healthcare professional or are you waiting to see one<br />
                                            (except for routine screening appointments)?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd17" type="radio" id="rd17y" name="rd17" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd17" type="radio" id="rd17n" name="rd17" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.8</td>
                                        <td>In the past 12 months:</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a &nbsp;&nbsp;&nbsp;Have you been ill, received any treatment<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or taken any medication?
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd18a" type="radio" id="rd18ay" name="rd18a" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd18a" type="radio" id="rd18an" name="rd18a" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b &nbsp;&nbsp;&nbsp;Have you been under a doctor's care, undergone surgery,<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or a diagnostic procedure, suffered a major illness, or been<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;involved in a serious accident?
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd18b" type="radio" id="rd18by" name="rd18b" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd18b" type="radio" id="rd18bn" name="rd18b" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.9</td>
                                        <td>Have you ever had yellow jaundice (excluding jaundice at birth),<br />
                                            hepatitis or liver disease or a positive test for hepatitis?</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a &nbsp;&nbsp;&nbsp;In the past 12 months, have you had close contact with<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a person with yellow jaundice or viral hepatitis, or have<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;you been given hepatitis B vaccination?
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19a" type="radio" id="rd19ay" name="rd19a" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19a" type="radio" id="rd19an" name="rd19a" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b &nbsp;&nbsp;&nbsp;Have you ever had hepatitis B or hepatitis C or think you<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;may have hepatitis now?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19b" type="radio" id="rd19by" name="rd19b" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19b" type="radio" id="rd19bn" name="rd19b" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>c &nbsp;&nbsp;&nbsp;In the past 12 months, have you been tattoed, had ear or body<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;piercing, acupuncture, circumcision or scarification, cosmetic<br>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;treatment?<br>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19c" type="radio" id="rd19cy" name="rd19c" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd19c" type="radio" id="rd19cn" name="rd19c" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.10</td>
                                        <td>In the past 12 months, have you or your sexual partner<br />
                                            received blood transfusion?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd110" type="radio" id="rd110y" name="rd110" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd110" type="radio" id="rd110n" name="rd110" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.11</td>
                                        <td>Have you or your sexual partner been treated with human or<br />
                                            animal blood products or clotting factors?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd111" type="radio" id="rd111y" name="rd111" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd111" type="radio" id="rd111n" name="rd111" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.12</td>
                                        <td>Have you ever had injections of human pituitary growth hormone,<br />
                                            pituitary gonadotrophin (fertility medicine) or seen a neurosurgeon<br />
                                            or neurologist?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd112" type="radio" id="rd112y" name="rd112" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd112" type="radio" id="rd112n" name="rd112" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">1.13</td>
                                        <td>Have you or your close relatives had an unexplained neurological<br />
                                            condition or been diagnosed with Creutzfeldt-Jacob Disease or<br />
                                            'mad cow disease'?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd113" type="radio" id="rd113y" name="rd113" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton  Enabled="false" runat="server" GroupName="rd113" type="radio" id="rd113n" name="rd113" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>1.14</td>
                                        <td>Have you:</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a &nbsp;&nbsp;&nbsp;Ever had malaria or an unexplained fever associated<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;with travel?
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd114a" type="radio" id="rd114ay" name="rd114a" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd114a" type="radio" id="rd114an" name="rd114a" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b &nbsp;&nbsp;&nbsp;Visited any malarial area in the last 12 months?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd114b" type="radio" id="rd114by" name="rd114b" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd114b" type="radio" id="rd114bn" name="rd114b" value="no"  /></td>
                                    </tr>
                                </table>
                                <table style="text-align: left; width: 51%; margin: auto">
                                    <tr>
                                        <td style="vertical-align: top">1.15</td>
                                        <td>When did you last travel to another region or country<br />
                                            (in months/years)?</td>
                                        <td style="vertical-align: top">
                                            <input readonly="" type="text" id="1.15" name="rd115"  value=" <%=ViewState["rd115"] %>" /></td>
                                    </tr>
                                </table>
                                <p style="font-size: 25px; font-style: bold; margin-left: -32%">
                                    <br />
                                2 RISK ASSESSMENT
                                </p>
                                <table style="text-align: left; width: 51%; margin: auto">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>YES</td>
                                        <td>NO</td>
                                    </tr>
                                    <tr>
                                        <td>2.1</td>
                                        <td>Is your reason for donating blood to undergo an HIV test?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd21" type="radio" id="rd21y" name="rd21" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd21" type="radio" id="rd21n" name="rd21" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>2.2</td>
                                        <td>Have you ever been tested for HIV?</td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd22" type="radio" id="rd22y" name="rd22" value="yes"  /></td>
                                        <td>
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd22" type="radio" id="rd22n" name="rd22" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">2.3</td>
                                        <td>If "Yes" what was the reason?<br />
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd23" type="radio" id="rd23VOL" name="rd23" value="voluntary"   onclick="CleanInputField()" />Voluntary
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd23" type="radio" id="rd23EMP" name="rd23" value="employment"  onclick="CleanInputField()" />Employment
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd23" type="radio" id="rd23INS" name="rd23" value="insurance"  onclick="CleanInputField()" />Insurance
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd23" type="radio" id="rd23MED" name="rd23" value="medadvice"  onclick="CleanInputField()" />Medical Advice<br />
                                                Others
                                            <input type="text" id="2.3OTH" name="rd23"  onchange="UncheckRadioIfTextIsClicked()" value="<%=ViewState["rd23"] %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.4</td>
                                        <td>Have you ever had casual, oral or anal sex with someone whose<br />
                                            background you do not know, with or without a condom?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd24" type="radio" id="rd24y" name="rd24" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd24" type="radio" id="rd24n" name="rd24" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.5</td>
                                        <td>Have you ever exchangeed money, drugs, goods or favours<br />
                                            in return for sex?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd25" type="radio" id="rd25y" name="rd25" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd25" type="radio" id="rd25n" name="rd25" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.6</td>
                                        <td>Have you suffered from a sexually transmitted disease (STD):<br />
                                            e.g.syphilis, gonorrhoea, genital herpes, genital ulcer, VD, or 'drop'?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd26" type="radio" id="rd26y" name="rd26" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd26" type="radio" id="rd26n" name="rd26" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td>2.7</td>
                                        <td>In the past 12 months:</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>a &nbsp;&nbsp;&nbsp;Has there been any change in your marital status?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27a" type="radio" id="rd27ay" name="rd27a" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27a" type="radio" id="rd27an" name="rd27a" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>b &nbsp;&nbsp;&nbsp;If sexually active, do you think any of the above questions (2.1-2.6)<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;may be true for your sexual partner?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27b" type="radio" id="rd27by" name="rd27b" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27b" type="radio" id="rd27bn" name="rd27b" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>c &nbsp;&nbsp;&nbsp;Have you been a victim of sexual abuse?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27c" type="radio" id="rd27cy" name="rd27c" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd27c" type="radio" id="rd27cn" name="rd27c" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.8</td>
                                        <td>Have you or your sexual partner suffered from night sweats,<br />
                                            unintentional weight loss, diarrhea or swollen glands?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd28" type="radio" id="rd28y" name="rd28" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd28" type="radio" id="rd28n" name="rd28" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.9</td>
                                        <td>Have you ever injected yourself or been injected with illegal or non-prescribed<br />
                                            drugs including body-building drugs or cosmetics (even if this was<br />
                                            only once or a long time ago)?</td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd29" type="radio" id="rd29y" name="rd29" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd29" type="radio" id="rd29n" name="rd29" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.10</td>
                                        <td>Have you been in contact with anyone with an infectious disease or in the<br />
                                            last 12 months have you had any immunizations, vaccinations or jabs?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd210" type="radio" id="rd210y" name="rd210" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd210" type="radio" id="rd210n" name="rd210" value="no"  /></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top">2.11</td>
                                        <td>Have you ever been refused as a blood donor, or told not to donate<br />
                                            blood?<br />
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd211" type="radio" id="rd211y" name="rd211" value="yes"  /></td>
                                        <td style="vertical-align: top">
                                            <asp:RadioButton Enabled="false" runat="server" GroupName="rd211" type="radio" id="rd211n" name="rd211" value="no"  /></td>
                                    </tr>
                                </table>
                                <p style="font-size: 25px; font-style: bold; margin-left: -35%">
                                    <br />
                                3 DECLARATION
                                </p>
                                <p style="font-size: 18px; font-style: bold; margin-left: 25%; text-align: left;">
                                    Please do not sign until you have answered all the questions and read the<br />
                                    declaration below.
                                </p>
                                <table style="text-align: left; width: 51%; margin: auto">
                                    <tr>
                                        <td>a &nbsp;&nbsp;&nbsp;I confirm that, to the best of my knowledge, I have answered all the<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;questions accurately and I consider my blood safe for<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transfusion to a patient.</td>
                                    </tr>
                                    <tr>
                                        <td>b &nbsp;&nbsp;&nbsp;I understand that any wilful misrepresentation of facts could endanger<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;my health or that of patients receiving my blood and may lead to<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;litigation. I am aware that my blood will be screened for among<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;others, HIV, hepatitis B, hepatitis C and syphilis. I understand that<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;these screening tests are not diagnostic and may yield false-positive<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;results. If any tests give a reactive result, I will be contacted<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using the information I have provided, and offered counselling.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>c &nbsp;&nbsp;&nbsp;I understand the blood donation process, and I have been<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;counseled regarding the importance of safe blood donation.<br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>d &nbsp;&nbsp;&nbsp;I confirm that I am over the age of 18 years.<br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>e &nbsp;&nbsp;&nbsp;I undertake that should there be any reason for my blood to be<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;deemed unsafe for use at any age, I will inform the Blood Transfusion<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Service.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="card-footer text-muted">
                            <a class="btn btn-primary  btn-signin" style="background: rgb(119,40,32);margin-top: 5px;" href="BB_BloodTransaction.aspx" >Back</a>
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
