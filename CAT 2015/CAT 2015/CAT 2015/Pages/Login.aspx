<%@ Page Title="Computing Achievement Tracker - Login" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CAT_2015.Pages.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:Table Width="100%" HorizontalAlign="Center" runat="server" CssClass="base" CellSpacing="6">
        <asp:TableRow CssClass="base">
            <asp:TableCell CssClass="base">
            Username:
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="base">
            <asp:TableCell>
                <asp:TextBox ID="textBoxUsername" runat="server" CssClass="textbox"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow cssClass="base">
            <asp:TableCell CssClass="base">
            Password:
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="base">
            <asp:TableCell>
                <asp:TextBox ID="textBoxPassword" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="base">
            <asp:TableCell>
                <asp:Button ID="buttonLogin" runat="server" CssClass="button" Text="Login"></asp:Button>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="labelError" runat="server" CssClass="base"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
