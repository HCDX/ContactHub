<%@ Page Language="C#" Title="Subscription" AutoEventWireup="true" CodeFile="Unsubscription.aspx.cs" Inherits="Unsubscription" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>UnSubscription</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" type="image/ico" href="http://www.unhcr.org/favicon.ico" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/weather-icons.min.css" rel="stylesheet" />

    <link href="css/dibaw.min.css" rel="stylesheet" />
    <link href="css/dem.min.css" rel="stylesheet" />
    <link href="css/typicons.min.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />

    <script type="text/javascript" src="js/skins.min.js"></script>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        * {
            box-sizing: border-box;
        }

        /* Add padding to containers */
        .container {
            padding: 16px;
            background-color: white;
        }

        /* Full-width input fields */


        /* Overwrite default styles of hr */
        hr {
            border: 1px solid #f1f1f1;
            margin-bottom: 25px;
        }

        .autocomplete {
            /*the container must be positioned relative:*/
            position: relative;
            /*display: inline-block;*/
        }

        input {
            border: 1px solid transparent;
            background-color: #f1f1f1;
            padding: 5px;
            font-size: 16px;
        }

            input[type=text] {
                background-color: #f1f1f1;
                width: 100%;
            }

            input[type=submit] {
                background-color: DodgerBlue;
                color: #fff;
                cursor: pointer;
            }

        .autocomplete-items {
            position: absolute;
            border: 1px solid #d4d4d4;
            border-bottom: none;
            border-top: none;
            z-index: 99;
            /*position the autocomplete items to be the same width as the container:*/
            top: 100%;
            left: 0;
            right: 0;
        }

            .autocomplete-items div {
                padding: 10px;
                cursor: pointer;
                background-color: #fff;
                border-bottom: 1px solid #d4d4d4;
            }

                .autocomplete-items div:hover {
                    /*when hovering an item:*/
                    background-color: #e9e9e9;
                }

        .autocomplete-active {
            /*when navigating through the items using the arrow keys:*/
            background-color: DodgerBlue !important;
            color: #ffffff;
        }
    </style>
</head>


