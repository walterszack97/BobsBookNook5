<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
	<style type="text/css">
	    .auto-style1 {
            width: 100%;
        }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>Enter your ownerID</td>
                <td>
                    <asp:TextBox ID="txtOwnerID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Re-enter ownerID</td>
                <td>
                    <asp:TextBox ID="txtOwnerID0" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
