<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USER_CHAT.aspx.cs" Inherits="LifePoints.USER_CHAT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" runat="server" href="~/assets/img/321479999_548324667206662_5830804446592810955_n.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Blog Posts | LifePoints</title>
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
</head>

<body id="page-top">
    <form runat="server" id="wrapper">
        <nav class="navbar navbar-dark align-items-start sidebar sidebar-dark accordion bg-gradient-primary p-0" style="background: rgb(119,40,32); color: var(--bs-red);">
            <div class="container-fluid d-flex flex-column p-0">
                <img src="assets/img/321479999_548324667206662_5830804446592810955_n.png" width="92" height="92" style="margin-top: 30px;" /><a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span>LIFEPOINTS</span></div>
                </a>
                <hr class="sidebar-divider my-0" />
                <ul class="navbar-nav text-light" id="accordionSidebar">
                    <li class="nav-item"><a class="nav-link active" href="USER_BLOGPOST.aspx"><i class="fas fa-tachometer-alt"></i><span>Blog Post</span></a></li>

                    <li class="nav-item"><a class="nav-link" href="USER_REQUEST_A_BLOOD.aspx"><i class="fa fa-tint"></i><span>Request a Blood</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="USER_BECOMEADONOR.aspx"><i class="fa fa-heart"></i><span>Become a Blood Donor</span></a></li>

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
                <div class="container-fluid">
                    <div class="row" style="height: 100%;">
                        <div class="col-lg-4 col-xl-4">
                            <div class="row">
                                <div class="col d-flex flex-nowrap justify-content-md-center align-items-md-center justify-content-lg-center align-items-lg-center justify-content-xl-center align-items-xl-center py-2" style="background: rgba(52,58,64,0.2); height: 4rem;">
                                    <h5 class="mr-auto my-auto">Inbox</h5>
                                    <button class="btn shadow-none border-0 my-auto" type="button" style="width: 2.5rem; height: 2.5rem;"><i class="far fa-comment-alt"></i></button>
                                </div>
                            </div>
                            <div class="row px-3 py-2">
                                <div class="col" style="border-radius: 25px; box-shadow: 0px 0px 5px var(--gray-dark);">
                                    <div class="row">
                                        <div class="col d-flex justify-content-xxl-start align-items-xxl-center">
                                            <input class="flex-fill" type="text" placeholder="Email Address" inputmode="email" style="height: 100%; padding-left: 10px; padding-right: 10px;" /></div>
                                        <div class="col-5 d-flex align-items-xxl-center">
                                            <button class="btn btn-primary flex-fill" type="button" style="background: rgb(119,40,32); font-weight: bold;">Start Conversation</button></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col" style="overflow-x: none; overflow-y: auto; max-height: 60vh; height: auto;">
                                    <ul class="list-unstyled">
                                        <li style="cursor: pointer;">
                                            <div class="card border-0">
                                                <div class="card-body">
                                                    <span class="text-nowrap text-truncate text-uppercase text-white float-end p-1 text-center" style="width: 2rem; height: 2rem; border-radius: 15px; background: #00db5f;">1</span>
                                                    <h4 class="text-nowrap text-truncate card-title">Ximena Samano</h4>
                                                    <h6 class="text-nowrap text-truncate text-muted card-subtitle mb-2" style="font-size: .7rem;">19 de Julio de 2021, 11:53 AM</h6>
                                                    <h6 class="text-nowrap text-truncate text-muted card-subtitle mb-2">I can donate</h6>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col d-none d-sm-none d-md-none d-lg-block d-xl-block">
                            <div class="row">
                                <div class="col d-flex align-items-lg-center align-items-xl-center border-start border-muted" style="background: rgba(52,58,64,0.2); height: 4rem;">
                                    <button class="btn d-block d-sm-block d-md-block d-lg-none d-xl-none border-0 my-auto" type="button" style="width: 2.5rem; height: 2.5rem;"><i class="far fa-arrow-alt-circle-left"></i></button>
                                    <h5 class="mr-auto my-auto">Ximena Samano</h5>
                                    <span class="my-auto"></span>
                                </div>
                            </div>
                            <div class="row px-3 py-2 border-start border-muted">
                                <div class="col" style="overflow-x: none; overflow-y: auto; max-height: 55vh; height: 55vh;">
                                    <ul class="list-unstyled">
                                        <li class="my-2">
                                            <div class="card border border-muted" style="width: 65%; border-top-left-radius: 0px; border-top-right-radius: 20px; border-bottom-right-radius: 20px; border-bottom-left-radius: 20px; background: rgba(52,58,64,0.05);">
                                                <div class="card-body text-center p-2">
                                                    <p class="text-start card-text" style="font-size: 1rem;">I can donate</p>
                                                    <h6 class="text-muted card-subtitle text-end" style="font-size: .75rem;">Julio 22, 2021. 12:33 P.M.</h6>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="d-flex justify-content-end my-2">
                                            <div class="card border border-muted" style="width: 65%; border-top-left-radius: 20px; border-top-right-radius: 0px; border-bottom-right-radius: 20px; border-bottom-left-radius: 20px; background: rgba(52,58,64,0.05);">
                                                <div class="card-body text-center p-2">
                                                    <p class="text-start card-text" style="font-size: 1rem;">weh</p>
                                                    <h6 class="text-muted card-subtitle text-end" style="font-size: .75rem;">Julio 22, 2021. 12:33 P.M.</h6>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row px-3 py-2" style="background: rgba(52,58,64,0.2);">
                                <div class="col d-flex justify-content-xxl-center align-items-xxl-center">
                                    <textarea class="flex-fill" style="padding-left: 10px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px;"></textarea></div>
                                <div class="col-1 d-flex justify-content-xxl-center align-items-xxl-center">
                                    <button class="btn btn-light flex-fill h-100 w-auto" type="button" style="border-radius: 10px;"><i class="fab fa-telegram-plane"></i></button>
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
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/js/bs-init.js"></script>
    <script src="assets/js/theme.js"></script>
</body>

</html>
