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
                Boolean Iscancel = Convert.ToBoolean(gvCustomerDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_IsRealInvestor"]); 
                CheckBox chk=(CheckBox)e.Item.FindControl("chk");
                if (Iscancel == true)
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

        }

    }
    }
