using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Configuration;
using BoCustomerPortfolio;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using WealthERP.Base;
using BoCommon;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BoUploads;

namespace WealthERP.Advisor
{
    public partial class RMAMCWiseMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string userType;
        int advisorId;
        int userId;
        int rmid;
        DateTime LatestValuationdate = new DateTime();
        bool GridViewCultureFlag = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session["UserType"].ToString().ToLower();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            rmid = rmVo.RMId;
            trMessage.Visible = false;
            if (!IsPostBack)
            {
                PortfolioBo portfoliobo = new PortfolioBo();
                string valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                hdnValuationDate.Value = valuedate.ToString();
                if (userType == "rm")
                {
                    spnBranch.Visible = false;
                    spnRM.Visible = false;
                }
                if (userType == "adviser")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                if (valuedate != "")
                {
                    LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                    hdnValuationDate.Value = LatestValuationdate.ToString();
                }
                if (LatestValuationdate != DateTime.MinValue)
                {
                    txtDate.Text = LatestValuationdate.Date.ToShortDateString();
                    bindgrid(LatestValuationdate);
                }
                else
                {
                    lblMessage.Text = "No valuation done";
                    trMessage.Visible = true;
                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            LatestValuationdate = Convert.ToDateTime(txtDate.Text);
            hdnValuationDate.Value = LatestValuationdate.ToString();
            TextBox AMCSearchval = new TextBox();
            if (gvMFMIS.HeaderRow != null)
            {
                AMCSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch"); //GetAMCTextBox();
            }
            hdnAMCSearchVal.Value = AMCSearchval.Text;
            
            bindgrid(LatestValuationdate);
        }

        private void bindgrid(DateTime Valuationdate)
        {
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            double totalAum = 0;
            double totalUnits = 0;
            decimal TotalAumPercentage=0;
            int rmId, branchId;
            int.TryParse(ddlBranch.SelectedValue, out branchId);
            int.TryParse(ddlRM.SelectedValue, out rmId);
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());
            if (userType == "rm")
                dsMISReport = adviserMISBo.GetAMCwiseMISForRM(rmid, Valuation_Date, hdnAMCSearchVal.Value.ToString());
            else
                dsMISReport = adviserMISBo.GetAMCwiseMISForAdviser(advisorVo.advisorId, branchId, rmId, Valuation_Date, hdnAMCSearchVal.Value.ToString());

            if (dsMISReport.Tables[0].Rows.Count == 0)
            {
                trMessage.Visible = true;
                gvMFMIS.DataSource = dsMISReport;
                gvMFMIS.DataBind();
            }
            else
            {
                double no_of_units = 0;
                double aum = 0;
                decimal AumPercentage = 0;
                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    dsMISReport.Tables[0].Rows[i]["AUM"] = Decimal.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i]["AUM"].ToString()));
                    //dsMISReport.Tables[0].Rows[i]["Percentage"] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()).ToString();
                        //decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                foreach (DataRow dr in dsMISReport.Tables[0].Rows)
                {

                    //no_of_units = Convert.ToDouble(dr["Units"].ToString());
                    aum = Convert.ToInt32(dr["AUM"].ToString());
                    AumPercentage = decimal.Parse(dr["Percentage"].ToString());
                    //totalUnits = totalUnits + no_of_units;
                    totalAum = totalAum + aum;
                    TotalAumPercentage = TotalAumPercentage + AumPercentage;

                }

                DataTable dtMISReport = new DataTable();

                dtMISReport.Columns.Add("AMC");
                dtMISReport.Columns.Add("AMCCode");
                dtMISReport.Columns.Add("AUM");
                dtMISReport.Columns.Add("Percentage");
               

                DataRow drMISReport;

                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    drMISReport = dtMISReport.NewRow();

                    drMISReport[0] = dsMISReport.Tables[0].Rows[i]["AMC"].ToString();
                    drMISReport[1] = dsMISReport.Tables[0].Rows[i]["AMCCode"].ToString();
                  
                    if (GridViewCultureFlag == true)
                    {
                        decimal tempAum = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i]["AUM"].ToString()), 2);
                        drMISReport[2] = tempAum.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        
                    }
                    else
                    {
                        //decimal tempAum = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][2].ToString()), 2);
                        //drMISReport[2] = tempAum.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMISReport[2] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["AUM"].ToString());
                    }

                    drMISReport[3] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()), 2).ToString();



                    dtMISReport.Rows.Add(drMISReport);

                }


                gvMFMIS.DataSource = dtMISReport;
                gvMFMIS.DataBind();

                if (GridViewCultureFlag == true)
                {
                    decimal tempTotalAum = System.Math.Round(decimal.Parse(totalAum.ToString()), 2);
                    gvMFMIS.FooterRow.Cells[2].Text = tempTotalAum.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    gvMFMIS.FooterRow.Cells[3].Text = TotalAumPercentage.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
 
                }
                else
                {
                    decimal tempTotalAum = System.Math.Round(decimal.Parse(totalAum.ToString()), 4);
                    gvMFMIS.FooterRow.Cells[2].Text = tempTotalAum.ToString();

                    gvMFMIS.FooterRow.Cells[3].Text = TotalAumPercentage.ToString();

                }
                
            }

            ViewState["Valuationdate"] = Valuationdate.ToString();
            TextBox txtamc = new TextBox();
            TextBox txtscheme = new TextBox();

            if (gvMFMIS.HeaderRow != null)
            {
                txtamc = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");
            }
            txtamc.Text = hdnAMCSearchVal.Value.ToString();
        }

        protected void gvMFMIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int amcCode;
            amcCode = int.Parse(gvMFMIS.SelectedDataKey["AMCCode"].ToString());
            LatestValuationdate = Convert.ToDateTime(txtDate.Text).Date;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMAMCSchemewiseMIS','?amcCode=" + amcCode + "&latestValuationdate=" + LatestValuationdate.ToShortDateString() + "');", true);

        }

        protected void ddlMISType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCSchemeWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "FolioWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMCustomerAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "TurnOverSummary")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TextBox AMCSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");

            hdnAMCSearchVal.Value = AMCSearchval.Text;
            LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());

            bindgrid(LatestValuationdate);

        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            gvMFMIS.Columns[0].Visible = false;

            GridViewCultureFlag = false;
            bindgrid(LatestValuationdate);
            GridViewCultureFlag = true;

            PrepareGridViewForExport(gvMFMIS);
            ExportGridView("Excel");

            //LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());

            //bindgrid(LatestValuationdate);
        }

        private void ExportGridView(string Filetype)
        {
            LatestValuationdate = Convert.ToDateTime(hdnValuationDate.Value.ToString());
            HtmlForm frm = new HtmlForm();
            frm.Controls.Clear();
            frm.Attributes["runat"] = "server";
            if (Filetype == "Excel")
            {

                string temp = rmVo.FirstName + rmVo.LastName + "'s_MF_MIS.xls";
                string attachment = "attachment; filename=" + temp;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);


                //Add details for the header

                Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
                Response.Output.Write("Report  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td><b>");
                Response.Output.Write("AMC Wise AUM</b>");
                Response.Output.Write("</td></tr><tr><td>");
                Response.Output.Write("As on Date : ");
                Response.Output.Write("</td><td><b>");
                System.DateTime tDate1 = LatestValuationdate;
                Response.Output.Write(tDate1.ToShortDateString());
                Response.Output.Write("</b></td></tr>");
                Response.Output.Write("</tbody></table>");



                if (gvMFMIS.HeaderRow != null)
                {
                    PrepareControlForExport(gvMFMIS.HeaderRow);
                }
                foreach (GridViewRow row in gvMFMIS.Rows)
                {
                    PrepareControlForExport(row);
                }
                if (gvMFMIS.FooterRow != null)
                {
                    PrepareControlForExport(gvMFMIS.FooterRow);
                }

                gvMFMIS.Parent.Controls.Add(frm);
                frm.Controls.Add(gvMFMIS);
                frm.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
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
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
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

        private void BindBranchDropDown()
        {
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                ddlRM.Items.Clear();
                if (ddlBranch.SelectedValue != "All")
                    dt = advisorStaffBo.GetBranchRMList(int.Parse(ddlBranch.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}