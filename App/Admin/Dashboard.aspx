<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Admin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Cărți pe edituri--%>
    <!-- top tiles -->
    <div class="row tile_count" runat="server">
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Total Utilizatori</span>
            <asp:Label ID="txtTotalUsers" class="count" runat="server"></asp:Label>
            <%--<span class="count_bottom"><i class="green">4% </i>From last Week</span>--%>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-book"></i>Total cărți</span>
            <asp:Label ID="txtTotalBooks" class="count" runat="server"></asp:Label>
            <%--<span class="count_bottom"><i class="green"><i class="fa fa-sort-asc"></i>3% </i>From last Week</span>--%>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-address-card-o"></i>Total cărți împrumutate</span>
            <asp:Label ID="txtTotalLoans" class="count" runat="server"></asp:Label>
            <%--            <span class="count_bottom"><i class="green"><i class="fa fa-sort-asc"></i>34% </i>From last Week</span>--%>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-address-card-o"></i>Cărți împrumutate azi</span>
            <asp:Label ID="txtTotalLoansToday" class="count" runat="server"></asp:Label>
            <%--            <span class="count_bottom"><i class="red"><i class="fa fa-sort-desc"></i>12% </i>From last Week</span>--%>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-book"></i>Cărți adăugate azi</span>
            <asp:Label ID="txtTotalBooksToday" class="count" runat="server"></asp:Label>
            <%--            <span class="count_bottom"><i class="red"><i class="fa fa-sort-desc"></i>12% </i>From last Week</span>--%>
        </div>
        <div class="col-md-2 col-sm-4 col-xs-6 tile_stats_count">
            <span class="count_top"><i class="fa fa-user"></i>Utilizatori adăugați azi</span>
            <asp:Label ID="txtTotalUsersToday" class="count" runat="server"></asp:Label>
            <%--            <span class="count_bottom"><i class="red"><i class="fa fa-sort-desc"></i>12% </i>From last Week</span>--%>
        </div>
    </div>
    <!-- /top tiles -->

    <div class="row">

<%--        <div class="col-md-4 col-sm-4 col-xs-12">
            <asp:Panel runat="server" DefaultButton="lnkMainSearch">
                <asp:TextBox ID="txtMainSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:LinkButton ID="lnkMainSearch" Text="Caută" runat="server" OnClick="lnkMainSearch_Click" CausesValidation="false"></asp:LinkButton>
            </asp:Panel>
        </div>--%>

        <div class="col-md-4 col-sm-4 col-xs-12" runat="server" visible="false">
            <div class="x_panel tile fixed_height_320 overflow_hidden">
                <div class="x_title">
                    <h2>Cărți per Edituri</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <table class="" style="width: 100%">
                        <tr>
                            <th style="width: 42%;">
                                <p>Top 5</p>
                            </th>
                            <th>
                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7">
                                    <p class="">Cărți</p>
                                </div>
                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 textAlignRight">
                                    <p class="">Procentaj</p>
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <canvas id="canvas1" height="140" width="140" style="margin: 15px 10px 10px 0"></canvas>
                            </td>
                            <td>
                                <asp:Table ID="tblTopPulishers" runat="server" CssClass="tile_info">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <%-- Cărți pe edituri--%>
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/dashboard.js") %>'></script>
</asp:Content>


