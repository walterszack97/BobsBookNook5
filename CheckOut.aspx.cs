using System;
using System.Web.UI.WebControls;

public partial class CheckOut : System.Web.UI.Page
{
    string ownerID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ownerID = getOwnerID.getID(Server.MapPath("~/App_Data/"));

        if (!IsPostBack)
        {
            lblMessage.Text = "Please enter the email address for delivery of your books.";
            txtEmail.Visible = true;
            txtEmail.Text = "";
        }
        if (IsPostBack)
        {
            //maintain email address for history
            string historyEMail = txtEmail.Text;
            //Re-populate the txtbox with the message that says these are to be sent to you.
            txtEmail.Text = "The following books will be sent to " + txtEmail.Text + " within 10 minutes.";
            string sqlCommand;
            string table2 = Session["tempUserID"].ToString();
            sqlCommand = "SELECT * FROM " + table2;
            myDatabaseConnection.executeSQL(sqlCommand, ref gvISBN, ref lblMessage);

            gvISBN.Visible = false;
            Response.Write("Your card will be charged US$" + Math.Round(Convert.ToDecimal(gvISBN.Rows.Count * 5.99), 2) + "<br />");
            //send purchases to History
            sqlCommand = "INSERT INTO " + ownerID + "HISTORY (EMAIL, ISBN) SELECT '" + historyEMail + "', ISBN FROM " + table2;
            //Response.Write(sqlCommand); //Make sure to delete me prior to final submission
            myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblMessage);

            //send purchases to History
            sqlCommand = "INSERT INTO " + ownerID + "HISTORY (EMAIL, ISBN) SELECT '" + historyEMail + "', ISBN FROM " + table2;
            //Response.Write(sqlCommand); //Make sure to delete me prior to final submissionsubmission
            //myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblMessage);

            //show contents of history just for validation. To be removed!!!
            //sqlCommand = "SELECT * FROM " + ownerID + "HISTORY";
            //myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblMessage);
            try
            {
                string table1 = ownerID + "BOOKS";
                sqlCommand = "SELECT TITLE, AUTHOR, " + table1 + ".ISBN from " + table1 + " INNER JOIN " + table2 + " ON " + table1 + ".ISBN = " + table2 + ".ISBN ";
                myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblMessage);
            }
            catch
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        string sqlCommand;
        string table2 = Session["tempUserID"].ToString();
        sqlCommand = "DROP TABLE " + table2;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvISBN, ref lblMessage);
        Session["tempUserID"] = null;
        Response.Redirect("Default.aspx");
    }
}