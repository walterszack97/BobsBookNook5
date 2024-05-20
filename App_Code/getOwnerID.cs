using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for getOwnerID
/// </summary>
public class getOwnerID
{
    public getOwnerID()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string getID(string uploadPath)
    {
        string ownerID;
        try
        {
            ownerID = System.IO.File.ReadAllText(uploadPath + "ownerID.txt");
            ownerID = ownerID.Replace("\r\n", "");
            return (ownerID);
        }
        catch { return (""); }
    }
}