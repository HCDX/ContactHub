<%@ Page Title="" Language="C#" MasterPageFile="~/immain.Master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ContactList.aspx.cs" Inherits="ContactList" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeadPlaceHolder" runat="server">
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <title>Contact List </title>

    <script type="text/javascript">
        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var inputList = row.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        row.style.backgroundColor = "";
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderTitlePlaceHolder" runat="server">
    <div class="header-title">
        <h1>Management of Contact List
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
            <div class="form-group " style="float: right">
                <asp:UpdatePanel ID="UpCopy" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnCopyEmail" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDeleteModal" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnImportModal" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:LinkButton ID="BtnCopyEmail" runat="server" OnClick="BtnCopyEmail_Click" CssClass="btn btn-blue-eu"> <i class="fa fa-print"></i> Copy Email </asp:LinkButton>
                        <asp:LinkButton ID="BtnDeleteModal" runat="server" OnClick="BtnDeleteModal_Click" CssClass="btn btn-danger"> <i class="fa fa-trash-o"></i> Delete Selected </asp:LinkButton>
                        <asp:LinkButton ID="BtnImportModal" runat="server" OnClick="BtnImportModal_Click" CssClass="btn btn-success"> <i class="fa fa-file-excel-o"></i> Import from file </asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-group " style="float: left">
                <asp:UpdatePanel ID="UpExport" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <asp:LinkButton ID="BtnAddNew" runat="server" OnClick="BtnAddNew_Click" CssClass="btn btn-info"> <i class="fa fa-user-circle-o"></i> Add a Contact </asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="form-group " style="float: left">
                <asp:LinkButton ID="BtnExportToExcel" runat="server" OnClick="BtnExportToExcel_Click" CssClass="btn btn-warning"> <i class="fa fa-arrow-alt-to-top"></i> Export to Excel</asp:LinkButton>
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
                                    <div class="col-sm-4">
                                        <div class="form-group ">
                                            <asp:TextBox ID="txtSearch" runat="server" type="text"
                                                class="form-control" placeholder="">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <asp:Button ID="BtnSearch" CssClass="btn btn-default" UseSubmitBehavior="false" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group ">

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-map-marker" style="color: #3c8dbc"></i>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlSectorInterest" class="form-control select2"
                                                    OnSelectedIndexChanged="ddlSectorInterest_Changed"
                                                    AutoPostBack="true"
                                                    data-bv-regexp="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="form-group ">

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-map-marker" style="color: #3c8dbc"></i>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlAreaInterest" class="form-control select2"
                                                    OnSelectedIndexChanged="ddlAreaInterest_Changed"
                                                    AutoPostBack="true"
                                                    data-bv-regexp="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
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
                            <div style="float: left">List of Contacts</div>
                            <div style="float: right">Total : <span style="color: red"><%=l%> </span>Contacts </div>
                        </div>
                        <div runat="server" id="no_data">
                            <asp:Label ID="NoDatalbl" class="h3" runat="server"></asp:Label>
                        </div>
                        <asp:GridView ID="gvAll" runat="server" CssClass="table table-striped table-bordered table-hover  "
                            AutoGenerateColumns="false"
                            OnRowCommand="gvAll_RowCommand"
                            AllowPaging="true"
                            EmptyDataText="No contact found."
                            OnPageIndexChanging="OnPaging" PageSize="10">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="checkbox" runat="server" visible="false">
                                            <label>
                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);"
                                                    AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                                <span class="text"></span>
                                            </label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="checkbox" runat="server">
                                            <label>
                                                <asp:CheckBox ID="chk" runat="server" onclick="Check_Click(this)"
                                                    AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                                <span class="text"></span>
                                            </label>
                                        </div>
                                        <asp:HiddenField ID="SubscriberID" Value='<%# Bind("SubscriberID") %>' runat="server" />
                                        <asp:HiddenField ID="InterestID" Value='<%# Bind("InterestID") %>' runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="InterestID" HeaderText="ID"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SectorInterestName" HeaderText="Sector"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="AreaInterestName" HeaderText="Area"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberOrganization" HeaderText="Organization"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberFullName" HeaderText="FullName"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberTitle" HeaderText="Title"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberEmail" HeaderText="Email"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberContact" HeaderText="Phone Number"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="SubscriberAddedDate" HeaderText="Added Date"
                                    HtmlEncode="false" />
                                <asp:TemplateField HeaderText="ACTIONS">
                                    <ItemTemplate>
                                        <div class="row-command-preview" style="text-align: center">
                                            <asp:LinkButton ID="BtnEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-default btn-xs purple" CommandName="view" Text="Update"><i class="fa fa-edit"></i>  Update</asp:LinkButton>
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
                        <br />
                        <asp:GridView ID="gvSelected" runat="server" Visible="false"
                            AutoGenerateColumns="false" Font-Names="Arial"
                            Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green" EmptyDataText="No Records Selected">
                            <Columns>
                                <asp:BoundField DataField="InterestID" HeaderText="InterestID"
                                    HtmlEncode="false" />

                                <asp:BoundField DataField="SectorInterestName" HeaderText="Sector" />
                                <asp:BoundField DataField="AreaInterestName" HeaderText="Area" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers></Triggers>
                </asp:UpdatePanel>


                <div class="footer">
                    Execution time :
                    <asp:Literal ID="execTimeLit" runat="server"></asp:Literal>
                </div>

                <div id="addEdiModal" class="bootbox modal fade modal-silver in" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">
                    <asp:UpdatePanel ID="upEdit" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvAll" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        <h4 class="modal-title">ContactHub</h4>
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
                                        <span class="alert-header darker">View Contact Information</span>
                                        <div>
                                            <div class="row">
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSectorInterestName">
                                                            Sector
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSectorInterestName" runat="server" type="text"
                                                                class="form-control" ReadOnly="true">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtAreaInterestName">
                                                            Area
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtAreaInterestName" runat="server" type="text"
                                                                class="form-control" ReadOnly="true">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberFullName">
                                                            Full Name
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSubscriberFullName" runat="server" type="text"
                                                                class="form-control">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberEmail">
                                                            Email
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSubscriberEmail" runat="server" type="text"
                                                                class="form-control" placeholder="">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberTitle">
                                                            Title
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSubscriberTitle" runat="server" type="text"
                                                                class="form-control" placeholder="">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberOrganization">
                                                            Organization
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSubscriberOrganization" runat="server" type="text"
                                                                class="form-control" placeholder="">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberContact">
                                                            Phone Number
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSubscriberContact" runat="server" type="text"
                                                                class="form-control" placeholder="">
                                                            </asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                          

                                        </div>
                                        <div class="row" runat="server" id="divSubscriptList" visible="false">
                                                <div class="col-lg-12 col-sm-12 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="txtSubscriberContact">
                                                            Personal Subscription List
                                                        </label>
                                                        <div class="form-group">
                                                            <asp:GridView
                                                    ID="EntGridView"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    AllowPaging="true"
                                                    OnRowDataBound="EntGridView_RowDataBound"
                                                    DataKeysName="SectorInterestID"
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
                                                                <asp:HiddenField ID="SubscriberID" Value='<%# Bind("SubscriberID") %>' runat="server" />
                                                                <asp:HiddenField ID="SubscriberEmail" Value='<%# Bind("SubscriberEmail") %>' runat="server" />
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

                <div id="copyEmailModal" class="bootbox modal fade modal-silver in" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">
                    <asp:UpdatePanel ID="upEmail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        <h4 class="modal-title">ContactHub</h4>
                                    </div>
                                    <div class="modal-body">
                                        <span class="alert-header darker">COPY THE SELECTED EMAILS</span>
                                        <div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">

                                                        <CKEditor:CKEditorControl ID="CKEmail" runat="server"></CKEditor:CKEditorControl>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Exit</button>
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
                                        <asp:Label ID="lblDeletemessage1" runat="server" Text="This subscriber will be removed from your contact list. Please confirm"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="BtnDelete" UseSubmitBehavior="false" type="button" CssClass="btn btn-danger" runat="server" Text="Remove" OnClick="BtnDelete_Click" />
                                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

    <div id="importContactModal" class="bootbox modal fade modal-silver in" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">ContactHub</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-sm-12 col-xs-12">

                            <div class="alert alert-danger fade in" id="Div1" runat="server" visible="false">
                                <button class="close" title="Close the alert" data-dismiss="alert">
                                    ×
                                </button>
                                <i class="fa-fw fa fa-check"></i>
                                <strong>
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></strong>
                            </div>

                        </div>
                    </div>
                    <span class="alert-header darker">Import the Subscribers</span>
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" accept=".xls, .xlsx" />
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <br />
                        <div class="loading" style="text-align: center">
                            <img src="image/loader.gif" alt="" />
                        </div>

                    </div>
                    <div>
                        If you do not have the template, please download it from here
                        <a href="Files/SampleUploadContacts.xls" download target="_blank"><i class="fa fa-download"></i></a>

                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnImport" CssClass="btn btn-orange" UseSubmitBehavior="false" runat="server" OnClick="BtnImport_Click" Text="Import" />
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooterPlaceHolder" runat="Server">
</asp:Content>
