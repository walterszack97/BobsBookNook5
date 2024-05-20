<%@ Page Language="C#" AutoEventWireup="true" CodeFile="processBestsellers.aspx.cs" Inherits="processBestsellers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Best Sellers</title>
	<style type="text/css">
	    .auto-style1 {
            width: 100%;
        }
        td {
            vertical-align: top;
        }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnAdmin" runat="server" OnClick="btnAdmin_Click" Text="Return to Admin" />
        </div>
        <asp:Label ID="lblErrorMessage" runat="server" Visible="False"></asp:Label>
        <br />
        <table class="auto-style1">
            <tr>
                <td>Select ISBN to Add Best Seller</td>
                <td>Select ISBN to Remove Best Seller</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCatalog" runat="server" DataKeyNames="ISBN" AutoGenerateColumns="False" OnSelectedIndexChanged="gvCatalog_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Add to Best Sellers" ShowCancelButton="False" ShowSelectButton="True" />
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Author" HeaderText="Author" />
                            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="gvBestSellers" runat="server" DataKeyNames="ISBN" AutoGenerateColumns="False" OnSelectedIndexChanged="gvBestSellers_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Remove" ShowCancelButton="False" ShowSelectButton="True" />
                            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
