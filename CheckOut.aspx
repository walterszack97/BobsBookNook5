<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOut.aspx.cs" Inherits="CheckOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Out</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
	
            <asp:Label ID="lblMessage" runat="server" Text="Please enter the email address for delivery of your books."></asp:Label>
            <br />
            <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="True" Width="445px"></asp:TextBox>
            <br />
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return to Book Store" />
            <br />
            <asp:GridView ID="gvISBN" runat="server" DataKeyNames="ISBN" Visible="False">
            </asp:GridView>
            <br />
            <asp:GridView ID="gvDisplay" runat="server" DataKeyNames="ISBN">
            </asp:GridView>
            <br />
	
        </div>
    </form>
</body>
</html>
