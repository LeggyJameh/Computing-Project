<%@ Page Title="Computing Achievement Tracker - Overview" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="CAT_2015.Pages.User.Overview" %>
<%@ Register TagPrefix="Controls" TagName="OverviewPanel" Src="~/Pages/User/Controls/OverviewPanel.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:Table ID="overviewTable" runat="server" CssClass="base" Height="100%" Width="100%" CellSpacing="0">
        <asp:TableRow Height="40%">
            <asp:TableCell Width="50%"><Controls:OverviewPanel ID="overviewPanel" runat="server"/></asp:TableCell>
            <asp:TableCell RowSpan="2" Width="50%"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="60%">
            <asp:TableCell Width="50%"></asp:TableCell>
            <asp:TableCell Width="50%"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
