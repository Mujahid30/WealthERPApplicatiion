using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class MoneyBackEpisodeAdd : System.Web.UI.Page
    {
        int count;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
           count = int.Parse(Session["episodeCount"].ToString());


           if (Session["table"] != null)
           {

               Table tb = (Table)Session["table"];
               this.PlaceHolder.Controls.Add(tb);
           }

           else
           {
               LoadContent();
           }
        }

        public void LoadContent()
        {
            LiteralControl literal = new LiteralControl();
            Table tb = new Table();
            TableCell tc;

            for (int i = 0; i < count; i++)
            {
                TableRow tr = new TableRow();
                tc = new TableCell();
                TextBox txtBox1 = new TextBox();
                txtBox1.ID = "txtPaymentDate" + i.ToString();
                txtBox1.CssClass = "txtField";
                tc.Controls.Add(txtBox1);
                tr.Cells.Add(tc);

                tc = new TableCell();
                TextBox txtBox2 = new TextBox();
                txtBox2.ID = "txtRepaidPer" + i.ToString();
                //txtBox2.ID = ds.Tables[0].Rows[i][0].ToString();
                txtBox2.CssClass = "txtField";
                tc.Controls.Add(txtBox2);
                tr.Cells.Add(tc);

                tb.Rows.Add(tr);
            }
            PlaceHolder.Controls.Add(tb);
            Session["table"] = tb;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<MoneyBackEpisodeVo> moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
            MoneyBackEpisodeVo moneyBackEpisodeVo;
            for (int i = 0; i < count; i++)
            {
                moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                string paymentDate = (((TextBox)PlaceHolder.FindControl("txtPaymentDate" + i.ToString())).Text.ToString());
                moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                string repaidPercent = (((TextBox)PlaceHolder.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                moneyBackEpisodeList.Add(moneyBackEpisodeVo);
            }
            Session["episodeList"] = moneyBackEpisodeList;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
        }
    }
}