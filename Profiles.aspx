<%@ Page Title="" Language="C#" MasterPageFile="~/immain.Master" AutoEventWireup="true" CodeFile="Profiles.aspx.cs" Inherits="Profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeadPlaceHolder" runat="server">
    <title>Profile </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderTitlePlaceHolder" runat="server">
    <div class="header-title">
        <h1>Management of Profiles
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

    </div>

    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="widget">
                <div class="widget-header ">
                    <span class="widget-caption this-search-widget-caption">Search</span>
                </div>
                <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="widget-body">
                            <div>
                                <div class="row">
                                    <div class="col-sm-7">
                                        <div class="form-group ">
                                            <asp:TextBox ID="txtSearch" runat="server" type="text"
                                                class="form-control" placeholder="Type a Profile">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <asp:Button ID="BtnSearch" CssClass="btn btn-default" UseSubmitBehavior="false" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="well with-header with-footer">
                <asp:UpdatePanel ID="upCrudGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="header bordered-blueberry this-search-widget-caption">
                            <div style="float: left">List of Profiles</div>
                            <div style="float: right">Total : <span style="color: red"><%=l%> </span>Profiles </div>
                        </div>
                        <div runat="server" id="no_data">
                            <asp:Label ID="NoDatalbl" class="h3" runat="server"></asp:Label>
                        </div>
                        <asp:GridView
                            ID="EntGridView"
                            runat="server"
                            AutoGenerateColumns="False"
                            OnRowCommand="EntGridView_RowCommand"
                            AllowPaging="true"
                            OnRowDataBound="EntGridView_RowDataBound"
                            DataKeysName="ModuleID"
                            PageSize="10"
                            Style="font-size: 14px"
                            OnPreRender="EntGridView_PreRender"
                            OnPageIndexChanging="EntGridView_PageIndexChanging"
                            CssClass="table table-striped table-bordered table-hover  ">

                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Sector Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="ProfileID" Value='<%# Bind("ProfileID") %>' runat="server" />
                                        <asp:Label ID="ProfileName" runat="server" Text='<%# Bind("ProfileName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="ProfileDescription" runat="server" Text='<%# Bind("ProfileDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Last Modified By">
                                    <ItemTemplate>
                                        <asp:Label ID="UserFullName" runat="server" Text='<%# Bind("UserFullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Modified Date">
                                    <ItemTemplate>
                                        <asp:Label ID="LastModifiedDate" runat="server" Text='<%# Bind("LastModifiedDate" ,"{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ACTIONS" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <div class="row-command-preview" style="text-align: center">
                                            <asp:LinkButton ID="BtnEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-default btn-xs purple" CommandName="view" Text="Update"><i class="fa fa-edit"></i>  Update</asp:LinkButton>
                                            <asp:LinkButton ID="BtnDelete" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-default btn-xs red" CommandName="deleting" Text="Delete"><i class="fa fa-trash-o"></i>  Delete</asp:LinkButton>
                                        </div>
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
                    </ContentTemplate>
                    <Triggers></Triggers>
                </asp:UpdatePanel>


                <div id="addEdiModal" class="bootbox modal fade modal-silver in" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">
                    <asp:UpdatePanel ID="upEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="EntGridView" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        <h4 class="modal-title">Humanitarian Addres Book</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-lg-12 col-sm-12 col-xs-12">

                                                <div class="alert alert-danger fade in" id="Edit_global" runat="server" visible="false">
                                                    <button class="close" title="Close the alert" data-dismiss="alert">
                                                        ×
                                                    </button>
                                                    <i class="fa-fw fa fa-check"></i>
                                                    <strong>
                                                        <asp:Literal ID="Edit_global_alert_msg" runat="server"></asp:Literal></strong>
                                                </div>

                                            </div>
                                        </div>
                                        <span class="alert-header darker">Add/Update a Sector</span>
                                        <div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-sm-12 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="txtProfileName">Profile Name</label>
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtProfileName" runat="server" type="text"
                                                                        data-bv-message="This value is not valid"
                                                                        data-bv-notempty="true"
                                                                        data-bv-notempty-message="Mandatory field and can not be empty"
                                                                        data-bv-regexp="true"
                                                                        data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                                        data-bv-regexp-message="Alphanumeric field only"
                                                                        class="form-control" placeholder="">
                                                                    </asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-sm-12 col-xs-12">
                                                            <div class="form-group ">
                                                                <label for="txtProfileDescription">Description</label>
                                                                <asp:TextBox ID="txtProfileDescription" runat="server" type="text"
                                                                    data-bv-message="This value is not valid"
                                                                    data-bv-notempty="true"
                                                                    data-bv-notempty-message="Mandatory field and can not be empty"
                                                                    data-bv-regexp="true"
                                                                    data-bv-regexp-regexp="[a-zA-Z0-9_\.]+"
                                                                    data-bv-regexp-message="Alphanumeric field only"
                                                                    TextMode="MultiLine" Rows="4"
                                                                    class="form-control" placeholder="">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="BtnSave" CssClass="btn btn-orange" UseSubmitBehavior="false" runat="server" OnClick="BtnSave_Click" Text="Save" />
                                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

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
                                        <asp:Label ID="lblDeletemessage" runat="server" Text="This data will be deleted. Please confirm"></asp:Label>
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

                <div class="footer">
                    Execution time :
                    <asp:Literal ID="execTimeLit" runat="server"></asp:Literal>
                </div>

            </div>


        </div>

    </div>

</asp:Content>
