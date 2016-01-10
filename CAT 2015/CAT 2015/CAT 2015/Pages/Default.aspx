<%@ Page Title="Computing Achievement Tracker" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CAT_2015.Pages.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:Table ID="table1" runat="server" CellSpacing="0" CssClass="base">
        <asp:TableRow CssClass="base">
            <asp:TableCell CssClass="subHeading">
                What is this all about?
            </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow CssClass="base">
            <asp:TableCell>
                Throughout the 2 year course you will have the opportunity to earn complete tasks which will reward achievement points.
                Most achievements are worth 5 points, however some of the harder ones are worth 10, 15 or even 20+, so it is worth checking out the full list on the achievements page. 
                These achivement points will build up throughout the year and allow you to "Level Up" earning kudos and even rewards along the way.
                What is the point?  A sense of fun and challenge?  Can you be the first student in the class to get the "PC Master Race" achievement?
                Will you be beaten to Level 25 by your friends?
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
