using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class PopUp : System.Web.UI.Page
    {
        int count;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["table"] != null)
            {

                Table tb = (Table)Session["table"];
                Page.Form.Controls.Add(tb);
            }
            if (!IsPostBack)
            {
              
                if(Session["count"].ToString() != string.Empty)
                    count = int.Parse(Session["count"].ToString());
                createId();
            }

           
         }

       

        public void createId()
        {


            LiteralControl literal = new LiteralControl();
            Table tb = new Table();

            TableCell tc;

            //Terminal.Controls.Add(tb);
        
            for (int i = 0; i < count; i++)
            {
                TableRow tr = new TableRow();
               
                    tc = new TableCell();
                    Label lbl = new Label();                
                    lbl.ID = "lblTerminalId" + (i).ToString();
                    lbl.Text = "TerminalId " + (i + 1).ToString();                
                    tc.Controls.Add(lbl);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox = new TextBox();
                    txtBox.ID = "txtTerminalId" + i.ToString();                                        
                    tc.Controls.Add(txtBox);                    
                    tr.Cells.Add(tc);
                                
                tb.Rows.Add(tr);
            }
            PlaceHolder1.Controls.Add(tb);
            Session["table"] = tb;
          
        }
       

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<int> terminalList=null;            
            count = int.Parse(Session["count"].ToString());
            terminalList = new List<int>();
            for (int i = 0; i < count; i++)
            {                
                int txt = int.Parse(((TextBox)PlaceHolder1.FindControl("txtTerminalId" + i.ToString())).Text.ToString());
                terminalList.Add(txt);                
            }
            Session["terminalId"] = terminalList;
            Button1.Enabled = false;            
            Response.Write("<script language=javascript> window.close();</script>");

         
            
        }
    }
}
