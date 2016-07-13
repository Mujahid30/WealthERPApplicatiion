﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUser;
using BoOfflineOrderManagement;
using BoCommon;
using WealthERP.Base;
using System.Data;
namespace WealthERP.OffLineOrderManagement
{
    public partial class CustomerOfflineBondOrderView : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                BindFixedIncomeList(); 
            }
        }
        protected void BindFixedIncomeList()
        {
            DataSet ds = OfflineBondOrderBo.GetCustomerAllotedData(customerVO.CustomerId);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
                Cache.Remove("BondOrderBookList" + userVo.UserId.ToString());
                Cache.Insert("BondOrderBookList" + userVo.UserId.ToString(), ds.Tables[0]);
                pnlGrid.Visible = true;
                gvBondOrderList.DataSource = ds.Tables[0];
                gvBondOrderList.DataBind();
            //}
        }
        protected void gvBondOrderList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrder;
            dtOrder = (DataTable)Cache["BondOrderBookList" + userVo.UserId.ToString()];
            if (dtOrder != null)
            {
                gvBondOrderList.DataSource = dtOrder;
            }

        }
    }
}