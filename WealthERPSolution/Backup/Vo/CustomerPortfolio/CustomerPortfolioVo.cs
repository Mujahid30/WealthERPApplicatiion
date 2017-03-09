using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class CustomerPortfolioVo
    {
        #region Fields

        private int m_PortfolioId;       
        private int m_CustomerId;        
        private int m_IsMainPortfolio;       
        private string m_PortfolioTypeCode;        
        private string m_PMSIdentifier;
        private string m_PortfolioName;
        private string m_CategoryId;
        private string m_SubCategoryId;

        
        #endregion Fields


        #region Properties

        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }
        public string PortfolioName
        {
            get { return m_PortfolioName; }
            set { m_PortfolioName = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public int IsMainPortfolio
        {
            get { return m_IsMainPortfolio; }
            set { m_IsMainPortfolio = value; }
        }
        public string PortfolioTypeCode
        {
            get { return m_PortfolioTypeCode; }
            set { m_PortfolioTypeCode = value; }
        }
        public string PMSIdentifier
        {
            get { return m_PMSIdentifier; }
            set { m_PMSIdentifier = value; }
        }
        public string CategoryId
        {
            get { return m_CategoryId; }
            set { m_CategoryId = value; }
        }
        public string SubCategoryId
        {
            get { return m_SubCategoryId; }
            set { m_SubCategoryId = value; }
        }
        #endregion Properties

    }
}
