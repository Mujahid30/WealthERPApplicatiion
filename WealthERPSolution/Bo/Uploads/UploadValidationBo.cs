using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoUploads
{
    /// <summary>
    /// -
    /// </summary>
    public class UploadValidationBo
    {
        
      /// <summary>
      /// Used for doing all the data validations for the the Uploads
      /// </summary>
      /// <param name="dtInputfile">Data table of the actual file which is being uploaded</param>
      /// <param name="Uploadtype">Type of R&T</param>
      /// <param name="Extracttype">Profile or Transaction Upload</param>
      /// <param name="Path">Path of the XML</param>
      /// <param name="Skiprowsval">Used for finding the index of the row which is being validated </param>
      /// <returns></returns>
        public DataTable InputValidation(DataTable dtInputfile, string Uploadtype, string Extracttype, string Path, int Skiprowsval)
        {
            DataTable dtresult = new DataTable();
            
            DataTable dtValidationColumns = new DataTable();

            try
            {
                dtInputfile.Columns.Add("Error", typeof(string));
                //dtresult = dtInputfile.Clone();

                //Get the columnnames for which the validations has to be done for the selected Upload type
                dtValidationColumns = XMLBo.GetUploadInpuValidationColumns(Uploadtype, Extracttype, Path);

                //Loop through the rows for which the validations must be done for the selected upload type
                foreach (DataRow drValidations in dtValidationColumns.Rows)
                {
                    string columnname = drValidations["ColumnName"].ToString();
                    dtresult.Columns.Add(columnname);
                    //Loop through the column names in the input table

                    foreach (DataColumn dcInputFile in dtInputfile.Columns)
                    {

                        if (dcInputFile.ColumnName.ToString() == drValidations["ColumnName"].ToString())
                        {
                            int rowindex = Skiprowsval;
                            //Perform null check for the columnname if flag is set as 1 for the selected column
                            foreach (DataRow drInputfile in dtInputfile.Rows)
                            {
                                rowindex++;
                                //if (drValidations["CheckNull"].ToString() == "1")
                                //{

                                //    if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                //    {
                                //        drInputfile["Error"] = drInputfile["Error"].ToString()+"Invalida data in " + columnname + ",Line:"+rowindex+";";
                                //        continue;
                                //    }

                                //}

                                //Perform Is Numeric check for the columnname if flag is set as 1 for the selected column
                                if (drValidations["CheckNumeric"].ToString() == "1")
                                {
                                    double Num;
                                    //Update value as zero if null
                                    if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                    {
                                        drInputfile[columnname] = 0;
                                        continue;
                                    }
                                    //Update Error field if not numeric
                                    if (double.TryParse(drInputfile[columnname].ToString(), out Num) == false)
                                    {
                                        drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                        continue;
                                    }

                                }

                                //Perform Date check for the columnname if flag is set as 1 for the selected column
                                if (drValidations["CheckDate"].ToString() == "1")
                                {
                                    DateTime date;
                                    if (DateTime.TryParse(drInputfile[columnname].ToString(), out date) == false)
                                    {
                                        drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                        continue;
                                    }

                                }

                                //Perform Length check for the columnname if flag is set as 1 for the selected column
                                if (drValidations["CheckLengthVal"].ToString() != "0")
                                {
                                    //break;
                                }
                                if (drValidations["CheckValue"].ToString() == "1")
                                {

                                    if (drInputfile[columnname].ToString() != "NSE" && drInputfile[columnname].ToString() != "BSE" && drInputfile[columnname].ToString() != "B" && drInputfile[columnname].ToString() != "S")
                                    {
                                        drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                        
                                        
                                        continue;
                                    }
                                   
                                }
                                if (drValidations["CheckValue"].ToString() == "2")
                                {

                                    if (drInputfile[columnname].ToString() != "1" && drInputfile[columnname].ToString() != "2" )
                                    {
                                        drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";


                                        continue;
                                    }

                                }
                            }
                        }

                    }
                }
                dtresult.Columns.Add("Error");
                //Select error rows and assign to resultant table
                DataRow[] drresults = dtInputfile.Select("Error is not null");
                foreach (DataRow dr in drresults)
                    dtresult.ImportRow(dr);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadValidationBo.cs:InputValidation()");


                object[] objects = new object[5];
                objects[0] = dtInputfile;
                objects[1] = Uploadtype;
                objects[2] = Extracttype;
                objects[3] = Path;
                objects[4] = Skiprowsval;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtresult;
        }
        public DataTable InputValidation1(DataTable dtInputfile, string Uploadtype, string Extracttype, string Path, int Skiprowsval, string uploadtype)
        {
            DataTable dtresult = new DataTable();

            DataTable dtValidationColumns = new DataTable();

            try
            {
                dtInputfile.Columns.Add("Error", typeof(string));
                //dtresult = dtInputfile.Clone();

                //Get the columnnames for which the validations has to be done for the selected Upload type
                dtValidationColumns = XMLBo.GetUploadInpuValidationColumns1(Uploadtype, Extracttype, Path,uploadtype);

                //Loop through the rows for which the validations must be done for the selected upload type
                if (uploadtype == "NSE")
                {
                    foreach (DataRow drValidations in dtValidationColumns.Rows)
                    {
                        string columnname = drValidations["ColumnName"].ToString();
                        dtresult.Columns.Add(columnname);
                        //Loop through the column names in the input table

                        foreach (DataColumn dcInputFile in dtInputfile.Columns)
                        {

                            if (dcInputFile.ColumnName.ToString() == drValidations["ColumnName"].ToString())
                            {
                                int rowindex = Skiprowsval;
                                //Perform null check for the columnname if flag is set as 1 for the selected column
                                foreach (DataRow drInputfile in dtInputfile.Rows)
                                {
                                    rowindex++;
                                    //if (drValidations["CheckNull"].ToString() == "1")
                                    //{

                                    //    if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                    //    {
                                    //        drInputfile["Error"] = drInputfile["Error"].ToString()+"Invalida data in " + columnname + ",Line:"+rowindex+";";
                                    //        continue;
                                    //    }

                                    //}

                                    //Perform Is Numeric check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckNumeric"].ToString() == "1")
                                    {
                                        double Num;
                                        //Update value as zero if null
                                        if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                        {
                                            drInputfile[columnname] = 0;
                                            continue;
                                        }
                                        //Update Error field if not numeric
                                        if (double.TryParse(drInputfile[columnname].ToString(), out Num) == false)
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                            continue;
                                        }

                                    }

                                    //Perform Date check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckDate"].ToString() == "1")
                                    {
                                        DateTime date;
                                        if (DateTime.TryParse(drInputfile[columnname].ToString(), out date) == false)
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                            continue;
                                        }

                                    }

                                    //Perform Length check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckLengthVal"].ToString() != "0")
                                    {
                                        //break;
                                    }
                                    if (drValidations["CheckValue"].ToString() == "1")
                                    {
                                        string m = drInputfile[columnname].ToString();
                                        string n = m.TrimEnd(new char[] { ' ' });
                                        n = m.TrimEnd(new char[] { '\n' });
                                        if ( n !=  "Margin")
                                        {
                                            
                                            drInputfile["Error"] = "Wrong file format, does not contain all columns";
                                            //drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";


                                            continue;
                                        }

                                    }
                                    if (drValidations["CheckValue"].ToString() == "2")
                                    {

                                        if (drInputfile[columnname].ToString() != "1" && drInputfile[columnname].ToString() != "2")
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";


                                            continue;
                                        }

                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    foreach (DataRow drValidations in dtValidationColumns.Rows)
                    {
                        string columnname = drValidations["ColumnName"].ToString();
                        dtresult.Columns.Add(columnname);
                        //Loop through the column names in the input table

                        foreach (DataColumn dcInputFile in dtInputfile.Columns)
                        {

                            if (dcInputFile.ColumnName.ToString() == drValidations["ColumnName"].ToString())
                            {
                                int rowindex = Skiprowsval;
                                //Perform null check for the columnname if flag is set as 1 for the selected column
                                foreach (DataRow drInputfile in dtInputfile.Rows)
                                {
                                    rowindex++;
                                    //if (drValidations["CheckNull"].ToString() == "1")
                                    //{

                                    //    if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                    //    {
                                    //        drInputfile["Error"] = drInputfile["Error"].ToString()+"Invalida data in " + columnname + ",Line:"+rowindex+";";
                                    //        continue;
                                    //    }

                                    //}

                                    //Perform Is Numeric check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckNumeric"].ToString() == "1")
                                    {
                                        double Num;
                                        //Update value as zero if null
                                        if (String.IsNullOrEmpty(drInputfile[columnname].ToString()))
                                        {
                                            drInputfile[columnname] = 0;
                                            continue;
                                        }
                                        //Update Error field if not numeric
                                        if (double.TryParse(drInputfile[columnname].ToString(), out Num) == false)
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                            continue;
                                        }

                                    }

                                    //Perform Date check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckDate"].ToString() == "1")
                                    {
                                        DateTime date;
                                        if (DateTime.TryParse(drInputfile[columnname].ToString(), out date) == false)
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";
                                            continue;
                                        }

                                    }

                                    //Perform Length check for the columnname if flag is set as 1 for the selected column
                                    if (drValidations["CheckLengthVal"].ToString() != "0")
                                    {
                                        //break;
                                    }
                                    if (drValidations["CheckValue"].ToString() == "1")
                                    {

                                        if (drInputfile[columnname].ToString() != "Margin" )
                                        {
                                            drInputfile["Error"] = "Wrong file format, does not contain all columns";
                                            //drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";


                                            continue;
                                        }

                                    }
                                    if (drValidations["CheckValue"].ToString() == "2")
                                    {

                                        if (drInputfile[columnname].ToString() != "1" && drInputfile[columnname].ToString() != "2")
                                        {
                                            drInputfile["Error"] = drInputfile["Error"].ToString() + "Invalida data in " + columnname + ",Line:" + rowindex + ";";


                                            continue;
                                        }

                                    }
                                }
                            }

                        }
                    }

                }
                dtresult.Columns.Add("Error");
                //Select error rows and assign to resultant table
                DataRow[] drresults = dtInputfile.Select("Error is not null");
                foreach (DataRow dr in drresults)
                    dtresult.ImportRow(dr);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadValidationBo.cs:InputValidation()");


                object[] objects = new object[5];
                objects[0] = dtInputfile;
                objects[1] = Uploadtype;
                objects[2] = Extracttype;
                objects[3] = Path;
                objects[4] = Skiprowsval;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtresult;
        }
    }
}
