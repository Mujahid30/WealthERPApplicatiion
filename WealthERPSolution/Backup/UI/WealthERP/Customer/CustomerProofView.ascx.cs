using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VoUser;

namespace WealthERP.Customer
{
    public partial class CustomerProofView : System.Web.UI.UserControl
    {
        protected string imgPath;
        protected string strExt;
        protected string strFileName;

        CustomerVo customerVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ImagePath"] != null)
                imgPath = Request.QueryString["ImagePath"].ToString();
            //proofImage. = imgPath;
            //if (!imgPath.Equals(String.Empty))
            //{
            //    string[] strArray = new string[2];
            //    strArray = imgPath.Split('\\');
            //    strFileName = strArray[1];
            //    string[] strExtn = new string[2];
            //    strExtn = strFileName.Split('.');
            //    strExt = strExtn[1];
            //}
            
            //Response.ContentType = "image/jpeg"; // for JPEG file
            //string physicalFileName = imgPath;
            //Response.WriteFile(physicalFileName);


            strExt = String.Empty;
            //string proofPath = String.Empty;
            strFileName = String.Empty;
            
            strExt = Request.QueryString["strExt"].ToString();
            strFileName = Request.QueryString["strFileName"].ToString();

            ltrCtrl.Text = LoadImage(strExt, imgPath, strFileName);
        }

        protected string LoadImage(string extension, string proofPath, string fileName)
        {
            customerVo = new CustomerVo();
            string control = "";

            if (extension != ".pdf")
            {
                control = "<ul class=\"quickZoom\">"
                          // + "<li style=\"text-align: center; float: left;\">"
                           + "<img src=\"../General/ImageServe.aspx?Path=" + proofPath + "\" style=\"float: left;\" alt=\"Image file\" />"
                           //+ "</li>"
                           + "</ul>"
                           + "<br />"
                           + "<br />";
            }
            else
            {
                string Filepath = Server.MapPath("TempCustomerProof") + "\\" + customerVo.CustomerId.ToString() + "\\";
                Directory.CreateDirectory(Filepath);
                //Filepath = Server.MapPath("TempCustomerProof") + "\\" + customerVo.CustomerId.ToString() + "\\";
                Filepath = Filepath + fileName;
                if (!File.Exists(Filepath))
                {
                    File.Copy(proofPath, Filepath);
                }
                control = "<a href=\"../TempCustomerProof/" + customerVo.CustomerId.ToString() + "/" + fileName + "\" target=\"_blank\" class=\"LinkButtons\" >" + fileName + "</a>";
            }
            return control;
        }
    }
}