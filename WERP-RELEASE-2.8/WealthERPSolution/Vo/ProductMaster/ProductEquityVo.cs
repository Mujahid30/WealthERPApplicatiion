using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoProductMaster
{
    public class ProductEquityVo
    {
        #region Fields

        private int m_EquityCode;
        private string m_CompanyName;
        private string m_Exchange;
        private string m_NSECode;    
        private string m_BSECode;       

        #endregion Fields



        #region Properties

        public int EquityCode
        {
            get { return m_EquityCode; }
            set { m_EquityCode = value; }
        }
        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }
        public string Exchange
        {
            get { return m_Exchange; }
            set { m_Exchange = value; }
        }
        public string NSECode
        {
            get { return m_NSECode; }
            set { m_NSECode = value; }
        }
        public string BSECode
        {
            get { return m_BSECode; }
            set { m_BSECode = value; }
        }

        #endregion Properties
    }
}
