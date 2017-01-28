<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Admin.Books.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id='TextBoxesGroup'>
        <div id="TextBoxDiv1">
            <label>Textbox #1 : </label>
            <input type='textbox' id='textbox1'>
        </div>
    </div>
    <input type='button' value='Add Button' id='addButton'>
    <input type='button' value='Remove Button' id='removeButton'>
    <input type='button' value='Get TextBox Value' id='getButtonValue'>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainStyle" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainScript" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            var counter = 2;

            $("#addButton").click(function () {

                var newTextBoxDiv = $(document.createElement('div'))
                     .attr("id", 'TextBoxDiv' + counter);

                newTextBoxDiv.after().html('<label>Textbox #' + counter + ' : </label>' +
                      '<input type="text" name="textbox' + counter +
                      '" id="textbox' + counter + '" value="" >');

                newTextBoxDiv.appendTo("#TextBoxesGroup");


                counter++;
            });

            $("#removeButton").click(function () {
                if (counter == 1) {
                    alert("No more textbox to remove");
                    return false;
                }

                counter--;

                $("#TextBoxDiv" + counter).remove();

            });

            $("#getButtonValue").click(function () {

                var msg = '';
                for (i = 1; i < counter; i++) {
                    msg += "\n Textbox #" + i + " : " + $('#textbox' + i).val();
                }
                alert(msg);
            });
        });

    </script>
</asp:Content>
