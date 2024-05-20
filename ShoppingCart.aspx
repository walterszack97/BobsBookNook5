<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bobs Book Nook Shopping Cart</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return to Bookstore" />
            <br />
            <asp:Button ID="btnCheckout" runat="server" OnClick="btnCheckout_Click" Text="Checkout" />
            <asp:GridView ID="gvShoppingCart" runat="server" DataKeyNames="ISBN" Visible="False">
            </asp:GridView>
            <br />
            <asp:GridView ID="gvDisplay" runat="server" DataKeyNames="ISBN" AutoGenerateColumns="False" OnSelectedIndexChanged="gvDisplay_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                    <ItemTemplate> <asp:Image ID="Image1" runat ="server" ImageUrl='<%# (string) FormatImageUrl( (string) Eval("Image")) %>' />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Author" HeaderText="Author" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True" SelectText="Place Back on Shelf" />
                </Columns>
             </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" Text="People who purchased this also purchased the following:"></asp:Label>
            <br />
            <asp:GridView ID="gvPeopleWhoPurchasedThis" runat="server" AutoGenerateColumns="False" DataKeyNames="ISBN">
                <Columns>
                <asp:TemplateField>
                <ItemTemplate> <asp:Image ID="Image1" runat ="server" ImageUrl='<%# (string) FormatImageUrl( (string) Eval("Image")) %>' />
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" />
                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                <asp:CommandField ButtonType="Button" InsertVisible="False" SelectText="Purchase" ShowSelectButton="True" />
                </Columns>
                </asp:GridView>
        </div>
    </form>
</body>
</html>
