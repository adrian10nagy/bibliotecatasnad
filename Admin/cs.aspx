<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cs.aspx.cs" Inherits="Admin.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainStyle" runat="server">
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            Enter search term:
            <asp:TextBox ID="txtSearch" runat="server" />
            <asp:Button Text="Submit" runat="server" OnClick = "Submit" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainScript" runat="server">


     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
 <script type="text/javascript">
      //On Page Load.
      $(function () {
          SetAutoComplete();
      });

      //On UpdatePanel Refresh.
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      if (prm != null) {
          prm.add_endRequest(function (sender, e) {
              if (sender._postBackSettings.panelsToUpdate != null) {
                  SetAutoComplete();
              }
          });
      };
      function SetAutoComplete() {
          $("[id$=txtSearch]").autocomplete({
              source: function (request, response) {
                  $.ajax({
                      url: 'CS.aspx/GetFruits',
                      data: "{ 'prefix': '" + request.term + "'}",
                      dataType: "json",
                      type: "POST",
                      contentType: "application/json; charset=utf-8",
                      success: function (data) {
                          response($.map(data.d, function (item) {
                              return {
                                  label: item.split('-')[0],
                                  val: item.split('-')[1]
                              };
                          }))
                      }
                  });
              }
          });
      }
    </script>

</asp:Content>
