using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for myDatabaseConnection
/// </summary>
public class myDatabaseConnection
{
    public static SqlConnection myConnection = new SqlConnection(
        "Data Source= SQL5025.myWindowsHosting.com;" +
        "Initial Catalog=DB_A28DC6_mccSupport;" +
        "User Id=DB_A28DC6_mccSupport_admin;" +
        "Password=passw0rd;");
    public myDatabaseConnection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void executeSQL(string sqlCommand, ref GridView gvDisplay, ref Label lblErrorMessage)
    {
        //turn off the grid display and erase any previous error messages on the parent page
        gvDisplay.Visible = false;
        lblErrorMessage.Text = "";
     

        try //we can nest try catch blocks
        {
            //open connection
            myConnection.Open(); // references the public static SqlConnection above

            SqlCommand myCommand = new SqlCommand("Command String", myConnection);
            //pass the sql statement to myCommand
            myCommand.CommandText = sqlCommand;
            try
            {
                if (sqlCommand.StartsWith("SELECT"))
                {
                    //gives the data someplace to land
                    gvDisplay.DataSource = myCommand.ExecuteReader();
                    gvDisplay.DataBind();
                    //turn the grid display back on
                    gvDisplay.Visible = true;
                }
                else
                {
                    //just execute the query
                    myCommand.ExecuteNonQuery();
                }
            }
            //print any catch conditions in lblErrorMessage
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.ToString();
                
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.ToString();
        }
        //close the connection
        try
        {
            myConnection.Close();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.ToString();
        }
    }

    public static void fillDropDownList(DropDownList dropdownId, ListBox listboxId, string tableName, string field, string index, ref Label lblErrorMessage)
    {
        //throw new NotImplementedException();
        dropdownId.Items.Add("*");

        DataRow dr;
        DataTable dt = new DataTable();
        string sqlCommand = "SELECT " + field + ", " + index + " FROM " + tableName + " ORDER BY " + field;

        try
        { //open the connection myConnection.Open();
            myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand, myConnection);
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                dropdownId.Items.Add(dr[field].ToString());
                listboxId.Items.Add(dr[index].ToString());
            }
            dropdownId.SelectedIndex = 0;
        }
        catch (Exception ex) { lblErrorMessage.Text = ex.ToString(); }

        try
        { //close the connection
            myConnection.Close();
        }
        catch (Exception ex) { lblErrorMessage.Text = ex.ToString(); }
    }
}