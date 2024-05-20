using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class ProcessEmail : System.Web.UI.Page
{
    string ownerID;
	protected void Page_Load(object sender, EventArgs e)
	{
        ownerID = getOwnerID.getID(Server.MapPath("~/App_Data/"));
        string fileName = Server.MapPath("~/img") + "\\BookInventory.xml";

        try
        {
            if (File.Exists(fileName))
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (sr.Peek() >= 0)
                    {
                        string lineIn = sr.ReadLine();
                        lineIn = lineIn.Trim().ToUpper();
                        if (lineIn.StartsWith("<TITLE>") || lineIn.StartsWith("<AUTHOR>") || lineIn.StartsWith("<IMAGE>"))
                        {
                            lineIn = stripTags(lineIn);
                            lstRawXML.Items.Add(lineIn);
                        }
                    }
                }
        } // end try
        catch (Exception f)
        {
            Console.WriteLine("The process failed: {0}", f.ToString());
        }
    }

    private string stripTags(string field)
    {
        //remove up to first >
        int x = field.IndexOf(">");
        field = field.Substring(x + 1);
        //remove second tag
        x = field.LastIndexOf("<");
        field = field.Substring(0, x);
        return (field);
    }

    protected void btnBackToAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin.aspx");
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sqlCommand;
        string author;
        string title;
        string image;
        while (lstRawXML.Items.Count > 0)
        {
            //title
            title = lstRawXML.Items[0].ToString();
            lstRawXML.Items.RemoveAt(0);
            //author
            author = lstRawXML.Items[0].ToString();
            lstRawXML.Items.RemoveAt(0);
            //image
            image = lstRawXML.Items[0].ToString();
            lstRawXML.Items.RemoveAt(0);
            //add the new record to lstEmail
            lstEmail.Items.Add(title + ":" + author + ":" + image);
            //look for users this is that "cat's meow" sql
            sqlCommand = "SELECT DISTINCT " + ownerID + "HISTORY.EMAIL FROM " + ownerID + "HISTORY WHERE " + ownerID + "HISTORY.ISBN IN (SELECT DISTINCT " + ownerID + "BOOKS.ISBN FROM " + ownerID + "BOOKS WHERE " + ownerID + "BOOKS.AUTHOR = '" + author + "')";
            fillListBox(sqlCommand, lstEmail, ownerID + "HISTORY", "EMAIL");
            btnStep2.Enabled = true;
            btnGo.Enabled = false;
        }
    }

    private void fillListBox(string sqlCommand, ListBox listBox, string tableName, string field)
    {
        DataRow dr;
        DataTable dt = new DataTable();
        try
        { //open the connection
            myDatabaseConnection.myConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand, myDatabaseConnection.myConnection);
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                listBox.Items.Add(dr[field].ToString());
            }
            listBox.SelectedIndex = 0;
        }
        catch (Exception ex) { lblErrorMessage.Text = ex.ToString(); }
        try
        { //close the connection
            myDatabaseConnection.myConnection.Close();
        }
        catch (Exception ex) { lblErrorMessage.Text = ex.ToString(); }
    }

    protected void btnStep2_Click(object sender, EventArgs e)
    {
        string[] record = null;
        //throw new NotImplementedException();
        //Clear lstRawXML
        lstRawXML.Items.Clear();
        //Add the first two lines of the XML file
        lstRawXML.Items.Add("<? xml version = \"1.0\" encoding = \"utf-8\" ?>");
        lstRawXML.Items.Add("<EMAIL>");
        // process each record in the dropdown list
        while (lstEmail.Items.Count > 0)
        {
            if (lstEmail.Items[0].ToString().Contains(":"))
            {
                //get and parse title, author, image
                record = lstEmail.Items[0].ToString().Split(':');
            }
            else
            {
                lstRawXML.Items.Add("<RECORD>");
                lstRawXML.Items.Add("<AUTHOR>" + record[1] + "</AUTHOR>");
                lstRawXML.Items.Add("<TITLE>" + record[0] + "</TITLE>");
                lstRawXML.Items.Add("<IMAGEURL>" + record[2] + "</IMAGEURL>");
                lstRawXML.Items.Add("<ADDRESS>" + lstEmail.Items[0].ToString() + "</ADDRESS > ");
                lstRawXML.Items.Add(" </ RECORD > ");
            }
            //remove the first element in the dropdown list
            lstEmail.Items.RemoveAt(0);
        }
        //When done, add the closing </EMAIL> tag.
        lstRawXML.Items.Add("</EMAIL>");
    }
}
