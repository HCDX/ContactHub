<%@ Page Title="" Language="C#" MasterPageFile="~/immain.Master" AutoEventWireup="true" CodeFile="SystemPageAccess.aspx.cs" Inherits="SystemPageAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeadPlaceHolder" runat="server">
    <title>System Page Access </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderTitlePlaceHolder" runat="server">
    <div class="header-title">
        <h1>Management of Page Access
        </h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyPlaceHolder" runat="server">


    <%-- <asp:UpdatePanel ID="upCrudGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

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
            <div class="widget">
                <div class="widget-header ">
                    <span class="widget-caption this-search-widget-caption"></span>
                </div>

                <div class="widget-body">
                    <div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group ">
                                    <label for="ddlProfile">List of Profiles</label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-users" style="color: #3c8dbc"></i>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlProfile" class="form-control select2"
                                            data-allow-clear="true"
                                            data-placeholder="Select a Focal Point"
                                            data-bv-message="This value is not valid"
                                            data-bv-notempty="true"
                                            data-bv-notempty-message="This is a mandatory field and can not be empty"
                                            data-bv-regexp="true"
                                            OnSelectedIndexChanged="ddlProfile_Click"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group ">
                                    <label for="ddlModule">List of Modules</label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-sliders" style="color: #3c8dbc"></i>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlModule" class="form-control select2"
                                            data-allow-clear="true"
                                            data-placeholder="Select a Focal Point"
                                            data-bv-message="This value is not valid"
                                            data-bv-notempty="true"
                                            data-bv-notempty-message="This is a mandatory field and can not be empty"
                                            data-bv-regexp="true"
                                            OnSelectedIndexChanged="ddlProfile_Click"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                        </div>
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


                    </div>
                </div>


            </div>
        </div>
    </div>

    <div class="row">

        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="form-group " style="float: right">

                <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-success"> <i class="fa fa-save"></i> Save </asp:LinkButton>

            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="well with-header with-footer">

                <div class="header bordered-blueberry this-search-widget-caption">
                    <div style="float: left">List of Pages</div>
                    <div style="float: right">Total : <span style="color: red"><%=l%> </span>Pages </div>
                </div>
                <div runat="server" id="no_data">
                    <asp:Label ID="NoDatalbl" class="h3" runat="server"></asp:Label>
                </div>
                <asp:GridView
                    ID="EntGridView"
                    runat="server"
                    AutoGenerateColumns="False"
                    AllowPaging="true"
                    DataKeysName="ModulePageAccessID,ModulesPageID"
                    PageSize="10"
                    Style="font-size: 14px"
                    OnPageIndexChanging="EntGridView_PageIndexChanging"
                    CssClass="table table-striped table-bordered table-hover  ">

                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Module">
                            <ItemTemplate>
                                <asp:Label ID="ModuleName" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Page Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="ModulePageAccessID" Value='<%# Bind("ModulePageAccessID") %>' runat="server" />
                                <asp:HiddenField ID="ModulesPageID" Value='<%# Bind("ModulesPageID") %>' runat="server" />
                                <asp:Label ID="PageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Page URL">
                            <ItemTemplate>
                                <asp:Label ID="PageUrl" runat="server" Text='<%# Bind("PageUrl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="PageDescription" runat="server" Text='<%# Bind("PageDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="Access">
                            <HeaderTemplate>
                                <div class="checkbox" runat="server">
                                    <label>
                                        <asp:CheckBox runat="server" ID="chkAll"></asp:CheckBox>
                                        <span class="text"></span>
                                    </label>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="checkbox" runat="server">
                                    <label>
                                        <asp:CheckBox ID="Chk" runat="server" Checked='<%# Bind("Access") %>'></asp:CheckBox>
                                        <span class="text"></span>
                                    </label>
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


                <div class="footer">
                    Execution time :
                    <asp:Literal ID="execTimeLit" runat="server"></asp:Literal>
                </div>

            </div>


        </div>

    </div>

    <%-- </ContentTemplate>
        <Triggers></Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooterPlaceHolder" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
           
            $("#<%=EntGridView.ClientID%> input[id*='Chk']:checkbox").click(function () {
                //Get number of checkboxes in list either checked or not checked
                var totalCheckboxes = $("#<%=EntGridView.ClientID%> input[id*='Chk']:checkbox").size();
                //Get number of checked checkboxes in list
                var checkedCheckboxes = $("#<%=EntGridView.ClientID%> input[id*='Chk']:checkbox:checked").size();
                //Check / Uncheck top checkbox if all the checked boxes in list are checked
                $("#<%=EntGridView.ClientID%> input[id*='PageBodyPlaceHolder_EntGridView_chkAll']:checkbox").attr('checked', totalCheckboxes == checkedCheckboxes);
            });

            $("#<%=EntGridView.ClientID%> input[id*='chkAll']:checkbox").click(function () {
                //Check/uncheck all checkboxes in list according to main checkbox 
         
                //$("#<%=EntGridView.ClientID%> input[id*='PageBodyPlaceHolder_EntGridView_Chk']:checkbox").attr('checked', $(this).is(':checked'));
                 $("#<%=EntGridView.ClientID%> input[id*='Chk']:checkbox").prop('checked', $(this).is(':checked'));
             
            });
        });
    </script>

</asp:Content>
