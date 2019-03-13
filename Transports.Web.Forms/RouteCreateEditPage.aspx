<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RouteCreateEditPage.aspx.cs" Inherits="Transports.Web.Forms.RouteCreateEditPage" %>

<asp:Content ID="DriverCreatePage" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label" runat="server" Text=""></asp:Label>
    </div>
    <label>Input length</label>
    <asp:TextBox ID="routeLength" runat="server" Text=""></asp:TextBox><br />
    <label>Input Is Traffic Jam</label>
    <asp:TextBox ID="traffic" runat="server" Text=""></asp:TextBox><br />
    <label>Input Estimated time</label>
    <asp:TextBox ID="estimate" runat="server" Text=""></asp:TextBox><br />
    <label>Select Shift</label>
    <asp:DropDownList ID="dropdown" runat="server" OnSelectedIndexChanged="dropdown_OnSelectedIndexChanged"></asp:DropDownList><br />
    <asp:Button ID="btnCreate" runat="server" class='btn btn-info' Text="Create" OnClick="btnCreate_Click" />
    <asp:Button ID="btnUpdate" runat="server" class='btn btn-info' Text="Update" OnClick="btnUpdate_Click" />
</asp:Content>
