<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DriversPage.aspx.cs" Inherits="Transports.Web.Forms.DriversPage" %>
<%@ Import Namespace="Transports.Core.Models.SQL" %>

<asp:Content ID="DriverPage" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Drivers list </h1>
        <a href='newdriver' class='btn btn-warning' role='button'>Create</a>
        <hr>
        <asp:Repeater ID="Repeater" runat="server">
            <ItemTemplate>
                <div>
                    <h5>Name: <%#Eval("Name") %></h5>
                    <h6>Age:  <%#Eval("Age") %></h6>
                    <h6>Rang:  <%#Eval("Rang") %></h6>
                </div>
                <asp:Button ID="btnUpdate" runat="server" class='btn btn-info' Text="Update"></asp:Button>
                <asp:Button ID="btnDelete" runat="server" class='btn btn-info' Text="Delete"></asp:Button>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
