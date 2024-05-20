<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bob's BookNook Admini Page</title>
	<style type="text/css">
		
	    .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 108px;
        }

        td {
            vertical-align: top;
        }
		
	    .auto-style3 {
            height: 108px;
            width: 130px;
            margin-left: 40px;
        }
		
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">To BookStore</asp:LinkButton>
        <br />
        <table class="auto-style1">
            <tr>
                <td rowspan="2">
                    <asp:ListBox ID="lstProgress" runat="server" Height="350px" Width="200px"></asp:ListBox>
                    <asp:GridView ID="gvHidden" runat="server" Visible="False">
                    </asp:GridView>
                </td>
                <td class="auto-style2">Your Software OwnerID:
                    <asp:TextBox ID="txtOwnerID" runat="server" Enabled="False"></asp:TextBox>
                    <asp:Button ID="btnResetUser" runat="server" Text="Reset User" OnClick="btnResetUser_Click" />
                    <br />
                    <br />
                    If you click
                    <asp:Button ID="btnClearDB" runat="server" BackColor="Red" ForeColor="Yellow" Text="Clear Database" OnClick="btnClearDatabase_Click" />
&nbsp;you will have to reload all xml and jpg files.<br />
                    <br />
                    Browse for and select BookJacket images then click [Load Images]<br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btnLoadImages" runat="server" Text="Load Images" OnClick="btnLoadImages_Click" />
                    <br />
                    <br />
                    Browse for BookInventory.xml file, then click [Import Book Data]<br />
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                    <asp:Button ID="btnLoadXML" runat="server" Text="Import Book Data" OnClick="btnLoadXML_Click" />
                </td>
                <td class="auto-style3">
                    <asp:Button ID="btnViewInventory" runat="server" Height="108px" Text="View Inventory" Width="140px" OnClick="btnViewInventory_Click" />
                    <asp:Button ID="btnProcessBestSellers" runat="server" Height="30px" Text="Process Best Sellers" Width="140px" OnClick="btnProcessBestSellers_Click" />
                    <br />
                    <asp:Button ID="btnDeleteBooks" runat="server" Height="30px" OnClick="btnDeleteBooks_Click" Text="Delete Books" Width="140px" />
                    <br />
                    <asp:Button ID="btnProcessEmail" runat="server" Height="30px" Text="Process Email" Width="140px" OnClick="btnProcessEmail_Click" />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                    <asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="False">
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
                    </Columns>
                    </asp:GridView>
                    <asp:ListBox ID="lstHidden" runat="server" Width="458px" Visible="False"></asp:ListBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
