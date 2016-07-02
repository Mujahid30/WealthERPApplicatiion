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
using Telerik.Web.UI;
using BoAdvisorProfiling;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class CustomerDematAcceptedDetails : System.Web.UI.UserControl
    {
        static Dictionary<string, string> genDictRM;
        string UserRole;
        UserVo userVo = new UserVo();
        AdvisorBo advisorBo = new AdvisorBo();
        AdvisorVo adviserVo;
        int producttype = 0;
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
                BindCustomerDetailsGrid();

        }

        protected void gvCustomerDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (userVo.UserType == "Advisor")
            {
                //if (rbtnProspect.Checked)
                //    producttype = 2;
                //else if (rbtnFpClient.Checked)
                //    producttype = 1;
                if (producttype == 2)
                {
                    gvCustomerDetails.MasterTableView.GetColumn("Action").Visible = false;
                    gvCustomerDetails.MasterTableView.GetColumn("MarkFPClient").Visible = false;
                    gvCustomerDetails.MasterTableView.GetColumn("ActionForProspect").Visible = true;
                }
                else
                {
                    gvCustomerDetails.MasterTableView.GetColumn("Action").Visible = true;
                    gvCustomerDetails.MasterTableView.GetColumn("MarkFPClient").Visible = true;
                    gvCustomerDetails.MasterTableView.GetColumn("ActionForProspect").Visible = false;
                }
                return;
            }
            if (userVo.UserType == "Associates")
            {
                gvCustomerDetails.MasterTableView.GetColumn("Action").Visible = false;
                gvCustomerDetails.MasterTableView.GetColumn("MarkFPClient").Visible = false;
                gvCustomerDetails.MasterTableView.GetColumn("ActionForProspect").Visible = false;
            }
            if (e.Item is GridDataItem) 
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                Boolean Iscancel = Convert.ToBoolean(gvCustomerDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_IsDematInvestor"]);
                CheckBox chk = (CheckBox)e.Item.FindControl("chk");
                if (Iscancel)
                    chk.Visible = false;
                else
                    chk.Visible = true;
            }
        }



        public void BindCustomerDetailsGrid()
        {
            DataSet ds = onlineOrderBackOfficeBo.BindCustomerDetails(adviserVo.advisorId);
            gvCustomerDetails.DataSource = ds.Tables[0];
            gvCustomerDetails.DataBind();
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
                        drcustomer["customerId"] = int.Parse(gvCustomerDetails.MasterTableView.DataKeyValues[radItem.ItemIndex ]["C_CustomerId"].ToString());
                        CheckBox chk = radItem.FindControl("chk") as CheckBox;
                        drcustomer["demataccepted"] = chk.Checked ? 1 : 0;
                        dtcustomer.Rows.Add(drcustomer);
                    }
                }
                OnlineOrderBackOfficeBo.UpdateCustomerCode(dtcustomer, userVo.UserId);
                BindCustomerDetailsGrid();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Selected Customer Marked as Demat Investor!!');", true);
            }

        }
    }
}
