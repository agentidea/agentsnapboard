using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void getStoryInfo()
    {
        SqlConnection conn = new SqlConnection("Context Connection=true");
        SqlCommand cmd = new SqlCommand(@"SELECT id FROM PageElement ORDER BY id desc", conn);

        conn.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        SqlContext.Pipe.Send(rdr);

        rdr.Close();
        conn.Close();
    }
};
