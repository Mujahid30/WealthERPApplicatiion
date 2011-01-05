using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wizard_POC
{
    [Serializable]
    public class RiskOptionVo
    {
        private string m_Option;

        public string Option
        {
            get { return m_Option; }
            set { m_Option = value; }
        }
        private int m_Value;

        public int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }
}
