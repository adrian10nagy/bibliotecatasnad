<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Admin.Users.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainStyle" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
            <asp:Label ID="lblUserId" Visible="false" runat="server"></asp:Label>
            <h2>Adaugă utilizator <small>[poate fii modificat după salvare]</small></h2>

            <div class="clearfix"></div>
        </div>
        <div class="x_content form-horizontal form-label-left">
            <br />
            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <input type="text" class="form-control has-feedback-left" id="userName" placeholder="Nume" runat="server" required="required" autocomplete="off" />
                <span class="fa fa-user form-control-feedback left" aria-hidden="true"></span>
                <asp:RequiredFieldValidator ControlToValidate="userName" runat="server" ErrorMessage="Numele este obligatoriu" CssClass="requiredField"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <input type="text" class="form-control has-feedback-left" id="userSurname" placeholder="Prenume" runat="server" required="required" autocomplete="off">
                <span class="fa fa-user form-control-feedback left" aria-hidden="true"></span>
                <asp:RequiredFieldValidator ControlToValidate="userSurname" runat="server" ErrorMessage="Prenumele este obligatoriu" CssClass="requiredField"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <input type="text" class="form-control has-feedback-left" id="userEmail" placeholder="Email (opțional)" runat="server" autocomplete="off">
                <span class="fa fa-envelope form-control-feedback left" aria-hidden="true"></span>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 form-group has-feedback">
                <input type="text" class="form-control has-feedback-left" id="userPhone" placeholder="Telefon (opțional)" runat="server" autocomplete="off">
                <span class="fa fa-phone form-control-feedback left" aria-hidden="true"></span>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12" runat="server" id="lblUserUsername">Nume de utilizator</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <input type="text" class="form-control" id="UserUsername" runat="server" autocomplete="off">
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Data nașterii</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <input id="userBirthday" class="form-control date-picker" required="required" type="text" runat="server" autocomplete="off">
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Oraș</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:DropDownList ID="drpUserCity" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Adresă</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <input type="text" class="form-control" placeholder="Adresă(opțional)" id="userAddress" runat="server" autocomplete="off">
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Sex</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButtonList ID="userGender" runat="server">
                        <asp:ListItem Value="1" Text="Masculin" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feminim"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12" runat="server" id="lblUserType">Tipul membrului</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:DropDownList ID="drpUsertype" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Naționalitate</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:DropDownList ID="drpUserNationality" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="item form-group">
                <label class="control-label col-md-2 col-sm-2 col-xs-12">Facebook</label>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <input type="text" class="form-control" placeholder="(opțional)" id="userFacebook" runat="server" autocomplete="off">
                </div>
            </div>
            <div class="ln_solid"></div>
            <div class="form-group">
                <div class="col-md-9 col-sm-9 col-xs-12 col-md-offset-3">
                    <asp:Button ID="btnSave" runat="server" Text="Salvează" class="btn btn-success" OnClick="btnSave_Click" CausesValidation="true" />
                    <asp:Button ID="btnCancel" runat="server" Text="Anulează" class="btn btn-primary" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainScript" runat="server">
    <script>

        $(window).load(function () {
            autoHideStatusLabel('<%=lblStatus.ClientID%>', 10000)
        });

        $(document).ready(function () {
            $('#<%=userBirthday.ClientID%>').daterangepicker({
                locale: {
                    format: 'DD-MM-YYYY'
                },
                singleDatePicker: true,
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

        });
    </script>
</asp:Content>
