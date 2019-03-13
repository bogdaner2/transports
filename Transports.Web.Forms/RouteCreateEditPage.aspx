<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteCreateEditPage.aspx.cs" Inherits="Transports.Web.Forms.RouteCreateEditPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="StatusDropDownList" runat="server" S>
            <asp:ListItem>Не завершено</asp:ListItem>
            <asp:ListItem>Завершено</asp:ListItem>
        </asp:DropDownList>
    </form>
</body>
</html>
