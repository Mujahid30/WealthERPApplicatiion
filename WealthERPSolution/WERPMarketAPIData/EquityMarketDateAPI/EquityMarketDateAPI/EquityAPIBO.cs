using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EquityMarketDateAPI
{
  public  class EquityAPIBO
    {
      EquityAPIDAO EquityAPIDAO = new EquityAPIDAO();
      public int CreateUpdateEquityMarketData(DataTable dt)
      {
          int result = 0;
          result = EquityAPIDAO.CreateUpdateEquityMarketData(dt);
          return result;
      }
      public int CreateUpdateEquityMasterData(DataTable dt)
      {
          int result = 0;
          result = EquityAPIDAO.CreateUpdateEquityMasterData(dt);
          return result;
      }
    }
}
