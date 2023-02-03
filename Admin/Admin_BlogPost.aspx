<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_BlogPost.aspx.cs" Inherits="LifePoints.Admin.Admin_BlogPost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Blog Posts | Admin</title>
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
                    <li class="nav-item"><a class="nav-link active" href="Admin_BlogPost.aspx"><i class="fas fa-tachometer-alt" style="font-size: 20px;"></i><span style="font-size: 15px;">Blog Post</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="BB_BloodTransaction.aspx"><i class="fa fa-tint" style="font-size: 20px;"></i><span style="font-size: 15px;">Blood Transaction</span></a></li>
                    <li class="nav-item"><a class="nav-link" href="Admin_Reports.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Reports</span></a></li> 
                    <li class="nav-item"><a class="nav-link" href="BB_ActionLogs.aspx"><i class="fa fa-list-ul" style="font-size: 20px;"></i><span style="font-size: 15px;">Action Log</span></a></li>

                    </ul>
                <div class="text-center d-none d-md-inline"></div>
            </div>
        </nav>
        <div class="d-flex flex-column" id="content-wrapper">
            <div id="content" style="background: linear-gradient(rgb(249,243,243) 28%, white), #ffffff;">
                <nav class="navbar navbar-light navbar-expand bg-white shadow mb-4 topbar static-top">
                    <div class="container-fluid"><button class="btn btn-link d-md-none rounded-circle mr-3" id="sidebarToggleTop" type="button"><i class="fas fa-bars"></i></button>
                        <div class="form-inline d-none d-sm-inline-block mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search"><input class="form-control-plaintext" type="text" value="Post"  readonly="" style="font-size: 40px;"></div>
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
                                        <a class="dropdown-item" href="BB_Profile.aspx"><i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>&nbsp;Profile</a>
                                        <div class="dropdown-divider"></div>
                                        <a class="dropdown-item" runat="server" id="BtnLogout" autopostback="true" onserverclick="BtnLogout_ServerClick"><i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>&nbsp;Logout</a>

                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </nav>

 
                <div class="container-fluid">
                    <div class="row" style="height: 100%;">
                        <div class="col-8" style="padding-left: 40px; padding-right: 40px; overflow: auto; max-height: 750px;">
                            <asp:Repeater runat="server" ID="BlogPosts">
                                <ItemTemplate>
                                    <div style="margin-bottom: 20px;">
                                        <div class="card">
                                            <div class="card-header" style="padding-right: 30px; padding-left: 30px;">
                                                <div class="row">
                                                    <div class="col-1 d-flex justify-content-xxl-center align-items-xxl-center" style="width: 72.8281px;">
                                                        <img class="img-fluid" src="assets/img/user.png" style="width: 57.8281px; height: 57.5938px; margin-top: 6px;" width="45" height="74">
                                                    </div>
                                                    <div class="col">
                                                        <div class="row d-flex flex-column">
                                                            <div class="col">
                                                                <h3 class="fs-3"><%# Eval("BLOG_UACC_NAME") %></h3>
                                                            </div>
                                                            <div class="col d-flex" style="flex-direction: column">
                                                                <h4 class="text-lowercase fs-5"><%# Eval("BLOG_UACC_EMAIL") %></h4>
                                                                <h5><%# Eval("BLOG_DATE") %></h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <h4 class="card-text"><%# Eval("BLOG_CONTENT") %></h4>
                                            </div>
                                            <div class="card-footer">
                                                <div class="row" style="padding-right: 30px; padding-left: 30px;">
                                                    <div class="col-1 d-flex justify-content-xxl-start align-items-xxl-center" style="width: fit-content;">
                                                        <asp:LinkButton runat="server" ID="ReportBtn" ForeColor="#606060" 
                                                            CommandName="ReportPost" CommandArgument='<%# Eval("BLOG_ID") %>'
                                                            UseSubmitBehavior="false" >
                                                            <asp:Image runat="server" ImageUrl="~/assets/img/dislike.png" style="width: min(8vw, 20px); height: fit-content; margin-right: 5vw;" />
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-2 d-flex align-items-xxl-center"><span style="font-weight: bold; color: rgb(119,40,32);"><%#Eval("BLOG_REPORT") %></span></div>
                                                    <div class="col d-flex justify-content-xxl-end align-items-xxl-center">
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
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
