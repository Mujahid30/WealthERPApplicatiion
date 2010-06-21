using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class DematAccountVo
    {
        private string _dpname;

        public string DpName
        {
            get { return _dpname; }
            set { _dpname = value; }
        }

        private string _modeofholding;

        public string ModeOfHolding
        {
            get { return _modeofholding; }
            set { _modeofholding = value; }
        }

        private string _dpid;

        public string DpId
        {
            get { return _dpid; }
            set { _dpid = value; }
        }
        private string _dpclientid;

        public string DpclientId
        {
            get { return _dpclientid; }
            set { _dpclientid = value; }
        }
        private DateTime _accountopeningdate;

        public DateTime AccountOpeningDate
        {
            get { return _accountopeningdate; }
            set { _accountopeningdate = value; }
        }
        private Int32 _isheldjointly;

        public Int32 IsHeldJointly
        {
            get { return _isheldjointly; }
            set { _isheldjointly = value; }
        }
        private string _beneficiaryaccountnbr;

        public string BeneficiaryAccountNbr
        {
            get { return _beneficiaryaccountnbr; }
            set { _beneficiaryaccountnbr = value; }
        }
        
    }
}
