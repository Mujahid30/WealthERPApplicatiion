using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WERP_DAILY_MF_GOLD_PRICE_UPDATE
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {

            ProcessGoldScrapping();

        }

        public static void ProcessGoldScrapping()
        {
            ProcessGoldScrappingBo processGoldScrappingBo = new ProcessGoldScrappingBo();
            processGoldScrappingBo.GoldPriceUpdateProcessor();
        }
    }
}
