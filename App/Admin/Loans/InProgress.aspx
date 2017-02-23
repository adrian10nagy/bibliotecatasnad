<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InProgress.aspx.cs" Inherits="Admin.Loans.InProgress" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainStyle" runat="server">
    <link href="/Content/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/responsive.bootstrap.min.css"" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12 col-sm-12 col-xs-12">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="x_panel">
            <div class="x_title">
                <h2 runat="server" id="lblPageTitle">Împrumuturi curente</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <asp:Table ID="datatableResponsive" runat="server" class="table table-striped table-bordered dt-responsive nowrap" CellSpacing="0" Width="100%" ClientIDMode="Static">
                    <asp:TableHeaderRow TableSection="TableHeader">
                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Carte</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Utilizator</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Dată împrumut</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>

            <asp:Label ID="lblLoanReturn" runat="server">
                <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
                    <asp:Label ID="lblLoanId" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnLoanCancel" Visible="false" runat="server" Text="Du-mă înapoi" class="btn btn-primary" OnClick="btnLoanReturnCancel_Click" CausesValidation="false" />
                    <asp:Button ID="btnLoanRemove" Visible="false" runat="server" Text="Anulează împrumut" OnClick="btnLoanRemove_Click" CssClass="btn btn-danger" />
                    <asp:Button ID="btnReturnBook" Visible="false" runat="server" Text="Returnează cartea" OnClick="btnReturnBook_Click" CssClass="btn btn-success" />
                 </div>
            </asp:Label>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/Datatables/jquery.dataTables.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.bootstrap.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/dataTables.responsive.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/Datatables/responsive.bootstrap.js")%>'></script>

    <script>
        $('#datatableResponsive').DataTable();
    </script>

</asp:Content>

