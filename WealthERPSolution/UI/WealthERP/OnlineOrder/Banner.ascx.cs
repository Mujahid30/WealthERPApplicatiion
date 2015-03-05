using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BoOnlineOrderManagement;
namespace WealthERP.OnlineOrder
{
    public partial class Banner : System.Web.UI.UserControl
    {
        public string assetCategory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
                string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
                string innerHtml = String.Empty;
                DataTable dtProductBannerDetails = new DataTable();

                dtProductBannerDetails = onlineOrderBo.GetImageListForBanner(assetCategory);

                foreach (DataRow dr in dtProductBannerDetails.Rows)
                {
                    innerHtml += string.Format(@"<li><img src=""{0}/{1}"" /></li>", path.Replace("~", ".."), dr["PDB_BannerImage"].ToString());
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetBannerImageDetails", @"SetBannerImageDetails('" + innerHtml + "');", true);
            //}
        }
    }
}