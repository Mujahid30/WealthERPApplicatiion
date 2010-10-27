using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using System.Web.Services;
using System.Xml;
using System.Web.Script.Services;
using System.Xml.Linq;
namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminMessageBroadcast : System.Web.UI.UserControl
    {
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        protected void Page_Init(object sender, EventArgs e)
        {
            

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsMessage = advisermaintanencebo.GetMessageBroadcast();
            if (dsMessage != null)
            {
                MessageBroadcast.Visible = true;
                if (dsMessage.Tables[0].Rows[0]["ABM_IsActive"].ToString() == "1")
                {
                    DateTime dtMessageDate = DateTime.Parse(dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessageDate"].ToString());
                    lblSuperAdmnMessage.Text = "Last Sent Message:" + dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() + Environment.NewLine + " Sent on:" + dtMessageDate.ToString();                    
                }
            }
        }
    //    [WebMethod]
    //    [ScriptMethod(UseHttpGet = false,
    //XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
    //    public XmlDocument XmlData()
    //    {
    //        int page = 1;
    //        int total = 4;
    //        XDocument xmlDoc = new XDocument(
    //            new XDeclaration("1.0", "utf-8", "yes"),
    //            new XElement("rows",
    //            new XElement("page", page.ToString()),
    //            new XElement("total", total.ToString(),
    //                new XElement("row", new XAttribute("id", "111"),
    //                    new XElement("cell", "111"),
    //                    new XElement("cell", "row1"),
    //                    new XElement("cell", "rowDescription1"),
    //                    new XElement("cell", "rowUnit1"),
    //                    new XElement("cell", "rowUnitPrice1"),
    //                    new XElement("cell", DateTime.Now.ToShortDateString())
    //    ),
    //    new XElement("row", new XAttribute("id", "222"),
    //                    new XElement("cell", "222"),
    //                    new XElement("cell", "row2"),
    //                    new XElement("cell", "rowDescription2"),
    //                    new XElement("cell", "rowUnit2"),
    //                    new XElement("cell", "rowUnitPrice2"),
    //                    new XElement("cell", DateTime.Now.ToShortDateString())
    //    ),
    //     new XElement("row", new XAttribute("id", "333"),
    //                    new XElement("cell", "333"),
    //                    new XElement("cell", "row3"),
    //                    new XElement("cell", "rowDescription3"),
    //                    new XElement("cell", "rowUnit3"),
    //                    new XElement("cell", "rowUnitPrice3"),
    //                    new XElement("cell", DateTime.Now.ToShortDateString())
    //    ),
    //       new XElement("row", new XAttribute("id", "444"),
    //                    new XElement("cell", "444"),
    //                    new XElement("cell", "row4"),
    //                    new XElement("cell", "rowDescription4"),
    //                    new XElement("cell", "rowUnit4"),
    //                    new XElement("cell", "rowUnitPrice4"),
    //                    new XElement("cell", DateTime.Now.ToShortDateString())
    //    )
    //                                       )
    //                            )
    //    );

    //        XmlDocument newDoc = new XmlDocument();
    //        newDoc.LoadXml(xmlDoc.ToString());
    //        return newDoc;
    //    }
        protected void SendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
                DateTime dtExpiry = new DateTime();
                bool result=false;
                result = DateTime.TryParse(txtExpiryDate.Text.ToString(), out dtExpiry);
                advisermaintanencebo.MessageBroadcastSendMessage(MessageBox.Text, DateTime.Now,dtExpiry);
                SuccessMessage.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}