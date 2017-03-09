using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class PersonalVo
    {
        #region Fields

        private int m_PersonalPortfolioId;       
        private int m_PortfolioId;
        private string m_AssetSubCategoryCode;        
        private string m_AssetCategoryCode;
        private string m_AssetSubCategoryName;
        private string m_AssetCategoryName;
        private string m_AssetGroupCode;        
        private string m_Name;        
        private DateTime m_PurchaseDate;       
        private float m_Quantity;
        private double m_PurchasePrice;
        private double m_PurchaseValue;
        private double m_CurrentPrice;
        private double m_CurrentValue;
        
        #endregion Fields


        #region Properties

        public int PersonalPortfolioId
        {
            get { return m_PersonalPortfolioId; }
            set { m_PersonalPortfolioId = value; }
        }
        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }
        public string AssetSubCategoryCode
        {
            get { return m_AssetSubCategoryCode; }
            set { m_AssetSubCategoryCode = value; }
        }
        public string AssetCategoryCode
        {
            get { return m_AssetCategoryCode; }
            set { m_AssetCategoryCode = value; }
        }
        public string AssetSubCategoryName
        {
            get { return m_AssetSubCategoryName; }
            set { m_AssetSubCategoryName = value; }
        }
        public string AssetCategoryName
        {
            get { return m_AssetCategoryName; }
            set { m_AssetCategoryName = value; }
        }
        public string AssetGroupCode
        {
            get { return m_AssetGroupCode; }
            set { m_AssetGroupCode = value; }
        }
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }
        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
        }
        public double PurchaseValue
        {
            get { return m_PurchaseValue; }
            set { m_PurchaseValue = value; }
        }
        public double CurrentPrice
        {
            get { return m_CurrentPrice; }
            set { m_CurrentPrice = value; }
        }
        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }
        #endregion Properties

    }
}
