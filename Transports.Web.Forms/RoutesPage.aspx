<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RoutesPage.aspx.cs" Inherits="Transports.Web.Forms.RoutesPage" %>

<asp:Content ID="ShiftPage" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Shifts list </h1>
        <asp:Button runat="server" class='btn btn-warning' OnClick="OnClick" Text="Create" role='button'></asp:Button>
        <hr>
        <asp:Repeater ID="Repeater" runat="server" onitemcommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <div>
                    <span>Id: <%#Eval("RouteId") %></span>
                    <h5>Length: <%#Eval("Length") %></h5>
                    <h5>Is Traffic Jam:  <%#Eval("IsTrafficJam") %></h5>
                    <h5>Estimated time:  <%#Eval("EstimatedTime") %></h5>
                    <span>Shift Id: <%#Eval("Shift.ShiftId") %></span>
                </div>
                <asp:Button ID="test" runat="server" CommandName="Update" CommandArgument='<%# Eval("RouteId") %>' class='btn btn-info' Text="Update"></asp:Button>
                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("RouteId") %>' class='btn btn-info' Text="Delete"></asp:Button>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
