<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BB_Dashboard.aspx.cs" Inherits="LifePoints.BB_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Dashboard</title>
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
                <img style="font-size: 12px; line-height: 23px; border-width: -14px; height: 97px; margin-top: 35px;" src="assets/img/vsmmclogo1.png"><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span style="font-size: 30px;">VSMMC</span></div>
                </a>
                <hr class="sidebar-divider my-0">
                <ul class="navbar-nav text-light" id="accordionSidebar">
                    <li class="nav-item"><a class="nav-link active" href="BB_Dashboard.aspx"><i class="fas fa-tachometer-alt" style="font-size: 20px;"></i><span style="font-size: 15px;">Dashboard</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="BB_BloodTransaction.aspx"><i class="fa fa-tint" style="font-size: 20px;"></i><span style="font-size: 15px;">Blood Transaction</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="BB_ActionLogs.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Logs</span></a></li>
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
                            <input class="form-control-plaintext" type="text" value="Dashboard" readonly="" style="font-size: 40px;">
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
                    <div class="d-sm-flex justify-content-between align-items-center mb-4">
                        <div id="content">

                            

                        <div class="row" style="margin-bottom:20px">
                            <div class="col-5">
                              <div class="card shadow">

                                <div class="card-header py-3">
                                    <p class="text-primary m-0 fw-bold">Blood Inventory</p>
                                </div>

                                <div class="card-body">
                                    <h3 runat="server" id="NoDataMsg" style="display: none;">No Data</h3>
                                     <div class="table-responsive" > 
                                         <asp:GridView runat="server" ID="GridInventory" AutoGenerateColumns="False" Width="100%"
                                             BorderColor="Transparent" AutoPostBack="true">
                                                <RowStyle CssClass="grid-item-style  grid-font-style" />
                                                <Columns>
                                                            <asp:BoundField HeaderText="Blood Type" DataField="inv_blood_type" />
                                                            <asp:BoundField HeaderText="Amount" DataField="inv_numbers" />
                                        
                                               </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </div>
                            </div>

                                       <div class="col-7">
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="row d-flex flex-column">
                                                <div class="col d-flex" style="margin-bottom: 10px;">
                                                    <div class="card" style="width: 100%;">
                                                        <div class="card-header" style="padding: 0px; padding-top: 9px; background: var(--red);">
                                                            <h3 class="text-center" style="font-weight: bold; color: var(--white);">TOTAL NUMBER OF USERS</h3>
                                                        </div>
                                                        <div class="card-body" style="width: 100%;">
                                                            <div class="row">
                                                                <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                    <h1 runat="server" id="TotalNumberUser"></h1>
                                                                </div>
                                                                <div class="col-2 d-flex justify-content-xl-center align-items-xl-center">
                                                                    <img src="assets/img/user.png" width="30">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="row">
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: var(--red);">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">REQUESTER</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="NumberRequestor"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-drop.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: var(--red);">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">DONORS</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="NumberDonor"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-donation.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="row d-flex flex-column">
                                                <div class="col d-flex" style="margin-bottom: 10px;">
                                                    <div class="card" style="width: 100%;">
                                                        <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                            <h3 class="text-center" style="font-weight: bold; color: var(--white);">PENDING TRANSACTIONS</h3>
                                                        </div>
                                                        <div class="card-body" style="width: 100%;">
                                                            <div class="row">
                                                                <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                    <h1 runat="server" id="TotalNumberTransaction"></h1>
                                                                </div>
                                                                <div class="col-2 d-flex justify-content-xl-center align-items-xl-center">
                                                                    <img src="assets/img/bill.png" width="30">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="row">
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">REQUESTS(BLOOD)</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="NumberRequestTransaction"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-drop.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">REQUESTS(DONOR)</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="NumberDonationTransaction"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-donation.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                       </div>
                            

                            <div class="row">
                             
                                <div class="col-5">
                                    <div class="card d-flex">
                                        <div class="card-header" style="background: var(--blue); padding: 0px; padding-top: 9px;">
                                            <div class="row">
                                                <div class="col">
                                                    <h3 style="font-weight: bold; color: var(--white);">Blood <span runat="server" id="PieTitle"></span> Transactions</h3>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList runat="server" ID="PieOption" AutoPostBack="true" OnSelectedIndexChanged="PieOption_SelectedIndexChanged">
                                                        <asp:ListItem Text="Blood Requests" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Blood Donations" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div>
                                                <canvas id="myPieChart" width="350" height="350"></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-7">
                                    <div class="col-6">
                                            <div class="row d-flex flex-column">
                                                <div class="col d-flex" style="margin-bottom: 10px;">
                                                    <div class="card" style="width: 100%;">
                                                        <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                            <h3 class="text-center" style="font-weight: bold; color: var(--white);">APPROVED REQUEST</h3>
                                                        </div>
                                                        <div class="card-body" style="width: 100%;">
                                                            <div class="row">
                                                                <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                    <h1 runat="server" id="TotalApproved"></h1>
                                                                </div>
                                                                <div class="col-2 d-flex justify-content-xl-center align-items-xl-center">
                                                                    <img src="assets/img/bill.png" width="30">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col">
                                                    <div class="row">
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">REQUESTS(BLOOD)</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="BR_Approved"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-drop.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="card" style="width: 100%;">
                                                                <div class="card-header" style="padding: 0px; padding-top: 9px; background: #25476a;">
                                                                    <h5 class="text-center" style="font-weight: bold; color: var(--white);">REQUESTS(DONOR)</h5>
                                                                </div>
                                                                <div class="card-body" style="width: 100%;">
                                                                    <div class="row">
                                                                        <div class="col d-flex justify-content-xl-center align-items-xl-center">
                                                                            <h1 runat="server" id="BD_Approved"></h1>
                                                                        </div>
                                                                        <div class="col-3 d-flex justify-content-xl-center align-items-xl-center">
                                                                            <img src="assets/img/blood-donation.png" width="30">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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
    <script src="assets/js/chart.min.js"></script>
    <script src="assets/js/bs-init.js"></script>
    <script src="assets/js/login-full-page-bs4.js"></script>
    <script src="assets/js/login-full-page-bs4-1.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.js"></script>
    <script src="assets/js/Table-With-Search.js"></script>
    <script src="assets/js/theme.js"></script>
    <script>
        var barColors = ["red", "green", "blue", "orange", "brown"];
        var ctx = document.getElementById('myPieChart').getContext('2d');
        var myPieChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Labels)%>,  // labels array created in step 4
                datasets: [{
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data)%>,  // data array created in step 4
                    backgroundColor: barColors,
                }]
            },
            options: {
                maintainAspectRatio: false,
                // options object
            }
        });
    </script>
</body>

</html>
