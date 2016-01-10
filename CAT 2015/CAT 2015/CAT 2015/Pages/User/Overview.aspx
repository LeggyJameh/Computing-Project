<%@ Page Title="Computing Achievement Tracker - Overview" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="CAT_2015.Pages.User.Overview" %>

<%@ Register TagPrefix="Controls" TagName="OverviewPanel" Src="~/Pages/User/Controls/OverviewPanel.ascx" %>
<%@ Register TagPrefix="Controls" TagName="AchievementElement" Src="~/Pages/User/Controls/AchievementElement.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:Table ID="overviewTable" runat="server" CssClass="base" Height="100%" Width="100%" CellSpacing="0">
        <asp:TableRow Height="40%">
            <asp:TableCell ID="cellOverviewPanel" Width="50%">
                <Controls:OverviewPanel ID="overviewPanel" runat="server" />
            </asp:TableCell>
            <asp:TableCell RowSpan="3" ID="cellAchievements" Width="50%" Height="100%">
                <asp:Panel ID="panelAchievements" Height="100%" Width="100%" ScrollBars="Vertical" runat="server">
                    <asp:Table ID="tableAchievements" Height="100%" Width="100%" runat="server" CellSpacing="0">
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="overviewPanelImportant">
                Recent Achievements
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="60%">
            <asp:TableCell ID="cellRecentAchievements" Width="50%">
                <asp:Table ID="tableRecentAchievements" Height="100%" Width="100%" runat="server" CellSpacing="0">
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
