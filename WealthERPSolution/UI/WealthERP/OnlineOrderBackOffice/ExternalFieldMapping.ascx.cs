using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOnlineOrderManagemnet;
using VoUser;
using WealthERP.Base;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class ExternalFieldMapping : System.Web.UI.UserControl
    {
        OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                //BindDropDownListType();
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindDropDownListType();


        }

        protected void BindDropDownListType()
        {
            //CommonLookupBo CommonLookupBo = new CommonLookupBo();
            OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();
            DataSet dsGetSourceCode = OnlineCommonBackOfficeBo.GetSourceCode();
            ddlType.DataTextField = dsGetSourceCode.Tables[0].Columns["XES_SourceName"].ToString();
            ddlType.DataValueField = dsGetSourceCode.Tables[0].Columns["XES_SourceCode"].ToString();
            if (ddlPrduct.SelectedValue == "MF")
            {
                if (dsGetSourceCode.Tables[0].Rows.Count > 0)
                {
                    ddlType.DataSource = dsGetSourceCode.Tables[0];
                    ddlType.DataBind();
                }
            }
            if (ddlPrduct.SelectedValue == "BO")
            {
                if (dsGetSourceCode.Tables[1].Rows.Count > 0)
                {
                    ddlType.DataSource = dsGetSourceCode.Tables[1];
                    ddlType.DataBind();
                }
            }
        }
        protected void BindInternalHeaderMApping()
        {

            DataSet dsGetInternalHeaderMapping = new DataSet();
            try
            {
                dsGetInternalHeaderMapping = OnlineCommonBackOfficeBo.GetInternalHeaderMapping(ddlType.SelectedValue);
                if (dsGetInternalHeaderMapping.Tables[0].Rows.Count > 0)
                {


                    if (Cache["HeaderMapping" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("HeaderMapping" + advisorVo.advisorId);
                        Cache.Insert("HeaderMapping" + advisorVo.advisorId, dsGetInternalHeaderMapping);

                    }
                    else
                    {
                        Cache.Remove("HeaderMapping" + advisorVo.advisorId);
                        Cache.Insert("HeaderMapping" + advisorVo.advisorId, dsGetInternalHeaderMapping);
                    }
                    gvHeaderMapping.DataSource = dsGetInternalHeaderMapping;
                    gvHeaderMapping.DataBind();
                    AdviserIssueList.Visible = true;
                }
                else
                {
                    gvHeaderMapping.DataSource = dsGetInternalHeaderMapping;
                    gvHeaderMapping.DataBind();
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
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindInternalHeaderMApping()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindInternalHeaderMApping();
        }
        protected void gvHeaderMapping_OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
           
             DataSet dsGetInternalHeaderMapping = new DataSet();
            
            dsGetInternalHeaderMapping = (DataSet)Cache["HeaderMapping" + advisorVo.advisorId];

            if(dsGetInternalHeaderMapping!=null)
                {
                gvHeaderMapping.DataSource=dsGetInternalHeaderMapping;
                }
        }
    }
}

