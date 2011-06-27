using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class PropertyVo
    {
        #region Fields


        private int m_PropertyId;
        private int m_AccountId;      
        private string m_AssetSubCategoryCode;
        private string m_AssetSubCategoryName;
        private string m_AssetCategoryCode;
        private string m_AssetCategoryName;
        private string m_AssetGroupCode;
        private string m_MeasureCode;
        private string m_Name;       
        private string m_PropertyAdrLine1;       
        private string m_PropertyAdrLine2;       
        private string m_PropertyAdrLine3;       
        private string m_PropertyCity;       
        private string m_PropertyState;       
        private string m_PropertyCountry;       
        private int m_PropertyPinCode;        
        private DateTime m_PurchaseDate;
        private double m_PurchasePrice;       
        private float m_Quantity;
        private double m_CurrentPrice;
        private double m_CurrentValue;
        private double m_PurchaseValue;       
        private DateTime m_SellDate;        
        private float m_SellPrice;       
        private float m_SellValue;
        private string m_Remark;

        
        #endregion Fields


        #region Properties


        public int PropertyId
        {
            get { return m_PropertyId; }
            set { m_PropertyId = value; }
        }
        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
        public string AssetSubCategoryCode
        {
            get { return m_AssetSubCategoryCode; }
            set { m_AssetSubCategoryCode = value; }
        }
        public string AssetSubCategoryName
        {
            get { return m_AssetSubCategoryName; }
            set { m_AssetSubCategoryName = value; }
        }
        public string AssetCategoryCode
        {
            get { return m_AssetCategoryCode; }
            set { m_AssetCategoryCode = value; }
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
        public string PropertyAdrLine1
        {
            get { return m_PropertyAdrLine1; }
            set { m_PropertyAdrLine1 = value; }
        }
        public string PropertyAdrLine2
        {
            get { return m_PropertyAdrLine2; }
            set { m_PropertyAdrLine2 = value; }
        }
        public string PropertyAdrLine3
        {
            get { return m_PropertyAdrLine3; }
            set { m_PropertyAdrLine3 = value; }
        }
        public string PropertyCity
        {
            get { return m_PropertyCity; }
            set { m_PropertyCity = value; }
        }
        public string PropertyState
        {
            get { return m_PropertyState; }
            set { m_PropertyState = value; }
        }
        public string PropertyCountry
        {
            get { return m_PropertyCountry; }
            set { m_PropertyCountry = value; }
        }
        public int PropertyPinCode
        {
            get { return m_PropertyPinCode; }
            set { m_PropertyPinCode = value; }
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
        public double PurchaseValue
        {
            get { return m_PurchaseValue; }
            set { m_PurchaseValue = value; }
        }
        public DateTime SellDate
        {
            get { return m_SellDate; }
            set { m_SellDate = value; }
        }
        public float SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }
        public float SellValue
        {
            get { return m_SellValue; }
            set { m_SellValue = value; }
        }

        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }

        #endregion Properties



    }
}
