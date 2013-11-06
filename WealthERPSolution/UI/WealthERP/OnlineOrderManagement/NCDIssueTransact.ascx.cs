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
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;


namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueTransact : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        OnlineBondOrderVo OnlineBondVo = new OnlineBondOrderVo();
        CustomerVo customerVo = new CustomerVo();
        bool RESULT = false;
        int customerId;
        //int selectedRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            if (!IsPostBack)
            {

                BindKYCDetailDDl();
                if (Request.QueryString["customerId"] != null)
                {
                    customerId = int.Parse((Request.QueryString["customerId"]).ToString());
                }
                  
                if (Request.QueryString["IssuerId"] != null)
                {
                    string IssuerId = Request.QueryString["IssuerId"].ToString();
                    lblIssuer.Text = "Selected Issue Name :" + IssuerId;
                    //int IssueIdN = Convert.ToInt32(IssueId);
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;
                    BindStructureRuleGrid(IssuerId);
                }
                else
                {
                    BindDropDownListIssuer();
                    lblIssuer.Text = "Kindly Select Issue Name";
                    btnConfirm.Enabled = true;
                }

            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string IssuerId = ddIssuerList.SelectedValue.ToString();
            BindStructureRuleGrid(IssuerId);
            //OnlineBondVo = new OnlineBondOrderVo();
            //OnlineBondVo = CollectOnlineBondData(sender);
            //OnlineBondBo.onlineBOndtransact(OnlineBondVo);

        }
        protected void BindStructureRuleGrid(string IssuerId)
        {
            //DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(1,2);
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransaction(IssuerId);
            if (dsStructureRules.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvCommMgmt.DataSource = dsStructureRules.Tables[0];
            gvCommMgmt.DataBind();
            pnlNCDTransactact.Visible = true;
            //Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules.Tables[0]);
        }
        protected void BindDropDownListIssuer()
        {
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransactionList();
            ddIssuerList.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            ddIssuerList.DataValueField = dsStructureRules.Tables[0].Columns["AIM_IssueId"].ToString();
            ddIssuerList.DataSource = dsStructureRules.Tables[0];
            ddIssuerList.DataBind();
        }
        protected void BindKYCDetailDDl()
        {
            DataSet dsNomineeAndJointHolders = OnlineBondBo.GetNomineeJointHolder(customerVo.CustomerId);
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[0].Rows)
            {
                strbJointHolder.Append(dr["JointHolderName"].ToString() );
                strbNominee.Append(dr["JointHolderName"].ToString() );
            }

            lblNomineeTwo.Text = strbNominee.ToString();
            lblHolderTwo.Text = strbJointHolder.ToString();
            //if (dsStructureRules.Tables[0].Rows.Count > 0)
            //{
            //    lblHolderTwo.Text = dsStructureRules.Tables[0].Columns[""].ToString();
            //    lblHolderThird.Text = dsStructureRules.Tables[0].Columns["ThirdHolder"].ToString();
            //    //ddlHolder.DataSource = dsStructureRules.Tables[0];
            //    //ddlHolder.DataBind();
            //}
            //else
            //{
            //    lblHolderTwo.Text = "No Second Holder";
            //    lblHolderThird.Text = "No Third Holder";
            //}
            //if (dsStructureRules.Tables[1].Rows.Count > 0)
            //{
            //    lblNomineeTwo.Text = dsStructureRules.Tables[1].Columns["NomineeName1"].ToString();
            //    lblNomineeThird.Text = dsStructureRules.Tables[1].Columns["NomineeName2"].ToString();

            //}
            //else
            //{
            //    lblNomineeTwo.Text = "No Nominee Name Found";
            //    lblNomineeThird.Text = "No Nominee Name Found";
            //}
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((TextBox)sender).NamingContainer).RowIndex;

            int rowindex = (rowindex1 / 2) - 1;
            TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Quantity"].FindControl("txtQuantity");

            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                int PFISD_BidQty = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["PFISD_BidQty"].ToString());
                int PFISD_InMultiplesOf = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["PFISD_InMultiplesOf"].ToString());
                int Qty = Convert.ToInt32(txtQuantity.Text);

                if (Qty < PFISD_BidQty)
                {
                    lblMSG.Text = "Bid Quantity should not be less than the Minimum BID Qty i.e." + PFISD_BidQty.ToString();
                    txtQuantity.Text = "";
                    return;
                }
                int Mod = Qty % PFISD_InMultiplesOf;
                if (Mod != 0)
                {
                    lblMSG.Text = "Bid Quantity should be in Allowed Multiplication i.e. i.e." + PFISD_InMultiplesOf.ToString();
                    txtQuantity.Text = "";
                    return;
                }
                int AIM_FaceValue = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_FaceValue"].ToString());
                TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Amount"].FindControl("txtAmount");
                txtAmount.Text = Convert.ToString(Qty * AIM_FaceValue);
                CheckBox cbSelectOrder = (CheckBox)gvCommMgmt.MasterTableView.Items[rowindex]["Check"].FindControl("cbOrderCheck");
                cbSelectOrder.Checked = true;
            }
        }


        protected void lbconfirmOrder_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            Button Button = (Button)sender;
            int MaxAppNo = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString());
           // int maxDB = OnlineBondBo.GetMAXTransactNO();


            DataTable dt = new DataTable();
            //GridEditableItem editedItem = Button.NamingContainer as GridEditableItem;
            //Need to be collect from Session...
            dt.Columns.Add("CustomerId");
            dt.Columns.Add("PFISD_SeriesId");
            dt.Columns.Add("PFIIM_IssuerId");
            dt.Columns.Add("PFISM_SchemeId");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            int rowNo = 0;
            int tableRow = 0;
            foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            {

                TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");

                //OnlineBondVo.CustomerId = "ESI123456".ToString();
                OnlineBondVo.CustomerId = customerVo.CustomerId;
                OnlineBondVo.BankAccid = 1002321521;
                OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISD_SeriesId"].ToString());
                OnlineBondVo.PFIIM_IssuerId = Convert.ToString(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFIIM_IssuerId"].ToString());
                OnlineBondVo.PFISM_SchemeId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISM_SchemeId"].ToString());
                CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
                if (Check.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
                        TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
                        OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
                        //TextBox txtAmountAtMat = (TextBox)gvCommMgmt.MasterTableView.Items[0]["AmountAtMaturity"].FindControl("txtAmountAtMaturity");
                        //OnlineBondVo.AmountAtMat = Convert.ToDouble(txtAmountAtMat.Text);
                        dt.Rows.Add();
                        dt.Rows[tableRow]["CustomerId"] = OnlineBondVo.CustomerId;
                        dt.Rows[tableRow]["PFISD_SeriesId"] = OnlineBondVo.PFISD_SeriesId;
                        dt.Rows[tableRow]["PFIIM_IssuerId"] = OnlineBondVo.PFIIM_IssuerId;
                        dt.Rows[tableRow]["PFISM_SchemeId"] = OnlineBondVo.PFISM_SchemeId;
                        dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
                        dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;
                    }
                    tableRow++;
                }
                if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                    rowNo++;
                else
                    break;

            }
            RESULT = OnlineBondBo.onlineBOndtransact(dt);
            //string CustId = Session["CustId"].ToString();
            if(RESULT==true)            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueBooks','&customerId=" + customerVo.CustomerId + "');", true);
        }

        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {



        }


    }
}