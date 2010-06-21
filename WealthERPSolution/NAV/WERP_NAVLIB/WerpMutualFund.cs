namespace WERP_NAVLIB
{
    using FileHelpers;

    [DelimitedRecord("|")]
    internal class WerpMutualFund
    {
        public string SCHEMECODE;
        public string NAV_DATE;

        public string NAVRS;
        public string REPURPRICE;
        public string SALEPRICE;
        public string CLDATE;

        public string CHANGE;
        public string NETCHANGE;
     
        public string PREVNAV;

        public string PRENAVDATE;
       
       
        public string UPD_FLAG;

        
    }
    
}

