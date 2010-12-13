using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using WealthERP;
using BoCommon;
using BoCustomerPortfolio;
using VoUser;
using VoCustomerPortfolio;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerPortfolio : System.Web.UI.UserControl
    {
        string path = "";
        CustomerVo customerVo = new CustomerVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<CustomerPortfolioVo> customerPortfolioList = new List<CustomerPortfolioVo>();
        CustomerPortfolioVo customerPortfolioVo;
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["RMVo"];
           if(!IsPostBack)
                BindGridView();
           
        }

        private void BindGridView()
        {
            int count;

            DataTable dt = portfolioBo.GetRMCustomerPortfolios(rmVo.RMId, mypager.CurrentPage, out count, hdnNameFilter.Value);
            lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
            gvCustomerPortfolio.DataSource = dt;
            gvCustomerPortfolio.DataBind();
            GetPageCount();

            if (dt == null || dt.Rows.Count == 0)
            {
                trMessage.Visible = true;
                trPager.Visible = false;
                trPage.Visible = false;
            }
            else
            {
                trMessage.Visible = false;
                trPager.Visible = true;
                trPage.Visible = true;
                Label2.Visible = false;
            }
        }

        private DataTable BindPortfolioType(string path)
        {
            return XMLBo.GetPortfolioType(path);

        }

        private DropDownList GetDropDown()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvCustomerPortfolio.FindControl("ddlType") != null)
            {
                ddl = (DropDownList)gvCustomerPortfolio.FindControl("ddlType");
            }
            return ddl;
        }

        //protected void gvCustomerPortfolio_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerVo.CustomerId);

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("PortfolioId");
        //    dt.Columns.Add("lblPortfolioName");

        //    DataRowView drv = e.Row.DataItem as DataRowView;


        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        DropDownList ddlType;

        //        ddlType = e.Row.FindControl("ddlType") as DropDownList;

        //        RadioButton rbtn;
        //        rbtn = e.Row.FindControl("rbtnPortfolio") as RadioButton;
        //        if (ddlType != null)
        //        {

        //            ddlType.DataSource = BindPortfolioType(path);
        //            ddlType.DataTextField = "PortfolioType";
        //            ddlType.DataValueField = "PortfolioTypeCode";
        //            ddlType.DataBind();
        //            customerPortfolioVo = new CustomerPortfolioVo();
        //            customerPortfolioVo = customerPortfolioList[gvCustomerPortfolio.Rows.Count];
        //            /*Bind the Selected Value to the Drop Down*/

        //            ddlType.SelectedValue = customerPortfolioVo.PortfolioTypeCode.ToString();

        //            if (customerPortfolioVo.IsMainPortfolio == 1)
        //            {



        //                rbtn.Checked = true;

        //            }
        //            else
        //            {
        //                rbtn.Checked = false;
        //            }
        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        DropDownList ddlAddType;

        //        ddlAddType = e.Row.FindControl("ddlAddType") as DropDownList;
        //        if (ddlAddType != null)
        //        {
        //            ddlAddType.DataSource = BindPortfolioType(path);
        //            ddlAddType.DataTextField = "PortfolioType";
        //            ddlAddType.DataValueField = "PortfolioTypeCode";
        //            ddlAddType.DataBind();
        //            ddlAddType.Items.Insert(0, "Select the Portfolio Type");
        //        }

        //    }
        //}

        //protected void gvCustomerPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int index = int.Parse(gvCustomerPortfolio.SelectedRow.RowIndex.ToString());
        //    int portfolioId = int.Parse(gvCustomerPortfolio.DataKeys[index].Value.ToString());
        //    Session[SessionContents.PortfolioId] = portfolioId;
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
        //}

        protected void btnAddPortfolio_Click(object sender, EventArgs e)
        {
            string newPortfolioName = string.Empty;
            string newPMSIdentifier = string.Empty;
            string newPortfolioType = "RGL";
            int IsMainPortfolio;

            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            CustomerPortfolioVo newCustomerPortfolioVo = new CustomerPortfolioVo();




            GridViewRow footerRow = gvCustomerPortfolio.FooterRow;

            DropDownList ddl = (DropDownList)footerRow.FindControl("ddlAddType");
            if (footerRow.Enabled)
            {


                if (((TextBox)footerRow.FindControl("txtPortfolioName")).Text.Trim() != "")
                {
                    newPortfolioName = ((TextBox)footerRow.FindControl("txtPortfolioName")).Text;

                }
                if (((RadioButton)footerRow.FindControl("rbtnAddMainPortfolio")).Checked)
                {
                    IsMainPortfolio = 1;
                    foreach (GridViewRow oldrow in gvCustomerPortfolio.Rows)
                    {
                        ((RadioButton)oldrow.FindControl("rbtnPortfolio")).Checked = false;
                    }
                }
                else
                {
                    IsMainPortfolio = 0;
                }
                if (((DropDownList)footerRow.FindControl("ddlAddType")).SelectedItem.Value.Trim().ToString() != "")
                {

                    if (ddl.SelectedIndex != 0)
                    {
                        newPortfolioType = ddl.SelectedItem.Value.ToString();
                    }
                }
                if (((TextBox)footerRow.FindControl("txtPMSIdentifier")).Text.Trim() != "")
                {
                    if (newPortfolioType == "PMS")
                    {
                        newPMSIdentifier = ((TextBox)footerRow.FindControl("txtPMSIdentifier")).Text;
                    }


                }

                if (newPMSIdentifier == "" && newPortfolioName == "" && ddl.SelectedIndex == 0)
                {

                }
                else
                {
                    newCustomerPortfolioVo.CustomerId = customerVo.CustomerId;
                    newCustomerPortfolioVo.IsMainPortfolio = IsMainPortfolio;
                    newCustomerPortfolioVo.PMSIdentifier = newPMSIdentifier;
                    newCustomerPortfolioVo.PortfolioName = newPortfolioName;
                    newCustomerPortfolioVo.PortfolioTypeCode = newPortfolioType;


                    portfolioBo.CreateCustomerPortfolio(newCustomerPortfolioVo, userVo.UserId);

                    newPMSIdentifier = string.Empty;
                    newPortfolioName = string.Empty;

                }
            }



            foreach (GridViewRow dr in gvCustomerPortfolio.Rows)
            {


                if (((Label)dr.FindControl("lblPortfolioName")).Text.Trim() != "")
                {
                    newPortfolioName = ((Label)dr.FindControl("lblPortfolioName")).Text;

                }
                if (((RadioButton)dr.FindControl("rbtnPortfolio")).Checked)
                {
                    IsMainPortfolio = 1;
                }
                else
                {
                    IsMainPortfolio = 0;
                }
                if (((DropDownList)dr.FindControl("ddlType")).SelectedItem.Value.Trim().ToString() != "")
                {
                    DropDownList ddlType = (DropDownList)dr.FindControl("ddlType");

                    newPortfolioType = ddlType.SelectedItem.Value.ToString();
                }
                if (((Label)dr.FindControl("lblPMSIdentifier")).Text.Trim() != "")
                {
                    if (newPortfolioType == "PMS")
                    {
                        newPMSIdentifier = ((Label)dr.FindControl("lblPMSIdentifier")).Text;
                    }


                }
                else
                {
                    newPMSIdentifier = string.Empty;
                }

                newCustomerPortfolioVo.CustomerId = customerVo.CustomerId;
                newCustomerPortfolioVo.IsMainPortfolio = IsMainPortfolio;
                newCustomerPortfolioVo.PMSIdentifier = newPMSIdentifier;
                newCustomerPortfolioVo.PortfolioName = newPortfolioName;
                newCustomerPortfolioVo.PortfolioTypeCode = newPortfolioType;
                if (((Label)dr.FindControl("PortfolioId")).Text.Trim() != "")
                {
                    newCustomerPortfolioVo.PortfolioId = int.Parse(((Label)dr.FindControl("PortfolioId")).Text);

                }

                portfolioBo.UpdateCustomerPortfolio(newCustomerPortfolioVo, userVo.UserId);
                newPortfolioName = string.Empty;
                newPMSIdentifier = string.Empty;




            }

            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            // BindGrid

            BindGridView();


        }

        //protected void gvCustomerPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    string newPortfolioName = string.Empty;
        //    string newPMSIdentifier = string.Empty;
        //    string newPortfolioType = "RGL";
        //    int IsMainPortfolio;

        //    CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        //    CustomerPortfolioVo newCustomerPortfolioVo = new CustomerPortfolioVo();

        //    if (e.CommandName == "Edit")
        //    {


        //        //int index = Convert.ToInt32(e.CommandArgument);

        //        //int portfolioId = int.Parse(gvCustomerPortfolio.DataKeys[index].Value.ToString());
        //        //if (((Label)gvCustomerPortfolio.Rows[index].FindControl("lblPortfolioName")).Text.Trim() != "")
        //        //{
        //        //    newPortfolioName = ((Label)gvCustomerPortfolio.Rows[index].FindControl("lblPortfolioName")).Text;

        //        //}
        //        //if (((RadioButton)gvCustomerPortfolio.Rows[index].FindControl("rbtnPortfolio")).Checked)
        //        //{

        //        //    IsMainPortfolio = 1;
        //        //}
        //        //else
        //        //{
        //        //    IsMainPortfolio = 0;
        //        //}
        //        //if (((DropDownList)gvCustomerPortfolio.Rows[index].FindControl("ddlType")).SelectedItem.Value.Trim().ToString() != "")
        //        //{
        //        //    DropDownList ddl = (DropDownList)gvCustomerPortfolio.Rows[index].FindControl("ddlType");
        //        //    newPortfolioType = ddl.SelectedItem.Value.ToString();
        //        //}
        //        //if (((Label)gvCustomerPortfolio.Rows[index].FindControl("lblPMSIdentifier")).Text.Trim() != "")
        //        //{
        //        //    if (newPortfolioType == "PMS")
        //        //    {
        //        //        newPMSIdentifier = ((Label)gvCustomerPortfolio.Rows[index].FindControl("lblPMSIdentifier")).Text;
        //        //    }


        //        //}

        //        //newCustomerPortfolioVo.CustomerId = customerVo.CustomerId;
        //        //newCustomerPortfolioVo.IsMainPortfolio = IsMainPortfolio;
        //        //newCustomerPortfolioVo.PMSIdentifier = newPMSIdentifier;
        //        //newCustomerPortfolioVo.PortfolioName = newPortfolioName;
        //        //newCustomerPortfolioVo.PortfolioTypeCode = newPortfolioType;
        //        //if (((Label)gvCustomerPortfolio.Rows[index].FindControl("PortfolioId")).Text.Trim() != "")
        //        //{
        //        //    newCustomerPortfolioVo.PortfolioId = int.Parse(((Label)gvCustomerPortfolio.Rows[index].FindControl("PortfolioId")).Text);

        //        //}

        //        //portfolioBo.UpdateCustomerPortfolio(newCustomerPortfolioVo, userVo.UserId);
        //        //BindGridView();
        //    }
        //    //if (e.CommandName == "Select")
        //    //{
        //    //    int index = Convert.ToInt32(e.CommandArgument);
        //    //    int portfolioId = int.Parse(gvCustomerPortfolio.DataKeys[index].Value.ToString());
        //    //    Session[SessionContents.PortfolioId] = portfolioId;
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
        //    //}

        //}

        //protected void gvCustomerPortfolio_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //gvCustomerPortfolio.EditIndex = e.NewEditIndex;
        //gvCustomerPortfolio.DataBind();
        //Session["SelecetdRowIndex"] = e.NewEditIndex;
        //BindGridView();
        //}

        protected void rbtnPortfolio_CheckedChanged(object sender, EventArgs e)
        {
            //Clear the existing selected row 

            foreach (GridViewRow row in gvCustomerPortfolio.Rows)
            {
                ((RadioButton)row.FindControl("rbtnPortfolio")).Checked = false;
            }

            //Set the new selected row

            RadioButton rb = (RadioButton)sender;
            GridViewRow tempRow = (GridViewRow)rb.NamingContainer;
            ((RadioButton)tempRow.FindControl("rbtnPortfolio")).Checked = true;
        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomerPortfolio.HeaderRow != null)
            {
                if ((TextBox)gvCustomerPortfolio.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomerPortfolio.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        //protected void btnNameSearch_Click(object sender, EventArgs e)
        //{
           
        //}

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
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
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
                        lowerlimit = (((mypager.CurrentPage - 1) * 15) + 1).ToString();
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

                FunctionInfo.Add("Method", "CustomerPortfolio.ascx.cs:GetPageCount()");

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
                this.BindGridView();
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = mypager.CurrentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnNameSearch_Click1(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindGridView();
                btnSubmit.Visible = true;
                btnSubmit1.Visible = false;
                ddlAdvisorBranchList.Visible = false;
            }
        }

        protected void gvCustomerPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditDetails")
            {
                Session["PortfolioId"] = e.CommandArgument.ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolioSetup','?action=EditCustomerPortfolio');", true);
            }
        }

        protected void Deactive_Click(object sender, EventArgs e)
        {


            string GoalIds = GetSelectedGoalIDString();
            if (GoalIds == "MultipleSelected")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation2();", true);
                return;
 
            }
            if (GoalIds == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation3();", true);
            }
            else
            {
                int folioDefault = portfolioBo.CustomerPortfolioDefault(GoalIds, "F");
                if (folioDefault == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation4();", true);
                }
                else
                {

                    int folioDs = portfolioBo.CustomerPortfolioCheck(GoalIds, "C");
                    if (folioDs > 1)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
                    }
                    else
                    {

                        int countDs = portfolioBo.CustomerPortfolioMultiple(GoalIds, "D");
                        if (countDs > 1)
                        {
                            trReassignPortfloio.Visible = true;
                            bindFolioDropDown(GoalIds, "E");
                            btnSubmit.Visible = false;
                            btnSubmit1.Visible = true;


                        }
                        else
                        {
                            Dissociatecustomer1();
                        }

                    }

                }
            }
        }
        protected void bindFolioDropDown(string GoalIds, string Flag)
        {
           
            DataSet folioDs;
            folioDs = new DataSet();
            folioDs = portfolioBo.CustomerPortfolioNumber(GoalIds, Flag);
            ddlAdvisorBranchList.DataSource = folioDs;
            ddlAdvisorBranchList.DataValueField = folioDs.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlAdvisorBranchList.DataBind();

        }
        protected void Deactive_Click1(object sender, EventArgs e)
        {
            Dissociatecustomer();
            btnSubmit1.Visible = false;
        }

        protected void Dissociatecustomer()
        {
            string GoalIds = GetSelectedGoalIDString();
            string toPortfolio = ddlAdvisorBranchList.SelectedValue.ToString();
            int folioDs = portfolioBo.PortfolioDissociate(GoalIds, toPortfolio,"E");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);


        }

        protected void Dissociatecustomer1()
        {
            string GoalIds = GetSelectedGoalIDString();

            int folioDs = portfolioBo.PortfolioDissociateUnmanaged(GoalIds, "D");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);


        }
        protected void hiddenassociationfound_Click(object sender, EventArgs e)
        {
            

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);

        }

        private string GetSelectedGoalIDString()
        {
            string gvGoalIds = "";
            int count = 0;

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridViewRow gvRow in gvCustomerPortfolio.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkBox");
                if (ChkBxItem.Checked)
                {
                    gvGoalIds += Convert.ToString(gvCustomerPortfolio.DataKeys[gvRow.RowIndex].Value) + "~";
                    count = count + 1;
                    if (count > 1)
                    {                     
                       
                        return "MultipleSelected";
                    }

                }
            }
            return gvGoalIds;

        }
    }
}
