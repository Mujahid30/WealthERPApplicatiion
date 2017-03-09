using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class GoldVo
    {
        #region Fields


        
        private int m_GoldNPId;
        private string m_AssetCategoryCode;
        private string m_AssetCategoryName;

        
        private string m_AssetGroupCode;        
        
        private int m_PortfolioId;
        private string m_MeasureCode;        
        private string m_Name;        
        private DateTime m_PurchaseDate;        
        private double m_PurchasePrice;
        private float m_Quantity;
        private double m_PurchaseValue;
        private double m_CurrentPrice;
        private double m_CurrentValue;        
        private DateTime m_SellDate;
        private double m_SellPrice;
        private double m_SellValue;
        private string m_Remarks;

       
        

        #endregion Fields

        #region Properties

        public string AssetCategoryName
        {
            get { return m_AssetCategoryName; }
            set { m_AssetCategoryName = value; }
        }
        public int GoldNPId
        {
            get { return m_GoldNPId; }
            set { m_GoldNPId = value; }
        } 
        public string AssetCategoryCode
        {
            get { return m_AssetCategoryCode; }
            set { m_AssetCategoryCode = value; }
        }
        public string AssetGroupCode
        {
            get { return m_AssetGroupCode; }
            set { m_AssetGroupCode = value; }
        }
        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }
        public string MeasureCode
        {
            get { return m_MeasureCode; }
            set { m_MeasureCode = value; }
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
        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
        }
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
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
        public DateTime SellDate
        {
            get { return m_SellDate; }
            set { m_SellDate = value; }
        }
        public double SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }
        public double SellValue
        {
            get { return m_SellValue; }
            set { m_SellValue = value; }
        }
        public string Remarks
        {
            get { return m_Remarks; }
            set { m_Remarks = value; }
        }

        #endregion Properties


    }
}
