<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Achievements.aspx.cs" Inherits="CAT_2015.Pages.User.Achievements" %>

<%@ Register TagPrefix="Controls" TagName="AchievementElement" Src="~/Pages/User/Controls/AchievementElement.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:Table ID="tableAchievements" CssClass="base" Height="100%" Width="100%" CellSpacing="0" runat="server">
    </asp:Table>
</asp:Content>
