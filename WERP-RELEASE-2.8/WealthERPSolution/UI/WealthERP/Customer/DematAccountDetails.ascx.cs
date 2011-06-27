using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using System.Data.Common;
using System.Data.SqlClient;
using VoCustomerPortfolio;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Customer
{
    public partial class DematAccountDetails : System.Web.UI.UserControl
    {
        BoDematAccount bodemataccount = new BoDematAccount();
        CustomerVo customervo = new CustomerVo();
        



        //protected override void Init(object sender,EventArgs e)
        //{
            //try
            //{
            //    ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            //    mypager.EnableViewState = true;
            //    base.OnInit(e);
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "DematDetails.ascx.cs:OnInit()");
            //    object[] objects = new object[0];

            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        //}

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    //portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
            //    GetPageCount();
            //    //this.LoadGridview(portfolioId);
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "DematDetails.ascx.cs:HandlePagerEvent()");
            //    object[] objects = new object[0];
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}

        }

        private void GetPageCount()
        {
            //string upperlimit = "";
            //int rowCount = 0;
            //int ratio = 0;
            //string lowerlimit = "";
            //string PageRecords = "";
            //try
            //{
            //    if (hdnRecordCount.Value.Trim() != "")
            //        rowCount = Convert.ToInt32(hdnRecordCount.Value);
            //    ratio = rowCount / 10;
            //    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            //    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            //    lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
            //    upperlimit = (mypager.CurrentPage * 10).ToString();
            //    if (mypager.CurrentPage == mypager.PageCount)
            //        upperlimit = hdnRecordCount.Value;
            //    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            //    lblCurrentPage.Text = PageRecords;
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx.cs:GetPageCount()");
            //    object[] objects = new object[5];
            //    objects[0] = upperlimit;
            //    objects[1] = rowCount;
            //    objects[2] = ratio;
            //    objects[3] = lowerlimit;
            //    objects[4] = PageRecords;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}

        }




        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerPortfolioVo customerportfoliovo = (CustomerPortfolioVo)Session["customerPortfolioVo"];
            customervo = (CustomerVo)Session["CustomerVo"];
            int customerId = customervo.CustomerId;
            //int demataccountid = int.Parse(Session["DematAccountId"].ToString());
            try
            {
                
                
                DataSet dsDematDetails = bodemataccount.GetDematAccountHolderDetails(customerId);
                if (dsDematDetails == null)
                {
                    gvDematDetails.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "No data available to show";
                }
                else
                {
                    if (dsDematDetails.Tables[0].Rows.Count <= 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "No data available to show";
                        mypager.Visible = false;
                    }
                    else
                    {
                        lblError.Visible = false;
                        gvDematDetails.Visible = true;
                        gvDematDetails.DataSource = dsDematDetails.Tables[0];
                        gvDematDetails.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow=0;
            int dematAccountId=0;
            GridViewRow gvr=null;
            DropDownList ddlAction = null;
            ddlAction = (DropDownList)sender;      
            
            try
            {
                gvr = (GridViewRow)ddlAction.NamingContainer;
                selectedRow = gvr.RowIndex;
                dematAccountId = int.Parse(gvDematDetails.DataKeys[selectedRow].Value.ToString());
                Session["DematAccountId"] = dematAccountId;
                if (ddlAction.SelectedItem.Value == "View")
                {
                    Session["DematDetailsView"] = "View";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
                }
                else if (ddlAction.SelectedItem.Value == "Edit")
                {
                    Session["DematDetailsView"] = "Edit";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDematDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvDematDetails.PageIndex = e.NewPageIndex;
            //gvDematDetails.DataBind();
        }

        protected void gvDematDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            //string sortExpression = e.SortExpression;
            //ViewState["sortExpression"] = sortExpression;
            //if (GridViewSortDirection == SortDirection.Ascending)
            //{
            //    GridViewSortDirection = SortDirection.Descending;
            //    SortGridVIew(sortExpression, DESCENDING);
            //}
            //else
            //{
            //    GridViewSortDirection = SortDirection.Ascending;
            //    SortGridVIew(sortExpression, ASCENDING);
            //}
        }
        //private SortDirection GridViewSortDirection
        //{
            //get
            //{
            //    if (ViewState["sortDirection"] == null)
            //        ViewState["sortDirection"] = SortDirection.Ascending;
            //    return (SortDirection)ViewState["sortDirection"];
            //}
            //set { ViewState["sortDirection"] = value; }
        //}

        private void SortGridVIew(string sortExpression, string direction)
        {

            //List<InsuranceVo> insuranceList = new List<InsuranceVo>();
            //try
            //{
            //    int count;

            //    insuranceList = insuranceBo.GetInsurancePortfolio(portfolioId, mypager.CurrentPage, hdnSort.Value.Trim(), out count);

            //    if (count > 0)
            //    {
            //        lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
            //        tblPager.Visible = true;
            //    }

            //    InsuranceVo insuranceVo;
            //    DataTable dtInsurance = new DataTable();
            //    dtInsurance.Columns.Add("SI.No");
            //    dtInsurance.Columns.Add("InsuranceId");
            //    dtInsurance.Columns.Add("Category");
            //    dtInsurance.Columns.Add("Particulars");
            //    dtInsurance.Columns.Add("Premium Amount");
            //    dtInsurance.Columns.Add("Sum Assured");
            //    dtInsurance.Columns.Add("Maturity Value");
            //    DataRow drInsurance;
            //    for (int i = 0; i < insuranceList.Count; i++)
            //    {
            //        drInsurance = dtInsurance.NewRow();
            //        insuranceVo = new InsuranceVo();
            //        insuranceVo = insuranceList[i];
            //        drInsurance[0] = insuranceVo.CustInsInvId.ToString();
            //        drInsurance[1] = insuranceVo.AssetInstrumentCategoryCode.ToString();
            //        drInsurance[2] = insuranceVo.Name.ToString();
            //        drInsurance[3] = String.Format("{0:n2}", decimal.Parse(insuranceVo.PremiumAmount.ToString("f2")));
            //        drInsurance[4] = String.Format("{0:n0}", decimal.Parse(insuranceVo.SumAssured.ToString("f0")));
            //        drInsurance[5] = String.Format("{0:n2}", decimal.Parse(insuranceVo.MaturityValue.ToString("f2")));

            //        dtInsurance.Rows.Add(drInsurance);

            //    }

            //    DataView dv = new DataView(dtInsurance);
            //    dv.Sort = sortExpression + direction;
            //    gvrLifeInsurance.DataSource = dv;
            //    gvrLifeInsurance.DataBind();
            //    gvrLifeInsurance.Visible = true;
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:SortGridVIew()");
            //    object[] objects = new object[2];
            //    objects[0] = insuranceVo;
            //    objects[1] = insuranceList;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        }

    }
   
}