<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AchievementElement.ascx.cs" Inherits="CAT_2015.Pages.User.Controls.AchievementElement" %>
<asp:Table ID="Table1" runat="server" CssClass="base_w_border" Width="100%" Height="200">
    <asp:TableRow runat="server">
        <asp:TableCell RowSpan="3" ID="cellImage" CssClass="achievementElement" runat="server">
            <asp:Image ID="imageAchievement" CssClass="achievementElement" runat="server" Width="100" Height="100" />
        </asp:TableCell>
        <asp:TableCell ID="cellName" CssClass="achievementElement" runat="server" Width="400">
            <asp:Label ID="labelName" CssClass="achievementElement" runat="server" Width="100%"></asp:Label>
        </asp:TableCell>
        <asp:TableCell ID="cellPointsValue" RowSpan="3" CssClass="achievementElement" runat="server" Width="200">
            <asp:Label ID="labelPointsValue" CssClass="achievementElement" runat="server" Width="100%"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell ID="cellDescription" CssClass="achievementElementDescription" runat="server" Width="400">
            <asp:Label ID="labelDescription" CssClass="achievementElementDescription" runat="server" Width="100%"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell ID="cellDateAchieved" CssClass="achievementElement" runat="server" Width="400">
            <asp:Label ID="labelDateAchieved" CssClass="achievementElement" runat="server" Width="100%"></asp:Label>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
