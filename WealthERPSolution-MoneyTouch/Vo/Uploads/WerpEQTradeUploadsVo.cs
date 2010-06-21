using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class WerpEQTradeUploadsVo
    {
        public Int32 ProcessID { get; set; }
        public Int32 XMLFileTypeId { get; set; }
        public Int64 TradeNumber { get; set; }
        public string TradeAccountNumber { get; set; }
        
        
        
        public string BrokerCode { get; set; }
        public string Exchange { get; set; }
        public string EducationCess { get; set; }
        public string Brokerage { get; set; }
        public string ServiceTax { get; set; }
        public string STT { get; set; }
        public string OtherCharges { get; set; }
        public string RateInclBrokerage { get; set; }
        public string TradeTotal { get; set; }
        public string BuySell { get; set; }
        public string OrderNum { get; set; }
        public string AccountId { get; set; }
        public string PortfolioId { get; set; }
        public string CustomerId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string PANNumber { get; set; }
    }
}
