using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOnlineOrderManagemnet
{
    public class OnlineMFOrderVo:OnlineOrderVo
    {
        #region Fields

        private int m_SchemePlanCode;
        private double m_Amount;
        #endregion

        public int SchemePlanCode
        {
            get { return m_SchemePlanCode; }
            set { m_SchemePlanCode = value; }
        }
        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

    }
}
