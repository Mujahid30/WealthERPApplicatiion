using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoOps
{
    public class BondOrderVo : OrderVo
    {
        #region Fields

        private int m_BondOrderDetailsId;
        private int m_BondSchemeId;
        private int m_BondIssuerid;
        private string m_ModeOfHolding;
        private int m_IsJointlyHeld;
        private DateTime m_DepositDate;
        private DateTime m_MaturityDate;
        private int m_FaceValue;
        private int m_IsBuyBackFacility;
        private DateTime m_BuyBackDate;
        private double m_BuyBackAmount;
        private double m_Amount;
        private string m_Frequency;
        private int m_IsFormOfHoldingPhysical;
        private int m_AccountId;

        #endregion

        #region Properties

        public int BondOrderDetailsId
        {
            get { return m_BondOrderDetailsId; }
            set { m_BondOrderDetailsId = value; }
        }
        public int BondSchemeId
        {
            get { return m_BondSchemeId; }
            set { m_BondSchemeId = value; }
        }
        public int BondIssuerid
        {
            get { return m_BondIssuerid; }
            set { m_BondIssuerid = value; }
        }
        public string ModeOfHolding
        {
            get { return m_ModeOfHolding; }
            set { m_ModeOfHolding = value; }
        }
        public int IsJointlyHeld
        {
            get { return m_IsJointlyHeld; }
            set { m_IsJointlyHeld = value; }
        }
        public DateTime DepositDate
        {
            get { return m_DepositDate; }
            set { m_DepositDate = value; }
        }
        public DateTime MaturityDate
        {
            get { return m_MaturityDate; }
            set { m_MaturityDate = value; }
        }
        public int FaceValue
        {
            get { return m_FaceValue; }
            set { m_FaceValue = value; }
        }
        public int IsBuyBackFacility
        {
            get { return m_IsBuyBackFacility; }
            set { m_IsBuyBackFacility = value; }
        }
        public DateTime BuyBackDate
        {
            get { return m_BuyBackDate; }
            set { m_BuyBackDate = value; }
        }
        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        public double BuyBackAmount
        {
            get { return m_BuyBackAmount; }
            set { m_BuyBackAmount = value; }
        }
        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }
        public int IsFormOfHoldingPhysical
        {
            get { return m_IsFormOfHoldingPhysical; }
            set { m_IsFormOfHoldingPhysical = value; }
        }
        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        #endregion

    }
}
