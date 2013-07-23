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
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.CommisionManagement
{
    public partial class CommissionStructureToSchemeMapping : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        int StructureId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];

            if (!IsPostBack) {
                pnlGrid.Visible = false;
                BindStructDD();
                if (Request.QueryString["ID"] != null)
                {
                    string sStructId = Request.QueryString["ID"];
                    Session["StructureId"] = StructureId;
                    ddlStructures.SelectedValue = sStructId;
                    ddlStructures.Enabled = false;
                }
            }            
        }

        private void BindStructDD() {
            DataSet dsStructList = commisionReceivableBo.GetCMStructNames(advisorVo.advisorId, 0);

            DataRow drStruct = dsStructList.Tables[0].NewRow();
            drStruct["ACSM_CommissionStructureId"] = 0;
            drStruct["ACSM_CommissionStructureName"] = "All";
            dsStructList.Tables[0].Rows.InsertAt(drStruct, 0);
            ddlStructures.DataSource = dsStructList.Tables[0];
            ddlStructures.DataValueField = dsStructList.Tables[0].Columns["ACSM_CommissionStructureId"].ToString();
            ddlStructures.DataTextField = dsStructList.Tables[0].Columns["ACSM_CommissionStructureName"].ToString();
            ddlStructures.DataBind();
        }

        private void CreateMappedSchemeGrid() {
            DataSet dsMappedSchemes = new DataSet();
            dsMappedSchemes = commisionReceivableBo.GetMappedSchemes(this.StructureId);
            gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
            gvMappedSchemes.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "MappedSchemes", dsMappedSchemes);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            StructureId = int.Parse(ddlStructures.SelectedItem.Value);
            pnlGrid.Visible = true;
            CreateMappedSchemeGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void gvMappedSchemes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataSet dsMappedSchemes = new DataSet();
            if (Cache[userVo.UserId.ToString() + "MappedSchemes"] != null)
            {
                dsMappedSchemes = (DataSet)Cache[userVo.UserId.ToString() + "MappedSchemes"];
                gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
            }
        }

        protected void gvMappedSchemes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvMappedSchemes.CurrentPageIndex = e.NewPageIndex;
            CreateMappedSchemeGrid();
        }

        protected void ddlStructures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}