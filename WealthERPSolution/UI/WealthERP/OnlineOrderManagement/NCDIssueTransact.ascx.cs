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
        OnlineBondOrderVo OnlineBondVo=new OnlineBondOrderVo();
        bool RESULT = false;
        int selectedRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownListIssuer();
                BindKYCDetailDDl();
                ////DHFL
                //string IssueId = "1";
                ////Request.QueryString["IssueId"].ToString();
                ////if (Request.Form["IssueId"] != null)
                ////{
                //if(IssueId!=null)
                //{
                //    lblIssuer.Text = "Selected Issue Name :";
                //    int IssueIdN = Convert.ToInt32(IssueId);
                //    btnConfirm.Enabled = false;
                //    BindStructureRuleGrid(4);
                //}
                //else
                //{
                    lblIssuer.Text = "Kindly Select Issue Name";
                //}
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            BindStructureRuleGrid(IssuerId);
            //OnlineBondVo = new OnlineBondOrderVo();
            //OnlineBondVo = CollectOnlineBondData(sender);
            //OnlineBondBo.onlineBOndtransact(OnlineBondVo);

        }
        protected void BindStructureRuleGrid(int IssuerId)
        {
            //DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(1,2);
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(2, IssuerId);
            if (dsStructureRules.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvCommMgmt.DataSource = dsStructureRules.Tables[0];
            gvCommMgmt.DataBind();
            //Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules.Tables[0]);
        }
        protected void BindDropDownListIssuer()
        {
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(5, 9);
            ddIssuerList.DataTextField = dsStructureRules.Tables[0].Columns["AIM_SchemeName"].ToString();
            ddIssuerList.DataValueField = dsStructureRules.Tables[0].Columns["AIM_IssueID"].ToString();
            ddIssuerList.DataSource = dsStructureRules.Tables[0];
            ddIssuerList.DataBind();
        }
        protected void BindKYCDetailDDl()
        {
            DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(6, 11);
            if (dsStructureRules.Tables[0].Rows.Count > 0)
            {
                lblHolderTwo.Text = dsStructureRules.Tables[0].Columns["SecondHolder"].ToString();
                lblHolderThird.Text = dsStructureRules.Tables[0].Columns["ThirdHolder"].ToString();
                //ddlHolder.DataSource = dsStructureRules.Tables[0];
                //ddlHolder.DataBind();
            }
            else
            {
                lblHolderTwo.Text = "No Second Holder";
                lblHolderThird.Text = "No Third Holder";
            }
            if (dsStructureRules.Tables[1].Rows.Count > 0)
            {
                lblNomineeTwo.Text = dsStructureRules.Tables[1].Columns["NomineeName1"].ToString();
                lblNomineeThird.Text = dsStructureRules.Tables[1].Columns["NomineeName2"].ToString();
                
            }
            else
            {
                lblNomineeTwo.Text = "No Nominee Name Found";
                lblNomineeThird.Text = "No Nominee Name Found";
            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
           int rowindex1= ((GridDataItem)((TextBox)sender).NamingContainer).RowIndex;

           int rowindex = (rowindex1/ 2)-1;
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
            DataTable dt = new DataTable();
            //GridEditableItem editedItem = Button.NamingContainer as GridEditableItem;
            //Need to be collect from Session...
            dt.Columns.Add("CustomerId");
            dt.Columns.Add("PFISD_SeriesId");
            dt.Columns.Add("PFIIM_IssuerId");
            dt.Columns.Add("PFISM_SchemeId");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            int rowNo=0;
            int tableRow = 0;
            foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            {
              
                TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");

                OnlineBondVo.CustomerId = "ESI123456".ToString();
                OnlineBondVo.BankAccid = 1002321521;

                OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISD_SeriesId"].ToString());
                OnlineBondVo.PFIIM_IssuerId = Convert.ToString(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFIIM_IssuerId"].ToString());
                OnlineBondVo.PFISM_SchemeId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISM_SchemeId"].ToString());
                CheckBox Check= (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
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
        }

        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            

            
        }
        

    }
}