<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BB_ActionLogs.aspx.cs" Inherits="LifePoints.BB_ActionLogs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>BloodBank Logs</title>
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
                    <li class="nav-item"><a class="nav-link" href="BB_BloodTransaction.aspx"><i class="fa fa-tint" style="font-size: 20px;"></i><span style="font-size: 15px;">Blood Transaction</span></a></li>
                    <li class="nav-item"><a class="nav-link active" href="BB_ActionLogs.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Logs</span></a></li>

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
                            <input class="form-control-plaintext" type="text" value="Logs" readonly="" style="font-size: 40px;">
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
                <div class="container-fluid">
                    <div class="row">
                        <div class="col">
                            <div class="card shadow">
                                <div class="card-header py-3">
                            <h3 class="text-dark mb-4" style="height: 31.5938px; margin: 32px;" runat="server" id="HeadingText">Transaction Logs</h3>
                                </div>
                                <div class="card-body">
                                    <div class="card-header d-flex" style="flex-direction: row; justify-content: start; align-items: center;">

                                    <div class="d-flex" style="flex-direction: row; justify-content: center; align-items: start;margin-right: 20px;">
                                        <h5 style="margin-right: 10px;">Table</h5>
                                        <asp:DropDownList runat="server" ID="TableView" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="TableView_SelectedIndexChanged" />
                                    </div>
                                    </div>

                                    <h3 runat="server" id="NoDataMsg" style="display: none;">No Data</h3>
                                    <div runat="server" id="TableContainer" style="max-height: 600px">
                                        <div id="VerticalScroll" style="overflow: auto; max-height: inherit;">
                                            <asp:GridView runat="server" ID="ActionLogs" Visible="true" AutoGenerateColumns="false" Width="100%"
                                                BorderColor="Transparent" AutoPostBack="false">
                                                <RowStyle CssClass="grid-item-style  grid-font-style" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="ACT_ID" />
                                                    <asp:BoundField HeaderText="DESCRIPTION" DataField="ACT_DESCRIPTION" />
                                                    <asp:BoundField HeaderText="USER ID" DataField="ACT_UACC_ID" />
                                                    <asp:BoundField HeaderText="USER NAME" DataField="ACT_UNAME" />
                                                    <asp:BoundField HeaderText="DATE" DataField="ACT_DATE" />
                                                </Columns>
                                            </asp:GridView>
                                             <asp:GridView runat="server" ID="TransactionLogs" Visible="true" AutoGenerateColumns="false" Width="100%"
                                                BorderColor="Transparent" AutoPostBack="false">
                                                <RowStyle CssClass="grid-item-style  grid-font-style" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="TL_ID" />
                                                    <asp:BoundField HeaderText="TRANSACTION ID" DataField="TL_TRANSACTION_ID" />
                                                    <asp:BoundField HeaderText="USER ID" DataField="TL_ACC_ID" />
                                                    <asp:BoundField HeaderText="TYPE" DataField="TL_TRANSACTION" />
                                                    <asp:BoundField HeaderText="BLOOD TYPE" DataField="TL_BLOOD_TYPE" />
                                                    <asp:BoundField HeaderText="BLOOD BAGS" DataField="TL_TRANSACTION_AMOUNT" />
                                                    <asp:BoundField HeaderText="DATE" DataField="TLTRANSACTION_DATE" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
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
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/login-full-page-bs4.js"></script>
    <script src="assets/js/login-full-page-bs4-1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.js"></script>
    <script src="assets/js/Table-With-Search.js"></script>
    <script src="assets/js/theme.js"></script>
</body>

</html>
