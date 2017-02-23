<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Admin.Books.Manage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainStyle" runat="server">
    <link href="/Content/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/responsive.bootstrap.min.css"" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="preLoadIcon"></div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="x_panel">
            <div class="x_title">
                <h2>Cărțile bibliotecii</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <asp:Table ID="datatableResponsive" runat="server" class="table table-striped table-bordered dt-responsive nowrap" CellSpacing="0" Width="100%" ClientIDMode="Static">
                    <asp:TableHeaderRow TableSection="TableHeader">
                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Disponibilitate</asp:TableHeaderCell>
                        <asp:TableHeaderCell>ISBN</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Număr de inventar</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Title</asp:TableHeaderCell>
                        <asp:TableHeaderCell>An Apariție</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Autor(i)</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nr. pagini</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Editura</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Volum</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Condiție</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Format</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Limbă</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/Datatables/jquery.dataTables.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.bootstrap.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.responsive.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/responsive.bootstrap.js")%>'></script>

    <script>

        $(window).load(function () {
            // Animate loader off screen
            $(".preLoadIcon").fadeOut("slow");
        });

        $('#datatableResponsive').DataTable();
    </script>

</asp:Content>
