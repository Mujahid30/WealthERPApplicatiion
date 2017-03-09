using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using WatiN.Core;
using BoCommon;

namespace WERP_DAILY_MF_GOLD_PRICE_UPDATE
{
    public class ProcessGoldScrappingBo
    {
        private static DateTime goldUpdatedDate;
        private static decimal goldPrice;
        
        public void GoldPriceUpdateProcessor()
        {
           OpenNCDXWebsiteforGoldPriceScrapping();
            
        }
        private void OpenNCDXWebsiteforGoldPriceScrapping()
        {
            using (var browser = new IE("http://www.ncdex.com/MarketData/LiveSpotQuotes.aspx"))
            {
                RetrieveGoldPricefromNCDXPage(browser);
            }
        }

        private void RetrieveGoldPricefromNCDXPage(IE browser)
        {
            ProcessGoldScrappingDao processGoldScrappingDao = new ProcessGoldScrappingDao();
            int i = 0;
            Table _table = browser.Table(Find.ById("ctl00_ContentPlaceHolder3_gvSpotQuotes"));
            if (_table.TableRows.Count != 0)
            {
                foreach (TableRow TR in _table.TableRows)
                {
                    if ((_table.TableRows[i].TableCells[1].Text == "GOLD (100 gms)" && _table.TableRows[i].TableCells[3].Text == "Mumbai"))
                    {
                        goldPrice = decimal.Parse(_table.TableRows[i].TableCells[5].Text);
                        goldUpdatedDate = DateTime.Now;
                        break;
                    }
                    else
                        i++;
                }
            }
            processGoldScrappingDao.UpdateGoldPrice(goldPrice, goldUpdatedDate);

        }

        
    }
}
