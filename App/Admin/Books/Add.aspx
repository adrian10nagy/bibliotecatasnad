<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Admin.Books.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
            <h2>Adaugă o carte  <small>[poate fii modificată după salvare]</small></h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content form-horizontal form-label-left">
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookTitle" runat="server" Text="Titlul*" AssociatedControlID="txtBookTitle"></asp:Label>
                <asp:TextBox ID="txtBookTitle" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookTitle" CssClass="field-validation-error" ErrorMessage="Titlul este obligatoriu." />
            </div>
             <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookPublisher" runat="server" Text="Editura*" AssociatedControlID="txtBookPublisher"></asp:Label>
                <input type="text" id="txtBookPublisher" class="form-control" runat="server" clientidmode="Static" autocomplete="off" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookPublisher" CssClass="field-validation-error" ErrorMessage="Titlul este obligatoriu." />
                <div id="divBookPublishersSuggestion">
                </div>
            </div>
             <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookAuthors" runat="server" Text="Autori*" AssociatedControlID="txtBookAuthorsSearch"></asp:Label>
                <asp:Label ID="lblBookAuthorsStatus" runat="server" Text=""></asp:Label>
                <asp:BulletedList ID="bltBookAuthorsSelected" runat="server" ClientIDMode="Static">
                </asp:BulletedList>
                <div class="bookAuthorsSearchMain">
                    <input type="text" id="txtBookAuthorsSearch" class="form-control" runat="server" clientidmode="Static" autocomplete="off" />
                    <asp:Button ID="btnBookauthorAddSelected" runat="server" Text="Adaugă autor" OnClick="btnBookauthorAddSelected_Click" CausesValidation="false" />
                    <div id="divBookAuthorsSuggestion">
                    </div>
                </div>
                <asp:Button ID="btnBookAuthorsAdd" runat="server" Text="Adaugă mai mulți autori +" OnClick="btnBookAuthorsAdd_Click" CausesValidation="false" />
            </div>
             <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookInternalNr" runat="server" Text="Nr Intern*" AssociatedControlID="txtBookInternalNr"></asp:Label>
                <asp:TextBox ID="txtBookInternalNr" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookInternalNr" CssClass="field-validation-error" ErrorMessage="Numarul intern este obligatoriu." />
            </div>
             <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookIsbn" runat="server" Text="ISBN*" AssociatedControlID="txtBookIsbn"></asp:Label>
                <asp:TextBox ID="txtBookIsbn" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookIsbn" CssClass="field-validation-error" ErrorMessage="ISBN-ul este obligatoriu." />
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <asp:Label ID="lblBookNrPages" runat="server" Text="Număr pagini*" AssociatedControlID="txtBookNrPages"></asp:Label>
                <asp:TextBox ID="txtBookNrPages" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookNrPages" CssClass="field-validation-error" ErrorMessage="Numarul paginilor este obliogatorie" />
            </div>
             <div class="item form-group">
                <asp:Label ID="lblBookLanguage" runat="server" Text="Limba" AssociatedControlID="drpBookLanguage"></asp:Label>
                <div class="col-md-6 col-sm-6 col-xs-12">                
                     <asp:DropDownList ID="drpBookLanguage" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div>
            <div class="item form-group">
                <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookVolume" runat="server" Text="Volum" AssociatedControlID="txtBookVolume"></asp:Label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="txtBookVolume" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="item form-group">
                <asp:Label class="control-label col-md-2 col-sm-2 col-xs-12" ID="lblBookDomain" runat="server" Text="Domeniul" AssociatedControlID="txtBookDomain"></asp:Label>
                <div class="col-md-6 col-sm-6 col-xs-12">                
                    <input type="text" id="txtBookDomain" class="form-control" runat="server" clientidmode="Static" autocomplete="off" />
                    <div id="divBookDomainSuggestion">
                    </div>
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
                    <input type="text" id="txtBookSubject" runat="server" class="form-control" autocomplete="off" />
                </div>
            </div>
                </div>                
            <div class="ln_solid"></div>
            <div>
                <asp:Button ID="btnSubmit" runat="server" Text="Salvează" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnCancel" runat="server" Text="Anulează" class="btn btn-primary" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainStyle" runat="server">
    <style>
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainScript" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/BooksAdd.js") %>'></script>
    <script>
        window.onload = autoHideStatusLabel('<%=lblStatus.ClientID%>', 10000);

        $(document).ready(function () {
            $('#txtBookAuthorsSearch').keyup(function (e) {
                AsyncAddAuthorSuggestions(document.getElementById('txtBookAuthorsSearch').value);
            });

            $('#txtBookPublisher').keyup(function (e) {
                AsyncAddPublisherSuggestions(document.getElementById('txtBookPublisher').value);
            });

            $('#txtBookDomain').keyup(function (e) {
                AsyncAddDomainSuggestions(document.getElementById('txtBookDomain').value);
            });

            $('.searchMainSuggestions').mouseleave(function () {
                $(this).hide();
            });
        });

        function BookAuthorAddtoSuggestions(inputId, inputValue) {
            $('#txtBookAuthorsSearch').val(inputValue);
            $('.searchMainSuggestions').hide();
        }

        function BookPublisherAddToSuggestions(inputId, inputValue) {
            $('#txtBookPublisher').val(inputValue);
            $('.searchMainSuggestions').hide();
        }

        function BookDomainAddToSuggestions(inputId, inputValue) {
            $('#txtBookDomain').val(inputValue);
            $('.searchMainSuggestions').hide();
        }

    </script>
</asp:Content>
