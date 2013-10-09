using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerMFOrderBookList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;
        UserVo userVo;
        CustomerVo customerVO = new CustomerVo();
        string userType;
        int customerId = 0;
        int AccountId=0;
        DateTime fromDate;
        DateTime toDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();           
            userVo = (UserVo)Session["userVo"];
            customerId = customerVO.CustomerId;
            if (!Page.IsPostBack)
            {
                hdnAccount.Value = "0";
                BindFolioAccount();
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindOrderBook();
        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
            DataSet dsFolioAccount;
            DataTable dtFolioAccount;
            dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
            dtFolioAccount = dsFolioAccount.Tables[0];
            if (dtFolioAccount.Rows.Count > 0)
            {
                ddlAccount.DataSource = dsFolioAccount.Tables[0];
                ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
                ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
                ddlAccount.DataBind();
            }
              ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindOrderBook()
        {
             
             DataSet dsOrderBookMIS = new DataSet();
             DataTable dtOrderBookMIS = new DataTable();
             if (txtFrom.SelectedDate != null)
             fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
             if (txtTo.SelectedDate != null)
             toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            // AccountId = int.Parse(ddlAccount.SelectedValue.ToString());

             dsOrderBookMIS = OnlineMFOrderBo.GetOrderBookMIS(advisorVo.advisorId, customerId, 0, fromDate, toDate);
            dtOrderBookMIS = dsOrderBookMIS.Tables[0];
            if (dtOrderBookMIS.Rows.Count > 0)
            {
                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dsOrderBookMIS.Tables[0]);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dsOrderBookMIS.Tables[0]);
                }
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                gvOrderBookMIS.Visible = true;
                pnlOrderBook.Visible = true;
                btnExport.Visible = true;
               
                }
            else
            {
                gvOrderBookMIS.DataSource = null;
                gvOrderBookMIS.Visible = false;
                pnlOrderBook.Visible = false;
                btnExport.Visible = false;
            }
        }
        private void SetParameter()
        {
            if (ddlAccount.SelectedIndex != 0)
            {
                hdnAccount.Value = ddlAccount.SelectedValue;
                ViewState["AccountDropDown"] = hdnAccount.Value;
            }            
            else
            {
                hdnAccount.Value = "0";
            }
        }
        protected void gvOrderBookMISt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            gvOrderBookMIS.Visible = true;
            DataTable dtOrderBookMIS = new DataTable();
           // dsOrderBookMIS = (DataSet)Cache["OrderList" + advisorVo.advisorId];
            dtOrderBookMIS = (DataTable)Cache["OrderList" + advisorVo.advisorId.ToString()];
            if (dtOrderBookMIS != null)
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.Visible = true;
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        { 
        }
    }
}