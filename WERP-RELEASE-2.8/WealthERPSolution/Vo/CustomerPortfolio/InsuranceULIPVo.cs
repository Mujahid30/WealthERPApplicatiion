﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class InsuranceULIPVo
    {
        #region Fields
        private int m_CIUP_ULIPPlanId;        
        private int m_CIP_CustInsInvId;
        private string m_WUP_ULIPSubPlaCode;
        private float m_CIUP_AllocationPer;
        private float m_CIUP_Unit;
        private float m_CIUP_PurchasePrice;
        private DateTime m_CIUP_PurchaseDate; 
        private int m_CIUP_CreatedBy;
        private int m_CIUP_ModifiedBy;
        #endregion Fields

        #region Properties

        public int CIUP_ULIPPlanId
        {
            get { return m_CIUP_ULIPPlanId; }
            set { m_CIUP_ULIPPlanId = value; }
        }

        public int CIP_CustInsInvId
        {
            get { return m_CIP_CustInsInvId; }
            set { m_CIP_CustInsInvId = value; }
        }

        public string WUP_ULIPSubPlaCode
        {
            get { return m_WUP_ULIPSubPlaCode; }
            set { m_WUP_ULIPSubPlaCode = value; }
        }
       
        public float CIUP_AllocationPer
        {
            get { return m_CIUP_AllocationPer; }
            set { m_CIUP_AllocationPer = value; }
        }
        
        public float CIUP_Unit
        {
            get { return m_CIUP_Unit; }
            set { m_CIUP_Unit = value; }
        }
    
        public float CIUP_PurchasePrice
        {
            get { return m_CIUP_PurchasePrice; }
            set { m_CIUP_PurchasePrice = value; }
        }

        public DateTime CIUP_PurchaseDate
        {
            get { return m_CIUP_PurchaseDate; }
            set { m_CIUP_PurchaseDate = value; }
        }
       
        public int CIUP_CreatedBy
        {
            get { return m_CIUP_CreatedBy; }
            set { m_CIUP_CreatedBy = value; }
        }
       
        public int CIUP_ModifiedBy
        {
            get { return m_CIUP_ModifiedBy; }
            set { m_CIUP_ModifiedBy = value; }
        }

        #endregion Properties


    }
}
