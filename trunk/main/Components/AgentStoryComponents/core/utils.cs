using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;	
using System.Collections.Specialized;	
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace AgentStoryComponents.core
{


    public static class TheUtils
    {
        private static AgentStoryComponents.core.utils _ute;

        public static AgentStoryComponents.core.utils ute
        {
            get
            {
                if (_ute == null)
                    _ute = new utils();
                return _ute;
            
            }
        }
	
    }


	public class OleDbHelper
	{
		public System.Data.OleDb.OleDbConnection connection=null;
		public System.Data.OleDb.OleDbCommand cmd=null;
		public System.Data.OleDb.OleDbDataReader reader=null;

		public void cleanup()
		{
			try
			{
				if(reader!=null)
				{
					if(reader.IsClosed==false)
						reader.Close();

					reader = null;
					
				}

				if(cmd!=null)
				{
					cmd.Dispose();
					cmd = null;
				}

				if(connection!=null)
				{
					connection.Dispose();
					connection.Close();
					connection=null;
				}


			}
			catch(Exception exp)
			{
				throw new Exception("Cleanup Error: " + exp.Message);
			}


		}

		
	}


    public class Snip
    {
        public string snippet="";
        public int typeID = -1;

    }
	
	public class utils
	{

		public const bool EMPTY_AS_NULL = true;
		public const bool EMPTY_NOTAS_NULL = false;


        public Snip getSnippet(string sVal,int maxLenSnip)
        {
            string snippet = "";
            Snip snip = new Snip();
            if (sVal.IndexOf("<pre") != -1 || sVal.IndexOf("<PRE") != -1 )
            {
                snip.typeID = 1; //txt
                int lenToSnip = sVal.Length - 11;  //11 is the total of tags(?)
                if (lenToSnip > maxLenSnip) lenToSnip = maxLenSnip;
                //stript out spaces!! and white space !!!!

                snip.snippet = sVal.Substring(5, lenToSnip);
                snip.snippet = this.stripWhiteSpace(snip.snippet);

            }
            else
            if (sVal.IndexOf("<img") != -1 || sVal.IndexOf("<IMG") != -1)
            {
                snip.typeID = 4; //IMG
                snip.snippet = "image";

            }
            else
            if (sVal.IndexOf("<embed") != -1 || sVal.IndexOf("<EMBED") != -1)
            {
                snip.typeID = 3; //VIDEO
                snip.snippet = "video";

            }
            else
            if (sVal.IndexOf("<DIV") != -1 || sVal.IndexOf("<div") != -1)
            {
                snip.typeID = 5; //random
                snip.snippet = "div container";

            }

            return snip;
        }


		#region db utils


		public string build_OLEDB_ConnectionString(string asUser,
			string asPwd,
			string asCatalog,
			string asURL,
			string asDBprovider)
		{

			string lsTempConnectionString = "";

			lsTempConnectionString += "Provider=";
			lsTempConnectionString += asDBprovider;
			lsTempConnectionString += "; ";

			if(asDBprovider.IndexOf("Microsoft.Jet") !=-1)
			{
				//Microsoft Access
				//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application("dbPath") & ";

				lsTempConnectionString += "Data Source=";
				lsTempConnectionString += asURL + "\\" + asCatalog;
				lsTempConnectionString += ";";

			}
			else if(asDBprovider.IndexOf("SQLOLEDB") !=-1)
			{
				//Microsoft MSSQL
				//"Provider=SQLOLEDB.1; Network Library=dbmssocn;Password=tulum2000;User ID=sa;Initial Catalog="&dbName&";Data Source="&dbIP&";"
				lsTempConnectionString += "Network Library=dbmssocn";
				lsTempConnectionString += ";";
				lsTempConnectionString += "Password=";
				lsTempConnectionString += asPwd;
				lsTempConnectionString += ";";
				lsTempConnectionString += "User ID=";
				lsTempConnectionString += asUser;
				lsTempConnectionString += ";";
				lsTempConnectionString += "Initial Catalog=";
				lsTempConnectionString += asCatalog;
				lsTempConnectionString += ";";
				lsTempConnectionString += "Data Source=";
				lsTempConnectionString += asURL;
				lsTempConnectionString += ";";
			}






			return lsTempConnectionString;

		}
		public string build_SqlClient_ConnectionString(string asUser,
			string asPwd,
			string asCatalog,
			string asURL,
			int timeout)
		{

			//"Persist Security Info=False;Password=tulum2000;User ID=sa;database=master;
			//server="+this.txtIP.Value+";Connect Timeout=15";

			string lsTempConnectionString = "";


			lsTempConnectionString += "Persist Security Info=False;";
			lsTempConnectionString += "Password=";
			lsTempConnectionString += asPwd;
			lsTempConnectionString += ";";
			lsTempConnectionString += "User ID=";
			lsTempConnectionString += asUser;
			lsTempConnectionString += ";";
			lsTempConnectionString += "database=";
			lsTempConnectionString += asCatalog;
			lsTempConnectionString += "; ";
			lsTempConnectionString += "server=";
			lsTempConnectionString += asURL;
			lsTempConnectionString += "; ";
			lsTempConnectionString += "Connect Timeout=";
			lsTempConnectionString += timeout;
			lsTempConnectionString += "; ";

			return lsTempConnectionString;

		}




		public string escapeSQLreservedWords(string s)
		{
			string ls = s;

			if(s.ToUpper() == "ADD")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ALL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ALTER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "AND")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ANY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "AS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ASC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "AUTHORIZATION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BACKUP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BEGIN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BETWEEN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BREAK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BROWSE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BULK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "BY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CASCADE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CASE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CHECK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CHECKPOINT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CLOSE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CLUSTERED")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "COALESCE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "COLLATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "COLUMN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "COMMIT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "COMPUTE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CONSTRAINT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CONTAINS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CONTAINSTABLE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CONTINUE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CONVERT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CREATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CROSS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURRENT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURRENT_DATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURRENT_TIME")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURRENT_TIMESTAMP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURRENT_USER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "CURSOR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DATABASE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DBCC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DEALLOCATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DECLARE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DEFAULT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DELETE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DENY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DESC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DISK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DISTINCT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DISTRIBUTED")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DOUBLE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DROP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DUMMY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "DUMP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ELSE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "END")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ERRLVL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ESCAPE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "EXCEPT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "EXEC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "EXECUTE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "EXISTS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "EXIT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FETCH")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FILE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FILLFACTOR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FOR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FOREIGN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FREETEXT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FREETEXTTABLE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FROM")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FULL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "FUNCTION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "GOTO")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "GRANT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "GROUP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "HAVING")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "HOLDLOCK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IDENTITY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IDENTITY_INSERT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IDENTITYCOL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IF")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "INDEX")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "INNER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "INSERT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "INTERSECT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "INTO")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "IS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "JOIN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "KEY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "KILL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "LEFT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "LIKE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "LINENO")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "LOAD")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NATIONAL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NOCHECK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NONCLUSTERED")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NOT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NULL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "NULLIF")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OF")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OFF")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OFFSETS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ON")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPEN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPENDATASOURCE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPENQUERY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPENROWSET")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPENXML")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OPTION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ORDER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OUTER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "OVER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PERCENT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PLAN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PRECISION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PRIMARY")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PRINT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PROC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PROCEDURE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "PUBLIC")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RAISERROR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "READ")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "READTEXT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RECONFIGURE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "REFERENCES")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "REPLICATION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RESTORE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RESTRICT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RETURN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "REVOKE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RIGHT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ROLLBACK")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ROWCOUNT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "ROWGUIDCOL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "RULE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SAVE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SCHEMA")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SELECT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SESSION_USER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SET")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SETUSER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SHUTDOWN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SOME")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "STATISTICS")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "SYSTEM_USER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TABLE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TEXTSIZE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "THEN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TO")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TOP")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TRAN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TRANSACTION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TRIGGER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TRUNCATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "TSEQUAL")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "UNION")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "UNIQUE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "UPDATE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "UPDATETEXT")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "USE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "USER")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "VALUES")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "VARYING")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "VIEW")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WAITFOR")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WHEN")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WHERE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WHILE")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WITH")
				ls = "[" + s + "]";
			else
				if(s.ToUpper() == "WRITETEXT")
				ls = "[" + s + "]";



			return ls;

		}



		public string getStringValue(string asColName,System.Data.OleDb.OleDbDataReader aoReader)
		{
			string lsOutput = null;
			
			try
			{
				
				lsOutput = (string) aoReader[asColName];
			}
			catch(Exception ex)
			{
                string msg = ex.Message;
				lsOutput = "null";
			}
			return lsOutput;
		}

		public int getIntValue(string asColName,System.Data.OleDb.OleDbDataReader aoReader)
		{
			int lnOutput = 0;
			
			try
			{
				
				lnOutput = (int) aoReader[asColName];
			}
			catch(Exception ex)
			{
                string msg = ex.Message;
				lnOutput = -1;
			}
			return lnOutput;
		}


		public OleDbCommand getCMD(string sCONN)
		{

			OleDbConnection dbCon = new OleDbConnection(sCONN);
			OleDbCommand CMD = dbCon.CreateCommand();
			dbCon.Open();

			return CMD;
		}
		
		public OleDbHelper getDBcmd(string asConnectionString)
		{

			if(asConnectionString == null || asConnectionString.Trim().Length==0)
				throw new Exception("Empty database connection string");

			OleDbHelper odbs = new OleDbHelper();

			odbs.connection = new System.Data.OleDb.OleDbConnection();
			odbs.connection.ConnectionString = asConnectionString;

			try
			{
				odbs.connection.Open();
				odbs.cmd = odbs.connection.CreateCommand();

				return odbs;
			} 
			catch (Exception exp)
			{
				odbs.connection.Close();
				odbs.connection = null;
				odbs.cmd = null;
				odbs = null;
				throw new Exception("Error connecting to database " + exp.Message);
			}
		}

		/// <summary>
		/// simulates @@IDENTITY 
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="strFieldName"></param>
		/// <param name="asConnectionString"></param>
		/// <returns>MAX id from table</returns>
		public int lastRecordID(string strTableName, string strFieldName,string asConnectionString)
		{
			string Query = "";
			int iLastRecordID = 0;

			Query += "SELECT MAX(" + strFieldName + ") from " + strTableName ;
			OleDbHelper lDBhelper = this.getDBcmd(asConnectionString);
			lDBhelper.cmd.CommandText = Query;
			OleDbDataReader dr1 = lDBhelper.cmd.ExecuteReader();

			try
			{
				if(dr1.HasRows)
				{
	
					dr1.Read();
					iLastRecordID = (int) dr1.GetValue(0);

				}

			}
			catch(Exception ex)
			{
				throw new Exception("error getting last identity value " + ex.Message + " SQL: " + Query);
			}

			dr1.Close();
			lDBhelper.cleanup();
			lDBhelper = null;

			return iLastRecordID;
		}


		/// <summary>
		/// escape single quotes, encodes CRLF
		/// </summary>
		/// <param name="asString"></param>
		/// <returns></returns>
		public string prepSQLstring(string asString)
		{
			
			if(asString == null)
				throw new Exception("Null string passed into prepSQLstring()");
			
			char[] acString = asString.ToCharArray();
			string sOutput = "";
			for(int i=0;i<acString.Length;i++)
			{
				if(acString[i] ==39)
				{
					sOutput += "''";
				}
				else
					if(acString[i] ==34)
				{
					sOutput += "\"\"";
				}
				else
					if(acString[i] ==10)
				{
					sOutput += "[lf]";
				}
				else
					if(acString[i] ==13)
				{
					sOutput += "[cr]";
				}
				else
				{
					sOutput += acString[i];
				}
			}

			return sOutput;

		}
		public string prepSQLstring2(string asString)
		{
			
			if(asString == null)
				throw new Exception("Null string passed into prepSQLstring()");
			
			char[] acString = asString.ToCharArray();
			string sOutput = "";
			for(int i=0;i<acString.Length;i++)
			{
				if((acString[i] ==39) && ((i+1) < acString.Length) && (acString[i+1] != 39) && (acString[i-1] != 39))
				{
					sOutput += "''";
				}
				else
					if((acString[i] ==34) && ((i+1) < acString.Length) && (acString[i+1] != 34) && (acString[i-1] != 34))
				{
					sOutput += "\"\"";
				}
				else
					if(acString[i] ==10)
				{
					sOutput += "[lf]";
				}
				else
					if(acString[i] ==13)
				{
					sOutput += "[cr]";
				}
				else
				{
					sOutput += acString[i];
				}
			}

			return sOutput;

		}

		public string sqlHack(string s)
		{
			if(s==null||s.Trim().Length==0)
			{
				return " ";
			}
			else
			{
				return s;
			}
		}	

		public string nullChecker(string asIn,bool abEmptyAsNull)
		{
			string s = null;
			if(asIn == null)
			{
				//do nothing
			}
			else
				if(asIn == "null")
			{
				//do nothing
			}
			else
			{

				if(abEmptyAsNull)
				{
					if(asIn.Trim().Length==0)
					{
						//do nothing
					}
					else
					{
						s = asIn;
					}

				}
				else
				{
					s = asIn;
				}


			}

			return s;
		}

		/// <summary>
		/// if sql null or empty, returns 0 else returns intValue
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public int sqlZeroIntIfNull(object s)
		{
			int ret = 0;

			if(sqlNullIfEmpty(s) == "null")
			{
				//do nothing
			}
			else
			{
				try
				{
					ret = System.Convert.ToInt32(s);
				}
				catch(Exception ex)
				{
                    string msg = ex.Message;
					throw new Exception("sqlZeroIntIfNull() failed , could not convert " + s.ToString() + " to integer value");
				}
			}

			return ret;

		}


		public string  EmptyIfsqlNull(object s)
		{
			
			if(s is System.DBNull) return "";
			if(s.ToString().ToLower() == "null") return "";

			string tmp = s.ToString().Trim();
			if(tmp.Length==0)
				return "";
			else
				return s.ToString();
		}
		public string  sqlNullIfEmpty(object s)
		{
			
			if(s is System.DBNull) return "null";

			string tmp = s.ToString().Trim();
			if(tmp.Length==0)
				return "null";
			else
//				return "'" + s + "'";
			return s.ToString();
		}


		
		public string prepSQLdoubleQuote(string asString)
		{
			
			if(asString==null)
				return "null";

			StringBuilder sbOutput = new StringBuilder();

			char[] acString = asString.ToCharArray();
				
			for(int i=0;i<acString.Length;i++)
			{
				if( acString[i] ==34 )
				{
					sbOutput.Append("'");
				}
				else
				{
					sbOutput.Append( acString[i] );
				}
			}

			return sbOutput.ToString();

		}



		public string prepSQLsingleQuote(string asString)
		{
			
			if(asString==null)
				return "null";

			string sOutput = "";

			char[] acString = asString.ToCharArray();
				
			for(int i=0;i<acString.Length;i++)
			{
				if((acString[i] ==39) && ((i+1) < acString.Length) && (acString[i+1] != 39) && (acString[i-1] != 39))
				{
					sOutput += "''";
				}
				else
				{
					sOutput += acString[i];
				}
			}

			return sOutput;

		}


		public string unprepSQLcrlf(string s)
		{
			//Regex	reg = new Regex(pat);
			//[cr][lf]
			string ret = Regex.Replace(s, @"\[cr\]\[lf\]", "\r\n");
			ret = Regex.Replace(ret, "\\\"\\\"", "\"");
			ret = Regex.Replace(ret, @"\[amp\]", "&");
			return ret;

		}


        public string stripWhiteSpace(string asString)
        {
          
            char[] acString = asString.ToCharArray();
            string sOutput = "";
            for (int i = 0; i < acString.Length; i++)
            {

                if (acString[i] == 10)
                {
                    sOutput += "";
                }
                else
                    if (acString[i] == 13)
                    {
                        sOutput += "";
                    }
                    else
                    {
                        sOutput += acString[i];
                    }
            }

            return sOutput;
        }


		public string prepSQLcflf(string asString)
		{
			if(asString == null)
				throw new Exception("Null string passed into prepSQLcflf()");
			
			char[] acString = asString.ToCharArray();
			string sOutput = "";
			for(int i=0;i<acString.Length;i++)
			{
					
				if(acString[i] ==10)
				{
					sOutput += "[lf]";
				}
				else
					if(acString[i] ==13)
				{
					sOutput += "[cr]";
				}
				else
				{
					sOutput += acString[i];
				}
			}

			return sOutput;
		}

		public string prepSQL2(string s)
		{

			//IDEA HERE IS TO RETURN A STRING PADDED WITH SINQLE
			//QUOTES IF NOT NULL
			StringBuilder sbOut = new StringBuilder();
			if (s == null)
			{
				sbOut.Append("null");
			}
			else
			{
				sbOut.Append("'");
					
				sbOut.Append( sqlNullIfEmpty(prepSQLcflf( prepSQLsingleQuote (s))) );

				sbOut.Append("'");
			}


			return sbOut.ToString();
		}

		public string padAndPrepSQL(object o)
		{
			string ls = null;

			try
			{
				ls = o.ToString();
				ls = prepSQLstring(ls);
			}
			catch(Exception ex)
			{
                string msg = ex.Message;
				//string is probably null
				ls = "";
			}

			return sqlNullIfEmpty(ls);

		}




		//some new functions to execute sql statements


