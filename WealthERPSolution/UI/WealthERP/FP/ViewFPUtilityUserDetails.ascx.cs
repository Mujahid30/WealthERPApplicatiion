using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerRiskProfiling;
using BoCommon;
using BoUser;
using VoUser;
using Telerik.Web.UI;
using WealthERP.Base;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using BoFPUtility;

namespace WealthERP.FP
{
    public partial class ViewFPUtilityUserDetails : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo adviserVo = new AdvisorVo();
        FPUserBO fpUserBo = new FPUserBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
             adviserVo = (AdvisorVo)Session["advisorVo"];
             if (!IsPostBack)
             {
                 txtToDate.SelectedDate = DateTime.Now;
                 txtFromDate.SelectedDate = DateTime.Now.AddDays(-7);
             }
        }

        protected void gvLeadList_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName == "OpenQnA")
            {
                GridDataItem item = e.Item as GridDataItem;
                int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["FPUUD_UserId"].ToString());
                string spName = item.OwnerTableView.DataKeyValues[item.ItemIndex]["FPUUD_Name"].ToString();
                //lbl1.Text = id.ToString() + "_" + spName;
                //placeholder.Controls.Add(new LiteralControl());
                BindQnA(id);
                string script = "function f(){radopen(null, 'UserListDialog1'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customQnAOpener", script, true);
            }

           else if (e.CommandName == "OpenQnAEdit")
            {
                GridDataItem item = e.Item as GridDataItem;
                int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["FPUUD_UserId"].ToString());
                string spName = item.OwnerTableView.DataKeyValues[item.ItemIndex]["FPUUD_Name"].ToString();
                //lbl1.Text = id.ToString() + "_" + spName;
                //placeholder.Controls.Add(new LiteralControl());
                BindQnAEdit(id);
                string script = "function f(){radopen(null, 'UserListDialog1'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customQnAOpener", script, true);
            }



        }
        protected void gvLeadList_OnNeedDataSource(Object sender, GridNeedDataSourceEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["gvLeadList"];
            gvLeadList.DataSource = ds;
        }
    
        protected void BindGrid()
        {
            RiskProfileBo bo = new RiskProfileBo();
            DataSet ds = new DataSet();
            ds = bo.GetFPUtilityUserDetailsList(Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate));
            gvLeadList.DataSource = ds;
            gvLeadList.DataBind();
            gvLeadList.Visible = true;
            if (Cache["gvLeadList"] == null)
            {
                Cache.Insert("gvLeadList", ds);
            }
            else
            {
                Cache.Remove("gvLeadList");
                Cache.Insert("gvLeadList", ds);
            }
        }
        protected void ddlActionForProspect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = 0;
            UserBo userBo = new UserBo();
            bool isGrpHead = false;
            CustomerVo customerVo = new CustomerVo();
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerBo customerBo = new CustomerBo();
            int ParentId;
            if (Session[SessionContents.PortfolioId] != null)
            {
                Session.Remove(SessionContents.PortfolioId);
            }
            LinkButton lnkAction = (LinkButton)sender;
            //RadComboBox ddlAction = (RadComboBox)sender;
            GridDataItem item = (GridDataItem)lnkAction.NamingContainer;
            ParentId = int.Parse(gvLeadList.MasterTableView.DataKeyValues[item.ItemIndex]["C_CustomerId"].ToString());

            Session["ParentIdForDelete"] = ParentId;
            customerVo = customerBo.GetCustomer(ParentId);
            Session["CustomerVo"] = customerVo;
            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);

            //to set portfolio Id and its details
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
            //Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            Session["customerPortfolioVo"] = customerPortfolioVo;

            Session["IsDashboard"] = "false";
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
            if (customerVo.IsProspect == 0)
            {
                //Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                //Session["customerPortfolioVo"] = customerPortfolioVo;
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
            }
            else
            {
                isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                if (isGrpHead == false)
                {
                    ParentId = customerBo.GetCustomerGroupHead(ParentId);
                }
                else
                {
                    ParentId = customerVo.CustomerId;
                }
                Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
            }

        }
        private void BindQnA(int id)
        {
            DataSet dsQuestionNOptions = fpUserBo.GetQuestionAndOptions(id);
            DataSet dsGetquestionList = new DataSet();
            dsGetquestionList = fpUserBo.GetRiskProfileQuestion(adviserVo.advisorId);
            int questionNo = 0;
            int questioncount = 1;
            foreach (DataRow dr in dsGetquestionList.Tables[0].Rows)
            {
                //lbl1.Text = "asa";
                questionNo = Convert.ToInt32(dr["QM_QuestionId"].ToString());
                //PlaceHolder placeholder = new PlaceHolder();
                placeholder.Controls.Add(new LiteralControl("<div class=\"well\"><div class=\"row\"><div class=\"col-sm-2  form-group\"></div><div class=\"col-sm-8  form-group\"><p>" + questioncount.ToString() + "." + dr["QM_Question"].ToString()
                    + "</p></div><div class=\"col-sm-2\"></div></div><div class=\"row\"><div class=\"col-sm-3\"></div><div class=\"col-sm-3\"></div><div class=\"col-sm-6\">"));

                placeholder.Controls.Add(new LiteralControl("</div></div>"));

                DataSet ds = new DataSet();
                ds = fpUserBo.GetQuestionOption(Convert.ToInt32(dr["QM_QuestionId"].ToString()), adviserVo.advisorId);
                int optionNo = 1;
                foreach (DataRow droption in ds.Tables[0].Rows)
                {
                    optionNo = Convert.ToInt32(droption["QOM_OptionId"].ToString());
                    placeholder.Controls.Add(new LiteralControl("<div class=\"row\"><div class=\"col-sm-3  form-group\"></div><div class=\"col-sm-6  form-group\"><div class=\"radio radio-info radio-inline\">"));
                    RadioButton rbtn = new RadioButton();
                    rbtn.ID = "rbtn" + questionNo.ToString() + optionNo.ToString();
                    rbtn.GroupName = questionNo.ToString();
                    //rbtn.AccessKey = optionNo.ToString();
                    rbtn.ValidationGroup = optionNo.ToString();
                    rbtn.Enabled = false;
                    if (dsQuestionNOptions.Tables[0].Rows.Count > 0)
                    {
                        string expression;
                        expression = "AQM_QuestionId=" + questionNo.ToString();
                        DataRow[] foundRows;
                        foundRows = dsQuestionNOptions.Tables[0].Select(expression);
                        if (foundRows.Length > 0)
                        {
                            if (foundRows[0]["AQOM_OptionId"].ToString() == optionNo.ToString())
                                rbtn.Checked = true;
                        }
                    }
                    placeholder.Controls.Add(rbtn);
                    placeholder.Controls.Add(new LiteralControl("<label for=\"" + rbtn.ID + "\">" + droption["QOM_Option"].ToString()
                        + "</label></div></div><div class=\"col-sm-3\"></div></div>"));

                }
                questioncount++;
            }
            
        }


        private void BindQnAEdit(int id)
        {
            DataSet dsQuestionNOptions = fpUserBo.GetQuestionAndOptions(id);
            DataSet dsGetquestionList = new DataSet();
            dsGetquestionList = fpUserBo.GetRiskProfileQuestion(adviserVo.advisorId);
            int questionNo = 0;
            int questioncount = 1;
            foreach (DataRow dr in dsGetquestionList.Tables[0].Rows)
            {
                //lbl1.Text = "asa";
                questionNo = Convert.ToInt32(dr["QM_QuestionId"].ToString());
                //PlaceHolder placeholder = new PlaceHolder();
                placeholder.Controls.Add(new LiteralControl("<div class=\"well\"><div class=\"row\"><div class=\"col-sm-2  form-group\"></div><div class=\"col-sm-8  form-group\"><p>" + questioncount.ToString() + "." + dr["QM_Question"].ToString()
                    + "</p></div><div class=\"col-sm-2\"></div></div><div class=\"row\"><div class=\"col-sm-3\"></div><div class=\"col-sm-3\"></div><div class=\"col-sm-6\">"));

                placeholder.Controls.Add(new LiteralControl("</div></div>"));

                DataSet ds = new DataSet();
                ds = fpUserBo.GetQuestionOption(Convert.ToInt32(dr["QM_QuestionId"].ToString()), adviserVo.advisorId);
                int optionNo = 1;
                foreach (DataRow droption in ds.Tables[0].Rows)
                {
                    optionNo = Convert.ToInt32(droption["QOM_OptionId"].ToString());
                    placeholder.Controls.Add(new LiteralControl("<div class=\"row\"><div class=\"col-sm-3  form-group\"></div><div class=\"col-sm-6  form-group\"><div class=\"radio radio-info radio-inline\">"));
                    RadioButton rbtn = new RadioButton();
                    rbtn.ID = "rbtn" + questionNo.ToString() + optionNo.ToString();
                    rbtn.GroupName = questionNo.ToString();
                    //rbtn.AccessKey = optionNo.ToString();
                    rbtn.ValidationGroup = optionNo.ToString();
                    rbtn.Enabled = true;
                    if (dsQuestionNOptions.Tables[0].Rows.Count > 0)
                    {
                        string expression;
                        expression = "AQM_QuestionId=" + questionNo.ToString();
                        DataRow[] foundRows;
                        foundRows = dsQuestionNOptions.Tables[0].Select(expression);
                        if (foundRows.Length > 0)
                        {
                            if (foundRows[0]["AQOM_OptionId"].ToString() == optionNo.ToString())
                                rbtn.Checked = true;
                        }
                    }
                    placeholder.Controls.Add(rbtn);
                    placeholder.Controls.Add(new LiteralControl("<label for=\"" + rbtn.ID + "\">" + droption["QOM_Option"].ToString()
                        + "</label></div></div><div class=\"col-sm-3\"></div></div>"));

                }
                questioncount++;
            }

        }


        public void imgBtngvLeadList_OnClick(object sender, ImageClickEventArgs e)
        {
            gvLeadList.ExportSettings.OpenInNewWindow = true;
            gvLeadList.ExportSettings.IgnorePaging = true;
            gvLeadList.ExportSettings.HideStructureColumns = true;
            gvLeadList.ExportSettings.ExportOnlyData = true;
            gvLeadList.ExportSettings.FileName = "LeadList" ;
            gvLeadList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvLeadList.MasterTableView.ExportToExcel();
        }
        protected void Submit_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}