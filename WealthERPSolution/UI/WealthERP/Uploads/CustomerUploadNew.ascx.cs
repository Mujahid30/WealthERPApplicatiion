using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.SqlServer.Dts.Runtime;
using System.IO;
using BoUploads;
using VoUploads;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;
using BoSuperAdmin;
using System.Xml.Serialization;


namespace WealthERP.Uploads
{
    public partial class CustomerUploadNew : System.Web.UI.UserControl
    {
        List<KarvyUploadsVo> karvyNewCustomerList = new List<KarvyUploadsVo>();
        List<CamsUploadsVo> camsNewCustomerList = new List<CamsUploadsVo>();
        List<WerpUploadsVo> werpNewCustomerList = new List<WerpUploadsVo>();

        StringBuilder sbXMLString = new StringBuilder();
        KarvyUploadsVo karvyUploadsVo = new KarvyUploadsVo();
        StandardFolioUploadBo standardFolioUploadBo = new StandardFolioUploadBo();
        WerpMFUploadsBo werpMFUploadsBo = new WerpMFUploadsBo();
        WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
        WerpUploadsBo werpUploadBo = new WerpUploadsBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo = new UploadProcessLogVo();
        RMVo rmVo = new RMVo();
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();

        string ValidationProgress = "";
        CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
        KarvyUploadsBo karvyUploadsBo = new KarvyUploadsBo();
        StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
        TempletonUploadsBo templetonUploadsBo = new TempletonUploadsBo();
        DeutscheUploadsBo deutscheUploadsBo = new DeutscheUploadsBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();
        UploadValidationBo uploadsvalidationBo = new UploadValidationBo();
        UserBo userBo = new UserBo();

        Random id = new Random();
        DataSet getNewFoliosDs = new DataSet();
        DataTable getNewFoliosDt = new DataTable();
        DataSet dsXML = new DataSet();
        DataTable dtInputRejects;
        string message = "";

        int adviserId;
        int rmId;
        int customerId;
        int customerId2;
        int userId;
        int UploadProcessId = 0;
        int portfolioId;
        int countCustCreated = 0;
        int countFolioCreated = 0;

        string folioNum;
        string packagePath;
        string reject_reason = "";
        string configPath;
        string xmlPath;
        bool badData;
        bool rejectUpload_Flag = false;
        string lastUploadDate = "";

        protected void Page_Load(object sender, EventArgs e)
        {          
            btn_Upload.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            this.Page.Culture = "en-US";
            SessionBo.CheckSession();
            rmVo = (RMVo)Session["rmVo"];
            rmUserVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];

            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Session["userVo"] == null)
            {
                Session.Clear();
                Session.Abandon();

                // If User Sessions are empty, load the login control 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);
            }

