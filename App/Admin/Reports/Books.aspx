<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Admin.Reports.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainStyle" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-4 col-sm-4 col-xs-12">
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
                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5">
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


        <%-- Grad acoperire --%>
        <div class="col-md-4 col-sm-4 col-xs-12">
            <div class="x_panel tile fixed_height_320">
                <div class="x_title">
                    <h2>Cărți adăugate din totalul estimat</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="dashboard-widget-content">
                        <ul class="quick-list">
                            <li><i class="fa fa-line-chart"></i><a href="#">Evoluție</a></li>
                        </ul>

                        <div class="sidebar-widget">
                            <h4>Grad de acoperire</h4>
                            <canvas width="150" height="80" id="foo2" class="" style="width: 160px; height: 100px;"></canvas>
                            <div class="goal-wrapper">
                                <span class="gauge-value pull-left">$</span>
                                <span id="gauge-text2" class="gauge-value pull-left">0</span>
                                <span id="goal-text2" class="goal-value pull-right">50,000</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- Grad acoperire --%>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/dashboard.js") %>'></script>

    <!-- gauge.js -->
    <script>

        var opts2 = {
            lines: 12,
            angle: 0,
            lineWidth: 0.4,
            pointer: {
                length: 0.75,
                strokeWidth: 0.042,
                color: '#1D212A'
            },
            limitMax: 'false',
            colorStart: '#1ABC9C',
            colorStop: '#1ABC9C',
            strokeColor: '#F0F3F3',
            generateGradient: true
        };
        var target = document.getElementById('foo2'),
            gauge2 = new Gauge(target).setOptions(opts2);

        gauge2.maxValue = 50000;
        gauge2.animationSpeed = 32;
        gauge2.set(453);
        gauge2.setTextField(document.getElementById("gauge-text2"));

    </script>
    <!-- /gauge.js -->
</asp:Content>
