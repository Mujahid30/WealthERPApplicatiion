using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoProductMaster
{
    public class ProductMFSchemeVo
    {
        #region Fields

        private int m_SchemeCode;       
        private string m_SchemeName;       

        #endregion Fields



        #region Properties

        public int SchemeCode
        {
            get { return m_SchemeCode; }
            set { m_SchemeCode = value; }
        }
        public string SchemeName
        {
            get { return m_SchemeName; }
            set { m_SchemeName = value; }
        }

        #endregion Properties
    }
}
