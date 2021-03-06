﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class CollectiblesVo
    {
        #region Fields


        private int m_CollectibleId;       
        private string m_AssetCategoryCode;
        private string m_AssetCategoryName;

    
        private string m_AssetGroupCode;
        private int m_PortfolioId;
        private string m_Name;
        private double m_PurchasePrice;        
        private DateTime m_PurchaseDate;
        private double m_PurchaseValue;
        private double m_CurrentPrice;
        private double m_CurrentValue;       
        private float m_Quantity;
        private string m_Remark;

       

        #endregion Fields


        #region Properties
        public string AssetCategoryName
        {
            get { return m_AssetCategoryName; }
            set { m_AssetCategoryName = value; }
        }

        public int CollectibleId
        {
            get { return m_CollectibleId; }
            set { m_CollectibleId = value; }
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
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
        }
        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
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
        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }

        #endregion Properties
    }
}
