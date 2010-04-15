using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AgentStoryComponents.core
{
   public class TupleElements
    {

       public static List<Tuple> getStoryTuples(int storyID,string aSqlConn)
       {
           List<Tuple> tups = new List<Tuple>();

           //call a stored procedure
           System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(aSqlConn);
           sqlConn.Open();


           System.Data.SqlClient.SqlDataAdapter sqlDataAdapter =
               new System.Data.SqlClient.SqlDataAdapter("readTuplesByStory", sqlConn);

           sqlDataAdapter.SelectCommand.CommandTimeout = 800000;

           sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@storyID", SqlDbType.Int, 4));
           sqlDataAdapter.SelectCommand.Parameters["@storyID"].Value = storyID;

           System.Data.SqlClient.SqlDataReader dr = sqlDataAdapter.SelectCommand.ExecuteReader();

           while (dr.Read())
           {
               Tuple t = new Tuple();
               t.id = Convert.ToInt32(dr["id"]);
               t.name = Convert.ToString(dr["name"]);
               t.code = Convert.ToString(dr["code"]);
               t.description = Convert.ToString(dr["description"]);
               t.units = Convert.ToString(dr["units"]);
               if (dr["value"] is DBNull)
                   t.val = null;
               else
                   t.val = Convert.ToString(dr["value"]);

               if (dr["numValue"] is DBNull)
                   t.valNum = -1;
               else
                   t.valNum = Convert.ToDecimal(dr["numValue"]);

               t.guid = new Guid(Convert.ToString(dr["guid"]));

               tups.Add(t);

           }


           dr.Close();
           dr.Dispose();

           sqlDataAdapter.Dispose();

           sqlConn.Close();
           sqlConn.Dispose();


           return tups;
       }




    }


   public class Tuple
   {
       private string _sqlConn = null;
       private utils ute = new utils();

       public int id { get; set; }
       public Guid guid { get; set; }
       public string name { get; set; }
       public string val { get; set; }
       public string description { get; set; }
       public string code { get; set; }
       public string units { get; set; }

       public decimal valNum { get; set; }


       public Tuple()
       {
            
       }
       public Tuple(string aSqlConn)
       {
           this._sqlConn = aSqlConn;
           //empty tuple
       }
       public Tuple(string aSqlConn, int id)
       {
           //load existing by PK
           this._sqlConn = aSqlConn;
           loadFromDB(id);
       }

      
       public Tuple(string aSqlConn, string name, string val)
       {
           this._sqlConn = aSqlConn;
           //new with string val
           this.name = name;
           this.val = val;
           this.save();
       }


       public Tuple(string aSqlConn, string name, decimal val)
       {
           this._sqlConn = aSqlConn;
           //new with num val
           this.name = name;
           this.valNum = val;
           this.save();
       }


       public void save()
       {

           //call a stored procedure
           System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(_sqlConn);
           sqlConn.Open();

           System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = null;

           if (id == 0)
           {
               //insert
               System.Guid g = System.Guid.NewGuid();
               
              sqlDataAdapter =
                  new System.Data.SqlClient.SqlDataAdapter("addTuple", sqlConn);

               sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@name", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@name"].Value = this.name;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@code", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@code"].Value = this.code;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@description", SqlDbType.NVarChar, 254));
               sqlDataAdapter.SelectCommand.Parameters["@description"].Value = this.description;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@units", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@units"].Value = this.units;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@guid", SqlDbType.UniqueIdentifier));
               sqlDataAdapter.SelectCommand.Parameters["@guid"].Value = g;
               if (this.val != null)
               {
                   //string value
                   sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@value", SqlDbType.NText));
                   sqlDataAdapter.SelectCommand.Parameters["@value"].Value = this.val;
               }
               
              
               //numeric value
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@numValue", SqlDbType.Decimal));
               sqlDataAdapter.SelectCommand.Parameters["@numValue"].Value = this.valNum;
           
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@pk", SqlDbType.Int, 4));
               sqlDataAdapter.SelectCommand.Parameters["@pk"].Direction = ParameterDirection.Output;
               sqlDataAdapter.SelectCommand.ExecuteNonQuery();

               int pk = Convert.ToInt32( sqlDataAdapter.SelectCommand.Parameters["@pk"].Value );
               this.id = pk;
               this.guid = g;

           }
           else
           {
               //update
               sqlDataAdapter =
                new System.Data.SqlClient.SqlDataAdapter("updateTuple", sqlConn);

               sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tupleID", SqlDbType.Int, 4));
               sqlDataAdapter.SelectCommand.Parameters["@tupleID"].Value = this.id;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@name", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@name"].Value = this.name;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@code", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@code"].Value = this.code;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@description", SqlDbType.NVarChar, 254));
               sqlDataAdapter.SelectCommand.Parameters["@description"].Value = this.description;
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@units", SqlDbType.NVarChar, 100));
               sqlDataAdapter.SelectCommand.Parameters["@units"].Value = this.units;
              
               if (this.val != null)
               {
                   //string value
                   sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@value", SqlDbType.NText));
                   sqlDataAdapter.SelectCommand.Parameters["@value"].Value = this.val;
               }


               //numeric value
               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@numValue", SqlDbType.Decimal));
               sqlDataAdapter.SelectCommand.Parameters["@numValue"].Value = this.valNum;

               sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rows", SqlDbType.Int, 4));
               sqlDataAdapter.SelectCommand.Parameters["@rows"].Direction = ParameterDirection.Output;
               sqlDataAdapter.SelectCommand.ExecuteNonQuery();

               int rows = Convert.ToInt32(sqlDataAdapter.SelectCommand.Parameters["@rows"].Value);

               if (rows != 1) throw new Exception("UpdateTuple failed");

           }

           sqlDataAdapter.Dispose();

           sqlConn.Close();
           sqlConn.Dispose();


       }

       private void loadFromDB(int tupleID)
       {
           //call a stored procedure
           System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(_sqlConn);
           sqlConn.Open();


           System.Data.SqlClient.SqlDataAdapter sqlDataAdapter =
               new System.Data.SqlClient.SqlDataAdapter("readTuplesByID", sqlConn);

           sqlDataAdapter.SelectCommand.CommandTimeout = 800000;

           sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tupleID", SqlDbType.Int, 4));
           sqlDataAdapter.SelectCommand.Parameters["@tupleID"].Value = tupleID;

           System.Data.SqlClient.SqlDataReader dr = sqlDataAdapter.SelectCommand.ExecuteReader();
           this.load(dr);
           dr.Close();
           dr.Dispose();
           
           sqlDataAdapter.Dispose();

           sqlConn.Close();
           sqlConn.Dispose();
       }

       private void load(System.Data.SqlClient.SqlDataReader dr)
       {
           if (dr.HasRows)
           {
               dr.Read();
               this.id = Convert.ToInt32(dr["id"]);
               this.name = Convert.ToString(dr["name"]);
               this.code = Convert.ToString(dr["code"]);
               this.description = Convert.ToString(dr["description"]);
               this.units = Convert.ToString(dr["units"]);
               if (dr["value"] is DBNull)
                   this.val = null;
               else
                   this.val = Convert.ToString(dr["value"]);

               if (dr["numValue"] is DBNull)
                   this.valNum = -1;
               else
                   this.valNum = Convert.ToDecimal(dr["numValue"]);

               this.guid = new Guid(Convert.ToString(dr["guid"]));
              
           }
       }





       public  int delete()
       {
           System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(_sqlConn);
           sqlConn.Open();

           System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = 
                new System.Data.SqlClient.SqlDataAdapter("deleteTuple", sqlConn);

           sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tupleID", SqlDbType.Int,4));
           sqlDataAdapter.SelectCommand.Parameters["@tupleID"].Value = this.id;
           sqlDataAdapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rows", SqlDbType.Int, 4));
           sqlDataAdapter.SelectCommand.Parameters["@rows"].Direction = ParameterDirection.Output;
           sqlDataAdapter.SelectCommand.ExecuteNonQuery();

           int rows = Convert.ToInt32(sqlDataAdapter.SelectCommand.Parameters["@rows"].Value);

           return rows;

       }
   }


}
