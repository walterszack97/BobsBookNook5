using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

public partial class ShoppingCart : System.Web.UI.Page
{
    string ownerID;
    protected void Page_Load(object sender, EventArgs e)
    {

        ownerID = getOwnerID.getID(Server.MapPath("~/App_Data/"));

        //try
        //{
        //    //Save session variable
        //    Session[Session.Count.ToString()] = Request.QueryString["id"];
        //    //display session variable
        //    for (int i = 0; i < Session.Count; i++)
        //    {
        //        Response.Write("<br />" + Session[i.ToString()].ToString());
        //    }
        //}
        //catch { Response.Redirect("~/Default.aspx"); }

        string strQS = Request.QueryString["id"];
        if (strQS == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            CreateSessionVariable(strQS);
            //if (!IsPostBack)
            //{
            //    addToAndShowGridView(strQS);
            //    viewGvDisplay();
            //}
        }

        if (!IsPostBack)
        {
            //show session variable so we know it works.
            // Response.Write(Session["tempUserID"].ToString() + "<br/>");
            //Show ISBN of new Selection
            // Response.Write(Request.QueryString["id"] + "<br/>");
            //Add ISBN to database table and view
           addToShoppingList(Session["tempUserID"].ToString(), Request.QueryString["id"]);
        }
        viewGvDisplay();
        loadPurchase(Request.QueryString["id"]);
    }

    private void loadPurchase(string ISBN)
    {
        // throw new NotImplementedException();
        string sqlCommand;
        // throw new NotImplementedException();
        sqlCommand = "SELECT IMAGE, TITLE, AUTHOR, ISBN FROM " + ownerID + "BOOKS WHERE " + ownerID + "BOOKS.ISBN NOT LIKE '" + ISBN + "' AND " + ownerID + "BOOKS.ISBN IN (SELECT DISTINCT ISBN FROM " + ownerID + "HISTORY WHERE " + ownerID + "HISTORY.EMAIL IN (SELECT DISTINCT " + ownerID + "HISTORY.EMAIL FROM " + ownerID + "HISTORY WHERE " + ownerID + "HISTORY.ISBN = '" + ISBN + "'))";
        myDatabaseConnection.executeSQL(sqlCommand, ref gvPeopleWhoPurchasedThis, ref lblErrorMessage);
    }

    private void viewGvDisplay()
    {
        //throw new NotImplementedException();
        string sqlCommand;
        //throw new NotImplementedException();
        try
        {
            string table1 = ownerID + "BOOKS";
            string table2 = Session["tempUserID"].ToString();
            sqlCommand = "SELECT IMAGE, AUTHOR, TITLE, " + table1 + ".ISBN from " + table1 + " INNER JOIN " + table2 + " ON " + table1 + ".ISBN = " + table2 + ".ISBN ";
            myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void addToShoppingList(string userID, string ISBN)
    {
        //throw new NotImplementedException();
        string sqlCommand = "INSERT INTO " + userID + " VALUES(" + ISBN + ")";
        myDatabaseConnection.executeSQL(sqlCommand, ref gvShoppingCart, ref lblErrorMessage);
        sqlCommand = "SELECT * FROM " + userID;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvShoppingCart, ref lblErrorMessage);
        gvShoppingCart.Visible = false;
    }

    private void CreateSessionVariable(string strQS)
    {
        string newTempUserID = "";
        string tempUserSource = "A" + "B" + "C" + "D" + "E" + "F" + "G" + "H" + "I" + "J" + "K" + "L" + "M" + "N" + "O" + "P" + "Q" + "R" + "S" + "T" + "U" + "V" + "W" + "X" + "Y" + "Z" + "0" + "1" + "2" + "3" + "4" + "5" + "6" + "7" + "8" + "9";
        //remove after testing
        //Response.Write(tempUserSource + "<hr>");

        if (System.Web.HttpContext.Current.Session["tempUserID"] != null)
        {
            newTempUserID = Session["tempUserID"].ToString();
        }
        else
        {
            Random r = new Random();
            int index = r.Next(0, 26);//Makes sure first character is a letter
            newTempUserID += tempUserSource.Substring(index, 1);
            for (int i = 2; i < 21; i++)
            {
                index = r.Next(0, 36);
                //Must be += or we only capture the last letter.
                newTempUserID += tempUserSource.Substring(index, 1);
            }
            Session["tempUserID"] = newTempUserID;
            string sqlCommand = "CREATE TABLE " + newTempUserID + " (ISBN CHAR(14))";
            myDatabaseConnection.executeSQL(sqlCommand, ref gvShoppingCart, ref lblErrorMessage);
            gvShoppingCart.Visible = false;
        }
        //Response.Write(newTempUserID + "<HR>");

        if (System.Web.HttpContext.Current.Session["tempUserID"] != null)
        {
            newTempUserID = Session["tempUserID"].ToString();
        }
        else
        {
            Random r = new Random();
            int index = r.Next(0, 26);//Makes sure first character is a letter
            newTempUserID += tempUserSource.Substring(index, 1);
            for (int i = 2; i < 21; i++)
            {
                index = r.Next(0, 36);
                //Must be += or we only capture the last letter.
                newTempUserID += tempUserSource.Substring(index, 1);
            }
            Session["tempUserID"] = newTempUserID;
            string sqlCommand = "CREATE TABLE " + newTempUserID + " (ISBN CHAR(14))";
            myDatabaseConnection.executeSQL(sqlCommand, ref gvShoppingCart, ref lblErrorMessage);
            gvShoppingCart.Visible = false;
        }
        if(!IsPostBack)
        {
            //addToShoppingList(Session["tempUserID"].ToString(), Request.QueryString["id"]);
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        //string sqlCommand = "DROP TABLE " + Session["tempUserID"];
        //myDatabaseConnection.executeSQL(sqlCommand, ref gvShoppingCart, ref lblErrorMessage);
        //gvShoppingCart.Visible = false;
        //Session["tempUserID"] = null;
        //Response.Redirect("Default.aspx");
        Response.Redirect("CheckOut.aspx");
    }

    protected string FormatImageUrl(string url)
    {
        if (url != null && url.Length > 0)
            return ("~/" + url);
        else return null;
    }

    protected void gvDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Response.Write(gvDisplay.SelectedValue.ToString());
        string sqlCommand;
        string table1 = ownerID + "BOOKS";
        string table2 = Session["tempUserID"].ToString();
        sqlCommand = "DELETE FROM " + table2 + " WHERE ISBN = " + gvDisplay.SelectedValue;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        // and use a call to viewGvDisplay to repopulate gvDisplay.
        viewGvDisplay();
    }

    protected void gvPeopleWhoPurchasedThis_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("ShoppingCart.aspx?ID=" + gvPeopleWhoPurchasedThis.SelectedValue);
    }
}
