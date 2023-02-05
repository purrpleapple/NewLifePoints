<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BB_BloodTransaction.aspx.cs" Inherits="LifePoints.Admin.BB_BloodTransaction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Blood Transactions | Admin</title>
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
            <div class="container-fluid d-flex flex-column p-0"><img style="font-size: 12px;line-height: 23px;border-width: -14px;margin-top:50px;height: 97px;" src="assets/img/solologo.png"><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span style="font-size: 25px;">LifePoints</span></div>
                </a>
                <hr class="sidebar-divider my-0">
                <ul class="navbar-nav text-light" id="accordionSidebar">
                    <li class="nav-item"><a class="nav-link" href="BB_Dashboard.aspx"><i class="fas fa-tachometer-alt" style="font-size: 20px;"></i><span style="font-size: 15px;">Dashboard</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="Admin_BlogPost.aspx"><i class="fas fa-tachometer-alt" style="font-size: 20px;"></i><span style="font-size: 15px;">Blog Post</span></a></li>
                    <li class="nav-item"><a class="nav-link active" href="BB_BloodTransaction.aspx"><i class="fa fa-tint" style="font-size: 20px;"></i><span style="font-size: 15px;">Blood Transaction</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="Admin_Reports.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Reports</span></a></li> 
                    <li class="nav-item"><a class="nav-link" href="BB_ActionLogs.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Action Logs</span></a></li>

                </ul>
                <div class="text-center d-none d-md-inline"></div>
            </div>
        </nav>
        <div class="d-flex flex-column" id="content-wrapper">
            <div id="content" style="background: linear-gradient(rgb(249,243,243) 28%, white), #ffffff;">
                <nav class="navbar navbar-light navbar-expand bg-white shadow mb-4 topbar static-top">
                    <div class="container-fluid"><button class="btn btn-link d-md-none rounded-circle mr-3" id="sidebarToggleTop" type="button"><i class="fas fa-bars"></i></button>
                        <div class="form-inline d-none d-sm-inline-block mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search"><h3>Blood Transactions</h3></div>
                        <ul class="navbar-nav flex-nowrap ml-auto">
                            <li class="nav-item dropdown no-arrow mx-1">
                                <div class="nav-item dropdown no-arrow">
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
                <div class="container-fluid">
                    <div class="card shadow">
                        <div class="card-header py-3">
                            <h3 class="text-dark mb-4" style="height: 31.5938px; margin: 32px;" runat="server" id="HeadingText">Blood Requests</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-header d-flex" style="flex-direction: row; justify-content: start; align-items: center;">
                                <div class="d-flex" style="flex-direction: row; justify-content: center; align-items: start;margin-right: 20px;">
                                    <h5 style="margin-right: 5px;">Table</h5>
                                    <asp:DropDownList runat="server" ID="TableView" AutoPostBack="true" OnSelectedIndexChanged="TableView_SelectedIndexChanged" />
                                </div>
                                <div class="d-flex" style="flex-direction: row; justify-content: center; align-items: center; margin-right: 20px;">
                                    <h5 style="margin-right: 5px;">Search Request ID</h5>
                                    <asp:TextBox runat="server" ID="SearchRequest"  placeholder="" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" style="margin-right: 5px;" ErrorMessage="*" Text="*" ControlToValidate="Searchrequest" ValidationGroup="SearchRequest" Font-Bold="True" Font-Size="XX-Large"></asp:RequiredFieldValidator>
                                    <asp:Button runat="server" ID="SearchBloodRequest" Text="Search" ValidationGroup="SearchRequest" UseSubmitBehavior="true" OnClick="SearchBloodRequest_Click" />
                                </div>
                                <div class="d-flex" style="flex-direction: row; justify-content: center; align-items: start;">
                                    <h5 style="margin-right: 5px;">Request Status</h5>
                                    <asp:DropDownList runat="server" ID="RequestStatus" OnSelectedIndexChanged="RequestStatus_SelectedIndexChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="table-responsive table mt-2" id="dataTable" role="grid" aria-describedby="dataTable_info" style="max-height: 550px; overflow: auto;">
                                <asp:GridView runat="server" ID="GridUserBloodRequest" Visible="true" AutoGenerateColumns="false" Width="100%"
                                    BorderColor="Transparent" OnSelectedIndexChanged="GridUserBloodRequest_SelectedIndexChanged"
                                    AutoPostBack="false" 
                                    AllowSorting="true">
                                     <Columns>
                                        <asp:BoundField HeaderText="REQUEST ID" DataField="BREQ_ID" />
                                        <asp:BoundField HeaderText="REQUESTER" DataField="BREQ_UACC_ID" />
                                        <asp:BoundField HeaderText="BLOOD TYPE" DataField="BREQ_BLOOD_TYPE" />
                                        <asp:BoundField HeaderText="BLOOD BAGS" DataField="BREQ_NO_BLOOD" />
                                         <asp:BoundField HeaderText="REQUEST DATE" DataField="BREQ_DEMAND_DATE" />
                                        <asp:BoundField HeaderText="REQUEST STATUS" DataField="BREQ_SURVEY_STATUS" />
                                        <asp:BoundField HeaderText="CLAIMED" DataField="BREQ_BLOOD_STATUS" />
                                        <asp:BoundField HeaderText="EXPECTED DATE CLAIM" DataField="BREQ_VISIT_DATE" />
                                        <asp:CommandField ButtonType="Button" ShowSelectButton="true" SelectText="View" ControlStyle-CssClass="btn btn-danger" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" ID="GridUserBloodDonation" Visible="true" AutoGenerateColumns="false" Width="100%"
                                    BorderColor="Transparent" OnSelectedIndexChanged="GridUserBloodDonation_SelectedIndexChanged"
                                    AutoPostBack="false" style="display: none;"
                                    AllowSorting="true">
                                    <Columns>
                                       <asp:BoundField HeaderText="REQUEST ID" DataField="BD_ID" />
                                        <asp:BoundField HeaderText="DONOR" DataField="BD_UACC_ID" />
                                        <asp:BoundField HeaderText="REQUEST STATUS" DataField="BD_SURVEY_STATUS" />
                                        <asp:BoundField HeaderText="WITHIN DATE" DataField="BD_VISIT_DATE" />
                                        
                                        <asp:BoundField HeaderText="DONATED" DataField="BD_BLOOD_STATUS" />
                                        <asp:CommandField ButtonType="Button" ShowSelectButton="true" SelectText="View" ControlStyle-CssClass="btn btn-danger"  />
                                    </Columns>
                                </asp:GridView>
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
        </div><a class="border rounded d-inline scroll-to-top" href="#page-top"><i class="fas fa-angle-up"></i></a>
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