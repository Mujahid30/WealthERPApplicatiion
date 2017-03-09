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
using BoCustomerProfiling;
using System.Data;
using Telerik.Web.UI;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace WealthERP.OffLineOrderManagement
{
    public partial class OfflineCustomerMerge : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo adviserVo;
        CustomerBo customerBo = new CustomerBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
        }

        protected void btnGO_Click(object sender, EventArgs e)
        {
            pnlCustomerList.Visible = true;
            GetDummyPanBasedOnCriteria();

        }

        private void GetDummyPanBasedOnCriteria()
        {
            DataTable dtDummyPan = new DataTable();
            try{
                dtDummyPan = customerBo.GetDummyPanCustomer(hdnPan.Value, hdnDOB.Value, hdnEMAIL.Value, hdnMoblile.Value, adviserVo.advisorId);
            gvCustomer.DataSource = dtDummyPan;
            gvCustomer.DataBind();
            if (Cache["GvCustomer" + adviserVo.advisorId] != null)
                Cache.Remove("GvCustomer" + adviserVo.advisorId);
            Cache.Insert("GvCustomer" + adviserVo.advisorId, dtDummyPan);
             }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void GetCriteriaMatches(int customerId)
        {
            try
            {
            DataTable dtCriteriaMatches = new DataTable();
            dtCriteriaMatches = customerBo.GetCriteriaMatches(hdnPan.Value, hdnDOB.Value, hdnEMAIL.Value, hdnMoblile.Value, customerId);
          

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void chkdummypan_OnCheckedChanged(object sender, EventArgs e)
        {
            string matchCrieteria = string.Empty;
            CheckBox CheckedCustomer = (CheckBox)sender;
            if (CheckedCustomer.Checked == false) return;
            GridDataItem gdi;
            gdi = (GridDataItem)CheckedCustomer.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            string mobileNo = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_Mobile1"].ToString();
            string C_DOB = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_DOB"].ToString();
            string C_Email = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_Email"].ToString();
            int customerid = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString());
            DataTable dtCriteriaMatches = new DataTable();
            GetSearchCriteria(mobileNo, C_DOB, C_Email, out matchCrieteria);
            if (matchCrieteria == string.Empty)
            {
                matchCrieteria = "NO_MATCH";
                ShowMessage(CreateUserMessage("MatchCriteria", 0, matchCrieteria), 's');
                CheckedCustomer.Checked = false;
                return;
            }

            dtCriteriaMatches = customerBo.GetCriteriaMatches(hdnPan.Value, hdnDOB.Value, hdnEMAIL.Value, hdnMoblile.Value, customerid);


            foreach (DataRow dr in dtCriteriaMatches.Rows)
            {
                gdi["MatchCriteria"].Text = matchCrieteria;
                gdi["MatchCount"].Text = dr["MatchCount"].ToString();
                if (Convert.ToInt32(dr["MatchCount"].ToString()) > 1)
                {
                    ShowMessage(CreateUserMessage("MatchCount", Convert.ToInt32(dr["MatchCount"].ToString()), ""), 's');
                    CheckedCustomer.Checked = false;
                    break;
                }
                gdi["MatchPan"].Text = dr["C_PANNum"].ToString();
                gdi["MatchDOB"].Text = dr["C_DOB"].ToString();
                gdi["MatchMobile"].Text = dr["C_Mobile1"].ToString();
                gdi["MatchEmail"].Text = dr["C_Email"].ToString();
                ((TextBox)gdi.FindControl("txtCustomerId")).Text = dr["C_CustomerId"].ToString();
                gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["MatchCustomerId"] = dr["C_CustomerId"].ToString();

            }
        }

        protected void lnkSelectClient_Click(object sender, EventArgs e)
        {
            radPopUpCustomer.VisibleOnPageLoad = true;
            string matchCrieteria = string.Empty;
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            string mobileNo = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_Mobile1"].ToString();
            string C_DOB = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_DOB"].ToString();
            string C_Email = gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_Email"].ToString();
            int customerid = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString());
            DataTable dtCriteriaMatches = new DataTable();
            GetSearchCriteria(mobileNo, C_DOB, C_Email, out matchCrieteria);
            if (matchCrieteria == string.Empty)
            {
                matchCrieteria = "NO_MATCH";
                ShowMessage(CreateUserMessage("MatchCriteria", 0, matchCrieteria), 's');
                return;
            }

            dtCriteriaMatches = customerBo.GetAutoMergeCriteria(hdnPan.Value, hdnDOB.Value, hdnEMAIL.Value, hdnMoblile.Value, customerid);

            RgPopUpCustomer.DataSource = dtCriteriaMatches;
            RgPopUpCustomer.DataBind();
            if (Cache[userVo.UserId.ToString() + "GvAutoMatchCustomer"] != null)
                Cache.Remove(userVo.UserId.ToString() + "GvAutoMatchCustomer");
            Cache.Insert(userVo.UserId.ToString() + "GvAutoMatchCustomer", dtCriteriaMatches);
 
        }

        protected void btnManualMerge_Click(object sender, EventArgs e)
        {
            try
            {
            ManualMerge();
            ShowMessage(CreateUserMessage("MatchSuccessFul", 0, ""), 's');
            GetDummyPanBasedOnCriteria();        

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            //radPopUpCustomer.VisibleOnPageLoad = false;

        }

        private string CreateUserMessage(string type, int matchCount, string matchCriteria)
        {
            string userMessage = string.Empty;
            //  string matchCriteria= string.Empty;

            if (type == "MatchCriteria" && matchCriteria == "NO_MATCH")
            {
                userMessage = "Please select match criteria";
            }
            else if (type == "MatchCount" && matchCount > 1)
            {
                userMessage = "Please refine search as getting match Count  " + matchCount;
            }
            else if (type == "MatchSuccessFul")
            {
                userMessage = "MatchDone Successfully.";
            }

            return userMessage;

        }

        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            type = 'S';
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You should enter the amount in multiples of Subsequent amount ');", true); return;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
        }

        protected void gvCustomer_OnItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int deleteCustomerId = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustomerId"].ToString());
                int matchCustomerId = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["MatchCustomerId"].ToString());

                customerBo.CreateCustomerMerge(deleteCustomerId, matchCustomerId);
                ShowMessage(CreateUserMessage("MatchSuccessFul", 0, ""), 's');

                GetDummyPanBasedOnCriteria();
            }
        }

        private bool ManualMerge()
        {            
            bool result = false;
            try{
            foreach (GridDataItem gdi in RgPopUpCustomer.Items)
            {
                if (((CheckBox)gdi.FindControl("chkManualMerge")).Checked == true)
                {
                    int selectedRow = gdi.ItemIndex + 1;
                    int deleteCustomerId = Convert.ToInt32(RgPopUpCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["DeleteCustomerId"].ToString());
                    int matchCustomerId = Convert.ToInt32(RgPopUpCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString());
                    customerBo.CreateCustomerMerge(deleteCustomerId, matchCustomerId);                   
                }
            }
            return result;
             }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

         private bool BulkMerge()
        {
            try
            {

           
           
            bool result = false;
            foreach (GridDataItem gdi in gvCustomer.Items)
            {
                if (((CheckBox)gdi.FindControl("chkdummypan")).Checked == true)
                {
                    int selectedRow = gdi.ItemIndex + 1;
                    int deleteCustomerId = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString());
                    int matchCustomerId = Convert.ToInt32(gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["MatchCustomerId"].ToString());
                    customerBo.CreateCustomerMerge(deleteCustomerId, matchCustomerId);                   
                }
            }
            return result;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void btnBulkMerge_Click(object sender, EventArgs e)
        {
            try
            {
            BulkMerge();
            GetDummyPanBasedOnCriteria();
          

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void gvCustomer_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {

           
            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["GvCustomer" + adviserVo.advisorId];
            gvCustomer.DataSource = dtCustomer;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void RgPopUpCustomer_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {

            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["GvAutoMatchCustomer" + adviserVo.advisorId];
            RgPopUpCustomer.DataSource = dtCustomer;
           
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void GetSearchCriteria(string mobileNo, string C_DOB, string C_Email, out string matchCrieteria)
        {
            matchCrieteria = string.Empty;

            foreach (RadListBoxItem li in chkbldepart.Items)
            {
                //pan, dob, email, moblile
                if (li.Checked == true)
                {
                    if (li.Value == "pan")
                    {
                        matchCrieteria = li.Value;
                        hdnPan.Value = li.Value;
                    }
                    else if (li.Value == "DOB")
                    {
                        matchCrieteria = matchCrieteria + "-" + li.Value;
                        hdnDOB.Value = C_DOB;
                    }
                    else if (li.Value == "Email")
                    {
                        matchCrieteria = matchCrieteria + "-" + li.Value;
                        hdnEMAIL.Value = C_Email;
                    }
                    else if (li.Value == "Mobile")
                    {
                        matchCrieteria = matchCrieteria + "-" + li.Value;
                        hdnMoblile.Value = mobileNo;
                    }
                }

            }

        }

    }
}