            if (!IsPostBack)
            {
                UpdateLastUploadedDate();
            }

        }
        protected void UpdateLastUploadedDate()
        {
            if (lastUploadDate != "")
            {
                //DateTime dt = new DateTime();
                //String.Format("{0:d}", dt);

                lblLastUploadDateText.Visible = true;
                lblLastUploadDate.Visible = true;
                if (lastUploadDate != "01/01/0001 00:00:00")
                {
                    lastUploadDate = DateTime.Parse(lastUploadDate).ToLongDateString();                     
                }
                else
                {
                    lastUploadDate = "No Uploaded History!";
                }

                lblLastUploadDate.Text = lastUploadDate.ToString();
            }

        }

        protected void ddlUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUploadType.SelectedValue == Contants.ExtractTypeFIHoldings)
            {   // MF FOLIO  Only
                ddlListCompany.DataSource = GetFDGenericDictionary();
                ddlListCompany.DataTextField = "Key";
                ddlListCompany.DataValueField = "Value";
                ddlListCompany.DataBind();
                ddlListCompany.Items.Insert(0, new ListItem("Select Source Type", "Select Source Type"));

            }
        }

        private static Dictionary<string, string> GetFDGenericDictionary()
        {
            Dictionary<string, string> genDictProfile = new Dictionary<string, string>();
            genDictProfile.Add("Holdings/Allotment", "LK");
            return genDictProfile;
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                #region Uploading Content
                string pathxml = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                bool XmlCreated = GetInputXML();

                xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                string fileName = Server.MapPath("\\UploadFiles\\" + UploadProcessId + ".xml");

              
                # endregion
            }
        }
        protected bool GetInputXML()
        {
            bool XmlCreated = false;
            ReadExternalFile readFile = new ReadExternalFile();

            UploadCommonBo uploadcommonBo = new UploadCommonBo();
            int filetypeid = 0; // holds filtype id for the type of file which is to be ulpoaded
            DataSet ds = new DataSet(); //holds data from the file

            string pathxml = "";

            // This dataset stores the values of actual colum names for the file type with Mandatory flags
            DataSet dsColumnNames = new DataSet();
            DataSet dsWerpColumnNames = new DataSet();

            //This dataset is used as data for the final XML to be created.
            //DataSet dsXML = new DataSet();

            int indexOfExtension = FileUpload.FileName.LastIndexOf('.');
            int length = FileUpload.FileName.Length;
            string UploadFileName = FileUpload.FileName.ToString();
            string extension = (UploadFileName.Substring(indexOfExtension + 1)).ToLower();
            string strFileReadError = string.Empty;

            pathxml = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            try
            {
                bool filereadflag = true;

                 #region FI Holding
                //Read File for Mf CAMS Transaction DBF Upload
               if (ddlUploadType.SelectedValue == Contants.ExtractTypeFIHoldings && ddlListCompany.SelectedValue == Contants.UploadExternalTypeFixedIncome)
                {
                    if (extension == "dbf")
                    {
                        string filename = "FIHD.dbf";
                        string filepath = Server.MapPath("UploadFiles");

                        FileUpload.SaveAs(filepath + "\\" + filename);
                        ds = readFile.ReadDBFFile(filepath, filename, out strFileReadError);                       
                    }
                    else if (extension == "xls" || extension == "xlsx")
                    {
                        string Filepath = Server.MapPath("UploadFiles") + "\\FIHoldingsXls.xls";
                        FileUpload.SaveAs(Filepath);
                        ds = readFile.ReadExcelfile(Filepath);
                    }

                    else
                    {
                        ValidationProgress = "Failure";
                    }
                    if (filereadflag == true)
                    {
                        //get all column nams for the selcted file type
                        dsColumnNames = uploadcommonBo.GetColumnNames((int)Contants.UploadTypes.FILinkHolding);

                        //Get werp Column Names for the selected type of file
                        dsWerpColumnNames = uploadcommonBo.GetUploadWERPNameForExternalColumnNames((int)Contants.UploadTypes.FILinkHolding);

                        dsXML = RemoveUnwantedDatafromXMLDs(ds, dsColumnNames, dsWerpColumnNames, 1);
                        //Get XML after mapping, checking for columns
                        dsXML = GetXMLDs(ds, dsColumnNames, dsWerpColumnNames);

                        //Get filetypeid from XML
                        filetypeid = XMLBo.getUploadFiletypeCode(pathxml, Contants.FixedIncome, Contants.UploadExternalTypeFixedIncome, Contants.ExtractTypeFIHoldings);

                        //Reject upload if there are any data error validations
                    
                    }
                }
                #endregion

               processlogVo.AdviserId = adviserVo.advisorId;
               processlogVo.CreatedBy = userVo.UserId;
               processlogVo.StartTime = DateTime.Now;
               processlogVo.FileName = FileUpload.FileName;
               processlogVo.FileTypeId = filetypeid;
               processlogVo.IsExternalConversionComplete = 1;
               processlogVo.ModifiedBy = userVo.UserId;
              
               processlogVo.UserId = userVo.UserId;

               if (ddlUploadType.SelectedValue == Contants.ExtractTypeFIHoldings)
                   processlogVo.ExtractTypeCode = "HD";

               processlogVo.ProcessId = uploadcommonBo.CreateUploadProcess(processlogVo);
               dsXML.Tables[0].Columns.Add("ProcessId");
               dsXML.Tables[0].Columns.Add("AdviserId");
               dsXML.Tables[0].Columns.Add("CreatedBy");
               dsXML.Tables[0].Columns.Add("CreatedOn");
               dsXML.Tables[0].Columns.Add("ModifiedBy");
               dsXML.Tables[0].Columns.Add("ModifiedOn");

               foreach (DataRow dr in dsXML.Tables[0].Rows)
               {
                   dr["ProcessId"] = processlogVo.ProcessId;
                   dr["AdviserId"] = adviserId;
                   dr["CreatedBy"] = userVo.UserId;
                   dr["CreatedOn"] = DateTime.Now;
                   dr["ModifiedBy"] = userVo.UserId;
                   dr["ModifiedOn"] = DateTime.Now;
               }

               dsXML.WriteXml(Server.MapPath("UploadFiles") + "\\" + processlogVo.ProcessId + ".xml", XmlWriteMode.WriteSchema);
               XmlCreated = true;

               Session[SessionContents.UploadProcessId] = processlogVo.ProcessId;
               Session[SessionContents.UploadFileTypeId] = processlogVo.FileTypeId;
               UploadProcessId = processlogVo.ProcessId;            

               System.IO.StringWriter sw = new System.IO.StringWriter(sbXMLString);
               dsXML.Tables[0].WriteXml(sw);
               
            }
            catch (BaseApplicationException ex)
            {
                rejectUpload_Flag = true;
                reject_reason = reject_reason + ex.Message;
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerUploadNew.ascx:GetInputXML()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return XmlCreated;
        }

        public DataSet RemoveUnwantedDatafromXMLDs(DataSet dsFile, DataSet dsActual, DataSet dsColumnNames, int fileTypeId)
        {
            DataSet dsXML = new DataSet();
            DataTable dt = new DataTable();
            UploadCommonBo uploadcommonBo = new UploadCommonBo();
            DataTable dtFInalTable = new DataTable();
            DataTable dtFinalAfterRowSkipp = new DataTable();
            string isMandatory = string.Empty;
            bool isValidFile = false;
            bool isFiltered = false;
            string columnName = string.Empty;
            int count = 0;
            try
            {
                int dsfileCount = dsFile.Tables[0].Rows.Count;
                DataSet ds = new DataSet();
                ds = dsFile;
                string[] strMandatoryCoulmn = new string[0];
                string[] strMappingCoulmn = new string[0];
                foreach (DataRow dr in dsActual.Tables[0].Rows)
                {
                    dt.Columns.Add(dr["XMLHeaderName"].ToString());
                }
                //get all column names for the DB to match the headers with the WERP headers
                DataSet WERPColumnnames = dsColumnNames;
                int rowIndex = 0;
                foreach (DataRow row1 in WERPColumnnames.Tables[0].Rows)
                {
                    if (isFiltered == true)
                        break;

                    foreach (DataRow dr in dsActual.Tables[0].Rows)
                    {
                        if (isFiltered == true)
                            break;

                        bool isExistingFlag = false;

                        DataSet dsFilteredData = new DataSet();
                        dsFilteredData = dsFile;

                        DataTable dsMandatoryData = new DataTable();

                        if (fileTypeId == 3 || fileTypeId == 8 || fileTypeId == 15 || fileTypeId == 1 || fileTypeId == 6 || fileTypeId == 19 || fileTypeId == 11 || fileTypeId == 10 || fileTypeId == 17)
                        {
                            dsActual.Tables[0].DefaultView.RowFilter = "IsTransactionMandatory=" + 1;
                            dsMandatoryData = dsActual.Tables[0].DefaultView.ToTable();
                        }
                       

                        string isMandatoryIndefaultview = string.Empty;
                        foreach (DataRow drMd in dsMandatoryData.Rows)
                        {
                            isMandatoryIndefaultview = isMandatoryIndefaultview + drMd["XMLHeaderName"].ToString() + "~";
                        }

                        strMandatoryCoulmn = isMandatoryIndefaultview.Split('~');
                        columnName = row1.ItemArray.GetValue(1).ToString();

                        isFiltered = true;
                    }
                }
                foreach (DataColumn dcFile in ds.Tables[0].Columns)
                {
                    if (count > 0)
                    {
                        break;
                    }

                    int i = 0;
                    //string columnName = row1.ItemArray.GetValue(1).ToString();
                    string excelfileColumnName = dcFile.ColumnName.ToString();
                    //columnName = row1.ItemArray.GetValue(1).ToString();

                    DataTable dsFilteredData = new DataTable();

                    foreach (string c in strMandatoryCoulmn)
                    {
                        if (count > 0)
                            break;

                        WERPColumnnames.Tables[0].DefaultView.RowFilter = "XMLHeaderName=" + "'" + c.ToString() + "'";
                        dsFilteredData = WERPColumnnames.Tables[0].DefaultView.ToTable();

                        string columnNameIndefaultview = string.Empty;
                        foreach (DataRow dr in dsFilteredData.Rows)
                        {
                            columnNameIndefaultview = columnNameIndefaultview + dr["ExternalHeaderName"].ToString() + "~";
                        }

                        columnNameIndefaultview = columnNameIndefaultview + c + "~";

                        strMappingCoulmn = columnNameIndefaultview.Split('~');

                        //for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                        for (int k = 0; k < 10; k++)
                        {
                            if (count > 0)
                                break;

                            int drofDS = 0;
                            count = 0;
                            int columnCount = 0;

                            foreach (string s in strMappingCoulmn)
                            {
                                int iterateColumn = 0;
                                while (iterateColumn < ds.Tables[0].Columns.Count)
                                {
                                    if (ds.Tables[0].Columns[iterateColumn].ToString().Trim().ToUpper() == s.ToString().Trim().ToUpper())
                                    {
                                        count++;
                                        isValidFile = true;
                                        break;
                                    }
                                    iterateColumn++;
                                    columnCount++;
                                }
                                if (count > 0)
                                    break;
                            }
                        }

                        if (!isValidFile)
                        {
                            DataRow drToDelete;

                            for (int k = 0; k < 10; k++)
                            //for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                            {
                                int drofDS = 0;
                                count = 0;
                                int columnCount = 0;

                                foreach (string s in strMappingCoulmn)
                                {
                                    int iterateColumn = 0;
                                    while (iterateColumn < ds.Tables[0].Columns.Count)
                                    {
                                        if (!string.IsNullOrEmpty(s.Trim()))
                                        {
                                            if (ds.Tables[0].Rows[drofDS][iterateColumn].ToString().Trim() == s.ToString().Trim())
                                            {
                                                count++;
                                                break;
                                            }
                                            iterateColumn++;
                                            columnCount++;
                                        }
                                        else
                                            break;
                                    }
                                    if (count > 0)
                                        break;
                                }
                                if (count > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    drToDelete = ds.Tables[0].Rows[drofDS];
                                    ds.Tables[0].Rows.Remove(drToDelete);
                                }
                            }
                        }
                    }
                }
                dtFInalTable = ds.Tables[0];
                if (!isValidFile)
                {
                    int colId = 0;
                    if (count > 0)
                    {
                        //Replace arbitrary names with columnanmes of the rows skipped
                        colId = 0;
                        DataRow drToRemove = dtFInalTable.Rows[0];
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (ddlUploadType.SelectedValue == "MFFI" && ddlListCompany.SelectedValue == "FIHD")
                            {
                                dc.ColumnName = dtFInalTable.Rows[0][colId].ToString();
                            }
                            
                            colId++;
                        }
                        dtFInalTable.Rows.Remove(drToRemove);
                    }
                }
                dtFinalAfterRowSkipp = dtFInalTable;
                dtFinalAfterRowSkipp = dtFInalTable.Copy();
                //For inserting the values into table used for creating xml if all mandatory is there
                if (rejectUpload_Flag == false)
                    dsXML.Tables.Add(dtFinalAfterRowSkipp);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
           
            return dsXML;
        }
        private DataSet GetXMLDs(DataSet dsFile, DataSet dsActual, DataSet dsColumnNames)
        {
            DataSet dsXML = new DataSet();
            DataTable dt = new DataTable();
            UploadCommonBo uploadcommonBo = new UploadCommonBo();

            try
            {


                int dsfileCount = dsFile.Tables[0].Rows.Count;


                //Add headers to the datatable which will be used for creating xml
                foreach (DataRow dr in dsActual.Tables[0].Rows)
                {
                    dt.Columns.Add(dr["XMLHeaderName"].ToString());
                }


                //get all column names for the DB to match the headers with the WERP headers
                DataSet WERPColumnnames = dsColumnNames;


                foreach (DataColumn dcFile in dsFile.Tables[0].Columns)
                {
                    foreach (DataRow drwerpclmns in WERPColumnnames.Tables[0].Rows)
                    {
                        if (drwerpclmns["ExternalHeaderName"].ToString().ToUpper().TrimEnd() == dcFile.ColumnName.ToString().ToUpper().TrimEnd())
                        {
                            dcFile.ColumnName = drwerpclmns["XMLHeaderName"].ToString();
                        }
                    }
                }


                //Check for mandatory 

                foreach (DataRow dr in dsActual.Tables[0].Rows)
                {
                    bool isExistingFlag = false;
                    foreach (DataColumn dcFile in dsFile.Tables[0].Columns)
                    {
                        if (dr["XMLHeaderName"].ToString().ToUpper().TrimEnd() == dcFile.ColumnName.ToString().ToUpper().TrimEnd())
                        {
                            isExistingFlag = true;
                            string WERPcolumnname = dr["XMLHeaderName"].ToString();

                            DataRow drXML;
                            for (int i = 0; i < dsfileCount; i++)
                            {

                                if (dt.Rows.Count < dsfileCount)
                                {
                                    drXML = dt.NewRow();
                                    drXML[WERPcolumnname] = dsFile.Tables[0].Rows[i][dcFile.ColumnName].ToString();
                                    dt.Rows.Add(drXML);
                                }
                                else
                                {
                                    dt.Rows[i][WERPcolumnname] = dsFile.Tables[0].Rows[i][dcFile.ColumnName];
                                }

                            }

                            break;
                        }

                    }
                    if (isExistingFlag == false)
                    {
                        if ((ddlUploadType.SelectedValue == Contants.ExtractTypeFIHoldings ) && dr["IsProfileMandatory"].ToString() == "1")
                        {
                            rejectUpload_Flag = true;
                            reject_reason = reject_reason + "The mandatory Column '" + dr["XMLHeaderName"].ToString() + "' does not exist; <br />";
                        } 
                    }
                }

                //For inserting the values into table used for creating xml if all mandatory is there

                if (rejectUpload_Flag == false)
                    dsXML.Tables.Add(dt);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerUpload.ascx:GetXMLDs()");

                object[] objects = new object[2];
                objects[0] = dsFile;
                objects[1] = dsActual;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsXML;
        }
    }
}