using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCalculator
{
    public class InstallmentVo
    {
        private int m_Period;

        public int Period
        {
            get { return m_Period; }
            set { m_Period = value; }
        }
        private DateTime m_InstallmentDate;

        public DateTime InstallmentDate
        {
            get { return m_InstallmentDate; }
            set { m_InstallmentDate = value; }
        }
        private double m_InstallmentValue;

        public double InstallmentValue
        {
            get { return m_InstallmentValue; }
            set { m_InstallmentValue = value; }
        }
        private double m_Principal;

        public double Principal
        {
            get { return m_Principal; }
            set { m_Principal = value; }
        }
        private double m_CummulativePrincipal;

        public double CummulativePrincipal
        {
            get { return m_CummulativePrincipal; }
            set { m_CummulativePrincipal = value; }
        }
        private double m_Interest;

        public double Interest
        {
            get { return m_Interest; }
            set { m_Interest = value; }
        }
        private double m_CummulativeInterestPaid;

        public double CummulativeInterestPaid
        {
            get { return m_CummulativeInterestPaid; }
            set { m_CummulativeInterestPaid = value; }
        }
        private double m_Balance;

        public double Balance
        {
            get { return m_Balance; }
            set { m_Balance = value; }
        }

    }
}
