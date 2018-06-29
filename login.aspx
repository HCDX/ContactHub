<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login" Title="Log in to Address Book" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" type="image/ico" href="http://www.unhcr.org/favicon.ico" />

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/weather-icons.min.css" rel="stylesheet" />

    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,600,700,300" rel="stylesheet" type="text/css">

    <link href="css/dibaw.min.css" rel="stylesheet" />
    <link href="css/dem.min.css" rel="stylesheet" />
    <link href="css/typicons.min.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />

    <script type="text/javascript" src="js/skins.min.js"></script>
</head>
<!-- /Head -->
<body>
    <form runat="server">
        <div class="login-container animated fadeInDown">
            <div class="loginbox bg-white">
                <div class="loginbox-title">CONNEXION</div>
                <div class="loginbox-social">
                    <div class="social-title "><a style="color: #36638E;" href="login.aspx">ContactHub Lebanon</a></div>
                    <div class="social-buttons">
                        <div class="eu-logo">
                            <img src="img/home_back.png" alt="Inter-Agency Coordination" height="62" width="250" />
                        </div>
                    </div>
                </div>
                <div class="loginbox-or">
                    <div class="or-line"></div>
                    <div class="or">Login</div>
                </div>
                <div class="loginbox-textbox">
                    <asp:TextBox ID="txtUsername" class="form-control" placeholder="Username" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_login" runat="server" CssClass="rfv"
                        ControlToValidate="txtUsername" EnableClientScript="true"
                        ErrorMessage="Please enter your username" SetFocusOnError="true"
                        Text="(*)" ValidationGroup="authentication_box">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="loginbox-textbox">
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" placeholder="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_pwd" runat="server" CssClass="rfv"
                        ControlToValidate="txtPassword" EnableClientScript="true"
                        ErrorMessage="Please enter your password" SetFocusOnError="true"
                        Text="(*)" ValidationGroup="authentication_box">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="loginbox-forgot">
                    <asp:LinkButton ID="BtnForgottenPwd" runat="server" OnClick="BtnForgottenPwd_Click" ToolTip="Click here to change your password">Password forgotten ?</asp:LinkButton>
                </div>
                <div class="loginbox-forgot">
                    <asp:LinkButton ID="BtnSubscribe" runat="server" OnClick="BtnSubscribe_Click" ToolTip="Click to Subscribe to a mailing list"> Subscribe </asp:LinkButton>
                </div>
                
                <div class="loginbox-submit">
                    <asp:Button ID="BtnLogin" CssClass="btn btn-darkorange btn-block" ValidationGroup="authentication_box" OnClick="BtnLogin_Click" runat="server" Text="Sign in" />
                </div>

            </div>
            <div class="validationbox">
                <asp:ValidationSummary ID="ValidationSummary1"
                    runat="server" DisplayMode="List"
                    CssClass="validation-summary"
                    ShowMessageBox="false" ShowSummary="true"
                    ValidationGroup="authentication_box" />
            </div>
            <div class="validationbox" runat="server" id="errorSummaryBox" visible="false">
                <div class="validation-summary">
                    <asp:Literal ID="errorSummaryLiteral" runat="server"></asp:Literal>
                </div>
            </div>

        </div>
    </form>
    <!--Basic Scripts-->
    <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/slimscroll/jquery.slimscroll.min.js"></script>

    <!--MOPE manager Scripts-->
    <script src="js/dibaw.js"></script>

</body>
