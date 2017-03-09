using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoProductMaster
{
    public class ProductMFSchemePlanVo
    {
        #region Fields

        private int m_SchemePlanCode;
        private string m_SchemePlan;
        private int m_SchemeCode;
        private int m_AMCCode;     

        #endregion Fields



        #region Properties

        public int SchemePlanCode
        {
            get { return m_SchemePlanCode; }
            set { m_SchemePlanCode = value; }
        }
        public string SchemePlan
        {
            get { return m_SchemePlan; }
            set { m_SchemePlan = value; }
        }
        public int SchemeCode
        {
            get { return m_SchemeCode; }
            set { m_SchemeCode = value; }
        }
        public int AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }

        #endregion Properties
    }
}
