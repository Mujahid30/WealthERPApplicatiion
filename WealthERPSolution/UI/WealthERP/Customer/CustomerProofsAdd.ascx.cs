using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoUser;
using BoCustomerProfiling;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Customer
{
    public partial class CustomerProofsAdd : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        RMVo rmVo = new RMVo();
        List<string> proofList;
        List<string> proofList2;
        string proof;
        int proofCode;
        string path;
        int KYCFlag;
        int customerId;
        int rmId;


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            int customerTypeId = 0;
            if (customerVo.Type == "IND")
            {
                customerTypeId = 68;
            }
            else
            {
                customerTypeId = 69;
            }
                customerTypeId = 68;
                DataTable dtAddressProofList = customerBo.GetProofList(customerTypeId, 1,customerVo.CustomerId);
                DataTable dtIDProofList = customerBo.GetProofList(customerTypeId, 2,customerVo.CustomerId);
                DataTable dtOtherProofList = customerBo.GetProofList(customerTypeId, 7,customerVo.CustomerId);

            // ADDRESS PROOF LIST

                DataTable dt = new DataTable();
                dt.Columns.Add("ProofCode");
                dt.Columns.Add("Proof Name");

                DataRow drCustomerProof;
                for (int i = 0; i < dtAddressProofList.Rows.Count; i++)
                {
                    drCustomerProof = dt.NewRow();
                    drCustomerProof[0] = dtAddressProofList.Rows[i]["XP_ProofCode"].ToString();
                    drCustomerProof[1] = dtAddressProofList.Rows[i]["XP_ProofName"].ToString();
                    dt.Rows.Add(drCustomerProof);
                }
                gvAddressProofList.DataSource = dt;
                gvAddressProofList.DataBind();
                gvAddressProofList.Visible = true;


            // ID PROOF LIST

                DataTable dtId = new DataTable();
                dtId.Columns.Add("ProofCode");
                dtId.Columns.Add("Proof Name");

                DataRow drIDProof;
                for (int i = 0; i < dtIDProofList.Rows.Count; i++)
                {
                    drIDProof = dtId.NewRow();
                    drIDProof[0] = dtIDProofList.Rows[i]["XP_ProofCode"].ToString();
                    drIDProof[1] = dtIDProofList.Rows[i]["XP_ProofName"].ToString();
                    dtId.Rows.Add(drIDProof);
                }
                gvIDProofList.DataSource = dtId;
                gvIDProofList.DataBind();
                gvIDProofList.Visible = true;

            // FOR OTHER PROOF LIST 


                DataTable dtOther = new DataTable();
                dtOther.Columns.Add("ProofCode");
                dtOther.Columns.Add("Proof Name");

                DataRow drOtherProof;
                for (int i = 0; i < dtOtherProofList.Rows.Count; i++)
                {
                    drOtherProof = dtOther.NewRow();
                    drOtherProof[0] = dtOtherProofList.Rows[i]["XP_ProofCode"].ToString();
                    drOtherProof[1] = dtOtherProofList.Rows[i]["XP_ProofName"].ToString();
                    dtOther.Rows.Add(drOtherProof);
                }
                gvOtherProofList.DataSource = dtOther;
                gvOtherProofList.DataBind();
                gvOtherProofList.Visible = true;

           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                rmVo = (RMVo)Session["RmVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                rmId = rmVo.RMId;
                foreach (GridViewRow gvr in this.gvAddressProofList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        proof = gvAddressProofList.DataKeys[gvr.RowIndex].Value.ToString();
                        proofCode = int.Parse(proof);
                        customerBo.SaveCustomerProofs(customerId, proofCode, rmId);
                    }
                }
                foreach (GridViewRow gvr in this.gvIDProofList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        proof = gvIDProofList.DataKeys[gvr.RowIndex].Value.ToString();
                        proofCode = int.Parse(proof);
                        customerBo.SaveCustomerProofs(customerId, proofCode, rmId);
                    }
                }
                foreach (GridViewRow gvr in this.gvOtherProofList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        proof = gvOtherProofList.DataKeys[gvr.RowIndex].Value.ToString();
                        proofCode = int.Parse(proof);
                        customerBo.SaveCustomerProofs(customerId, proofCode, rmId);
                    }
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProofsAdd.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = customerId;
                objects[3] = path;
                objects[4] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        
        }

        //protected void chkKYCFlag_CheckedChanged(object sender, EventArgs e)
        //{
         
        //    try
        //    {
        //        customerVo = (CustomerVo)Session["CustomerVo"];
        //        string assetInterest = Session["assetInterest"].ToString();
        //        path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

        //        if (chkKYCFlag.Checked)
        //            KYCFlag = 1;
        //        else
        //            KYCFlag = 0;

        //        if (assetInterest == "MF" || assetInterest == "Equity")
        //        {
        //            if (assetInterest == "Equity")
        //            {
        //                KYCFlag = 0;
        //            }

                   

        //            //string path=Server.MapPath(ConfigurationManager.AppSettings[xmllookuppath]);
        //            proofList = customerBo.GetProofList(customerVo.Type, KYCFlag, assetInterest, path);
        //        }
        //        else
        //        {
        //            proofList = customerBo.GetProofList(customerVo.Type, KYCFlag, "MF", path);
        //            proofList2 = customerBo.GetProofList(customerVo.Type, 0, "Equity", path);
        //            for (int i = 0; i < proofList2.Count; i++)
        //            {
        //                proofList.Add(proofList2[i]);
        //            }
        //        }
        //        DataTable dtCustomerProofs = new DataTable();

        //        dtCustomerProofs.Columns.Add("MandatoryIdentifier");

        //        DataRow drCustomerProof;
        //        for (int i = 0; i < proofList.Count; i++)
        //        {
        //            drCustomerProof = dtCustomerProofs.NewRow();
        //            customerVo = new CustomerVo();
        //            drCustomerProof["MandatoryIdentifier"] = proofList[i].ToString();
        //            dtCustomerProofs.Rows.Add(drCustomerProof);
        //        }
        //        gvProofList.DataSource = dtCustomerProofs;
        //        gvProofList.DataBind();
        //        gvProofList.Visible = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerProofsAdd.ascx:chkKYCFlag_CheckedChanged()");

        //        object[] objects = new object[7];

        //        objects[0] = rmVo;
        //        objects[1] = customerVo;                
        //        objects[2] = KYCFlag;
        //        objects[3] = proofList;
        //        objects[4] = proofList2;
        //        objects[5] = proofCode;
        //        objects[6] = path;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        rmVo = (RMVo)Session["RmVo"];
        //        customerVo = (CustomerVo)Session["CustomerVo"];
        //        customerId = customerVo.CustomerId;
        //        path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
        //        rmId = rmVo.RMId;
        //        foreach (GridViewRow gvr in this.gvProofList.Rows)
        //        {
        //            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
        //            {
        //                proof = gvProofList.DataKeys[gvr.RowIndex].Value.ToString();
        //                proofCode = customerBo.GetCustomerProofCode(proof, path);
        //                customerBo.SaveCustomerProofs(customerId, proofCode, rmId);
        //            }
        //        }
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CustomerProofsAdd.ascx:btnSubmit_Click()");
        //        object[] objects = new object[5];
        //        objects[0] = rmVo;
        //        objects[1] = customerVo;
        //        objects[2] = customerId;
        //        objects[3] = path;
        //        objects[4] = rmId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

    }
}
