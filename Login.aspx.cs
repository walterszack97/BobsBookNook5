using System;
using System.IO;

public partial class Login : System.Web.UI.Page
{
	string uploadPath;
	protected void Page_Load(object sender, EventArgs e)
	{
	}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //use a bool flag to indicate errors
        bool okFlag = true;
        //make sure ownerID is entered twice in the form
        if ((txtOwnerID.Text == "") || (txtOwnerID0.Text == ""))
        {
            okFlag = false;
            txtOwnerID.Text = "";
            txtOwnerID0.Text = "";
            txtOwnerID.Focus();
        }
        //make sure ownerIDs match
        if (txtOwnerID.Text != txtOwnerID0.Text)
        {
            okFlag = false;
            txtOwnerID.Text = "";
            txtOwnerID0.Text = "";
            txtOwnerID.Focus();
        }
        //if ownerID does not end with underscore, simply add it.
        if (!txtOwnerID.Text.EndsWith("_"))
        {
            txtOwnerID.Text += "_";
        }
        //Check okFlag for 'permission' to go back to Admin.
        if (okFlag)
        {
            // this is the original code to write the file and move to Admin.
            using (StreamWriter _writeFile = new StreamWriter(Server.MapPath("~/App_Data/ownerID.txt"), false))
            {
                _writeFile.WriteLine(txtOwnerID.Text); // Write the file.
                Response.Redirect("Admin.aspx");
            }
        }//end of if(okFlag)
    }
}