using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoAdvisorProfiling;
using VoUser;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AddLOB : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (!IsPostBack)
            {
                chkDerivative.Visible = false;
                chkCash.Visible = false;
                chkBroker.Visible = false;
                chkSubbroker.Visible = false;
                chkRemissary.Visible = false;
                chkIntermediary.Visible = false;
                chkInsuranceAgent.Visible = false;
                chkPostalAgent.Visible = false;
                chkLiabilitiesAgent.Visible = false;
                chkPMSBroker.Visible = false;
                chkPMSCash.Visible = false;
                chkPMSSubBroker.Visible = false;
                chkPMSDerivative.Visible = false;
                chkPMSRemissary.Visible = false;
                chkRealEstateAgent.Visible = false;
                chkCommBroker.Visible = false;
                chkCommCash.Visible = false;
                chkCommSubBroker.Visible = false;
                chkCommDerivative.Visible = false;
                chkCommRemissary.Visible = false;
                chkFIAgent.Visible = false;
            }
            lblErrorMsg.Visible = false;
        }

        //protected void chkMFEQ_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkMf.Checked == false && chkEquity.Checked == false)
        //    {
        //        chkBroker.Enabled = false;
        //        chkSubbroker.Enabled = false;
        //        chkRemissary.Enabled = false;
        //        chkIntermediary.Enabled = false;
        //        chkCash.Enabled = false;
        //        chkDerivative.Enabled = false;
        //    }
        //    else if (chkMf.Checked == false && chkEquity.Checked == true)
        //    {
        //        chkDerivative.Enabled = true;
        //        chkCash.Enabled = true;
        //        chkBroker.Enabled = true;
        //        chkSubbroker.Enabled = true;
        //        chkRemissary.Enabled = true;
        //        chkIntermediary.Enabled = false;
        //    }
        //    else if (chkMf.Checked == true && chkEquity.Checked == false)
        //    {
        //        chkDerivative.Enabled = false;
        //        chkCash.Enabled = false;
        //        chkBroker.Enabled = false;
        //        chkSubbroker.Enabled = false;
        //        chkRemissary.Enabled = false;
        //        chkIntermediary.Enabled = true;
        //    }
        //    else if (chkMf.Checked == true && chkEquity.Checked == true)
        //    {
        //        chkBroker.Enabled = true;
        //        chkSubbroker.Enabled = true;
        //        chkRemissary.Enabled = true;
        //        chkIntermediary.Enabled = true;
        //        chkCash.Enabled = true;
        //        chkDerivative.Enabled = true;
        //    }
        //}

        protected void btnAddLOB_Click(object sender, EventArgs e)
        {
            AdvisorLOBBo AdvisorLOBBo = new AdvisorLOBBo();
            try
            {
                Session["LOBId"] = "lob";
                Session["mf1"] = null;
                Session["equityBrokerCash1"] = null;
                Session["equityBrokerDerivative1"] = null;
                Session["equitySubBrokerCash1"] = null;
                Session["equitySubBrokerDerivative1"] = null;
                Session["equityRemissaryCash1"] = null;
                Session["equityRemissaryDerivative1"] = null;
                Session["pmsBrokerCash1"] = null;
                Session["pmsBrokerDerivative1"] = null;
                Session["pmsSubBrokerCash1"] = null;
                Session["pmsSubBrokerDerivative1"] = null;
                Session["pmsRemissaryCash1"] = null;
                Session["pmsRemissaryDerivative1"] = null;
                Session["commBrokerCash1"] = null;
                Session["commBrokerDerivative1"] = null;
                Session["commSubBrokerCash1"] = null;
                Session["commSubBrokerDerivative1"] = null;
                Session["commRemissaryCash1"] = null;
                Session["commRemissaryDerivative1"] = null;
                Session["insuranceAgent1"] = null;
                Session["postalSavingsAgent1"] = null;
                Session["realEstateAgent1"] = null;
                Session["liabilitiesAgent1"] = null;
                Session["fixedIncomeAgent1"] = null;

                if (chkMf.Checked == true && chkIntermediary.Checked == true)
                {
                    if (!AdvisorLOBBo.CheckLOBExistence(advisorVo.advisorId, "LMIT"))
                    {
                        Session["mf1"] = "mf";
                    }
                    else
                    {
                        chkMf.Checked = false;
                        chkIntermediary.Checked = false;
                        chkIntermediary.Visible = false;
                        lblErrorMsg.Visible = true;
                    }
                }
                if (chkInsurance.Checked && chkInsuranceAgent.Checked)
                    Session["insuranceAgent1"] = "insuranceAgent";
                if (chkPostalSavings.Checked && chkPostalAgent.Checked)
                    Session["postalSavingsAgent1"] = "postalSavingsAgent";
                if (chkRealEstate.Checked && chkRealEstateAgent.Checked)
                    Session["realEstateAgent1"] = "realEstateAgent";
                if (chkLiabilities.Checked && chkLiabilitiesAgent.Checked)
                    Session["liabilitiesAgent1"] = "liabilitiesAgent";
                if (chkFixedIncome.Checked && chkFIAgent.Checked)
                    Session["fixedIncomeAgent1"] = "fixedIncomeAgent";
                if (chkEquity.Checked == true)
                {
                    if (chkBroker.Checked == true)
                    {
                        if (chkCash.Checked == true)
                        {
                            Session["equityBrokerCash1"] = "equityBrokerCash";
                        }
                        if (chkDerivative.Checked == true)
                        {
                            Session["equityBrokerDerivative1"] = "equityBrokerDerivative";
                        }
                    }
                    if (chkSubbroker.Checked == true)
                    {
                        if (chkCash.Checked == true)
                        {
                            Session["equitySubBrokerCash1"] = "equitySubBrokerCash";
                        }
                        if (chkDerivative.Checked == true)
                        {
                            Session["equitySubBrokerDerivative1"] = "equitySubBrokerDerivaitve";
                        }
                    }
                    if (chkRemissary.Checked == true)
                    {
                        if (chkCash.Checked == true)
                        {
                            Session["equityRemissaryCash1"] = "equityRemissaryCash";
                        }
                        if (chkDerivative.Checked == true)
                        {
                            Session["equityRemissaryDerivative1"] = "equityRemissaryDerivative";
                        }
                    }
                }

                if (chkPMS.Checked)
                {
                    if (chkPMSBroker.Checked)
                    {
                        if (chkPMSCash.Checked)
                        {
                            Session["pmsBrokerCash1"] = "pmsBrokerCash";
                        }
                        if (chkPMSDerivative.Checked)
                        {
                            Session["pmsBrokerDerivative1"] = "pmsBrokerDerivative";
                        }
                    }
                    if (chkPMSSubBroker.Checked)
                    {
                        if (chkPMSCash.Checked)
                        {
                            Session["pmsSubBrokerCash1"] = "pmsSubBrokerCash";
                        }
                        if (chkPMSDerivative.Checked)
                        {
                            Session["pmsSubBrokerDerivative1"] = "pmsSubBrokerDerivative";
                        }
                    }
                    if (chkPMSRemissary.Checked)
                    {
                        if (chkPMSCash.Checked)
                        {
                            Session["pmsRemissaryCash1"] = "pmsRemissaryCash";
                        }
                        if (chkPMSDerivative.Checked)
                        {
                            Session["pmsRemissaryDerivative1"] = "pmsRemissaryDerivative";
                        }
                    }
                }


                if (chkCommodities.Checked)
                {
                    if (chkCommBroker.Checked)
                    {
                        if (chkCommCash.Checked)
                        {
                            Session["commBrokerCash1"] = "commBrokerCash";
                        }
                        if (chkCommDerivative.Checked)
                        {
                            Session["commBrokerDerivative1"] = "commBrokerDerivative";
                        }
                    }
                    if (chkCommSubBroker.Checked)
                    {
                        if (chkCommCash.Checked)
                        {
                            Session["commSubBrokerCash1"] = "commSubBrokerCash";
                        }
                        if (chkCommDerivative.Checked)
                        {
                            Session["commSubBrokerDerivative1"] = "commSubBrokerDerivative";
                        }
                    }
                    if (chkCommRemissary.Checked)
                    {
                        if (chkCommCash.Checked)
                        {
                            Session["commRemissaryCash1"] = "commRemissaryCash";
                        }
                        if (chkCommDerivative.Checked)
                        {
                            Session["commRemissaryDerivative1"] = "commRemissaryDerivative";
                        }
                    }
                }
                if(Session["mf1"] != null || 
                Session["equityBrokerCash1"] != null ||
                Session["equityBrokerDerivative1"] != null ||
                Session["equitySubBrokerCash1"] != null ||
                Session["equitySubBrokerDerivative1"] != null ||
                Session["equityRemissaryCash1"] != null ||
                Session["equityRemissaryDerivative1"] != null ||
                Session["pmsBrokerCash1"] != null ||
                Session["pmsBrokerDerivative1"] != null ||
                Session["pmsSubBrokerCash1"] != null ||
                Session["pmsSubBrokerDerivative1"] != null ||
                Session["pmsRemissaryCash1"] != null ||
                Session["pmsRemissaryDerivative1"] != null ||
                Session["commBrokerCash1"] != null ||
                Session["commBrokerDerivative1"] != null ||
                Session["commSubBrokerCash1"] != null ||
                Session["commSubBrokerDerivative1"] != null ||
                Session["commRemissaryCash1"] != null ||
                Session["commRemissaryDerivative1"] != null ||
                Session["insuranceAgent1"] != null ||
                Session["postalSavingsAgent1"] != null ||
                Session["realEstateAgent1"] != null ||
                Session["liabilitiesAgent1"] != null ||
                Session["fixedIncomeAgent1"] != null)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AddLOB.ascx:chkEquity_CheckedChanged()");

                object[] objects = new object[1];
                objects[0] = null;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void chkMF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMf.Checked)
                chkIntermediary.Visible = true;
            else
                chkIntermediary.Visible = false;
        }

        protected void chkEQ_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEquity.Checked)
            {
                chkBroker.Visible = true;
                chkSubbroker.Visible = true;
                chkRemissary.Visible = true;
                chkCash.Visible = true;
                chkDerivative.Visible = true;
            }
            else
            {
                chkBroker.Visible = false;
                chkSubbroker.Visible = false;
                chkRemissary.Visible = false;
                chkCash.Visible = false;
                chkDerivative.Visible = false;
            }
        }

        protected void chkInsurance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInsurance.Checked)
                chkInsuranceAgent.Visible = true;
            else
                chkInsuranceAgent.Visible = false;
        }

        protected void chkPostalSavings_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPostalSavings.Checked)
                chkPostalAgent.Visible = true;
            else
                chkPostalAgent.Visible = false;
        }

        protected void chkLiabilities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabilities.Checked)
                chkLiabilitiesAgent.Visible = true;
            else
                chkLiabilitiesAgent.Visible = false;
        }

        protected void chkPMS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPMS.Checked)
            {
                chkPMSBroker.Visible = true;
                chkPMSCash.Visible = true;
                chkPMSDerivative.Visible = true;
                chkPMSRemissary.Visible = true;
                chkPMSSubBroker.Visible = true;
            }
            else
            {
                chkPMSBroker.Visible = false;
                chkPMSCash.Visible = false;
                chkPMSDerivative.Visible = false;
                chkPMSRemissary.Visible = false;
                chkPMSSubBroker.Visible = false;
            }
        }

        protected void chkRealEstate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealEstate.Checked)
                chkRealEstateAgent.Visible = true;
            else
                chkRealEstateAgent.Visible = false;
        }

        protected void chkCommodities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommodities.Checked)
            {
                chkCommRemissary.Visible = true;
                chkCommSubBroker.Visible = true;
                chkCommBroker.Visible = true;
                chkCommCash.Visible = true;
                chkCommDerivative.Visible = true;
            }
            else
            {
                chkCommRemissary.Visible = false;
                chkCommSubBroker.Visible = false;
                chkCommBroker.Visible = false;
                chkCommCash.Visible = false;
                chkCommDerivative.Visible = false;
            }
        }

        protected void chkFixedIncome_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFixedIncome.Checked)
                chkFIAgent.Visible = true;
            else
                chkFIAgent.Visible = false;
        }
    }
}