using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ProviderWebPart : System.Web.UI.UserControl,ITextTransfer
{
    [ConnectionProvider("Text", "TextProvider")]
    public ITextTransfer GetTextTransferInterface()
    {
        return this;
    }

    public string GetText()
    {
        return TextBox1.Text;
    }
  
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