/// <summary>
/// executes SQL statement non-query 
/// </summary>
/// <param name="cmd">pass oledbcommand</param>
/// <param name="asSQL">sql statement</param>
/// <param name="abThrowExceptions">true/false</param>
/// <returns></returns>
		public int exec_nonQuery(OleDbCommand cmd,string asSQL,bool abThrowExceptions,bool abCleanupConn)
		{
			int lnNumRowsAffected = -1;
			try
			{
				cmd.CommandText = asSQL;
				lnNumRowsAffected = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				if(abThrowExceptions)
					throw new Exception("Error executing query " + ex.Message + " " + ex.Message);
				else
					lnNumRowsAffected = -67;
			}
			finally
			{
				if(abCleanupConn)
				{
					cmd.Connection.Close();
					cmd.Dispose();
				}
			}

			return lnNumRowsAffected;

		}
		/// <summary>
		/// executes SQL statement non-query
		/// </summary>
		/// <param name="asSQL">sql statement</param>
		/// <param name="abThrowExceptions">true/false</param>
		/// <returns>number of rows affected, 0 if none or if errors are suppressed</returns>
		public int exec_nonQuery(string asConnectionString, string asSQL,bool abThrowExceptions,bool abCleanupCommand)
		{
		
			int lnNumRowsAffected = -1;
			OleDbHelper dbh = null;

			try
			{
				dbh = this.getDBcmd(asConnectionString);

			}
			catch (Exception ex)
			{
				throw new Exception("Error getting db cmd " + ex.Message + " " + asConnectionString);
			}

			try
			{

				lnNumRowsAffected =
					this.exec_nonQuery(dbh.cmd,asSQL,abThrowExceptions,abCleanupCommand);

			}
			catch (Exception ex)
			{
				if(abThrowExceptions)
					throw new Exception("Error exec_nonQuery() " + ex.Message + " " + asSQL);
				else
					lnNumRowsAffected = -67;

			}
			finally
			{
				if(abCleanupCommand)
					dbh.cleanup();

			}
		

			return lnNumRowsAffected;
		}


		/// <summary>
		/// executes SQL query and returns dataset in HTML table format
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public string exec_Query_HTML(OleDbCommand cmd,string s)
		{
			
			OleDbDataReader dr = null;
			string lsOut=null;

			try
			{
				cmd.CommandText = s;
				dr=cmd.ExecuteReader();
				

				if(dr.HasRows)
				{
					int lnCols = dr.FieldCount;
					string colRow = "";
					string regRow = "";
					string tmpRegRow = "";
					int lnRows = 0;


					while(dr.Read())
					{
						lnRows++;
						tmpRegRow="";

						//loop columns
						for(int i = 0;i<lnCols;i++)
						{
							if(lnRows==1)
							{
								//first row print col headers
								colRow += "<td>";
								colRow += dr.GetName(i);
								colRow += "</td>";
							}

							tmpRegRow += "<td>";
							tmpRegRow += System.Convert.ToString( dr.GetValue(i) ) ;
							tmpRegRow += "</td>";

						}

						if(lnRows ==1)
						{
							colRow = "<tr style='font-weight:bold;'>"+colRow+"</tr>";
						}

						regRow += "<tr style='font-weight:normal;'>" + tmpRegRow + "</tr>";





					}
					lsOut = "<table cellpadding=0 cellspacing=1 border=1>" + colRow + regRow + "</table>";
							
									}
				else
				{
					lsOut = "<b>no rows returned from query " + s + "</b>";
				}
				
			}
			catch(Exception exp)
			{
				lsOut = "error executing SQL " + s + "<p>" + exp.Message + "</p>";
			}
			finally
			{
				if(dr!=null) dr.Close();
			}

			return lsOut;
		}










		#endregion
		#region general




		public ArrayList readValuesIntoArrayList(string path)
		{
			ArrayList tmpA = new ArrayList();
			StreamReader reader = new StreamReader(path);
			bool hasLines = true;
			string tmpLine = null;

			while( hasLines )
			{
				tmpLine = reader.ReadLine();
				if(tmpLine == null)
					break;
				else
				{
					//ignore blank lines and lines with -- comment

					if( tmpLine.Trim().Length == 0 || tmpLine.Substring(0,2) == "--" )
					{
						//do nothing
					}
					else
					{
						tmpA.Add(tmpLine);
					}
				}
			}

			return tmpA;
			


		}

		public string GetText(string fileName)
		{
			StreamReader reader = new StreamReader(fileName);
			return reader.ReadToEnd();
		}



