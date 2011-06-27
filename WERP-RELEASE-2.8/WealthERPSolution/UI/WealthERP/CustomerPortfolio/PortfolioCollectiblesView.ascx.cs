using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;


namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioCollectiblesView : System.Web.UI.UserControl
    {
        CollectiblesVo collectiblesVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                collectiblesVo = (CollectiblesVo)Session["collectiblesVo"];
                LoadFields();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCollectiblesView.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = collectiblesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void LoadFields()
        {
            try
            {
                lblInsCategory.Text = collectiblesVo.AssetCategoryCode.ToString();
                lblAssetParticulars.Text = collectiblesVo.Name.ToString();
                lblCurrentValue.Text = collectiblesVo.CurrentValue.ToString();
                lblPurchaseDate.Text = collectiblesVo.PurchaseDate.ToShortDateString().ToString();
                lblPurchaseValue.Text = collectiblesVo.PurchaseValue.ToString();
                lblRemarks.Text = collectiblesVo.Remark.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCollectiblesView.ascx:LoadFields()");
                object[] objects = new object[1];
                objects[0] = collectiblesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
    }
}