<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Admin.Users.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainStyle" runat="server">
        <link href="/Content/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="/Content/responsive.bootstrap.min.css"" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                  <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Responsive example <small>Users</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <ul class="dropdown-menu" role="menu">
                          <li><a href="#">Settings 1</a>
                          </li>
                          <li><a href="#">Settings 2</a>
                          </li>
                        </ul>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <p class="text-muted font-13 m-b-30">
                      Puteți căuta după orice criteriu
                    </p>
                    <asp:Table ID="datatableResponsive" runat="server" class="table table-striped table-bordered dt-responsive nowrap" cellspacing="0" width="100%"   ClientIDMode="Static">
                        <asp:TableHeaderRow TableSection="TableHeader">
                            <asp:TableHeaderCell>#</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Prenume</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Nume</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Username</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Adresa de acasă</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Data nașterii</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Telefon</asp:TableHeaderCell>
                            <asp:TableHeaderCell>E-mail</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Facebook</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Sex</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Localitate</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                  </div>
                </div>
              </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainScript" runat="server">
        <script src='<%= ResolveUrl("~/Scripts/Datatables/jquery.dataTables.min.js")%>'></script>
        <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.bootstrap.min.js")%>'></script>
        <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.responsive.min.js")%>'></script>
        <script src='<%= ResolveUrl("~/Scripts/Datatables/responsive.bootstrap.js")%>'></script>

    <script>
        $('#datatableResponsive').DataTable();
    </script>

</asp:Content>
