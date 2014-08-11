using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using BoCommon;
using BoOnlineOrderManagement;
using Telerik.Web.UI;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class MFHoldingRecon : System.Web.UI.UserControl
    {
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            UserVo userVo = new UserVo();
            AdvisorVo adviserVo = new AdvisorVo();
        }
        protected void BindIssueName()
        {
            DataTable dtGetIssueName = new DataTable();

            dtGetIssueName = OnlineOrderMISBo.GetMFHolding();
            ddlIssue.DataSource = dtGetIssueName;
            ddlIssue.DataValueField = dtGetIssueName.Columns["WR_RequestId"].ToString();
            ddlIssue.DataTextField = dtGetIssueName.Columns["WRD_InputParameterValue"].ToString();
            ddlIssue.DataBind();
            ddlIssue.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void BindSchemeMIS()
        {
            try
            {
                DataSet dsSchemeMIs = new DataSet();
                DataTable dtschememis = new DataTable();
                dsSchemeMIs = OnlineOrderMISBo.GetSchemeMIS(hdnAssettype.Value, int.Parse(ddlTosee.SelectedItem.Value), hdnStatus.Value);
                dtschememis = dsSchemeMIs.Tables[0];

                if (dtschememis.Rows.Count > 0)
                {
                    if (Cache["SchemeMIS" + userVo.UserId] == null)
                    {
                        Cache.Insert("SchemeMIS" + userVo.UserId, dtschememis);
                    }
                    else
                    {
                        Cache.Remove("SchemeMIS" + userVo.UserId);
                        Cache.Insert("SchemeMIS" + userVo.UserId, dtschememis);
                    }
                    gvonlineschememis.DataSource = dtschememis;
                    gvonlineschememis.DataBind();
                    SchemeMIS.Visible = true;
                    pnlSchemeMIS.Visible = true;
                }
                else
                {
                    // tdtosee.Visible = false;
                    gvonlineschememis.DataSource = dtschememis;
                    gvonlineschememis.DataBind();
                    SchemeMIS.Visible = true;
                    pnlSchemeMIS.Visible = true;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:SetParameter()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}