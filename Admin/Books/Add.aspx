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
            <asp:Label ID="lblBookAuthors" runat="server" Text="Autori(*)" AssociatedControlID="drpBookAuthors"></asp:Label>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>


                    <asp:BulletedList ID="bulBookAuthorsItems" runat="server" CssClass="bookAuthorsItems" OnClick="bulBookAuthorsItems_Click" DisplayMode="LinkButton"></asp:BulletedList>
                    <asp:TextBox ID="txtBookAuthorsSearch" runat="server" AutoPostBack="true" CssClass="form-control"
                        OnTextChanged="txtBookAuthorsSearch_TextChanged"
                        onkeyup="RefreshUpdatePanel();"></asp:TextBox>



                    <asp:DropDownList ID="drpBookAuthors" runat="server"
                        CssClass="form-control" Visible="false" AutoPostBack="true"
                        OnSelectedIndexChanged="drpBookAuthors_SelectedIndexChanged" onmouseover="this.size=5;" onmouseout="this.size=1">
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkBookAuthorsAdd" runat="server" OnClick="lnkBookAuthorsAdd_Click" CausesValidation="false">Adaugă autor +</asp:LinkButton>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtBookAuthorsSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:Label ID="lblBookPublisher" runat="server" Text="Editura(*)" AssociatedControlID="txtBookPublisher"></asp:Label>
            <asp:TextBox ID="txtBookPublisher" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="drpBookPublisher" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookPublisher" CssClass="field-validation-error" ErrorMessage="Editura este obligatorie." />
        </div>
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
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookNrPages" runat="server" Text="Număr pagini(*)" AssociatedControlID="txtBookNrPages"></asp:Label>
            <asp:TextBox ID="txtBookNrPages" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookNrPages" CssClass="field-validation-error" ErrorMessage="Numarul paginilor este obliogatorie" />
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookLanguage" runat="server" Text="Limba(*)" AssociatedControlID="drpBookLanguage"></asp:Label>
            <asp:DropDownList ID="drpBookLanguage" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookDomain" runat="server" Text="Domeniul" AssociatedControlID="txtBookDomain"></asp:Label>
            <asp:TextBox ID="txtBookDomain" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="drpBookDomain" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookPublishYear" runat="server" Text="Anul publicării" AssociatedControlID="txtBookPublishYear"></asp:Label>
            <asp:TextBox ID="txtBookPublishYear" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookPublishYear" CssClass="field-validation-error" ErrorMessage="Anul publicării este obligatoriu." />
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookVolume" runat="server" Text="Volum" AssociatedControlID="txtBookVolume"></asp:Label>
            <asp:TextBox ID="txtBookVolume" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookVolume" CssClass="field-validation-error" ErrorMessage="Volumul este obligatoriu." />
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookCondition" runat="server" Text="Starea cărții" AssociatedControlID="drpBookCondition"></asp:Label>
            <asp:DropDownList ID="drpBookCondition" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookFormat" runat="server" Text="Formatul cărții" AssociatedControlID="drpBookFormat"></asp:Label>
            <asp:DropDownList ID="drpBookFormat" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="formCreateTwoColumns">
            <asp:Label ID="lblBookSubject" runat="server" Text="Subiectul" AssociatedControlID="txtBookSubject"></asp:Label>
            <asp:TextBox ID="txtBookSubject" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookSubject" CssClass="field-validation-error" ErrorMessage="Subiectul este obligatoriu." />
        </div>
        <asp:Button ID="btnSubmit" runat="server" Text="Salvează" OnClick="btnSubmit_Click" CssClass="btn btn-main" />
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

        function RefreshUpdatePanel() {
            __doPostBack('<%= txtBookAuthorsSearch.ClientID %>', '');
        };

    </script>
</asp:Content>
