<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressBar.ascx.cs" Inherits="CAT_2015.Pages.User.Controls.ProgressBar" %>
<asp:Table ID="tableProgressBar" runat="server" CssClass="base" CellSpacing="0" Width="100%" Height="90%">
    <asp:TableRow Width="100%">
        <asp:TableCell ID="part1" CssClass="progressBarFilled"></asp:TableCell>
        <asp:TableCell ID="part2" CssClass="progressBarEmpty"></asp:TableCell>
    </asp:TableRow>
</asp:Table>