<body>



    <form runat="server">

        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

        </div>
        <div class="loading-container">
            <div class="loader"></div>
        </div>

        <div class="main-container container-fluid">
            <div class="page-container">

                <div class="page-header position-relative">
                    <div class="header-title">
                        <h1>ContactHub in Lebanon
                        </h1>
                    </div>
                    <div class="header-buttons">
                        <a class="sidebar-toggler" title="Hide the left sidebar" href="#">
                            <i class="fa fa-arrows-h"></i>
                        </a>
                        <a class="refresh" id="refresh-toggler" title="Refresh the page" href="#">
                            <i class="glyphicon glyphicon-refresh"></i>
                        </a>
                        <a class="fullscreen" id="fullscreen-toggler" title="Full screen" href="#">
                            <i class="glyphicon glyphicon-fullscreen"></i>
                        </a>
                    </div>
                </div>
                <div class="page-body">

                    <asp:UpdatePanel ID="UpdAdd" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>

                            <div class="row">
                                <div class="container">
                                    <h1>Unsubscription</h1>
                                    <h6>By clicking on <b>YES</b>, this subscription will be remove from the sector and area of intereset.
                                        Or click on <b>CANCEL</b> to cancel the action.
                                    </h6>
                                    <br />


                                    <div class="alert alert-success fade in" id="global_success" runat="server" visible="false">
                                        <button class="close" title="Close the alert" data-dismiss="alert">
                                            ×
                                        </button>
                                        <i class="fa-fw fa fa-check"></i>
                                        <strong>
                                            <asp:Literal ID="global_success_msg" runat="server"></asp:Literal></strong>
                                    </div>

                                    <div class="alert alert-danger fade in" id="global_error" runat="server" visible="false">
                                        <button class="close" title="Close the alert" data-dismiss="alert">
                                            ×
                                        </button>
                                        <i class="fa-fw fa fa-times"></i>
                                        <strong>Error ! </strong>
                                        <asp:Literal ID="global_error_msg" runat="server"></asp:Literal>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-12 col-xs-12">
                                            <div class="form-group ">
                                                <label for="psw"><b><i class="fa fa-user" style="color: #3c8dbc"></i>&nbsp;Full Name</b></label>
                                                <asp:TextBox ID="txtSubFullName" runat="server" type="text"
                                                    data-bv-message="This value is not valid"
                                                    data-bv-notempty="true"
                                                    data-bv-notempty-message="This is a mandatory field and can not be empty"
                                                    data-bv-regexp="true"
                                                    data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                    data-bv-regexp-message="Only allows alphanumeric characters"
                                                    class="form-control" placeholder=""
                                                    ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-12 col-xs-12">
                                            <div class="form-group ">
                                                <label for="txtSubEmail"><b><i class="fa fa-envelope" style="color: #3c8dbc"></i>&nbsp;Email</b></label>
                                                <asp:TextBox ID="txtSubEmail" runat="server" type="text"
                                                    data-bv-message="This value is not valid"
                                                    data-bv-notempty="true"
                                                    data-bv-notempty-message="This is a mandatory field and can not be empty"
                                                    data-bv-regexp="true"
                                                    data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                    data-bv-regexp-message="Only allows alphanumeric characters"
                                                    class="form-control" placeholder=""
                                                    ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-12 col-xs-12">
                                            <div class="form-group ">
                                                <label for="psw"><b><i class="fa fa-compress" style="color: #3c8dbc"></i>&nbsp;Title</b></label>
                                                <asp:TextBox ID="txtSubTitle" runat="server" type="text"
                                                    data-bv-message="This value is not valid"
                                                    data-bv-notempty="true"
                                                    data-bv-notempty-message="This is a mandatory field and can not be empty"
                                                    data-bv-regexp="true"
                                                    data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                    data-bv-regexp-message="Only allows alphanumeric characters"
                                                    class="form-control" placeholder=""
                                                    ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-12 col-xs-12">
                                            <div class="form-group ">
                                                <label for="email"><b><i class="fa fa-phone-square" style="color: #3c8dbc"></i>&nbsp;Phone (with country code)</b></label>
                                                <asp:TextBox ID="txtSubContact" runat="server" type="text"
                                                    data-bv-message="This value is not valid"
                                                    data-bv-notempty="true"
                                                    data-bv-notempty-message="This is a mandatory field and can not be empty"
                                                    data-bv-regexp="true"
                                                    data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                    data-bv-regexp-message="Only allows alphanumeric characters"
                                                    class="form-control" placeholder=""
                                                    ReadOnly="true">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-12 col-xs-12">
                                            <div class="form-group ">
                                                <div class="autocomplete">
                                                    <label for="email"><b><i class="fa fa-sitemap" style="color: #3c8dbc"></i>&nbsp;Organization</b></label>
                                                    <input runat="server" readonly="true" id="txtSubOrganization" type="text" name="myCountry" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row" runat="server" id="DivGrid">
                                        <div class="col-lg-12 col-sm-12 col-xs-12">
                                            <div class="widget">
                                                <div class="widget-header ">
                                                    <span class="widget-caption this-search-widget-caption">This Sector and Area of Interest will be removed from the mailing list</span>
                                                </div>

                                                <div class="widget-body">
                                                    <div>

                                                        <div class="row" runat="server" id="divEntGridView">
                                                            <div class="col-lg-12 col-sm-12 col-xs-12">
                                                                <div class="well with-header with-footer">
                                                                    <div class="header bordered-blueberry this-search-widget-caption">
                                                                        <div style="float: left">List of Interest</div>
                                                                        <div style="float: right">Total : <span style="color: red"><%=l%> </span>Interest(s) </div>
                                                                    </div>

                                                                    <div runat="server" id="no_data">
                                                                        <asp:Label ID="NoDatalbl" class="h3" runat="server"></asp:Label>
                                                                    </div>
                                                                    <asp:GridView
                                                                        ID="EntGridView"
                                                                        runat="server"
                                                                        AutoGenerateColumns="False"
                                                                        AllowPaging="true"
                                                                        OnRowDataBound="EntGridView_RowDataBound"
                                                                        DataKeysName="ModuleID"
                                                                        PageSize="8"
                                                                        Style="font-size: 14px"
                                                                        OnPreRender="EntGridView_PreRender"
                                                                        OnPageIndexChanging="EntGridView_PageIndexChanging"
                                                                        CssClass="table table-striped table-bordered table-hover  ">

                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                                                HeaderText="Sector Name">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="InterestID" Value='<%# Bind("InterestID") %>' runat="server" />
                                                                                    <asp:HiddenField ID="InterestSector" runat="server" Value='<%# Bind("InterestSector") %>' />
                                                                                    <asp:HiddenField ID="InterestArea" runat="server" Value='<%# Bind("InterestArea") %>' />
                                                                                    <asp:Label ID="SectorInterestName" runat="server" Text='<%# Bind("SectorInterestName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                                                HeaderText="Area of Interest">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="AreaInterestName" runat="server" Text='<%# Bind("AreaInterestName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                        </Columns>

                                                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                                                        <PagerSettings Position="Bottom"
                                                                            Mode="Numeric"
                                                                            FirstPageText="First"
                                                                            LastPageText="Last"
                                                                            NextPageText="Next"
                                                                            PreviousPageText="Prev" />

                                                                    </asp:GridView>

                                                                    <div id="deleteModal" class="bootbox modal fade modal-silver in" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">

                                                                        <asp:UpdatePanel ID="upDel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="BtnDelete" EventName="Click" />
                                                                            </Triggers>
                                                                            <ContentTemplate>
                                                                                <div class="modal-dialog">
                                                                                    <div class="modal-content">
                                                                                        <div class="modal-header">
                                                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                                                            <h4 class="modal-title">ContactHub Lebanon</h4>
                                                                                        </div>
                                                                                        <div class="modal-body">
                                                                                            <asp:Label ID="lblDeletemessage" runat="server" Text="This subscription will be deleted. Please confirm"></asp:Label>
                                                                                        </div>
                                                                                        <div class="modal-footer">
                                                                                            <asp:Button ID="BtnDelete" UseSubmitBehavior="false" type="button" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="BtnDelete_Click" />
                                                                                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                                                                        </div>

                                                                                    </div>
                                                                                    <!-- /.modal-content -->
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>

                                                                    <div class="modal fade" id="overlay">
                                                                        <div class="modal-dialog">
                                                                            <div class="modal-content">
                                                                                <div class="modal-header">
                                                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                                    <h4 class="modal-title">Modal title</h4>
                                                                                </div>
                                                                                <div class="modal-body">
                                                                                    <p>Context here</p>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                    <div class="footer">
                                                                        <h6>Execution time :
                    <asp:Literal ID="execTimeLit" runat="server"></asp:Literal>
                                                                        </h6>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group " style="float: right">
                                        <asp:Button ID="BtnUnsubscribe" CssClass="btn btn-danger" ValidationGroup="authentication_box" OnClick="BtnUnsubscribe_Click" runat="server" Text="Unsubscribe" />
                                        <%--<i class="fa fa-plus"></i>--%>
                                    </div>
                                    <br />
                                    <br />
                                    <br />


                                </div>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>
        </div>

        <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <script type="text/javascript" src="js/slimscroll/jquery.slimscroll.min.js"></script>

        <script type="text/javascript" src="js/dibaw.js"></script>
        <script type="text/javascript" src="js/bootbox.js"></script>
        <script type="text/javascript" src="js/doAppmin.js"></script>

        <script type="text/javascript" src="js/charts/sparkline/jquery.sparkline.js"></script>
        <script type="text/javascript" src="js/charts/sparkline/sparkline-init.js"></script>

        <script type="text/javascript" src="js/charts/easypiechart/jquery.easypiechart.js"></script>
        <script type="text/javascript" src="js/charts/easypiechart/easypiechart-init.js"></script>

        <script type="text/javascript" src="js/charts/chartjs/Chart.js"></script>
        <script type="text/javascript" src="js/charts/chartjs/chartjs-init.js"></script>

        <script type="text/javascript">
            function autocomplete(inp, arr) {
                /*the autocomplete function takes two arguments,
                the text field element and an array of possible autocompleted values:*/
                var currentFocus;
                /*execute a function when someone writes in the text field:*/
                inp.addEventListener("input", function (e) {
                    var a, b, i, val = this.value;
                    /*close any already open lists of autocompleted values*/
                    closeAllLists();
                    if (!val) { return false; }
                    currentFocus = -1;
                    /*create a DIV element that will contain the items (values):*/
                    a = document.createElement("DIV");
                    a.setAttribute("id", this.id + "autocomplete-list");
                    a.setAttribute("class", "autocomplete-items");
                    /*append the DIV element as a child of the autocomplete container:*/
                    this.parentNode.appendChild(a);
                    /*for each item in the array...*/
                    for (i = 0; i < arr.length; i++) {
                        /*check if the item starts with the same letters as the text field value:*/
                        if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                            /*create a DIV element for each matching element:*/
                            b = document.createElement("DIV");
                            /*make the matching letters bold:*/
                            b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                            b.innerHTML += arr[i].substr(val.length);
                            /*insert a input field that will hold the current array item's value:*/
                            b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                            /*execute a function when someone clicks on the item value (DIV element):*/
                            b.addEventListener("click", function (e) {
                                /*insert the value for the autocomplete text field:*/
                                inp.value = this.getElementsByTagName("input")[0].value;
                                /*close the list of autocompleted values,
                                (or any other open lists of autocompleted values:*/
                                closeAllLists();
                            });
                            a.appendChild(b);
                        }
                    }
                });
                /*execute a function presses a key on the keyboard:*/
                inp.addEventListener("keydown", function (e) {
                    var x = document.getElementById(this.id + "autocomplete-list");
                    if (x) x = x.getElementsByTagName("div");
                    if (e.keyCode == 40) {
                        /*If the arrow DOWN key is pressed,
                        increase the currentFocus variable:*/
                        currentFocus++;
                        /*and and make the current item more visible:*/
                        addActive(x);
                    } else if (e.keyCode == 38) { //up
                        /*If the arrow UP key is pressed,
                        decrease the currentFocus variable:*/
                        currentFocus--;
                        /*and and make the current item more visible:*/
                        addActive(x);
                    } else if (e.keyCode == 13) {
                        /*If the ENTER key is pressed, prevent the form from being submitted,*/
                        e.preventDefault();
                        if (currentFocus > -1) {
                            /*and simulate a click on the "active" item:*/
                            if (x) x[currentFocus].click();
                        }
                    }
                });
                function addActive(x) {
                    /*a function to classify an item as "active":*/
                    if (!x) return false;
                    /*start by removing the "active" class on all items:*/
                    removeActive(x);
                    if (currentFocus >= x.length) currentFocus = 0;
                    if (currentFocus < 0) currentFocus = (x.length - 1);
                    /*add class "autocomplete-active":*/
                    x[currentFocus].classList.add("autocomplete-active");
                }
                function removeActive(x) {
                    /*a function to remove the "active" class from all autocomplete items:*/
                    for (var i = 0; i < x.length; i++) {
                        x[i].classList.remove("autocomplete-active");
                    }
                }
                function closeAllLists(elmnt) {
                    /*close all autocomplete lists in the document,
                    except the one passed as an argument:*/
                    var x = document.getElementsByClassName("autocomplete-items");
                    for (var i = 0; i < x.length; i++) {
                        if (elmnt != x[i] && elmnt != inp) {
                            x[i].parentNode.removeChild(x[i]);
                        }
                    }
                }
                /*execute a function when someone clicks in the document:*/
                document.addEventListener("click", function (e) {
                    closeAllLists(e.target);
                });
            }

            /*An array containing all the country names in the world:*/
            var countries = ["ABAAD", "ACF", "ACTED", "ActionAid", "ADRA", "Al Mithaq", "Al-Fayhaa", "ALLC", "AlMajmoua", "Alpha", "AMEL", "Ana Aqra", "AND", "ARCS"
            , "AVSI", "B&Z", "Basmeh & Zeitooneh", "Bluemission", "CARE", "Caritas Lebanon", "CESVI", "ISP", "CISP", "CONCERN", "CWW", "DAF", "Danish Red Cross/Lebanese Red Cross"
            , "Dar El Fatwa", "Default", "Dorcas", "DPNA", "DRC", "DRC/LRC", "FAO", "Fondation-merieux", "FPSC - Lebanon", "GVC", "HDA", "HDA Associaition", "Heartland"
            , "HI", "Hilfswerk Austria International", "Himaya", "Humedica", "ICU", "ILO", "IMC", "INARA", "Intersos", "IOCC", "IOM", "IR", "IR Lebanon", "IRAP", "IRC"
            , "KAFA", "Kayany", "Leb Relief", "Lebanese Developers", "Lebanese Red Cross", "Lebanon Support", "LECORVAW", "LFPADE", "LOST", "LRC", "LSESD", "Makassed"
            , "Makhzoumi", "Makhzoumi Foundation", "MCC", "MDM", "MEDAIR", "Medecin du Monde", "Mercy Corps", "MoSA", "Mouvement Social", "MSF", "Nabad", "Near East Foundation"
            , "NRC", "Order of Malta", "OWS", "OXFAM", "PCPM", "PCRF", "PU-AMI", "Red Oak", "RESTART Lebanon", "RI", "Right to Play", "RMF", "SAFADI", "SAMS", "SAWA Group", "SB Overseas", "SBO", "SCI", "SFCG", "SHEILD"
            , "SHIELD", "SIF", "Solidar Suisse", "Solidarités", "Solidarites International", "TdH - It", "TdH - L", "TdH-It", "UNDP", "UNHCR", "UNICEF"
            , "UNIDO", "UNRWA", "URDA", "WCH", "WFP", "WRF", "WVI", "YFORD", "YNCA"];

            /*initiate the autocomplete function on the "myInput" element, and pass along the countries array as possible autocomplete values:*/
            autocomplete(document.getElementById("txtSubOrganization"), countries);
        </script>

    </form>
</body>
</html>

