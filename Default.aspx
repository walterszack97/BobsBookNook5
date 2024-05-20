<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bobs Book Nook</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
        }
        td {
            vertical-align: top;
           }
        .auto-style2 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
        </div>
        <table class="auto-style1">
            <tr style="background-color: grey">
                <td style="display: flex; justify-content: center; align-items: center;"><img src="img/logo.svg" alt="logo" width="300" height="100" style=""/></td>
                <td><img src="img/banner.jpg" alt="banner" style="width: 100%; height: 100px;"/></td>
            </tr>
            <tr>
                <td>
                    <table class="auto-style2">
                        <tr>
                            <td>
                                <asp:Button ID="btnClearSearch" runat="server" Text="Clear Search Options" />
                                <br />
                                <asp:GridView ID="gvAuthors" runat="server" AutoGenerateColumns="False" BackColor="LightBlue" DataKeyNames="Author" ForeColor="DarkBlue" OnSelectedIndexChanged="gvAuthors_SelectedIndexChanged" Width="116px">
                                    <Columns>
                                        <asp:BoundField DataField="Author" HeaderText="Author" />
                                        <asp:CommandField ButtonType="Button" CausesValidation="False" SelectText="View" ShowCancelButton="False" ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </td>
                            <td>
                                <asp:Button ID="btnBestSellers" runat="server" OnClick="btnBestSellers_Click" Text="View Best Sellers" />
                                <br />
                                <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" BackColor="LightGreen" DataKeyNames="Category" ForeColor="DarkGreen" OnSelectedIndexChanged="gvCategories_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="Category" HeaderText="Category" />
                                        <asp:CommandField ButtonType="Button" CausesValidation="False" SelectText="View" ShowCancelButton="False" ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:GridView ID="gvCatalog" runat="server" AutoGenerateColumns="False">
                        <Columns>
                        <asp:TemplateField>
                        <ItemTemplate> <asp:Image ID="Image1" runat ="server" ImageUrl='<%# (string) FormatImageUrl( (string) Eval("Image")) %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Author" HeaderText="Author" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:HyperLinkField DataNavigateUrlFields="ISBN" DataNavigateUrlFormatString="ShoppingCart.aspx?Id={0}" DataTextField="ISBN" DataTextFormatString="Buy Me" HeaderText="Click to Purchase" NavigateUrl="~/ShoppingCart.aspx" Target="_top" Text="ISBN" />
                        </Columns>
                     </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
