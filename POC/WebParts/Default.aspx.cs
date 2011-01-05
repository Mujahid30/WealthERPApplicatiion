using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode;
    }
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.DesignDisplayMode;
    }
    
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.CatalogDisplayMode;
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.EditDisplayMode;
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        WebPartManager1.DisplayMode = WebPartManager.ConnectDisplayMode;
    }

    protected void Welcome1_Load(object sender, EventArgs e)
    {

    }
}