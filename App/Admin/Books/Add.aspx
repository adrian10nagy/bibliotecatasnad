<%@ Page EnableEventValidation="false" ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Admin.Books.Add" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="x_panel">
        <div class="x_title">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
            <asp:Label ID="lblBookId" Visible="false" runat="server"></asp:Label>
            <h2>Adaugă o carte  <small>[poate fi modificată după salvare]</small></h2>
            <div class="clearfix"></div>
        </div>

        <div class="x_content form-horizontal form-label-left">
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblBookInternalNr" runat="server" Text="Număr de inventar*" AssociatedControlID="txtBookInternalNr"></asp:Label>
                        <asp:TextBox ID="txtBookInternalNr" runat="server" CssClass="form-control" autocomplete="off" OnTextChanged="txtBookInternalNr_TextChanged" AutoPostBack="true" CausesValidation="false">
                        </asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtBookInternalNr" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkIsbnLookUp">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                            <asp:Label ID="lblBookIsbn" runat="server" Text="ISBN*" AssociatedControlID="txtBookIsbn" CssClass="col-md-2 col-sm-2 col-xs-12"></asp:Label>
                            <asp:LinkButton ID="lnkBookIsbnAddNew" runat="server" CssClass="control-label col-md-10 col-sm-10 col-xs-12" OnClick="lnkBookIsbnAddNew_Click" CausesValidation="false">
                        Mai multe ISBN-uri? Click aici
                            </asp:LinkButton><br />
                            <asp:TextBox ID="txtBookIsbn" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:BulletedList ID="bltBookIsbnSelected" Visible="false" runat="server"></asp:BulletedList>
                            <asp:Button ID="btnBookIsbnAddNew" AutoPostBack="true" CausesValidation="false" Visible="false" Text="Adaugă ISBN-ul" runat="server" CssClass="btn btn-info" OnClick="btnBookIsbnAddNew_Click" />
                            <asp:Button ID="btnBookIsbnRemove" AutoPostBack="true" CausesValidation="false" Visible="false" Text="Șterge ISBN-urile" runat="server" CssClass="btn btn-danger" OnClick="btnBookIsbnRemove_Click" />
                            <asp:Label ID="lblBooksSuggestions" runat="server" Text="" CausesValidation="false"></asp:Label>
                            <asp:LinkButton ID="lnkIsbnLookUp" Text="Caută" runat="server" OnClick="lnkIsbnLookUp_Click" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton ID="lnkIsbnSuggestionsRemove" Text="Șterge sugestii" runat="server" OnClick="lnkIsbnSuggestionsRemove_Click" CausesValidation="false" Visible="false"></asp:LinkButton>
                        </div>

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3" DynamicLayout="true">
                            <ProgressTemplate>
                                <img class="imagheCenter" src="../Content/Images/Preloader.gif">
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                            <div class="x_content">
                                <asp:GridView ID="gvBookSuggestions" HeaderStyle-BackColor="#5FCFBA" HeaderStyle-ForeColor="White"
                                    runat="server" AutoGenerateColumns="false" AutoGenerateSelectButton="true"
                                    OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged"
                                    class="tbBookAddBookSuggestions table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline collapsed">
                                    <Columns>
                                        <asp:BoundField DataField="Title" HeaderText="Titlu" ItemStyle-Width="200" />
                                        <asp:BoundField DataField="Editura" HeaderText="Editura" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="Autor(i)" HeaderText="Autor(i)" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="An Apariție" HeaderText="An Apariție" ItemStyle-Width="50" />
                                        <asp:BoundField DataField="Nr. pagini" HeaderText="Nr. pagini" ItemStyle-Width="50" />
                                        <asp:BoundField DataField="Limbă" HeaderText="Limbă" ItemStyle-Width="100" />
                                        <asp:TemplateField HeaderText="Descriere">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblexp" Text='<%# Eval("DescrierePreview")%>' />
                                                <asp:HiddenField ID="DescriereFull" runat="server" Value='<%# Eval("DescriereFull") %>' />
                                                <asp:HiddenField ID="ImageUrl" runat="server" Value='<%# Eval("ImageUrl") %>' />
                                                <asp:HiddenField ID="PreviewLink" runat="server" Value='<%# Eval("PreviewLink") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                            <asp:Label ID="lblBookTitle" runat="server" Text="Titlul*" AssociatedControlID="txtBookTitle"></asp:Label>
                            <asp:TextBox ID="txtBookTitle" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookTitle" CssClass="field-validation-error" ErrorMessage="Titlul este obligatoriu." />
                            <asp:LinkButton ID="lnkTitleLookUp" Text="Caută" runat="server" OnClick="lnkTitleLookUp_Click" CausesValidation="false"></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBookIsbnAddNew" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lnkBookIsbnAddNew" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtBookIsbn" EventName="Unload" />
                        <asp:AsyncPostBackTrigger ControlID="lnkIsbnLookUp" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lnkTitleLookUp" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvBookSuggestions" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>


            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookNrPages" runat="server" Text="Număr pagini*" AssociatedControlID="txtBookNrPages"></asp:Label>
                <asp:TextBox ID="txtBookNrPages" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookNrPages" CssClass="field-validation-error" ErrorMessage="Numarul paginilor este obligatoriu" />
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <div>
                    <asp:Label ID="lblBookPublisher" class="control-label col-md-2 col-sm-2 col-xs-12 marginRight40" runat="server" Text="Editura*" AssociatedControlID="drdBookPublisher"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBookPublisherAddNew" Visible="false" runat="server"></asp:TextBox>
                        <asp:Button ID="btnBookPublisherAddNew" AutoPostBack="true" CausesValidation="false" Visible="false" Text="Adaugă noua editură" runat="server" CssClass="btn btn-info" OnClick="btnBookPublisherAddNew_Click" />
                        <div>
                            <asp:DropDownList ID="drdBookPublisher" CssClass="select2_single select2MinWidth350" runat="server">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:LinkButton ID="lnkBookPublisherAddNew" runat="server" CssClass="control-label col-md-2 col-sm-2 col-xs-12 marginTopNegatve15" OnClick="lnkBookPublisherAddNew_Click" CausesValidation="false">
                        Nu există? Click aici
                        </asp:LinkButton><br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkBookPublisherAddNew" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <div>
                    <asp:Label ID="lblBooksAutors" class="control-label col-md-2 col-sm-2 col-xs-12 marginRight40" runat="server" Text="Autori*:" AssociatedControlID="drdBooksAutors"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBookAuthorsAddNew" Visible="false" runat="server"></asp:TextBox>
                        <asp:Button AutoPostBack="true" CausesValidation="false" Visible="false" Text="Adaugă noul autor" ID="btnBookAuthorAddNew" runat="server" CssClass="btn btn-info" OnClick="btnBookAuthorAddNew_Click" />
                        <div>
                            <asp:DropDownList AutoPostBack="true" ID="drdBooksAutors" CssClass="select2_single select2MinWidth350" runat="server" OnSelectedIndexChanged="drdBooksAutors_SelectedIndexChanged">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="lnkBookAuthorsAddNew" runat="server" CssClass="control-label col-md-2 col-sm-2 col-xs-12 marginTopNegatve15" OnClick="lnkBookAuthorsAddNew_Click" CausesValidation="false">
                            Nu există? Click aici
                            </asp:LinkButton><br />
                            <asp:BulletedList ID="bltBooksAuthorsSelected" runat="server" CssClass="paddingLeft160"></asp:BulletedList>
                            <asp:Button AutoPostBack="true" CausesValidation="false" Text="Sterge autorii" ID="btnBookAuthorsRemove" runat="server" CssClass="btn btn-danger" OnClick="btnBookAuthorsRemove_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drdBooksAutors" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="lnkBookAuthorsAddNew" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="clearfix"></div>
            <div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookLanguage" runat="server" Text="Limba" AssociatedControlID="drpBookLanguage"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="drpBookLanguage" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookVolume" runat="server" Text="Volum" AssociatedControlID="txtBookVolume"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtBookVolume" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12 widthMax543" ID="lblBookDomain" runat="server" Text="Domeniul*" AssociatedControlID="drdBookDomain"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="drdBookDomain" CssClass="select2_single select2MinWidth350" runat="server">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookPublishYear" runat="server" Text="Anul publicării" AssociatedControlID="txtBookPublishYear"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtBookPublishYear" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookCondition" runat="server" Text="Starea cărții" AssociatedControlID="drpBookCondition"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="drpBookCondition" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookFormat" runat="server" Text="Formatul cărții" AssociatedControlID="drpBookFormat"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="drpBookFormat" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookSubject" runat="server" Text="Subiectul" AssociatedControlID="txtBookSubject"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtBookSubject" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>

                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookImage" runat="server" Text="Imagine" AssociatedControlID="txtBookImage"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtBookImage" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>

                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="Label1" runat="server" Text="Link de prezentare" AssociatedControlID="txtBookPreviewLink"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="txtBookPreviewLink" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>

                <div class="item form-group">
                    <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookDescription" runat="server" Text="Descriere" AssociatedControlID="txtBookDescription"></asp:Label>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <textarea type="text" rows="10" id="txtBookDescription" runat="server" class="form-control" autocomplete="off" />
                    </div>
                </div>
            </div>
            <div class="ln_solid"></div>
            <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
                <asp:Button ID="btnCancel" runat="server" Text="Anulează" class="btn btn-primary" OnClick="btnCancel_Click" CausesValidation="false" UseSubmitBehavior="false" />
                <asp:Button ID="btnSubmit" runat="server" Text="Salvează" OnClick="btnSubmit_Click" CssClass="btn btn-success" CausesValidation="true" UseSubmitBehavior="true" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainStyle" runat="server">
    <link href="/Content/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/select2.full.min.js")%>'></script>
    <script src='<%= ResolveUrl("~/Scripts/BooksAdd.js") %>'></script>

    <script>
        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }

        function EndRequestHandler(sender, args) {
            $("#<%=drdBooksAutors.ClientID%>").select2({
                placeholder: "Alege un autor",
                allowClear: true
            });

            $("#<%=drdBookPublisher.ClientID%>").select2({
                placeholder: "Alege o editură",
                allowClear: true
            });
        }

        $(document).ready(function () {
            autoHideStatusLabel('<%=lblStatus.ClientID%>', 10000);

            $("#<%=drdBookPublisher.ClientID%>").select2({
                placeholder: "Alege o editură",
                allowClear: true
            });

            $("#<%=drdBookDomain.ClientID%>").select2({
                placeholder: "Alege un domeniu",
                allowClear: true
            });

            $("#<%=drdBooksAutors.ClientID%>").select2({
                placeholder: "Alege un autor",
                allowClear: true
            });

        });

        $(window).load(function () {
            load();
        });

    </script>

</asp:Content>
