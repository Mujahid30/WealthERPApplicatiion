using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoProductMaster
{
    public class ProductMFAMCVo
    {
        #region Fields

        private int m_AMCCode;
        private string m_AMCName;

        #endregion Fields




        #region Properties

        public int AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }
        public string AMCName
        {
            get { return m_AMCName; }
            set { m_AMCName = value; }
        }

        #endregion Properties

    }
}
