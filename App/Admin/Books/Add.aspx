<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Admin.Books.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formCreateArea">
        <h1>Adaugă o carte </h1>
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
        <div>
            <asp:Label ID="lblBookTitle" runat="server" Text="Titlul(*)" AssociatedControlID="txtBookTitle"></asp:Label>
            <asp:TextBox ID="txtBookTitle" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookTitle" CssClass="field-validation-error" ErrorMessage="Titlul este obligatoriu." />
        </div>

        <div>
            <asp:Label ID="lblBookAuthors" runat="server" Text="Autori(*)"></asp:Label>
            <asp:Label ID="lblBookAuthorsStatus" runat="server" Text=""></asp:Label>
            <asp:BulletedList ID="bltBookAuthorsSelected" runat="server"  ClientIDMode="Static">
            </asp:BulletedList>
            <div class="bookAuthorsSearchMain">
                <input type="text" id="txtBookAuthorsSearch" class="form-control" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnBookauthorAddSelected" runat="server" Text="Adaugă autor" OnClick="btnBookauthorAddSelected_Click" CausesValidation="false"/>
                <div id="divBookAuthorsSuggestion">
                </div>
            </div>
                <asp:Button ID="btnBookAuthorsAdd" runat="server" Text="Adaugă mai mulți autori +" OnClick="btnBookAuthorsAdd_Click" CausesValidation="false"/>
        </div>
        <div>
            <div>
                <asp:Label ID="lblBookPublisher" runat="server" Text="Editura(*)"></asp:Label>
                <input type="text" ID="txtBookPublisher" class="form-control" runat="server"  ClientIDMode="Static" />
                <div id="divBookPublishersSuggestion">
                </div>
            </div>
            <div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookInternalNr" runat="server" Text="Nr Intern(*)" AssociatedControlID="txtBookInternalNr"></asp:Label>
                    <asp:TextBox ID="txtBookInternalNr" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookInternalNr" CssClass="field-validation-error" ErrorMessage="Numarul intern este obligatoriu." />
                </div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookIsbn" runat="server" Text="ISBN(*)" AssociatedControlID="txtBookIsbn"></asp:Label>
                    <asp:TextBox ID="txtBookIsbn" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookIsbn" CssClass="field-validation-error" ErrorMessage="ISBN-ul este obligatoriu." />
                </div>
            </div>
            <div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookNrPages" runat="server" Text="Număr pagini(*)" AssociatedControlID="txtBookNrPages"></asp:Label>
                    <asp:TextBox ID="txtBookNrPages" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookNrPages" CssClass="field-validation-error" ErrorMessage="Numarul paginilor este obliogatorie" />
                </div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookLanguage" runat="server" Text="Limba(*)" AssociatedControlID="drpBookLanguage"></asp:Label>
                    <asp:DropDownList ID="drpBookLanguage" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookDomain" runat="server" Text="Domeniul" ></asp:Label>
                    <input type="text" id="txtBookDomain" class="form-control" runat="server" ClientIDMode="Static"/>
                    <div id="divBookDomainSuggestion">
                    </div>
                </div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookPublishYear" runat="server" Text="Anul publicării" AssociatedControlID="txtBookPublishYear"></asp:Label>
                    <asp:TextBox ID="txtBookPublishYear" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookPublishYear" CssClass="field-validation-error" ErrorMessage="Anul publicării este obligatoriu." />
                </div>
            </div>
            <div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookVolume" runat="server" Text="Volum" AssociatedControlID="txtBookVolume"></asp:Label>
                    <asp:TextBox ID="txtBookVolume" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookCondition" runat="server" Text="Starea cărții" AssociatedControlID="drpBookCondition"></asp:Label>
                    <asp:DropDownList ID="drpBookCondition" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="formCreateTwoColumns">
                    <asp:Label ID="lblBookFormat" runat="server" Text="Formatul cărții" AssociatedControlID="drpBookFormat"></asp:Label>
                    <asp:DropDownList ID="drpBookFormat" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="formCreateTwoColumns">
                <asp:Label ID="lblBookSubject" runat="server" Text="Subiectul" AssociatedControlID="txtBookSubject"></asp:Label>
                <input type="text" ID="txtBookSubject" runat="server" Class="form-control" />
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Salvează" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
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
