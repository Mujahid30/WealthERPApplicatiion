using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoProductMaster
{
    public class ProductEquityScripVo
    {
        #region Fields

        private int m_ScripCode;
        private string m_CompanyName;
        private int m_SectorId;
        private int m_MarketLot;
        private float m_FaceValue;
        private DateTime m_BookClosure;
        private string m_Listing;
        private DateTime m_Incorporation;
        private DateTime m_Ticker;
           
        #endregion Fields



        #region Properties

        public int ScripCode
        {
            get { return m_ScripCode; }
            set { m_ScripCode = value; }
        }
        public int SectorId
        {
            get { return m_SectorId; }
            set { m_SectorId = value; }
        }
        public int MarketLot
        {
            get { return m_MarketLot; }
            set { m_MarketLot = value; }
        }
        public float FaceValue
        {
            get { return m_FaceValue; }
            set { m_FaceValue = value; }
        }
        public DateTime BookClosure
        {
            get { return m_BookClosure; }
            set { m_BookClosure = value; }
        }
        public string Listing
        {
            get { return m_Listing; }
            set { m_Listing = value; }
        }
        public DateTime Incorporation
        {
            get { return m_Incorporation; }
            set { m_Incorporation = value; }
        }
        public DateTime Ticker
        {
            get { return m_Ticker; }
            set { m_Ticker = value; }
        }

        #endregion Properties

    }
}
