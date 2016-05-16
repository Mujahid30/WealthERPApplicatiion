using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Collections;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using BoWerpAdmin;
using System.Data;
using BoProductMaster;
using System.Xml;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Web.Script.Serialization;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineCustomerOrderandTransactionBook : System.Web.UI.UserControl
    {
        UserVo userVo;
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        CustomerVo customerVO;
        string exchangeType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVO = (CustomerVo)Session["customerVo"];
            if (Session["ExchangeMode"] != null)
                exchangeType = Session["ExchangeMode"].ToString();
            else
                exchangeType = "Online";

            if (exchangeType == "Demat")
            {
                ddlAction.Items.FindByValue("SIP").Enabled = false;
                ddlAction.Items.FindByValue("ABY").Enabled = false;
            }
            else
            {
                ddlAction.Items.FindByValue("SIP").Enabled = true;
                ddlAction.Items.FindByValue("ABY").Enabled = true;
            }
            if (!IsPostBack)
            {
                BindAmc();
                BindScheme();
            }
        }
        protected void BindAmc()
        {
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = OnlineMFOrderBo.GetOrderAmcDetails(customerVO.CustomerId);
            dtAmc = ds.Tables[0];

            if (dtAmc.Rows.Count > 0)
            {
                ddlAMC.DataSource = dtAmc;
                ddlAMC.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMC.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMC.DataBind();
            }
            ddlAMC.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void BindScheme()
        {
            OnlineMFSchemeDetailsBo onlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            DataSet ds;
            if (ddlAMC.SelectedValue != "")
            {
                ds = onlineMFSchemeDetailsBo.GetSIPCustomeSchemePlan(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), exchangeType == "Online" ? 1 : 0);
                ddlScheme.DataSource = ds.Tables[0];
                ddlScheme.DataValueField = ds.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = ds.Tables[0].Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {

            switch (ddlAction.SelectedValue)
            {
                case "All":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "0", exchangeType == "Online" ? 1 : 0);
                    break;
                case "SIP":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "SIP", exchangeType == "Online" ? 1 : 0);
                    break;
                case "BUY":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "BUY", exchangeType == "Online" ? 1 : 0);
                    break;
                case "ABY":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "ABY", exchangeType == "Online" ? 1 : 0);

                    break;
                case "SEL":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "SEL", exchangeType == "Online" ? 1 : 0);
                    break;
            }
        }


        protected void BindOrderTransactionBook(int customerId, int amcCode, int schemeCode, string OrderType, int exchangeType)
        {
            DataSet ds = OnlineMFOrderBo.GetCustomerOrderBookTransaction(customerId, amcCode, schemeCode, OrderType, exchangeType);

            string df = ds.GetXml();
            DataTable dtOrderTransactionBook = ds.Tables[0];
            if (dtOrderTransactionBook.Rows.Count >= 0)
            {
                if (Cache[userVo.UserId.ToString() + "OrderTransactionBook"] != null)
                    
                    Cache.Remove("OrderTransactionBook" + userVo.UserId.ToString());
                Cache.Insert("OrderTransactionBook" + userVo.UserId.ToString(), dtOrderTransactionBook);
                var page = 0;
                gvOrderBook.CurrentPageIndex = page;
                gvOrderBook.DataSource = ds.Tables[0];
                gvOrderBook.DataBind();
                Div1.Visible = true;
                btnExport.Visible = true;
            }
        }
        protected void gvOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrderTransactionBook = (DataTable)Cache["OrderTransactionBook" + userVo.UserId.ToString()];
            if (dtOrderTransactionBook != null)
            {
                gvOrderBook.DataSource = dtOrderTransactionBook;
            }
        }
        protected void gvOrderBook_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }
        protected void ddlAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedValue != "0")
            {
                BindScheme();
            }
            else
            {
                BindScheme();
            }

        }
        protected void btnDetails_OnClick(object sender, EventArgs e)
        {
            int count = gvOrderBook.MasterTableView.Items.Count;
            DataTable dtIssueDetail;
            Button btnDetails = (Button)sender;
            GridDataItem gdi;
            DataTable filldt = new DataTable();
            gdi = (GridDataItem)btnDetails.NamingContainer;
            int orderId = int.Parse(gvOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("gvChildDetails");
            //Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (gvChildDetails.Visible == false)
            {
                gvChildDetails.Visible = true;
                //btnDetails.Text = "-";
            }
            else if (gvChildDetails.Visible == true)
            {
                gvChildDetails.Visible = false;
                //buttonlink.Text = "+";
            }
            DataTable dtNCDOrderBook = (DataTable)Cache["OrderTransactionBook" + userVo.UserId.ToString()];
            DataRow[] rows = dtNCDOrderBook.Select("CO_OrderId = " + orderId + " ");
            if (rows.Length > 0)
            {
                filldt = rows.CopyToDataTable();
            }

            gvChildDetails.DataSource = filldt;
            gvChildDetails.DataBind();
        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {

            DataTable dtUnitHolding = new DataTable();
            dtUnitHolding = (DataTable)Cache["OrderTransactionBook" + userVo.UserId.ToString()];
            DataTable dtMFOrderBook = new DataTable();
            dtMFOrderBook.Columns.Add("Order Type");
            dtMFOrderBook.Columns.Add("Scheme Name");
            dtMFOrderBook.Columns.Add("Order No.");
            dtMFOrderBook.Columns.Add("Category");
            dtMFOrderBook.Columns.Add("Request Date", typeof(DateTime));
            dtMFOrderBook.Columns.Add("Dividend Type");
            dtMFOrderBook.Columns.Add("Amount");
            dtMFOrderBook.Columns.Add("Redeem All");
            dtMFOrderBook.Columns.Add("Online");
            dtMFOrderBook.Columns.Add("Order Status");
            dtMFOrderBook.Columns.Add("Reject Remark");
            dtMFOrderBook.Columns.Add("Units");
            dtMFOrderBook.Columns.Add("Actioned NAV");
            foreach (DataRow sourcerow in dtUnitHolding.Rows)
            {
                DataRow destRow = dtMFOrderBook.NewRow();
                destRow["Scheme Name"] = sourcerow["PASP_SchemePlanName"];
                destRow["Category"] = sourcerow["PAIC_AssetInstrumentCategoryName"];
                destRow["Order No."] = sourcerow["CO_OrderId"];
                destRow["Request Date"] = sourcerow["CO_OrderDate"];
                destRow["Order Type"] = sourcerow["WMTT_TransactionType"];
                destRow["Dividend Type"] = sourcerow["CMFOD_DividendOption"];
                destRow["Amount"] = sourcerow["CMFOD_Amount"];
                destRow["Units"] = sourcerow["CMFOD_Units"];
                destRow["Actioned NAV"] = sourcerow["CMFT_Price"];
                destRow["Redeem All"] = sourcerow["CMFOD_IsAllUnits"];
                destRow["Order Status"] = sourcerow["XS_Status"];
                destRow["Online"] = sourcerow["Channel"];
                destRow["Reject Remark"] = sourcerow["COS_Reason"];
                dtMFOrderBook.Rows.Add(destRow);
            }
            System.Data.DataView view = new System.Data.DataView(dtMFOrderBook);
            
            
            System.Data.DataTable selected =
                    view.ToTable("Selected", false, "Scheme Name", "Category", "Order No.", "Request Date", "Order Type", "Dividend Type",
                    "Amount", "Units", "Actioned NAV", "Redeem All", "Order Status", "Online", "Reject Remark");
          
            if (dtMFOrderBook.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerOrder.xls"));
                Response.ContentType = "application/ms-excel";

                string str = string.Empty;

                foreach (DataColumn dtcol in selected.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";

                }
                Response.Write("\n");
                foreach (DataRow dr in selected.Rows)
                {
                    str = "";
                    for (int j = 0; j < selected.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

            }
        }
    }
}