using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;


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
               
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{

                //    if (dr["Exchange"].ToString().Contains("---") && dr["Exchange Type"].ToString().Contains("---")  )
                //    {
                //        //dr.Table.Rows[0].Delete();
                //        ds.Tables[0].Rows.Remove(dr);
                //        if (string.IsNullOrEmpty(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString().Trim()) && string.IsNullOrEmpty(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][1].ToString().Trim()))
                //        {
                //            DataRow dr1 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                //            ds.Tables[0].Rows.Remove(dr1);                            
                //        }
                //        break; 
                //    }
                    
                //    //if (dr["Exchange"].ToString().Contains("   ") && dr["Exchange Type"].ToString().Contains("   "))
                //    //{
                //    //    //dr.Table.Rows[0].Delete();
                //    //    ds.Tables[0].Rows.Remove(dr);

                //    //}
                    
                //}
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
                string exce = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public DataSet ReadTxtFile(string FileName, string filetype)
        {
             DataSet domains = new DataSet();
             DataRow drOdin;
             string delimeter;
             if (filetype == "NSE")
             {
                  delimeter = ",";
             }
             else
             {
                  delimeter = "|";
             }
             //domains.Tables.Add(tableName);
          
        if (File.Exists(FileName))
        {
            StreamReader reader = new StreamReader(FileName);
           
             
            //now we need to read the rest of the text file
            string data = reader.ReadToEnd();
            //now we will split the file on the carriage return/line feed
            //and toss it into a string array
            DataTable dtOdin = new DataTable();
            dtOdin.Columns.Add("Col1");
            dtOdin.Columns.Add("Col2");
            dtOdin.Columns.Add("Col3");
            dtOdin.Columns.Add("Col4");
            dtOdin.Columns.Add("Col5");
            dtOdin.Columns.Add("Col6");

            dtOdin.Columns.Add("Col7");
            dtOdin.Columns.Add("Col8");
            dtOdin.Columns.Add("Col9");
            dtOdin.Columns.Add("Col10");
            dtOdin.Columns.Add("Col11");
            dtOdin.Columns.Add("Col12");

            dtOdin.Columns.Add("Col13");
            dtOdin.Columns.Add("Col14");
            dtOdin.Columns.Add("Col15");
            dtOdin.Columns.Add("Col16");
            dtOdin.Columns.Add("Col17");
            dtOdin.Columns.Add("Col18");

            dtOdin.Columns.Add("Col19");
            dtOdin.Columns.Add("Col20");
            dtOdin.Columns.Add("Col21");
            dtOdin.Columns.Add("Col22");
            dtOdin.Columns.Add("Col23");
            dtOdin.Columns.Add("Col24");
            dtOdin.Columns.Add("Col25");
          
            string[] rows = data.Split("\r".ToCharArray());
            //now we will add the rows to our DataTable
            foreach (string r in rows)
            {
                string[] items = r.Split(delimeter.ToCharArray());
                //split the row at the delimiter
                //domains.Tables[0].Rows.Add(items);                
                drOdin = dtOdin.NewRow();
                
                for (int i = 0; i < items.Count(); i++)
                {
                    if (filetype == "NSE")
                    {
                        if (i == 19 || i == 20 || i==24)
                        {
                            drOdin[i] = DateTime.Parse(items[i]);

                        }
                        else
                            drOdin[i] = items[i];
                    }
                   else if (filetype == "BSE")
                    {
                        //if (i == 9)
                        //{
                        //    drOdin[i] = DateTime.Parse(items[i]);

                        //}
                        //else
                            drOdin[i] = items[i];
                    }

                   
                    
 
                }
                dtOdin.Rows.Add(drOdin);                
                
            }

            domains.Tables.Add(dtOdin);
            int count = domains.Tables[0].Rows.Count;
            if (string.IsNullOrEmpty(domains.Tables[0].Rows[count - 1][0].ToString().Trim()) && string.IsNullOrEmpty(domains.Tables[0].Rows[count - 1][1].ToString().Trim()))
            {
                DataRow dr=domains.Tables[0].Rows[count - 1];
                domains.Tables[0].Rows.Remove(dr);
 
            }

            //foreach (DataRow dr in domains.Tables[0].Rows)
            //{

            //    if (dr.IsNull("Col1"))
            //    {
            //        //dr.Table.Rows[0].Delete();
            //        domains.Tables[0].Rows.Remove(dr);
            //        domains.Tables[0].Rows.Remove(dr);

            //    }
            //    //if (dr["Exchange"].ToString().Contains("   ") && dr["Exchange Type"].ToString().Contains("   "))
            //    //{
            //    //    //dr.Table.Rows[0].Delete();
            //    //    ds.Tables[0].Rows.Remove(dr);

            //    //}

            //}
        }
      
    return domains;
}

        public DataSet ReadExcelfile1(string FileName)
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

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (dr["Exchange"].ToString().Contains("---") && dr["Exchange Type"].ToString().Contains("---"))
                    {
                        //dr.Table.Rows[0].Delete();
                        ds.Tables[0].Rows.Remove(dr);
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString().Trim()) && string.IsNullOrEmpty(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][1].ToString().Trim()))
                        {
                            DataRow dr1 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                            ds.Tables[0].Rows.Remove(dr1);
                        }
                        break;
                    }

                    //if (dr["Exchange"].ToString().Contains("   ") && dr["Exchange Type"].ToString().Contains("   "))
                    //{
                    //    //dr.Table.Rows[0].Delete();
                    //    ds.Tables[0].Rows.Remove(dr);

                    //}

                }

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
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "; Extended Properties=DBASE 5.0");
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


        