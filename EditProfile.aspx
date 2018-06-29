<%@ Page Title="" Language="C#" MasterPageFile="~/immain.Master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeadPlaceHolder" runat="server">
    <title>Edit Profile </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderTitlePlaceHolder" runat="server">
    <div class="header-title">
        <h1>Edit your Profile
        </h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyPlaceHolder" runat="server">
    <div class="row">
        <div class="col-lg-6 col-sm-6 col-xs-12">
            <asp:UpdatePanel ID="UpdatePanel"
                UpdateMode="Conditional"
                runat="server">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="row">
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <h3><b><%= _userFullName %>  </b></h3>

                    <div class="well bordered-left bordered-themeprimary" style="opacity: 0.9; margin-top: 25px">
                        <div class="row">
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUsername">Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" type="text"
                                        class="form-control" placeholder=""
                                        ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtProfile">Profile</label>
                                    <asp:TextBox ID="txtProfile" runat="server" type="text"
                                        class="form-control" placeholder=""
                                        ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserFullName">Full Name</label>
                                    <asp:TextBox ID="txtUserFullName" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserOrganization">Organization</label>
                                    <asp:TextBox ID="txtUserOrganization" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserTitle">Title</label>
                                    <asp:TextBox ID="txtUserTitle" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserMail">Email</label>
                                    <asp:TextBox ID="txtUserMail" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserPhone">Phone</label>
                                    <asp:TextBox ID="txtUserPhone" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserAddress">Address</label>
                                    <asp:TextBox ID="txtUserAddress" runat="server" type="text"
                                        class="form-control" placeholder="">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <label for="txtUserNote">Note</label>
                                    <asp:TextBox ID="txtUserNote" runat="server" type="text"
                                        class="form-control" TextMode="MultiLine" Rows="3">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:LinkButton ID="BtnUpdate" runat="server" CssClass="btn btn-blue-eu" OnClick="BtnUpdate_Click"> <i class="fa fa-plus"></i> Update </asp:LinkButton>
                    </div>
                    <div class="footer" style="float: right">
                    </div>
                    <div class="well bordered-left bordered-themeprimary" style="opacity: 0.9; margin-top: 25px">
                        <div class="row">
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    
                                    <asp:TextBox ID="txtUserPasswordNew" runat="server" type="text"
                                        class="form-control" placeholder="New Password" TextMode="Password">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    
                                    <asp:TextBox ID="txtUserPasswordConfirm" runat="server" type="text"
                                        class="form-control" placeholder="Confirm the Password" TextMode="Password">
                                    </asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-12 col-xs-12">
                                <div class="form-group ">
                                    <asp:LinkButton ID="BtnChangePwd" runat="server" CssClass="btn btn-blue-eu" OnClick="BtnChangePwd_Click"> <i class="fa fa-key"></i> Change </asp:LinkButton>
                                </div>

                            </div>
                            
                            <%-- <div class="input-group">
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">Go!</button>
                                </span>
                                <input type="text" class="form-control">
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <div class="row">

        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="form-group " style="float: left">
                <asp:UpdatePanel ID="UpdAdd" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:LinkButton ID="BtnNew" runat="server" OnClick="BtnNew_Click" CssClass="btn btn-blue-eu"> <i class="fa fa-plus"></i> Add New </asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-group" style="float: right">
                <a class="btn btn-magenta" href="javascript:window.open('','_self')"><i class="fa fa-print"></i>Print List</a>
            </div>
        </div>

    </div>--%>

    <div class="row" id="homeback"></div>


</asp:Content>

