<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNew.aspx.cs" Inherits="ITG_Tsk2.LoginNew" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Main2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
<div class="simple-login-container">
    <div class="Main text-center">
        <h2>Login</h2>
        <div class="row justify-content-center align-items-center">
            <div class="col-md-2 form-group position-relative">
                <label for="UsernameTextBox" class="inside-label">Username*</label>
                <asp:TextBox ID="UsernameTextBox" CssClass="form-control" runat="server" placeholder=" UserName" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UsernameTextBox" CssClass="error-massage-login" runat="server" ErrorMessage="Username*"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row justify-content-center align-items-center">
            <div class="col-md-2 form-group position-relative">
                <label for="PasswordTextBox" class="inside-label">Password*</label>
                <asp:TextBox ID="PasswordTextBox" CssClass="form-control" runat="server" TextMode="Password" placeholder="Password " />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="PasswordTextBox" CssClass="error-massage-login" runat="server" ErrorMessage="Password*"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row justify-content-center">
            <div class="col-md-6 form-group">
                <asp:Button ID="SubmitButton" CssClass="btn btn-success" runat="server" Text="Login" OnClick="SubmitButton_Click" />
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Label Visible="false" ID="lblInvalid" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</div>


</asp:Content>
