using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.Reports
{
    public partial class FPOfflineForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            //if (!IsPostBack)
            //{
            //    btnViewReport.Visible = true;
            //    btnViewInPDF.Visible = false;
            //    btnViewInDOC.Visible = false;
            //}
           
        }
        //protected void ddlBtnSelect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBtnSelect.SelectedIndex != -1)
        //    {
        //        if (ddlBtnSelect.SelectedValue == "ViewReport")
        //        {
        //            btnViewReport.Visible = true;
        //            btnViewInPDF.Visible = false;
        //            btnViewInDOC.Visible = false;
        //        }
        //        if (ddlBtnSelect.SelectedValue == "ViewInPdf")
        //        {
        //            btnViewReport.Visible = false;
        //            btnViewInPDF.Visible = true;
        //            btnViewInDOC.Visible = false;
        //        }
        //        if (ddlBtnSelect.SelectedValue == "ViewInDoc")
        //        {
        //            btnViewReport.Visible = false;
        //            btnViewInPDF.Visible = false;
        //            btnViewInDOC.Visible = true;
        //        }
        //    }
        //}
    }

}