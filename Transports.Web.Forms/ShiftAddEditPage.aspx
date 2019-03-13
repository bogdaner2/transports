<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ShiftAddEditPage.aspx.cs" Inherits="Transports.Web.Forms.ShiftAddEditPage" %>

<asp:Content ID="DriverCreatePage" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label" runat="server" Text=""></asp:Label>
    </div>
    <label>Select start date</label>
    <asp:Calendar ID = "Calendar1" runat = "server" OnSelectionChanged="Calendar1_OnSelectionChanged"></asp:Calendar>
    <label>Select end date</label>
    <asp:Calendar ID = "Calendar2" runat = "server" OnSelectionChanged="Calendar2_OnSelectionChanged"></asp:Calendar>
    <asp:Button ID="btnCreate" runat="server" class='btn btn-info' Text="Create" OnClick="btnCreate_Click" />
    <asp:Button ID="btnUpdate" runat="server" class='btn btn-info' Text="Update" OnClick="btnUpdate_Click" />
</asp:Content>
