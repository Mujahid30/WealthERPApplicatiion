using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using System.Data.OleDb;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using System.Xml;

namespace ETLSourceFileToXMLConversionRequestProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dsGetHeaderName = new DataSet();

            DataTable dt;
            DataTable dt1;
            string textFilePath = ConfigurationManager.AppSettings["Text_File_Path"];
            string connectionString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand("", connection);
            SqlDataAdapter adapter = new SqlDataAdapter("SP_GetXmlAndMandatoryFields", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@RequestId", SqlDbType.Int, 8));
            adapter.SelectCommand.Parameters["@RequestId"].Direction = ParameterDirection.Output;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 300));
            adapter.SelectCommand.Parameters["@FileName"].Direction = ParameterDirection.Output;
            adapter.Fill(dsGetHeaderName, "dsXmlAndMandatoryFields");
            int requestId = 0;
            string xmlFileUploadName = string.Empty;
            if (adapter.SelectCommand.Parameters[0].Value != DBNull.Value)
            {
                requestId = Convert.ToInt32(adapter.SelectCommand.Parameters[0].Value);
                xmlFileUploadName = adapter.SelectCommand.Parameters[1].Value.ToString();
                dt = dsGetHeaderName.Tables[0];
                dt1 = dsGetHeaderName.Tables[1];

                //Upload XML file.
                int indexOfExtension = xmlFileUploadName.LastIndexOf('.');
                string extension = (xmlFileUploadName.Substring(indexOfExtension + 1)).ToLower();
                try
                {
                    string externalHeader;
                    DataTable dtGetXml = new DataTable();
                    OleDbConnection con;
                    string ErrorMessage;
                    if (extension == "xls" || extension == "xlsx")
                    {

                        con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xmlFileUploadName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"");
                        //Get All data from Xml into Datatable
                        DataSet dsGetXml = new DataSet();
                        try
                        {
                            con.Open();
                            dtGetXml = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetname = dtGetXml.Rows[0]["TABLE_NAME"].ToString();
                            OleDbDataAdapter myCommand = new OleDbDataAdapter(" SELECT * FROM [" + sheetname + "]", con);
                            myCommand.Fill(dsGetXml);
                            dtGetXml = dsGetXml.Tables[0];
                        }

                        catch (Exception ex)
                        {
                            string Message = ex.Message;
                            ErrorMessage = "The file is corrupted. The upload process cannot be proceeded" + Message;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    else if (extension == "dbf")
                    {
                        int indexOfExtension1 = xmlFileUploadName.LastIndexOf('\\');
                        string filename = xmlFileUploadName.Substring(indexOfExtension + 1);
                        string filepath = xmlFileUploadName.Substring(0, indexOfExtension1 - 1);
                        con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "; Extended Properties=DBASE 5.0");
                        DataSet ds = new DataSet();
                        try
                        {
                            con.Open();
                            OleDbDataAdapter daGetTableData = new OleDbDataAdapter("select *  from " + filename, con);

                            //OleDbCommand cmd = new OleDbCommand("Select *  from camsprof",conn);
                            daGetTableData.Fill(ds);
                            int CountCol = 0; //Counting each column of a row in  Table
                            DataRow dr;      //Data row to check which row contains dummy record. 

                            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                            {
                                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                                {
                                    if (ds.Tables[0].Rows[a][i] == DBNull.Value)
                                    {
                                        CountCol++;
                                    }
                                }
                                if (CountCol == ds.Tables[0].Columns.Count)
                                {
                                    dr = ds.Tables[0].Rows[a];
                                    ds.Tables[0].Rows.Remove(dr);
                                }
                                CountCol = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            string Message = ex.Message;
                            ErrorMessage = "The file is corrupted. The upload process cannot be proceeded" + Message;
                        }
                        finally
                        {
                            con.Close();
                        }
                        dtGetXml = ds.Tables[0];



                    }

                    //Replace all header with match case.
                    // DataTable dtGetHeaderName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string nameNotFound = string.Empty;
                    foreach (DataColumn column in dtGetXml.Columns)
                    {
                        externalHeader = column.ColumnName;
                        string externalHeaderMatch = @"WEHXHM_ExternalHeaderName='" + externalHeader + @"'";
                        DataRow[] foundRows;
                        foundRows = dt.Select(externalHeaderMatch);

                        if (foundRows.Length > 0)
                        {
                            string aas = foundRows[0].ItemArray[0].ToString();
                            dtGetXml.Columns[externalHeader].ColumnName = aas;
                        }
                        else
                        {
                            nameNotFound = nameNotFound + externalHeader + ",";
                        }


                    }
                    //Split and Remove Header those not found.
                    string startString = nameNotFound;
                    char[] charSeparators = new char[] { ',' };
                    string[] splitStart = startString.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in splitStart)
                    {
                        dtGetXml.Columns.Remove(item);
                    }
                    //Check Mandatory fields and add column in datatable.

                    string mandatoryColumnMissing = string.Empty;
                    foreach (DataRow rows in dt1.Rows)
                    {
                        if (!dtGetXml.Columns.Contains(rows["WUXHM_XMLHeaderName"].ToString()) && rows["IsMandatory"].ToString() == "1")
                        {
                            mandatoryColumnMissing = mandatoryColumnMissing + rows["WUXHM_XMLHeaderName"].ToString() + ",";
                        }
                        if (!dtGetXml.Columns.Contains(rows["WUXHM_XMLHeaderName"].ToString()) && rows["IsMandatory"].ToString() == "0")
                        {
                            dtGetXml.Columns.Add(rows["WUXHM_XMLHeaderName"].ToString());
                        }
                    }

                    //Create Xml File.
                    string xmlCreationMessage = string.Empty;
                    string xmlFileName = string.Empty;
                    if (mandatoryColumnMissing == string.Empty)
                    {

                        DataTable newDataTable = new DataTable();
                        newDataTable = dtGetXml.Clone();
                        foreach (DataColumn dc in newDataTable.Columns)
                        {

                            dc.DataType = typeof(string);

                        }
                        foreach (DataRow dr in dtGetXml.Rows)
                        {
                            newDataTable.ImportRow(dr);
                        }

                        xmlFileName = ConfigurationManager.AppSettings["XML_FILE_PATH"] + requestId.ToString() + ".xml";
                        newDataTable.WriteXml(xmlFileName, XmlWriteMode.WriteSchema);
                        xmlCreationMessage = "XML Created Successfully";

                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString() + " " + mandatoryColumnMissing + "Columns Missing From External Table");
                        xmlCreationMessage = "Mandatory Columns Missing From External Table";
                    }

                    SqlDataAdapter da = new SqlDataAdapter("SP_UpdateLogForXMLCreation", connection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add(new SqlParameter("@RequestId", SqlDbType.Int, 32));
                    da.SelectCommand.Parameters["@RequestId"].Value = requestId;
                    da.SelectCommand.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 300));
                    if (xmlCreationMessage == "Mandatory Columns Missing From External Table")
                    {
                        da.SelectCommand.Parameters["@FileName"].Value = DBNull.Value;
                        xmlCreationMessage = mandatoryColumnMissing + "-Mandatory Columns Missing From External Table";
                    }
                    else
                        da.SelectCommand.Parameters["@FileName"].Value = xmlFileName;
                    da.SelectCommand.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 300));
                    da.SelectCommand.Parameters["@Message"].Value = xmlCreationMessage;
                    //DataSet ds = new DataSet();
                    da.Fill(dt);

                    WriteLog(xmlCreationMessage, textFilePath);
                }

                catch (Exception ex)
                {
                    string error = ex.Message;
                    WriteLog(error, textFilePath);
                }
                finally
                {


                }
            }
            else
            {
                string msg = "Request Not Exist";
                WriteLog(msg, textFilePath);
            }
        }

        private static void WriteLog(string err, string path)
        {
            // Create a stringbuilder and write the new user input to it.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = =  = = = = = =  = =");
            sb.Append(err);
            sb.AppendLine();
            sb.AppendLine();

            // Open a streamwriter to a new text file named "Log.txt"and write the contents of 
            // the stringbuilder to it. 
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(sb.ToString());
            }

        }
    }
}
