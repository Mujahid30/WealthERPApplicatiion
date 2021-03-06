﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using VoUser;
using System.Data;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Telerik.Web.UI;
using System.IO;
using System.Configuration;
using Ionic.Zip;



namespace WealthERP.OnlineOrderBackOffice
{
    public partial class HoldingIssueAllotment : System.Web.UI.UserControl
    {
        OnlineNCDBackOfficeBo OnlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineOrderBackOfficeBo onlineOrderBackOffice = new OnlineOrderBackOfficeBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        DateTime fromdate;
        DateTime todate;
        AdvisorVo adviserVo;
        int producttype = 0;
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {

                fromdate = DateTime.Now.AddDays(-1);
                txtFromDate.SelectedDate = fromdate;
                txtToDate.SelectedDate = DateTime.Now;

                BindAMC();
                btnDownload.Visible = false;
                ddlType.Visible = false;
            }
        }
        protected void BindAMC()
        {
            try
            {
                ddlAMC.Items.Clear();
                DataSet ds = new DataSet();
                DataTable dtAmc = new DataTable();
                dtAmc = commonLookupBo.GetProdAmc();
                if (dtAmc.Rows.Count > 0)
                {
                    ddlAMC.DataSource = dtAmc;
                    ddlAMC.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataBind();
                    ddlAMC.Items.Insert(0, new ListItem("All", "0"));

                }
                ddlAMC.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindAmcDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DataSet dsExtractData = new DataSet();

            String filename = "BSEClientupload-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt";
            DownloadBidFile(dsExtractData, filename, "|");
        }
        private void DownloadBidFile(DataSet dsExtractData, string filename, string delimit)
        {
            try
            {
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                string dateFormat = "dd-mm-yyyy";

                dsExtractData = onlineOrderBackOfficeBo.GetBSECustomer(adviserVo.advisorId, fromdate, todate);
                dsExtractData.Tables[0].Columns.Remove("C_DematAcceptedOn");

                StreamWriter str = new StreamWriter(Server.MapPath(@"~/UploadFiles/" + filename), false, System.Text.Encoding.Default);

                string Columns = string.Empty;

                foreach (DataColumn column in dsExtractData.Tables[0].Columns)

                    Columns += column.ColumnName + delimit;



                // Headers  For different types


                DataColumn[] arrCols = new DataColumn[dsExtractData.Tables[0].Columns.Count];
                dsExtractData.Tables[0].Columns.CopyTo(arrCols, 0);
                foreach (DataRow datarow in dsExtractData.Tables[0].Rows)
                {
                    string row = string.Empty;
                    int i = 0;
                    foreach (object item in datarow.ItemArray)
                    {
                        if (arrCols[i].DataType.FullName == "System.DateTime")
                        {
                            string strDate = string.IsNullOrEmpty(item.ToString()) ? "" : DateTime.Parse(item.ToString()).ToString(dateFormat);
                            row += strDate + delimit;
                        }
                        else
                        {
                            row += item.ToString().Trim() + delimit;
                        }
                        i++;
                    }
                    str.WriteLine(row.Remove(row.Length - 1, 1));
                }
                str.Flush();
                str.Close();
                string myFileData;
                // File in
                myFileData = File.ReadAllText(Server.MapPath("~/UploadFiles/" + filename));
                // Remove last CR/LF
                // 1) Check that the file has CR/LF at the end
                if (myFileData.EndsWith(Environment.NewLine))
                {
                    //2) Remove CR/LF from the end and write back to file (new file)
                    //File.WriteAllText(@"D:\test_backup.csv", myFileData.TrimEnd(null)); // Removes ALL white spaces from the end!
                    File.WriteAllText(Server.MapPath("~/UploadFiles/" + filename), myFileData.TrimEnd(Environment.NewLine.ToCharArray())); // Removes ALL CR/LF from the end!
                }
                #region download notepad or text file.
                Response.ContentType = "Text/Plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/UploadFiles/" + filename);
                Response.TransmitFile(Server.MapPath("~/UploadFiles/" + filename));
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                #endregion
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    System.IO.File.Delete(Server.MapPath("~/UploadFiles/" + filename));
            //}

        }
        protected void GetExtractData()
        {
            try
            {
                DataTable dsExtractData = new DataTable();
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                dsExtractData = onlineOrderBackOfficeBo.GetBSECustomer(adviserVo.advisorId, fromdate, todate).Tables[0];

                if (dsExtractData.Rows.Count > 0)
                {
                    if (Cache["gvCustomerDetails" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("gvCustomerDetails" + advisorVo.advisorId, dsExtractData);
                    }
                    else
                    {
                        Cache.Remove("gvCustomerDetails" + advisorVo.advisorId);
                        Cache.Insert("gvCustomerDetails" + advisorVo.advisorId, dsExtractData);
                    }
                    gvCustomerDetails.DataSource = dsExtractData;
                    gvCustomerDetails.DataBind();
                    pnlCustomerDetails.Visible = true;
                    btnDownload.Visible = true;
                    btnUpdate.Visible = false;
                    imgexportButton.Visible = true;

                }
                else
                {
                    gvCustomerDetails.DataSource = dsExtractData;
                    gvCustomerDetails.DataBind();
                }


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs: GetExtractData()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void BindRTAInitialReport()
        {
            try
            {
                pnlOrderReport.Visible = false;
                pnlFATCA.Visible = false;
                DataTable dtBindRTAInitialReport;
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                if (ddlOrderType.SelectedValue == "1")
                {
                    dtBindRTAInitialReport = onlineOrderBackOffice.GetRTAInitialReport(ddlType.SelectedValue.ToString(), fromdate, todate, 0, 0);
                    if (Cache["RTAInitialReport" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("RTAInitialReport" + advisorVo.advisorId);
                    }
                    Cache.Insert("RTAInitialReport" + advisorVo.advisorId, dtBindRTAInitialReport);

                    gvOrderReport.DataSource = dtBindRTAInitialReport;
                    gvOrderReport.DataBind();
                    pnlOrderReport.Visible = true;
                    pnlCustomerDetails.Visible = false;
                    pnlFATCA.Visible = false;
                    imgexportButton.Visible = true;
                    btnDownload.Visible = false;

                }
                else if (ddlOrderType.SelectedValue == "3")
                {
                    dtBindRTAInitialReport = onlineOrderBackOffice.GetCustomerDetails(adviserVo.advisorId, ddlType.SelectedValue.ToString(), fromdate, todate);
                    if (Cache["gvCustomerDetails" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("gvCustomerDetails" + advisorVo.advisorId);
                    }
                    Cache.Insert("gvCustomerDetails" + advisorVo.advisorId, dtBindRTAInitialReport);
                    gvCustomerDetails.DataSource = dtBindRTAInitialReport;
                    gvCustomerDetails.DataBind();
                    pnlCustomerDetails.Visible = true;
                    pnlFATCA.Visible = false;
                    imgexportButton.Visible = true;
                    pnlOrderReport.Visible = false;
                    btnUpdate.Visible = true;
                    btnDownload.Visible = false;


                }
                else if (ddlOrderType.SelectedValue == "2")
                {

                    dtBindRTAInitialReport = onlineOrderBackOffice.GetRTAInitialReport(ddlType.SelectedValue.ToString(), fromdate, todate, 1, int.Parse(ddlAMC.SelectedValue));
                    if (Cache["FATCAReport" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("FATCAReport" + advisorVo.advisorId);
                    }
                    Cache.Insert("FATCAReport" + advisorVo.advisorId, dtBindRTAInitialReport);

                    rgFATCA.DataSource = dtBindRTAInitialReport;
                    rgFATCA.DataBind();
                    pnlFATCA.Visible = true;
                    pnlCustomerDetails.Visible = false;
                    pnlOrderReport.Visible = false;
                    btnDownload.Visible = false;
                }

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindRTAInitialReport()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void ddlOrderType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlType.Items.FindByValue("FCS").Enabled = false;
            ddlType.Items.FindByValue("FCD").Enabled = false;
            if (ddlOrderType.SelectedValue == "1")
            {
                tdlblAMC.Visible = false;
                tdddlAMC.Visible = false;
                tdddlType.Visible = true;
                tdlblType.Visible = true;
                ddlType.Visible = true;
                pnlOrderReport.Visible = true;
                pnlCustomerDetails.Visible = false;
                pnlFATCA.Visible = false;
                ddlType.Items.FindByValue("EBSE").Enabled = false;
                ddlType.Items.FindByValue("EMIS").Enabled = false;


            }
            if (ddlOrderType.SelectedValue == "2")
            {
                tdddlType.Visible = false;
                tdlblType.Visible = false;
                tdlblAMC.Visible = true;
                tdddlAMC.Visible = true;
                pnlFATCA.Visible = true;
                pnlCustomerDetails.Visible = false;
                pnlOrderReport.Visible = false;
                ddlType.Visible = true;
                ddlType.Items.FindByValue("EBSE").Enabled = false;
                ddlType.Items.FindByValue("EMIS").Enabled = false;
                ddlType.Items.FindByValue("AMC").Enabled = true;
                ddlType.Items.FindByValue("RNT").Enabled = true;

            }

            if (ddlOrderType.SelectedValue == "3")
            {
                tdddlType.Visible = true;
                tdlblType.Visible = true;
                tdlblAMC.Visible = false;
                tdddlAMC.Visible = false;

                pnlFATCA.Visible = false;
                //pnlCustomerDetails.Visible = true;
                pnlOrderReport.Visible = false;
                ddlType.Visible = true;

                ddlType.Items.FindByValue("EBSE").Enabled = true;
                ddlType.Items.FindByValue("EMIS").Enabled = true;
                ddlType.Items.FindByValue("AMC").Enabled = false;
                ddlType.Items.FindByValue("RNT").Enabled = false;


            }
            if (ddlOrderType.SelectedValue == "KA" || ddlOrderType.SelectedValue == "CA")
            {
                tdddlType.Visible = true;
                tdlblType.Visible = true;
                tdlblAMC.Visible = false;
                tdddlAMC.Visible = false;
                pnlFATCA.Visible = false;
                pnlOrderReport.Visible = false;
                ddlType.Visible = true;
                ddlType.Items.FindByValue("EBSE").Enabled = false;
                ddlType.Items.FindByValue("EMIS").Enabled = false;
                ddlType.Items.FindByValue("AMC").Enabled = false;
                ddlType.Items.FindByValue("RNT").Enabled = false;
                pnlCustomerDetails.Visible = true;

                if (ddlOrderType.SelectedValue == "KA")
                {
                    ddlType.Items.FindByValue("FCD").Enabled = true;
                }
                else
                {
                    ddlType.Items.FindByValue("FCS").Enabled = true;
                }


            }


        }
        protected void gvCustomerDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerDetailsReport = new DataTable();
            dtCustomerDetailsReport = (DataTable)Cache["gvCustomerDetails" + advisorVo.advisorId];
            if (dtCustomerDetailsReport != null)
            {
                gvCustomerDetails.DataSource = dtCustomerDetailsReport;


            }
        }
        protected void gvOrderReport_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindRTAInitialReport = new DataTable();
            dtBindRTAInitialReport = (DataTable)Cache["RTAInitialReport" + advisorVo.advisorId];

            if (dtBindRTAInitialReport != null)
            {
                gvOrderReport.DataSource = dtBindRTAInitialReport;
            }

        }

        protected void gvCustomerDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                CheckBox chkBox = (CheckBox)dataItem["Is Real Investor"].Controls[0];
                chkBox.Enabled = true;
            }
        }

        protected void rgFATCA_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtFATCAReport = new DataTable();
            dtFATCAReport = (DataTable)Cache["FATCAReport" + advisorVo.advisorId];

            if (dtFATCAReport != null)
            {
                rgFATCA.DataSource = dtFATCAReport;
            }
        }
        protected void BindAdviserIssueAllotmentList()
        {
            try
            {
                DataSet dsGetAdviserissueallotmentList = new DataSet();
                dsGetAdviserissueallotmentList = OnlineNCDBackOfficeBo.GetAdviserissueallotmentList(advisorVo.advisorId, 1, ddlType.SelectedValue.ToString(), fromdate, todate);
                if (dsGetAdviserissueallotmentList.Tables[0].Rows.Count > 0)
                {
                    if (Cache["AdviserIssueList" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("AdviserIssueList" + advisorVo.advisorId, dsGetAdviserissueallotmentList);
                    }
                    else
                    {
                        Cache.Remove("AdviserIssueList" + advisorVo.advisorId);
                        Cache.Insert("AdviserIssueList" + advisorVo.advisorId, dsGetAdviserissueallotmentList);
                    }
                    gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
                    gvAdviserIssueList.DataBind();
                    AdviserIssueList.Visible = true;
                }
                else
                {
                    gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
                    gvAdviserIssueList.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindAdviserClientKYCStatusList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void AutoOrderExtract()
        {

            if (txtFromDate.SelectedDate != null)
                fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
            if (txtToDate.SelectedDate != null)
                todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
            string dailyDirName = ddlOrderType.SelectedItem.Text + @"_" + ddlType.SelectedItem.Text;
            string extractPath = ConfigurationSettings.AppSettings["RTA_EXTRACT_PATH"];
            string downloadFileName = ddlType.SelectedValue == "FCS" ? "CAMS_SM" : "DOC_0011" + todate.ToString("ddMMyyy");
            string localFilePath = onlineOrderBackOfficeBo.GenerateDailyOrderFATCASummaryFiles(Server.MapPath("~/ReferenceFiles/RTAExtractSampleFiles/"), ddlOrderType.SelectedValue, ddlType.SelectedValue, adviserVo.advisorId, fromdate, todate);
            if (File.Exists(extractPath + @"\" + adviserVo.advisorId + @"\" + downloadFileName + ".DBF") == true)
            {
                System.IO.File.Delete(extractPath + @"\" + adviserVo.advisorId + @"\" + downloadFileName + ".DBF");
            }
            File.Copy(localFilePath, extractPath + @"\" + adviserVo.advisorId + @"\" + downloadFileName + ".DBF");
            System.Threading.Thread.Sleep(1000);
            if (string.IsNullOrEmpty(localFilePath))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Data Available to download');", true);
                return;
            }
            Response.ContentType = "application/dbf";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + downloadFileName + ".DBF");
            string aaa = extractPath + @"\" + adviserVo.advisorId + @"\" + downloadFileName + ".DBF";
            Response.TransmitFile(extractPath + @"\" + adviserVo.advisorId + @"\" + downloadFileName + ".DBF");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();


        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "EBSE")
            {
                GetExtractData();
            }
            if (ddlType.SelectedValue == "0" || ddlType.SelectedValue == "AMC" || ddlType.SelectedValue == "RNT" || ddlType.SelectedValue == "EMIS")
            {
                BindRTAInitialReport();
            }
            else if (ddlType.SelectedValue == "FCS" || ddlType.SelectedValue == "FCD")
            {
                pnlCustomerDetails.Visible = true;
                AutoOrderExtract();
            }
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ddlOrderType.SelectedValue == "1")
            {
                gvOrderReport.ExportSettings.OpenInNewWindow = true;
                gvOrderReport.ExportSettings.IgnorePaging = true;
                gvOrderReport.ExportSettings.HideStructureColumns = true;
                gvOrderReport.ExportSettings.ExportOnlyData = true;
                gvOrderReport.ExportSettings.FileName = "Initial Order Report AMC/RTA Wise";
                gvOrderReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvOrderReport.MasterTableView.ExportToExcel();
            }
            else if (ddlOrderType.SelectedValue == "2")
            {
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                rgFATCA.ExportSettings.OpenInNewWindow = true;
                rgFATCA.ExportSettings.IgnorePaging = true;
                rgFATCA.ExportSettings.HideStructureColumns = true;
                rgFATCA.ExportSettings.ExportOnlyData = true;
                rgFATCA.ExportSettings.FileName = "FATCA Report For " + ddlAMC.SelectedItem.Text + " " + fromdate.ToShortDateString() + "-" + todate.ToShortDateString();
                rgFATCA.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                rgFATCA.MasterTableView.ExportToExcel();
            }
            else if (ddlOrderType.SelectedValue == "3")
            {
                gvCustomerDetails.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
                gvCustomerDetails.ExportSettings.OpenInNewWindow = true;
                gvCustomerDetails.ExportSettings.IgnorePaging = true;
                gvCustomerDetails.ExportSettings.HideStructureColumns = true;
                gvCustomerDetails.ExportSettings.ExportOnlyData = true;
                gvCustomerDetails.ExportSettings.FileName = "Report for Extract BSE Client";
                gvCustomerDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvCustomerDetails.MasterTableView.ExportToExcel();

            }
        }
        protected void gvAdviserIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsGetAdviserissueallotmentList = new DataSet();
            dsGetAdviserissueallotmentList = (DataSet)Cache["AdviserIssueList" + advisorVo.advisorId];
            if (dsGetAdviserissueallotmentList != null)
            {
                gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
            }
        }
        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("_", " ");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
            foreach (GridDataItem dataItem in gvCustomerDetails.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chk") as CheckBox).Checked == true)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select a customer!');", true);
                return;
            }
            else
            {
                DataTable dtcustomer = new DataTable();
                dtcustomer.Columns.Add("customerId", typeof(Int32));
                dtcustomer.Columns.Add("demataccepted", typeof(int));
                DataRow drcustomer;
                foreach (GridDataItem radItem in gvCustomerDetails.MasterTableView.Items)
                {

                    if ((radItem.FindControl("chk") as CheckBox).Checked == true)
                    {
                        drcustomer = dtcustomer.NewRow();
                        drcustomer["customerId"] = int.Parse(gvCustomerDetails.MasterTableView.DataKeyValues[radItem.ItemIndex]["C_CustomerId"].ToString());
                        CheckBox chk = radItem.FindControl("chk") as CheckBox;
                        drcustomer["demataccepted"] = chk.Checked ? 1 : 0;
                        dtcustomer.Rows.Add(drcustomer);
                    }
                }
                OnlineOrderBackOfficeBo.UpdateCustomerCode(dtcustomer, userVo.UserId);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Selected Customer Marked as Demat Investor!!');", true);
            }

        }

        //protected void gvCustomerDetails_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    gvCustomerDetails.MasterTableView.GetColumn("C_IsDematInvestor").Visible = true;


        //    if (userVo.UserType == "Advisor")
        //    {
        //        //if (rbtnProspect.Checked)
        //        //    producttype = 2;
        //        //else if (rbtnFpClient.Checked)
        //        //    producttype = 1;
        //        if (producttype == 2)
        //        {
        //            gvCustomerDetails.MasterTableView.GetColumn("Action").Visible = false;
        //            gvCustomerDetails.MasterTableView.GetColumn("MarkFPClient").Visible = false;
        //            gvCustomerDetails.MasterTableView.GetColumn("ActionForProspect").Visible = true;

        //        }
        //        else
        //        {
        //            //gvCustomerDetails1.MasterTableView.GetColumn("").Visible = true;
        //            //gvCustomerDetails1.MasterTableView.GetColumn("").Visible = true;
        //            //gvCustomerDetails1.MasterTableView.GetColumn("").Visible = false;
        //        }
        //        return;
        //    }
        //    if (userVo.UserType == "Associates")
        //    {
        //        gvCustomerDetails.MasterTableView.GetColumn("Action").Visible = false;
        //        gvCustomerDetails.MasterTableView.GetColumn("MarkFPClient").Visible = false;
        //        gvCustomerDetails.MasterTableView.GetColumn("ActionForProspect").Visible = false;
        //    }
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = (GridDataItem)e.Item;
        //        Boolean Iscancel = Convert.ToBoolean(gvCustomerDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_IsDematInvestor"]);
        //        CheckBox chk = (CheckBox)e.Item.FindControl("chk");
        //        if (Iscancel)
        //            chk.Visible = false;
        //        else
        //            chk.Visible = true;
        //    }
        //}

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            int CustomerId = 0;
            foreach (GridDataItem gdi in gvCustomerDetails.Items)
            {
                int selectedRow = gdi.ItemIndex+1;
                CustomerId = int.Parse(gvCustomerDetails.MasterTableView.DataKeyValues[selectedRow - 1]["Customer Id"].ToString());
                bool result = onlineOrderBackOfficeBo.UpdateFlag(CustomerId);
            }


        }
    }
}
    
