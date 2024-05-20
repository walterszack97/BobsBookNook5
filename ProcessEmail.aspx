<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessEmail.aspx.cs" Inherits="ProcessEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Email</title>
	<style type="text/css">
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnBackToAdmin" runat="server" OnClick="btnBackToAdmin_Click" Text="Back To Admin" />
            <br />
            <br />
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" />
            <asp:Button ID="btnStep2" runat="server" OnClick="btnStep2_Click" Text="Step 2" />
            <br />
            <asp:ListBox ID="lstRawXML" runat="server"></asp:ListBox>
            <asp:ListBox ID="lstEmail" runat="server"></asp:ListBox>
        </div>
    </form>
</body>
</html>
