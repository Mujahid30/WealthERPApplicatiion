using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;


namespace BoCommon
{
    public class ReadExternalFile
    {
        //reads Data from an Excel File 
        
        public DataSet ReadExcelfile(string FileName)
        {
            DataSet ds = new DataSet();
            //Provider String Extended properties: 
            //"Excel 8.0": Use Excel as source
            //"Header Yes": Header is included in the Excel sheet
            //"IMEX=1": When reading from the excel sheet ignore datatypes and read all data in the sheet.
            //Without setting IMEX=0, the excel reader looks for the datatype in the excel sheet.
            //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"");
            con.Open();
            try
            {
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetname = dt.Rows[0]["TABLE_NAME"].ToString();

                OleDbDataAdapter myCommand = new OleDbDataAdapter(" SELECT * FROM [" + sheetname + "]", con);
                myCommand.Fill(ds);
                
            }
            catch (Exception ex)
            {
                string exce = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }




        public DataSet ReadDBFFile(string filepath, string filename, out string ErrorMessage)
        {
            //string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + "; Extended Properties=DBASE 5.0";
            ErrorMessage = string.Empty;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + "; Extended Properties=DBASE 5.0");
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                OleDbDataAdapter daGetTableData = new OleDbDataAdapter("select *  from " + filename, con);

                //OleDbCommand cmd = new OleDbCommand("Select *  from camsprof",conn);
                daGetTableData.Fill(ds);
            }
            catch (Exception ex)
            {
                string Mesage = ex.Message;
                ErrorMessage = "The file is corrupted. The upload process cannot be proceeded";
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataSet ReadCSVFile(string filepath, string filename)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + "; Extended Properties=\"text;HDR=No;FMT=Delimited\"");
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                OleDbDataAdapter daGetTableData = new OleDbDataAdapter("select *  from " + filename, con);

                //OleDbCommand cmd = new OleDbCommand("Select *  from camsprof",conn);
                daGetTableData.Fill(ds);
            }
            catch (Exception ex)
            {
                string Mesage = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return ds;
        }
    }
}
