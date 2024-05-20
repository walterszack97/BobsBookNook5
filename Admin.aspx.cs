using System;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
//using System.Drawing;


public partial class Admin : System.Web.UI.Page
{
    string uploadPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    var data = File.ReadAllText(Server.MapPath("~/App_Data/ownerID.txt"));
        //    //Using the Trim() function makes sure return and line feed codes are removed
        //    txtOwnerID.Text = data.ToString().Trim();
        //} // end try
        //catch
        //{
        //    Response.Redirect("Login.aspx");
        //}
        txtOwnerID.Text = getOwnerID.getID(Server.MapPath("~/App_Data/"));

        //not necessary for FileUpload2
        //allows us to select all jpg files at the same time.
        FileUpload1.AllowMultiple = true;
        //Set path to Img
        uploadPath = Server.MapPath("~/Img/");
        //used when creating a new book store.
        //The following statement is on one line of code.
        checkTables(txtOwnerID.Text + "BOOKS", "CREATE TABLE " + txtOwnerID.Text + "BOOKS (Title CHAR(64), " +
            "ISBN CHAR(14) UNIQUE, Author CHAR(64), Category CHAR(48), Image CHAR(128), Description CHAR(255))");

        checkTables(txtOwnerID.Text + "BESTSELLER", "CREATE TABLE " + txtOwnerID.Text + "BESTSELLER (ISBN CHAR(20))");

