using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoWerpAdmin
{
    public class MappingVo
    {
        private string _Mode;
        //For Equity
        private string _WERPCOde;
        private string _ScripName;
        private string _Ticker;
        private DateTime _IncorporationDate;
        private DateTime _PublishDate;
        private Int32 _MarketLot;
        private Decimal _FaceValue;
        private DateTime _BookClosure;
        private string _InstrumentCategory;
        private string _SubCategory;
        private Int32 _Sector;
        private Int32 _MarketCap;
        private string _BSE;
        private string _NSE;
        private string _CERC;

        //ForMF
        private string _MFWERPCode;
        private string _SchemePlanName;
        private string _MFInstrumentCategory;
        private string _MFSubCategory;
        private string _MFSubSubCategory;
        private Int32 _MFMarketCap;
        private Int32 _MFSector;
        private string _AMFI;
        private string _CAMS;
        private string _Karvy;

        public string AMFI
        {
            get { return _AMFI; }
            set { _AMFI = value; }

        }
        public string CAMS
        {
            get { return _CAMS; }
            set { _CAMS = value; }

        }
        public string Karvy
        {
            get { return _Karvy; }
            set { _Karvy = value; }

        }
        public Int32 MFSector
        {
            get { return _MFSector; }
            set { _MFSector = value; }

        }
        public Int32 MFMarketCap
        {
            get { return _MFMarketCap; }
            set { _MFMarketCap = value; }

        }
        public string MFSubSubCategory
        {
            get { return _MFSubSubCategory; }
            set { _MFSubSubCategory = value; }

        }
        public string MFSubCategory
        {
            get { return _MFSubCategory; }
            set { _MFSubCategory = value; }

        }
        public string MFInstrumentCategory
        {
            get { return _MFInstrumentCategory; }
            set { _MFInstrumentCategory = value; }

        }
        public string SchemePlanName
        {
            get { return _SchemePlanName; }
            set { _SchemePlanName = value; }
        }
        public string MFWERPCode
        {
            get { return _MFWERPCode; }
            set { _MFWERPCode = value; }
        }
        public string CERC
        {
            get { return _CERC; }
            set { _CERC = value; }
        }
        public string NSE
        {
            get { return _NSE; }
            set { _NSE = value; }
        }
        public string BSE
        {
            get { return _BSE; }
            set { _BSE = value; }
        }
        public Int32 MarketCap
        {
            get { return _MarketCap; }
            set { _MarketCap = value; }
        }
        public Int32 Sector
        {
            get { return _Sector; }
            set { _Sector = value; }

        }
        public string SubCategory
        {
            get { return _SubCategory; }
            set { _SubCategory = value; }

        }
        public string InstrumentCategory
        {
            get { return _InstrumentCategory; }
            set { _InstrumentCategory = value; }


        }
        public DateTime BookClosure
        {
            get { return _BookClosure; }
            set { _BookClosure = value; }

        }
        public Decimal FaceValue
        {
            get { return _FaceValue; }
            set { _FaceValue = value; }

        }
        public Int32 MarketLot
        {
            get { return _MarketLot; }
            set { _MarketLot = value; }
        }
        public string WERPCOde
        {
            get { return _WERPCOde; }
            set { _WERPCOde = value; }
        }
        public string ScripName
        {
            get { return _ScripName; }
            set { _ScripName = value; }
        }
        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }
        public DateTime IncorporationDate
        {
            get { return _IncorporationDate; }
            set { _IncorporationDate = value; }

        }
        public DateTime PublishDate
        {
            get { return _PublishDate; }
            set { _PublishDate = value; }

        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

    }
}
