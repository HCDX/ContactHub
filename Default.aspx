<%@ Page Title="" Language="C#" MasterPageFile="~/immain.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHeadPlaceHolder" runat="server">
    <title>Default </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeaderTitlePlaceHolder" runat="server">
    <div class="header-title">
        <h1>ContactHub in Lebanon
        </h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyPlaceHolder" runat="server">

    <div class="row">
        <div class="col-lg-12 col-sm-12 col-xs-12">
            <div class="row">
                <div class="col-lg-12 col-sm-12 col-xs-12">
                    <h3><b><%= _userFullName %>  </b>, Welcome to your session</h3>
                    <h6>Today  <%= StrToday %>  </h6>

                    <div class="well bordered-left bordered-themeprimary" style="opacity: 0.9; margin-top: 25px">
                        <p>
                            <b>HAB</b> ContactHub is designed to be a support in the management of the  
                                                address used for the Humanitarian activies and action in Lebnon. 
                        </p>
                        <div runat="server" id="no_data">
                            <asp:Label ID="NoDatalbl" class="h3" runat="server"></asp:Label>
                        </div>
                        <asp:GridView
                            ID="EntGridView"
                            runat="server"
                            AutoGenerateColumns="False"
                            OnRowCommand="EntGridView_RowCommand"
                            AllowPaging="true"
                            EmptyDataText="No Contacts for your Sector and Area."
                            OnRowDataBound="EntGridView_RowDataBound"
                            DataKeysName="FocalPointsID"
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
                                        <asp:HiddenField ID="FocalPointsID" Value='<%# Bind("FocalPointsID") %>' runat="server" />
                                        <asp:HiddenField ID="FocalPointsSector" Value='<%# Bind("FocalPointsSector") %>' runat="server" />
                                        <asp:HiddenField ID="FocalPointsArea" Value='<%# Bind("FocalPointsArea") %>' runat="server" />
                                        <asp:HiddenField ID="FocalPointsUser" Value='<%# Bind("FocalPointsUser") %>' runat="server" />
                                        <asp:Label ID="SectorInterestName" runat="server" Text='<%# Bind("SectorInterestName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Area of Interest">
                                    <ItemTemplate>
                                        <asp:Label ID="AreaInterestName" runat="server" Text='<%# Bind("AreaInterestName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Total Contacts">
                                    <ItemTemplate>
                                        <asp:Label ID="Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="Email Notification">
                                   
                                    <ItemTemplate>
                                        <div class="checkbox" runat="server">
                                            <label>
                                                <asp:CheckBox ID="Chk" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" Checked='<%# Bind("FocalPointsNotification") %>'></asp:CheckBox>
                                                <span class="text"></span>
                                            </label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Contacts" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <div class="row-command-preview" style="text-align: center">
                                            <asp:LinkButton ID="BtnView" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-default btn-xs purple" CommandName="view" Text="Update"><i class="fa fa-edit"></i>  View</asp:LinkButton>
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

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="homeback"></div>


</asp:Content>

