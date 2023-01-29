<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USER_BLOGPOST.aspx.cs" Inherits="LifePoints.USER_BLOGPOST" EnableEventValidation="false" %>

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
                                                    <asp:LinkButton runat="server" ID="ViewNotifBtn" CommandName="ViewNotif" CommandArgument='<%# Eval("NTF_ID") %>'  CssClass="dropdown-item d-flex align-items-center"  >
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
                <h1 style="padding-left: 50px;">Posts</h1>
                <div class="container-fluid">
                    <div class="row" style="height: 100%;">
                        <div class="col-7" style="padding-left: 40px; padding-right: 40px; overflow: auto; max-height: 650px;">
                            <asp:Repeater runat="server" ID="BlogPosts" OnItemCommand="BlogPosts_ItemCommand">
                                <ItemTemplate>
                                    <div style="margin-bottom: 20px;">
                                        <div class="card">
                                            <div class="card-header" style="padding-right: 30px; padding-left: 30px;">
                                                <div class="row">
                                                    <div class="col-1 d-flex justify-content-xxl-center align-items-xxl-center" style="width: 72.8281px;">
                                                        <img class="img-fluid" src="assets/img/user.png" style="width: 62.8281px; height: 57.5938px; margin-top: 6px;" width="45" height="74">
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
                                                <h5 class="card-text"><%# Eval("BLOG_CONTENT") %></h5>
                                            </div>
                                            <div class="card-footer">
                                                <div class="row" style="padding-right: 30px; padding-left: 30px;">
                                                    <div class="col-1 d-flex justify-content-xxl-start align-items-xxl-center" style="width: fit-content;">
                                                        <asp:LinkButton runat="server" ID="ReportBtn" ForeColor="#606060"
                                                            CommandName="ReportPost" CommandArgument='<%# Eval("BLOG_ID") %>'
                                                            UseSubmitBehavior="false">
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
                        <div runat="server" class="col" style="padding-left: 50px; padding-right: 50px;" id="CompostBlogPost">
                            <div>
                                <asp:Panel ID="PostPanel" runat="server">
                                <div class="card" style="height: 300px;">
                                    <div class="card-header" style="padding-right: 30px; padding-left: 30px;">
                                        <h1 style="font-size: 30px;">Compost Post</h1>
                                    </div>
                                    <div class="card-body">
                                        <asp:TextBox runat="server" ID="BlogPostMessage" Class="form-control-lg" required="" name="Content" placeholder="Your Message..." spellcheck="true" Style="width: 100%; height: 100%; resize: none; border: none;" TextMode="MultiLine" />
                                    </div>
                                    <div class="card-footer">
                                        <div class="row" style="padding-right: 30px; padding-left: 30px;">
                                            <div class="col d-flex justify-content-xxl-end align-items-xxl-center">
                                                <asp:Button runat="server" ID="PostBlog" Text="POST" OnClick="PostBlog_Click" AutoPostBack="true" UseSubmitBehavior="false" ValidationGroup="none" Class="btn btn-primary" type="button" Style="background: rgb(119,40,32);" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </asp:Panel>
                            </div>
                            <div class="container-fluid" id="SendPrivateMessage" >
                                <asp:Panel ID="MessagePanel" runat="server" Visible="false">
                                <div class="card" style="height: 600px;">

                                    <div class="card-header" style="padding-right: 30px; padding-left: 50px;">
                                        <div class="row">
                                            <div class="col-xl-8 d-flex justify-content-xxl-start align-items-xxl-center">
                                               <asp:Label runat="server" id="ReceiverEmail" style="font-size:25px;"></asp:Label>
                                                <asp:Label runat="server" id="ReceiverID" Visible="false" style="font-size:25px;"></asp:Label>
                                             </div>
                                            <div class="col-1 col-xl-3 d-flex justify-content-xxl-center align-items-xxl-center" style="padding-right: 0px; padding-left: 115px;">
                                                <asp:ImageButton runat="server" src="assets/img/close.png" width="45" height="45" style="margin-left: 28px;" OnClick="Close_Click1" />
                                        </div>
                                        </div>
                                      </div>

                                   <div class="card-body" style="overflow: auto;">
                                    
                                       <div class="row" >
                                         <div class="col" >
                                           <asp:Repeater runat="server" ID="MessageReply" >
                                                <ItemTemplate>
                                                    <div class="col" style="margin-bottom: 5px;">
                                                        <div class="card">
                                                            <div class="card-header" style="padding-right:10px; padding-left: 10px;">
                                                               <h5 class="card-text"><%#  Eval("MESSAGE_NAME") %></h5>
                                                                <h6 class="card-text"><%# Eval("MESSAGE_TIME_SENT") %></h6>
                                                                
                                                             </div>
                                                             
                                                   
                                                            <div class="card-body">
                                                                <h5 class="card-text"><%# Eval("MESSAGE_CONTENT") %></h5>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </div>
                                          </div>
                                        
                                         </div>

                                    <div class="card-footer">
                                        <div class="row" style="padding-right: 30px; padding-left: 30px;">
                                            <div class="col d-flex justify-content-xxl-end align-items-xxl-center">
                                                 <asp:TextBox runat="server" ID="Mess" Class="form-control"  TextMode="Multiline"></asp:TextBox>

                                                 <asp:Button ID="SendBtn" runat="server" class="btn btn-danger" text="Send"  OnClick="Unnamed1_Click" AutoPostBack="true" stye="margin-left:5px;"></asp:Button>

                                             </div>
                                        </div>
                                    </div>

                                </div>
                                     </asp:Panel>
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
