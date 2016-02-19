using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace WealthERP
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["fileName"] != null)
            {

                string strURL = "~/UploadFiles/" + Request.QueryString["fileName"];
                if (System.IO.File.Exists(Server.MapPath(strURL)))
                {
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    Response.ContentType = "application/ms-excel";
                    //response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL));
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Request.QueryString["fileName"]));
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=" + Request.QueryString["fileName"]);
                    byte[] data = req.DownloadData(Server.MapPath(strURL));
                    response.BinaryWrite(data);
                    System.IO.File.Delete(Server.MapPath(strURL));
                    response.End();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "closeWindow", "closeWin();", true);

            }
        }
    }
}
