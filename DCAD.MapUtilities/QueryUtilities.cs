#region CLASS DOCUMENTATION
/*
 * Project:            DCAD.MapUtilities (DCADGIS Library)
 * Class:              QueryUtilities.cs
 * Author:             Craig Browne
 * Date Created:       07/07/2021
 * Target Application: ArcGIS Pro 2.6 +
 * Dependency:         None
 * Revisions:          mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Supporting methods that are used by the DCAD query tool to access geo-databases and query
 * them with a SQL definition.
 * 
 * **************************************************************************************************
 */
#endregion

using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System;

namespace DCAD.MapUtilities
{
    public class QueryUtilities
    {
        #region DECLARED VARIABLES
        public static string gis = "DCADGIS";
        public static string prod = "DCADGIS";
        public static string dev1 = "DCADSQLDV";
        public static string dev2 = "DCADVM02";
        public static string dev3 = "DCADZOEY";
        #endregion

        #region CreateConnString()- Creates a SQL ConnectionString needed for SQL querying. (WORKING)
        /// <summary>This code will create a SQL connection string from input parameters.</summary>
        /// <param name="instance">A string that represents the SQL Server Instance for the connection to be established.</param>
        /// <param name="database">A string that represents the name of the database to which the connection will be established.</param>
        /// <remarks>This code depends on other snippets that create a SQL connection. The returned SqlConnectionStringBuilder object must be converted to a string
        ///          object using the .ConnectionString() property. This function returns a SQL connection string builder object for connecting to a SQL database.
        ///          Gleaned from: http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnectionstringbuilder(v=vs.100).aspx</remarks>
        /// <returns>builder</returns>
        public static SqlConnectionStringBuilder CreateConnString(string instance, string database)
        {
            var builder = new SqlConnectionStringBuilder();

            try
            {
                builder["Data Source"] = instance;
                builder["Initial Catalog"] = database;
                builder["Integrated Security"] = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Create Connection String", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return builder;
        }
        #endregion

        #region CreateSQLDataTable()- Creates in-memory SQL data table to hold a set of rows from a SQL query. (WORKING)
        /// <summary> Create an in-memory table with rows from a parent table using a SQL statement.</summary>
        /// <param name="sqlConn">A SQL connection to be used.</param>
        /// <param name="sqlCmd">The SQL statement to constrain the results</param>
        /// <param name="dataTableName">The original table the data will import from.</param>
        public static DataTable CreateSQLDataTable(SqlConnection sqlConn, SqlCommand sqlCmd, string dataTableName)
        {
            // Create a SQL data adapter variable with a SQL connection & SQL statement
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd.CommandText, sqlConn);

            // Set a variable to hold the new table data from the original table parameter
            DataTable sqlDataTable = new DataTable(dataTableName);

            try
            {
                // Populate the new table with data from the original table
                int numFilledRows = sqlDataAdapter.Fill(sqlDataTable);

                // Confirm if new table was populated
                if (numFilledRows < 1)
                {
                    //return a message if no results are returned from a query.
                    System.Windows.Forms.MessageBox.Show("Your query produced no results.  Please confirm that the correct layer has been selected for your query and try again, if you feel this is in error.",
                                    "No Results Returned", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    SqlException sqlexc = (SqlException)ex;
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Create SQL Data Table", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Create SQL Data Table", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            return sqlDataTable;
        }
        #endregion

        #region GetSearchResultsArray()- Creates an array from a layer that matches user input. (WORKING)
        /// <summary>
        /// Takes the input parameters to search a specific SQL statement on a specific layer and adds results into an array list to populate the list box.
        /// </summary>
        /// <param name="inputText">The user's input from the text box.</param>
        /// <param name="fType">Either the gedt fabric type or the gpub type layer to be queried, set by the radio button selected in the docked window.</param>
        /// <param name="gisGDB">Either the gedt or gpub database to be queried.</param>
        /// <returns>array list to populate the list box.</returns>
        public static ArrayList GetSearchResultsArray(string userInput, int fType, string gisGDB)
        {
            ArrayList resultArray = new ArrayList();

            try
            {
                var srcSQLString = CreateConnString(gis, gisGDB);
                SqlCommand sqlCmd;

                DataTable tmpTbl = null;

                using (SqlConnection sourceConn = new SqlConnection(srcSQLString.ConnectionString))
                {
                    if (fType == 1) //Account Number
                    {
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([Account_Num])), LTRIM(RTRIM([Owner_Name1])), LTRIM(RTRIM([Legal1])), LTRIM(RTRIM([Legal2])) " +
                                                "FROM GPUB.ADM.PARCEL_VIEW pv WHERE pv.Division <> 'BPP' AND pv.Account_Num LIKE '%" + userInput + "%' ORDER BY [Account_Num]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 2) //Owner Name
                    {
                        //Parcel View query for DCAD owner name (line 1).
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([Account_Num])), LTRIM(RTRIM([Owner_Name1])), LTRIM(RTRIM([Legal1])), LTRIM(RTRIM([Legal2])) " +
                                                "FROM GPUB.ADM.PARCEL_VIEW pv WHERE pv.Division <> 'BPP' AND pv.Owner_Name1 LIKE '%" + userInput + "%' ORDER BY [Account_Num]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 3) //Legal Line 1
                    {
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([Account_Num])), LTRIM(RTRIM([Owner_Name1])), LTRIM(RTRIM([Legal1])), LTRIM(RTRIM([Legal2])) " +
                                                "FROM GPUB.ADM.PARCEL_VIEW pv WHERE pv.Division <> 'BPP' AND pv.Legal1 LIKE '%" + userInput + "%' ORDER BY [Account_Num]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 4)
                    {
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([BLKDSGNTR])) FROM GPUB.ADM.BLOCK blk " +
                                                "WHERE blk.BLKDSGNTR LIKE '%" + userInput + "%' AND NAME = 'Dallas' ORDER BY [BLKDSGNTR]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 5)
                    {
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([CNVYNAME])), LTRIM(RTRIM([SOURCEREF])), LTRIM(RTRIM([NAME])) " +
                                                "FROM GPUB.ADM.SimultaneousConveyance sc WHERE sc.CNVYNAME LIKE '%" + userInput + "%' ORDER BY [CNVYNAME]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 6)
                    {
                        sqlCmd = new SqlCommand("SELECT LTRIM(RTRIM([CNVYNAME])), LTRIM(RTRIM([SOURCEREF])), LTRIM(RTRIM([NAME])) " +
                                                "FROM GPUB.ADM.SimultaneousConveyance sc WHERE sc.SOURCEREF LIKE '%" + userInput + "%' ORDER BY [CNVYNAME]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                    else if (fType == 7)
                    {
                        sqlCmd = new SqlCommand("SELECT [Name] FROM GEDT.ADM.ParcelFabric_Parcels pf " +
                                                "WHERE pf.Type = 7 AND pf.Name LIKE '%" + userInput + "%' ORDER BY [Name]");
                        tmpTbl = CreateSQLDataTable(sourceConn, sqlCmd, "sourceTempTable");
                    }
                }

                foreach (DataRow rows in tmpTbl.Rows)
                {
                    foreach (DataColumn col in tmpTbl.Columns)
                    {
                        resultArray.Add(rows[col]);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }

            return resultArray;
        }
        #endregion
    }
}
