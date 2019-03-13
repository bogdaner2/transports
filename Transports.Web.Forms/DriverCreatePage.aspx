<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DriverCreatePage.aspx.cs" Inherits="Transports.Web.Forms.DriverCreatePage" %>

<asp:Content ID="DriverCreatePage" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            Create new driver
        </div>
                <label>Input name</label>
                <asp:TextBox ID="driverName" runat="server" Text=""></asp:TextBox><br />
                <label>Input age</label>
                <asp:TextBox ID="driverAge" runat="server" Text=""></asp:TextBox><br />
                <label>Input rang</label>
                <asp:TextBox ID="driverRang" runat="server" Text=""></asp:TextBox><br />
                <asp:Button ID="btnCreate" runat="server" class='btn btn-info' Text="Create" OnClick="btnCreate_Click" />
                <asp:Button ID="btnUpdate" runat="server" class='btn btn-info' Text="Update" OnClick="btnUpdate_Click" />
</asp:Content>
