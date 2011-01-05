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

public partial class ProgrammableWebPart : System.Web.UI.UserControl
{
    string _text = "Hello";

    public string Text
    {
        get { return _text; }
        set
        {
            Label1.Text = value;
            _text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = _text;
    }
}
