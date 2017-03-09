using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineMFOrderManagement : System.Web.UI.UserControl
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                //ShowHideControls("Transact");
            }


        }

        protected void lnkTransact_Click(object sender, EventArgs e)
        {
            ShowHideControls("Transact");
        }

        protected void lnkBooks_Click(object sender, EventArgs e)
        {
            ShowHideControls("OrderBook");
        }

        protected void lnkHoldings_Click(object sender, EventArgs e)
        {
            ShowHideControls("UniHolding");
        }

        protected void ShowHideControls(string type)
        {
            trTransact.Visible = false;
            trBooks.Visible = false;
            trUnitHoldings.Visible = false;
            switch (type)
            {
                case "Transact":
                    {
                        trTransact.Visible = true;
                        break;
                    }
                case "OrderBook":
                    {
                        trBooks.Visible = true;
                        break;
                    }
                case "UniHolding":
                    {
                        trUnitHoldings.Visible = true;
                        break;

                    }
            }

        }
        

    }
}