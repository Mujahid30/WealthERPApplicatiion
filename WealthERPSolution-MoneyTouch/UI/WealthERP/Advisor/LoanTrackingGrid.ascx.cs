using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Xml;
using System.Text;
using iTextSharp.text.html.simpleparser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class LoanTrackingGrid : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        DataSet dsloanProposal = new DataSet();
        LoanProposalVo loanProposalVo = new LoanProposalVo();
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        CustomerBo customerBo = new CustomerBo();
        List<CustomerVo> customerList = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int loanProposalId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 15;
                    mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 15) != 0)
                        lowerlimit = ((mypager.CurrentPage - 1) * 15).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 15).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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

                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx.cs:GetPageCount()");

                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindLoanProposal(mypager.CurrentPage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = mypager.CurrentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (!IsPostBack)
            {
                this.BindLoanProposal(mypager.CurrentPage);
            }
        }

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            DropDownList ddlAction = null;
            GridViewRow gvr = null;
            int selectedRow = 0;
            int userId = 0;
            UserVo tempUser = null;
            UserBo userBo = new UserBo();

            try
            {
                ddlAction = (DropDownList)sender;
                gvr = (GridViewRow)ddlAction.NamingContainer;
                selectedRow = gvr.RowIndex;
                loanProposalId = int.Parse(gvLoanProposals.DataKeys[selectedRow].Values["LoanProposalId"].ToString());
                dsloanProposal = liabilitiesBo.GetLoanProposalDetails(loanProposalId, rmVo.RMId);
                Session[SessionContents.LoanProposalDataSet] = dsloanProposal;

                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    Session["LoanProcessAction"] = "view";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LoanProcessTracking','login');", true);
                }
                else if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    Session["LoanProcessAction"] = "edit";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LoanProcessTracking','login');", true);
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindLoanProposal(int CurrentPage)
        {
            Dictionary<string, string> genDictLoanType = new Dictionary<string, string>();
            Dictionary<string, string> genDictLoanStage = new Dictionary<string, string>();

            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
            List<LoanProposalVo> proposalList = new List<LoanProposalVo>();
            DataTable dtLoanProposals = new DataTable();
            try
            {
                DropDownList ddl = new DropDownList();
                Label lbl = new Label();

                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                int Count = 0;

                proposalList = liabilitiesBo.GetLoanProposalList(rmVo.RMId, mypager.CurrentPage, out Count, hdnSort.Value, hdnNameFilter.Value, hdnLoanTypeFilter.Value, hdnLoanStageFilter.Value, out genDictLoanType, out genDictLoanStage);

                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                if (proposalList == null)
                {
                    hdnRecordCount.Value = "0";
                    trMessage.Visible = true;
                    lblMessage.Visible = true;
                    trPager.Visible = false;
                    lblTotalRows.Visible = false;
                    //tblGV.Visible = false;
                }
                else
                {
                    trMessage.Visible = false;
                    lblMessage.Visible = false;
                    trPager.Visible = true;
                    lblTotalRows.Visible = true;

                    dtLoanProposals.Columns.Add("LoanProposalId");
                    dtLoanProposals.Columns.Add("CustomerName");
                    dtLoanProposals.Columns.Add("LoanType");
                    dtLoanProposals.Columns.Add("LoanStage");
                    dtLoanProposals.Columns.Add("LoanAmount");
                    dtLoanProposals.Columns.Add("LoanPartner");
                    dtLoanProposals.Columns.Add("Remarks");
                    dtLoanProposals.Columns.Add("Commission");

                    DataRow drRMCustomer;

                    for (int i = 0; i < proposalList.Count; i++)
                    {
                        drRMCustomer = dtLoanProposals.NewRow();
                        loanProposalVo = new LoanProposalVo();
                        loanProposalVo = proposalList[i];
                        drRMCustomer[0] = loanProposalVo.LoanProposalId.ToString();
                        drRMCustomer[1] = loanProposalVo.CustomerName.ToString();
                        drRMCustomer[2] = loanProposalVo.LoanType.ToString();
                        drRMCustomer[3] = loanProposalVo.LoanStage.ToString();
                        drRMCustomer[4] = loanProposalVo.AppliedLoanAmount.ToString("f2");
                        drRMCustomer[5] = loanProposalVo.LoanPartner.ToString();
                        drRMCustomer[6] = loanProposalVo.Remark.ToString();
                        drRMCustomer[7] = loanProposalVo.Commission.ToString("f2");

                        dtLoanProposals.Rows.Add(drRMCustomer);
                    }

                    gvLoanProposals.DataSource = dtLoanProposals;
                    gvLoanProposals.DataBind();

                    if (genDictLoanType.Count > 0)
                    {
                        DropDownList ddlLoanType = GetLoanTypeDDL();
                        if (ddlLoanType != null)
                        {
                            ddlLoanType.DataSource = genDictLoanType;
                            ddlLoanType.DataTextField = "Key";
                            ddlLoanType.DataValueField = "Value";
                            ddlLoanType.DataBind();
                            ddlLoanType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnLoanTypeFilter.Value != "")
                        {
                            ddlLoanType.SelectedValue = hdnLoanTypeFilter.Value.ToString();
                        }
                    }

                    if (genDictLoanStage.Count > 0)
                    {
                        DropDownList ddlLoanStage = GetLoanStageDDL();
                        if (ddlLoanStage != null)
                        {
                            ddlLoanStage.DataSource = genDictLoanStage;
                            ddlLoanStage.DataTextField = "Key";
                            ddlLoanStage.DataValueField = "Value";
                            ddlLoanStage.DataBind();
                            ddlLoanStage.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        if (hdnLoanStageFilter.Value != "")
                        {
                            ddlLoanStage.SelectedValue = hdnLoanStageFilter.Value.ToString();
                        }
                    }

                    TextBox txtName = GetCustNameTextBox();
                    if (txtName != null)
                    {
                        if (hdnNameFilter.Value != "")
                        {
                            txtName.Text = hdnNameFilter.Value.ToString();
                        }
                    }

                    this.GetPageCount();
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
                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx.cs:BindLoanProposal()");
                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = proposalList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvLoanProposals_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindLoanProposal(mypager.CurrentPage);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindLoanProposal(mypager.CurrentPage);
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
                FunctionInfo.Add("Method", "LoanTrackingGrid.ascx.cs:gvLoanProposals_Sort()");
                object[] objects = new object[1];
                objects[0] = sortExpression;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvLoanProposals.Columns[0].Visible = false;
            gvLoanProposals.HeaderRow.Visible = true;
            //if (rbtnMultiple.Checked)
            //{

            //    BindGrid(mypager.CurrentPage, 1);

            //}
            //else
            //{
            //    BindGrid(mypager.CurrentPage, 0);
            //}

            //PrepareGridViewForExport(gvCustomers);
            //if (rbtnExcel.Checked)
            //{
            //    ExportGridView("Excel");
            //}
            //else if (rbtnPDF.Checked)
            //{

            //    ExportGridView("PDF");
            //}
            //else if (rbtnWord.Checked)
            //{
            //    ExportGridView("Word");
            //}
            //BindGrid(mypager.CurrentPage, 0);
            //gvCustomers.Columns[0].Visible = true;
        }

        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedValue.ToString();
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }

            }


        }

        //private void ExportGridView(string Filetype)
        //{
        //    {
        //        HtmlForm frm = new HtmlForm();
        //        frm.Controls.Clear();
        //        frm.Attributes["runat"] = "server";
        //        if (Filetype == "Excel")
        //        {
        //            // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);
        //            string temp = userVo.FirstName + userVo.LastName + "Customer.xls";
        //            string attachment = "attachment; filename=" + temp;
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", attachment);
        //            Response.ContentType = "application/ms-excel";
        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter htw = new HtmlTextWriter(sw);
        //            Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
        //            Response.Output.Write("Advisor Name : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write(userVo.FirstName + userVo.LastName);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("<tr><td>");
        //            Response.Output.Write("Report  : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write("Customer List");
        //            Response.Output.Write("</td></tr><tr><td>");
        //            Response.Output.Write("Date : ");
        //            Response.Output.Write("</td><td>");
        //            System.DateTime tDate1 = System.DateTime.Now;
        //            Response.Output.Write(tDate1);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("</tbody></table>");
        //            if (gvCustomers.HeaderRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.HeaderRow);
        //            }
        //            foreach (GridViewRow row in gvCustomers.Rows)
        //            {
        //                PrepareControlForExport(row);
        //            }
        //            if (gvCustomers.FooterRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.FooterRow);
        //            }
        //            gvCustomers.Parent.Controls.Add(frm);
        //            frm.Controls.Add(gvCustomers);
        //            frm.RenderControl(htw);
        //            HttpContext.Current.Response.Write(sw.ToString());
        //            HttpContext.Current.Response.End();
        //        }


        //        else if (Filetype == "PDF")
        //        {
        //            string temp = userVo.FirstName + userVo.LastName + "_Customer List";

        //            gvCustomers.AllowPaging = false;
        //            gvCustomers.DataBind();
        //            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvCustomers.Columns.Count - 1);

        //            table.HeaderRows = 4;
        //            iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
        //            Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
        //            PdfPCell clApplicationName = new PdfPCell(phApplicationName);
        //            clApplicationName.Border = PdfPCell.NO_BORDER;
        //            clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


        //            Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
        //            PdfPCell clDate = new PdfPCell(phDate);
        //            clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
        //            clDate.Border = PdfPCell.NO_BORDER;


        //            headerTable.AddCell(clApplicationName);
        //            headerTable.AddCell(clDate);
        //            headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

        //            PdfPCell cellHeader = new PdfPCell(headerTable);
        //            cellHeader.Border = PdfPCell.NO_BORDER;
        //            cellHeader.Colspan = gvCustomers.Columns.Count - 1;
        //            table.AddCell(cellHeader);

        //            Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
        //            PdfPCell clHeader = new PdfPCell(phHeader);
        //            clHeader.Colspan = gvCustomers.Columns.Count - 1;
        //            clHeader.Border = PdfPCell.NO_BORDER;
        //            clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //            table.AddCell(clHeader);


        //            Phrase phSpace = new Phrase("\n");
        //            PdfPCell clSpace = new PdfPCell(phSpace);
        //            clSpace.Border = PdfPCell.NO_BORDER;
        //            clSpace.Colspan = gvCustomers.Columns.Count - 1;
        //            table.AddCell(clSpace);

        //            GridViewRow HeaderRow = gvCustomers.HeaderRow;
        //            if (HeaderRow != null)
        //            {
        //                string cellText = "";
        //                for (int j = 1; j < gvCustomers.Columns.Count; j++)
        //                {
        //                    if (j == 1)
        //                    {
        //                        cellText = "Parent";
        //                    }
        //                    else if (j == 2)
        //                    {
        //                        cellText = "Customer Name / Company Name";
        //                    }
        //                    else if (j == 6)
        //                    {
        //                        cellText = "Area";
        //                    }
        //                    else if (j == 8)
        //                    {
        //                        cellText = "Pincode";
        //                    }
        //                    else if (j == 9)
        //                    {
        //                        cellText = "Assigned RM";
        //                    }
        //                    else
        //                    {
        //                        cellText = Server.HtmlDecode(gvCustomers.HeaderRow.Cells[j].Text);
        //                    }

        //                    Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
        //                    table.AddCell(ph);
        //                }

        //            }

        //            for (int i = 0; i < gvCustomers.Rows.Count; i++)
        //            {
        //                string cellText = "";
        //                if (gvCustomers.Rows[i].RowType == DataControlRowType.DataRow)
        //                {
        //                    for (int j = 1; j < gvCustomers.Columns.Count; j++)
        //                    {
        //                        if (j == 1)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblParenteHeader")).Text;
        //                        }
        //                        else if (j == 2)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblCustNameHeader")).Text;
        //                        }
        //                        else if (j == 6)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAreaHeader")).Text;
        //                        }
        //                        else if (j == 8)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblPincodeHeader")).Text;
        //                        }
        //                        else if (j == 9)
        //                        {
        //                            cellText = ((Label)gvCustomers.Rows[i].FindControl("lblAssignedRMHeader")).Text;
        //                        }
        //                        else
        //                        {
        //                            cellText = Server.HtmlDecode(gvCustomers.Rows[i].Cells[j].Text);
        //                        }
        //                        Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
        //                        iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
        //                        table.AddCell(ph);

        //                    }

        //                }

        //            }



        //            //Create the PDF Document

        //            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();
        //            pdfDoc.Add(table);
        //            pdfDoc.Close();
        //            Response.ContentType = "application/pdf";
        //            temp = "filename=" + temp + ".pdf";
        //            //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
        //            Response.AddHeader("content-disposition", "attachment;" + temp);
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.Write(pdfDoc);
        //            Response.End();



        //        }
        //        else if (Filetype == "Word")
        //        {
        //            gvCustomers.Columns.Remove(this.gvCustomers.Columns[0]);
        //            string temp = userVo.FirstName + userVo.LastName + "_Customer.doc";
        //            string attachment = "attachment; filename=" + temp;
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", attachment);
        //            Response.ContentType = "application/msword";
        //            StringWriter sw = new StringWriter();
        //            HtmlTextWriter htw = new HtmlTextWriter(sw);

        //            Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
        //            Response.Output.Write("Advisor Name : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write(userVo.FirstName + userVo.LastName);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("<tr><td>");
        //            Response.Output.Write("Report  : ");
        //            Response.Output.Write("</td>");
        //            Response.Output.Write("<td>");
        //            Response.Output.Write("Customer List");
        //            Response.Output.Write("</td></tr><tr><td>");
        //            Response.Output.Write("Date : ");
        //            Response.Output.Write("</td><td>");
        //            System.DateTime tDate1 = System.DateTime.Now;
        //            Response.Output.Write(tDate1);
        //            Response.Output.Write("</td></tr>");
        //            Response.Output.Write("</tbody></table>");
        //            if (gvCustomers.HeaderRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.HeaderRow);
        //            }
        //            foreach (GridViewRow row in gvCustomers.Rows)
        //            {
        //                PrepareControlForExport(row);
        //            }
        //            if (gvCustomers.FooterRow != null)
        //            {
        //                PrepareControlForExport(gvCustomers.FooterRow);
        //            }
        //            gvCustomers.Parent.Controls.Add(frm);
        //            frm.Controls.Add(gvCustomers);
        //            frm.RenderControl(htw);
        //            Response.Write(sw.ToString());
        //            Response.End();

        //        }

        //    }

        //}

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader
            ("Content-Disposition", "attachment; filename=" + strS);
            Response.TransmitFile(strS);
            Response.End();
            Response.Flush();
            Response.Clear();

        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvLoanProposals.HeaderRow != null)
            {
                if ((TextBox)gvLoanProposals.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvLoanProposals.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private DropDownList GetLoanTypeDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvLoanProposals.HeaderRow != null)
            {
                if ((DropDownList)gvLoanProposals.HeaderRow.FindControl("ddlLoanType") != null)
                {
                    ddl = (DropDownList)gvLoanProposals.HeaderRow.FindControl("ddlLoanType");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetLoanStageDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvLoanProposals.HeaderRow != null)
            {
                if ((DropDownList)gvLoanProposals.HeaderRow.FindControl("ddlLoanStage") != null)
                {
                    ddl = (DropDownList)gvLoanProposals.HeaderRow.FindControl("ddlLoanStage");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindLoanProposal(mypager.CurrentPage);
            }
        }

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlLoanType = GetLoanTypeDDL();
            if (ddlLoanType != null)
            {
                if (ddlLoanType.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnLoanTypeFilter.Value = ddlLoanType.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnLoanTypeFilter.Value = "";
                }
                this.BindLoanProposal(mypager.CurrentPage);
            }
        }

        protected void ddlLoanStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlLoanStage = GetLoanStageDDL();
            if (ddlLoanStage != null)
            {
                if (ddlLoanStage.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnLoanStageFilter.Value = ddlLoanStage.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnLoanStageFilter.Value = "";
                }
                this.BindLoanProposal(mypager.CurrentPage);
            }
        }


    }
}