        initLstProgress();
    }

    private void checkTables(string Table, string sqlStatement)
    {
        //Response.Write(Table + " " + sqlStatement + "<br/>");
        //throw new NotImplementedException();
        lblErrorMessage.Text = "";
        myDatabaseConnection.executeSQL("SELECT * FROM " + Table, ref gvHidden, ref lblErrorMessage);
        //turn off gridview
        gvHidden.Visible = false;
        //check for error message: create the connection if necessary
        if (lblErrorMessage.Text.Length > 0)
        {
            lblErrorMessage.Text = "";
            myDatabaseConnection.executeSQL(sqlStatement, ref gvHidden, ref lblErrorMessage);
        }
    }

    private void initLstProgress()
    {
        //throw new NotImplementedException();
        lstProgress.Items.Clear();
        lstProgress.Items.Add("General Instructions:");
        lstProgress.Items.Add("To view the current Inventory,");
        lstProgress.Items.Add("click [View Inventory]");
        lstProgress.Items.Add("");
        lstProgress.Items.Add("To upload images:");
        lstProgress.Items.Add("Click [Browse]");
        lstProgress.Items.Add("Locate and select the images");
        lstProgress.Items.Add("for uploading.");
        lstProgress.Items.Add("Click [Open]");
        lstProgress.Items.Add("Click [Load Images]");
        lstProgress.Items.Add("");
        lstProgress.Items.Add("To upload images:");
        lstProgress.Items.Add("Click [Browse]");
        lstProgress.Items.Add("Locate and select the XML ");
        lstProgress.Items.Add("file for uploading.");
        lstProgress.Items.Add("Click [Open]");
        lstProgress.Items.Add("Click [Import Book Data]");
        lstProgress.Items.Add("");
    }

    protected void btnResetUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void btnClearDatabase_Click(object sender, EventArgs e)
    {
        string sqlCommand = "Drop Table " + txtOwnerID.Text + "BOOKS";
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
    }

    protected void btnLoadImages_Click(object sender, EventArgs e)
    {
        //Clear list and label
        lstHidden.Items.Clear();
        //check to see if you have selected a file
        if (!FileUpload1.HasFiles)
        {
            //if not, error message is displayed and focus set to fileupload
            lstProgress.Items.Add("Browse for files and select");
            lstProgress.Items.Add("all images from the directory."); FileUpload1.Focus();
        }
        else
        {
            lstProgress.Items.Add("Process JPG Files.");
            foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
            {
                //if the file exists on the server, it will simply be overwritten.
                // string fileName = Path.GetFileName(uploadedFile.FileName);
                //uploadedFile.SaveAs(Server.MapPath("~/Img/") + fileName);
                //lstHidden.Items.Add(String.Format("{0}", fileName));
                System.Drawing.Image bm = System.Drawing.Image.FromStream(uploadedFile.InputStream);
                // Response.Write(bm.Width + " " + bm.Height + " " + "<HR>");
                bm = ResizeBitmap((Bitmap)bm, 100, 150); /// new width, height
                int len = uploadedFile.FileName.ToString().Length;
                int x = uploadedFile.FileName.ToString().LastIndexOf('\\');
                string newFileName = uploadedFile.FileName.ToString().Substring(x + 1);
                //Response.Write(newFileName + "<hr>");//TESTING ONLY
                bm.Save(Path.Combine(uploadPath, newFileName));
            }
        }
    }

    private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
    {
        Bitmap result = new Bitmap(nWidth, nHeight);
        using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
            g.DrawImage(b, 0, 0, nWidth, nHeight);
        return result;
    }

    protected void btnLoadXML_Click(object sender, EventArgs e)
    {
        lstHidden.Items.Clear();
        lblErrorMessage.Text = null;
        //check to see if you have selected a file
        if (!FileUpload2.HasFile)
        {
            //if not, error message is displayed and focus set to fileupload
            lstProgress.Items.Add("[Browse] for the XML file");
            lstProgress.Items.Add("and select.");
            FileUpload2.Focus();
        }
        else
        {
            lstProgress.Items.Add("Process XML File.");

            String fileName = uploadPath + FileUpload2.FileName;
            lstProgress.Items.Add(fileName);
            FileUpload2.SaveAs(fileName);
            //Read xml file to lstHidden
            try
            {
                if (File.Exists(fileName))
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        while (sr.Peek() >= 0)
                        {
                            lstHidden.Items.Add(sr.ReadLine());
                        }
                    }
            } // end try
            catch (Exception f)
            {
                Console.WriteLine("The process failed: {0}", f.ToString());
            }

            decode(lstHidden);
        }
    }

    private void decode(ListBox lstHidden)
    {
        // throw new NotImplementedException();
        //method level variable
        string field;
        //remove the first two lines from lstHidden
        //we do not need the <?xml version="1.0" encoding="utf-8"?>
        //nor do we need to process < Inventory >
        lstHidden.Items.RemoveAt(0);
        lstHidden.Items.RemoveAt(0);

        //Create the start of an SQL statement
        while (lstHidden.Items.Count > 1)
        {
            string sqlStatement = "INSERT INTO " + txtOwnerID.Text + "BOOKS (Title, ISBN, Author, Category, Image, Description) VALUES (";
            //process 6 tags
            //remove <books> tag
            lstHidden.Items.RemoveAt(0);
            for (int i = 0; i < 6; i++)
            {
                //pull the top record from lstHidden
                field = lstHidden.Items[0].ToString();
                //call stripTags to remove each third level matching tag.
                field = stripTags(field);
                //field 5, i == 4, is the jpg field
                //We are just saving the folder and file name
                if (i == 4)
                {
                    sqlStatement += "'" + "\\Img\\" + field + "'";
                }
                else
                {
                    // if description is over 255 characters, it needs to be truncated to fit our field descriptors
                    if (i == 5) //255 length
                    {
                        if (field.Length >= 255)
                        {
                            field = field.Substring(0, 254);
                        }
                    }
                    //replace all ' with '': single with double
                    field = field.Replace("'", "''");
                    //append to sqlStatement
                    sqlStatement += " '" + field + "'";
                }
                if (i < 5) //5 is the last field. We are not yet there, so add a ,
                {
                    sqlStatement += ", ";
                }
                //remove record from lstHidden
                lstHidden.Items.RemoveAt(0);
            }
            //when all 6 records have been processed, append the closing )
            sqlStatement += (")");
            //see what it looks like on the screen
            // Response.Write(sqlStatement + "<hr>");
            myDatabaseConnection.executeSQL(sqlStatement, ref gvDisplay, ref lblErrorMessage);
            //remove </books> tag
            // Make sure this is within the while code block.
            lstHidden.Items.RemoveAt(0);
        }
    }

    private string stripTags(string field)
    {
        // throw new NotImplementedException();
        //remove up to first >
        int x = field.IndexOf(">");
        field = field.Substring(x + 1);
        //remove second tag
        x = field.LastIndexOf("<");
        field = field.Substring(0, x);
        return (field);
    }

    protected string FormatImageUrl(string url)
    {
        if (url != null && url.Length > 0)
            return ("~/" + url);
        else return null;
    }

    protected void btnViewInventory_Click(object sender, EventArgs e)
    {
        string sqlCommand = "SELECT Title, ISBN, Author, Category, Image, Description FROM " + txtOwnerID.Text + "BOOKS";
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnProcessBestSellers_Click(object sender, EventArgs e)
    {
        Response.Redirect("processBestsellers.aspx");
    }

    protected void btnDeleteBooks_Click(object sender, EventArgs e)
    {
        Response.Redirect("removeBooksFromInventory.aspx");
    }




    protected void btnProcessEmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProcessEmail.aspx");
    }
}