/// <summary>
/// JavaScript specific character escape sequence
/// </summary>
/// <param name="s"></param>
/// <returns>JavaScript safe string</returns>
		public string cleanStringForJavaScript(string s)
		{

			//when passing error messages to JavaScript, need to escape 
			// CR/LF
			// BACKSLASHES
			// ;
			// '
			// "
			//REF http://html.megalink.com/programmer/jstut/jsTabChars.html
			//REF http://www.jimprice.com/ascii-0-127.gif

			char[] acString = s.ToCharArray();
			
			string sOutput = null;

			for (int i = 0; i<acString.Length ; i++)
			{

				if(acString[i] ==39) //single quote
				{
					sOutput += "\\'";
				}
				else
					if(acString[i] ==34) //double quote
				{
					sOutput += "\\\"";
				}
				else
					if(acString[i] ==59) //semi colon
				{
					sOutput += "\\;";
				}
				else
					if(acString[i] ==92) //back slash
				{
					sOutput += "\\\\";
				}	
				else
					if(acString[i] ==10) //LF
				{
					sOutput += "\\n";
				}	
				else
					if(acString[i] ==13) //CR
				{
					sOutput += "\\r";
				}				
				else
				{
					sOutput += acString[i];
				}				

			
			}

	return sOutput;



		}

		public string getDateStamp()
		{
			
			return System.DateTime.Now.ToString();

		}

		public string getShortDateStamp()
		{
			return System.DateTime.Now.Date.ToShortDateString();
		}
		public NameValueCollection parseQuery(string RawUrl)
		{
			//parse 
			NameValueCollection nvc = new NameValueCollection(); 

			string[] aRawUrl = RawUrl.Split('?');

			if(aRawUrl.Length==1)
			{
				return nvc;
			}

			string queryParams = aRawUrl[1];
			
			string[] tuples = queryParams.Split('&');

			string[] tuple;
			for(int i=0;i<tuples.Length;i++)
			{
				tuple = tuples[i].Split('=');
				nvc.Add(tuple[0],tuple[1]);

			}
			return nvc;

		}

		
		public void rename(string path, string oldName, string newName)
		{
			try
			{
				if(this.FileExists(path + "\\" + newName))
				{
					this.del(path + "\\" + newName);
				}
				
				System.IO.File.Move(path + "\\" + oldName,path + "\\" + newName);
			}
			catch(Exception ex)
			{
				throw new Exception("File rename unsuccessful " + ex.Message);
			}
		}




		public void DeleteDir(string sPath,bool bRecurse)
		{
			if (System.IO.Directory.Exists(sPath))
				System.IO.Directory.Delete(sPath,bRecurse);
			else
				throw new Exception("Directory ["+sPath+"] does not exist");
			
		}





		/// <summary>
		/// deletes a single file
		/// </summary>
		/// <param name="path">path to a single file</param>
		public void del(string path)
		{
			try
			{
				System.IO.File.Delete(path);
			}
			catch(Exception ex)
			{
				throw new Exception("File Delete unsuccessful " + ex.Message);
			}
		}

		public void mkdir(string path)
		{
			try
			{
				System.IO.Directory.CreateDirectory(path);
			}
			catch(Exception ex)
			{
				throw new Exception("Unable to create directory " + path + " - " + ex.Message);
			}
		}
		
		

		public bool FileExists(string path)
		{
			if(System.IO.File.Exists(path))
				return true;
			else
				return false;

		}
		public bool DirExists(string path)
		{
			if(System.IO.Directory.Exists(path))
				return true;
			else
				return false;

		}	
	

		/// <summary>
		/// get file name off a windows local file path
		/// eg c://apps/test/log.txt
		/// </summary>
		/// <param name="sInput"></param>
		/// <param name="bRemoveUnderscores"></param>
		/// <param name="bPreserveExtension"></param>
		/// <returns></returns>
		public string ParseFileNameOffPath(
			string sInput,
			bool bRemoveUnderscores,
			bool bPreserveExtension
			)
		{


			string sNameParse = "";
			
			//c:\\apps\dev\babu.html

			//first split the path on the file separator

			string[] aPathParts = sInput.Split('\\');

			//file name is in the last bucket

			sNameParse = aPathParts[aPathParts.Length-1];

			if(bPreserveExtension==false)
			{
				//remove extension
				string[] aFileNameParts = sNameParse.Split('.');
				sNameParse = aFileNameParts[aFileNameParts.Length-1];
			}

			if(bRemoveUnderscores)
			{
				//replace underscores with spaces
				sNameParse = sNameParse.Replace("_"," ");
			}


			return sNameParse;
		}


		#endregion


		public string[] getStrings(string[] unsortedArray,int[] index,string replace,string with)
		{
			string[] tmp = this.getStrings(unsortedArray,index);
			foreach(string s in tmp)
			{
				s.Replace(replace,with);
			}

			return tmp;
		}

		public double[] getDoubles(double[] unsortedArray,int[] index)
		{
			double[] tmp = new double[unsortedArray.Length];

			for(int i = 0;i<index.Length;i++)
			{
				tmp[i] = unsortedArray[ index[i] ];
			}

			return tmp;
		}
		public string[] getStrings(string[] unsortedArray,int[] index)
		{
			string[] tmp = new string[unsortedArray.Length];

			for(int i = 0;i<index.Length;i++)
			{
				tmp[i] = unsortedArray[ index[i] ];
			}

			return tmp;
		}



		public string convArrayToString(string[] arr, char separator)
		{
			string str="";
			for(int i=0; i<arr.Length; i++)
			{
				str+=arr[i];
				if (i < (arr.Length-1)) str += separator;
			}
			return(str);

		}
		public string convArrayToString(string[] arr)
		{

			return(convArrayToString(arr, '|'));

		}
		public object convStringToArray(string str, string sType, char separator )
		{
			string[] laVals = str.Split(separator);

			if (sType == "int")
			{
				int []arr = new int[laVals.Length] ;

				for (int i=0; i<laVals.Length; i++)
					arr[i] = System.Convert.ToInt32(laVals[i]);
				return (arr);
			}
			else if(sType == "double")
			{
				double []arr = new double[laVals.Length] ;

				for (int i=0; i<laVals.Length; i++)
					arr[i] = System.Convert.ToDouble(laVals[i]);
				return(arr);
			}
			else if(sType == "string")
			{

				return(laVals);
			}

			return(0);
		}
		public object convStringToArray(string str, string sType)
		{
			return(convStringToArray(str, sType, '|'));
		}



		public int getInitYear(int startmonth)
		{
			int res=0;
			DateTime now = DateTime.Now;
			if(now.Month < startmonth)
			{
				res=now.Year;
			}
			else
			{
				res=1+now.Year;
			}
			if(startmonth==0)res = now.Year;
			return res;
		}

		public string getNumMask(int numdec)
		{
			string NumMask = "#,##0";
				if(numdec>0)
				{
					NumMask += ".";
					for(int i=0;i<numdec;i++)
					{
						NumMask+="0";
					}
				}
			NumMask = NumMask + ";(" + NumMask + ")";
			return NumMask;
		}

		public double max_dbl(double x, double y)
		{
			if (x<y)
			{
				return y;
			}
			return x;
		}

		public double min_dbl(double x, double y)
		{
			if (x>y)
			{
				return y;
			}
			return x;
		}

		public int max_int(int x, int y)
		{
			if (x<y)
			{
				return y;
			}
			return x;
		}

		public int min_int(int x, int y)
		{
			if (x>y)
			{
				return y;
			}
			return x;
		}

		public string FiscalToCalendar(double fiscal,int startmonth,int showmonth)
		{
			double year=Math.Floor(fiscal);
			double month=fiscal - year + startmonth/12; //round((fiscal-year),2);
			double yr= year-1+Math.Floor(month);
			month=month-Math.Floor(month);
			string  mon="";
			if(month < (.5/12)) {mon="January, " + yr;}
			else if(month < (1.5/12)){mon="February, " + yr;}
			else if(month < (2.5/12)){mon="March, " + yr;}
			else if(month < (3.5/12)){mon="April, " + yr;}
			else if(month < (4.5/12)){mon="May, " + yr;}
			else if(month < (5.5/12)){mon="June, " + yr;}
			else if(month < (6.5/12)){mon="July, " + yr;}
			else if(month < (7.5/12)){mon="August, " + yr;}
			else if(month < (8.5/12)){mon="September, " + yr;}
			else if(month < (9.5/12)){mon="October, " + yr;}
			else if(month < (10.5/12)){mon="November, " + yr;}
			else if(month < (11.5/12)){mon="December, " + yr;}
			else {mon="January, " + yr;}
	
	//		if(showmonth == 0){mon=Convert.ToString(fiscal);}
			if(showmonth == 0){mon=fiscal.ToString("####.##");}
			if(fiscal == 0){mon="";}
		//	if(fiscal == undefined){mon="";}
			return mon;
		}

        public System.Guid getGUID()
        {
            System.Guid guid = Guid.NewGuid();
            return guid;
        }


        public string encode64(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in encode64" + e.Message);
            }
        }

        public string decode64(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in decode64" + e.Message);
            }
        }

        
    }
}

    