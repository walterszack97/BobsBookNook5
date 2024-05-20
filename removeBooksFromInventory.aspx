<%@ Page Language="C#" AutoEventWireup="true" CodeFile="removeBooksFromInventory.aspx.cs" Inherits="removeBooksFromInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remove Books From Inventory</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return to Admin" />
            <br />
            <asp:Label ID="lblErrorMessage" runat="server" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="gvDeleteBooks" runat="server" AutoGenerateColumns="False" DataKeyNames="ISBN" OnSelectedIndexChanged="gvDeleteBooks_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate> <asp:Image ID="Image1" runat ="server" ImageUrl='<%# (string) FormatImageUrl( (string) Eval("Image")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:CommandField ButtonType="Button" InsertVisible="False" SelectText="Remove from Inventory" ShowCancelButton="False" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
