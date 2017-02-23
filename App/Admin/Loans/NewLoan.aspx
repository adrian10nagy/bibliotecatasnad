<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewLoan.aspx.cs" Inherits="Admin.Loans.NewLoan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainStyle" runat="server">
    <link href="/Content/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Adăugați un nou împrumut</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content form-horizontal form-label-left">
                <br />
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorSummaryMessge"></asp:Label>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" runat="server" Text="Alege utilizator:" AssociatedControlID="drdLoanUserAll"></asp:Label>
                    <asp:Label runat="server"> </asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="drdLoanUserAll" CssClass="select2_single" runat="server">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>    
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" runat="server" Text="Alege cărți:" AssociatedControlID="drdLoanBooksAll"></asp:Label>
                    
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate> 
                            <div >
                                <asp:DropDownList AutoPostBack="true" ID="drdLoanBooksAll" CssClass="select2_single" runat="server" OnSelectedIndexChanged="drdLoanBooksAll_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:BulletedList ID="bltLoanBooksSelected" runat="server"></asp:BulletedList>
                                <asp:Button AutoPostBack="true" Text="Sterge selecțiile" ID="btnLoanBookRemove" runat="server" CssClass="btn btn-danger" OnClick="btnLoanBookRemove_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drdLoanBooksAll" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="ln_solid"></div>
                <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
                    <asp:Button ID="btnCancel" runat="server" Text="Anulează" class="btn btn-primary" OnClick="btnCancel_Click" CausesValidation="false" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Salvează împrumutul" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
     </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/select2.full.min.js")%>'></script>
    <script>
        document.body.onload = load;

        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }

        function EndRequestHandler(sender, args) {
            $("#<%=drdLoanBooksAll.ClientID%>").select2({
                placeholder: "Alege o carte",
                allowClear: true
            });
        }

        $(document).ready(function () {

            $("#<%=drdLoanBooksAll.ClientID%>").select2({
                placeholder: "Alege o carte",
                allowClear: true
            });

            $("#<%=drdLoanUserAll.ClientID%>").select2({
                placeholder: "Alege utilizatorul",
                allowClear: true
            });

        });
    </script>
</asp:Content>
